using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlServerCe;

public class DBCheck
{

    public static void RunSQLFile(Database db, string data)
    {
        string[] Commands = data.Split(new string[] { "GO\r\n" }, StringSplitOptions.RemoveEmptyEntries);
        IDbCommand cmd = new SqlCeCommand();
        cmd.Connection = db.myconnection;
        foreach (string s in Commands)
        {
            cmd.CommandText = s;
            cmd.ExecuteNonQuery();
        }
    }

    public static void CheckTable(Database db, string TableName, Dictionary<string, string> Fields, string[] PrimaryKeys)
    {
        Fields.Add("LUDate", "[datetime]");
        Fields.Add("LUBy", "[nvarchar](50)");
        Fields.Add("CRC", "[nvarchar](50)");
        Fields.Add("MachineID", "[nvarchar](36)");
        Fields.Add("DirtyFlag", "[int]"); // 1-Server Updated 2-Owner Update 3- Other Client Update

        //create table
        string SQL = "CREATE TABLE [" + TableName + "] (";
        string PK = " PRIMARY KEY (";
        foreach (string s in PrimaryKeys)
        {
            SQL += " [" + s + "] " + Fields[s] + ",";
            PK += " [" + s + "] " + ",";
        }

        PK = PK.Substring(0, PK.Length - 1) + ") ";
        SQL = SQL + PK + ") ";

        string err = db.RunQuery(SQL);

        //check for fields
        foreach (string s in Fields.Keys)
        {
            string Err = db.RunQuery("ALTER TABLE [" + TableName + "] ADD [" + s + "] " + Fields[s]);
        }

    }

    public static string DBUpdate()
    {
        Database db = new Database(@"Data Source=|DataDirectory|\rapidInfo.sdf");


        try
        {
            int Counter = 0;
            while (DBCheck.UpdateDBStructure(db, ++Counter))
            {
            }
            return "rapidInfo db Done <br/>";
        }
        catch (Exception ex)
        {
            Cmn.LogError(ex, "DBUpdate");
            return ex.Message;
        }
        finally
        {
            db.Close();
        }
    }

    public static Boolean UpdateDBStructure(Database db, int Counter)
    {
        Dictionary<string, string> fields = new Dictionary<string, string>();

        switch (Counter)
        {

            case 1://Area

                fields.Add("Id", "[int]");
                fields.Add("Name", "[nvarchar](1000)");
                fields.Add("ParentId", "[int]");
                
                CheckTable(db, "Area", fields, new string[] { "Id" });
                break;

            case 2://Identity

                fields.Add("Id", "[int]");
                fields.Add("Name", "[nvarchar](1000)");
                fields.Add("DetailsLength", "[int]");
                fields.Add("Details", "[image]");
                fields.Add("LastUpdate", "[datetime]");

                CheckTable(db, "Identity", fields, new string[] { "Id" });
                break;

            case 3://AreaLink

                fields.Add("AreaId", "[int]");
                fields.Add("IdentityId", "[int]");

                CheckTable(db, "AreaLink", fields, new string[] { "IdentityId", "AreaId" });
                break;
        }
        return true;
    }
}
