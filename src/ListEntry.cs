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

namespace CustomMetroForms
{
    public partial class ListEntry : UserControl
    {
        public event EventHandler<EventArgs> OnClickEvent;
        private int ID = -1;
        private Color HoverColor;
        private Color NormalColor;

        private void OnEnterEvent()
        {
            this.BackColor = HoverColor;
        }

        private void OnLeaveEvent()
        {
            this.BackColor = NormalColor;
        }

        public Color ColorHover
        {
            get { return HoverColor; }
            set { HoverColor = value; }
        }

        public Color ColorNormal
        {
            get { return NormalColor; }
            set { NormalColor = value; }
        }

        public Color BackgroundColor
        {
            get { return this.BackColor; }
            set { this.BackColor = value; }
        }

        public int id
        {
            get { return ID; }
            set { ID = value; }
        }

        public ListEntry() 
        {
            InitializeComponent();
        }
        
        public Image FavIcon
        {
            get { return pictureBox1.Image; }
            set { pictureBox1.Image = value; }
        }

        public String HostName
        {
            get { return label1.Text; }
            set { label1.Text = value; }
        }

        public String UserName
        {
            get { return label2.Text; }
            set { label2.Text = value; }
        }
        public String TimeStamp
        {
            get { return label3.Text; }
            set { label3.Text = value; }
        }

        public Font HostNameFont
        {
            get { return label1.Font; }
            set { label1.Font = value; }
        }
        public Font UserNameFont
        {
            get { return label2.Font; }
            set { label2.Font = value; }
        }
        public Font TimeStampFont
        {
            get { return label3.Font; }
            set { label3.Font = value; }
        }

        public Color HostNameForeColor
        {
            get { return label1.ForeColor; }
            set { label1.ForeColor = value; }
        }
        public Color UserNameForeColor
        {
            get { return label1.ForeColor; }
            set { label2.ForeColor = value; }
        }
        public Color TimeStampForeColor
        {
            get { return label1.ForeColor; }
            set { label3.ForeColor = value; }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            OnClickEvent(this, e);
        }

        private void label1_Click(object sender, EventArgs e)
        {
            OnClickEvent(this, e);
        }

        private void label2_Click(object sender, EventArgs e)
        {
            OnClickEvent(this, e);
        }

        private void panel1_Click(object sender, EventArgs e)
        {
            OnClickEvent(this, e);
        }

        private void tableLayoutPanel1_Click(object sender, EventArgs e)
        {
            OnClickEvent(this, e);
        }

        private void label3_Click(object sender, EventArgs e)
        {
            OnClickEvent(this, e);
        }

        private void ListEntry_Click(object sender, EventArgs e)
        {
            OnClickEvent(this, e);
        }

        private void ListEntry_MouseEnter(object sender, EventArgs e)
        {
            OnEnterEvent();
        }

        private void panel1_MouseEnter(object sender, EventArgs e)
        {
            OnEnterEvent();
        }

        private void pictureBox1_MouseEnter(object sender, EventArgs e)
        {
            OnEnterEvent();
        }

        private void tableLayoutPanel1_MouseEnter(object sender, EventArgs e)
        {
            OnEnterEvent();
        }

        private void label1_MouseEnter(object sender, EventArgs e)
        {
            OnEnterEvent();
        }

        private void label2_MouseEnter(object sender, EventArgs e)
        {
            OnEnterEvent();
        }

        private void label3_MouseEnter(object sender, EventArgs e)
        {
            OnEnterEvent();
        }

        private void ListEntry_MouseLeave(object sender, EventArgs e)
        {
            OnLeaveEvent();
        }

        private void panel1_MouseLeave(object sender, EventArgs e)
        {
            OnLeaveEvent();
        }

        private void pictureBox1_MouseLeave(object sender, EventArgs e)
        {
            OnLeaveEvent();
        }

        private void tableLayoutPanel1_MouseLeave(object sender, EventArgs e)
        {
            OnLeaveEvent();
        }

        private void label1_MouseLeave(object sender, EventArgs e)
        {
            OnLeaveEvent();
        }

        private void label2_MouseLeave(object sender, EventArgs e)
        {
            OnLeaveEvent();
        }

        private void label3_MouseLeave(object sender, EventArgs e)
        {
            OnLeaveEvent();
        }

        
        //-------------------------------------------------------------------
        /*GraphicsPath GetRoundPath(RectangleF Rect, int radius)
        {
            float r2 = radius / 2f;
            GraphicsPath GraphPath = new GraphicsPath();

            GraphPath.AddArc(Rect.X, Rect.Y, radius, radius, 180, 90);
            GraphPath.AddLine(Rect.X + r2, Rect.Y, Rect.Width - r2, Rect.Y);
            GraphPath.AddArc(Rect.X + Rect.Width - radius, Rect.Y, radius, radius, 270, 90);
            GraphPath.AddLine(Rect.Width, Rect.Y + r2, Rect.Width, Rect.Height - r2);
            GraphPath.AddArc(Rect.X + Rect.Width - radius,
                             Rect.Y + Rect.Height - radius, radius, radius, 0, 90);
            GraphPath.AddLine(Rect.Width - r2, Rect.Height, Rect.X + r2, Rect.Height);
            GraphPath.AddArc(Rect.X, Rect.Y + Rect.Height - radius, radius, radius, 90, 90);
            GraphPath.AddLine(Rect.X, Rect.Height - r2, Rect.X, Rect.Y + r2);

            GraphPath.CloseFigure();
            return GraphPath;
        }

        public void RePaint()
        {
            this.Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            RectangleF Rect = new RectangleF(0, 0, this.Width, this.Height);
            GraphicsPath GraphPath = GetRoundPath(Rect, 10);

            this.Region = new Region(GraphPath);
            using (Pen pen = new Pen(Color.CadetBlue, 1.75f))
            {
                pen.Alignment = PenAlignment.Inset;
                e.Graphics.DrawPath(pen, GraphPath);
            }
        }
        */
    }
}
