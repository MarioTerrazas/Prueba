using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class CD_cliente: ConnectionDataBase
    {
        public void Create(string firstname,string lastname, string phone, string email, string street, string city, string state,string zipcode)
        {
            using (var conn = getConnection())
            {
                conn.Open();
                using (var cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "INSERT INTO customers (first_name,last_name, phone, email,street, city, state, zip_code) VALUES (@firstname, @lastname, @phone,@email, @street, @city, @state, @zipcode)";

                    cmd.Parameters.Add("@firtsname", SqlDbType.VarChar).Value = firstname;
                    cmd.Parameters.Add("@lastname", SqlDbType.VarChar).Value = lastname;
                    cmd.Parameters.Add("@phone", SqlDbType.VarChar).Value = phone;
                    cmd.Parameters.Add("@email", SqlDbType.VarChar).Value = email;
                    cmd.Parameters.Add("@street", SqlDbType.VarChar).Value = street;
                    cmd.Parameters.Add("@city", SqlDbType.VarChar).Value = city;
                    cmd.Parameters.Add("@state", SqlDbType.VarChar).Value = state;
                    cmd.Parameters.Add("@zipcode", SqlDbType.VarChar).Value = zipcode;
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Update(int id,string firstname, string lastname, string phone,  string email ,string street, string city,string state, string zipcode)
        {
            using (var conn = getConnection())
            {
                conn.Open();
                using (var cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "UPDATE customers SET first_name = @firstname, last_name = @lastname, phone = @phone, email=@email, street=@street, city=@city, state=@state, zip_code=@zipcode WHERE customer_id = @id";
                    cmd.Parameters.Add("@id", SqlDbType.Int).Value = id;
                    cmd.Parameters.Add("@firtsname", SqlDbType.VarChar).Value = firstname;
                    cmd.Parameters.Add("@lastname", SqlDbType.VarChar).Value = lastname;
                    cmd.Parameters.Add("@phone", SqlDbType.VarChar).Value = phone;
                    cmd.Parameters.Add("@email", SqlDbType.VarChar).Value = email;
                    cmd.Parameters.Add("@street", SqlDbType.VarChar).Value = street;
                    cmd.Parameters.Add("@city", SqlDbType.VarChar).Value = city;
                    cmd.Parameters.Add("@state", SqlDbType.VarChar).Value = state;
                    cmd.Parameters.Add("@zipcode", SqlDbType.VarChar).Value = zipcode;
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Delete(int id)
        {
            using (var conn = getConnection())
            {
                conn.Open();
                using (var cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "DELETE FROM customers WHERE customer_id = @id";
                    cmd.Parameters.Add("@id", SqlDbType.Int).Value = id;

                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected == 0)
                    {
                        // Aquí puedes manejar el caso en que no se encontró ningún producto con el ID especificado.
                        // Puedes mostrar un mensaje de error o tomar otra acción adecuada.
                    }
                }
            }
        }


        public DataTable GetTable()
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
        public DataTable SearchById(int id)
        {
            using (var conn = getConnection())
            {
                conn.Open();
                using (var cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "SELECT * FROM customers WHERE customer_id = @id";
                    cmd.Parameters.Add("@id", SqlDbType.Int).Value = id;
                    cmd.CommandType = CommandType.Text;

                    var reader = cmd.ExecuteReader();
                    var table = new DataTable();
                    table.Load(reader);
                    reader.Dispose();

                    return table;
                }
            }
        }
        public DataTable SearchByName(string name)
        {
            using (var conn = getConnection())
            {
                conn.Open();
                using (var cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "SELECT * FROM customers WHERE first_name = @firstname";
                    cmd.Parameters.Add("@firstname", SqlDbType.VarChar).Value = name;
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
