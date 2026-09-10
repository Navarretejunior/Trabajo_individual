namespace Interfaces_de_Usuario_Propuestas_Payless.Formularios
{
    partial class SubClienteAgregar
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SubClienteAgregar));
            this.panel2 = new System.Windows.Forms.Panel();
            this.label18 = new System.Windows.Forms.Label();
            this.pictureBox4 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.CBestado = new System.Windows.Forms.ComboBox();
            this.txtTelefono = new System.Windows.Forms.TextBox();
            this.q = new System.Windows.Forms.TextBox();
            this.LblNombre = new System.Windows.Forms.Label();
            this.Lbltelefono = new System.Windows.Forms.Label();
            this.LblEstado = new System.Windows.Forms.Label();
            this.LblBusqueda = new System.Windows.Forms.Label();
            this.txtcedula = new System.Windows.Forms.TextBox();
            this.LblNumero = new System.Windows.Forms.Label();
            this.richtxtDirreccion = new System.Windows.Forms.RichTextBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.White;
            this.panel2.Controls.Add(this.label18);
            this.panel2.Controls.Add(this.pictureBox4);
            this.panel2.Controls.Add(this.label1);
            this.panel2.ForeColor = System.Drawing.Color.White;
            this.panel2.Location = new System.Drawing.Point(10, 11);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(567, 70);
            this.panel2.TabIndex = 63;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label18.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(106)))), ((int)(((byte)(0)))));
            this.label18.Location = new System.Drawing.Point(457, 29);
            this.label18.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(92, 19);
            this.label18.TabIndex = 11;
            this.label18.Text = "Acerca De...";
            // 
            // pictureBox4
            // 
            this.pictureBox4.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox4.Image")));
            this.pictureBox4.Location = new System.Drawing.Point(2, 2);
            this.pictureBox4.Margin = new System.Windows.Forms.Padding(2);
            this.pictureBox4.Name = "pictureBox4";
            this.pictureBox4.Size = new System.Drawing.Size(67, 60);
            this.pictureBox4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox4.TabIndex = 85;
            this.pictureBox4.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(106)))), ((int)(((byte)(0)))));
            this.label1.Location = new System.Drawing.Point(101, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(244, 36);
            this.label1.TabIndex = 0;
            this.label1.Text = "Agregar Clientes";
            // 
            // btnCancelar
            // 
            this.btnCancelar.BackColor = System.Drawing.Color.Black;
            this.btnCancelar.Font = new System.Drawing.Font("Times New Roman", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancelar.ForeColor = System.Drawing.Color.White;
            this.btnCancelar.Location = new System.Drawing.Point(397, 511);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(75, 43);
            this.btnCancelar.TabIndex = 100;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = false;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // btnGuardar
            // 
            this.btnGuardar.BackColor = System.Drawing.Color.Black;
            this.btnGuardar.Font = new System.Drawing.Font("Times New Roman", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGuardar.ForeColor = System.Drawing.Color.White;
            this.btnGuardar.Location = new System.Drawing.Point(212, 511);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(75, 43);
            this.btnGuardar.TabIndex = 99;
            this.btnGuardar.Text = "Guardar";
            this.btnGuardar.UseVisualStyleBackColor = false;
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // CBestado
            // 
            this.CBestado.Font = new System.Drawing.Font("Times New Roman", 12F);
            this.CBestado.FormattingEnabled = true;
            this.CBestado.Items.AddRange(new object[] {
            "Activo",
            "Inactivo"});
            this.CBestado.Location = new System.Drawing.Point(166, 257);
            this.CBestado.Name = "CBestado";
            this.CBestado.Size = new System.Drawing.Size(131, 27);
            this.CBestado.TabIndex = 98;
            // 
            // txtTelefono
            // 
            this.txtTelefono.Font = new System.Drawing.Font("Times New Roman", 12F);
            this.txtTelefono.Location = new System.Drawing.Point(166, 214);
            this.txtTelefono.Name = "txtTelefono";
            this.txtTelefono.Size = new System.Drawing.Size(235, 26);
            this.txtTelefono.TabIndex = 96;
            // 
            // q
            // 
            this.q.BackColor = System.Drawing.Color.White;
            this.q.Font = new System.Drawing.Font("Times New Roman", 12F);
            this.q.Location = new System.Drawing.Point(166, 131);
            this.q.Name = "q";
            this.q.Size = new System.Drawing.Size(235, 26);
            this.q.TabIndex = 95;
            // 
            // LblNombre
            // 
            this.LblNombre.AutoSize = true;
            this.LblNombre.BackColor = System.Drawing.Color.White;
            this.LblNombre.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblNombre.ForeColor = System.Drawing.Color.Black;
            this.LblNombre.Location = new System.Drawing.Point(22, 132);
            this.LblNombre.Name = "LblNombre";
            this.LblNombre.Size = new System.Drawing.Size(115, 19);
            this.LblNombre.TabIndex = 94;
            this.LblNombre.Text = "Nombre Cliente";
            // 
            // Lbltelefono
            // 
            this.Lbltelefono.AutoSize = true;
            this.Lbltelefono.BackColor = System.Drawing.Color.White;
            this.Lbltelefono.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Lbltelefono.ForeColor = System.Drawing.Color.Black;
            this.Lbltelefono.Location = new System.Drawing.Point(22, 213);
            this.Lbltelefono.Name = "Lbltelefono";
            this.Lbltelefono.Size = new System.Drawing.Size(68, 19);
            this.Lbltelefono.TabIndex = 93;
            this.Lbltelefono.Text = "Télefono";
            // 
            // LblEstado
            // 
            this.LblEstado.AutoSize = true;
            this.LblEstado.BackColor = System.Drawing.Color.White;
            this.LblEstado.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblEstado.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.LblEstado.Location = new System.Drawing.Point(22, 256);
            this.LblEstado.Name = "LblEstado";
            this.LblEstado.Size = new System.Drawing.Size(55, 19);
            this.LblEstado.TabIndex = 91;
            this.LblEstado.Text = "Estado";
            // 
            // LblBusqueda
            // 
            this.LblBusqueda.AutoSize = true;
            this.LblBusqueda.BackColor = System.Drawing.Color.White;
            this.LblBusqueda.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblBusqueda.ForeColor = System.Drawing.Color.Black;
            this.LblBusqueda.Location = new System.Drawing.Point(22, 296);
            this.LblBusqueda.Name = "LblBusqueda";
            this.LblBusqueda.Size = new System.Drawing.Size(79, 19);
            this.LblBusqueda.TabIndex = 90;
            this.LblBusqueda.Text = "Dirrección";
            // 
            // txtcedula
            // 
            this.txtcedula.BackColor = System.Drawing.Color.White;
            this.txtcedula.Font = new System.Drawing.Font("Times New Roman", 12F);
            this.txtcedula.Location = new System.Drawing.Point(166, 174);
            this.txtcedula.Name = "txtcedula";
            this.txtcedula.Size = new System.Drawing.Size(235, 26);
            this.txtcedula.TabIndex = 108;
            // 
            // LblNumero
            // 
            this.LblNumero.AutoSize = true;
            this.LblNumero.BackColor = System.Drawing.Color.White;
            this.LblNumero.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblNumero.ForeColor = System.Drawing.Color.Black;
            this.LblNumero.Location = new System.Drawing.Point(22, 173);
            this.LblNumero.Name = "LblNumero";
            this.LblNumero.Size = new System.Drawing.Size(134, 19);
            this.LblNumero.TabIndex = 107;
            this.LblNumero.Text = "Número de Cédula";
            // 
            // richtxtDirreccion
            // 
            this.richtxtDirreccion.Font = new System.Drawing.Font("Times New Roman", 12F);
            this.richtxtDirreccion.Location = new System.Drawing.Point(166, 298);
            this.richtxtDirreccion.Margin = new System.Windows.Forms.Padding(2);
            this.richtxtDirreccion.Name = "richtxtDirreccion";
            this.richtxtDirreccion.Size = new System.Drawing.Size(357, 152);
            this.richtxtDirreccion.TabIndex = 110;
            this.richtxtDirreccion.Text = "";
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // SubClienteAgregar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(586, 571);
            this.Controls.Add(this.richtxtDirreccion);
            this.Controls.Add(this.txtcedula);
            this.Controls.Add(this.LblNumero);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.btnGuardar);
            this.Controls.Add(this.CBestado);
            this.Controls.Add(this.txtTelefono);
            this.Controls.Add(this.q);
            this.Controls.Add(this.LblNombre);
            this.Controls.Add(this.Lbltelefono);
            this.Controls.Add(this.LblEstado);
            this.Controls.Add(this.LblBusqueda);
            this.Controls.Add(this.panel2);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "SubClienteAgregar";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "SubClienteAgregar";
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.PictureBox pictureBox4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.ComboBox CBestado;
        private System.Windows.Forms.TextBox txtTelefono;
        private System.Windows.Forms.TextBox q;
        private System.Windows.Forms.Label LblNombre;
        private System.Windows.Forms.Label Lbltelefono;
        private System.Windows.Forms.Label LblEstado;
        private System.Windows.Forms.Label LblBusqueda;
        private System.Windows.Forms.TextBox txtcedula;
        private System.Windows.Forms.Label LblNumero;
        private System.Windows.Forms.RichTextBox richtxtDirreccion;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
    }
}