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
using System.Drawing.Imaging;

namespace CustomMetroForms
{

    public partial class AdvancedTextBox : UserControl
    {
        [DllImport("user32.dll")]
        static extern bool CreateCaret(IntPtr hWnd, IntPtr hBitmap, int nWidth, int nHeight);
        [DllImport("user32.dll")]
        static extern bool ShowCaret(IntPtr hWnd);
        float SizeAnimation = 0;
        float PointAnimation;
        float SizeInc_Dec;
        float PointInc_Dec;
        private Color NormalColor = Color.FromArgb(255,255,255);
        private Color FocusColor = Color.FromArgb(255, 96, 49);
        private Color NormalForeColor = Color.FromArgb(33, 33, 33);
        private Color FocusForeColor = Color.Black;
        private Color bgColor = Color.White;
        private Boolean IsFocused = false;
        private Boolean UseCaret = true;
        Timer AnimationTimer = new Timer { Interval = 1 };
        private string defaultValue = "Enter some text...";
        private Boolean isEmpty = false;
        private Boolean useDefaultValue = true;
        public event EventHandler TextBoxTextChanged;
        public AdvancedTextBox()
        {
            InitializeComponent();
            this.Height = textBox1.Height + 4;
            this.textBox1.GotFocus += OnFocus;
            this.textBox1.LostFocus += OnFocusLost;
            AnimationTimer.Tick += new EventHandler(AnimationTick);
            SizeInc_Dec = Width / 18;
            PointInc_Dec = Width / 36;
            if (useDefaultValue)
            {
                if (textBox1.Text.Equals(""))
                {
                    isEmpty = true;
                    textBox1.Text = defaultValue;
                }
            }
        }

        #region getters/setters

        public Boolean IsEmpty
        {
            get { return isEmpty; }
        }

        public Boolean UseDefaultValue
        {
            get { return useDefaultValue; }
            set { useDefaultValue = value; }
        }

        public string DefaultValue
        {
            get { return defaultValue; }
            set { defaultValue = value; }
        }

        /*public char PasswordChar
        {
            get { return textBox1.PasswordChar; }
            set { textBox1.PasswordChar = value; }
        }*/

        public Boolean UseSystemPasswordChar
        {
            get { return textBox1.UseSystemPasswordChar; }
            set { textBox1.UseSystemPasswordChar = value; }
        }

        public HorizontalAlignment TextAlign
        {
            get { return textBox1.TextAlign; }
            set { textBox1.TextAlign = value; }
        }

        public Boolean UseColoredCaret
        {
            get { return UseCaret; }
            set { UseCaret = value; }
        }

        public Color BackgroundColor
        {
            get { return bgColor; }
            set { bgColor = value; this.BackColor = value; textBox1.BackColor = value; }
        }

        public Color ForeColorNormal
        {
            get { return NormalForeColor; }
            set { NormalForeColor = value; textBox1.ForeColor = value; }
        }

        public Color ForeColorFocus
        {
            get { return FocusForeColor; }
            set { FocusForeColor = value; }
        }

        public String TextValue
        {
            get { return textBox1.Text; }
            set { textBox1.Text = value; }
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
            get { return textBox1.Font; }
            set { textBox1.Font = value; }
        }

        #endregion
        private void AdvancedTextBox_Paint(object sender, PaintEventArgs e)
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

        private void textBox1_SizeChanged(object sender, EventArgs e)
        {
            this.Height = textBox1.Height + 4;
            SizeInc_Dec = Width / 18;
            PointInc_Dec = Width / 36;
            PointAnimation = Width / 2;
        }

        private void OnFocus(object sender, EventArgs e)
        {
            if (useDefaultValue && isEmpty && textBox1.Text.Equals(defaultValue))
            {
                textBox1.Text = "";
            }
            IsFocused = true;
            this.Invalidate();
            if (UseCaret)
            {
                Bitmap bmp = new Bitmap(1, 1);
                bmp.SetPixel(0, 0, ColorExtensions.GetContrast(FocusColor, true));
                bmp = new Bitmap(bmp, 2, textBox1.Height);
                CreateCaret(textBox1.Handle, bmp.GetHbitmap(FocusColor), 2, textBox1.Height);
                ShowCaret(textBox1.Handle);
            }
            AnimationTimer.Start();
            textBox1.ForeColor = FocusForeColor;
        }

        private void OnFocusLost(object sender, EventArgs e)
        {
            if (useDefaultValue)
            {
                if (textBox1.Text.Equals(""))
                {
                    isEmpty = true;
                    textBox1.Text = defaultValue;
                }
                else
                {
                    isEmpty = false;
                }
            }
            IsFocused = false;
            AnimationTimer.Start();
            textBox1.ForeColor = NormalForeColor;
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

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            OnTextChange(e);
        }
        protected virtual void OnTextChange(EventArgs e)
        {
            TextBoxTextChanged?.Invoke(this, e);
        }
    }
}
