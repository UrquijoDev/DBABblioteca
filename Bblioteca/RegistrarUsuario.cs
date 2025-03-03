using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics.Contracts;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bblioteca
{
    public partial class RegistrarUsuario : Form
    {
        public RegistrarUsuario()
        {
            InitializeComponent();
        }

        private void RegistrarUsuario_Load(object sender, EventArgs e)
        {

        }

        private void txtUsuario_MouseClick(object sender, MouseEventArgs e)
        {
            if (txtUsuario.Text == "Usuario")
            {
                txtUsuario.Clear();
            }
        }

        private void txtContraseña_MouseClick(object sender, MouseEventArgs e)
        {
            if (txtContraseña.Text == "Contraseña ")
            {
                txtContraseña.Clear();
            }
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnRegistrar_Click_1(object sender, EventArgs e)
        {
            if (txtUsuario.Text != "" && txtContraseña.Text != "" && txtCodigo.Text != "")
            {

                Int64 Codigo = Int64.Parse(txtCodigo.Text);
                SqlConnection con = new SqlConnection();
                con.ConnectionString = "data source = LAPTOP-K2ADCD77\\SQLEXPRESS; " +
                "database = Bblioteca; integrated security = True ";
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;

                cmd.CommandText = "SELECT COUNT(*) FROM TablaLogin WHERE Codigo = @Codigo";
                cmd.Parameters.AddWithValue("@Codigo", Codigo);

                con.Open();
                int count = (int)cmd.ExecuteScalar();
                if (count > 0)
                {
                    MessageBox.Show("El código ya está registrado porfavor ingrese un nuevo", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    cmd.CommandText = " Insert into TablaLogin values  ('" + txtUsuario.Text + "', '" + txtContraseña.Text + "', '" + Codigo + "')";
                    cmd.ExecuteNonQuery();
                    con.Close();

                    txtContraseña.Clear();
                    txtUsuario.Clear();


                    MessageBox.Show("Usuario agregado exitosamente", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Form1 F1 = new Form1();
                    F1.Show();
                    this.Close();
                }

            }
            else
            {
                MessageBox.Show("No ha ingresado el usuario, contraseña o  codigo ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void btnCancelar_Click_1(object sender, EventArgs e)
        {
                this.Close();
            

        }

        private void txtCodigo_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
