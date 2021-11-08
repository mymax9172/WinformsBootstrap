using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.Windows.Forms.Layout;

namespace MyMax.WinformsBootstrap.Controls
{
    /// <summary>
    /// This control implement a standard container
    /// Version: 1.0 - 30/08/2021
    /// Version: 1.0 - 13/10/2021 Bug fixing
    /// Author: Agostinoni Massimiliano
    /// </summary>
    public class ContainerControl : Panel, ICorner
    {
        // Internal members
        #region Internal member

        private bool contentResize = false;
        private int borderSize;
        private Color borderColor = Color.Black;

        private ControlOrders controlOrder = ControlOrders.Design;
        private FlowDirections flowDirection = FlowDirections.Vertical;
        private ControlAlignments controlAlignment = ControlAlignments.LeftOrTop;
        private UIContainerFlowLayout layoutEngine;
        private bool dockedControls = true;

        private CornerRadius cornerRadius;

        #endregion

        #region Enumerations

        /// <summary>
        /// Flow direction
        /// Vertical, controls will be stacked
        /// Horizontal, controls will be aligned on the same row
        /// </summary>
        public enum FlowDirections
        {
            Vertical = 0,
            Horizontal
        }

        /// <summary>
        /// Alignment of children controls
        /// LeftOrTop, depending on Flow direction, controls are aligned on container Left border or Top border
        /// Center, , depending on Flow direction, controls are aligned on container middle Y or X axis
        /// RightOrBottom, , depending on Flow direction, controls are aligned on container Bottom border or Right border
        /// </summary>
        public enum ControlAlignments
        {
            LeftOrTop = 0,
            Center,
            RightOrBottom
        }

        /// <summary>
        /// Control order displacement
        /// Runtime, controls are placed based on the order they are inserted programatically
        /// Design, controls are placed based on the order they are added via designer
        /// </summary>
        public enum ControlOrders
        {
            Runtime = 0,
            Design
        }

        #endregion

        #region Properties

        /// <summary>
        /// Border size for this container
        /// </summary>
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

        /// <summary>
        /// Resize the content
        /// </summary>
        [Category("Winforms Bootstrap")]
        [Description("Set True if control resizes based on content")]
        public bool ContentResize
        {
            get { return contentResize; }
            set
            {
                if (!dockedControls)
                    contentResize = false;
                else
                    contentResize = value;
                Invalidate();
            }
        }

        /// <summary>
        /// Control order appereance for child controls
        /// </summary>
        [Category("Winforms Bootstrap")]
        [Description("Define how those controls are rendered, in regular order (for controls added at runtime) or in reverse order (for controls added at designtime)")]
        public ControlOrders ControlOrder
        {
            get => controlOrder;
            set
            {
                if (value != controlOrder)
                {
                    controlOrder = value;
                    layoutEngine = new UIContainerFlowLayout((controlOrder == ControlOrders.Runtime));
                    Invalidate();
                }
            }
        }
        // [TypeConverter(typeof(BorderRadius))]

        /// <summary>
        /// Border radius of the container
        /// </summary>
        [Category("Winforms Bootstrap")]
        [Description("Radius of the border corners")]
        [TypeConverter(typeof(System.ComponentModel.ExpandableObjectConverter))]
        //[TypeConverter(typeof(BorderRadius))]
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

        /// <summary>
        /// Border color of the container
        /// </summary>
        [Category("Winforms Bootstrap")]
        [Description("Color of the border")]
        public Color BorderColor
        {
            get => borderColor;
            set
            {
                borderColor = value;
                Invalidate();
            }
        }

        /// <summary>
        /// Flow direction of the container
        /// </summary>
        [Category("Winforms Bootstrap")]
        [Description("Direction on how controls are rendered")]
        public FlowDirections FlowDirection
        {
            get => flowDirection;
            set
            {
                flowDirection = value;
                Invalidate();
            }
        }

        /// <summary>
        /// Alignment of controls
        /// </summary>
        [Category("Winforms Bootstrap")]
        [Description("Control alignement when rendered")]
        public ControlAlignments ControlAlignment
        {
            get => controlAlignment;
            set
            {
                controlAlignment = value;
                Invalidate();
            }
        }

        /// <summary>
        /// Flow direction of the container
        /// </summary>
        [Category("Winforms Bootstrap")]
        [Description("Get/Set if controls are free or docked")]
        public bool DockedControls
        {
            get => dockedControls;
            set
            {
                dockedControls = value;
                if (!dockedControls) contentResize = false;
                Invalidate();
            }
        }

        #endregion

        /// <summary>
        /// Constructor
        /// </summary>
        public ContainerControl()
        {
            Size = new Size(150, 40);
            BackColor = DefaultStyles.ContainerBackcolor;
            ForeColor = DefaultStyles.ContainerForecolor;
            Margin = new Padding(3);
            Padding = new Padding(3);
            CornerRadius = new CornerRadius() { All = 5 };
            Font = new Font("Segoe UI", 8, FontStyle.Regular);
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

            Rectangle rectSurface = this.ClientRectangle;
            Rectangle rectBorder = Rectangle.Inflate(rectSurface, -borderSize, -borderSize);
            int smoothSize = 2;
            if (borderSize > 0)
                smoothSize = borderSize;


            using (GraphicsPath pathSurface = GetFigurePath(rectSurface))
            using (GraphicsPath pathBorder = GetFigurePath(rectBorder))
            using (Pen penSurface = new Pen(this.Parent.BackColor, smoothSize))
            using (Pen penBorder = new Pen(borderColor, borderSize))
            {
                pevent.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                Region = new Region(pathSurface);
                pevent.Graphics.DrawPath(penSurface, pathSurface);                   
                if (borderSize >= 1)
                    pevent.Graphics.DrawPath(penBorder, pathBorder);
            }

        }

        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            Parent.BackColorChanged += new EventHandler(Container_BackColorChanged);
        }

        private void Container_BackColorChanged(object sender, EventArgs e)
        {
            Invalidate();
        }

        public override LayoutEngine LayoutEngine
        {
            get
            {
                if (!DockedControls) return base.LayoutEngine;

                if (layoutEngine == null)
                {
                    layoutEngine = new UIContainerFlowLayout((controlOrder == ControlOrders.Runtime));
                }
                return layoutEngine;
            }
        }

        private class UIContainerFlowLayout : LayoutEngine
        {
            bool order;

            public UIContainerFlowLayout(bool regularOrder = true)
            {
                order = regularOrder;
            }

            public override bool Layout(object container, LayoutEventArgs layoutEventArgs)
            {
                ContainerControl parent = container as ContainerControl;

                // Use DisplayRectangle so that parent.Padding is honored.
                Point nextControlLocation = parent.DisplayRectangle.Location;

                if (order)
                {
                    foreach (Control c in parent.Controls)
                    {
                        LayoutControl(parent, c, ref nextControlLocation);
                    }
                }
                else
                {
                    for (int index = parent.Controls.Count - 1; index >= 0; index--)
                    {
                        Control c = parent.Controls[index];
                        LayoutControl(parent, c, ref nextControlLocation);
                    }
                }

                // Resize the UIContainer
                if ((parent.ContentResize) && (parent.Controls.Count > 0))
                {
                    if (parent.FlowDirection == FlowDirections.Vertical)
                    {
                        
                        int maxY = nextControlLocation.Y + parent.Padding.Bottom; 

                        if (!parent.MinimumSize.IsEmpty)
                        {     
                            if (maxY < parent.MinimumSize.Height) maxY = parent.MinimumSize.Height;
                        }

                        if (!parent.MaximumSize.IsEmpty)
                        {
                            if (maxY > parent.MaximumSize.Height) maxY = parent.MaximumSize.Height;
                        }

                        parent.Height = maxY;
                    }
                    else
                    {
                        int maxX = nextControlLocation.X + parent.Padding.Right;

                        if (!parent.MinimumSize.IsEmpty)
                        {
                            if (maxX < parent.MinimumSize.Width) maxX = parent.MinimumSize.Width;
                        }

                        if (!parent.MaximumSize.IsEmpty)
                        {
                            if (maxX > parent.MaximumSize.Width) maxX = parent.MaximumSize.Width;
                        }

                        parent.Width = maxX;
                    }
                }

                return false;
            }

            private void LayoutControl(ContainerControl parent, Control c, ref Point nextControlLocation)
            {
                // Use DisplayRectangle so that parent.Padding is honored.
                Rectangle parentDisplayRectangle = parent.DisplayRectangle;

                // Only apply layout to visible controls.
                if (!c.Visible) return;

                if (parent.FlowDirection == FlowDirections.Vertical)
                {
                    if (parent.ControlAlignment == ControlAlignments.LeftOrTop) nextControlLocation.Offset(c.Margin.Left, c.Margin.Top);
                    if (parent.ControlAlignment == ControlAlignments.Center) nextControlLocation.Offset((parent.DisplayRectangle.Width - (c.Margin.Left + c.Margin.Right + c.Width)) / 2, c.Margin.Top);
                    if (parent.ControlAlignment == ControlAlignments.RightOrBottom) nextControlLocation.Offset(parent.DisplayRectangle.Width - (c.Margin.Left + c.Margin.Right + c.Width), c.Margin.Top);
                }
                else
                {
                    if (parent.ControlAlignment == ControlAlignments.LeftOrTop) nextControlLocation.Offset(c.Margin.Left, c.Margin.Top);
                    if (parent.ControlAlignment == ControlAlignments.Center) nextControlLocation.Offset(c.Margin.Left, (parent.DisplayRectangle.Height - (c.Margin.Top + c.Margin.Bottom + c.Height)) / 2);
                    if (parent.ControlAlignment == ControlAlignments.RightOrBottom) nextControlLocation.Offset(c.Margin.Left, (parent.DisplayRectangle.Height - (c.Margin.Top + c.Margin.Bottom + c.Height)));
                }

                // Set the location of the control.
                c.Location = nextControlLocation;

                // Set the autosized controls to their 
                // autosized heights.
                if (c.AutoSize)
                {
                    c.Size = c.GetPreferredSize(parentDisplayRectangle.Size);
                }

                if (parent.FlowDirection == FlowDirections.Vertical)
                {
                    // Move X back to the display rectangle origin.
                    nextControlLocation.X = parentDisplayRectangle.X;

                    // Increment Y by the height of the control 
                    // and the bottom margin.
                    nextControlLocation.Y += c.Height + c.Margin.Bottom;
                }
                else
                {
                    // Move X back to the display rectangle origin.
                    nextControlLocation.X += c.Width + c.Margin.Right;

                    // Increment Y by the height of the control 
                    // and the bottom margin.
                    nextControlLocation.Y = parentDisplayRectangle.Y;
                }
            }
        }

    }
}
