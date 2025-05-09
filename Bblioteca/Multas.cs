using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace Bblioteca
{
    public partial class Multas : Form
    {
        private readonly SqlConnection con;
        private SqlCommand cmd;
        private SqlDataAdapter da;
        private DataTable dt;

        public Multas()
        {
            InitializeComponent();
            con = new SqlConnection("data source=localhost; database=Bblioteca; user id=sa; password=sa1@;");
            this.Load += Multas_Load;
        }

        private void Multas_Load(object sender, EventArgs e)
        {
            DGVMulta.Columns.Clear();
            DGVMulta.AutoGenerateColumns = false;

            var colIdMulta = new DataGridViewTextBoxColumn
            {
                HeaderText = "ID Multa",
                DataPropertyName = "IdMulta",
                Name = "IdMulta"
            };
            DGVMulta.Columns.Add(colIdMulta);

            var colIdPrestamo = new DataGridViewTextBoxColumn
            {
                HeaderText = "ID Préstamo",
                DataPropertyName = "IdPrestamo",
                Name = "IdPrestamo"
            };
            DGVMulta.Columns.Add(colIdPrestamo);

            var colNombreE = new DataGridViewTextBoxColumn
            {
                HeaderText = "Nombre del Alumno",
                DataPropertyName = "NombreE",
                Name = "NombreE"
            };
            DGVMulta.Columns.Add(colNombreE);

            var colMonto = new DataGridViewTextBoxColumn
            {
                HeaderText = "Monto",
                DataPropertyName = "Monto",
                Name = "Monto"
            };
            DGVMulta.Columns.Add(colMonto);

            var colFechaGen = new DataGridViewTextBoxColumn
            {
                HeaderText = "Fecha Generación",
                DataPropertyName = "FechaGeneracion",
                Name = "FechaGeneracion"
            };
            DGVMulta.Columns.Add(colFechaGen);

            var pagadoCol = new DataGridViewCheckBoxColumn
            {
                HeaderText = "Pagado",
                DataPropertyName = "Pagado",
                Name = "Pagado"
            };
            DGVMulta.Columns.Add(pagadoCol);

            // Generar multas antes de cargar
            GenerarMultasAutomaticas();
            CargarMultasNoPagadas();
        }

        private void CargarMultasNoPagadas()
        {
            try
            {
                con.Open();
                cmd = new SqlCommand(@"SELECT m.IdMulta, m.IdPrestamo, p.NombreE, m.Monto, m.FechaGeneracion, m.Pagado 
                                        FROM Multas m
                                        INNER JOIN Prestamos p ON m.IdPrestamo = p.IdPrestamo
                                        WHERE m.Pagado = 0", con);
                da = new SqlDataAdapter(cmd);
                dt = new DataTable();
                da.Fill(dt);
                DGVMulta.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar multas: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                    con.Close();
            }
        }

        private void tbxBuscarM_TextChanged(object sender, EventArgs e)
        {
            if (dt != null)
            {
                DataView dv = dt.DefaultView;
                dv.RowFilter = $"NombreE LIKE '%{tbxBuscarM.Text}%";
                DGVMulta.DataSource = dv;
            }
        }

        private void btnMulta_Click(object sender, EventArgs e)
        {
            if (DGVMulta.SelectedRows.Count == 0)
            {
                MessageBox.Show("Selecciona una multa primero.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var row = DGVMulta.SelectedRows[0];
            int idMulta = Convert.ToInt32(row.Cells["IdMulta"].Value);
            int idPrestamo = Convert.ToInt32(row.Cells["IdPrestamo"].Value);
            decimal monto = Convert.ToDecimal(row.Cells["Monto"].Value);
            DateTime fechaGeneracion = Convert.ToDateTime(row.Cells["FechaGeneracion"].Value);
            bool pagado = Convert.ToBoolean(row.Cells["Pagado"].Value);

            GenerarMultaPDF(idMulta, idPrestamo, monto, fechaGeneracion, pagado);
        }

        private void GenerarMultaPDF(int idMulta, int idPrestamo, decimal montoOriginal, DateTime fechaGeneracion, bool pagado)
        {
            try
            {
                string nombreLibro = "";
                string diaPrestamo = "";
                DateTime fechaEntrega;

                using (var conTemp = new SqlConnection("data source=localhost; database=Bblioteca; user id=sa; password=sa1@;"))
                {
                    conTemp.Open();
                    using (var cmd = new SqlCommand("SELECT NombreLibro, DiaPrestamo, FechaEntrega FROM Prestamos WHERE IdPrestamo = @idPrestamo", conTemp))
                    {
                        cmd.Parameters.AddWithValue("@idPrestamo", idPrestamo);
                        using (var reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                nombreLibro = reader["NombreLibro"].ToString();
                                diaPrestamo = reader["DiaPrestamo"].ToString();
                                fechaEntrega = Convert.ToDateTime(reader["FechaEntrega"]);
                            }
                            else
                            {
                                throw new Exception("No se encontró el préstamo.");
                            }
                        }
                    }
                }

                DateTime fechaHoy = DateTime.Today;
                int diasRetraso = (fechaHoy - fechaEntrega).Days;
                decimal montoFinal = montoOriginal;

                if (diasRetraso > 0)
                {
                    montoFinal = diasRetraso * 30m;
                    if (montoFinal > 700m)
                        montoFinal = 700m;
                }

                // Actualizar el monto de la multa en la base de datos
                using (var conUpdate = new SqlConnection("data source=localhost; database=Bblioteca; user id=sa; password=sa1@;"))
                {
                    conUpdate.Open();
                    using (var cmd = new SqlCommand("UPDATE Multas SET Monto = @montoFinal WHERE IdMulta = @idMulta", conUpdate))
                    {
                        cmd.Parameters.AddWithValue("@montoFinal", montoFinal);
                        cmd.Parameters.AddWithValue("@idMulta", idMulta);
                        cmd.ExecuteNonQuery();
                    }
                }

                CargarMultasNoPagadas(); // refrescar

                // Crear el PDF
                string carpeta = Path.Combine(Application.StartupPath, "MultasGeneradas");
                Directory.CreateDirectory(carpeta);
                string rutaArchivo = Path.Combine(carpeta, $"Multa_{idMulta}.pdf");

                using (var fs = new FileStream(rutaArchivo, FileMode.Create))
                {
                    var customSize = new iTextSharp.text.Rectangle(250f, 400f);
                    var doc = new Document(customSize, 20, 20, 20, 20);
                    PdfWriter.GetInstance(doc, fs);
                    doc.Open();

                    var fuenteTitulo = FontFactory.GetFont("Helvetica", 18f, iTextSharp.text.Font.BOLD);
                    var fuenteTexto = FontFactory.GetFont("Helvetica", 10f, iTextSharp.text.Font.NORMAL);
                    var fuenteNegrita = FontFactory.GetFont("Helvetica", 10f, iTextSharp.text.Font.BOLD);

                    doc.Add(new Paragraph("RECIBO DE MULTA\n\n", fuenteTitulo) { Alignment = Element.ALIGN_CENTER });

                    var tabla = new PdfPTable(2) { WidthPercentage = 100 };
                    tabla.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;

                    tabla.AddCell(new Phrase("Libro:", fuenteNegrita));
                    tabla.AddCell(new Phrase(nombreLibro, fuenteTexto));

                    tabla.AddCell(new Phrase("Fecha Préstamo:", fuenteNegrita));
                    tabla.AddCell(new Phrase(diaPrestamo, fuenteTexto));

                    tabla.AddCell(new Phrase("Fecha Entrega:", fuenteNegrita));
                    tabla.AddCell(new Phrase(fechaEntrega.ToString("dd/MM/yyyy"), fuenteTexto));

                    tabla.AddCell(new Phrase("Fecha Multa:", fuenteNegrita));
                    tabla.AddCell(new Phrase(fechaGeneracion.ToString("dd/MM/yyyy"), fuenteTexto));

                    tabla.AddCell(new Phrase("Días de Retraso:", fuenteNegrita));
                    tabla.AddCell(new Phrase(diasRetraso.ToString() + " día(s)", fuenteTexto));

                    tabla.SpacingAfter = 15f;
                    doc.Add(tabla);

                    var total = new Paragraph($"TOTAL A PAGAR:\n${montoFinal:F2}\n", FontFactory.GetFont("Helvetica", 14f, iTextSharp.text.Font.BOLD))
                    {
                        Alignment = Element.ALIGN_CENTER,
                        SpacingAfter = 15f
                    };
                    doc.Add(total);

                    var estado = new Paragraph($"Estado: {(pagado ? "Pagado" : "Pendiente de Pago")}", fuenteNegrita) { Alignment = Element.ALIGN_CENTER };
                    doc.Add(estado);

                    doc.Close();
                }

                // Abrir el PDF
                Process.Start(new ProcessStartInfo { FileName = rutaArchivo, UseShellExecute = true });

                // Preguntar si ya pagó
                DialogResult result = MessageBox.Show(
                    "¿El alumno ya pagó la multa?",
                    "Confirmar pago",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                );

                if (result == DialogResult.Yes)
                {
                    using (var conTemp = new SqlConnection("data source=localhost; database=Bblioteca; user id=sa; password=sa1@;"))
                    {
                        conTemp.Open();
                        using (var cmdUpdate = new SqlCommand(@"UPDATE Multas 
                                                SET Pagado = 1, 
                                                    FechaPago = @fechaPago 
                                                WHERE IdMulta = @idMulta", conTemp))
                        {
                            cmdUpdate.Parameters.AddWithValue("@fechaPago", DateTime.Now);
                            cmdUpdate.Parameters.AddWithValue("@idMulta", idMulta);
                            cmdUpdate.ExecuteNonQuery();
                        }
                    }

                    MessageBox.Show("Multa marcada como pagada exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    CargarMultasNoPagadas(); // refrescar para quitar la multa del grid
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al generar el PDF o actualizar la multa: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void GenerarMultasAutomaticas()
        {
            try
            {
                using (var conTemp = new SqlConnection("data source=localhost; database=Bblioteca; user id=sa; password=sa1@;"))
                {
                    conTemp.Open();
                    using (var cmd = new SqlCommand(@"SELECT p.IdPrestamo, p.FechaEntrega
                                                    FROM Prestamos p
                                                    WHERE p.FechaEntrega IS NOT NULL
                                                    AND NOT EXISTS (SELECT 1 FROM Multas m WHERE m.IdPrestamo = p.IdPrestamo)", conTemp))
                    {
                        using (var reader = cmd.ExecuteReader())
                        {
                            var prestamos = new List<(int IdPrestamo, DateTime FechaEntrega)>();

                            while (reader.Read())
                            {
                                prestamos.Add((Convert.ToInt32(reader["IdPrestamo"]), Convert.ToDateTime(reader["FechaEntrega"])));
                            }
                            reader.Close();

                            foreach (var prestamo in prestamos)
                            {
                                int diasRetraso = (DateTime.Today - prestamo.FechaEntrega).Days;
                                if (diasRetraso > 0)
                                {
                                    decimal montoMulta = diasRetraso * 30m;
                                    if (montoMulta > 700m)
                                        montoMulta = 700m;

                                    using (var cmdInsert = new SqlCommand("INSERT INTO Multas (IdPrestamo, Monto, FechaGeneracion, Pagado) VALUES (@idPrestamo, @monto, @fechaGeneracion, 0)", conTemp))
                                    {
                                        cmdInsert.Parameters.AddWithValue("@idPrestamo", prestamo.IdPrestamo);
                                        cmdInsert.Parameters.AddWithValue("@monto", montoMulta);
                                        cmdInsert.Parameters.AddWithValue("@fechaGeneracion", DateTime.Now);
                                        cmdInsert.ExecuteNonQuery();
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al generar multas automáticas: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
