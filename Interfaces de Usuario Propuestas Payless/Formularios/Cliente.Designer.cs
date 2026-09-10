namespace Interfaces_de_Usuario_Propuestas_Payless
{
    partial class Cliente
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Cliente));
            this.CmbBusqueda = new System.Windows.Forms.ComboBox();
            this.DGVtabla1 = new System.Windows.Forms.DataGridView();
            this.LblBusqueda = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label11 = new System.Windows.Forms.Label();
            this.lblMantenimiento = new System.Windows.Forms.Label();
            this.lblInventario = new System.Windows.Forms.Label();
            this.lblProveedores = new System.Windows.Forms.Label();
            this.label26 = new System.Windows.Forms.Label();
            this.lblCaja = new System.Windows.Forms.Label();
            this.lblCredito = new System.Windows.Forms.Label();
            this.lblDevoluciones = new System.Windows.Forms.Label();
            this.lblVenta = new System.Windows.Forms.Label();
            this.lblCompras = new System.Windows.Forms.Label();
            this.lblUsuarios = new System.Windows.Forms.Label();
            this.lblCliente = new System.Windows.Forms.Label();
            this.lblProductos = new System.Windows.Forms.Label();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label18 = new System.Windows.Forms.Label();
            this.pictureBox4 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnbuscar = new System.Windows.Forms.Button();
            this.btnAgregar = new System.Windows.Forms.Button();
            this.btnEditar = new System.Windows.Forms.Button();
            this.btnEliminar = new System.Windows.Forms.Button();
            this.txtBuscar = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.DGVtabla1)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
            this.SuspendLayout();
            // 
            // CmbBusqueda
            // 
            this.CmbBusqueda.FormattingEnabled = true;
            this.CmbBusqueda.Items.AddRange(new object[] {
            "Nombre",
            "Numero de cédula",
            "Código"});
            this.CmbBusqueda.Location = new System.Drawing.Point(229, 121);
            this.CmbBusqueda.Margin = new System.Windows.Forms.Padding(4);
            this.CmbBusqueda.Name = "CmbBusqueda";
            this.CmbBusqueda.Size = new System.Drawing.Size(231, 24);
            this.CmbBusqueda.TabIndex = 27;
            this.CmbBusqueda.SelectedIndexChanged += new System.EventHandler(this.CBbusqueda_SelectedIndexChanged);
            // 
            // DGVtabla1
            // 
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(106)))), ((int)(((byte)(0)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.DGVtabla1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.DGVtabla1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.DGVtabla1.DefaultCellStyle = dataGridViewCellStyle2;
            this.DGVtabla1.EnableHeadersVisualStyles = false;
            this.DGVtabla1.Location = new System.Drawing.Point(229, 188);
            this.DGVtabla1.Margin = new System.Windows.Forms.Padding(4);
            this.DGVtabla1.Name = "DGVtabla1";
            this.DGVtabla1.RowHeadersWidth = 62;
            this.DGVtabla1.Size = new System.Drawing.Size(853, 332);
            this.DGVtabla1.TabIndex = 26;
            this.DGVtabla1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DGVtabla1_CellContentClick);
            // 
            // LblBusqueda
            // 
            this.LblBusqueda.AutoSize = true;
            this.LblBusqueda.BackColor = System.Drawing.Color.White;
            this.LblBusqueda.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblBusqueda.ForeColor = System.Drawing.Color.Black;
            this.LblBusqueda.Location = new System.Drawing.Point(225, 94);
            this.LblBusqueda.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.LblBusqueda.Name = "LblBusqueda";
            this.LblBusqueda.Size = new System.Drawing.Size(101, 23);
            this.LblBusqueda.TabIndex = 15;
            this.LblBusqueda.Text = "Buscar por";
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.White;
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.lblMantenimiento);
            this.groupBox1.Controls.Add(this.lblInventario);
            this.groupBox1.Controls.Add(this.lblProveedores);
            this.groupBox1.Controls.Add(this.label26);
            this.groupBox1.Controls.Add(this.lblCaja);
            this.groupBox1.Controls.Add(this.lblCredito);
            this.groupBox1.Controls.Add(this.lblDevoluciones);
            this.groupBox1.Controls.Add(this.lblVenta);
            this.groupBox1.Controls.Add(this.lblCompras);
            this.groupBox1.Controls.Add(this.lblUsuarios);
            this.groupBox1.Controls.Add(this.lblCliente);
            this.groupBox1.Controls.Add(this.lblProductos);
            this.groupBox1.Controls.Add(this.pictureBox3);
            this.groupBox1.ForeColor = System.Drawing.Color.White;
            this.groupBox1.Location = new System.Drawing.Point(-5, 44);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox1.Size = new System.Drawing.Size(221, 862);
            this.groupBox1.TabIndex = 59;
            this.groupBox1.TabStop = false;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.BackColor = System.Drawing.Color.White;
            this.label11.Cursor = System.Windows.Forms.Cursors.Hand;
            this.label11.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.ForeColor = System.Drawing.Color.Black;
            this.label11.Location = new System.Drawing.Point(25, 827);
            this.label11.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(100, 23);
            this.label11.TabIndex = 127;
            this.label11.Text = "🆘   Ayuda";
            // 
            // lblMantenimiento
            // 
            this.lblMantenimiento.AutoSize = true;
            this.lblMantenimiento.BackColor = System.Drawing.Color.White;
            this.lblMantenimiento.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblMantenimiento.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMantenimiento.ForeColor = System.Drawing.Color.Black;
            this.lblMantenimiento.Location = new System.Drawing.Point(20, 767);
            this.lblMantenimiento.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lblMantenimiento.Name = "lblMantenimiento";
            this.lblMantenimiento.Size = new System.Drawing.Size(174, 23);
            this.lblMantenimiento.TabIndex = 126;
            this.lblMantenimiento.Text = "🛠️   Mantenimiento";
            this.lblMantenimiento.Click += new System.EventHandler(this.label12_Click);
            // 
            // lblInventario
            // 
            this.lblInventario.AutoSize = true;
            this.lblInventario.BackColor = System.Drawing.Color.White;
            this.lblInventario.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblInventario.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInventario.ForeColor = System.Drawing.Color.Black;
            this.lblInventario.Location = new System.Drawing.Point(21, 713);
            this.lblInventario.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lblInventario.Name = "lblInventario";
            this.lblInventario.Size = new System.Drawing.Size(134, 23);
            this.lblInventario.TabIndex = 118;
            this.lblInventario.Text = "🏢   Inventario";
            this.lblInventario.Click += new System.EventHandler(this.label10_Click);
            // 
            // lblProveedores
            // 
            this.lblProveedores.AutoSize = true;
            this.lblProveedores.BackColor = System.Drawing.Color.White;
            this.lblProveedores.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblProveedores.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProveedores.ForeColor = System.Drawing.Color.Black;
            this.lblProveedores.Location = new System.Drawing.Point(21, 433);
            this.lblProveedores.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lblProveedores.Name = "lblProveedores";
            this.lblProveedores.Size = new System.Drawing.Size(153, 23);
            this.lblProveedores.TabIndex = 117;
            this.lblProveedores.Text = "🚚   Proveedores";
            this.lblProveedores.Click += new System.EventHandler(this.lblProveedores_Click);
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.BackColor = System.Drawing.Color.White;
            this.label26.Cursor = System.Windows.Forms.Cursors.Hand;
            this.label26.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label26.ForeColor = System.Drawing.Color.Black;
            this.label26.Location = new System.Drawing.Point(23, 180);
            this.label26.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(88, 23);
            this.label26.TabIndex = 116;
            this.label26.Text = "☰   Menú";
            this.label26.Click += new System.EventHandler(this.label26_Click);
            // 
            // lblCaja
            // 
            this.lblCaja.AutoSize = true;
            this.lblCaja.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblCaja.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold);
            this.lblCaja.ForeColor = System.Drawing.Color.Black;
            this.lblCaja.Location = new System.Drawing.Point(23, 228);
            this.lblCaja.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lblCaja.Name = "lblCaja";
            this.lblCaja.Size = new System.Drawing.Size(88, 23);
            this.lblCaja.TabIndex = 115;
            this.lblCaja.Text = "💰   Caja";
            this.lblCaja.Click += new System.EventHandler(this.label20_Click_1);
            // 
            // lblCredito
            // 
            this.lblCredito.AutoSize = true;
            this.lblCredito.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblCredito.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold);
            this.lblCredito.ForeColor = System.Drawing.Color.Black;
            this.lblCredito.Location = new System.Drawing.Point(21, 608);
            this.lblCredito.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lblCredito.Name = "lblCredito";
            this.lblCredito.Size = new System.Drawing.Size(111, 23);
            this.lblCredito.TabIndex = 114;
            this.lblCredito.Text = "🧾   Credito";
            this.lblCredito.Click += new System.EventHandler(this.label21_Click);
            // 
            // lblDevoluciones
            // 
            this.lblDevoluciones.AutoSize = true;
            this.lblDevoluciones.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblDevoluciones.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold);
            this.lblDevoluciones.ForeColor = System.Drawing.Color.Black;
            this.lblDevoluciones.Location = new System.Drawing.Point(19, 658);
            this.lblDevoluciones.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lblDevoluciones.Name = "lblDevoluciones";
            this.lblDevoluciones.Size = new System.Drawing.Size(158, 23);
            this.lblDevoluciones.TabIndex = 113;
            this.lblDevoluciones.Text = "🔄   Devoluciones";
            this.lblDevoluciones.Click += new System.EventHandler(this.label22_Click);
            // 
            // lblVenta
            // 
            this.lblVenta.AutoSize = true;
            this.lblVenta.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblVenta.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold);
            this.lblVenta.ForeColor = System.Drawing.Color.Black;
            this.lblVenta.Location = new System.Drawing.Point(21, 548);
            this.lblVenta.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lblVenta.Name = "lblVenta";
            this.lblVenta.Size = new System.Drawing.Size(104, 23);
            this.lblVenta.TabIndex = 91;
            this.lblVenta.Text = "🛒   Ventas";
            this.lblVenta.Click += new System.EventHandler(this.label7_Click);
            // 
            // lblCompras
            // 
            this.lblCompras.AutoSize = true;
            this.lblCompras.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblCompras.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold);
            this.lblCompras.ForeColor = System.Drawing.Color.Black;
            this.lblCompras.Location = new System.Drawing.Point(21, 488);
            this.lblCompras.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lblCompras.Name = "lblCompras";
            this.lblCompras.Size = new System.Drawing.Size(122, 23);
            this.lblCompras.TabIndex = 90;
            this.lblCompras.Text = "💳   Compras";
            this.lblCompras.Click += new System.EventHandler(this.label17_Click_1);
            // 
            // lblUsuarios
            // 
            this.lblUsuarios.AutoSize = true;
            this.lblUsuarios.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblUsuarios.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold);
            this.lblUsuarios.ForeColor = System.Drawing.Color.Black;
            this.lblUsuarios.Location = new System.Drawing.Point(23, 272);
            this.lblUsuarios.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lblUsuarios.Name = "lblUsuarios";
            this.lblUsuarios.Size = new System.Drawing.Size(120, 23);
            this.lblUsuarios.TabIndex = 89;
            this.lblUsuarios.Text = "👥   Usuarios";
            this.lblUsuarios.Click += new System.EventHandler(this.label16_Click);
            // 
            // lblCliente
            // 
            this.lblCliente.AutoSize = true;
            this.lblCliente.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblCliente.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold);
            this.lblCliente.ForeColor = System.Drawing.Color.Black;
            this.lblCliente.Location = new System.Drawing.Point(21, 323);
            this.lblCliente.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lblCliente.Name = "lblCliente";
            this.lblCliente.Size = new System.Drawing.Size(117, 23);
            this.lblCliente.TabIndex = 88;
            this.lblCliente.Text = "🧑‍🤝‍🧑   Clientes";
            this.lblCliente.Click += new System.EventHandler(this.label15_Click_1);
            // 
            // lblProductos
            // 
            this.lblProductos.AutoSize = true;
            this.lblProductos.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblProductos.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold);
            this.lblProductos.ForeColor = System.Drawing.Color.Black;
            this.lblProductos.Location = new System.Drawing.Point(20, 382);
            this.lblProductos.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lblProductos.Name = "lblProductos";
            this.lblProductos.Size = new System.Drawing.Size(131, 23);
            this.lblProductos.TabIndex = 85;
            this.lblProductos.Text = "🛍️   Productos";
            this.lblProductos.Click += new System.EventHandler(this.label13_Click);
            // 
            // pictureBox3
            // 
            this.pictureBox3.BackColor = System.Drawing.Color.White;
            this.pictureBox3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pictureBox3.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox3.Image")));
            this.pictureBox3.Location = new System.Drawing.Point(24, 41);
            this.pictureBox3.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(157, 117);
            this.pictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox3.TabIndex = 86;
            this.pictureBox3.TabStop = false;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.White;
            this.panel2.Controls.Add(this.label18);
            this.panel2.Controls.Add(this.pictureBox4);
            this.panel2.Controls.Add(this.label1);
            this.panel2.ForeColor = System.Drawing.Color.White;
            this.panel2.Location = new System.Drawing.Point(-5, -12);
            this.panel2.Margin = new System.Windows.Forms.Padding(4);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1354, 86);
            this.panel2.TabIndex = 60;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label18.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(106)))), ((int)(((byte)(0)))));
            this.label18.Location = new System.Drawing.Point(904, 52);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(114, 23);
            this.label18.TabIndex = 11;
            this.label18.Text = "Acerca De...";
            // 
            // pictureBox4
            // 
            this.pictureBox4.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox4.Image")));
            this.pictureBox4.Location = new System.Drawing.Point(1024, 18);
            this.pictureBox4.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pictureBox4.Name = "pictureBox4";
            this.pictureBox4.Size = new System.Drawing.Size(79, 57);
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
            this.label1.Location = new System.Drawing.Point(25, 34);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(157, 45);
            this.label1.TabIndex = 0;
            this.label1.Text = "Clientes";
            // 
            // btnbuscar
            // 
            this.btnbuscar.BackColor = System.Drawing.Color.Black;
            this.btnbuscar.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnbuscar.ForeColor = System.Drawing.Color.White;
            this.btnbuscar.Location = new System.Drawing.Point(755, 110);
            this.btnbuscar.Margin = new System.Windows.Forms.Padding(4);
            this.btnbuscar.Name = "btnbuscar";
            this.btnbuscar.Size = new System.Drawing.Size(120, 49);
            this.btnbuscar.TabIndex = 89;
            this.btnbuscar.Text = "Buscar";
            this.btnbuscar.UseVisualStyleBackColor = false;
            this.btnbuscar.Click += new System.EventHandler(this.btnbuscar_Click);
            // 
            // btnAgregar
            // 
            this.btnAgregar.BackColor = System.Drawing.Color.Black;
            this.btnAgregar.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAgregar.ForeColor = System.Drawing.Color.White;
            this.btnAgregar.Location = new System.Drawing.Point(959, 110);
            this.btnAgregar.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnAgregar.Name = "btnAgregar";
            this.btnAgregar.Size = new System.Drawing.Size(99, 49);
            this.btnAgregar.TabIndex = 90;
            this.btnAgregar.Text = "Agregar";
            this.btnAgregar.UseVisualStyleBackColor = false;
            this.btnAgregar.Click += new System.EventHandler(this.btnAgregar_Click_3);
            // 
            // btnEditar
            // 
            this.btnEditar.BackColor = System.Drawing.Color.Black;
            this.btnEditar.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEditar.ForeColor = System.Drawing.Color.White;
            this.btnEditar.Location = new System.Drawing.Point(1081, 110);
            this.btnEditar.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnEditar.Name = "btnEditar";
            this.btnEditar.Size = new System.Drawing.Size(99, 49);
            this.btnEditar.TabIndex = 91;
            this.btnEditar.Text = " Editar";
            this.btnEditar.UseVisualStyleBackColor = false;
            this.btnEditar.Click += new System.EventHandler(this.btnEditar_Click);
            // 
            // btnEliminar
            // 
            this.btnEliminar.BackColor = System.Drawing.Color.Black;
            this.btnEliminar.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEliminar.ForeColor = System.Drawing.Color.White;
            this.btnEliminar.Location = new System.Drawing.Point(1036, 569);
            this.btnEliminar.Name = "btnEliminar";
            this.btnEliminar.Size = new System.Drawing.Size(94, 50);
            this.btnEliminar.TabIndex = 92;
            this.btnEliminar.Text = "Eliminar";
            this.btnEliminar.UseVisualStyleBackColor = false;
            this.btnEliminar.Click += new System.EventHandler(this.btnEliminar_Click);
            // 
            // txtBuscar
            // 
            this.txtBuscar.Location = new System.Drawing.Point(483, 121);
            this.txtBuscar.Name = "txtBuscar";
            this.txtBuscar.Size = new System.Drawing.Size(203, 22);
            this.txtBuscar.TabIndex = 93;
            // 
            // Cliente
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1221, 904);
            this.Controls.Add(this.txtBuscar);
            this.Controls.Add(this.btnEliminar);
            this.Controls.Add(this.btnEditar);
            this.Controls.Add(this.btnAgregar);
            this.Controls.Add(this.btnbuscar);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.CmbBusqueda);
            this.Controls.Add(this.DGVtabla1);
            this.Controls.Add(this.LblBusqueda);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Cliente";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Cliente";
            this.Load += new System.EventHandler(this.Cliente_Load);
            ((System.ComponentModel.ISupportInitialize)(this.DGVtabla1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox CmbBusqueda;
        private System.Windows.Forms.DataGridView DGVtabla1;
        private System.Windows.Forms.Label LblBusqueda;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.PictureBox pictureBox4;
        private System.Windows.Forms.Label lblProductos;
        private System.Windows.Forms.Label lblCliente;
        private System.Windows.Forms.Label lblUsuarios;
        private System.Windows.Forms.Label lblCompras;
        private System.Windows.Forms.Label lblVenta;
        private System.Windows.Forms.Label lblDevoluciones;
        private System.Windows.Forms.Label lblCredito;
        private System.Windows.Forms.Label lblCaja;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.Label lblProveedores;
        private System.Windows.Forms.Label lblInventario;
        private System.Windows.Forms.Label lblMantenimiento;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Button btnbuscar;
        private System.Windows.Forms.Button btnAgregar;
        private System.Windows.Forms.Button btnEditar;
        private System.Windows.Forms.Button btnEliminar;
        private System.Windows.Forms.TextBox txtBuscar;
    }
}