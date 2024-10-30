using System;
using System.Threading;
using System.Threading.Tasks;
using Fs.Domain.Events.User;
using Fs.Domain.Exceptions.Team;
using Fs.Domain.Exceptions.User;
using Fs.Domain.SeedWork;
using Fs.Domain.Services;

namespace Fs.Domain.Aggregates.UserAggregate
{
    public class User : BaseEntity,
        IAggregateRoot
    {
        public string Email { get; private set; }

        public string PasswordHash { get; private set; }
        public string PasswordSalt { get; private set; }

        public UserStatus Status { get; private set; }
        public UserRole Role { get; private set; }

        public bool EmailConfirmed { get; private set; }
        public string EmailConfirmationToken { get; private set; }

        public Profile Profile { get; private set; }
        public Guid? TeamId { get; private set; }
        public Guid? OrganizationId { get; private set; }

        public User(Guid id)
            : base(id) { }
        protected User()
        {
        }

        public static async Task<User> CreateAsync(Guid id, string email, IUserService userService, CancellationToken cancellationToken)
        {
            if (await userService.HasEmailAsync(email, cancellationToken))
                throw new DuplicateEmailException("The email address is already being used.");

            var user = new User(id)
            {
                Email = email.Trim().ToLowerInvariant(),
                Status = UserStatus.Active
            };

            return user;
        }
        public static async Task<User> RegisterUserAsync(Guid id, string email, IUserService userService, CancellationToken cancellationToken)
        {
            if (await userService.HasEmailAsync(email, cancellationToken))
                throw new DuplicateEmailException("The email address is already being used.");

            var user = new User(id)
            {
                Email = email,
                Status = UserStatus.Inactive,
                EmailConfirmationToken = Guid.NewGuid().ToString()
            };

            user.RaiseEvent(new UserRegisteredDomainEvent());

            return user;
        }

        public void ChangePassword(string password, IPasswordHashGenerator generator)
        {
            var (hash, salt) = generator.Generate(password);
            PasswordHash = hash;
            PasswordSalt = salt;
        }
        public void UpdateProfile(Profile profile)
        {
            Profile = profile;
        }
        public void ChangeRole(UserRole role)
        {
            Role = role;
        }
        public async Task ChangeTeamAsync(Guid? teamId, ITeamService teamService, CancellationToken cancellationToken)
        {
            if (!teamId.HasValue)
            {
                TeamId = null;
                OrganizationId = null;
                return;
            }

            var team = await teamService.GetDetailsAsync(teamId.Value, cancellationToken);
            if (!team.HasValue)
                throw new TeamNotFoundException();

            TeamId = teamId;
            OrganizationId = team.Value.OrganizationId;
        }
    }
}