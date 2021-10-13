
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
            this.buttonControl1 = new MyMax.WinformsBootstrap.Controls.ButtonControl();
            this.buttonControl2 = new MyMax.WinformsBootstrap.Controls.ButtonControl();
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
            this.containerControl1.Controls.Add(this.buttonControl2);
            this.containerControl1.Controls.Add(this.buttonControl1);
            this.containerControl1.DockedControls = true;
            this.containerControl1.FlowDirection = MyMax.WinformsBootstrap.Controls.ContainerControl.FlowDirections.Horizontal;
            this.containerControl1.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.containerControl1.ForeColor = System.Drawing.Color.Black;
            this.containerControl1.Location = new System.Drawing.Point(53, 41);
            this.containerControl1.Name = "containerControl1";
            this.containerControl1.Padding = new System.Windows.Forms.Padding(3);
            this.containerControl1.Size = new System.Drawing.Size(340, 159);
            this.containerControl1.TabIndex = 0;
            // 
            // buttonControl1
            // 
            this.buttonControl1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(14)))), ((int)(((byte)(109)))), ((int)(((byte)(253)))));
            this.buttonControl1.BorderColor = System.Drawing.Color.Black;
            this.buttonControl1.BorderRadius = 5;
            this.buttonControl1.BorderSize = 0;
            this.buttonControl1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonControl1.FlatAppearance.BorderSize = 0;
            this.buttonControl1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonControl1.ForeColor = System.Drawing.Color.White;
            this.buttonControl1.IconColor = System.Drawing.Color.Black;
            this.buttonControl1.IconName = FontAwesome.Sharp.IconChar.None;
            this.buttonControl1.IconSize = 16;
            this.buttonControl1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonControl1.Location = new System.Drawing.Point(6, 82);
            this.buttonControl1.Name = "buttonControl1";
            this.buttonControl1.Size = new System.Drawing.Size(150, 68);
            this.buttonControl1.Style = MyMax.WinformsBootstrap.Controls.Styles.Primary;
            this.buttonControl1.TabIndex = 0;
            this.buttonControl1.Text = "buttonControl1";
            this.buttonControl1.UseVisualStyleBackColor = false;
            // 
            // buttonControl2
            // 
            this.buttonControl2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(14)))), ((int)(((byte)(109)))), ((int)(((byte)(253)))));
            this.buttonControl2.BorderColor = System.Drawing.Color.Black;
            this.buttonControl2.BorderRadius = 5;
            this.buttonControl2.BorderSize = 0;
            this.buttonControl2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonControl2.FlatAppearance.BorderSize = 0;
            this.buttonControl2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonControl2.ForeColor = System.Drawing.Color.White;
            this.buttonControl2.IconColor = System.Drawing.Color.Black;
            this.buttonControl2.IconName = FontAwesome.Sharp.IconChar.None;
            this.buttonControl2.IconSize = 16;
            this.buttonControl2.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonControl2.Location = new System.Drawing.Point(162, 110);
            this.buttonControl2.Name = "buttonControl2";
            this.buttonControl2.Size = new System.Drawing.Size(101, 40);
            this.buttonControl2.Style = MyMax.WinformsBootstrap.Controls.Styles.Primary;
            this.buttonControl2.TabIndex = 1;
            this.buttonControl2.Text = "buttonControl2";
            this.buttonControl2.UseVisualStyleBackColor = false;
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
        private MyMax.WinformsBootstrap.Controls.ButtonControl buttonControl2;
        private MyMax.WinformsBootstrap.Controls.ButtonControl buttonControl1;
    }
}

