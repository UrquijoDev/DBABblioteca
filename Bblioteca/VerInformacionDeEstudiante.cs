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
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ToolTip;

namespace Bblioteca
{
    public partial class VerInformacionDeEstudiante : Form
    {
        public VerInformacionDeEstudiante()
        {
            InitializeComponent();
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }



        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (txtBuscarEstudiante.Text != "")
            {
                // Definir la ConnectionString
                string connectionString = "Data Source=HACEDORDELLUVIA\\MSSQLSERVER3;Initial Catalog=Bblioteca;User ID=sa;Password=sa1@;";

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    try
                    {
                        con.Open();

                        // Configurar el comando SQL para buscar estudiantes
                        SqlCommand cmd = new SqlCommand();
                        cmd.Connection = con;
                        cmd.CommandText = "Select * from AgregarAlumno where NumeroControl Like '" + txtBuscarEstudiante.Text + "%'";

                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataSet ds = new DataSet();
                        da.Fill(ds);

                        dataGridView1.DataSource = ds.Tables[0];
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error al buscar estudiantes: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                label1.Visible = true;

                // Definir la ConnectionString
                string connectionString = "Data Source=HACEDORDELLUVIA\\MSSQLSERVER3;Initial Catalog=Bblioteca;User ID=sa;Password=sa1@;";

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    try
                    {
                        con.Open();

                        // Configurar el comando SQL para mostrar todos los estudiantes
                        SqlCommand cmd = new SqlCommand();
                        cmd.Connection = con;
                        cmd.CommandText = "Select * from AgregarAlumno";

                        SqlDataAdapter DA = new SqlDataAdapter(cmd);
                        DataSet ds = new DataSet();
                        DA.Fill(ds);

                        dataGridView1.DataSource = ds.Tables[0];
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error al cargar estudiantes: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void VerInformacionDeEstudiante_Load(object sender, EventArgs e)
        {
            panel3.Visible = false;
            SqlConnection con = new SqlConnection();
            con.ConnectionString = "Data Source=HACEDORDELLUVIA\\MSSQLSERVER3;Initial Catalog=Bblioteca;User ID=sa;Password=sa1@;";
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;

            cmd.CommandText = "Select * from AgregarAlumno";
            SqlDataAdapter DA = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            DA.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
        }
        int IdAlumno;
        Int64 IdFila;

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                if (dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value == null)
                {
                    MessageBox.Show("La celda seleccionada está vacía.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    VerInformacionDeEstudiante_Load(sender, e);
                    
                }
                int IdAlumno;
                if (int.TryParse(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString(), out IdAlumno))
                {
                    panel3.Visible = true;
                    SqlConnection con = new SqlConnection();
                    con.ConnectionString = "Data Source=HACEDORDELLUVIA\\MSSQLSERVER3;Initial Catalog=Bblioteca;User ID=sa;Password=sa1@;";
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = con;

                    cmd.CommandText = "Select * from AgregarAlumno where IdAlumno = " + IdAlumno + "";
                    SqlDataAdapter DA = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    DA.Fill(ds);

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        IdFila = Int64.Parse(ds.Tables[0].Rows[0][0].ToString());
                        txtNombreA.Text = ds.Tables[0].Rows[0][1].ToString();
                        txtNoControlA.Text = ds.Tables[0].Rows[0][2].ToString();
                        txtDepartamentoA.Text = ds.Tables[0].Rows[0][3].ToString();
                        txtSemestreA.Text = ds.Tables[0].Rows[0][4].ToString();
                        txtContactoA.Text = ds.Tables[0].Rows[0][5].ToString();
                        txtCorreoA.Text = ds.Tables[0].Rows[0][6].ToString();
                    }
                    else
                    {
                        MessageBox.Show("No se encontraron datos para el ID de cliente seleccionado.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        panel3.Visible = false;
                    }
                }
                else
                {
                    MessageBox.Show("El valor en la celda seleccionada no es un número válido.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    panel3.Visible = false;

                }
            }
            else
            {
                MessageBox.Show("No se puede hacer clic en esta celda.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                panel3.Visible = false;

            }
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            String ANombre = txtNombreA.Text;
            String NoControl = txtNoControlA.Text;
            String Dep = txtDepartamentoA.Text;
            String Sem = txtSemestreA.Text;
            Int64 Contacto = Int64.Parse(txtContactoA.Text);
            String Correo = txtCorreoA.Text;

            VerificacionCodigo VC = new VerificacionCodigo(this);
            if (VC.ShowDialog() == DialogResult.OK)
            {

                if (MessageBox.Show("La informacion se va a modificar", "Estas seguro?", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                {
                    SqlConnection con = new SqlConnection();
                    con.ConnectionString = "Data Source=HACEDORDELLUVIA\\MSSQLSERVER3;Initial Catalog=Bblioteca;User ID=sa;Password=sa1@;";
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = con;

                    cmd.CommandText = "Update AgregarAlumno set NombreA = '" + ANombre + "', NumeroControl = '" + NoControl + "', " +
                        "Departamento = '" + Dep + "' , Semestre = '" + Sem + "' , Contacto = '" + Contacto + "' , Correo = '" + Correo + "'where IdAlumno = " + IdFila + "";
                    SqlDataAdapter DA = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    DA.Fill(ds);

                    VerInformacionDeEstudiante_Load(this, null);
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
                if (MessageBox.Show("La informacion sera eliminada", "Estas seguro?", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
                {
                    SqlConnection con = new SqlConnection();
                    con.ConnectionString = "Data Source=HACEDORDELLUVIA\\MSSQLSERVER3;Initial Catalog=Bblioteca;User ID=sa;Password=sa1@;";
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = con;

                    cmd.CommandText = "delete from AgregarAlumno where IdAlumno = " + IdFila + "";
                    SqlDataAdapter DA = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    DA.Fill(ds);

                    VerInformacionDeEstudiante_Load(this, null);
                }
            }
            else {
                return;
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("La informacion no guardad se perdera", "Estas seguro?", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
            {
                this.Close();
            }
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            txtBuscarEstudiante.Clear();
        }
    }
}
