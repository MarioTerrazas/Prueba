using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
	public class OrdeDAO:ConnectionDataBase
	{
		public DataTable getSalesOrder(DateTime fromDate, DateTime todate)
		{
			using (var conn = getConnection())
			{
				conn.Open();
				using (var cmd = new SqlCommand())
				{
					cmd.Connection = conn;
					cmd.CommandText = @"select o.order_id,
										o.order_date,
										c.first_name+', '+ c.last_name as customer,
										products=stuff((select ' - ' +'x' +convert (varchar (10),oi2.quantity)+' '+ product_name
										from order_items oi2
										inner join products on products.product_id= oi2.product_id
										where oi2.order_id = oil.order_id
										for xml path('')), 1, 2, ''),
										sum( (quantity*price)-discount) as total_amount
										from orders o
										inner join customers c on c.customer_id=o.customer_id
										inner join order_items oil on oil.order_id=o.order_id
										where o.order_date between @fromDate and @todate
										group by o.order_id, oil.order_id, o.order_date, c.first_name, c.last_name
										order by o.order_id asc";
					cmd.Parameters.Add("@fromDate", SqlDbType.Date).Value = fromDate;
					cmd.Parameters.Add("@todate", SqlDbType.Date).Value = todate;
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
