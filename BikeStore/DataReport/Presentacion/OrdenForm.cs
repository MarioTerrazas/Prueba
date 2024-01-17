using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DataAccess;

namespace Presentacion
{
    public partial class OrdenForm : Form
    {

        private int customerIDSeleccionado;

        private CD_cliente cliente = new CD_cliente();
        private ProductDAO Produc = new ProductDAO();
        private Orders ord = new Orders();
        public OrdenForm()
        {
            InitializeComponent();
            CargarTabla();
            CargarProductosEnDataGridView();
            dataGridCarrito.Columns.Add("product_id", "Product ID");
            dataGridCarrito.Columns.Add("product_name", "Nombre del Producto");
            dataGridCarrito.Columns.Add("price", "Precio");

        }
        private void CargarTabla()
        {

            dataGridView1.DataSource = cliente.GetTable();
        }
        private void CargarProductosEnDataGridView()
        {
            dataGridProductos.DataSource = Produc.GetProducts(); 
        }
        private void dataGridCarrito_CellClick(object sender, DataGridViewCellEventArgs e)
        {
           
        }
        private decimal CalcularTotalOrden()
        {
            decimal total = 0;

            foreach (DataGridViewRow row in dataGridCarrito.Rows)
            {
                decimal precio = Convert.ToDecimal(row.Cells["price"].Value);
                total += precio;
            }

            return total;
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            int customerID = ObtenerClienteSeleccionadoDesdeGrid(); // Obten el ID del cliente seleccionado desde la DataGridView.
            DateTime orderDate = DateTime.Now; 
                 
            int orderID = ord.GuardarOrden(customerID, orderDate);

            if (orderID > 0)
            {
                // Ahora, recorre la DataGridView del carrito y guarda los productos en la tabla "order_items".
                foreach (DataGridViewRow row in dataGridCarrito.Rows)
                {
                    // Verifica si la fila no está vacía y tiene datos
                    if (row.Cells["product_id"].Value != null)
                    {
                        int productID = Convert.ToInt32(row.Cells["product_id"].Value);
                  
                        MessageBox.Show($"product_id: {productID}");

                        int cantidad = 1; // Obtén la cantidad 1 estática (cambia según sea necesario)
                        //int cantidad = Convert.ToInt32(txtCantidad.Text);
                        decimal precio = Convert.ToDecimal(row.Cells["price"].Value);
                        
                        decimal descuento = 0; // Obtiene el descuento estático por "cambiar"
                        //decimal descuento = Convert.ToDecimal(txtDescuento.Text);

                        // Inserta el producto en la tabla "order_items".
                        ord.guardar(orderID, productID, cantidad, precio, descuento);
                    }
                }
                MessageBox.Show("Orden guardada exitosamente.");
                LimpiarCarrito();
            }
            else
            {
                MessageBox.Show("Error al guardar la orden.");
            }
        }
        private void LimpiarCarrito()
        {
            dataGridCarrito.Rows.Clear();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                customerIDSeleccionado = Convert.ToInt32(row.Cells["customer_id"].Value); 
            }
        }
        private void btnAgregar_Click(object sender, EventArgs e)
        {
            // Obten el producto seleccionado desde el DataGridView de productos.
            if (dataGridProductos.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = dataGridProductos.SelectedRows[0];
                int productID = Convert.ToInt32(selectedRow.Cells["product_id"].Value);
                string productName = selectedRow.Cells["product_name"].Value.ToString();
                decimal productPrice = Convert.ToDecimal(selectedRow.Cells["price"].Value);

                // Agregar el producto al DataGridView del carrito.
                dataGridCarrito.Rows.Add(productID, productName, productPrice);
            }
        }
        private int ObtenerClienteSeleccionadoDesdeGrid()
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                // Obten el valor de la celda correspondiente al ID del cliente en la fila seleccionada.
                int customerID = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["customer_id"].Value);
                return customerID;
            }
            else
            {
                
                MessageBox.Show("Por favor, seleccione un cliente.");
                return -1; 
            }
        }

    }
}





