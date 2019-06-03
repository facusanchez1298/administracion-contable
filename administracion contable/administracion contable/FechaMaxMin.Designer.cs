using System;

namespace administracion_contable
{
    partial class FechaMaxMin
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.dateMin = new System.Windows.Forms.DateTimePicker();
            this.dateMax = new System.Windows.Forms.DateTimePicker();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 95);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(76, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Fecha Maxima";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 38);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(73, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Fecha Minima";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(39, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Rango";
            // 
            // dateMin
            // 
            this.dateMin.Location = new System.Drawing.Point(12, 54);
            this.dateMin.Name = "dateMin";
            this.dateMin.Size = new System.Drawing.Size(200, 20);
            this.dateMin.TabIndex = 1;
            this.dateMin.Value = new System.DateTime(2019, 1, 1, 0, 0, 0, 0);
            // 
            // dateMax
            // 
            this.dateMax.Location = new System.Drawing.Point(12, 111);
            this.dateMax.Name = "dateMax";
            this.dateMax.Size = new System.Drawing.Size(200, 20);
            this.dateMax.TabIndex = 1;
            this.dateMax.Value = new System.DateTime(2019, 12, 12, 0, 0, 0, 0);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(137, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "Aceptar";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // FechaMaxMin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.PowderBlue;
            this.ClientSize = new System.Drawing.Size(224, 158);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.dateMax);
            this.Controls.Add(this.dateMin);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "FechaMaxMin";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Fecha Maxima";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker dateMin;
        private System.Windows.Forms.DateTimePicker dateMax;
        private System.Windows.Forms.Button button1;
    }
}