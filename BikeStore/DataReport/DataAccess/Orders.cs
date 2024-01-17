using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;


namespace DataAccess
{
    public class Orders: ConnectionDataBase
    {   
        public DataTable GetTable()
        {
            using (var conn = getConnection())
            {
                conn.Open();
                using (var cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "SELECT * FROM orders";
                    cmd.CommandType = CommandType.Text;

                    var reader = cmd.ExecuteReader();
                    var table = new DataTable();
                    table.Load(reader);
                    reader.Dispose();

                    return table;
                }
            }
        }
        public DataTable GetCustomers()
        {
            using (var conn = getConnection())
            {
                conn.Open();
                using (var cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "SELECT * FROM customers";
                    cmd.CommandType = CommandType.Text;

                    var reader = cmd.ExecuteReader();
                    var table = new DataTable();
                    table.Load(reader);
                    reader.Dispose();

                    return table;
                }
            }
        }
        public DataTable BuscarOrdenPorID(int productId)
        {
            using (var conn = getConnection())
            {
                conn.Open();
                using (var cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "SELECT * FROM orders WHERE order_id = @orderID";
                    cmd.Parameters.Add("@orderID", SqlDbType.Int).Value = productId;
                    cmd.CommandType = CommandType.Text;

                    var reader = cmd.ExecuteReader();
                    var table = new DataTable();
                    table.Load(reader);
                    reader.Dispose();

                    return table;
                }
            }
        }

        public int GuardarOrden(int customerID, DateTime orderDate)
        {
            using (var conn = getConnection())
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "INSERT INTO orders (customer_id, order_date) VALUES (@customerID, @orderDate); SELECT SCOPE_IDENTITY()";
                    cmd.Parameters.Add("@customerID", SqlDbType.Int).Value = customerID;
                    cmd.Parameters.Add("@orderDate", SqlDbType.Date).Value = orderDate;

                    
                    int orderID = Convert.ToInt32(cmd.ExecuteScalar());

                    return orderID; 
                }
            }
        }

        public void guardar( int orderID, int productID, int quantity, decimal price, decimal discount)
        {
            using (var conn = getConnection())
            {
                conn.Open();
                using (var cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "INSERT INTO order_items (order_id, product_id, quantity, price, discount) VALUES (@OrderID, @ProductID, @Quantity, @Price, @Discount)";
                   
                    cmd.Parameters.Add("@OrderID", SqlDbType.Int).Value = orderID;
                    cmd.Parameters.Add("@ProductID", SqlDbType.Int).Value = productID;
                    cmd.Parameters.Add("@Quantity", SqlDbType.Int).Value = quantity;
                    cmd.Parameters.Add("@Price", SqlDbType.Decimal).Value = price;
                    cmd.Parameters.Add("@Discount", SqlDbType.Decimal).Value = discount;

                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
