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
    public partial class WindowButton : UserControl
    {
        private Image NormalImage;
        private Image HoverImage;
        private Color NormalColor;
        private Color HoverColor;
        public event EventHandler<EventArgs> OnClickEvent;
        public WindowButton()
        {
            InitializeComponent();
        }

        public Image ImageNormal
        {
            get { return NormalImage; }
            set { NormalImage = value; pictureBox.Image = value; }
        }

        public Image ImageHover
        {
            get { return HoverImage; }
            set { HoverImage = value; }
        }

        public Color BackgroundColorNormal
        {
            get { return NormalColor; }
            set { NormalColor = value; this.BackColor = value; }
        }

        public Color BackgroundColorHover
        {
            get { return HoverColor; }
            set { HoverColor = value; }
        }

        private void MouseEnterEvent()
        {
            pictureBox.Image = HoverImage;
            this.BackColor = HoverColor;
        }

        private void MouseLeaveEvent()
        {
            pictureBox.Image = NormalImage;
            this.BackColor = NormalColor;
        }

        private void WindowButton_MouseEnter(object sender, EventArgs e)
        {
            MouseEnterEvent();
        }

        private void WindowButton_MouseLeave(object sender, EventArgs e)
        {
            MouseLeaveEvent();
        }

        private void WindowButton_Click(object sender, EventArgs e)
        {
            OnClickEvent(this, e);
        }

        private void tableLayoutPanel_MouseEnter(object sender, EventArgs e)
        {
            MouseEnterEvent();
        }

        private void tableLayoutPanel_MouseLeave(object sender, EventArgs e)
        {
            MouseLeaveEvent();
        }

        private void tableLayoutPanel_Click(object sender, EventArgs e)
        {
            OnClickEvent(this, e);
        }

        private void pictureBox_MouseEnter(object sender, EventArgs e)
        {
            MouseEnterEvent();
        }

        private void pictureBox_MouseLeave(object sender, EventArgs e)
        {
            MouseLeaveEvent();
        }

        private void pictureBox_Click(object sender, EventArgs e)
        {
            OnClickEvent(this, e);
        }
    }
}
