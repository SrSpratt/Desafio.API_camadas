using System;
using System.IO;
using System.Data.SqlClient;

class ScriptExecutor
{
    static void Main()
    {
        string serverConnectionString = "Server=.\\SQLEXPRESS01;Trusted_Connection=True;TrustServerCertificate=true";
        string scriptPath = ""; 

        System.Console.WriteLine("1 to Create, and 2 to Populate the database:");
        var option = Console.ReadLine();

        try
        {
            if (option == 1.ToString())
                scriptPath = "CreateDB.sql";
            else
                scriptPath = "PopulateDB.sql";
            ExecuteSqlScript(serverConnectionString, scriptPath);
            System.Console.WriteLine("Script SQL executado com sucesso.");
        }
        catch (Exception ex)
        {
            System.Console.WriteLine($"Erro: {ex.Message}");
        }
    }

    static void ExecuteSqlScript(string connectionString, string scriptPath)
    {
        string script = File.ReadAllText(scriptPath);

        string[] commands = script.Split(new[] { ";" }, StringSplitOptions.RemoveEmptyEntries);

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();

            foreach (string commandText in commands)
            {
                if (!string.IsNullOrWhiteSpace(commandText))
                {
                    using (SqlCommand command = new SqlCommand(commandText, connection))
                    {
                        command.ExecuteNonQuery();
                    }
                }
            }
            connection.Close();
        }
    }
}