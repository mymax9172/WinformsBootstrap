using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace MyMax.WinformsBootstrap.Controls
{
    public partial class Button : BootstrapControl
    {
        /// <summary>
        /// This region takes care of color styles of the button
        /// </summary>
        #region Style management

        // Internal members
        private Styles style = Styles.None;                             // Style of the button
        private ButtonStatuses status = ButtonStatuses.MouseOutside;    // Status of the mouse
        private bool isSuspended = false;                               // True if style application is suspended
        private bool isPushed = false;

        /// <summary>
        /// Bootstrap styles
        /// </summary>
        public enum Styles
        {
            None = 0,
            Primary,
            Secondary,
            Success,
            Danger,
            Warning,
            Info,
            Light,
            Dark,
            Cancel,
            Outlined_Primary,
            Outlined_Secondary,
            Outlined_Success,
            Outlined_Danger,
            Outlined_Warning,
            Outlined_Info,
            Outlined_Light,
            Outlined_Dark,
            Outlined_Cancel,
        }

        /// <summary>
        /// Button status
        /// </summary>
        private enum ButtonStatuses
        {
            MouseOutside = 0,
            MouseHover,
            ButtonPushed,
            Disabled
        }

        /// <summary>
        /// Private structure to manage color styles
        /// </summary>
        private struct ButtonStyle
        {
            public Color BackColor { get; set; }
            public Color ForeColor { get; set; }
            public Color BorderColor { get; set; }
            public int BorderSize { get; set; }
            public Color MouseHoverBackColor { get; set; }
            public Color MouseHoverForeColor { get; set; }
            public Color MouseHoverBorderColor { get; set; }
            public int MouseHoverBorderSize { get; set; }
            public Color MouseDownBackColor { get; set; }
            public Color MouseDownForeColor { get; set; }
            public Color MouseDownBorderColor { get; set; }
            public int MouseDownBorderSize { get; set; }
            public Color DisabledBackColor { get; set; }
            public Color DisabledForeColor { get; set; }
            public Color DisabledBorderColor { get; set; }
            public int DisabledBorderSize { get; set; }


            public static ButtonStyle SolidColor(Color backColor, Color foreColor)
            {
                ButtonStyle cs = new ButtonStyle()
                {
                    BackColor = backColor,
                    ForeColor = foreColor,
                    BorderColor = backColor,
                    MouseHoverForeColor = foreColor,
                    MouseHoverBorderColor = backColor,
                    MouseDownBorderColor = backColor,
                    MouseDownForeColor = foreColor,
                    DisabledBorderColor = foreColor,
                    DisabledForeColor = foreColor,
                    BorderSize = 1,
                    MouseDownBorderSize = 2,
                    MouseHoverBorderSize = 1,
                    DisabledBorderSize = 1
                };
                cs.MouseHoverBackColor = ChangeColorBrightness(backColor, 0.2f);
                cs.MouseDownBackColor = ChangeColorBrightness(backColor, -0.2f);
                cs.DisabledBackColor = ChangeColorBrightness(backColor, 0.8f);
                return cs;
            }

            public static ButtonStyle OutlinedColor(Color backColor, Color foreColor)
            {
                ButtonStyle cs = new ButtonStyle()
                {
                    BackColor = Color.Transparent,
                    ForeColor = backColor,
                    BorderColor = backColor,
                    MouseHoverBackColor = backColor,
                    MouseHoverForeColor = foreColor,
                    MouseHoverBorderColor = backColor,
                    MouseDownBorderColor = backColor,
                    MouseDownForeColor = foreColor,
                    DisabledBorderColor = foreColor,
                    DisabledBackColor = Color.Transparent,
                    BorderSize = 1,
                    MouseDownBorderSize = 2,
                    MouseHoverBorderSize = 1,
                    DisabledBorderSize = 1
                };
                cs.MouseDownBackColor = ChangeColorBrightness(backColor, -0.2f);
                return cs;
            }
        }

        /// <summary>
        /// Bootstrap style collection
        /// </summary>
        private static Dictionary<Styles, ButtonStyle> BootstrapStyles { get; set; }

        /// <summary>
        /// Create Bootstrap styles (solid and outlined)
        /// </summary>
        private void DefineBootstrapColorStyle()
        {
            BootstrapStyles = new Dictionary<Styles, ButtonStyle>();
            BootstrapStyles.Add(Styles.Primary, ButtonStyle.SolidColor(Color.FromArgb(14, 109, 253), Color.White));
            BootstrapStyles.Add(Styles.Secondary, ButtonStyle.SolidColor(Color.FromArgb(108, 117, 125), Color.White));
            BootstrapStyles.Add(Styles.Success, ButtonStyle.SolidColor(Color.FromArgb(25, 135, 83), Color.White));
            BootstrapStyles.Add(Styles.Danger, ButtonStyle.SolidColor(Color.FromArgb(221, 52, 68), Color.White));
            BootstrapStyles.Add(Styles.Warning, ButtonStyle.SolidColor(Color.FromArgb(255, 193, 7), Color.Black));
            BootstrapStyles.Add(Styles.Info, ButtonStyle.SolidColor(Color.FromArgb(15, 202, 240), Color.Black));
            BootstrapStyles.Add(Styles.Light, ButtonStyle.SolidColor(Color.FromArgb(248, 249, 250), Color.Black));
            BootstrapStyles.Add(Styles.Dark, ButtonStyle.SolidColor(Color.FromArgb(33, 37, 41), Color.White));
            BootstrapStyles.Add(Styles.Cancel, ButtonStyle.SolidColor(Color.LightGray, Color.FromArgb(14, 109, 253)));
            BootstrapStyles.Add(Styles.Outlined_Primary, ButtonStyle.OutlinedColor(Color.FromArgb(14, 109, 253), Color.White));
            BootstrapStyles.Add(Styles.Outlined_Secondary, ButtonStyle.OutlinedColor(Color.FromArgb(108, 117, 125), Color.White));
            BootstrapStyles.Add(Styles.Outlined_Success, ButtonStyle.OutlinedColor(Color.FromArgb(25, 135, 83), Color.White));
            BootstrapStyles.Add(Styles.Outlined_Danger, ButtonStyle.OutlinedColor(Color.FromArgb(221, 52, 68), Color.White));
            BootstrapStyles.Add(Styles.Outlined_Warning, ButtonStyle.OutlinedColor(Color.FromArgb(255, 193, 7), Color.Black));
            BootstrapStyles.Add(Styles.Outlined_Info, ButtonStyle.OutlinedColor(Color.FromArgb(15, 202, 240), Color.Black));
            BootstrapStyles.Add(Styles.Outlined_Light, ButtonStyle.OutlinedColor(Color.FromArgb(248, 249, 250), Color.Black));
            BootstrapStyles.Add(Styles.Outlined_Dark, ButtonStyle.OutlinedColor(Color.FromArgb(33, 37, 41), Color.White));
            BootstrapStyles.Add(Styles.Outlined_Cancel, ButtonStyle.OutlinedColor(Color.LightGray, Color.FromArgb(14, 109, 253)));
        }

        [Category("Bootstrap")]
        [Description("Bootstrap style of the button")]
        public Styles Style
        {
            get => style;
            set
            {
                style = value;
                ApplyStyle();
            }
        }

        [Category("Bootstrap")]
        [Description("True if the button act as a toggle")]
        public bool IsToggle { get; set; }

        [Category("Bootstrap")]
        [Description("True if the toggle button is pushed")]
        public bool IsPushed 
        { 
            get => isPushed;
            set
            {
                isPushed = value;

                if (value)
                {
                    Status = ButtonStatuses.ButtonPushed;

                    // Look for all button within the same parent and adjust their status
                    if (Parent == null) return;
                    foreach (var item in Parent.Controls)
                    {
                        Button b = item as Button;
                        if ((b != null) && (b != this) && (b.IsToggle) && (b.IsPushed))
                        {
                            b.IsPushed = false;
                        }
                    }
                }
                else
                {
                    Status = ButtonStatuses.MouseOutside;
                }
            } 
        }

        /// <summary>
        /// Button status
        /// </summary>
        private ButtonStatuses Status
        {
            get => status;
            set
            {
                if (Enabled)
                    status = value;
                else
                    status = ButtonStatuses.Disabled;
                ApplyStyle();
            }
        }

        /// <summary>
        /// Apply styles to the control
        /// </summary>
        private void ApplyStyle()
        {
            if (Style == Styles.None) return;
            if (isSuspended) return;

            isSuspended = true;
            switch (Status)
            {
                case ButtonStatuses.MouseOutside:
                    BackColor = BootstrapStyles[Style].BackColor;
                    ForeColor = BootstrapStyles[Style].ForeColor;
                    BorderColor = BootstrapStyles[Style].BorderColor;
                    BorderSize = BootstrapStyles[Style].BorderSize;
                    break;

                case ButtonStatuses.MouseHover:
                    BackColor = BootstrapStyles[Style].MouseHoverBackColor;
                    ForeColor = BootstrapStyles[Style].MouseHoverForeColor;
                    BorderColor = BootstrapStyles[Style].MouseHoverBorderColor;
                    BorderSize = BootstrapStyles[Style].MouseHoverBorderSize;
                    break;

                case ButtonStatuses.ButtonPushed:
                    BackColor = BootstrapStyles[Style].MouseDownBackColor;
                    ForeColor = BootstrapStyles[Style].MouseDownForeColor;
                    BorderColor = BootstrapStyles[Style].MouseDownBorderColor;
                    BorderSize = BootstrapStyles[Style].MouseDownBorderSize;
                    break;

                case ButtonStatuses.Disabled:
                    BackColor = BootstrapStyles[Style].DisabledBackColor;
                    ForeColor = BootstrapStyles[Style].DisabledForeColor;
                    BorderColor = BootstrapStyles[Style].DisabledBorderColor;
                    BorderSize = BootstrapStyles[Style].DisabledBorderSize;
                    break;
            }
            isSuspended = false;
        }

        protected override void OnBackColorChanged(EventArgs e)
        {
            if (!isSuspended) Style = Styles.None;
        }

        protected override void OnForeColorChanged(EventArgs e)
        {
            if (!isSuspended) Style = Styles.None;
        }

        private void Event_MouseEnter(object sender, EventArgs e)
        {
            if (!Enabled) return;
            if (IsToggle && isPushed) return;
            Status = ButtonStatuses.MouseHover; 
        }

        private void Event_MouseLeave(object sender, EventArgs e)
        {
            if (!Enabled) return;
            if (IsToggle && isPushed) return;
            Status = ButtonStatuses.MouseOutside;
        }

        private void Event_MouseDown(object sender, EventArgs e)
        {
            if (!Enabled) return;
            if (IsToggle)
            {
                IsPushed = !IsPushed;
                if (isPushed) Status = ButtonStatuses.ButtonPushed; else Status = ButtonStatuses.MouseHover;
            }
            else
                Status = ButtonStatuses.ButtonPushed;
        }

        private void Event_MouseUp(object sender, EventArgs e)
        {
            if (!Enabled) return;
            if (!IsToggle) Status = ButtonStatuses.MouseHover;
        }

        protected override void OnEnabledChanged(EventArgs e)
        {
            base.OnEnabledChanged(e);
            if (!Enabled)
            {
                Status = ButtonStatuses.Disabled;
                Cursor = DefaultCursor;
            }
            else
            {
                Status = ButtonStatuses.MouseOutside;
                Cursor = Cursors.Hand;
            }
        }

        private static Color ChangeColorBrightness(Color color, float correctionFactor)
        {
            float red = (float)color.R;
            float green = (float)color.G;
            float blue = (float)color.B;

            if (correctionFactor < 0)
            {
                correctionFactor = 1 + correctionFactor;
                red *= correctionFactor;
                green *= correctionFactor;
                blue *= correctionFactor;
            }
            else
            {
                red = (255 - red) * correctionFactor + red;
                green = (255 - green) * correctionFactor + green;
                blue = (255 - blue) * correctionFactor + blue;
            }

            return Color.FromArgb(color.A, (int)red, (int)green, (int)blue);
        }

        #endregion

        #region Size management

        // Internal members
        private SizeStyles sizeStyle = SizeStyles.None;                             // Size style of the button

        /// <summary>
        /// Bootstrap sizes
        /// </summary>
        public enum SizeStyles
        {
            None = 0,
            Regular,
            Large,
            Small
        }

        [Category("Bootstrap")]
        [Description("Size style")]
        public SizeStyles SizeStyle 
        { 
            get => sizeStyle; 
            set
            {
                sizeStyle = value;
                AdjustSize();
                Invalidate();
            }
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            AdjustSize();
        }

        public void AdjustSize()
        {
            if (SizeStyle == SizeStyles.None) return;

            Size size;
            Size letterSize;

            float fRatio = 1f;
            float hRatio = 2f;

            switch (SizeStyle)
            {
                case SizeStyles.Large:
                    fRatio = 1.4f;
                    hRatio = 1.4f;
                    break;
                case SizeStyles.Small:
                    hRatio = 1.5f;
                    break;
            }

            labelText.Font = new Font(Font.FontFamily, Font.Size * fRatio, Font.Style);
            size = TextRenderer.MeasureText(Text, labelText.Font);
            letterSize = TextRenderer.MeasureText("M", labelText.Font);
            Size = new Size(size.Width + letterSize.Width * 2, size.Height + (int)(letterSize.Height * hRatio));

        }

        #endregion

        [Category("Bootstrap")]
        [Description("Text of the button")]
        [Browsable(true), EditorBrowsable(EditorBrowsableState.Always), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible), Bindable(true), DefaultValue(true)]
        public override string Text
        {
            get => base.Text;
            set
            {
                base.Text = value;
                labelText.Text = value;
                AdjustSize();
                Invalidate();
            }
        }

        public Button()
        {
            InitializeComponent();
            DefineBootstrapColorStyle();
            Style = Styles.None;
            Status = ButtonStatuses.MouseOutside;
            SizeStyle = SizeStyles.None;
            BorderColor = Color.Transparent;
            Cursor = Cursors.Hand;
            BorderSize = 1;
            Text = Name;
            IsToggle = false;

            MouseEnter += Event_MouseEnter;
            labelText.MouseEnter += Event_MouseEnter;
            MouseLeave += Event_MouseLeave;
            labelText.MouseLeave += Event_MouseLeave;
            MouseDown += Event_MouseDown;
            MouseUp += Event_MouseUp;
            labelText.MouseDown += Event_MouseDown;
            labelText.MouseUp += Event_MouseUp;

        }


    }
}
