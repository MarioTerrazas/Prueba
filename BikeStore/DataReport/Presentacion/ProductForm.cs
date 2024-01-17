using DataAccess;
using System;
using System.Data;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Presentacion
{
    public partial class ProductForm : Form
    {
        private ProductDAO productDAO = new ProductDAO();

        public ProductForm()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            // Cargar la lista de productos en el DataGridView
            LoadProducts();
        }

        private void LoadProducts()
        {
            dataGridView1.DataSource = productDAO.GetProducts();
        }

        private void ClearFields()
        {
            txtProductName.Text = "";
            txtModelYear.Text = "";
            txtPrice.Text = "";
        }

       
        private void btnbuscar_Click(object sender, EventArgs e)
        {


          
                string searchTerm = txtProductName.Text;
                string idOrName = textBox1.Text;

                ProductDAO productDAO = new ProductDAO();
                DataTable searchResults;

                if (!string.IsNullOrEmpty(idOrName))
                {
                    if (int.TryParse(idOrName, out int productId))
                    {
                        // Es un número, busca por ID.
                        searchResults = productDAO.SearchProductById(productId);
                    }
                    else
                    {
                        // No es un número, busca por nombre.
                        searchResults = productDAO.SearchProductByName(idOrName);
                    }
                }
                else
                {
                    // El cuadro de texto de ID/nombre está vacío, muestra todos los productos.
                    searchResults = productDAO.GetProducts();
                }

                // Actualiza el DataGridView con los resultados de la búsqueda.
                dataGridView1.DataSource = searchResults;
            





        }

        private void btnmodificar_Click(object sender, EventArgs e)
        {

            
                // Obtener el ID del producto desde TextBox1
                if (!int.TryParse(textBox1.Text, out int productId))
                {
                    MessageBox.Show("Por favor, ingrese un ID de producto válido.", "ID de Producto Inválido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Obtener los nuevos datos del producto desde los TextBox
                string productName = txtProductName.Text;
                int modelYear;
                decimal price;

                if (!int.TryParse(txtModelYear.Text, out modelYear) || !decimal.TryParse(txtPrice.Text, out price))
                {
                    MessageBox.Show("Por favor, ingrese un año y un precio válidos.", "Datos de Producto Inválidos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Llamar al método de actualización en el DataAccess
                productDAO.UpdateProduct(productId, productName, modelYear, price);

                MessageBox.Show("Producto actualizado con éxito.");

                // Limpia los campos y recarga la lista de productos
                ClearFields();
                LoadProducts();
            



        }

        private void btneliminar_Click(object sender, EventArgs e)
        {
           
                // Verifica que el TextBox contenga un valor.
                if (!string.IsNullOrEmpty(textBox1.Text))
                {
                    int productId;
                    if (int.TryParse(textBox1.Text, out productId))
                    {
                        // Llamar al método de eliminación en el DataAccess
                        productDAO.DeleteProduct(productId);

                        MessageBox.Show("Producto eliminado con éxito.");

                        // Recarga la lista de productos
                        LoadProducts();
                    }
                    else
                    {
                        MessageBox.Show("Ingresa un valor válido para el ID del producto.");
                    }
                }
                else
                {
                    MessageBox.Show("Ingresa el ID del producto que deseas eliminar.");
                }
            

        }

        private void btnsave_Click_1(object sender, EventArgs e)
        {
            
                string productName = txtProductName.Text;
                int modelYear = Convert.ToInt32(txtModelYear.Text);
                decimal price = Convert.ToDecimal(txtPrice.Text);

                // Llamar al método de creación en el DataAccess
                productDAO.CreateProduct(productName, modelYear, price);

                MessageBox.Show("Producto guardado con éxito.");

                // Limpia los campos y recarga la lista de productos
                ClearFields();
                LoadProducts();
            

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
