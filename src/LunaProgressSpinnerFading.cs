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
    public partial class LunaProgressSpinnerFading : UserControl
    {
        private Timer timer = new Timer();
        private int x;
        private int colorIndex = 0;
        private readonly int steps = 9;
        private Color nextColor = Color.FromArgb(255, 0, 0);
        private Color previousColor = Color.FromArgb(255, 0, 0);
        private Color foreColor;
        private int astep, rstep, gstep, bstep;
        private bool useSingleColor = false;
        private bool isForcedResizeEvent = false;
        private bool timerRunning = false;
        private int padding = 5;
        private const double radianCoeffitient = Math.PI / 180;
        private int n = 16;
        private double minimumDotAlpha = 0.5d;
        private int[] alphas = new int[16];
        private double angle;
        private float R, r, centerXY;
        private float dotSize = 0.75f;
        private int index = 0;
        private int modifyAlpha = 0;
        private bool modifyColor = true;
        private int animationSlowDownCoefficient = 8;
        float radius;
        // COLOR CIRCLE
        private Color[] colors = new Color[]
        {
                Color.FromArgb(255, 0, 0),
                Color.FromArgb(255, 42, 0),
                Color.FromArgb(255, 84, 0),
                Color.FromArgb(255, 126, 0),
                Color.FromArgb(255, 168, 0),
                Color.FromArgb(255, 210, 0),
                Color.FromArgb(255, 255, 0),
                Color.FromArgb(210, 255, 0),
                Color.FromArgb(168, 255, 0),
                Color.FromArgb(126, 255, 0),
                Color.FromArgb(84, 255, 0),
                Color.FromArgb(42, 255, 0),
                Color.FromArgb(0, 255, 0),
                Color.FromArgb(0, 255, 42),
                Color.FromArgb(0, 255, 84),
                Color.FromArgb(0, 255, 126),
                Color.FromArgb(0, 255, 168),
                Color.FromArgb(0, 255, 210),
                Color.FromArgb(0, 255, 255),
                Color.FromArgb(0, 210, 255),
                Color.FromArgb(0, 168, 255),
                Color.FromArgb(0, 126, 255),
                Color.FromArgb(0, 84, 255),
                Color.FromArgb(0, 42, 255),
                Color.FromArgb(0, 0, 255),
                Color.FromArgb(42, 0, 255),
                Color.FromArgb(84, 0, 255),
                Color.FromArgb(126, 0, 255),
                Color.FromArgb(168, 0, 255),
                Color.FromArgb(210, 0, 255),
                Color.FromArgb(255, 0, 255),
                Color.FromArgb(255, 0, 210),
                Color.FromArgb(255, 0, 168),
                Color.FromArgb(255, 0, 126),
                Color.FromArgb(255, 0, 84),
                Color.FromArgb(255, 0, 42)
        };
        public LunaProgressSpinnerFading()
        {
            InitializeComponent();
            timer.Interval = 10;
            timer.Tick += Timer_Tick;
            Invalidate();
        }
        #region Getters / Setters
        /// <summary>
        /// Used to set the animation speed. The higher the number the slower the animation. Valid values: integer > 0
        /// </summary>
        [DefaultValue(8)]
        public int AnimationSlowDownCoefficient
        {
            get { return animationSlowDownCoefficient; }
            set { animationSlowDownCoefficient = value; }
        }
        /// <summary>
        /// The size of the dots to be displayed from 0 to 1. Default: 0.75.
        /// </summary>
        [DefaultValue(0.75f)]
        public float DotSizeMultiplier
        {
            get { return dotSize; }
            set { dotSize = value; }
        }

        /// <summary>
        /// The number of dots to be displayed.
        /// </summary>
        [DefaultValue(16)]
        public int NumberOfDots
        {
            get { return n; }
            set
            {
                n = value;
                alphas = new int[n];
            }
        }

        /// <summary>
        /// Should the set ForeColor be used to draw the dots.
        /// </summary>
        [DefaultValue(false)]
        public bool UseForeColor
        {
            get { return useSingleColor; }
            set { useSingleColor = value; }
        }

        /// <summary>
        /// The color that is used to draw the dots.
        /// </summary>
        public override Color ForeColor
        {
            get => base.ForeColor;
            set
            {
                base.ForeColor = value;
                foreColor = value;
            }
        }

        /// <summary>
        /// The minimum radius of the dots relativ to their maximum radius.
        /// </summary>
        [DefaultValue(0.5d)]
        public double MinimumDotAlpha
        {
            get { return minimumDotAlpha; }
            set { minimumDotAlpha = value; }
        }
        #endregion
        /// <summary>
        /// Starts the loading animation.
        /// </summary>
        public void Start()
        {
            x = Math.Max(Width, Height);
            angle = 360d / (double)n;
            R = (x - (2 * padding)) / (float)(2f + Math.Sin(angle * radianCoeffitient));
            r = R * (float)(Math.Sin(angle * radianCoeffitient) / 2f);
            radius = r * dotSize;
            centerXY = R + r + padding;
            for (int i = 0; i < n; i++)
            {
                alphas[i] = 128;
            }
            timerRunning = true;
            if (!useSingleColor)
            {
                foreColor = Color.FromArgb(255, 0, 0);
            }
            timer.Start();
        }
        /// <summary>
        /// Stops the loading animation.
        /// </summary>
        public void Stop()
        {
            timer.Stop();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (!useSingleColor)
            {
                if (modifyColor)
                {
                    if (foreColor.Equals(nextColor))
                    {
                        colorIndex = colorIndex < colors.Length - 1 ? colorIndex + 1 : 0;
                        previousColor = nextColor;
                        nextColor = colors[colorIndex];
                        astep = Convert.ToInt32(nextColor.A - previousColor.A > 0 ? Math.Ceiling((double)(nextColor.A - previousColor.A) / (double)steps) : Math.Floor((double)(nextColor.A - previousColor.A) / (double)steps));
                        rstep = Convert.ToInt32(nextColor.R - previousColor.R > 0 ? Math.Ceiling((double)(nextColor.R - previousColor.R) / (double)steps) : Math.Floor((double)(nextColor.R - previousColor.R) / (double)steps));
                        gstep = Convert.ToInt32(nextColor.G - previousColor.G > 0 ? Math.Ceiling((double)(nextColor.G - previousColor.G) / (double)steps) : Math.Floor((double)(nextColor.G - previousColor.G) / (double)steps));
                        bstep = Convert.ToInt32(nextColor.B - previousColor.B > 0 ? Math.Ceiling((double)(nextColor.B - previousColor.B) / (double)steps) : Math.Floor((double)(nextColor.B - previousColor.B) / (double)steps));
                    }
                    int A, R, G, B;
                    if (nextColor.A > foreColor.A)
                    {
                        A = (foreColor.A + astep > nextColor.A ? nextColor.A : foreColor.A + astep);
                    }
                    else if (nextColor.A < foreColor.A)
                    {
                        A = (foreColor.A + astep < nextColor.A ? nextColor.A : foreColor.A + astep);
                    }
                    else
                    {
                        A = nextColor.A;
                    }
                    if (nextColor.R > foreColor.R)
                    {
                        R = (foreColor.R + rstep > nextColor.R ? nextColor.R : foreColor.R + rstep);
                    }
                    else if (nextColor.R < foreColor.R)
                    {
                        R = (foreColor.R + rstep < nextColor.R ? nextColor.R : foreColor.R + rstep);
                    }
                    else
                    {
                        R = nextColor.R;
                    }
                    if (nextColor.G > foreColor.G)
                    {
                        G = (foreColor.G + gstep > nextColor.G ? nextColor.G : foreColor.G + gstep);
                    }
                    else if (nextColor.G < foreColor.G)
                    {
                        G = (foreColor.G + gstep < nextColor.G ? nextColor.G : foreColor.G + gstep);
                    }
                    else
                    {
                        G = nextColor.G;
                    }
                    if (nextColor.B > foreColor.B)
                    {
                        B = (foreColor.B + bstep > nextColor.B ? nextColor.B : foreColor.B + bstep);
                    }
                    else if (nextColor.B < foreColor.B)
                    {
                        B = (foreColor.B + bstep < nextColor.B ? nextColor.B : foreColor.B + bstep);
                    }
                    else
                    {
                        B = nextColor.B;
                    }
                    foreColor = Color.FromArgb(R, G, B);
                    modifyColor = false;
                }
                else
                {
                    modifyColor = true;
                }
            }
            if (modifyAlpha == 0)
            {
                for (int i = 0; i < n; i++)
                {
                    int alphaIndex = index + i < n ? index + i : index + i - n;
                    alphas[alphaIndex] = 255 - (int)((double)i * minimumDotAlpha * 255d / (double)n);
                }
                index = index - 1 > -1 ? index - 1 : n - 1;
            }
            modifyAlpha = modifyAlpha + 1 >= animationSlowDownCoefficient ? 0 : modifyAlpha + 1;
            Invalidate();
        }

        private void LunaProgressSpinnerFading_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            for (int i = 0; i < n; i++)
            {
                float _x = R * (float)Math.Sin(i * angle * radianCoeffitient);
                float _y = R * (float)Math.Cos(i * angle * radianCoeffitient);
                Brush brush = new SolidBrush(timerRunning ? Color.FromArgb(alphas[i], foreColor) : foreColor);
                g.FillEllipse(brush, centerXY + _x - radius, centerXY + _y - radius, 2 * radius, 2 * radius);
            }
        }

        private void LunaProgressSpinnerFading_SizeChanged(object sender, EventArgs e)
        {
            if (isForcedResizeEvent)
            {
                isForcedResizeEvent = false;
                return;
            }
            if (Width != Height)
            {
                x = Math.Max(Width, Height);
                isForcedResizeEvent = true;
                Width = x;
                Height = x;
            }
            Invalidate();
        }
    }
}
