using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CustomMetroForms
{
    public partial class AdvancedButton : UserControl
    {
        public event EventHandler<EventArgs> OnClickEvent;
        public AdvancedButton()
        {
            InitializeComponent();
        }

        private Image NormalImage;
        private Image HoverImage;
        private Color NormalColor;
        private Color HoverColor;
        private String NormalText;
        private String HoverText;
        private Font NormalFont;
        private Font HoverFont;

        public ContentAlignment TextAlign
        {
            get { return label.TextAlign; }
            set { label.TextAlign = value; }
        }

        public Image ImageNormal
        {
            get { return NormalImage; }
            set { NormalImage = value; this.pictureBox.Image = value; }
        }

        public Image ImageHover
        {
            get { return HoverImage; }
            set { HoverImage = value; }
        }

        public Color ColorNormal
        {
            get { return NormalColor; }
            set { NormalColor = value; this.label.ForeColor = value; }
        }

        public Color ColorHover
        {
            get { return HoverColor; }
            set { HoverColor = value; }
        }

        public String TextNormal
        {
            get { return NormalText; }
            set { NormalText = value; this.label.Text = value; }
        }

        public String TextHover
        {
            get { return HoverText; }
            set { HoverText = value; }
        }

        public Font FontNormal
        {
            get { return NormalFont; }
            set { NormalFont = value; this.label.Font = value; }
        }

        public Font FontHover
        {
            get { return HoverFont; }
            set { HoverFont = value; }
        }

        private void AdvancedButton_MouseLeave(object sender, EventArgs e)
        {
            EventMouseLeave();
        }

        private void AdvancedButton_MouseEnter(object sender, EventArgs e)
        {
            EventMouseEnter();
        }

        private void pictureBox_MouseEnter(object sender, EventArgs e)
        {
            EventMouseEnter();
        }

        private void pictureBox_MouseLeave(object sender, EventArgs e)
        {
            EventMouseLeave();
        }

        private void label_MouseEnter(object sender, EventArgs e)
        {
            EventMouseEnter();
        }

        private void label_MouseLeave(object sender, EventArgs e)
        {
            EventMouseLeave();
        }
        private void tableLayoutPanel_MouseEnter(object sender, EventArgs e)
        {
            EventMouseEnter();
        }

        private void tableLayoutPanel_MouseLeave(object sender, EventArgs e)
        {
            EventMouseLeave();
        }

        private void EventMouseEnter()
        {
            this.label.ForeColor = HoverColor;
            this.label.Text = HoverText;
            this.label.Font = HoverFont;
            this.pictureBox.Image = HoverImage;
        }

        private void EventMouseLeave()
        {
            this.label.ForeColor = NormalColor;
            this.label.Text = NormalText;
            this.label.Font = NormalFont;
            this.pictureBox.Image = NormalImage;
        }

        private void label_Click(object sender, EventArgs e)
        {
            OnClickEvent(this, e);
        }

        private void pictureBox_Click(object sender, EventArgs e)
        {
            OnClickEvent(this, e);
        }

        private void tableLayoutPanel_Click(object sender, EventArgs e)
        {
            OnClickEvent(this, e);
        }

        private void AdvancedButton_Click(object sender, EventArgs e)
        {
            OnClickEvent(this, e);
        }
    }
}
