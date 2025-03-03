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
using static Bblioteca.Form1;

namespace Bblioteca
{
    public partial class AgregarLibros : Form
    {
        public AgregarLibros()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btnGuardarLibro_Click(object sender, EventArgs e)
        {
            if (txtAutorLibro.Text != "" && txtAutorLibro.Text != "" && DiaAgregado.Text != ""
                && txtPrecioLibro.Text != "" && txtCantidadLibros.Text != "")
            {

                if (Int64.TryParse(txtPrecioLibro.Text, out Int64 precio))
                {
                    if (Int64.TryParse(txtCantidadLibros.Text, out Int64 cantidad))
                    {
                        VerificacionCodigo VC = new VerificacionCodigo(this);
                        if (VC.ShowDialog() == DialogResult.OK)
                        {
                            String NombreLibro = txtNombreLibro.Text;
                            String AutorLibro = txtAutorLibro.Text;
                            String Dia = DiaAgregado.Text;
                            Int64 Precio = Int64.Parse(txtPrecioLibro.Text);
                            Int64 Cantidad = Int64.Parse(txtCantidadLibros.Text);

                            SqlConnection con = new SqlConnection();
                            con.ConnectionString = "data source = LAPTOP-K2ADCD77\\SQLEXPRESS; " +
                            "database = Bblioteca; integrated security = false ";
                            SqlCommand cmd = new SqlCommand();
                            cmd.Connection = con;

                            con.Open();
                            cmd.CommandText = " Insert into AgregarLibro values  " +
                            "('" + NombreLibro + "' , '" + AutorLibro + "', '" + Dia + "', '" + Precio + "', '" + Cantidad + "')";
                            cmd.ExecuteNonQuery();
                            con.Close();


                            MessageBox.Show("Libro guardado exitosamente", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            txtNombreLibro.Clear();
                            txtAutorLibro.Clear();
                            txtPrecioLibro.Clear();
                            txtCantidadLibros.Clear();
                        }
                        else
                        {
                            return;
                        }
                    }
                    else {
                        MessageBox.Show("La cantidad debe ser un número", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else {

                    MessageBox.Show("El precio debe ser un número", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
            else {
                MessageBox.Show("No se pueden dejar espacios vacios", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Esto BORRARA tu informacion no guardad", " Estas seguro? ", 
                MessageBoxButtons.OKCancel, MessageBoxIcon.Warning ) == DialogResult.OK)
            {
                this.Close();
            }
        }

        private void DiaAgregado_ValueChanged(object sender, EventArgs e)
        {
            DateTimePicker dateTimePicker = sender as DateTimePicker;

            if (dateTimePicker.Value < DateTime.Today)
            {
                MessageBox.Show("No se pueden seleccionar fechas pasadas. Por favor, elija una fecha presente o futura.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                dateTimePicker.Value = DateTime.Today;
            }
        }

        private void AgregarLibros_Load(object sender, EventArgs e)
        {

        }
    }
}
