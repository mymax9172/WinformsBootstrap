
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
            GUILibrary.Controls.BorderStyle borderStyle1 = new GUILibrary.Controls.BorderStyle();
            GUILibrary.Controls.ContainerStyle containerStyle1 = new GUILibrary.Controls.ContainerStyle();
            this.pane1 = new GUILibrary.Controls.Pane();
            this.SuspendLayout();
            // 
            // pane1
            // 
            borderStyle1.BottomLeftCorner = true;
            borderStyle1.BottomRightCorner = true;
            borderStyle1.Color = System.Drawing.Color.Gainsboro;
            borderStyle1.CornerRadius = 5F;
            borderStyle1.Thinkness = 1;
            borderStyle1.TopLeftCorner = true;
            borderStyle1.TopRightCorner = true;
            this.pane1.BorderStyle = borderStyle1;
            containerStyle1.ContentResize = true;
            containerStyle1.ControlAlignment = GUILibrary.Controls.ContainerStyle.ControlAlignments.LeftorTop;
            containerStyle1.ControlOrder = GUILibrary.Controls.ContainerStyle.ControlOrders.Design;
            containerStyle1.DockedControls = true;
            containerStyle1.FlowDirection = GUILibrary.Controls.ContainerStyle.FlowDirections.Vertical;
            this.pane1.ContainerStyle = containerStyle1;
            this.pane1.Location = new System.Drawing.Point(348, 138);
            this.pane1.Name = "pane1";
            this.pane1.Size = new System.Drawing.Size(153, 142);
            this.pane1.TabIndex = 0;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1078, 617);
            this.Controls.Add(this.pane1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private GUILibrary.Controls.Pane pane1;
    }
}

