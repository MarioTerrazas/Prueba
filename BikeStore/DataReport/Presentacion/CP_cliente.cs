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
    public partial class CP_cliente : Form
    {
        private CD_cliente cliente=new CD_cliente();
        public CP_cliente()
        {
            InitializeComponent();
            CargarTabla();
        }

        private void CargarTabla() {
            dataGridView1.DataSource = cliente.GetTable();
        }
        private void Eliminar() {
            // Verifica que el TextBox contenga un valor.
            if (!string.IsNullOrEmpty(textBox1.Text))
            {
                int productId;
                if (int.TryParse(textBox1.Text, out productId))
                {
                    // Llamar al método de eliminación en el DataAccess
                    cliente.Delete(productId);

                    MessageBox.Show(" eliminado con éxito.");

                    // Recarga la lista de productos
                    CargarTabla();
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
        private void SelectRow(){
            if (dataGridView1.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];
                // Asegúrate de que las columnas del DataGridView tengan los nombres correctos.
               string id= selectedRow.Cells["customer_id"].Value.ToString();
                string firstname = selectedRow.Cells["first_name"].Value.ToString();
                string lastname = selectedRow.Cells["last_name"].Value.ToString();
                string phone = selectedRow.Cells["phone"].Value.ToString();
                string email = selectedRow.Cells["email"].Value.ToString();
                string street = selectedRow.Cells["street"].Value.ToString();
                string city = selectedRow.Cells["city"].Value.ToString();
                string state = selectedRow.Cells["state"].Value.ToString();
                string zipcode = selectedRow.Cells["zip_code"].Value.ToString();

                textBox1.Text = id;
                textBox2.Text = firstname;
                textBox3.Text = lastname;
                textBox4.Text = phone;
                textBox5.Text = email;
                textBox6.Text = street;
                textBox7.Text = city;
                textBox8.Text = state;
                textBox9.Text = zipcode;
            }
        }
        private void Nuevo() {
            string firstname = textBox2.Text;
            string lastname = textBox3.Text;
            string phone = textBox4.Text;
            string email = textBox5.Text;
            string street = textBox6.Text;
            string city = textBox7.Text;
            string state = textBox8.Text;
            string zipcode = textBox9.Text;
            if (firstname.Length == 0 | lastname.Length == 0 | phone.Length == 0 | email.Length == 0 | street.Length == 0
                | city.Length == 0 | state.Length == 0 | zipcode.Length == 0)
            {
                MessageBox.Show("Por favor, llene los datos", "Casillas vacias", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            // Llamar al método de creación en el DataAccess
            cliente.Create(firstname,lastname,phone,email,street,city,state,zipcode);

            MessageBox.Show("guardado con éxito.");

            // Limpia los campos y recarga la lista de productos
            clear();
            CargarTabla();
        }
        private void clear() {
            textBox1.Text = string.Empty;
            textBox2.Text = string.Empty;
            textBox3.Text = string.Empty;
            textBox4.Text = string.Empty;
            textBox5.Text = string.Empty;
            textBox6.Text = string.Empty;
            textBox7.Text = string.Empty;
            textBox8.Text = string.Empty;
            textBox9.Text = string.Empty;

        }

        private void GuardarCambios() {
            // Obtener el ID del producto desde TextBox1
            if (!int.TryParse(textBox1.Text, out int productId))
            {
                MessageBox.Show("Por favor, ingrese un ID de producto válido.", "ID de Producto Inválido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Obtener los nuevos datos del producto desde los TextBox
            string firstname = textBox2.Text;
            string lastname=textBox3.Text;
            string phone = textBox4.Text;
            string email = textBox5.Text;
            string street = textBox6.Text;
            string city = textBox7.Text;
            string state = textBox8.Text;
            string zipcode = textBox9.Text;

            if (firstname.Length==0 | lastname.Length==0 | phone.Length == 0 | email.Length == 0 | street.Length == 0 
                | city.Length == 0 | state.Length == 0 | zipcode.Length == 0)
            {
                MessageBox.Show("Por favor, llene los datos", "Casillas vacias", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Llamar al método de actualización en el DataAccess
            cliente.Update(productId, firstname, lastname, phone, email, street,city,state,zipcode);

            MessageBox.Show(" actualizado con éxito.");

            // Limpia los campos y recarga la lista de productos
            clear();
            CargarTabla();
        }

        private void Buscar() {
            string firsname = textBox2.Text;
            string id = textBox1.Text;

            DataTable searchResults;

            if (!string.IsNullOrEmpty(id))
            {
                if (int.TryParse(id, out int productId))
                {
                    // Es un número, busca por ID.
                    searchResults = cliente.SearchById(productId);
                }
                else
                {
                    // No es un número, busca por nombre.
                    searchResults = cliente.SearchByName(id);
                }
            }
            else
            {
                // El cuadro de texto de ID/nombre está vacío, muestra todos los productos.
                searchResults = cliente.GetTable();
            }

            // Actualiza el DataGridView con los resultados de la búsqueda.
            dataGridView1.DataSource = searchResults;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Nuevo();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Buscar();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            GuardarCambios();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Eliminar();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void dataGridView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            SelectRow();
        }

        //private void CP_cliente_Load(object sender, EventArgs e)
        //{
        //    // TODO: esta línea de código carga datos en la tabla 'bike_StoreDataSet.orders' Puede moverla o quitarla según sea necesario.
        //    this.ordersTableAdapter.Fill(this.bike_StoreDataSet.orders);

        //}
    }
}
