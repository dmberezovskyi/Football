using System;
using System.Linq.Expressions;
using Fs.Domain.Values;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fs.Infrastructure.Storage.Extensions
{
    internal static class TypeBuilders
    {
        public static EntityTypeBuilder<TEntity> HasAddress<TEntity>(this EntityTypeBuilder<TEntity> builder, Expression<Func<TEntity, Address>> navigationExpression) 
            where TEntity : class
        {
            return builder.OwnsOne(navigationExpression, addressBuilder =>
            {
                addressBuilder.Property(x => x.Country).IsRequired().HasMaxLength(32);
                addressBuilder.Property(x => x.State).IsRequired().HasMaxLength(32);
                addressBuilder.Property(x => x.City).IsRequired().HasMaxLength(32);
                addressBuilder.Property(x => x.ZipCode).IsRequired().HasMaxLength(12);
                addressBuilder.Property(x => x.StreetAddress).IsRequired().HasMaxLength(64);
            });
        }

        public static OwnedNavigationBuilder<TEntity, TDependentEntity> HasAddress<TEntity, TDependentEntity>(this OwnedNavigationBuilder<TEntity, TDependentEntity> builder, Expression<Func<TDependentEntity, Address>> navigationExpression)
            where TEntity : class 
            where TDependentEntity : class
        {
            return builder.OwnsOne(navigationExpression, addressBuilder =>
            {
                addressBuilder.Property(x => x.Country).IsRequired().HasMaxLength(32);
                addressBuilder.Property(x => x.State).IsRequired().HasMaxLength(32);
                addressBuilder.Property(x => x.City).IsRequired().HasMaxLength(32);
                addressBuilder.Property(x => x.ZipCode).IsRequired().HasMaxLength(12);
                addressBuilder.Property(x => x.StreetAddress).IsRequired().HasMaxLength(64);
            });
        }
    }
}
