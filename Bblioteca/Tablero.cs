using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bblioteca
{
    public partial class Tablero : Form
    {
        public Tablero()
        {
            InitializeComponent();
        }

        private void booksToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void prestarLibrosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PrestamoDeLibros PL = new PrestamoDeLibros();
            PL.Show();
        }

        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Estas seguro que desea salir?","Confirme",  MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                Application.Exit();
            }
            
        }

        private void agregarNuevoLibroToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AgregarLibros AL = new AgregarLibros();
            AL.Show();
        }

        private void verLibrosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Ver_Libros verLibros = new Ver_Libros();
            verLibros.Show();
        }

        private void agregarEstudianteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AgregarEstudiante AE = new AgregarEstudiante();
            AE.Show();
        }

        private void verInformacionDeEstudianteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            VerInformacionDeEstudiante vie = new VerInformacionDeEstudiante();
            vie.Show();
        }

        private void regresarLibrosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RegresarLibro Rl = new RegresarLibro();
            Rl.Show();
        }

        private void completarDetallesDeLibrosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DetallesdeLibros Dl = new DetallesdeLibros();
            Dl.Show();
        }

        private void Tablero_Load(object sender, EventArgs e)
        {

        }
    }
}
