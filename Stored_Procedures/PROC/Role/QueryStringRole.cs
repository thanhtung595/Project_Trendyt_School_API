using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stored_Procedures.PROC.Role
{
    public static class QueryStringRole
    {
        public static string CheckIsNotNullGetRoles = @"
                        DECLARE @procExists INT

                        IF OBJECT_ID('GetRoles', 'P') IS NOT NULL
                        BEGIN
                            PRINT 'GetRoles đã tồn tại'
                            SET @procExists = 1
                        END
                        ELSE
                        BEGIN
                            SET @procExists = 0
                        END

                        SELECT @procExists";

        public static string CreateProcGetRoles = @"
                            CREATE PROCEDURE GetRoles
                            AS
                            BEGIN
                                SET NOCOUNT ON;
                                SELECT id_Role, name_Role FROM tbRole;
                            END";

    }
}
