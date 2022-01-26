using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.Layout;

namespace GUILibrary.Controls
{
    [Designer("System.Windows.Forms.Design.ParentControlDesigner, System.Design", typeof(IDesigner))]
    public partial class Pane : UserControl
    {
        #region Container Management

        /// <summary>
        /// Flow direction
        /// Vertical, controls will be vertically stacked
        /// Horizontal, controls will be horizontally stacked
        /// </summary>
        public enum FlowDirections
        {
            Vertical = 0,
            Horizontal
        }

        /// <summary>
        /// Alignment of controls
        /// LeftOrTop, depending on Flow direction, controls are aligned on container Left border or Top border
        /// Center, , depending on Flow direction, controls are aligned on container middle Y or X axis
        /// RightorBottom, , depending on Flow direction, controls are aligned on container Bottom border or Right border
        /// </summary>
        public enum ControlAlignments
        {
            LeftorTop = 0,
            Center,
            RightorBottom
        }

        /// <summary>
        /// Resize the content
        /// </summary>
        [Category("Bootstrap")]
        [Description("Set True if control resizes based on content")]
        public bool ContentResize { get; set; } = true;

        /// <summary>
        /// Flow direction of the container
        /// </summary>
        [Category("Bootstrap")]
        [Description("Direction on how controls are rendered")]
        public FlowDirections FlowDirection { get; set; } = FlowDirections.Vertical;

        /// <summary>
        /// Alignment of controls
        /// </summary>
        [Category("Bootstrap")]
        [Description("Control alignement when rendered")]
        public ControlAlignments ControlAlignment { get; set; } = ControlAlignments.LeftorTop;

        /// <summary>
        /// Flow direction of the container
        /// </summary>
        [Category("Bootstrap")]
        [Description("Get/Set if controls are docked")]
        public bool DockedControls { get; set; } = true;

        /// <summary>
        /// Layout engine
        /// </summary>
        public override LayoutEngine LayoutEngine
        {
            get
            {
                if (!DockedControls)
                    return base.LayoutEngine;
                else
                    return new BootstrapPaneLayout();
            }
        }

        /// <summary>
        /// Manage the layout engine for a pane
        /// </summary>
        private class BootstrapPaneLayout : LayoutEngine
        {

            public override bool Layout(object container, LayoutEventArgs layoutEventArgs)
            {
                Pane parent = container as Pane;

                // Use DisplayRectangle so that parent.Padding is honored.
                Point nextControlLocation = parent.DisplayRectangle.Location;

                foreach (Control c in parent.Controls)
                {
                    LayoutControl(parent, c, ref nextControlLocation);
                }
                
                // Resize the UIContainer
                if ((parent.ContentResize) && (parent.Controls.Count > 0))
                {
                    if (parent.FlowDirection == Pane.FlowDirections.Vertical)
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

            private void LayoutControl(Control parent, Control c, ref Point nextControlLocation)
            {
                // Use DisplayRectangle so that parent.Padding is honored.
                Rectangle parentDisplayRectangle = parent.DisplayRectangle;

                // Only apply layout to visible controls.
                if (!c.Visible) return;

                Pane container = parent as Pane;

                if (container.FlowDirection == Pane.FlowDirections.Vertical)
                {
                    if (container.ControlAlignment == Pane.ControlAlignments.LeftorTop) nextControlLocation.Offset(c.Margin.Left, c.Margin.Top);
                    if (container.ControlAlignment == Pane.ControlAlignments.Center) nextControlLocation.Offset((parent.DisplayRectangle.Width - (c.Margin.Left + c.Margin.Right + c.Width)) / 2, c.Margin.Top);
                    if (container.ControlAlignment == Pane.ControlAlignments.RightorBottom) nextControlLocation.Offset(parent.DisplayRectangle.Width - (c.Margin.Left + c.Margin.Right + c.Width), c.Margin.Top);
                }
                else
                {
                    if (container.ControlAlignment == Pane.ControlAlignments.LeftorTop) nextControlLocation.Offset(c.Margin.Left, c.Margin.Top);
                    if (container.ControlAlignment == Pane.ControlAlignments.Center) nextControlLocation.Offset(c.Margin.Left, (parent.DisplayRectangle.Height - (c.Margin.Top + c.Margin.Bottom + c.Height)) / 2);
                    if (container.ControlAlignment == Pane.ControlAlignments.RightorBottom) nextControlLocation.Offset(c.Margin.Left, (parent.DisplayRectangle.Height - (c.Margin.Top + c.Margin.Bottom + c.Height)));
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
                    switch (c.Dock)
                    {
                        case DockStyle.None:
                            break;
                        case DockStyle.Top:
                        case DockStyle.Bottom:
                            if (container.FlowDirection == Pane.FlowDirections.Vertical)
                                c.Size = new Size(parent.Width - parent.Padding.Right - c.Margin.Right - parent.Padding.Left - c.Margin.Left, c.Height);
                            else
                                c.Size = new Size(parent.Width - c.Left - parent.Padding.Left - c.Margin.Left, c.Height);
                            break;

                        case DockStyle.Left:
                        case DockStyle.Right:
                            if (container.FlowDirection == Pane.FlowDirections.Vertical)
                                c.Size = new Size(c.Width, parent.Height - c.Top - parent.Padding.Bottom - c.Margin.Bottom);
                            else
                                c.Size = new Size(c.Width, parent.Height - parent.Padding.Top - parent.Padding.Bottom - c.Margin.Top - c.Margin.Bottom);
                            break;
                        case DockStyle.Fill:
                            if (container.FlowDirection == Pane.FlowDirections.Vertical)
                                c.Size = new Size(parent.Width - parent.Padding.Right - c.Margin.Right - parent.Padding.Left - c.Margin.Left, parent.Height - c.Top - parent.Padding.Bottom - c.Margin.Bottom);
                            else
                                c.Size = new Size(parent.Width - c.Left - parent.Padding.Left - c.Margin.Left, parent.Height - parent.Padding.Top - parent.Padding.Bottom - c.Margin.Top - c.Margin.Bottom);
                            break;
                        default:
                            break;
                    }
                }

                if (container.FlowDirection == Pane.FlowDirections.Vertical)
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

        #endregion


        

    }
}
