using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Windows.Forms.VisualStyles;

namespace CustomMetroForms
{
    public partial class AdvancedRichTextBox : UserControl
    {
        [DllImport("user32.dll")]
        static extern bool CreateCaret(IntPtr hWnd, IntPtr hBitmap, int nWidth, int nHeight);
        [DllImport("user32.dll")]
        static extern bool ShowCaret(IntPtr hWnd);
        float SizeAnimation = 0;
        float PointAnimation;
        float SizeInc_Dec;
        float PointInc_Dec;
        private Color NormalColor = Color.FromArgb(33, 33, 33);
        private Color FocusColor = Color.FromArgb(255, 96, 49);
        private Color NormalForeColor = Color.FromArgb(33, 33, 33);
        private Color FocusForeColor = Color.Black;
        private Boolean IsFocused = false;
        Timer AnimationTimer = new Timer { Interval = 1 };
        VisualStyleRenderer renderer = new VisualStyleRenderer(VisualStyleElement.Button.PushButton.Normal);
        public AdvancedRichTextBox()
        {
            InitializeComponent();
            this.richTextBox1.GotFocus += OnFocus;
            this.richTextBox1.LostFocus += OnFocusLost;
            AnimationTimer.Tick += new EventHandler(AnimationTick);
            SizeInc_Dec = Width / 18;
            PointInc_Dec = Width / 36;
        }

        public Color ForeColorNormal
        {
            get { return NormalForeColor; }
            set { NormalForeColor = value; richTextBox1.ForeColor = value; }
        }

        public Color ForeColorFocus
        {
            get { return FocusForeColor; }
            set { FocusForeColor = value; }
        }

        public String TextValue
        {
            get { return richTextBox1.Text; }
            set { richTextBox1.Text = value; }
        }

        public Color ColorNormal
        {
            get { return NormalColor; }
            set { NormalColor = value; this.Invalidate(); }
        }

        public Color ColorFocus
        {
            get { return FocusColor; }
            set { FocusColor = value; }
        }

        public override Font Font
        {
            get { return richTextBox1.Font; }
            set { richTextBox1.Font = value; }
        }

        private void OnFocus(object sender, EventArgs e)
        {
            IsFocused = true;
            this.Invalidate();
            Bitmap bmp = new Bitmap(1, 1);
            bmp.SetPixel(0, 0, ColorExtensions.GetContrast(FocusColor, true));
            bmp = new Bitmap(bmp, 2, richTextBox1.Height);
            CreateCaret(richTextBox1.Handle, bmp.GetHbitmap(FocusColor), 2, richTextBox1.Height);
            ShowCaret(richTextBox1.Handle);
            AnimationTimer.Start();
            richTextBox1.ForeColor = FocusForeColor;
        }

        private void OnFocusLost(object sender, EventArgs e)
        {
            IsFocused = false;
            AnimationTimer.Start();
            richTextBox1.ForeColor = NormalForeColor;
            this.Invalidate();
        }

        private void AnimationTick(object sender, EventArgs e)
        {
            if (IsFocused)
            {
                if (SizeAnimation < Width)
                {
                    SizeAnimation += SizeInc_Dec;
                    this.Invalidate();
                }

                if (PointAnimation > 0)
                {
                    PointAnimation -= PointInc_Dec;
                    this.Invalidate();
                }
            }
            else
            {
                if (SizeAnimation > 0)
                {
                    SizeAnimation -= SizeInc_Dec;
                    this.Invalidate();
                }

                if (PointAnimation < Width / 2)
                {
                    PointAnimation += PointInc_Dec;
                    this.Invalidate();
                }
            }
        }

        private void AdvancedRichTextBox_Paint(object sender, PaintEventArgs e)
        {
            Bitmap B = new Bitmap(Width, Height);
            Graphics G = Graphics.FromImage(B);
            G.Clear(Color.Transparent);

            G.DrawLine(new Pen(new SolidBrush(NormalColor)), new Point(0, Height - 2), new Point(Width, Height - 2));
            if (this.Enabled)
            {
                G.FillRectangle(new SolidBrush(ColorFocus), PointAnimation, (float)Height - 3, SizeAnimation, 2);
            }

            e.Graphics.DrawImage((Image)(B.Clone()), 0, 0);
            G.Dispose();
            B.Dispose();
        }

        private void AdvancedRichTextBox_SizeChanged(object sender, EventArgs e)
        {
            richTextBox1.Height = this.Height - 4;
            SizeInc_Dec = Width / 18;
            PointInc_Dec = Width / 36;
            PointAnimation = Width / 2;
        }
    }
}
