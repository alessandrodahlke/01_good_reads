﻿using GoodReadas.Users.Domain.Entities;
using GoodReads.Core.DomainObjects.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GoodReads.Users.Infra.Persistence.Mappings
{
    public class UserMapping : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Name)
                .IsRequired()
                .HasColumnType("varchar(255)")
                .HasColumnName("Name");

            builder.OwnsOne(c => c.Email, cm =>
            {
                cm.Property(c => c.Address)
                    .IsRequired()
                    .HasColumnName("Email")
                    .HasMaxLength(Email.AddressMaxLength)
                    .HasColumnType($"varchar({Email.AddressMaxLength})");
            });

            builder.ToTable("Users");
        }
    }
}
