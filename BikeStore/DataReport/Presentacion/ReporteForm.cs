using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Dominio;
using Microsoft.Reporting.WinForms;

namespace Presentacion
{
	public partial class ReporteForm : Form
	{
		public ReporteForm()
		{
			InitializeComponent();
		}

		private void Form1_Load(object sender, EventArgs e)
		{

            this.reportViewer2.RefreshReport();
        }

        private void GetSalesReport(DateTime start, DateTime end)
		{
			ReporteVentas reportModel = new ReporteVentas();
			reportModel.createSalesOrderreport(start, end);
			var binding = new BindingSource();
			binding.DataSource = reportModel;
			this.reportViewer2.Reset();
			this.reportViewer2.ProcessingMode = ProcessingMode.Local;
			this.reportViewer2.LocalReport.ReportPath = @"E:\ingenieria -upds-mario eduardo\Desarrollo de sistemas II\BikeStore\BikeStore\BikeStore\DataReport\Report2.rdlc";
			this.reportViewer2.LocalReport.DataSources.Clear();
			this.reportViewer2.LocalReport.DataSources.Add(new ReportDataSource("reporteVentas", binding));
			this.reportViewer2.LocalReport.DataSources.Add(new ReportDataSource("ListadoVentas", reportModel.saleListings));
			this.reportViewer2.LocalReport.DataSources.Add(new ReportDataSource("VentasPorPeriodo", reportModel.netSalesByPeriods));
			this.reportViewer2.RefreshReport();

		}

		private void label1_Click(object sender, EventArgs e)
		{

		}

		private void button1_Click(object sender, EventArgs e)
		{
			var fromdate = DateTime.Today;
			var todate = DateTime.Now;

			GetSalesReport(fromdate, todate);
		}

		private void button2_Click(object sender, EventArgs e)
		{
			var fromdate = DateTime.Today.AddDays(-7);
			var todate = DateTime.Now;
			GetSalesReport(fromdate, todate);
		}

		private void button3_Click(object sender, EventArgs e)
		{
			var fromdate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
			var todate = DateTime.Now;
			GetSalesReport(fromdate, todate);
		}

		private void button5_Click(object sender, EventArgs e)
		{
			var fromdate = new DateTime(DateTime.Now.Year, 1, 1);
			var todate = DateTime.Now;
			GetSalesReport(fromdate, todate);
		}
		private void button4_Click(object sender, EventArgs e)
		{
			var fromdate = DateTime.Today.AddDays(-30);
			var todate = DateTime.Now;
			GetSalesReport(fromdate, todate);
		}

		private void button7_Click(object sender, EventArgs e)
		{
			var fromdate = dateTimePicker1.Value;
			var todate = dateTimePicker2.Value;
			GetSalesReport(fromdate, new DateTime(todate.Year,todate.Month, todate.Day,23,59,59));
		}

		private void reportViewer2_Load(object sender, EventArgs e)
		{

		}

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button7_Click_1(object sender, EventArgs e)
        {

        }
    }

	
}
