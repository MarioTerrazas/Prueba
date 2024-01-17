using DataAccess;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Presentacion
{
    public partial class MainOrderForm : Form
    {
        
        public MainOrderForm()
        {
            InitializeComponent();
            CargarTabla();
            
        }
        private void CargarTabla()
        {
            Orders orders = new Orders(); 
            dataGridView1.DataSource = orders.GetTable(); 
        }

        private void btnbuscar_Click(object sender, EventArgs e)
        {
            string idOrName = txtIDorder.Text;

            Orders orden = new Orders();
            DataTable searchResults;

            if (int.TryParse(idOrName, out int order) && order != 0)
            {
                
                searchResults = orden.BuscarOrdenPorID(order);
            }
            else
            {
                if (order == 0)
                {
                    
                    searchResults = orden.GetTable();
                }
                else
                {
                    
                    MessageBox.Show("Por favor, ingrese un ID válido o 0 para buscar todas las órdenes.");
                    return;
                }
            }
            
            dataGridView1.DataSource = searchResults;
        }
     
        private void btnNuevo_Click(object sender, EventArgs e)
        {
          
            OrdenForm order2 = new OrdenForm();
            order2.Show();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            this.Hide();
            LoginForm form3 = new LoginForm();
            form3.Show();
        }

        private void fillByToolStripButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.order_itemsTableAdapter.FillBy(this.bike_StoreDataSet.order_items);
            }
            catch (System.Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }

        }
    }
}
