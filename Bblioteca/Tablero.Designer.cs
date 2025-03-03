namespace Bblioteca
{
    partial class Tablero
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Tablero));
            this.menuStrip2 = new System.Windows.Forms.MenuStrip();
            this.booksToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.agregarNuevoLibroToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.verLibrosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.estudiantesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.agregarEstudianteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.verInformacionDeEstudianteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.prestarLibrosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.regresarLibrosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.completarDetallesDeLibrosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.salirToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip2.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip2
            // 
            this.menuStrip2.BackColor = System.Drawing.Color.Wheat;
            this.menuStrip2.GripMargin = new System.Windows.Forms.Padding(2, 2, 0, 2);
            this.menuStrip2.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.menuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.booksToolStripMenuItem,
            this.estudiantesToolStripMenuItem,
            this.prestarLibrosToolStripMenuItem,
            this.regresarLibrosToolStripMenuItem,
            this.completarDetallesDeLibrosToolStripMenuItem,
            this.salirToolStripMenuItem});
            this.menuStrip2.Location = new System.Drawing.Point(0, 0);
            this.menuStrip2.Name = "menuStrip2";
            this.menuStrip2.Size = new System.Drawing.Size(1174, 33);
            this.menuStrip2.TabIndex = 1;
            this.menuStrip2.Text = "menuStrip2";
            // 
            // booksToolStripMenuItem
            // 
            this.booksToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.agregarNuevoLibroToolStripMenuItem,
            this.verLibrosToolStripMenuItem});
            this.booksToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("booksToolStripMenuItem.Image")));
            this.booksToolStripMenuItem.Name = "booksToolStripMenuItem";
            this.booksToolStripMenuItem.Size = new System.Drawing.Size(100, 29);
            this.booksToolStripMenuItem.Text = "Libros";
            this.booksToolStripMenuItem.Click += new System.EventHandler(this.booksToolStripMenuItem_Click);
            // 
            // agregarNuevoLibroToolStripMenuItem
            // 
            this.agregarNuevoLibroToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("agregarNuevoLibroToolStripMenuItem.Image")));
            this.agregarNuevoLibroToolStripMenuItem.Name = "agregarNuevoLibroToolStripMenuItem";
            this.agregarNuevoLibroToolStripMenuItem.Size = new System.Drawing.Size(280, 34);
            this.agregarNuevoLibroToolStripMenuItem.Text = "Agregar Nuevo Libro";
            this.agregarNuevoLibroToolStripMenuItem.Click += new System.EventHandler(this.agregarNuevoLibroToolStripMenuItem_Click);
            // 
            // verLibrosToolStripMenuItem
            // 
            this.verLibrosToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("verLibrosToolStripMenuItem.Image")));
            this.verLibrosToolStripMenuItem.Name = "verLibrosToolStripMenuItem";
            this.verLibrosToolStripMenuItem.Size = new System.Drawing.Size(280, 34);
            this.verLibrosToolStripMenuItem.Text = "Ver Libros";
            this.verLibrosToolStripMenuItem.Click += new System.EventHandler(this.verLibrosToolStripMenuItem_Click);
            // 
            // estudiantesToolStripMenuItem
            // 
            this.estudiantesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.agregarEstudianteToolStripMenuItem,
            this.verInformacionDeEstudianteToolStripMenuItem});
            this.estudiantesToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("estudiantesToolStripMenuItem.Image")));
            this.estudiantesToolStripMenuItem.Name = "estudiantesToolStripMenuItem";
            this.estudiantesToolStripMenuItem.Size = new System.Drawing.Size(142, 29);
            this.estudiantesToolStripMenuItem.Text = "Estudiantes";
            // 
            // agregarEstudianteToolStripMenuItem
            // 
            this.agregarEstudianteToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("agregarEstudianteToolStripMenuItem.Image")));
            this.agregarEstudianteToolStripMenuItem.Name = "agregarEstudianteToolStripMenuItem";
            this.agregarEstudianteToolStripMenuItem.Size = new System.Drawing.Size(297, 34);
            this.agregarEstudianteToolStripMenuItem.Text = "Agregar Estudiante";
            this.agregarEstudianteToolStripMenuItem.Click += new System.EventHandler(this.agregarEstudianteToolStripMenuItem_Click);
            // 
            // verInformacionDeEstudianteToolStripMenuItem
            // 
            this.verInformacionDeEstudianteToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("verInformacionDeEstudianteToolStripMenuItem.Image")));
            this.verInformacionDeEstudianteToolStripMenuItem.Name = "verInformacionDeEstudianteToolStripMenuItem";
            this.verInformacionDeEstudianteToolStripMenuItem.Size = new System.Drawing.Size(297, 34);
            this.verInformacionDeEstudianteToolStripMenuItem.Text = "Ver Info. de Estudiante ";
            this.verInformacionDeEstudianteToolStripMenuItem.Click += new System.EventHandler(this.verInformacionDeEstudianteToolStripMenuItem_Click);
            // 
            // prestarLibrosToolStripMenuItem
            // 
            this.prestarLibrosToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("prestarLibrosToolStripMenuItem.Image")));
            this.prestarLibrosToolStripMenuItem.Name = "prestarLibrosToolStripMenuItem";
            this.prestarLibrosToolStripMenuItem.Size = new System.Drawing.Size(205, 29);
            this.prestarLibrosToolStripMenuItem.Text = "Prestamo de Libros";
            this.prestarLibrosToolStripMenuItem.Click += new System.EventHandler(this.prestarLibrosToolStripMenuItem_Click);
            // 
            // regresarLibrosToolStripMenuItem
            // 
            this.regresarLibrosToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("regresarLibrosToolStripMenuItem.Image")));
            this.regresarLibrosToolStripMenuItem.Name = "regresarLibrosToolStripMenuItem";
            this.regresarLibrosToolStripMenuItem.Size = new System.Drawing.Size(173, 29);
            this.regresarLibrosToolStripMenuItem.Text = "Regresar Libros";
            this.regresarLibrosToolStripMenuItem.Click += new System.EventHandler(this.regresarLibrosToolStripMenuItem_Click);
            // 
            // completarDetallesDeLibrosToolStripMenuItem
            // 
            this.completarDetallesDeLibrosToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("completarDetallesDeLibrosToolStripMenuItem.Image")));
            this.completarDetallesDeLibrosToolStripMenuItem.Name = "completarDetallesDeLibrosToolStripMenuItem";
            this.completarDetallesDeLibrosToolStripMenuItem.Size = new System.Drawing.Size(193, 29);
            this.completarDetallesDeLibrosToolStripMenuItem.Text = "Detalles de libros ";
            this.completarDetallesDeLibrosToolStripMenuItem.Click += new System.EventHandler(this.completarDetallesDeLibrosToolStripMenuItem_Click);
            // 
            // salirToolStripMenuItem
            // 
            this.salirToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("salirToolStripMenuItem.Image")));
            this.salirToolStripMenuItem.Name = "salirToolStripMenuItem";
            this.salirToolStripMenuItem.Size = new System.Drawing.Size(85, 29);
            this.salirToolStripMenuItem.Text = "Salir";
            this.salirToolStripMenuItem.Click += new System.EventHandler(this.salirToolStripMenuItem_Click);
            // 
            // Tablero
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Wheat;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1174, 614);
            this.Controls.Add(this.menuStrip2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Tablero";
            this.Text = "Tablero";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Tablero_Load);
            this.menuStrip2.ResumeLayout(false);
            this.menuStrip2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.MenuStrip menuStrip2;
        private System.Windows.Forms.ToolStripMenuItem booksToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem agregarNuevoLibroToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem verLibrosToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem estudiantesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem agregarEstudianteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem verInformacionDeEstudianteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem prestarLibrosToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem regresarLibrosToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem completarDetallesDeLibrosToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem salirToolStripMenuItem;
    }
}