using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMax.WinformsBootstrap.Controls
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Windows.Forms;
    using System.Windows.Forms.Layout;


    /// <summary>
    /// This control implement a standard group control
    /// Version: 1.0 - 21/10/2021
    /// Author: Agostinoni Massimiliano
    /// </summary>
    public class GroupControl : Panel
    {
        // Internal members
        #region Internal member

        private ControlOrders controlOrder = ControlOrders.Design;
        private UIContainerFlowLayout layoutEngine;
        private bool contentResize = false;

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

        /// <summary>
        /// Control order appereance for child controls
        /// </summary>
        [Category("Windows Bootstrap")]
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
                    this.Invalidate();
                }
            }
        }

        /// <summary>
        /// Flow direction of the container
        /// </summary>
        [Category("Windows Bootstrap")]
        [Description("Direction on how controls are rendered")]
        public FlowDirections FlowDirection { get; set; } = FlowDirections.Vertical;

        /// <summary>
        /// Resize the content
        /// </summary>
        [Category("Windows Bootstrap")]
        [Description("Set True if control resizes based on content")]
        public bool ContentResize
        {
            get { return contentResize; }
            set
            {
                contentResize = value;
                this.Invalidate();
            }
        }

        /// <summary>
        /// Constructur
        /// </summary>
        public GroupControl()
        {
            this.Size = new Size(150, 40);
            this.Resize += new EventHandler(Panel_Resize);
            this.Margin = new Padding(3);
            this.Padding = new Padding(0);
        }

        private void Panel_Resize(object sender, EventArgs e)
        {
            Refresh();
        }

        protected override void OnPaint(PaintEventArgs pevent)
        {
            base.OnPaint(pevent);

            Rectangle rectSurface = this.ClientRectangle;

            pevent.Graphics.SmoothingMode = SmoothingMode.None;
            this.Region = new Region(rectSurface);
        }

        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            this.Parent.BackColorChanged += new EventHandler(Container_BackColorChanged);
        }

        private void Container_BackColorChanged(object sender, EventArgs e)
        {
            this.BackColor = this.Parent.BackColor;
            this.Invalidate();
        }

        public override LayoutEngine LayoutEngine
        {
            get
            {
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
                GroupControl parent = container as GroupControl;
                int relativePosition;

                // Use DisplayRectangle so that parent.Padding is honored.
                Point nextControlLocation = parent.DisplayRectangle.Location;

                if (order)
                { 
                    if (parent.Controls.Count == 1)
                        LayoutControl(parent, parent.Controls[0], ref nextControlLocation, 9);
                    else
                    {
                        for (int index = 0; index < parent.Controls.Count; index++)
                        {
                            Control c = parent.Controls[index];

                                if (index == 0)
                                    relativePosition = 0;
                                else if (index < parent.Controls.Count - 1)
                                    relativePosition = 1;
                                else relativePosition = 2;

                                LayoutControl(parent, c, ref nextControlLocation, relativePosition);
              
                        }
                    }

                }
                else
                {
                    if (parent.Controls.Count == 1)
                        LayoutControl(parent, parent.Controls[0], ref nextControlLocation, 9);
                    else
                    {
                        for (int index = parent.Controls.Count - 1; index >= 0; index--)
                        {
                            Control c = parent.Controls[index];


                                if (index == 0)
                                    relativePosition = 0;
                                else if (index < parent.Controls.Count - 1)
                                    relativePosition = 1;
                                else relativePosition = 2;

                                LayoutControl(parent, c, ref nextControlLocation, relativePosition);
                        
                        }
                    }

                }

                // Resize the UIContainer
                if ((parent.ContentResize) && (parent.Controls.Count > 0))
                {
                    int maxX = 0, maxY = 0;

                    foreach (Control c in parent.Controls)
                    {
                        int x = c.Margin.Left + c.Left + c.Width + c.Margin.Right;
                        int y = c.Margin.Top + c.Top + c.Height + c.Margin.Bottom;
                        if (x > maxX) maxX = x;
                        if (y > maxY) maxY = y;
                    }
                    maxX += parent.Padding.Right;
                    maxY += parent.Padding.Bottom;

                    if (!parent.MinimumSize.IsEmpty)
                    {
                        if (maxX < parent.MinimumSize.Width) maxX = parent.MinimumSize.Width;
                        if (maxY < parent.MinimumSize.Height) maxY = parent.MinimumSize.Height;
                    }

                    parent.Size = new Size(maxX, maxY);
                }

                return false;
            }

            private void LayoutControl(GroupControl parent, Control c, ref Point nextControlLocation, int relativePosition)
            {
                // Use DisplayRectangle so that parent.Padding is honored.
                Rectangle parentDisplayRectangle = parent.DisplayRectangle;
                ICorner control = c as ICorner;

                // Only apply layout to visible controls.
                if (!c.Visible) return;

                // Define rounder border
                switch (relativePosition)
                {
                    case 0:
                        if (parent.FlowDirection == FlowDirections.Vertical)
                            control.CornerRadius.BottomCorners = 0;
                        else
                            control.CornerRadius.RightCorners = 0;
                        nextControlLocation.Offset(c.Margin.Left, c.Margin.Top);
                        break;
                    case 1:
                        control.CornerRadius.All = 0;
                        if (parent.FlowDirection == FlowDirections.Vertical)
                            nextControlLocation.Offset(c.Margin.Left, 0);
                        else
                            nextControlLocation.Offset(0, c.Margin.Top);
                        break;
                    case 2:
                        if (parent.FlowDirection == FlowDirections.Vertical)
                        {
                            control.CornerRadius.TopCorners = 0;
                            nextControlLocation.Offset(c.Margin.Left, 0);
                        }
                        else
                        {
                            control.CornerRadius.LeftCorners = 0;
                            nextControlLocation.Offset(0, c.Margin.Top);
                        }
                        break;
                    case 9:
                        nextControlLocation.Offset(c.Margin.Left, c.Margin.Top);
                        break;
                }

                // Set the location of the control.
                c.Location = nextControlLocation;

                // Set the autosized controls to their 
                // autosized heights.
                if (c.AutoSize)
                {
                    c.Size = c.GetPreferredSize(parentDisplayRectangle.Size);
                }
                else
                {
                    if (parent.FlowDirection == FlowDirections.Vertical)
                        c.Size = new Size(parent.Width - parent.Padding.Right - c.Margin.Right - parent.Padding.Left - c.Margin.Left, c.Height);
                    else
                        c.Size = new Size(c.Width, parent.Height - parent.Padding.Top - parent.Padding.Bottom - c.Margin.Top - c.Margin.Bottom);
                }

                if (parent.FlowDirection == FlowDirections.Vertical)
                {
                    // Move X back to the display rectangle origin.
                    nextControlLocation.X = parentDisplayRectangle.X;

                    // Increment Y by the height of the control 
                    // and the bottom margin.
                    nextControlLocation.Y += c.Height;

                }
                else
                {
                    // Move X back to the display rectangle origin.
                    nextControlLocation.X += c.Width;

                    // Increment Y by the height of the control 
                    // and the bottom margin.
                    nextControlLocation.Y = parentDisplayRectangle.Y;
                }
            }
        }

    }

}
