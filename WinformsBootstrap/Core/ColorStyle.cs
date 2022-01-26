using System;
using System.Collections.Generic;
using System.Drawing;

namespace GUILibrary.Controls
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
        Cancel,
        Custom
    }

    public class ColorStyle
    {
        private Styles style = Styles.Primary;
        private int customStyleIndex = 0;

        public Color BackColor { get; private set; }
        public Color ForeColor { get; private set; }
        public Styles Style 
        { 
            get => style;
            set
            {
                // if (value == style) return;
                style = value;
                if (value != Styles.Custom)
                {
                    BackColor = BootStrapColorStyles[(int)style].BackColor;
                    ForeColor = BootStrapColorStyles[(int)style].ForeColor;
                    OnChanged?.Invoke(this, EventArgs.Empty);
                }
                else
                {
                    if (CustomStyleIndex < CustomColorStyles.Count)
                    {
                        BackColor = CustomColorStyles[(int)CustomStyleIndex].BackColor;
                        ForeColor = CustomColorStyles[(int)CustomStyleIndex].ForeColor;
                        OnChanged?.Invoke(this, EventArgs.Empty);
                    }
                }

            }
        } 
        public int CustomStyleIndex
        {
            get => customStyleIndex;
            set
            {
                // if (value == customStyleIndex) return;
                customStyleIndex = value;
                if (customStyleIndex < CustomColorStyles.Count)
                {
                    BackColor = CustomColorStyles[(int)customStyleIndex].BackColor;
                    ForeColor = CustomColorStyles[(int)customStyleIndex].ForeColor;
                    OnChanged?.Invoke(this, EventArgs.Empty);
                }
            }
        } 

        private static List<ColorStyle> BootStrapColorStyles { get; set; }
        public static List<ColorStyle> CustomColorStyles { get; set; }

        public event EventHandler OnChanged;

        static ColorStyle()
        {
            BootStrapColorStyles = new List<ColorStyle>();
            CustomColorStyles = new List<ColorStyle>();
            BootStrapColorStyles.Add(new ColorStyle() { BackColor = Color.FromArgb(14, 109, 253), ForeColor = Color.White });
            BootStrapColorStyles.Add(new ColorStyle() { BackColor = Color.FromArgb(108, 117, 125), ForeColor = Color.White });
            BootStrapColorStyles.Add(new ColorStyle() { BackColor = Color.FromArgb(25, 135, 83), ForeColor = Color.White });
            BootStrapColorStyles.Add(new ColorStyle() { BackColor = Color.FromArgb(221, 52, 68), ForeColor = Color.White });
            BootStrapColorStyles.Add(new ColorStyle() { BackColor = Color.FromArgb(255, 193, 7), ForeColor = Color.Black });
            BootStrapColorStyles.Add(new ColorStyle() { BackColor = Color.FromArgb(15, 202, 240), ForeColor = Color.Black });
            BootStrapColorStyles.Add(new ColorStyle() { BackColor = Color.FromArgb(248, 249, 250), ForeColor = Color.Black });
            BootStrapColorStyles.Add(new ColorStyle() { BackColor = Color.FromArgb(33, 37, 41), ForeColor = Color.White });
            BootStrapColorStyles.Add(new ColorStyle() { BackColor = Color.LightGray, ForeColor = Color.FromArgb(14, 109, 253) });
        }

    }
}
