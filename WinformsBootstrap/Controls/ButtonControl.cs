using FontAwesome.Sharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace MyMax.WinformsBootstrap.Controls
{
    public enum Styles
    {
        Primary = 0,
        Secondary,
        Success,
        Danger,
        Warning,
        Info,
        Light,
        Dark, 
        Cancel
    }

    /// <summary>
    /// This control implement a standard button
    /// Version: 1.0 - 31/08/2021
    /// Author: Agostinoni Massimiliano
    /// Credits: RJ Code Advance
    /// </summary>
    public class ButtonControl : System.Windows.Forms.Button
    {
        // Internal members
        int borderSize = 0;
        int borderRadius = 5;

        int topleftBorderRadius = 5;
        int toprighttBorderRadius = 5;
        int bottomleftBorderRadius = 5;
        int bottomrightBorderRadius = 5;
        bool useDifferentRadius = false;

        Color borderColor = Color.Black;
        Styles style;
        IconChar icon;
        Color iconColor;
        int iconSize;
        List<ColorStyle> colorStyles = new List<ColorStyle>();

        public class ColorStyle
        {
            public Color BackColor { get; set; }
            public Color ForeColor { get; set; }
        }


        protected override bool ShowFocusCues
        {
            get
            {
                return false;
            }
        }
        /// <summary>
        /// Color of the icon
        /// </summary>
        [Category("Winforms Bootstrap")]
        [Description("Color of the FontAwesome icon")]
        public Color IconColor
        {
            get => iconColor;
            set
            {
                iconColor = value;
                Image = icon.ToBitmap(iconColor, iconSize);
                Invalidate();
            }
        }

        /// <summary>
        /// Size of the icon
        /// </summary>
        [Category("Winforms Bootstrap")]
        [Description("Size of the FontAwesome icon")]
        public int IconSize
        {
            get => iconSize;
            set
            {
                iconSize = value;
                Image = icon.ToBitmap(iconColor, iconSize);
                Invalidate();
            }
        }

        /// <summary>
        /// Name of the icon
        /// </summary>
        [Category("Winforms Bootstrap")]
        [Description("FontAwesome icon")]
        public IconChar IconName
        {
            get => icon;
            set
            {
                icon = value;
                if (value == IconChar.None)
                {
                    Image = null;
                    TextAlign = ContentAlignment.MiddleCenter;
                    TextImageRelation = TextImageRelation.Overlay;
                }
                else
                {
                    Image = icon.ToBitmap(iconColor, iconSize);
                    TextAlign = ContentAlignment.MiddleLeft;
                    TextImageRelation = TextImageRelation.ImageBeforeText;
                }
                Invalidate();
            }
        }

        [Category("Winforms Bootstrap")]
        [Description("Thickness of the border")]
        public int BorderSize
        {
            get { return borderSize; }
            set
            {
                borderSize = value;
                Invalidate();
            }
        }

        [Category("Winforms Bootstrap")]
        [Description("Radius of the border")]
        public int BorderRadius
        {
            get { return borderRadius; }
            set
            {
                borderRadius = value;
                topleftBorderRadius = value;
                toprighttBorderRadius = value;
                bottomleftBorderRadius = value;
                bottomrightBorderRadius = value;
                useDifferentRadius = false;

                Invalidate();
            }
        }

        [Category("Winforms Bootstrap")]
        [Description("Color of the border")]
        public Color BorderColor
        {
            get { return borderColor; }
            set
            {
                borderColor = value;
                Invalidate();
            }
        }

        [Category("Winforms Bootstrap")]
        [Description("Name of the bootstrap style like Primary, Success, ...")]
        public Styles Style
        {
            get { return style; }
            set
            {
                style = value;
                BackColor = colorStyles[((int)value)].BackColor;
                ForeColor = colorStyles[((int)value)].ForeColor;
                Invalidate();
            }
        }

        public void SetBorderRadius(int topLeft, int topRight, int bottomLeft, int bottomRight)
        {
            useDifferentRadius = true;
            topleftBorderRadius = topLeft;
            toprighttBorderRadius = topRight;
            bottomleftBorderRadius = bottomLeft;
            bottomrightBorderRadius = bottomRight;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public ButtonControl()
        {
            FlatStyle = FlatStyle.Flat;
            FlatAppearance.BorderSize = 0;
            Size = new Size(150, 40);
            BackColor = SystemColors.ControlDark;
            ForeColor = Color.White;
            Cursor = Cursors.Hand;

            iconSize = 16;
            iconColor = Color.Black;
            icon = IconChar.None;

            TextAlign = ContentAlignment.MiddleCenter;
            ImageAlign = ContentAlignment.MiddleLeft;
            TextImageRelation = TextImageRelation.Overlay;

            Resize += new EventHandler(Button_Resize);

            colorStyles.Add(new ColorStyle() { BackColor = Color.FromArgb(14, 109, 253), ForeColor = Color.White });
            colorStyles.Add(new ColorStyle() { BackColor = Color.FromArgb(108, 117, 125), ForeColor = Color.White });
            colorStyles.Add(new ColorStyle() { BackColor = Color.FromArgb(25, 135, 83), ForeColor = Color.White });
            colorStyles.Add(new ColorStyle() { BackColor = Color.FromArgb(221, 52, 68), ForeColor = Color.White });
            colorStyles.Add(new ColorStyle() { BackColor = Color.FromArgb(255, 193, 7), ForeColor = Color.Black });
            colorStyles.Add(new ColorStyle() { BackColor = Color.FromArgb(15, 202, 240), ForeColor = Color.Black });
            colorStyles.Add(new ColorStyle() { BackColor = Color.FromArgb(248, 249, 250), ForeColor = Color.Black });
            colorStyles.Add(new ColorStyle() { BackColor = Color.FromArgb(33, 37, 41), ForeColor = Color.White });
            colorStyles.Add(new ColorStyle() { BackColor = Color.LightGray, ForeColor = Color.FromArgb(14, 109, 253) });

            Style = Styles.Primary;

        }

        /// <summary>
        /// Resize the button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Resize(object sender, EventArgs e)
        {
            if (borderRadius > this.Height)
                borderRadius = this.Height;
        }

        //Methods
        private GraphicsPath GetFigurePath(Rectangle rect, float radius)
        {
            GraphicsPath path = new GraphicsPath();

            if (useDifferentRadius)
            {
                float curveSize = radius * 2F;

                path.StartFigure();
                if (topleftBorderRadius == 0)
                    path.AddLine(new PointF(rect.X, rect.Y), new PointF(rect.X + curveSize, rect.Y));
                else
                    path.AddArc(rect.X, rect.Y, curveSize, curveSize, 180, 90);

                if (toprighttBorderRadius == 0)
                    path.AddLine(new PointF(rect.Right - curveSize, rect.Y), new PointF(rect.Right, rect.Y));
                else
                    path.AddArc(rect.Right - curveSize, rect.Y, curveSize, curveSize, 270, 90);

                if (bottomrightBorderRadius == 0)
                    path.AddLine(new PointF(rect.Right, rect.Bottom - curveSize), new PointF(rect.Right, rect.Bottom));
                else
                    path.AddArc(rect.Right - curveSize, rect.Bottom - curveSize, curveSize, curveSize, 0, 90);

                if (bottomleftBorderRadius == 0)
                    path.AddLine(new PointF(rect.X + curveSize, rect.Bottom), new PointF(rect.X, rect.Bottom));
                else
                    path.AddArc(rect.X, rect.Bottom - curveSize, curveSize, curveSize, 90, 90);

                path.CloseFigure();
            }
            else
            {
                float curveSize = radius * 2F;

                path.StartFigure();
                path.AddArc(rect.X, rect.Y, curveSize, curveSize, 180, 90);
                path.AddArc(rect.Right - curveSize, rect.Y, curveSize, curveSize, 270, 90);
                path.AddArc(rect.Right - curveSize, rect.Bottom - curveSize, curveSize, curveSize, 0, 90);
                path.AddArc(rect.X, rect.Bottom - curveSize, curveSize, curveSize, 90, 90);
                path.CloseFigure();
            }
            
            
            return path;
        }

        protected override void OnPaint(PaintEventArgs pevent)
        {
            base.OnPaint(pevent);

            Rectangle rectSurface = this.ClientRectangle;
            Rectangle rectBorder = Rectangle.Inflate(rectSurface, -borderSize, -borderSize);
            int smoothSize = 2;
            if (borderSize > 0)
                smoothSize = borderSize;

            if (borderRadius > 2) //Rounded button
            {
                using (GraphicsPath pathSurface = GetFigurePath(rectSurface, borderRadius))
                using (GraphicsPath pathBorder = GetFigurePath(rectBorder, borderRadius - borderSize))
                using (Pen penSurface = new Pen(Parent.BackColor, smoothSize))
                using (Pen penBorder = new Pen(borderColor, borderSize))
                {
                    pevent.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                    //Button surface
                    this.Region = new Region(pathSurface);
                    //Draw surface border for HD result
                    pevent.Graphics.DrawPath(penSurface, pathSurface);

                    //Button border                    
                    if (borderSize >= 1)
                        //Draw control border
                        pevent.Graphics.DrawPath(penBorder, pathBorder);
                }
            }
            else //Normal button
            {
                pevent.Graphics.SmoothingMode = SmoothingMode.None;
                //Button surface
                this.Region = new Region(rectSurface);
                //Button border
                if (borderSize >= 1)
                {
                    using (Pen penBorder = new Pen(borderColor, borderSize))
                    {
                        penBorder.Alignment = PenAlignment.Inset;
                        pevent.Graphics.DrawRectangle(penBorder, 0, 0, this.Width - 1, this.Height - 1);
                    }
                }
            }

            // For children controls
            foreach (Control item in Controls)
            {
                item.Invalidate();
            }
        }

        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            this.Parent.BackColorChanged += new EventHandler(Container_BackColorChanged);
        }

        private void Container_BackColorChanged(object sender, EventArgs e)
        {
            this.Invalidate();
        }

    }

}
