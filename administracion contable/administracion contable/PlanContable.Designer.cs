namespace administracion_contable
{
    partial class PlanContable
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PlanContable));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textNombre = new System.Windows.Forms.TextBox();
            this.textId = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.comboBoxTipo = new System.Windows.Forms.ComboBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.buttonAgregar = new System.Windows.Forms.Button();
            this.textNumero = new System.Windows.Forms.TextBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.archivoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.abrirToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.salirToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.opcionesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.agregarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label4 = new System.Windows.Forms.Label();
            this.textBoxIndex = new System.Windows.Forms.TextBox();
            this.buttonBuscar = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.buttonCancelar = new System.Windows.Forms.Button();
            this.groupBox = new System.Windows.Forms.GroupBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.buttonBorrar = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.groupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(328, 103);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Codigo";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 103);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(105, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Numero Identificador";
            // 
            // textNombre
            // 
            this.textNombre.Location = new System.Drawing.Point(12, 47);
            this.textNombre.Name = "textNombre";
            this.textNombre.Size = new System.Drawing.Size(313, 20);
            this.textNombre.TabIndex = 1;
            this.textNombre.KeyDown += new System.Windows.Forms.KeyEventHandler(this.getNextCtrl);
            // 
            // textId
            // 
            this.textId.BackColor = System.Drawing.SystemColors.Window;
            this.textId.Location = new System.Drawing.Point(12, 119);
            this.textId.Name = "textId";
            this.textId.ReadOnly = true;
            this.textId.Size = new System.Drawing.Size(22, 20);
            this.textId.TabIndex = 1;
            this.textId.TabStop = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(328, 31);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(28, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Tipo";
            // 
            // comboBoxTipo
            // 
            this.comboBoxTipo.FormattingEnabled = true;
            this.comboBoxTipo.Items.AddRange(new object[] {
            "1 Activos",
            "2 Pasivos",
            "3 Patrimonio Neto",
            "4 Resultado Positivo",
            "5 Resultado Negativo",
            "6 Orden",
            "7 Movimiento"});
            this.comboBoxTipo.Location = new System.Drawing.Point(331, 47);
            this.comboBoxTipo.Name = "comboBoxTipo";
            this.comboBoxTipo.Size = new System.Drawing.Size(147, 21);
            this.comboBoxTipo.TabIndex = 2;
            this.comboBoxTipo.SelectedIndexChanged += new System.EventHandler(this.comboBoxTipo_SelectedIndexChanged);
            this.comboBoxTipo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.getNextCtrl);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(12, 145);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.Size = new System.Drawing.Size(556, 333);
            this.dataGridView1.TabIndex = 3;
            this.dataGridView1.TabStop = false;
            // 
            // buttonAgregar
            // 
            this.buttonAgregar.Location = new System.Drawing.Point(492, 116);
            this.buttonAgregar.Name = "buttonAgregar";
            this.buttonAgregar.Size = new System.Drawing.Size(75, 23);
            this.buttonAgregar.TabIndex = 5;
            this.buttonAgregar.Text = "Agregar";
            this.buttonAgregar.UseVisualStyleBackColor = true;
            this.buttonAgregar.Click += new System.EventHandler(this.guardarEnBaseDeDatos);
            this.buttonAgregar.KeyDown += new System.Windows.Forms.KeyEventHandler(this.enterEnGuardar);
            // 
            // textNumero
            // 
            this.textNumero.Location = new System.Drawing.Point(40, 119);
            this.textNumero.Name = "textNumero";
            this.textNumero.Size = new System.Drawing.Size(285, 20);
            this.textNumero.TabIndex = 4;
            this.textNumero.KeyDown += new System.Windows.Forms.KeyEventHandler(this.getNextCtrl);
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.Color.Transparent;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.archivoToolStripMenuItem,
            this.opcionesToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(580, 24);
            this.menuStrip1.TabIndex = 6;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // archivoToolStripMenuItem
            // 
            this.archivoToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.abrirToolStripMenuItem,
            this.toolStripSeparator,
            this.salirToolStripMenuItem});
            this.archivoToolStripMenuItem.Name = "archivoToolStripMenuItem";
            this.archivoToolStripMenuItem.Size = new System.Drawing.Size(60, 20);
            this.archivoToolStripMenuItem.Text = "&Archivo";
            // 
            // abrirToolStripMenuItem
            // 
            this.abrirToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem2,
            this.toolStripMenuItem1});
            this.abrirToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("abrirToolStripMenuItem.Image")));
            this.abrirToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.abrirToolStripMenuItem.Name = "abrirToolStripMenuItem";
            this.abrirToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.abrirToolStripMenuItem.Text = "&Abrir";
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Image = ((System.Drawing.Image)(resources.GetObject("toolStripMenuItem2.Image")));
            this.toolStripMenuItem2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(138, 22);
            this.toolStripMenuItem2.Text = "Libro Diario";
            this.toolStripMenuItem2.Click += new System.EventHandler(this.menu_abirLibroDiario);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripMenuItem1.Image")));
            this.toolStripMenuItem1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(138, 22);
            this.toolStripMenuItem1.Text = "Libro Mayor";
            this.toolStripMenuItem1.Click += new System.EventHandler(this.abrirLibroMayor);
            // 
            // toolStripSeparator
            // 
            this.toolStripSeparator.Name = "toolStripSeparator";
            this.toolStripSeparator.Size = new System.Drawing.Size(97, 6);
            // 
            // salirToolStripMenuItem
            // 
            this.salirToolStripMenuItem.Name = "salirToolStripMenuItem";
            this.salirToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.salirToolStripMenuItem.Text = "&Salir";
            this.salirToolStripMenuItem.Click += new System.EventHandler(this.salirToolStripMenuItem_Click);
            // 
            // opcionesToolStripMenuItem
            // 
            this.opcionesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.editarToolStripMenuItem,
            this.agregarToolStripMenuItem});
            this.opcionesToolStripMenuItem.Name = "opcionesToolStripMenuItem";
            this.opcionesToolStripMenuItem.Size = new System.Drawing.Size(69, 20);
            this.opcionesToolStripMenuItem.Text = "Opciones";
            // 
            // editarToolStripMenuItem
            // 
            this.editarToolStripMenuItem.Name = "editarToolStripMenuItem";
            this.editarToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.E)));
            this.editarToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
            this.editarToolStripMenuItem.Text = "Editar";
            this.editarToolStripMenuItem.Click += new System.EventHandler(this.Modoeditar);
            // 
            // agregarToolStripMenuItem
            // 
            this.agregarToolStripMenuItem.Name = "agregarToolStripMenuItem";
            this.agregarToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.A)));
            this.agregarToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
            this.agregarToolStripMenuItem.Text = "Agregar";
            this.agregarToolStripMenuItem.Click += new System.EventHandler(this.modoAgregar);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(9, 31);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(44, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "Nombre";
            // 
            // textBoxIndex
            // 
            this.textBoxIndex.Enabled = false;
            this.textBoxIndex.Location = new System.Drawing.Point(331, 119);
            this.textBoxIndex.Name = "textBoxIndex";
            this.textBoxIndex.Size = new System.Drawing.Size(75, 20);
            this.textBoxIndex.TabIndex = 7;
            this.textBoxIndex.Tag = "\"busqueda\"";
            // 
            // buttonBuscar
            // 
            this.buttonBuscar.Enabled = false;
            this.buttonBuscar.Location = new System.Drawing.Point(412, 116);
            this.buttonBuscar.Name = "buttonBuscar";
            this.buttonBuscar.Size = new System.Drawing.Size(75, 23);
            this.buttonBuscar.TabIndex = 8;
            this.buttonBuscar.Text = "Buscar";
            this.buttonBuscar.UseVisualStyleBackColor = true;
            this.buttonBuscar.Click += new System.EventHandler(this.buttonBuscar_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(0, 5);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 36);
            this.button2.TabIndex = 9;
            this.button2.Text = "Guardar Cambios";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.buttomGuardar_click);
            // 
            // buttonCancelar
            // 
            this.buttonCancelar.Location = new System.Drawing.Point(0, 42);
            this.buttonCancelar.Name = "buttonCancelar";
            this.buttonCancelar.Size = new System.Drawing.Size(75, 23);
            this.buttonCancelar.TabIndex = 9;
            this.buttonCancelar.Text = "cancelar";
            this.buttonCancelar.UseVisualStyleBackColor = true;
            this.buttonCancelar.Click += new System.EventHandler(this.buttonCancelar_Click_1);
            // 
            // groupBox
            // 
            this.groupBox.Controls.Add(this.button2);
            this.groupBox.Controls.Add(this.buttonCancelar);
            this.groupBox.Location = new System.Drawing.Point(493, 74);
            this.groupBox.Name = "groupBox";
            this.groupBox.Size = new System.Drawing.Size(75, 65);
            this.groupBox.TabIndex = 10;
            this.groupBox.TabStop = false;
            this.groupBox.Visible = false;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(484, 50);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(84, 17);
            this.checkBox1.TabIndex = 3;
            this.checkBox1.Text = "Resgistrable";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // buttonBorrar
            // 
            this.buttonBorrar.Location = new System.Drawing.Point(412, 79);
            this.buttonBorrar.Name = "buttonBorrar";
            this.buttonBorrar.Size = new System.Drawing.Size(75, 36);
            this.buttonBorrar.TabIndex = 11;
            this.buttonBorrar.Text = "Borrar";
            this.buttonBorrar.UseVisualStyleBackColor = true;
            this.buttonBorrar.Visible = false;
            this.buttonBorrar.Click += new System.EventHandler(this.buttonBorrar_Click);
            // 
            // PlanContable
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.PowderBlue;
            this.ClientSize = new System.Drawing.Size(580, 490);
            this.Controls.Add(this.buttonBorrar);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.buttonBuscar);
            this.Controls.Add(this.buttonAgregar);
            this.Controls.Add(this.textBoxIndex);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.comboBoxTipo);
            this.Controls.Add(this.textNumero);
            this.Controls.Add(this.textId);
            this.Controls.Add(this.textNombre);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.groupBox);
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(596, 529);
            this.MinimumSize = new System.Drawing.Size(596, 529);
            this.Name = "PlanContable";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Plan Contable";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.groupBox.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textNombre;
        private System.Windows.Forms.TextBox textId;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox comboBoxTipo;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button buttonAgregar;
        private System.Windows.Forms.TextBox textNumero;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem archivoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem abrirToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.ToolStripMenuItem salirToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem opcionesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editarToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem agregarToolStripMenuItem;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBoxIndex;
        private System.Windows.Forms.Button buttonBuscar;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button buttonCancelar;
        private System.Windows.Forms.GroupBox groupBox;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
        private System.Windows.Forms.Button buttonBorrar;
    }
}