﻿namespace Compilador
{
    partial class frmIDE
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
            this.dgvTablaErrores = new System.Windows.Forms.DataGridView();
            this.CADENA = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ERROR = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txtTokens = new System.Windows.Forms.RichTextBox();
            this.lblCodigo = new System.Windows.Forms.Label();
            this.lblTokens = new System.Windows.Forms.Label();
            this.lblTablaErrores = new System.Windows.Forms.Label();
            this.lblTablaSimbolos = new System.Windows.Forms.Label();
            this.dgvTablaSimbolos = new System.Windows.Forms.DataGridView();
            this.txtCodigo = new System.Windows.Forms.RichTextBox();
            this.btnComprobarErrores = new System.Windows.Forms.Button();
            this.btnGuardarArchivo = new System.Windows.Forms.Button();
            this.btnCargarArchivo = new System.Windows.Forms.Button();
            this.btnGuardarArchivoTokens = new System.Windows.Forms.Button();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTablaErrores)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTablaSimbolos)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvTablaErrores
            // 
            this.dgvTablaErrores.AllowUserToAddRows = false;
            this.dgvTablaErrores.AllowUserToDeleteRows = false;
            this.dgvTablaErrores.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvTablaErrores.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTablaErrores.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.CADENA,
            this.ERROR});
            this.dgvTablaErrores.Location = new System.Drawing.Point(26, 622);
            this.dgvTablaErrores.Name = "dgvTablaErrores";
            this.dgvTablaErrores.ReadOnly = true;
            this.dgvTablaErrores.RowHeadersVisible = false;
            this.dgvTablaErrores.RowHeadersWidth = 82;
            this.dgvTablaErrores.RowTemplate.Height = 33;
            this.dgvTablaErrores.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvTablaErrores.Size = new System.Drawing.Size(686, 463);
            this.dgvTablaErrores.TabIndex = 2;
            // 
            // CADENA
            // 
            this.CADENA.HeaderText = "CADENA";
            this.CADENA.MinimumWidth = 10;
            this.CADENA.Name = "CADENA";
            this.CADENA.ReadOnly = true;
            // 
            // ERROR
            // 
            this.ERROR.HeaderText = "ERROR";
            this.ERROR.MinimumWidth = 10;
            this.ERROR.Name = "ERROR";
            this.ERROR.ReadOnly = true;
            // 
            // txtTokens
            // 
            this.txtTokens.Location = new System.Drawing.Point(754, 47);
            this.txtTokens.Name = "txtTokens";
            this.txtTokens.Size = new System.Drawing.Size(686, 452);
            this.txtTokens.TabIndex = 4;
            this.txtTokens.Text = "";
            // 
            // lblCodigo
            // 
            this.lblCodigo.AutoSize = true;
            this.lblCodigo.Location = new System.Drawing.Point(28, 11);
            this.lblCodigo.Name = "lblCodigo";
            this.lblCodigo.Size = new System.Drawing.Size(159, 25);
            this.lblCodigo.TabIndex = 6;
            this.lblCodigo.Text = "Ingrese código.";
            // 
            // lblTokens
            // 
            this.lblTokens.AutoSize = true;
            this.lblTokens.Location = new System.Drawing.Point(749, 11);
            this.lblTokens.Name = "lblTokens";
            this.lblTokens.Size = new System.Drawing.Size(191, 25);
            this.lblTokens.TabIndex = 7;
            this.lblTokens.Text = "Archivo de Tokens";
            // 
            // lblTablaErrores
            // 
            this.lblTablaErrores.AutoSize = true;
            this.lblTablaErrores.Location = new System.Drawing.Point(28, 587);
            this.lblTablaErrores.Name = "lblTablaErrores";
            this.lblTablaErrores.Size = new System.Drawing.Size(176, 25);
            this.lblTablaErrores.TabIndex = 8;
            this.lblTablaErrores.Text = "Tabla de errores.";
            // 
            // lblTablaSimbolos
            // 
            this.lblTablaSimbolos.AutoSize = true;
            this.lblTablaSimbolos.Location = new System.Drawing.Point(753, 587);
            this.lblTablaSimbolos.Name = "lblTablaSimbolos";
            this.lblTablaSimbolos.Size = new System.Drawing.Size(193, 25);
            this.lblTablaSimbolos.TabIndex = 9;
            this.lblTablaSimbolos.Text = "Tabla de simbolos.";
            // 
            // dgvTablaSimbolos
            // 
            this.dgvTablaSimbolos.AllowUserToAddRows = false;
            this.dgvTablaSimbolos.AllowUserToDeleteRows = false;
            this.dgvTablaSimbolos.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvTablaSimbolos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTablaSimbolos.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn2});
            this.dgvTablaSimbolos.Location = new System.Drawing.Point(754, 622);
            this.dgvTablaSimbolos.Name = "dgvTablaSimbolos";
            this.dgvTablaSimbolos.ReadOnly = true;
            this.dgvTablaSimbolos.RowHeadersVisible = false;
            this.dgvTablaSimbolos.RowHeadersWidth = 82;
            this.dgvTablaSimbolos.RowTemplate.Height = 33;
            this.dgvTablaSimbolos.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvTablaSimbolos.Size = new System.Drawing.Size(686, 463);
            this.dgvTablaSimbolos.TabIndex = 10;
            // 
            // txtCodigo
            // 
            this.txtCodigo.Location = new System.Drawing.Point(26, 47);
            this.txtCodigo.Name = "txtCodigo";
            this.txtCodigo.Size = new System.Drawing.Size(686, 452);
            this.txtCodigo.TabIndex = 3;
            this.txtCodigo.Text = "";
            // 
            // btnComprobarErrores
            // 
            this.btnComprobarErrores.Location = new System.Drawing.Point(26, 505);
            this.btnComprobarErrores.Name = "btnComprobarErrores";
            this.btnComprobarErrores.Size = new System.Drawing.Size(204, 47);
            this.btnComprobarErrores.TabIndex = 11;
            this.btnComprobarErrores.Text = "Comprobar errores";
            this.btnComprobarErrores.UseVisualStyleBackColor = true;
            this.btnComprobarErrores.Click += new System.EventHandler(this.btnComprobarErrores_Click);
            // 
            // btnGuardarArchivo
            // 
            this.btnGuardarArchivo.Location = new System.Drawing.Point(269, 505);
            this.btnGuardarArchivo.Name = "btnGuardarArchivo";
            this.btnGuardarArchivo.Size = new System.Drawing.Size(204, 47);
            this.btnGuardarArchivo.TabIndex = 12;
            this.btnGuardarArchivo.Text = "Guardar archivo";
            this.btnGuardarArchivo.UseVisualStyleBackColor = true;
            this.btnGuardarArchivo.Click += new System.EventHandler(this.btnGuardarArchivo_Click);
            // 
            // btnCargarArchivo
            // 
            this.btnCargarArchivo.Location = new System.Drawing.Point(508, 505);
            this.btnCargarArchivo.Name = "btnCargarArchivo";
            this.btnCargarArchivo.Size = new System.Drawing.Size(204, 47);
            this.btnCargarArchivo.TabIndex = 13;
            this.btnCargarArchivo.Text = "Cargar archivo";
            this.btnCargarArchivo.UseVisualStyleBackColor = true;
            this.btnCargarArchivo.Click += new System.EventHandler(this.btnCargarArchivo_Click);
            // 
            // btnGuardarArchivoTokens
            // 
            this.btnGuardarArchivoTokens.Location = new System.Drawing.Point(754, 505);
            this.btnGuardarArchivoTokens.Name = "btnGuardarArchivoTokens";
            this.btnGuardarArchivoTokens.Size = new System.Drawing.Size(686, 47);
            this.btnGuardarArchivoTokens.TabIndex = 14;
            this.btnGuardarArchivoTokens.Text = "Guardar archivo";
            this.btnGuardarArchivoTokens.UseVisualStyleBackColor = true;
            this.btnGuardarArchivoTokens.Click += new System.EventHandler(this.btnGuardarArchivoTokens_Click);
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.HeaderText = "TOKEN";
            this.dataGridViewTextBoxColumn1.MinimumWidth = 10;
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.HeaderText = "IDENTIFICADOR";
            this.dataGridViewTextBoxColumn2.MinimumWidth = 10;
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            // 
            // frmIDE
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1473, 1107);
            this.Controls.Add(this.btnGuardarArchivoTokens);
            this.Controls.Add(this.btnCargarArchivo);
            this.Controls.Add(this.btnGuardarArchivo);
            this.Controls.Add(this.btnComprobarErrores);
            this.Controls.Add(this.dgvTablaSimbolos);
            this.Controls.Add(this.lblTablaSimbolos);
            this.Controls.Add(this.lblTablaErrores);
            this.Controls.Add(this.lblTokens);
            this.Controls.Add(this.lblCodigo);
            this.Controls.Add(this.txtTokens);
            this.Controls.Add(this.txtCodigo);
            this.Controls.Add(this.dgvTablaErrores);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MinimumSize = new System.Drawing.Size(1499, 1178);
            this.Name = "frmIDE";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ChanzaScript IDE";
            ((System.ComponentModel.ISupportInitialize)(this.dgvTablaErrores)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTablaSimbolos)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.DataGridView dgvTablaErrores;
        private System.Windows.Forms.RichTextBox txtTokens;
        private System.Windows.Forms.Label lblCodigo;
        private System.Windows.Forms.Label lblTokens;
        private System.Windows.Forms.Label lblTablaErrores;
        private System.Windows.Forms.Label lblTablaSimbolos;
        private System.Windows.Forms.DataGridView dgvTablaSimbolos;
        private System.Windows.Forms.RichTextBox txtCodigo;
        private System.Windows.Forms.Button btnComprobarErrores;
        private System.Windows.Forms.DataGridViewTextBoxColumn CADENA;
        private System.Windows.Forms.DataGridViewTextBoxColumn ERROR;
        private System.Windows.Forms.Button btnGuardarArchivo;
        private System.Windows.Forms.Button btnCargarArchivo;
        private System.Windows.Forms.Button btnGuardarArchivoTokens;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
    }
}

