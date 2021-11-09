using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace GUILibrary.Controls
{
    /// <summary>
    /// Interface for managing borders
    /// </summary>
    public interface IBorderStyle
    {
        BorderStyle BorderStyle { get; set; }
    }

    /// <summary>
    /// Border management class
    /// Author: Massimiliano Agostinoni
    /// Version: 1.0
    /// </summary>
    public class BorderStyle
    {
        #region Properties

        [Category("Bootstrap")]
        [Description("Radius for rounded corners")]
        public float CornerRadius { get; set; } = 5;

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
        public int Thinkness { get; set; } = 1;

        [Category("Bootstrap")]
        [Description("Color of the border")]
        public Color Color { get; set; } = Color.Gainsboro;

        #endregion

        #region Graphics methods

        /// <summary>
        /// Create a graphic path for rounded corners
        /// </summary>
        /// <param name="rect">Size and location of the rectangle</param>
        /// <param name="radius">Corner radius</param>
        /// <returns></returns>
        private GraphicsPath GetFigurePath(Rectangle rect, float radius)
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
        public void OnPaint(object sender, PaintEventArgs pevent)
        {
            Control control = sender as Control;
            Rectangle rectSurface = control.ClientRectangle;
            Rectangle rectBorder = Rectangle.Inflate(rectSurface, -Thinkness, -Thinkness);
            int smoothSize = 2;
            if (Thinkness > 0)
                smoothSize = Thinkness;

            if (CornerRadius > 2) //Rounded border
            {
                using (GraphicsPath pathSurface = GetFigurePath(rectSurface, CornerRadius))
                using (GraphicsPath pathBorder = GetFigurePath(rectBorder, CornerRadius - Thinkness))
                using (Pen penSurface = new Pen(control.Parent.BackColor, smoothSize))
                using (Pen penBorder = new Pen(Color, Thinkness))
                {
                    pevent.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                    // Panel surface
                    control.Region = new Region(pathSurface);
                    //Draw surface border for HD result
                    pevent.Graphics.DrawPath(penSurface, pathSurface);

                    // Panel border                    
                    if (Thinkness >= 1)
                        //Draw control border
                        pevent.Graphics.DrawPath(penBorder, pathBorder);
                }
            }
            else //Squared border
            {
                pevent.Graphics.SmoothingMode = SmoothingMode.None;
                // Panel surface
                control.Region = new Region(rectSurface);
                // Panel border
                if (Thinkness >= 1)
                {
                    using (Pen penBorder = new Pen(Color, Thinkness))
                    {
                        penBorder.Alignment = PenAlignment.Inset;
                        pevent.Graphics.DrawRectangle(penBorder, 0, 0, control.Width - 1, control.Height - 1);
                    }
                }
            }
        }


        #endregion

        /// <summary>
        /// Override the standard method for VS property grid
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return "Bootstrap Border style";
        }

    }

}
