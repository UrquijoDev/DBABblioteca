using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bblioteca
{
    public partial class Ver_Libros : Form
    {
        public Ver_Libros()
        {
            InitializeComponent();
        }

        private void Ver_Libros_Load(object sender, EventArgs e)
        {
            panel2.Visible = false;
            SqlConnection con = new SqlConnection();
            con.ConnectionString = "data source = 192.168.27.1,1433; database = Bblioteca; user id = sa; password = sa1@;";
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;

           
            cmd.CommandText = "Select * from AgregarLibro";
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        int IdLibro;
        Int64 IdFila;
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                if (dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value == null)
                {
                    MessageBox.Show("La celda seleccionada está vacía.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    panel2.Visible = false; 
                    return; 
                }

                int IdLibro;
                if (int.TryParse(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString(), out IdLibro))
                {
                    panel2.Visible = true;
                    SqlConnection con = new SqlConnection();
                    con.ConnectionString = "data source = 192.168.27.1,1433; database = Bblioteca; user id = sa; password = sa1@;";
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = con;

                    cmd.CommandText = "Select * from AgregarLibro where idLibro = " + IdLibro + " ";
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    da.Fill(ds);

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        IdFila = Int64.Parse(ds.Tables[0].Rows[0][0].ToString());
                        txtNombreL.Text = ds.Tables[0].Rows[0][1].ToString();
                        txtAutorL.Text = ds.Tables[0].Rows[0][2].ToString();
                        txtDiaL.Text = ds.Tables[0].Rows[0][3].ToString();
                        txtPrecioL.Text = ds.Tables[0].Rows[0][4].ToString();
                        txtCantidadL.Text = ds.Tables[0].Rows[0][5].ToString();
                    }
                    else
                    {
                        MessageBox.Show("No se encontraron datos para el ID de libro seleccionado.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("El valor en la celda seleccionada no es un número válido.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    panel2.Visible = false; 
                }
            }
            else
            {
                MessageBox.Show("No se puede hacer clic en esta celda.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                panel2.Visible = false; 
            }
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            panel2.Visible = false;
        }

        private void txtBusquedaNombre_TextChanged(object sender, EventArgs e)
        {
            if (txtBusquedaNombre.Text != "")
            {

                panel2.Visible = false;
                SqlConnection con = new SqlConnection();
                con.ConnectionString = "data source = 192.168.27.1,1433; database = Bblioteca; user id = sa; password = sa1@;";
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;


                cmd.CommandText = "Select * from AgregarLibro where Nombre Like '"+txtBusquedaNombre.Text+ "%'";
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                dataGridView1.DataSource = ds.Tables[0];
            }
            else {
                panel2.Visible = false;
                SqlConnection con = new SqlConnection();
                con.ConnectionString = "data source = 192.168.27.1,1433; database = Bblioteca; user id = sa; password = sa1@;";
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;


                cmd.CommandText = "Select * from AgregarLibro";
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                dataGridView1.DataSource = ds.Tables[0];
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            txtBusquedaNombre.Clear();
            panel2.Visible = false;
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            VerificacionCodigo VC = new VerificacionCodigo(this);
            if (VC.ShowDialog() == DialogResult.OK)
            {
                if (MessageBox.Show("La informacion sera MODIFICADA. Esta seguro?", "Success",
                MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                {


                    String Nombre = txtNombreL.Text;
                    String Autor = txtAutorL.Text;
                    String Dia = txtDiaL.Text;
                    Int64 Costo = Int64.Parse(txtPrecioL.Text);
                    Int64 Cantidad = Int64.Parse(txtCantidadL.Text);

                    SqlConnection con = new SqlConnection();
                    con.ConnectionString = "data source = 192.168.27.1,1433; database = Bblioteca; user id = sa; password = sa1@;";
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = con;


                    cmd.CommandText = "Update AgregarLibro set Nombre = '" + Nombre + "', Autor = '" + Autor + "', Dia = '" + Dia + "'" +
                        ", Precio = '" + Costo + "', Cantidad = '" + Cantidad + "' where idLibro = '" + IdFila + "'";
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                }
            }
            else {
                return;
            }


        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            VerificacionCodigo VC = new VerificacionCodigo(this);
            if (VC.ShowDialog() == DialogResult.OK)
            {
                if (MessageBox.Show("La informacion sera BORRADA. Esta seguro?", "Confirmation Dialog",
                MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
                {
                    SqlConnection con = new SqlConnection();
                    con.ConnectionString = "data source = 192.168.27.1,1433; database = Bblioteca; user id = sa; password = sa1@;";
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = con;


                    cmd.CommandText = "delete from AgregarLibro where idLibro = '" + IdFila + "'";
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    Ver_Libros_Load(sender, e);
                }
            }
            else {
                return;
            }
        }
    }
}
