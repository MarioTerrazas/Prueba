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
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text;
            string password = txtPassword.Text;

            if (EsAdministrador(username, password))
            {
                // Si es un administrador, abre el formulario de administrador.
                MainAddForm adminForm = new MainAddForm();
                adminForm.Show();
                this.Hide(); // Oculta el formulario de inicio de sesión.
            }
            else if (EsCliente(username, password))
            {
                // Si es un cliente, abre el formulario de cliente.
                MainOrderForm clientForm = new MainOrderForm();
                clientForm.Show();
                this.Hide(); // Oculta el formulario de inicio de sesión.
            }
            else
            {
                MessageBox.Show("Credenciales incorrectas. Inténtalo de nuevo.");
            }
        }

        private bool EsAdministrador(string username, string password)
        {
            // Comprueba si las credenciales coinciden con el usuario administrador.
            return (username == "admin" && password == "admin");
        }

        private bool EsCliente(string username, string password)
        {
            // Comprueba si las credenciales coinciden con un usuario cliente.
            return (username == "cliente" && password == "cliente");
        }
        public enum TipoUsuario
        {
            Administrador,
            Cliente
        }

        // En la función de autenticación
        private bool AutenticacionExitosa(string username, string password, TipoUsuario tipoUsuario)
        {
            if (tipoUsuario == TipoUsuario.Administrador)
            {
                // Verifica las credenciales del administrador.
                return username == "admin" && password == "admin";
            }
            else if (tipoUsuario == TipoUsuario.Cliente)
            {
                // Verifica las credenciales del cliente.
                return username == "cliente" && password == "cliente";
            }

            return false; // Si el tipo de usuario no es válido o las credenciales son incorrectas.
        }

        private void CheckBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
           
                // Cierra la aplicación cuando se hace clic en el botón de cierre.
                Application.Exit();
            
        }

        private void btnMinimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked)
            {
                // Si está marcado, muestra la contraseña en texto sin formato
                txtPassword.UseSystemPasswordChar = false;
            }
            else
            {
                // Si no está marcado, muestra la contraseña en formato de contraseña
                txtPassword.UseSystemPasswordChar = true;
            }
        }

        private void Form3_Load(object sender, EventArgs e)
        {

        }
    }
}
