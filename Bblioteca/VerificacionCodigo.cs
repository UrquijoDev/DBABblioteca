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
    public partial class VerificacionCodigo : Form
    {
        public VerificacionCodigo()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Validación: campo vacío
            if (string.IsNullOrWhiteSpace(txtCodigo.Text))
            {
                MessageBox.Show("El campo 'Código' está vacío. Por favor, ingrese un valor.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                Int64 Codigo = Int64.Parse(txtCodigo.Text);
                SqlConnection con = new SqlConnection();
                con.ConnectionString = "data source=localhost; database=Bblioteca; user id=sa; password=sa1@;";
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;

                con.Open();
                cmd.CommandText = "SELECT Codigo FROM TablaLogin WHERE Codigo = @Codigo";
                cmd.Parameters.AddWithValue("@Codigo", Codigo);
                String NombreU = cmd.ExecuteScalar()?.ToString();

                if (NombreU != null)
                {
                    cmd.Parameters.Clear();
                    cmd.CommandText = "SELECT Usuario FROM TablaLogin WHERE Codigo = @Codigo";
                    cmd.Parameters.AddWithValue("@Codigo", NombreU);
                    String NombreUsuario = cmd.ExecuteScalar()?.ToString();

                    cmd.CommandText = "INSERT INTO UltimoUsuarioActualizando VALUES (@Usuario)";
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@Usuario", NombreUsuario);
                    cmd.ExecuteNonQuery();

                    con.Close();

                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("El código ingresado no se encuentra en la base de datos. Por favor, ingrese un nuevo código.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (FormatException)
            {
                MessageBox.Show("El código ingresado no es válido. Asegúrese de ingresar un número.", "Error de formato", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private AgregarEstudiante agregarEstudiante;


        public VerificacionCodigo(AgregarEstudiante agregarEstudiante)
        {
            InitializeComponent();
            this.agregarEstudiante = agregarEstudiante;
        }

        private VerInformacionDeEstudiante verInformacionDeEstudiante;


        public VerificacionCodigo(VerInformacionDeEstudiante verInformacionDeEstudiante)
        {
            InitializeComponent();
            this.verInformacionDeEstudiante = verInformacionDeEstudiante;
        }

        private AgregarLibros agregarLibros;


        public VerificacionCodigo(AgregarLibros agregarLibros)
        {
            InitializeComponent();
            this.agregarLibros = agregarLibros;
        }

       
        private Ver_Libros ver_Libros;
        public VerificacionCodigo(Ver_Libros ver_Libros)
        {
            InitializeComponent();
            this.ver_Libros = ver_Libros;
        }

        private PrestamoDeLibros prestamoDeLibros;
        public VerificacionCodigo(PrestamoDeLibros prestamoDeLibros)
        {
            InitializeComponent();
            this.prestamoDeLibros = prestamoDeLibros;
        }

        private RegresarLibro regresarLibro;
        public VerificacionCodigo(RegresarLibro regresarLibro)
        {
            InitializeComponent();
            this.regresarLibro = regresarLibro;
        }
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
            this.DialogResult = DialogResult.Cancel;
        }
    }
}
