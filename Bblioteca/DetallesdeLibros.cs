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
    public partial class DetallesdeLibros : Form
    {
        public DetallesdeLibros()
        {
            InitializeComponent();
        }

        private void DetallesdeLibros_Load(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = "Data Source=HACEDORDELLUVIA\\MSSQLSERVER3;Initial Catalog=Bblioteca;User ID=sa;Password=sa1@;";
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;

            cmd.CommandText = "Select * from Prestamos where DiaDevolucion is null";
            SqlDataAdapter DA = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            DA.Fill(ds);

            dataGridView1.DataSource = ds.Tables[0];

            cmd.CommandText = "Select * from Prestamos where DiaDevolucion is not null";
            SqlDataAdapter DA1 = new SqlDataAdapter(cmd);
            DataSet ds1 = new DataSet();
            DA.Fill(ds1);

            dataGridView2.DataSource = ds1.Tables[0];
        }
    }
}
