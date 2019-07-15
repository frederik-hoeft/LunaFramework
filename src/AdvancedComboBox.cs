using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
namespace LunaForms
{
    public class AdvancedComboBox : ComboBox
    {
        private const int OCM_COMMAND = 8465;

        private const int WM_PAINT = 15;

        private bool displayFocusRectangle;

        private bool isHovered;

        private bool isPressed;

        private bool isFocused;
        private Font _Font = new Font("Century Gothic", 8F);
        private Color _ForeColor = Color.Black;
        private Color _BackColor = Color.White;
        private Color _NormalForeColor = Color.FromArgb(153, 153, 153);
        private Color _HoverForeColor = Color.FromArgb(51, 51, 51);
        private Color _PressForeColor = Color.FromArgb(153, 153, 153);
        private Color _DisabledForeColor = Color.FromArgb(204, 204, 204);
        private Color _NormalBorderColor = Color.FromArgb(153, 153, 153);
        private Color _HoverBorderColor = Color.FromArgb(51, 51, 51);
        private Color _PressBorderColor = Color.FromArgb(153, 153, 153);
        private Color _DisabledBorderColor = Color.FromArgb(204, 204, 204);
        private Color _HoverItemColor = Color.FromArgb(255, 96, 49);
        private Color _HoverItemForeColor = Color.White;
        private Color _NormalItemForeColor = Color.Black;

        public Font ItemFont
        {
            get { return _Font; }
            set { _Font = value; this.Font = value; }
        }
        [Browsable(false)]
        public override Color ForeColor
        {
            get { return _ForeColor; }
            set { _ForeColor = value; }
        }
        public Color BackgroundColor
        {
            get { return _BackColor; }
            set { _BackColor = value; }
        }
        public Color NormalItemForeColor
        {
            get { return _NormalItemForeColor; }
            set { _NormalItemForeColor = value; }
        }
        public Color HoverItemForeColor
        {
            get { return _HoverItemForeColor; }
            set { _HoverItemForeColor = value; }
        }
        public Color NormalForeColor
        {
            get { return _NormalForeColor; }
            set { _NormalForeColor = value; }
        }
        public Color HoverForeColor
        {
            get { return _HoverForeColor; }
            set { _HoverForeColor = value; }
        }
        public Color PressForeColor
        {
            get { return _PressForeColor; }
            set { _PressForeColor = value; }
        }
        public Color DisabledForeColor
        {
            get { return _DisabledForeColor; }
            set { _DisabledForeColor = value; }
        }
        public Color NormalBorderColor
        {
            get { return _NormalBorderColor; }
            set { _NormalBorderColor = value; }
        }
        public Color HoverBorderColor
        {
            get { return _HoverBorderColor; }
            set { _HoverBorderColor = value; }
        }
        public Color PressBorderColor
        {
            get { return _PressBorderColor; }
            set { _PressBorderColor = value; }
        }
        public Color DisabledBorderColor
        {
            get { return _DisabledBorderColor; }
            set { _DisabledBorderColor = value; }
        }
        public Color HoverItemColor
        {
            get { return _HoverItemColor; }
            set { _HoverItemColor = value; }
        }

        [DefaultValue(false)]
        [Browsable(false)]
        [Category("Metro Behaviour")]
        public bool UseSelectable
        {
            get
            {
                return base.GetStyle(ControlStyles.Selectable);
            }
            set
            {
                base.SetStyle(ControlStyles.Selectable, value);
            }
        }

        [Category("Metro Appearance")]
        [DefaultValue(false)]
        public bool DisplayFocus
        {
            get
            {
                return displayFocusRectangle;
            }
            set
            {
                displayFocusRectangle = value;
            }
        }

        [Browsable(false)]
        [DefaultValue(DrawMode.OwnerDrawFixed)]
        public new DrawMode DrawMode
        {
            get
            {
                return DrawMode.OwnerDrawFixed;
            }
            set
            {
                base.DrawMode = DrawMode.OwnerDrawFixed;
            }
        }

        [DefaultValue(ComboBoxStyle.DropDownList)]
        [Browsable(false)]
        public new ComboBoxStyle DropDownStyle
        {
            get
            {
                return ComboBoxStyle.DropDownList;
            }
            set
            {
                base.DropDownStyle = ComboBoxStyle.DropDownList;
            }
        }
        [Category("Metro Appearance")]
        public event EventHandler<MetroPaintEventArgs> CustomPaintBackground;

        [Category("Metro Appearance")]
        public event EventHandler<MetroPaintEventArgs> CustomPaint;

        [Category("Metro Appearance")]
        public event EventHandler<MetroPaintEventArgs> CustomPaintForeground;

        protected virtual void OnCustomPaintBackground(MetroPaintEventArgs e)
        {
            if (base.GetStyle(ControlStyles.UserPaint) && this.CustomPaintBackground != null)
            {
                this.CustomPaintBackground(this, e);
            }
        }

        protected virtual void OnCustomPaint(MetroPaintEventArgs e)
        {
            if (base.GetStyle(ControlStyles.UserPaint) && this.CustomPaint != null)
            {
                this.CustomPaint(this, e);
            }
        }

        protected virtual void OnCustomPaintForeground(MetroPaintEventArgs e)
        {
            if (base.GetStyle(ControlStyles.UserPaint) && this.CustomPaintForeground != null)
            {
                this.CustomPaintForeground(this, e);
            }
        }

        public AdvancedComboBox()
        {
            base.SetStyle(ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.SupportsTransparentBackColor | ControlStyles.OptimizedDoubleBuffer, true);
            base.DrawMode = DrawMode.OwnerDrawFixed;
            base.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            try
            {
                Color color = BackColor;
                if (color.A == 255 && BackgroundImage == null)
                {
                    e.Graphics.Clear(color);
                }
                else
                {
                    base.OnPaintBackground(e);
                    OnCustomPaintBackground(new MetroPaintEventArgs(color, Color.Empty, e.Graphics));
                }
            }
            catch
            {
                base.Invalidate();
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            try
            {
                if (base.GetStyle(ControlStyles.AllPaintingInWmPaint))
                {
                    OnPaintBackground(e);
                }
                OnCustomPaint(new MetroPaintEventArgs(Color.Empty, Color.Empty, e.Graphics));
                OnPaintForeground(e);
            }
            catch
            {
                base.Invalidate();
            }
        }

        protected virtual void OnPaintForeground(PaintEventArgs e)
        {
            base.ItemHeight = GetPreferredSize(Size.Empty).Height;
            Color color;
            Color color2;
            if (isHovered && !isPressed && base.Enabled)
            {
                color = _HoverForeColor;
                color2 = _HoverBorderColor;
            }
            else if (isHovered && isPressed && base.Enabled)
            {
                color = _PressForeColor;
                color2 = _PressBorderColor;
            }
            else if (!base.Enabled)
            {
                color = _DisabledForeColor;
                color2 = _DisabledBorderColor;
            }
            else
            {
                color = _NormalForeColor;
                color2 = _NormalBorderColor;
            }
            using (Pen pen = new Pen(color2))
            {
                Rectangle rect = new Rectangle(0, 0, base.Width - 1, base.Height - 1);
                e.Graphics.DrawRectangle(pen, rect);
            }
            using (SolidBrush brush = new SolidBrush(color))
            {
                e.Graphics.FillPolygon(brush, new Point[3]
                {
                new Point(base.Width - 20, base.Height / 2 - 2),
                new Point(base.Width - 9, base.Height / 2 - 2),
                new Point(base.Width - 15, base.Height / 2 + 4)
                });
            }
            Rectangle bounds = new Rectangle(2, 2, base.Width - 20, base.Height - 4);
            TextRenderer.DrawText(e.Graphics, Text, _Font, bounds, color, TextFormatFlags.VerticalCenter);
            OnCustomPaintForeground(new MetroPaintEventArgs(Color.Empty, color, e.Graphics));
            if (displayFocusRectangle && isFocused)
            {
                ControlPaint.DrawFocusRectangle(e.Graphics, base.ClientRectangle);
            }
        }

        protected override void OnDrawItem(DrawItemEventArgs e)
        {
            if (e.Index >= 0)
            {
                Color color = BackColor;
                Color foreColor;
                if (e.State == (DrawItemState.NoAccelerator | DrawItemState.NoFocusRect) || e.State == DrawItemState.None)
                {
                    using (SolidBrush brush = new SolidBrush(color))
                    {
                        e.Graphics.FillRectangle(brush, new Rectangle(e.Bounds.Left, e.Bounds.Top, e.Bounds.Width, e.Bounds.Height));
                    }
                    foreColor = _NormalItemForeColor;
                }
                else
                {
                    using (SolidBrush brush2 = new SolidBrush(_HoverItemColor))
                    {
                        e.Graphics.FillRectangle(brush2, new Rectangle(e.Bounds.Left, e.Bounds.Top, e.Bounds.Width, e.Bounds.Height));
                    }
                    foreColor = _HoverItemForeColor;
                }
                Rectangle bounds = new Rectangle(0, e.Bounds.Top, e.Bounds.Width, e.Bounds.Height);
                TextRenderer.DrawText(e.Graphics, base.GetItemText(base.Items[e.Index]), this.Font, bounds, foreColor, TextFormatFlags.VerticalCenter);
            }
            else
            {
                base.OnDrawItem(e);
            }
        }

        protected override void OnGotFocus(EventArgs e)
        {
            isFocused = true;
            isHovered = true;
            base.Invalidate();
            base.OnGotFocus(e);
        }

        protected override void OnLostFocus(EventArgs e)
        {
            isFocused = false;
            isHovered = false;
            isPressed = false;
            base.Invalidate();
            base.OnLostFocus(e);
        }

        protected override void OnEnter(EventArgs e)
        {
            isFocused = true;
            isHovered = true;
            base.Invalidate();
            base.OnEnter(e);
        }

        protected override void OnLeave(EventArgs e)
        {
            isFocused = false;
            isHovered = false;
            isPressed = false;
            base.Invalidate();
            base.OnLeave(e);
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
            {
                isHovered = true;
                isPressed = true;
                base.Invalidate();
            }
            base.OnKeyDown(e);
        }

        protected override void OnKeyUp(KeyEventArgs e)
        {
            base.Invalidate();
            base.OnKeyUp(e);
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            isHovered = true;
            base.Invalidate();
            base.OnMouseEnter(e);
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isPressed = true;
                base.Invalidate();
            }
            base.OnMouseDown(e);
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            isPressed = false;
            base.Invalidate();
            base.OnMouseUp(e);
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            if (!isFocused)
            {
                isHovered = false;
            }
            base.Invalidate();
            base.OnMouseLeave(e);
        }

        public override Size GetPreferredSize(Size proposedSize)
        {
            base.GetPreferredSize(proposedSize);
            using (Graphics dc = base.CreateGraphics())
            {
                string text = (Text.Length > 0) ? Text : "MeasureText";
                proposedSize = new Size(2147483647, 2147483647);
                Size result = TextRenderer.MeasureText(dc, text, _Font, proposedSize, TextFormatFlags.VerticalCenter | TextFormatFlags.LeftAndRightPadding);
                result.Height += 4;
                return result;
            }
        }

        protected override void OnSelectedIndexChanged(EventArgs e)
        {
            base.OnSelectedIndexChanged(e);
            base.Invalidate();
        }

        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);
            if (m.Msg != 15 && m.Msg != 8465)
            {
                return;
            }
        }
        public class MetroPaintEventArgs : EventArgs
        {
            public Color BackColor { get; private set; }
            public Color ForeColor { get; private set; }
            public Graphics Graphics { get; private set; }

            public MetroPaintEventArgs(Color backColor, Color foreColor, Graphics g)
            {
                BackColor = backColor;
                ForeColor = foreColor;
                Graphics = g;
            }
        }
    }
}