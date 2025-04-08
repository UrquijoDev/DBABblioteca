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
        private List<string> librosSeleccionados = new List<string>();
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
            con.ConnectionString = "data source = 192.168.27.1,1433; database = Bblioteca; user id = sa; password = sa1@;";
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
                con.ConnectionString = "data source = 192.168.27.1,1433; database = Bblioteca; user id = sa; password = sa1@;";
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;

                try
                {
                    con.Open();

                    // Verificar si el alumno existe en la base de datos
                    cmd.CommandText = "Select * from AgregarAlumno where NumeroControl = '" + Ide + "'";
                    SqlDataAdapter DA = new SqlDataAdapter(cmd);
                    DataSet DS = new DataSet();
                    DA.Fill(DS);

                    if (DS.Tables[0].Rows.Count != 0)
                    {
                        // Llenar los campos del alumno
                        txtNombre.Text = DS.Tables[0].Rows[0][1].ToString();
                        txtDepartamento.Text = DS.Tables[0].Rows[0][3].ToString();
                        txtSemestre.Text = DS.Tables[0].Rows[0][4].ToString();
                        txtContacto.Text = DS.Tables[0].Rows[0][5].ToString();
                        txtCorreo.Text = DS.Tables[0].Rows[0][6].ToString();

                        // Habilitar el botón "Agregar Libro"
                        btnAgregarLibro.Enabled = true;

                        // Limpiar el DataGridView y la lista de libros seleccionados
                        dataGridView1.Rows.Clear();
                        librosSeleccionados.Clear();

                        // Actualizar el contador Cuenta
                        ActualizarCuenta();
                    }
                    else
                    {
                        txtNombre.Clear();
                        txtDepartamento.Clear();
                        txtSemestre.Clear();
                        txtContacto.Clear();
                        txtCorreo.Clear();

                        MessageBox.Show("Número de control inválido", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                        // Deshabilitar el botón "Agregar Libro"
                        btnAgregarLibro.Enabled = false;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al buscar el alumno: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    if (con.State == ConnectionState.Open)
                    {
                        con.Close();
                    }
                }
            }
        }

        private void btnPrestar_Click(object sender, EventArgs e)
        {
            if (txtNombre.Text != "" && librosSeleccionados.Count > 0)
            {
                // Validar que no se exceda el límite de 3 libros
                if (Cuenta + librosSeleccionados.Count <= 3)
                {
                    VerificacionCodigo VC = new VerificacionCodigo(this);
                    if (VC.ShowDialog() == DialogResult.OK)
                    {
                        try
                        {
                            // Realizar el préstamo para cada libro seleccionado
                            foreach (string nombreLibro in librosSeleccionados)
                            {
                                RealizarPrestamo(nombreLibro);
                            }

                            MessageBox.Show("Libros prestados correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            // Limpiar el DataGridView y la lista de libros seleccionados
                            dataGridView1.Rows.Clear();
                            librosSeleccionados.Clear();

                            // Actualizar el contador Cuenta
                            ActualizarCuenta();

                            this.Close();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Error al realizar el préstamo: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Has alcanzado el límite máximo de préstamos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Ingrese un número de control válido y seleccione libros para prestar.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private int ObtenerCantidadDisponible(string nombreLibro)
        {
            int cantidadDisponible = 0;

                SqlConnection con = new SqlConnection();
            con.ConnectionString = "data source = 192.168.27.1,1433; database = Bblioteca; user id = sa; password = sa1@;";

            SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                con.Open();

                cmd.CommandText = "SELECT Existencia FROM AgregarLibro WHERE Nombre = '" + nombreLibro + "'";

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
            String DiaPrestamo = dateTimePicker.Text;

            SqlConnection con = new SqlConnection();
            con.ConnectionString = "data source = 192.168.27.1,1433; database = Bblioteca; user id = sa; password = sa1@;";
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            con.Open();

            cmd.CommandText = "Insert into Prestamos (NumeroControlE, NombreE, DepartamentoE, SemestreE, ContactoE, CorreoE, NombreLibro, DiaPrestamo) values ('" + NumeroControl + "', '" + NombreA + "', '" + DepA + "', '" + SemA + "', '" + ContactoA + "', '" + Correo + "', '" + nombreLibro + "', '" + DiaPrestamo + "')";
            cmd.ExecuteNonQuery();
            con.Close();

            ActualizarCantidad(nombreLibro);
        }

        private void ActualizarCantidad(string nombreLibro) {

            SqlConnection con = new SqlConnection();
            con.ConnectionString = "data source = 192.168.27.1,1433; database = Bblioteca; user id = sa; password = sa1@;";
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            con.Open();
            cmd.CommandText = "UPDATE AgregarLibro SET Existencia = Existencia - 1 WHERE Nombre = '" + nombreLibro + "'";
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

                // Limpiar el DataGridView y la lista de libros seleccionados
                dataGridView1.Rows.Clear();
                librosSeleccionados.Clear();

                // Deshabilitar el botón "Agregar Libro"
                btnAgregarLibro.Enabled = false;
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            txtNoControl.Clear();
            btnAgregarLibro.Enabled = false;
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Estás seguro que deseas salir?", "Confirmar", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
            {
                // Limpiar el DataGridView y la lista de libros seleccionados
                dataGridView1.Rows.Clear();
                librosSeleccionados.Clear();

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

        private void btnAgregarLibro_Click_1(object sender, EventArgs e)
        {
            if (cbbNombreL.SelectedIndex != -1)
            {
                string nombreLibro = cbbNombreL.SelectedItem.ToString();

                // Validar que no se haya alcanzado el límite de 3 libros
                if (Cuenta + librosSeleccionados.Count >= 3)
                {
                    MessageBox.Show("Ya has alcanzado el límite máximo de préstamos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Verificar si el libro ya está en la lista
                if (librosSeleccionados.Contains(nombreLibro))
                {
                    MessageBox.Show("Este libro ya ha sido agregado.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Verificar si el alumno ya tiene un préstamo activo de este libro
                if (TienePrestamoActivo(txtNoControl.Text, nombreLibro))
                {
                    MessageBox.Show("Ya tienes un préstamo activo de este libro.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Obtener la cantidad disponible del libro
                int cantidadDisponible = ObtenerCantidadDisponible(nombreLibro);

                if (cantidadDisponible > 0)
                {
                    // Agregar el libro al DataGridView
                    dataGridView1.Rows.Add(nombreLibro, cantidadDisponible);

                    // Agregar el libro a la lista temporal
                    librosSeleccionados.Add(nombreLibro);

                    MessageBox.Show("Libro agregado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("No hay ejemplares disponibles de este libro.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Selecciona un libro primero.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            btnQuitar.Visible = true;
            btnQuitar.Enabled = dataGridView1.SelectedRows.Count > 0;
        }

        private void btnQuitar_Click(object sender, EventArgs e)
        {
            try
            {
                // Verificar si hay filas seleccionadas
                if (dataGridView1.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Selecciona una fila para quitar.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Obtener la fila seleccionada
                DataGridViewRow filaSeleccionada = dataGridView1.SelectedRows[0];

                // Verificar si la fila seleccionada tiene datos válidos
                if (filaSeleccionada == null || filaSeleccionada.Cells["NombreLibro"] == null || filaSeleccionada.Cells["NombreLibro"].Value == null)
                {
                    MessageBox.Show("La fila seleccionada no contiene datos válidos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Obtener el nombre del libro de la fila seleccionada
                string nombreLibro = filaSeleccionada.Cells["NombreLibro"].Value.ToString();

                // Quitar el libro de la lista temporal
                if (librosSeleccionados.Contains(nombreLibro))
                {
                    librosSeleccionados.Remove(nombreLibro);
                }

                // Eliminar la fila del DataGridView
                dataGridView1.Rows.Remove(filaSeleccionada);

                MessageBox.Show("Libro quitado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al quitar el libro: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ActualizarCuenta()
        {
            if (!string.IsNullOrEmpty(txtNoControl.Text))
            {
                SqlConnection con = new SqlConnection();
                con.ConnectionString = "data source = 192.168.27.1,1433; database = Bblioteca; user id = sa; password = sa1@;";
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;

                try
                {
                    con.Open();
                    cmd.CommandText = "Select count(NumeroControlE) from Prestamos where NumeroControlE = '" + txtNoControl.Text + "' and DiaDevolucion is null";
                    object result = cmd.ExecuteScalar();
                    Cuenta = Convert.ToInt32(result);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al actualizar el contador de préstamos: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    if (con.State == ConnectionState.Open)
                    {
                        con.Close();
                    }
                }
            }
        }
        private bool TienePrestamoActivo(string numeroControl, string nombreLibro)
        {
            bool tienePrestamo = false;
            SqlConnection con = new SqlConnection("Data Source=HACEDORDELLUVIA\\MSSQLSERVER3;Initial Catalog=Bblioteca;User ID=sa;Password=sa1@;");
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;

            try
            {
                con.Open();
                cmd.CommandText = "SELECT COUNT(*) FROM Prestamos WHERE NumeroControlE = @NumeroControl AND NombreLibro = @NombreLibro AND DiaDevolucion IS NULL";
                cmd.Parameters.AddWithValue("@NumeroControl", numeroControl);
                cmd.Parameters.AddWithValue("@NombreLibro", nombreLibro);

                int count = Convert.ToInt32(cmd.ExecuteScalar());
                tienePrestamo = count > 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al verificar préstamos activos: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }

            return tienePrestamo;
        }
    }
}
