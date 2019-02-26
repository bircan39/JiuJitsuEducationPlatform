﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Taijitan_Yoshin_Ryu_vzw.Models;

namespace Taijitan_Yoshin_Ryu_vzw.Data.Mappers {
    public class SessieConfiguration : IEntityTypeConfiguration<Sessie> {
        public void Configure(EntityTypeBuilder<Sessie> builder) {
            #region Table
            builder.ToTable("Sessie");
            #endregion

            #region Primary Key
            builder.HasKey(t => t.Tijdstip);
            #endregion

            #region Properties
            builder.Property(t => t.Tijdstip)
                .IsRequired();
            #endregion

            #region Relaties
            builder.HasMany(t => t.Lesgroepen)
                .WithOne();
            #endregion
        }
    }
}
