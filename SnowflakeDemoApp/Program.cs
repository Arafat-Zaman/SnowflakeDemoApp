using System;
using System.Data;
using Snowflake.Data.Client;

namespace SnowflakeDemoApp
{
    class Program
    {
        static void Main(string[] args)
        {
            // Snowflake connection string (Update with your credentials)
            string connectionString = "account=<your_account_name>;user=<your_username>;password=<your_password>;role=<your_role>;warehouse=<your_warehouse>;database=<your_database>;schema=<your_schema>";

            try
            {
                // Create a connection to Snowflake
                using (IDbConnection conn = new SnowflakeDbConnection())
                {
                    conn.ConnectionString = connectionString;
                    conn.Open();

                    Console.WriteLine("Connected to Snowflake!");

                    // Execute a query to fetch some data
                    using (IDbCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = "SELECT CURRENT_TIMESTAMP();";
                        using (IDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Console.WriteLine("Current Timestamp: " + reader.GetDateTime(0));
                            }
                        }
                    }

                    // Close the connection
                    conn.Close();
                }
            }
            catch (SnowflakeDbException ex)
            {
                Console.WriteLine("Snowflake Error: " + ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("General Error: " + ex.Message);
            }
        }
    }
}
