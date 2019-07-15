using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LunaForms
{
    public partial class LunaTriangle : UserControl
    {
        /// <summary>
        /// The direction in which the triangle points.
        /// </summary>
        public enum Direction
        {
            /// <summary>
            /// Top
            /// </summary>
            Top,
            /// <summary>
            /// Right
            /// </summary>
            Right,
            /// <summary>
            /// Left
            /// </summary>
            Left,
            /// <summary>
            /// Down
            /// </summary>
            Down
        }
        private Color _foreColor = Color.FromArgb(144, 197, 100);
        private bool _isEquilateral = true;
        private bool _isCentered = false;
        private bool _isSolid = false;
        private Direction _triangleDirection = Direction.Top;
        private PointF a, b, c;

        /// <summary>
        /// Creates a simple triangle.
        /// </summary>
        public LunaTriangle()
        {
            InitializeComponent();
        }
        #region Getters / Setters
        /// <summary>
        /// The color the triangle is drawn in.
        /// </summary>
        public override Color ForeColor
        {
            get { return _foreColor; }
            set
            {
                base.ForeColor = value;
                _foreColor = value;
                Refresh();
            }
        }

        /// <summary>
        /// Specifies whether the triangle is equilateral.
        /// </summary>
        [DefaultValue(true)]
        public bool IsEquilateral
        {
            get { return _isEquilateral; }
            set
            {
                _isEquilateral = value;
                Refresh();
            }
        }

        /// <summary>
        /// Specifies whether the center of gravity of the triangle should be the same as the center of the control.
        /// </summary>
        [DefaultValue(false)]
        public bool IsCentered
        {
            get { return _isCentered; }
            set
            {
                _isCentered = value;
                Refresh();
            }
        }

        /// <summary>
        /// Specifies whether the triangle should be filled.
        /// </summary>
        [DefaultValue(false)]
        public bool IsSolid
        {
            get { return _isSolid; }
            set
            {
                _isSolid = value;
                Refresh();
            }
        }

        /// <summary>
        /// Specifies the direction the triangle is pointing in.
        /// </summary>
        public Direction TriangleDirection
        {
            get { return _triangleDirection; }
            set
            {
                _triangleDirection = value;
                Refresh();
            }
        }
        #endregion
        private void LunaTriangle_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            switch (_triangleDirection)
            {
                case Direction.Top:
                {
                    if (_isEquilateral)
                    {
                        a = new PointF(0, Height - 1);
                        b = new PointF(Width - 1, Height - 1);
                        c = new PointF(Width / 2f, Height - (float)Math.Sqrt(0.75f * Math.Pow(Width, 2f)));
                    }
                    else
                    {
                        a = new PointF(0, Height - 1);
                        b = new PointF(Width - 1, Height - 1);
                        c = new PointF(Width / 2f, 0);
                    }
                    break;
                }
                case Direction.Right:
                {
                    if (_isEquilateral)
                    {
                        a = new PointF(0, 0);
                        b = new PointF(0, Height);
                        c = new PointF((float)Math.Sqrt(0.75f * Math.Pow(Height, 2f)), Height / 2f);
                    }
                    else
                    {
                        a = new PointF(0, 0);
                        b = new PointF(0, Height - 1);
                        c = new PointF(Width - 1, Height / 2f);
                    }
                    break;
                }
                case Direction.Down:
                {
                    if (_isEquilateral)
                    {
                        a = new PointF(0, 0);
                        b = new PointF(Width - 1, 0);
                        c = new PointF(Width / 2f, (float)Math.Sqrt(0.75f * Math.Pow(Width, 2f)));
                    }
                    else
                    {
                        a = new PointF(0, 0);
                        b = new PointF(Width - 1, 0);
                        c = new PointF(Width / 2f, Height - 1);
                    }
                    break;
                }
                case Direction.Left:
                {
                    if (_isEquilateral)
                    {
                        a = new PointF(Width - 1, 0);
                        b = new PointF(Width - 1, Height - 1);
                        c = new PointF(Width - (float)Math.Sqrt(0.75f * Math.Pow(Height, 2f)), Height / 2f);
                    }
                    else
                    {
                        a = new PointF(Width - 1, 0);
                        b = new PointF(Width - 1, Height - 1);
                        c = new PointF(0, Height / 2f);
                    }
                    break;
                }
            }
            PointF[] points = new PointF[] { a, b, c };
            if (_isCentered && _isEquilateral)
            {
                float x = (a.X + b.X + c.X) / 3f;
                float y = (a.Y + b.Y + c.Y) / 3f;
                float xOffset = (Width / 2f) - x;
                float yOffset = (Height / 2f) - y;
                for (int i = 0; i < points.Length; i++)
                {
                    points[i] = new PointF(points[i].X + xOffset, points[i].Y + yOffset);
                }
            }
            if (_isSolid)
            {
                Brush brush = new SolidBrush(_foreColor);
                g.FillPolygon(brush, points);
            }
            else
            {
                Pen pen = new Pen(_foreColor);
                g.DrawPolygon(pen, points);
            }
        }
    }
}
