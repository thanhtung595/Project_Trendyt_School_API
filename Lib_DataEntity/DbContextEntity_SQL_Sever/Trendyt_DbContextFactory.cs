using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App_DataBaseEntity.DbContextEntity_SQL_Sever
{
    public class Trendyt_DbContextFactory : IDesignTimeDbContextFactory<Trendyt_DbContext>
    {
        public Trendyt_DbContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configurationRoot = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var connectString = configurationRoot.GetConnectionString("DataBase_Trendyt_School");
            var optionBuider = new DbContextOptionsBuilder<Trendyt_DbContext>();
            optionBuider.UseSqlServer(connectString);
            return new Trendyt_DbContext(optionBuider.Options);
        }
    }
}
