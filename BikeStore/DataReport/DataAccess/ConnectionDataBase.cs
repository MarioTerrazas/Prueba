using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
namespace DataAccess
{
	public abstract class ConnectionDataBase
    {
		protected SqlConnection getConnection()
		{
			return new SqlConnection("Server=DESKTOP-7DFO0MN; DataBase=Bike_Store; integrated security=true");
		}
	}
}
