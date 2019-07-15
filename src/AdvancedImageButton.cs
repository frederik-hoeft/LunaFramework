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
    public partial class AdvancedImageButton : UserControl
    {
        public event EventHandler<EventArgs> OnClickEvent;
        public AdvancedImageButton()
        {
            InitializeComponent();
        }

        private Image NormalImage;
        private Image HoverImage;

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

        private void AdvancedImageButton_MouseEnter(object sender, EventArgs e)
        {
            pictureBox.Image = HoverImage;
        }

        private void AdvancedImageButton_MouseLeave(object sender, EventArgs e)
        {
            pictureBox.Image = NormalImage;
        }

        private void AdvancedImageButton_Click(object sender, EventArgs e)
        {
            OnClickEvent(this, e);
        }

        private void pictureBox_Click(object sender, EventArgs e)
        {
            OnClickEvent(this, e);
        }

        private void pictureBox_MouseEnter(object sender, EventArgs e)
        {
            pictureBox.Image = HoverImage;
        }

        private void pictureBox_MouseLeave(object sender, EventArgs e)
        {
            pictureBox.Image = NormalImage;
        }
    }
}
