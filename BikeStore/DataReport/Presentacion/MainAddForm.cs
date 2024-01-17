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
    public partial class MainAddForm : Form
    {
        private Form currentChildForm;

        private void OpenChildForm(Form childForm)
        {
            if (currentChildForm != null)
            {
                //Abrimos solo un formulario
                currentChildForm.Close();
            }
            currentChildForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            panelDesktop.Controls.Add(childForm);
            panelDesktop.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
            //lblTitleChildForm.Text = childForm.Text;
        }
        public MainAddForm()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ReporteForm form1 = new ReporteForm();

            // Muestra Form1
            form1.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ProductForm form2 = new ProductForm();

         
            // Muestra Form1
            form2.Show();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            // Limpia la información de la sesión (por ejemplo, nombre de usuario, roles, etc.).
            // Puedes utilizar una clase de autenticación para manejar esto.

            // Cierra o oculta el formulario principal actual.
            this.Hide(); // Oculta el formulario principal.

            // Muestra el formulario de inicio de sesión o la página de autenticación.
            LoginForm form3 = new LoginForm();
            form3.Show();
        }

      
        private void button1_Click(object sender, EventArgs e)
        {
            CP_cliente cliente = new CP_cliente();
            cliente.Show();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
        
        private void btnOrdenes_Click(object sender, EventArgs e)
        {
            MainOrderForm order = new MainOrderForm();
            order.Show();
        }
    }
}
