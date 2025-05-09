namespace Bblioteca
{
    partial class Multas
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
            this.tbxBuscarM = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.DGVMulta = new System.Windows.Forms.DataGridView();
            this.btnMulta = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.DGVMulta)).BeginInit();
            this.SuspendLayout();
            // 
            // tbxBuscarM
            // 
            this.tbxBuscarM.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxBuscarM.Location = new System.Drawing.Point(308, 93);
            this.tbxBuscarM.Name = "tbxBuscarM";
            this.tbxBuscarM.Size = new System.Drawing.Size(182, 30);
            this.tbxBuscarM.TabIndex = 0;
            this.tbxBuscarM.TextChanged += new System.EventHandler(this.tbxBuscarM_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(303, 48);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(187, 25);
            this.label1.TabIndex = 1;
            this.label1.Text = "Buscar Estudiante";
            // 
            // DGVMulta
            // 
            this.DGVMulta.AllowUserToAddRows = false;
            this.DGVMulta.AllowUserToDeleteRows = false;
            this.DGVMulta.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DGVMulta.Location = new System.Drawing.Point(40, 143);
            this.DGVMulta.Name = "DGVMulta";
            this.DGVMulta.ReadOnly = true;
            this.DGVMulta.RowHeadersWidth = 51;
            this.DGVMulta.RowTemplate.Height = 24;
            this.DGVMulta.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.DGVMulta.Size = new System.Drawing.Size(720, 195);
            this.DGVMulta.TabIndex = 3;
            // 
            // btnMulta
            // 
            this.btnMulta.BackColor = System.Drawing.Color.White;
            this.btnMulta.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnMulta.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMulta.Location = new System.Drawing.Point(40, 366);
            this.btnMulta.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnMulta.Name = "btnMulta";
            this.btnMulta.Size = new System.Drawing.Size(151, 44);
            this.btnMulta.TabIndex = 7;
            this.btnMulta.Text = "Generar Multa";
            this.btnMulta.UseVisualStyleBackColor = false;
            this.btnMulta.Click += new System.EventHandler(this.btnMulta_Click);
            // 
            // Multas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnMulta);
            this.Controls.Add(this.DGVMulta);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tbxBuscarM);
            this.Name = "Multas";
            this.Text = "Multas";
            ((System.ComponentModel.ISupportInitialize)(this.DGVMulta)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbxBuscarM;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView DGVMulta;
        private System.Windows.Forms.Button btnMulta;
    }
}