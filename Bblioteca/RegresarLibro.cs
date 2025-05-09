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
    public partial class RegresarLibro : Form
    {
        public RegresarLibro()
        {
            InitializeComponent();
        }

        private string NombreLibroDevuelto;
        private void RegresarLibro_Load(object sender, EventArgs e)
        {
            panel2.Visible = true;
            txtNumeroControl.Clear();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = "data source=localhost; database=Bblioteca; user id=sa; password=sa1@;";
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;

            cmd.CommandText = "Select * from Prestamos where NumeroControlE = '" + txtNumeroControl.Text+ "' and DiaDevolucion is null";
            SqlDataAdapter DA = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            DA.Fill(ds);

            if (ds.Tables[0].Rows.Count != 0)
            {
                dataGridView1.DataSource = ds.Tables[0];
            }
            else {
                MessageBox.Show("Id invalida o no hay ingun libro prestado", "Error" , MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }


        String Dia;
        Int64 IdFila;
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                if (dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value == null || dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString() == "")
                {
                    MessageBox.Show("La celda seleccionada está vacía.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    panel2.Visible = true;
                    return;

                }
                else {
                    IdFila = Int64.Parse(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString());
                    NombreLibroDevuelto = dataGridView1.Rows[e.RowIndex].Cells[7].Value.ToString();
                    string Dia = dataGridView1.Rows[e.RowIndex].Cells[8].Value.ToString();

                    txtNombreLibro.Text = NombreLibroDevuelto;
                    txtDiaPrestamo.Text = Dia;

                    panel2.Visible = true;
                }


            }
            else
            {
                MessageBox.Show("No se puede hacer clic en esta celda.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                panel2.Visible = true; 
            }
        

    }
        private void btnRegresar_Click(object sender, EventArgs e)
        {
            VerificacionCodigo VC = new VerificacionCodigo(this);
            if (VC.ShowDialog() == DialogResult.OK)
            {
                SqlConnection con = new SqlConnection();
                con.ConnectionString = "data source=localhost; database=Bblioteca; user id=sa; password=sa1@;";
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                con.Open();

                // 💥 Verificar si el préstamo tiene multa pendiente
                bool tieneMultaNoPagada = false;
                SqlCommand cmdMulta = new SqlCommand("SELECT COUNT(*) FROM Multas WHERE IdPrestamo = @id AND Pagado = 0", con);
                cmdMulta.Parameters.AddWithValue("@id", IdFila);
                int count = (int)cmdMulta.ExecuteScalar();
                tieneMultaNoPagada = count > 0;

                if (tieneMultaNoPagada)
                {
                    MessageBox.Show("Este libro no se puede devolver porque tiene una multa pendiente.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    con.Close();
                    return;
                }

                // ✅ Si no tiene multa, permitir devolución
                cmd.CommandText = "UPDATE Prestamos SET DiaDevolucion = @fechaDevolucion WHERE NumeroControlE = @control AND IdPrestamo = @id";
                cmd.Parameters.AddWithValue("@fechaDevolucion", dateTimePicker10.Value.Date);
                cmd.Parameters.AddWithValue("@control", txtNumeroControl.Text);
                cmd.Parameters.AddWithValue("@id", IdFila);
                cmd.ExecuteNonQuery();

                con.Close();

                ActualizarCantidad(NombreLibroDevuelto);
                MessageBox.Show("Regreso de libro exitoso", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                RegresarLibro_Load(this, null);
            }
            else
            {
                return;
            }
        }


        private void ActualizarCantidad(string NombreLibroDevuelto) {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = "data source=localhost; database=Bblioteca; user id=sa; password=sa1@;";
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            con.Open();

            cmd.CommandText = "UPDATE AgregarLibro SET Existencia = Existencia + 1 WHERE Nombre = '" + NombreLibroDevuelto + "'";
            cmd.ExecuteNonQuery();
            con.Close();

        }


        private void txtNumeroControl_TextChanged(object sender, EventArgs e)
        {
            if (txtNumeroControl.Text == "")
            {
                panel2.Visible = true;
                dataGridView1.DataSource = null;
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            txtNumeroControl.Clear();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            panel2.Visible = true;
        }

        private void dateTimePicker10_ValueChanged(object sender, EventArgs e)
        {
            DateTimePicker dateTimePicker = sender as DateTimePicker;

            if (dateTimePicker.Value < DateTime.Today)
            {
                MessageBox.Show("No se pueden seleccionar fechas pasadas. Por favor, elija una fecha presente o futura.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                dateTimePicker.Value = DateTime.Today; 
            }
        }
    }
}
