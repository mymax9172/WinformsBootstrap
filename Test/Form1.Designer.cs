
namespace Test
{
    partial class Form1
    {
        /// <summary>
        /// Variabile di progettazione necessaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Pulire le risorse in uso.
        /// </summary>
        /// <param name="disposing">ha valore true se le risorse gestite devono essere eliminate, false in caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Codice generato da Progettazione Windows Form

        /// <summary>
        /// Metodo necessario per il supporto della finestra di progettazione. Non modificare
        /// il contenuto del metodo con l'editor di codice.
        /// </summary>
        private void InitializeComponent()
        {
            this.containerControl1 = new MyMax.WinformsBootstrap.Controls.ContainerControl();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.containerControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // containerControl1
            // 
            this.containerControl1.BackColor = System.Drawing.Color.White;
            this.containerControl1.BorderColor = System.Drawing.Color.Black;
            this.containerControl1.BorderRadius = 10;
            this.containerControl1.BorderSize = 0;
            this.containerControl1.ContentResize = false;
            this.containerControl1.ControlAlignment = MyMax.WinformsBootstrap.Controls.ContainerControl.ControlAlignments.RightOrBottom;
            this.containerControl1.ControlOrder = MyMax.WinformsBootstrap.Controls.ContainerControl.ControlOrders.Design;
            this.containerControl1.Controls.Add(this.button2);
            this.containerControl1.Controls.Add(this.button1);
            this.containerControl1.DockedControls = true;
            this.containerControl1.FlowDirection = MyMax.WinformsBootstrap.Controls.ContainerControl.FlowDirections.Horizontal;
            this.containerControl1.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.containerControl1.ForeColor = System.Drawing.Color.Black;
            this.containerControl1.Location = new System.Drawing.Point(144, 108);
            this.containerControl1.Name = "containerControl1";
            this.containerControl1.Padding = new System.Windows.Forms.Padding(3);
            this.containerControl1.Size = new System.Drawing.Size(294, 159);
            this.containerControl1.TabIndex = 0;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(87, 70);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(161, 80);
            this.button2.TabIndex = 1;
            this.button2.Text = "button2";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(6, 105);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 45);
            this.button1.TabIndex = 0;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.containerControl1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.containerControl1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private MyMax.WinformsBootstrap.Controls.ContainerControl containerControl1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
    }
}

