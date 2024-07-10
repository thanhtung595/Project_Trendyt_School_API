using App_Models.Models_Table_CSDL;
using Lib_DataEntity.ContextSeed;
using Lib_DataEntity.IndexEntity;
using Lib_Models.Models_Table_Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App_DataBaseEntity.DbContextEntity_SQL_Sever
{
    public class Trendyt_DbContext : DbContext
    {
        public Trendyt_DbContext(DbContextOptions options) : base(options) { }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //modelBuilder.Entity<tbRole>().HasData(SeedData.tbRoles);
            //modelBuilder.Entity<tbRoleSchool>().HasData(SeedData.tbRoleSchool);
            //modelBuilder.Entity<tbTypeAccount>().HasData(SeedData.tbTypeAccount);
            //modelBuilder.Entity<tbTag>().HasData(SeedData.tbTag);
            //modelBuilder.Entity<tbStyleBuoiHoc>().HasData(SeedData.tbStyleBuoiHoc);
            modelBuilder.Entity<tbTypeThongBao>().HasData(SeedData.tbTypeThongBaos);
            modelBuilder.Entity<tbAccount>(IndexCongiguration.IndexAccount());
            modelBuilder.Entity<tbToken>(IndexCongiguration.IndexToken());
        }

        public DbSet<tbRole> tbRole { get; set; }
        public DbSet<tbRoleSchool> tbRoleSchool { get; set; }
        public DbSet<tbTag> tbTag { get; set; }
        public DbSet<tbTypeAccount> tbTypeAccount { get; set; }
        public DbSet<tbStyleBuoiHoc> tbStyleBuoiHoc { get; set; }
        public DbSet<tbAccount> tbAccount { get; set; }
        public DbSet<tbToken> tbToken { get; set; }
        public DbSet<tbSchool> tbSchool { get; set; }
        public DbSet<tbKhoaSchool> tbKhoaSchool { get; set; }
        public DbSet<tbClassSchool> tbClassSchool { get; set; }
        public DbSet<tbMenberSchool> tbMenberSchool { get; set; }
        public DbSet<tbMonHoc> tbMonHoc { get; set; }
        public DbSet<tbClassSchool_Menber> tbClassSchool_Menber { get; set; }
        public DbSet<tbMonHocClass_Student> tbMonHocClass_Student { get; set; }
        public DbSet<tbLichHoc> tbLichHoc { get; set; }
        public DbSet<tbDiemDanh> tbDiemDanh { get; set; }
        public DbSet<tbTypeThongBao> tbTypeThongBao { get; set; }
        public DbSet<tbThongBao> tbThongBao { get; set; }
        public DbSet<tbMiddlewareThongBao> tbMiddlewareThongBao { get; set; }
        public DbSet<tbFileThongBao> tbFileThongBao { get; set; }

    }
}
