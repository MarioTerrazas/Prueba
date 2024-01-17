using System;
using System.Data;
using System.Data.SqlClient;

namespace DataAccess
{
    public class ProductDAO : ConnectionDataBase
    {
        public void CreateProduct(string productName, int modelYear, decimal price)
        {
            using (var conn = getConnection())
            {
                conn.Open();
                using (var cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "INSERT INTO products (product_name, model_year, price) VALUES (@productName, @modelYear, @price)";
            
                    cmd.Parameters.Add("@productName", SqlDbType.VarChar).Value = productName;
                    cmd.Parameters.Add("@modelYear", SqlDbType.SmallInt).Value = modelYear;
                    cmd.Parameters.Add("@price", SqlDbType.Decimal).Value = price;

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void UpdateProduct(int productId, string productName, int modelYear, decimal price)
        {
            using (var conn = getConnection())
            {
                conn.Open();
                using (var cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "UPDATE products SET product_name = @productName, model_year = @modelYear, price = @price WHERE product_id = @productId";
                    cmd.Parameters.Add("@productId", SqlDbType.Int).Value = productId;
                    cmd.Parameters.Add("@productName", SqlDbType.VarChar).Value = productName;
                    cmd.Parameters.Add("@modelYear", SqlDbType.SmallInt).Value = modelYear;
                    cmd.Parameters.Add("@price", SqlDbType.Decimal).Value = price;

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void DeleteProduct(int productId)
        {
            using (var conn = getConnection())
            {
                conn.Open();
                using (var cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "DELETE FROM products WHERE product_id = @productId";
                    cmd.Parameters.Add("@productId", SqlDbType.Int).Value = productId;

                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected == 0)
                    {
                        // Aquí puedes manejar el caso en que no se encontró ningún producto con el ID especificado.
                        // Puedes mostrar un mensaje de error o tomar otra acción adecuada.
                    }
                }
            }
        }


        public DataTable GetProducts()
        {
            using (var conn = getConnection())
            {
                conn.Open();
                using (var cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "SELECT * FROM products";
                    cmd.CommandType = CommandType.Text;

                    var reader = cmd.ExecuteReader();
                    var table = new DataTable();
                    table.Load(reader);
                    reader.Dispose();

                    return table;
                }
            }
        }
        public DataTable SearchProductById(int productId)
        {
            using (var conn = getConnection())
            {
                conn.Open();
                using (var cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "SELECT * FROM products WHERE product_id = @productId";
                    cmd.Parameters.Add("@productId", SqlDbType.Int).Value = productId;
                    cmd.CommandType = CommandType.Text;

                    var reader = cmd.ExecuteReader();
                    var table = new DataTable();
                    table.Load(reader);
                    reader.Dispose();

                    return table;
                }
            }
        }
        public DataTable SearchProductByName(string productName)
        {
            using (var conn = getConnection())
            {
                conn.Open();
                using (var cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "SELECT * FROM products WHERE product_name = @productName";
                    cmd.Parameters.Add("@productName", SqlDbType.VarChar).Value = productName;
                    cmd.CommandType = CommandType.Text;

                    var reader = cmd.ExecuteReader();
                    var table = new DataTable();
                    table.Load(reader);
                    reader.Dispose();

                    return table;
                }
            }
        }



    }

}

