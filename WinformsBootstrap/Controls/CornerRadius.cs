using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMax.WinformsBootstrap.Controls
{
    /// <summary>
    /// Define border radius for bootstrap controls
    /// </summary>
    public class CornerRadius
    {
        /// <summary>
        /// Top-left corner border radius
        /// </summary>
        public int TopLeft { get; set; }

        /// <summary>
        /// Top-right corner border radius
        /// </summary>
        public int TopRight { get; set; }

        /// <summary>
        /// Bottom-right corner border radius
        /// </summary>
        public int BottomRight { get; set; }

        /// <summary>
        /// Bottom-left corner border radius
        /// </summary>
        public int BottomLeft { get; set; }

        /// <summary>
        /// Set/Get all corners radius equals, return -1 if radius are different
        /// </summary>
        public int All
        {
            get
            {
                if ((TopLeft == TopRight) && (TopRight == BottomRight) && (BottomRight == BottomLeft))
                    return TopLeft;
                else
                    return -1;
            }

            set
            {
                TopLeft = value;
                TopRight = value;
                BottomRight = value;
                BottomLeft = value;
            }
        }

        /// <summary>
        /// Set/Get all bottom corners radius equals, return -1 if radius are different
        /// </summary>
        [Browsable(false)]
        public int BottomCorners
        {
            get
            {
                if (BottomLeft == BottomRight)
                    return BottomLeft;
                else
                    return -1;
            }

            set
            {
                BottomLeft = value;
                BottomRight = value;
            }
        }

        /// <summary>
        /// Set/Get all top corners radius equals, return -1 if radius are different
        /// </summary>
        [Browsable(false)]
        public int TopCorners
        {
            get
            {
                if (TopLeft == TopRight)
                    return TopLeft;
                else
                    return -1;
            }

            set
            {
                TopLeft = value;
                TopRight = value;
            }
        }

        /// <summary>
        /// Set/Get all right corners radius equals, return -1 if radius are different
        /// </summary>
        [Browsable(false)]
        public int RightCorners
        {
            get
            {
                if (BottomRight == TopRight)
                    return BottomRight;
                else
                    return -1;
            }

            set
            {
                BottomRight = value;
                TopRight = value;
            }
        }

        /// <summary>
        /// Set/Get all left corners radius equals, return -1 if radius are different
        /// </summary>
        [Browsable(false)]
        public int LeftCorners
        {
            get
            {
                if (BottomLeft == TopLeft)
                    return BottomLeft;
                else
                    return -1;
            }

            set
            {
                BottomLeft = value;
                TopLeft = value;
            }
        }

    }

}
