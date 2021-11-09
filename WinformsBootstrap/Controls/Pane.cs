using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.Layout;

namespace GUILibrary.Controls
{
    [Designer("System.Windows.Forms.Design.ParentControlDesigner, System.Design", typeof(IDesigner))]
    public partial class Pane : UserControl, IContainerStyle, IBorderStyle
    {
        [Category("Bootstrap")]
        [Description("Border configuration")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public new BorderStyle BorderStyle { get; set; } = new BorderStyle();

        [Category("Bootstrap")]
        [Description("Container layour configuration")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public ContainerStyle ContainerStyle { get; set; } = new ContainerStyle();

        public Pane()
        {
            InitializeComponent();
            Resize += new EventHandler(Pane_Resize);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            BorderStyle.OnPaint(this, e);
        }

        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            this.Parent.BackColorChanged += new EventHandler(Pane_BackColorChanged);
        }

        private void Pane_BackColorChanged(object sender, EventArgs e)
        {
            Invalidate();
        }

        private void Pane_Resize(object sender, EventArgs e)
        {
            if (BorderStyle.Thinkness > Height)
                BorderStyle.Thinkness = Height;
            Refresh();
        }

        public override LayoutEngine LayoutEngine
        {
            get
            {
                if (!ContainerStyle.DockedControls)
                    return base.LayoutEngine;
                else
                    return ContainerStyle.LayoutEngine;
            }
        }

    }
}
