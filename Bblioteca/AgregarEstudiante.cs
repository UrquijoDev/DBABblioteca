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
using static Bblioteca.Form1;

namespace Bblioteca
{
    public partial class AgregarEstudiante : Form
    {
        public AgregarEstudiante()
        {
            InitializeComponent();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Esta seguro que desea salir?", "Alert",MessageBoxButtons.OKCancel,MessageBoxIcon.Warning) == DialogResult.OK)
            {
                this.Close();
            }
            
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            txtNombreAlum.Clear();
            txtNoControlAlum.Clear();
            txtSemestreAlum.Clear();
            txtDepartamentoAlum.Clear();
            txtCorreoAlum.Clear();
            txtContactoAlum.Clear();


        }

        private void BtnGuardar_Click(object sender, EventArgs e)
        {
            if (txtNombreAlum.Text != "" && txtNoControlAlum.Text != "" && txtDepartamentoAlum.Text != "" && txtSemestreAlum.Text != ""
        && txtNoControlAlum.Text != "" && txtCorreoAlum.Text != "")
            {
                if (Int64.TryParse(txtNoControlAlum.Text, out Int64 noControl))
                {
                    if (Int64.TryParse(txtContactoAlum.Text, out Int64 contacto))
                    {
                        SqlConnection con = new SqlConnection();
                        con.ConnectionString = "data source=localhost; database=Bblioteca; user id=sa; password=sa1@;";
                        con.Open();

                        SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM AgregarAlumno WHERE NumeroControl = @NumeroControl", con);
                        cmd.Parameters.AddWithValue("@NumeroControl", txtNoControlAlum.Text);
                        int count = (int)cmd.ExecuteScalar();

                        if (count > 0)
                        {
                            MessageBox.Show("El número de control ya está registrado", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            con.Close();
                            return;
                        }
                        else {
                            VerificacionCodigo VC = new VerificacionCodigo(this);
                            if (VC.ShowDialog() == DialogResult.OK)
                            {
                                String nombre = txtNombreAlum.Text;
                                Int64 NO_Control = Int64.Parse(txtNoControlAlum.Text);
                                String Departamento = txtDepartamentoAlum.Text;
                                String Semestre = txtSemestreAlum.Text;
                                Int64 Contacto = Int64.Parse(txtContactoAlum.Text);
                                String Correo = txtCorreoAlum.Text;

                                cmd.CommandText = " Insert into AgregarAlumno values  " +
                                "('" + nombre + "' , '" + NO_Control + "', '" + Departamento + "', " +
                                "'" + Semestre + "', '" + Contacto + "', '" + Correo + "')";
                                cmd.ExecuteNonQuery();
                                con.Close();

                                MessageBox.Show("Alumno agregado exitosamente", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                txtNombreAlum.Clear();
                                txtNoControlAlum.Clear();
                                txtSemestreAlum.Clear();
                                txtDepartamentoAlum.Clear();
                                txtCorreoAlum.Clear();
                                txtContactoAlum.Clear();
                            }
                            else
                            {
                                return;
                            }
                        }
                    }
                    else {

                        MessageBox.Show("El contacto debe ser un número entero", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                }
                else {
                    MessageBox.Show("El Número de control debe ser un número entero ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
                
            }
            else
            {
                MessageBox.Show("Rellene los espacios vacíos", "Suggest", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }
    }
}
