using App_Models.Models_Table_CSDL;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Dapper.SqlMapper;

namespace Lib_DataEntity.IndexEntity
{
    public static class IndexCongiguration
    {
        // Action : phương thức delegate
        public static Action<EntityTypeBuilder<tbAccount>> IndexAccount() 
        {
            return entity =>
            {
                entity.HasIndex(ac => ac.user_Name).IsUnique(true);
            };
        }

        public static Action<EntityTypeBuilder<tbToken>> IndexToken()
        {
            return entity =>
            {
                entity.HasIndex(t => t.key_refresh_Token).IsUnique(true);
            };
        }
    }
}
