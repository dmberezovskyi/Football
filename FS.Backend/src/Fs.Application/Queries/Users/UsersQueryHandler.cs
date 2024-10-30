using System.Linq;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Fs.Application.Extensions;
using Fs.Application.Permissions.User.Pep;
using Fs.Infrastructure.ReadStorage.Abstractions;
using Fs.Infrastructure.ReadStorage.Entities;
using Microsoft.EntityFrameworkCore;

namespace Fs.Application.Queries.Users
{
    public sealed class UsersQueryHandler :
        IRequestHandler<UsersQuery, QueryResult<UserReadModel>>
    {
        private readonly IReadRepository _repository;
        private readonly IMediator _mediator;

        public UsersQueryHandler(IReadRepository repository, IMediator mediator)
        {
            _repository = repository;
            _mediator = mediator;
        }

        public async Task<QueryResult<UserReadModel>> Handle(UsersQuery request, CancellationToken cancellationToken)
        {
            await _mediator.Send(new EnforceListUsers(new EnforceListUsersResource(request.Ids, request.Parts)), cancellationToken);

            var filterQuery = BuildFilter(request);

            var orderedQuery = filterQuery
                .OrderByDescending(x => x.CreatedOn);

            var items = await BuildSelect(orderedQuery, request.Parts)
                .Skip(request.Skip)
                .Take(request.Take)
                .ToArrayAsync(cancellationToken);

            return new QueryResult<UserReadModel>
            {
                Items = items,
                Total = await GetTotalAsync(request, filterQuery, items, cancellationToken)
            };
        }

        private IQueryable<UserEntity> BuildFilter(UsersQuery request)
        {
            var query = _repository.GetAll<UserEntity>()
                .AsNoTracking();

            if (request.Ids == null || !request.Ids.Any())
                return query;

            var ids = request.Ids.Distinct().ToArray();

            return ids.Length == 1
                ? query.Where(x => x.Id == ids[0])
                : query.Where(x => ids.Contains(x.Id));
        }
        private static IQueryable<UserReadModel> BuildSelect(IQueryable<UserEntity> filterQuery, string[] parts)
        {
            var isIncludeSnippet = parts.Contains(Constants.User.Query.Parts.Snippet);
            var isIncludeProfile = parts.Contains(Constants.User.Query.Parts.Profile);
            var isIncludeTeam = parts.Contains(Constants.User.Query.Parts.Team);
            var isIncludeOrganization = parts.Contains(Constants.User.Query.Parts.Organization);
            var isIncludeUserInfo = parts.Contains(Constants.User.Query.Parts.UserInfo);
            var isIncludeUserInfoDetailed = parts.Contains(Constants.User.Query.Parts.UserInfoDetailed);

            return filterQuery
                .Select(x => new UserReadModel
                {
                    Id = x.Id,
                    Version = x.Version,
                    Snippet = isIncludeSnippet
                        ? new UserReadModel.SnippetModel
                        {
                            FirstName = x.Profile.FirstName,
                            MiddleName = x.Profile.MiddleName,
                            LastName = x.Profile.LastName,
                            BirthDate = x.Profile.BirthDate,
                            Role = x.Role.ToString().ToCamelCase()
                        }
                        : null,
                    Profile = isIncludeProfile 
                        ? new UserReadModel.ProfileModel
                        {
                            FirstName = x.Profile.FirstName,
                            MiddleName = x.Profile.MiddleName,
                            LastName = x.Profile.LastName,
                            BirthDate = x.Profile.BirthDate,
                            Email = x.Email,
                            About = x.Profile.About,
                            Phone = x.Profile.Phone,
                            Address = x.Profile.Address != null
                            ? new AddressReadModel
                            {
                                Country = x.Profile.Address.Country,
                                State = x.Profile.Address.State,
                                City = x.Profile.Address.City,
                                ZipCode = x.Profile.Address.ZipCode,
                                StreetAddress = x.Profile.Address.StreetAddress
                            }
                            : null
                        }
                        : null,
                    Team = isIncludeTeam && x.Team != null
                        ? new UserReadModel.TeamModel
                        {
                            Id = x.Team.Id,
                            Name = x.Team.Name
                        }
                        : null,
                    Organization = isIncludeOrganization  && x.Organization != null
                        ? new UserReadModel.OrganizationModel
                        {
                            Id = x.Organization.Id,
                            Name = x.Organization.Name
                        }
                        : null,
                    UserInfo = isIncludeUserInfo ? new UserReadModel.UserInfoModel
                    {
                        FirstName = x.Profile.FirstName,
                        LastName = x.Profile.LastName,
                        Role = x.Role.ToString().ToCamelCase(),
                        Email = x.Email
                    } : null,
                    UserInfoDetailed = isIncludeUserInfoDetailed ? new UserReadModel.UserInfoDetailedModel
                    {
                        Status = x.Status,
                        CreatedOn = x.CreatedOn,
                        UpdatedOn = x.UpdatedOn
                        //EmailConfirmed = x.EmailConfirmed
                    } : null
                });
        }
        private static async Task<int?> GetTotalAsync<TResult>(UsersQuery request, IQueryable<UserEntity> filterQuery, TResult[] result, CancellationToken cancellationToken)
        {
            if (request.Ids != null && request.Ids.Any()) 
                return result.Length;
            if (result.Length < request.Take)
                return request.Skip + result.Length;
            return await filterQuery.CountAsync(cancellationToken);
        }
    }
}