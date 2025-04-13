using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        string connectionString = "Server=localhost;Database=SeninVeritabaniAdin;Trusted_Connection=True;";
        var tables = GetTableNames(connectionString);

        foreach (var table in tables)
        {
            string classCode = GenerateClassForTable(connectionString, table);
            Console.WriteLine(classCode);
            Console.WriteLine("\n-----------------------------\n");
        }
    }

    static List<string> GetTableNames(string connectionString)
    {
        var tableNames = new List<string>();
        using (var conn = new SqlConnection(connectionString))
        {
            conn.Open();
            var cmd = new SqlCommand("SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE = 'BASE TABLE'", conn);
            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    tableNames.Add(reader.GetString(0));
                }
            }
        }
        return tableNames;
    }

    static string GenerateClassForTable(string connectionString, string tableName)
    {
        var sb = new StringBuilder();
        sb.AppendLine($"public class {tableName}");
        sb.AppendLine("{");

        using (var conn = new SqlConnection(connectionString))
        {
            conn.Open();
            string query = @"SELECT COLUMN_NAME, DATA_TYPE FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = @TableName";
            var cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@TableName", tableName);

            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    string columnName = reader.GetString(0);
                    string sqlType = reader.GetString(1);
                    string csharpType = MapSqlTypeToCSharp(sqlType);

                    sb.AppendLine($"    public {csharpType} {columnName} {{ get; set; }}");
                }
            }
        }

        sb.AppendLine("}");
        return sb.ToString();
    }

    static string MapSqlTypeToCSharp(string sqlType)
    {
        return sqlType switch
        {
            "int" => "int",
            "bigint" => "long",
            "smallint" => "short",
            "tinyint" => "byte",
            "bit" => "bool",
            "float" => "double",
            "real" => "float",
            "decimal" => "decimal",
            "numeric" => "decimal",
            "money" => "decimal",
            "smallmoney" => "decimal",
            "varchar" => "string",
            "nvarchar" => "string",
            "text" => "string",
            "ntext" => "string",
            "char" => "string",
            "nchar" => "string",
            "datetime" => "DateTime",
            "smalldatetime" => "DateTime",
            "date" => "DateTime",
            "time" => "TimeSpan",
            "uniqueidentifier" => "Guid",
            _ => "object"
        };
    }
}
