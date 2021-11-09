
namespace Sample
{
    partial class MainForm
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
            this.buttonPanes = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // buttonPanes
            // 
            this.buttonPanes.Location = new System.Drawing.Point(40, 106);
            this.buttonPanes.Name = "buttonPanes";
            this.buttonPanes.Size = new System.Drawing.Size(93, 30);
            this.buttonPanes.TabIndex = 0;
            this.buttonPanes.Text = "Panes";
            this.buttonPanes.UseVisualStyleBackColor = true;
            this.buttonPanes.Click += new System.EventHandler(this.buttonPanes_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.buttonPanes);
            this.Name = "MainForm";
            this.Text = "MainForm";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonPanes;
    }
}

