using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Cards.Models
{
    public class QueryTraderDB
    {
        private string CS { get; set; }

        public QueryTraderDB()
        {
            CS = Environment.GetEnvironmentVariable("TRADERJOESCS");
        }

        public Product GetProduct(int id)
        {
            //Product product = new Product();
            Product product = new Product();

            // establish sql connection
            using(SqlConnection sqlConnection = new SqlConnection(CS))
            {
                // query
                string query = "SELECT * FROM Product"
                    + $" WHERE ID = {id};";
                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);

                // open connection
                sqlConnection.Open();

                // get product
                using(SqlDataReader reader = sqlCommand.ExecuteReader())
                {
                    while(reader.Read())
                    {
                        product
                            = new Product((int)reader[0], (string)reader[1], (string)reader[2], (string)reader[3],
                            (string)reader[4], (string)reader[5], (string)reader[6]);
                    }
                }

                // close connection
                sqlConnection.Close();
            }

            return product;
        }

        public List<Product> GetAllProducts()
        {
            List<Product> products = new List<Product>();

            // establish sql connection
            using (SqlConnection sqlConnection = new SqlConnection(CS))
            {
                // query
                string query = "SELECT * FROM Product";
                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);

                // open connection
                sqlConnection.Open();

                // get product
                using (SqlDataReader reader = sqlCommand.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        products.Add(new Product((int)reader[0], (string)reader[1], (string)reader[2], (string)reader[3],
                            (string)reader[4], (string)reader[5], (string)reader[6]));
                    }
                }

                // close connection
                sqlConnection.Close();
            }

            return products;
        }

        public List<Product> Search(string name)
        {
            List<Product> products = new List<Product>();

            return products;
        }
    }
}
