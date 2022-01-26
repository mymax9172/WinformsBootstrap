using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace MyMax.WinformsBootstrap.Controls
{
    [Designer("System.Windows.Forms.Design.ParentControlDesigner, System.Design", typeof(IDesigner))]
    public partial class BootstrapControl : UserControl
    {
        /// <summary>
        /// This region takes care of the rounded border
        /// </summary>
        #region Border Management

        // Internal memmbers
        private int borderSize;                     // Size of the border
        private int cornerRadius;                   // Radius of the corner

        [Category("Bootstrap")]
        [Description("Radius for rounded corners, squared if less or equale than 2")]
        public int CornerRadius
        {
            get => cornerRadius;
            set
            {
                if (value > 2)
                    if (value > borderSize) cornerRadius = value; else return;
                else
                    cornerRadius = value;
                Invalidate();
            }
        }

        [Category("Bootstrap")]
        [Description("True if the top-left corner is rounded")]
        public bool TopLeftCorner { get; set; } = true;

        [Category("Bootstrap")]
        [Description("True if the top-right corner is rounded")]
        public bool TopRightCorner { get; set; } = true;

        [Category("Bootstrap")]
        [Description("True if the bottom-right corner is rounded")]
        public bool BottomRightCorner { get; set; } = true;

        [Category("Bootstrap")]
        [Description("True if the bottom-left corner is rounded")]
        public bool BottomLeftCorner { get; set; } = true;

        [Category("Bootstrap")]
        [Description("Size of the border")]
        public int BorderSize 
        { 
            get => borderSize; 
            set
            {
                if (cornerRadius > 2)
                    if (value < cornerRadius) borderSize = value; else return;
                else
                    borderSize = value;
                Invalidate();
            }
        }

        [Category("Bootstrap")]
        [Description("Color of the border")]
        public Color BorderColor { get; set; }

        /// <summary>
        /// Create a graphic path for rounded corners
        /// </summary>
        /// <param name="rect">Size and location of the rectangle</param>
        /// <param name="radius">Corner radius</param>
        /// <returns></returns>
        private GraphicsPath GetFigurePath(Rectangle rect, int radius)
        {
            GraphicsPath path = new GraphicsPath();

            float curveSize = radius * 2F;

            path.StartFigure();

            if (!TopLeftCorner)
                path.AddLine(new PointF(rect.X, rect.Y), new PointF(rect.X + curveSize, rect.Y));
            else
                path.AddArc(rect.X, rect.Y, curveSize, curveSize, 180, 90);

            if (!TopRightCorner)
                path.AddLine(new PointF(rect.Right - curveSize, rect.Y), new PointF(rect.Right, rect.Y));
            else
                path.AddArc(rect.Right - curveSize, rect.Y, curveSize, curveSize, 270, 90);

            if (!BottomRightCorner)
                path.AddLine(new PointF(rect.Right, rect.Bottom - curveSize), new PointF(rect.Right, rect.Bottom));
            else
                path.AddArc(rect.Right - curveSize, rect.Bottom - curveSize, curveSize, curveSize, 0, 90);

            if (!BottomLeftCorner)
                path.AddLine(new PointF(rect.X + curveSize, rect.Bottom), new PointF(rect.X, rect.Bottom));
            else
                path.AddArc(rect.X, rect.Bottom - curveSize, curveSize, curveSize, 90, 90);
            path.CloseFigure();

            return path;
        }

        /// <summary>
        /// Draw the border
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="pevent"></param>
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            Rectangle rectSurface = ClientRectangle;
            Rectangle rectBorder = Rectangle.Inflate(rectSurface, -BorderSize, -BorderSize);

            int smoothSize = 2;
            if (BorderSize > 0) smoothSize = BorderSize;

            if (CornerRadius > 2) //Rounded border
            {
                using (GraphicsPath pathSurface = GetFigurePath(rectSurface, CornerRadius))
                using (GraphicsPath pathBorder = GetFigurePath(rectBorder, CornerRadius - BorderSize))
                using (Pen penSurface = new Pen(Parent.BackColor, smoothSize))
                using (Pen penBorder = new Pen(BorderColor, BorderSize))
                {
                    e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                    // Panel surface
                    Region = new Region(pathSurface);
                    //Draw surface border for HD result
                    e.Graphics.DrawPath(penSurface, pathSurface);

                    // Panel border                    
                    if (BorderSize >= 1)
                        //Draw control border
                        e.Graphics.DrawPath(penBorder, pathBorder);
                }
            }
            else //Squared border
            {
                e.Graphics.SmoothingMode = SmoothingMode.None;
                // Panel surface
                Region = new Region(rectSurface);
                // Panel border
                if (BorderSize >= 1)
                {
                    using (Pen penBorder = new Pen(BorderColor, BorderSize))
                    {
                        penBorder.Alignment = PenAlignment.Inset;
                        e.Graphics.DrawRectangle(penBorder, 0, 0, Width - 1, Height - 1);
                    }
                }
            }
        }

        /// <summary>
        /// Track if parent's backcolor changes
        /// </summary>
        /// <param name="e"></param>
        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            Parent.BackColorChanged += (object o, EventArgs ea) => { Invalidate(); };
        }

        /// <summary>
        /// Control if the bordesize is greater than actual height
        /// </summary>
        /// <param name="e"></param>
        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            if (BorderSize > Height) BorderSize = Height;
            Invalidate();
        }

        #endregion

        public BootstrapControl()
        {
            InitializeComponent();
            CornerRadius = 5;
            DoubleBuffered = true;
            BorderSize = 1;
            BorderColor = Color.Gainsboro;
            Font = new Font("Segoe UI", 8, FontStyle.Regular);
        }

        /// <summary>
        /// Disable the focus cue
        /// </summary>
        protected override bool ShowFocusCues { get => false; }
       
    }
}
