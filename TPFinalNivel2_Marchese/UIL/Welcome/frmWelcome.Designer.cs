namespace TPFinalNivel2_Marchese.UIL.Welcome
{
    partial class frmWelcome
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
            this.btnConsultaArt = new System.Windows.Forms.Button();
            this.btnAltaArt = new System.Windows.Forms.Button();
            this.lblFecha = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 22F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(267, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(223, 42);
            this.label1.TabIndex = 0;
            this.label1.Text = "Bienvenidos";
            // 
            // btnConsultaArt
            // 
            this.btnConsultaArt.Font = new System.Drawing.Font("Microsoft Sans Serif", 22F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnConsultaArt.Location = new System.Drawing.Point(120, 160);
            this.btnConsultaArt.Name = "btnConsultaArt";
            this.btnConsultaArt.Size = new System.Drawing.Size(201, 148);
            this.btnConsultaArt.TabIndex = 1;
            this.btnConsultaArt.Text = "Consulta articulo";
            this.btnConsultaArt.UseVisualStyleBackColor = true;
            this.btnConsultaArt.Click += new System.EventHandler(this.btnConsultaArt_Click);
            // 
            // btnAltaArt
            // 
            this.btnAltaArt.Font = new System.Drawing.Font("Microsoft Sans Serif", 22F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAltaArt.Location = new System.Drawing.Point(443, 160);
            this.btnAltaArt.Name = "btnAltaArt";
            this.btnAltaArt.Size = new System.Drawing.Size(206, 148);
            this.btnAltaArt.TabIndex = 2;
            this.btnAltaArt.Text = "Alta articulo";
            this.btnAltaArt.UseVisualStyleBackColor = true;
            this.btnAltaArt.Click += new System.EventHandler(this.btnAltaArt_Click);
            // 
            // lblFecha
            // 
            this.lblFecha.AutoSize = true;
            this.lblFecha.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblFecha.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFecha.Location = new System.Drawing.Point(0, 392);
            this.lblFecha.Name = "lblFecha";
            this.lblFecha.Size = new System.Drawing.Size(0, 29);
            this.lblFecha.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(136, 93);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(495, 31);
            this.label2.TabIndex = 4;
            this.label2.Text = "TP Final curso de programacion Nivel 2 ";
            // 
            // frmWelcome
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(771, 421);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lblFecha);
            this.Controls.Add(this.btnAltaArt);
            this.Controls.Add(this.btnConsultaArt);
            this.Controls.Add(this.label1);
            this.Name = "frmWelcome";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmWelcome";
            this.Load += new System.EventHandler(this.frmWelcome_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnConsultaArt;
        private System.Windows.Forms.Button btnAltaArt;
        private System.Windows.Forms.Label lblFecha;
        private System.Windows.Forms.Label label2;
    }
}