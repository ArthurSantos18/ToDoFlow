using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoFlow.Domain.Models;

namespace ToDoFlow.Infrastructure.Context.Configurations
{
    public class UserRefreshTokenConfiguration : IEntityTypeConfiguration<UserRefreshToken>
    {
        public void Configure(EntityTypeBuilder<UserRefreshToken> builder)
        {
            builder.ToTable("user_refresh_token");

            builder.HasKey(x => x.Id);

            builder.Property(t => t.Id).HasColumnName("id").HasColumnType("int");
            builder.Property(t => t.RefreshToken).HasColumnName("refresh_token").HasColumnType("varchar(100)");
            builder.Property(t => t.Expiration).HasColumnName("expiration").HasColumnType("datetime");

            builder.Property(t => t.UserId).HasColumnName("user_id").HasColumnType("int");
        }
    }
}
