using App_Models.Models_Table_CSDL;
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
        }

        public DbSet<tbRole> tbRole { get; set; }
        public DbSet<tbRoleSchool> tbRoleSchool { get; set; }
        public DbSet<tbTypeAccount> tbTypeAccount { get; set; }
        public DbSet<tbAccount> tbAccount { get; set; }
        public DbSet<tbToken> tbToken { get; set; }
        public DbSet<tbSchool> tbSchool { get; set; }
        public DbSet<tbKhoaSchool> tbKhoaSchool { get; set; }
        public DbSet<tbClassSchool> tbClassSchool { get; set; }
        public DbSet<tbMenberSchool> tbMenberSchool { get; set; }
        public DbSet<tbMonHoc> tbMonHoc { get; set; }
        public DbSet<tbClassSchool_MonHoc> tbClassSchool_MonHoc { get; set; }
        public DbSet<tbBuoiHoc> tbBuoiHoc { get; set; }
        public DbSet<tbBuoiHoc_MonHoc> tbBuoiHoc_MonHoc { get; set; }
        public DbSet<tbClassSchool_Menber> tbClassSchool_Menber { get; set; }
        public DbSet<tbMonHocClass_Student> tbMonHocClass_Student { get; set; }
    }
}
