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
    public class ButtonControl : System.Windows.Forms.Button, ICorner
    {
        // Internal members
        int borderSize = 0;
        private CornerRadius cornerRadius;
        bool solid = true;

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

        #region Properties

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
        [Description("Solid or outline look and feel")]
        public bool Solid
        {
            get { return solid; }
            set
            {
                solid = value;
                Invalidate();
            }
        }

        /// <summary>
        /// Border radius of the control
        /// </summary>
        [Category("Winforms Bootstrap")]
        [Description("Radius of corners")]
        [TypeConverter(typeof(System.ComponentModel.ExpandableObjectConverter))]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [Browsable(true)]
        public CornerRadius CornerRadius
        {
            get { return cornerRadius; }
            set
            {
                cornerRadius = value;
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
                if (solid)
                {
                    BackColor = colorStyles[((int)value)].BackColor;
                    ForeColor = colorStyles[((int)value)].ForeColor;
                }
                else
                {
                    BackColor = DefaultStyles.ContainerBackcolor;
                    ForeColor = colorStyles[((int)value)].BackColor;
                    borderColor = colorStyles[((int)value)].BackColor;
                }
                Invalidate();
            }
        }

        #endregion

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
            Font = new Font("Segoe UI", 8, FontStyle.Regular);

            CornerRadius = new CornerRadius() { All = 5 };

            iconSize = 16;
            iconColor = Color.Black;
            icon = IconChar.None;

            TextAlign = ContentAlignment.MiddleCenter;
            ImageAlign = ContentAlignment.MiddleLeft;
            TextImageRelation = TextImageRelation.Overlay;
            this.Resize += Button_Resize;

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
            Solid = true;

        }

        /// <summary>
        /// Resize the button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Resize(object sender, EventArgs e)
        {
            Refresh();
        }


        /// <summary>
        /// Create a rounded figure path
        /// </summary>
        /// <param name="rect"></param>
        /// <returns></returns>
        private GraphicsPath GetFigurePath(Rectangle rect)
        {
            GraphicsPath path = new GraphicsPath();
            float factor = 2F;

            path.StartFigure();
            if (CornerRadius.TopLeft > 0)
            {
                float curveSize = CornerRadius.TopLeft * factor;
                path.AddArc(rect.X, rect.Y, curveSize, curveSize, 180, 90);
            }
            else
                path.AddLine(new PointF(rect.X, rect.Y), new PointF(rect.X, rect.Y));

            if (CornerRadius.TopRight > 0)
            {
                float curveSize = CornerRadius.TopRight * factor;
                path.AddArc(rect.Right - curveSize, rect.Y, curveSize, curveSize, 270, 90);
            }
            else
                path.AddLine(new PointF(rect.Right, rect.Y), new PointF(rect.Right, rect.Y));

            if (CornerRadius.BottomRight > 0)
            {
                float curveSize = CornerRadius.BottomRight * factor;
                path.AddArc(rect.Right - curveSize, rect.Bottom - curveSize, curveSize, curveSize, 0, 90);
            }
            else
                path.AddLine(new PointF(rect.Right, rect.Bottom), new PointF(rect.Right, rect.Bottom));

            if (CornerRadius.BottomLeft >= 0)
            {
                float curveSize = CornerRadius.BottomLeft * factor;
                path.AddArc(rect.X, rect.Bottom - curveSize, curveSize, curveSize, 90, 90);
            }
            else
                path.AddLine(new PointF(rect.X, rect.Bottom), new PointF(rect.X, rect.Bottom));

            path.CloseFigure();

            return path;
        }

        protected override void OnPaint(PaintEventArgs pevent)
        {
            base.OnPaint(pevent);

            Rectangle rectSurface = ClientRectangle;
            Rectangle rectBorder = Rectangle.Inflate(rectSurface, -borderSize, -borderSize);
            int smoothSize = 2;
            if (borderSize > 0)
                smoothSize = borderSize;

            if (CornerRadius.All != 0)
            {
                using (GraphicsPath pathSurface = GetFigurePath(rectSurface))
                using (GraphicsPath pathBorder = GetFigurePath(rectBorder))
                using (Pen penSurface = new Pen(BackColor, smoothSize))
                using (Pen penBorder = new Pen(borderColor, borderSize))
                {
                    pevent.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                    Region = new Region(pathSurface);
                    pevent.Graphics.DrawPath(penSurface, pathSurface);
                    if (borderSize >= 1)
                        pevent.Graphics.DrawPath(penBorder, pathBorder);
                }
            }
            else
            {
                pevent.Graphics.SmoothingMode = SmoothingMode.None;
                this.Region = new Region(rectSurface);
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
            this.Parent.BackColorChanged += Container_BackColorChanged;
        }

        private void Container_BackColorChanged(object sender, EventArgs e)
        {
            this.Invalidate();
        }

    }

}
