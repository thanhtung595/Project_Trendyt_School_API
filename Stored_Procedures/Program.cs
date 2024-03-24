using Stored_Procedures.DataConnectionMSSQL;
using Stored_Procedures.PROC.Role;
using System;
using System.Data.SqlClient;

class Program
{
    static void Main()
    {
        // Tạo một thực thể của lớp PROC_tbRole implement giao diện IPROC_tbRole
        IPROC_tbRole proc_tbRole = new PROC_tbRole();
        // Gọi phương thức CreateProc_GetRoles từ thực thể proc_tbRole
        bool create = proc_tbRole.CreateProc_GetRoles();
    }
}
