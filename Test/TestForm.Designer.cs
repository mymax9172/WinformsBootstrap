
namespace Test
{
    partial class TestForm
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
            this.button3 = new System.Windows.Forms.Button();
            this.button6 = new MyMax.WinformsBootstrap.Controls.Button();
            this.button5 = new MyMax.WinformsBootstrap.Controls.Button();
            this.SuspendLayout();
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(102, 123);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 0;
            this.button3.Text = "button3";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button6
            // 
            this.button6.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.button6.BackColor = System.Drawing.Color.Transparent;
            this.button6.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(14)))), ((int)(((byte)(109)))), ((int)(((byte)(253)))));
            this.button6.BorderSize = 1;
            this.button6.BottomLeftCorner = true;
            this.button6.BottomRightCorner = true;
            this.button6.CornerRadius = 5;
            this.button6.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button6.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.button6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(14)))), ((int)(((byte)(109)))), ((int)(((byte)(253)))));
            this.button6.IsPushed = false;
            this.button6.IsToggle = true;
            this.button6.Location = new System.Drawing.Point(487, 95);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(83, 39);
            this.button6.SizeStyle = MyMax.WinformsBootstrap.Controls.Button.SizeStyles.Regular;
            this.button6.Style = MyMax.WinformsBootstrap.Controls.Button.Styles.Outlined_Primary;
            this.button6.TabIndex = 2;
            this.button6.Text = "button6";
            this.button6.TopLeftCorner = true;
            this.button6.TopRightCorner = true;
            // 
            // button5
            // 
            this.button5.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.button5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(14)))), ((int)(((byte)(109)))), ((int)(((byte)(253)))));
            this.button5.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(14)))), ((int)(((byte)(109)))), ((int)(((byte)(253)))));
            this.button5.BorderSize = 1;
            this.button5.BottomLeftCorner = true;
            this.button5.BottomRightCorner = true;
            this.button5.CornerRadius = 5;
            this.button5.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button5.Enabled = false;
            this.button5.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.button5.ForeColor = System.Drawing.Color.White;
            this.button5.IsPushed = false;
            this.button5.IsToggle = true;
            this.button5.Location = new System.Drawing.Point(385, 95);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(83, 39);
            this.button5.SizeStyle = MyMax.WinformsBootstrap.Controls.Button.SizeStyles.Regular;
            this.button5.Style = MyMax.WinformsBootstrap.Controls.Button.Styles.Primary;
            this.button5.TabIndex = 1;
            this.button5.Text = "button5";
            this.button5.TopLeftCorner = true;
            this.button5.TopRightCorner = true;
            // 
            // TestForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.button6);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.button3);
            this.Name = "TestForm";
            this.Text = "TestForm";
            this.ResumeLayout(false);

        }

        #endregion

        private MyMax.WinformsBootstrap.Controls.Button button1;
        private MyMax.WinformsBootstrap.Controls.Button button2;
        private MyMax.WinformsBootstrap.Controls.Button button4;
        private System.Windows.Forms.Button button3;
        private MyMax.WinformsBootstrap.Controls.Button button5;
        private MyMax.WinformsBootstrap.Controls.Button button6;
    }
}