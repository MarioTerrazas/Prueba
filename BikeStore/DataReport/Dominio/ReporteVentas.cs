using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class ReporteVentas
    {
		public DateTime reportDate { get; private set; }
		public DateTime startDate { get; private set; }
		public DateTime endDate { get; private set; }
		public List<SaleListing> saleListings { get; private set; }

		public List<NetSalesByPeriod> netSalesByPeriods { get; private set; }
		public double totalnetSales { get; private set; }

		public void createSalesOrderreport(DateTime fromDate, DateTime toDate)
		{
			reportDate = DateTime.Now;
			startDate = fromDate;
			endDate = toDate;
			var orderDAO = new OrdeDAO();
			var result = orderDAO.getSalesOrder(fromDate, toDate);
			saleListings = new List<SaleListing>();
			foreach (System.Data.DataRow row in result.Rows)
			{
				var salesModel = new SaleListing()
				{
					orderid = Convert.ToInt32(row[0]),
					orderDate = Convert.ToDateTime(row[1]),
					customer = Convert.ToString(row[2]),
					products = Convert.ToString(row[3]),
					totalAmount = Convert.ToDouble(row[4])
				};
				saleListings.Add(salesModel);
				totalnetSales += Convert.ToDouble(row[4]);


			}
			var listaSelecBydate = (
				from sales in saleListings
				group sales by sales.orderDate
				into listSales
				select new
				{
					date = listSales.Key,
					amount = listSales.Sum(item => item.totalAmount)
				}).AsEnumerable();
			int rotalDays = Convert.ToInt32((toDate - fromDate).Days);

			if (rotalDays <= 7)
			{
				netSalesByPeriods = (from sales in listaSelecBydate
									 group sales by
									 System.Globalization.CultureInfo.CurrentCulture.Calendar.GetWeekOfYear(
										 sales.date, System.Globalization.CalendarWeekRule.FirstDay, DayOfWeek.Monday)
									 into listSales
									 select new NetSalesByPeriod
									 {
										 period = "Week" + listSales.Key.ToString(),
										 netSales = listSales.Sum(item => item.amount)
									 }).ToList();
			}
			else if (rotalDays <= 30)
			{
				netSalesByPeriods = (from sales in listaSelecBydate
									 group sales by sales.date.ToString("dd-MMM-yyyy")
									 into listSales
									 select new NetSalesByPeriod
									 {
										 period = listSales.Key,
										 netSales = listSales.Sum(item => item.amount)
									 }).ToList();
			}
			else if (rotalDays <= 365)
			{
				netSalesByPeriods = (from sales in listaSelecBydate
									 group sales by sales.date.ToString("MMM-yyyy")
									 into listSales
									 select new NetSalesByPeriod
									 {
										 period = listSales.Key,
										 netSales = listSales.Sum(item => item.amount)
									 }).ToList();
			}
			else
			{
				netSalesByPeriods = (from sales in listaSelecBydate
									 group sales by sales.date.ToString("yyyy")
									 into listSales
									 select new NetSalesByPeriod
									 {
										 period = listSales.Key,
										 netSales = listSales.Sum(item => item.amount)
									 }).ToList();
			}


		}


	}
}
