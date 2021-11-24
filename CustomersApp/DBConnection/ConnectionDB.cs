using System;
using System.Data;
using Npgsql;

namespace CustomersApp.DBConnection
{
    public class ConnectionDB
    {
        
        public NpgsqlConnection DBConnection()
        {
            var connString = "Host=localhost;Username=customersdata;Password=customersdata;Database=customersdata";
            try
            {
                var conn = new NpgsqlConnection(connString);
                if(conn != null)
                {
                    Console.WriteLine("Connected");
                    conn.Open();
                    return conn;
                }

            }
            catch (Exception e)
            {
                Console.WriteLine("Connection error!!!! " + e);
            }
            return null;
        }
    }
}
