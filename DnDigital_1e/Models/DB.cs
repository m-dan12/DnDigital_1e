using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;
using System.Data;

namespace DnDigital_1e.Models;
class DB
{
    private NpgsqlConnection connection;
    public DB()
    {
        string connectionString = "Server=your_server;Port=your_port;Database=your_database;User Id=your_user;Password=your_password;";
        connection = new NpgsqlConnection(connectionString);
        connection.Open();
    }
    public void Do()
    {
        string query = "SELECT * FROM your_table;";
        NpgsqlCommand command = new NpgsqlCommand(query, connection);
        NpgsqlDataReader reader = command.ExecuteReader();
        while (reader.Read())
        {
            Console.WriteLine(reader["column_name"]); // Access the result data
        }
        reader.Close();
    }
    public void CloseConnection()
    {
        connection.Close();
    }

}