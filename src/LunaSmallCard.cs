using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace LunaForms
{
    /// <summary>
    /// A small info card containing an image, a header text and an optional info text.
    /// </summary>
    public partial class LunaSmallCard : UserControl
    {
        private string _header = "LunaSmallCard";
        private string _info = "Info";
        private Font _font = new Font("Segoe UI", 25, GraphicsUnit.Pixel);
        private int _infoFontSizePx = 15;
        private Color _foreColorHeader = Color.Orange;
        private Color _foreColorHeaderHover = Color.Orange;
        private Color _foreColorInfo = Color.FromArgb(100, 100, 100);
        private Color _foreColorInfoHover = Color.FromArgb(64, 64, 64);
        private Color _backColorHover = Color.FromArgb(220, 220, 220);
        private Color _backColor = Color.White;
        private Color normalColor = Color.White;
        private Color foreColorHeader = Color.Orange;
        private Color foreColorInfo = Color.FromArgb(100, 100, 100);
        private Point _headerLocation = new Point(70, 0);
        private Point _infoLocation = new Point(72, 35);
        private bool _showInfo = false;
        private int _steps = 20;
        private int _animationInterval = 10;
        private Timer animationTimer = new Timer();
        private bool timerRunning = false;
        private bool _showBorder = true;
        private bool hasFocus = false;
        private int astep, rstep, gstep, bstep;
        

        /// <summary>
        /// Occurs when the component is clicked. Should be used instead of LunaSmallCard.Click
        /// </summary>
        public event EventHandler OnClickEvent;

        /// <summary>
        /// A small info card containing an image, a header text and an optional info text.
        /// </summary>
        public LunaSmallCard()
        {
            InitializeComponent();
            base.BorderStyle = System.Windows.Forms.BorderStyle.None;
            pictureBox1.BackColor = _foreColorHeader;
            animationTimer.Tick += new EventHandler(AnimationTimer_Tick);
            animationTimer.Interval = _animationInterval;
            _headerLocation = new Point(70, 10);
            Refresh();
        }

        #region Getters / Setters
        /// <summary>
        /// The location of the top left corner of the image.
        /// </summary>
        public Point ImageLocation
        {
            get { return pictureBox1.Location; }
            set
            {
                pictureBox1.Location = value;
            }
        }

        /// <summary>
        /// Defines whether a border should be drawn around the control.
        /// </summary>
        [DefaultValue(true)]
        public bool ShowBorder
        {
            get { return _showBorder; }
            set
            {
                _showBorder = value;
                Refresh();
            }
        }
        /// <summary>
        /// DEPRECATED. Use ShowBorder instead.
        /// </summary>
        [Browsable(false)]
        public new BorderStyle BorderStyle
        {
            get { return base.BorderStyle; }
            set
            {
                base.BorderStyle = BorderStyle.None;
                Refresh();
            }
        }
        /// <summary>
        /// The main text to be displayed.
        /// </summary>
        public string Header
        {
            get { return _header; }
            set
            {
                _header = value;
                Refresh();
            }
        }

        /// <summary>
        /// Additional information to be displayed.
        /// </summary>
        [DefaultValue("Info")]
        public string Info
        {
            get { return _info; }
            set
            {
                _info = value;
                Refresh();
            }
        }

        /// <summary>
        /// The font top be used to draw both Header and Info.
        /// </summary>
        public override Font Font
        {
            get { return _font; }
            set
            {
                base.Font = value;
                _font = value;
                Refresh();
            }
        }

        /// <summary>
        /// The font size in pixels of the info text.
        /// </summary>
        [DefaultValue(15)]
        public int InfoFontSizePx
        {
            get { return _infoFontSizePx; }
            set { _infoFontSizePx = value; }
        }

        /// <summary>
        /// The foreground color of the header text.
        /// </summary>
        public Color ForeColorHeader
        {
            get { return _foreColorHeader; }
            set
            {
                _foreColorHeader = value;
                foreColorHeader = value;
                Refresh();
            }
        }

        /// <summary>
        /// The foreground color of the info text.
        /// </summary>
        public Color ForeColorInfo
        {
            get { return _foreColorInfo; }
            set
            {
                _foreColorInfo = value;
                foreColorInfo = value;
                Refresh();
            }
        }

        /// <summary>
        /// The foreground color of the header text when the user hovers over it.
        /// </summary>
        public Color ForeColorHeaderHover
        {
            get { return _foreColorHeaderHover; }
            set
            {
                _foreColorHeaderHover = value;
                Refresh();
            }
        }

        /// <summary>
        /// The foreground color of the info text when the user hovers over it.
        /// </summary>
        public Color ForeColorInfoHover
        {
            get { return _foreColorInfoHover; }
            set
            {
                _foreColorInfoHover = value;
                Refresh();
            }
        }

        /// <summary>
        /// The background color of the LunaSmallCard.
        /// </summary>
        public Color BackColorNormal
        {
            get { return _backColor; }
            set
            {
                _backColor = value;
                normalColor = value;
            }
        }

        /// <summary>
        /// The background color of the LunaSmallCard when the user hovers over it.
        /// </summary>
        public Color BackColorHover
        {
            get { return _backColorHover; }
            set { _backColorHover = value; }
        }

        /// <summary>
        /// The background color of the PictureBox.
        /// </summary>
        public Color BackColorImage
        {
            get { return pictureBox1.BackColor; }
            set { pictureBox1.BackColor = value; }
        }

        /// <summary>
        /// The image shown in the LunaSmallCard.
        /// </summary>
        public Image Image
        {
            get { return pictureBox1.Image; }
            set { pictureBox1.Image = value; }
        }

        /// <summary>
        /// The location of the top left corner of the header text.
        /// </summary>
        public Point HeaderLocation
        {
            get { return _headerLocation; }
            set
            {
                _headerLocation = value;
                Refresh();
            }
        }

        /// <summary>
        /// The location of the top left corner of the info text.
        /// </summary>
        public Point InfoLocation
        {
            get { return _infoLocation; }
            set
            {
                _infoLocation = value;
                Refresh();
            }
        }

        /// <summary>
        /// Controls whether the info text is drawn or not.
        /// </summary>
        [DefaultValue(false)]
        public bool ShowInfo
        {
            get { return _showInfo; }
            set
            {
                _showInfo = value;
                if (_showInfo)
                {
                    _headerLocation = new Point(70, 0);
                    _infoLocation = new Point(72, 35);
                }
                else
                {
                    _headerLocation = new Point(70, 10);
                }
                Refresh();
            }
        }

        /// <summary>
        /// Controls how smooth the color transition is from the BackColor to BackColorHover. More _steps means smoother transition.
        /// </summary>
        [DefaultValue(20)]
        public int AnimationSteps
        {
            get { return _steps; }
            set { _steps = value; }
        }

        /// <summary>
        /// The interval in ms in which the Animations goes through it's animation steps. The total animation duration is AnimationInterval * AnimationSteps.
        /// </summary>
        [DefaultValue(10)]
        public int AnimationInterval
        {
            get { return _animationInterval; }
            set { _animationInterval = value; }
        }

        #endregion
        private void LunaSmallCard_Paint(object sender, PaintEventArgs e)
        {
            if (_showBorder)
            {
                ControlPaint.DrawBorder(e.Graphics, ClientRectangle, Color.Black, ButtonBorderStyle.Solid);
            }
            Graphics g = e.Graphics;
            g.DrawString(_header, _font, new SolidBrush(foreColorHeader), _headerLocation);
            if (_showInfo)
            {
                g.DrawString(_info, new Font(_font.FontFamily, _infoFontSizePx, GraphicsUnit.Pixel), new SolidBrush(foreColorInfo), _infoLocation);
            }
        }

        public virtual void LunaSmallCard_SizeChanged(object sender, EventArgs e)
        {
            if (Height != 60)
            {
                Height = 60;
            }
        }
        #region Click event
        private void LunaSmallCard_Click(object sender, EventArgs e)
        {
            ClickEvent(e);
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            ClickEvent(e);
        }

        protected virtual void ClickEvent(EventArgs e)
        {
            OnClickEvent?.Invoke(this, e);
        }
        #endregion

        #region Hover effects
        private void LunaSmallCard_MouseEnter(object sender, EventArgs e)
        {
            MouseEnterEvent();
        }

        private void LunaSmallCard_MouseLeave(object sender, EventArgs e)
        {
            MouseLeaveEvent();
        }

        private void pictureBox1_MouseEnter(object sender, EventArgs e)
        {
            MouseEnterEvent();
        }

        private void pictureBox1_MouseLeave(object sender, EventArgs e)
        {
            MouseLeaveEvent();
        }

        private void MouseEnterEvent()
        {
            foreColorHeader = _foreColorHeaderHover;
            foreColorInfo = _foreColorInfoHover;
            hasFocus = true;
            astep = Convert.ToInt32(_backColorHover.A - normalColor.A > 0 ? Math.Ceiling((double)(_backColorHover.A - normalColor.A) / (double)_steps) : Math.Floor((double)(_backColorHover.A - normalColor.A) / (double)_steps));
            rstep = Convert.ToInt32(_backColorHover.R - normalColor.R > 0 ? Math.Ceiling((double)(_backColorHover.R - normalColor.R) / (double)_steps) : Math.Floor((double)(_backColorHover.R - normalColor.R) / (double)_steps));
            gstep = Convert.ToInt32(_backColorHover.G - normalColor.G > 0 ? Math.Ceiling((double)(_backColorHover.G - normalColor.G) / (double)_steps) : Math.Floor((double)(_backColorHover.G - normalColor.G) / (double)_steps));
            bstep = Convert.ToInt32(_backColorHover.B - normalColor.B > 0 ? Math.Ceiling((double)(_backColorHover.B - normalColor.B) / (double)_steps) : Math.Floor((double)(_backColorHover.B - normalColor.B) / (double)_steps));
            if (!timerRunning)
            {
                timerRunning = true;
                animationTimer.Start();
            }
        }

        private void MouseLeaveEvent()
        {
            foreColorHeader = _foreColorHeader;
            foreColorInfo = _foreColorInfo;
            hasFocus = false;
            astep = Convert.ToInt32(normalColor.A - _backColorHover.A > 0 ? Math.Ceiling((double)(normalColor.A - _backColorHover.A) / (double)_steps) : Math.Floor((double)(normalColor.A - _backColorHover.A) / (double)_steps));
            rstep = Convert.ToInt32(normalColor.R - _backColorHover.R > 0 ? Math.Ceiling((double)(normalColor.R - _backColorHover.R) / (double)_steps) : Math.Floor((double)(normalColor.R - _backColorHover.R) / (double)_steps));
            gstep = Convert.ToInt32(normalColor.G - _backColorHover.G > 0 ? Math.Ceiling((double)(normalColor.G - _backColorHover.G) / (double)_steps) : Math.Floor((double)(normalColor.G - _backColorHover.G) / (double)_steps));
            bstep = Convert.ToInt32(normalColor.B - _backColorHover.B > 0 ? Math.Ceiling((double)(normalColor.B - _backColorHover.B) / (double)_steps) : Math.Floor((double)(normalColor.B - _backColorHover.B) / (double)_steps));
            if (!timerRunning)
            {
                timerRunning = true;
                animationTimer.Start();
            }
        }

        private void AnimationTimer_Tick(object sender, EventArgs e)
        {
            int A, R, G, B;
            if (hasFocus)
            {
                if (_backColorHover.A > _backColor.A)
                {
                    A = (_backColor.A + astep > _backColorHover.A ? _backColorHover.A : _backColor.A + astep);
                }
                else if (_backColorHover.A < _backColor.A)
                {
                    A = (_backColor.A + astep < _backColorHover.A ? _backColorHover.A : _backColor.A + astep);
                }
                else
                {
                    A = _backColorHover.A;
                }
                if (_backColorHover.R > _backColor.R)
                {
                    R = (_backColor.R + rstep > _backColorHover.R ? _backColorHover.R : _backColor.R + rstep);
                }
                else if (_backColorHover.R < _backColor.R)
                {
                    R = (_backColor.R + rstep < _backColorHover.R ? _backColorHover.R : _backColor.R + rstep);
                }
                else
                {
                    R = _backColorHover.R;
                }
                if (_backColorHover.G > _backColor.G)
                {
                    G = (_backColor.G + gstep > _backColorHover.G ? _backColorHover.G : _backColor.G + gstep);
                }
                else if (_backColorHover.G < _backColor.G)
                {
                    G = (_backColor.G + gstep < _backColorHover.G ? _backColorHover.G : _backColor.G + gstep);
                }
                else
                {
                    G = _backColorHover.G;
                }
                if (_backColorHover.B > _backColor.B)
                {
                    B = (_backColor.B + bstep > _backColorHover.B ? _backColorHover.B : _backColor.B + bstep);
                }
                else if (_backColorHover.B < _backColor.B)
                {
                    B = (_backColor.B + bstep < _backColorHover.B ? _backColorHover.B : _backColor.B + bstep);
                }
                else
                {
                    B = _backColorHover.B;
                }
            }
            else
            {
                if (normalColor.A > _backColor.A)
                {
                    A = (_backColor.A + astep > normalColor.A ? normalColor.A : _backColor.A + astep);
                }
                else if (normalColor.A < _backColor.A)
                {
                    A = (_backColor.A + astep < normalColor.A ? normalColor.A : _backColor.A + astep);
                }
                else
                {
                    A = normalColor.A;
                }
                if (normalColor.R > _backColor.R)
                {
                    R = _backColor.R + rstep > normalColor.R ? normalColor.R : _backColor.R + rstep;
                }
                else if (normalColor.R < _backColor.R)
                {
                    R = _backColor.R + rstep < normalColor.R ? normalColor.R : _backColor.R + rstep;
                }
                else
                {
                    R = normalColor.R;
                }
                if (normalColor.G > _backColor.G)
                {
                    G = _backColor.G + gstep > normalColor.G ? normalColor.G : _backColor.G + gstep;
                }
                else if (normalColor.G < _backColor.G)
                {
                    G = _backColor.G + gstep < normalColor.G ? normalColor.G : _backColor.G + gstep;
                }
                else
                {
                    G = normalColor.G;
                }
                if (normalColor.B > _backColor.B)
                {
                    B = _backColor.B + bstep > normalColor.B ? normalColor.B : _backColor.B + bstep;
                }
                else if (normalColor.B < _backColor.B)
                {
                    B = _backColor.B + bstep < normalColor.B ? normalColor.B : _backColor.B + bstep;
                }
                else
                {
                    B = normalColor.B;
                }
            }
            _backColor = Color.FromArgb(R, G, B);
            BackColor = _backColor;
            pictureBox1.BackColor = _backColor;
            if (hasFocus)
            {
                if (_backColor.Equals(_backColorHover))
                {
                    animationTimer.Stop();
                    timerRunning = false;
                }
            }
            else
            {
                if (_backColor.Equals(normalColor))
                {
                    animationTimer.Stop();
                    timerRunning = false;
                }
            }
            Refresh();
        }
        #endregion
    }
}
