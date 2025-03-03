using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bblioteca
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        //Hola joto

        private void Form1_Load(object sender, EventArgs e)
        {
           
        }

        private void txtUsuario_MouseClick(object sender, MouseEventArgs e)
        {
            if (txtUsuario.Text == "Usuario")
            {
                txtUsuario.Clear();
            }
        }

        private void txtContra_MouseClick(object sender, MouseEventArgs e)
        {
            if (txtContra.Text == "Contraseña ")
            {
                txtContra.Clear();
                txtContra.PasswordChar = '*';
            }
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public static class AuthHelper
        {
            public static string CurrentUser { get; set; }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = "data source = 192.168.27.1,1433; database = Bblioteca; user id = sa; password = sa1@;";
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;

            cmd.CommandText = "select * from TablaLogin where Usuario = '" + txtUsuario.Text + "' " +
                "and Contra = '" + txtContra.Text + "' ";

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);

            if (ds.Tables[0].Rows.Count != 0)
            {
                Tablero Tab = new Tablero();
                Tab.Show();
            }
            else {
                MessageBox.Show("Usuario o Contrasena incorrecta", "Error ",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            RegistrarUsuario Ru = new RegistrarUsuario();
            Ru.Show();
        }
    }
}
