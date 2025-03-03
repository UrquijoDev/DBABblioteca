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
    public partial class PrestamoDeLibros : Form
    {
        public PrestamoDeLibros()
        {
            InitializeComponent();
        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void PrestamoDeLibros_Load(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = "data source = LAPTOP-K2ADCD77\\SQLEXPRESS; " +
                "database = Bblioteca; integrated security = True ";
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            con.Open();

            cmd = new SqlCommand("Select Nombre from AgregarLibro", con);
            SqlDataReader Sdr = cmd.ExecuteReader();
            while (Sdr.Read()) {
                for (int i = 0; i < Sdr.FieldCount; i++)
                {
                    cbbNombreL.Items.Add(Sdr.GetString(i));
                }
            
            }

            Sdr.Close();
            con.Close();

        }

        int Cuenta;
        private void btnBuscar_Click(object sender, EventArgs e)
        {
            if (txtNoControl.Text != "")
            {
                String Ide = txtNoControl.Text;
                SqlConnection con = new SqlConnection();
                con.ConnectionString = "data source = LAPTOP-K2ADCD77\\SQLEXPRESS; " +
                    "database = Bblioteca; integrated security = True ";
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;

                cmd.CommandText = "Select * from AgregarAlumno where NumeroControl = '" + Ide + "'";
                SqlDataAdapter DA = new SqlDataAdapter(cmd);
                DataSet DS = new DataSet();
                DA.Fill(DS);

                cmd.CommandText = "Select count(NumeroControlE) from Prestamos where NumeroControlE =  '" + Ide + "' and DiaDevolucion is null";
                SqlDataAdapter Da = new SqlDataAdapter(cmd);
                DataSet Ds = new DataSet();
                DA.Fill(Ds);

                Cuenta = int.Parse(Ds.Tables[0].Rows[0][0].ToString());


                if (DS.Tables[0].Rows.Count != 0)
                {
                    txtNombre.Text = DS.Tables[0].Rows[0][1].ToString();
                    txtDepartamento.Text = DS.Tables[0].Rows[0][3].ToString();
                    txtSemestre.Text = DS.Tables[0].Rows[0][4].ToString();
                    txtContacto.Text = DS.Tables[0].Rows[0][5].ToString();
                    txtCorreo.Text = DS.Tables[0].Rows[0][6].ToString();


                }
                else {
                    txtNombre.Clear();
                    txtDepartamento.Clear();
                    txtSemestre.Clear();
                    txtContacto.Clear();  
                    txtCorreo.Clear();

                    MessageBox.Show("Numero de control invalido", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }

            }
        }

        private void btnPrestar_Click(object sender, EventArgs e)
        {
            if (txtNombre.Text != "")
            {
                if (cbbNombreL.SelectedIndex != -1 && Cuenta <= 2)
                {

                    string nombreLibro = cbbNombreL.SelectedItem.ToString();

                    
                    int cantidadDisponible = ObtenerCantidadDisponible(nombreLibro);

                    if (cantidadDisponible > 0)
                    {
                        VerificacionCodigo VC = new VerificacionCodigo(this);
                        if (VC.ShowDialog() == DialogResult.OK)
                        {
                            RealizarPrestamo(nombreLibro);
                        }
                        else {
                            return;
                        }
                    }
                    else
                    {
                        MessageBox.Show("No hay ejemplares disponibles de este libro", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }


                }
                else
                {

                    MessageBox.Show("Libro no seleccionado o limite de prestamos alcanzado", "Libro no seleccionado", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtNoControl.Clear();

                }
            }
            else {
                MessageBox.Show("Ingrese un numero de control valido", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtNoControl.Clear();

            }
        }

        private int ObtenerCantidadDisponible(string nombreLibro)
        {
            int cantidadDisponible = 0;

                SqlConnection con = new SqlConnection();
                con.ConnectionString = "data source = LAPTOP-K2ADCD77\\SQLEXPRESS; " +
                    "database = Bblioteca; integrated security = True ";

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                con.Open();

                cmd.CommandText = "SELECT Cantidad FROM AgregarLibro WHERE Nombre = '" + nombreLibro + "'";

                object result = cmd.ExecuteScalar();
                cantidadDisponible = Convert.ToInt32(result);
                con.Close();

            return cantidadDisponible;
        }

        private void RealizarPrestamo(string nombreLibro)
        {
                String NumeroControl = txtNoControl.Text;
                String NombreA = txtNombre.Text;
                String DepA = txtDepartamento.Text;
                String SemA = txtSemestre.Text;
                Int64 ContactoA = Int64.Parse(txtContacto.Text);
                String Correo = txtCorreo.Text;
                String NombreLibro = cbbNombreL.Text;
                String DiaPrestamo = dateTimePicker.Text;



                SqlConnection con = new SqlConnection();
                con.ConnectionString = "data source = LAPTOP-K2ADCD77\\SQLEXPRESS; " +
                    "database = Bblioteca; integrated security = True ";
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                con.Open();

                cmd.CommandText = "Insert into Prestamos (NumeroControlE, NombreE,DepartamentoE,SemestreE,ContactoE, CorreoE,NombreLibro,DiaPrestamo)values( '" + NumeroControl + "', '" + NombreA + "', '" + DepA + "', '" + SemA + "', '" + ContactoA + "', '" + Correo + "', '" + NombreLibro + "', '" + DiaPrestamo + "')";
                cmd.ExecuteNonQuery();
                con.Close();
                ActualizarCantidad(nombreLibro);
                MessageBox.Show("Libro Prestado", "Exito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
        }

        private void ActualizarCantidad(string nombreLibro) {

            SqlConnection con = new SqlConnection();
            con.ConnectionString = "data source = LAPTOP-K2ADCD77\\SQLEXPRESS; " +
                "database = Bblioteca; integrated security = True ";
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            con.Open();
            cmd.CommandText = "UPDATE AgregarLibro SET Cantidad = Cantidad - 1 WHERE Nombre = '" + nombreLibro + "'";
            cmd.ExecuteNonQuery();
            con.Close();
        }

        private void txtNoControl_TextChanged(object sender, EventArgs e)
        {
            if (txtNoControl.Text == "")
            {
                txtNombre.Clear();
                txtDepartamento.Clear();
                txtSemestre.Clear();
                txtContacto.Clear();
                txtCorreo.Clear();
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            txtNoControl.Clear();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Esta seguro que desea salir?","Confirmar", MessageBoxButtons.OKCancel,MessageBoxIcon.Warning)==DialogResult.OK)
            {
                this.Close();
            }

        }

        private void dateTimePicker_ValueChanged(object sender, EventArgs e)
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
