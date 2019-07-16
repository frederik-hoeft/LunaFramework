namespace LunaForms
{
    partial class LunaItemList
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lunaScrollBar1 = new LunaForms.LunaScrollBar();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoScroll = true;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(406, 612);
            this.flowLayoutPanel1.TabIndex = 0;
            this.flowLayoutPanel1.Scroll += new System.Windows.Forms.ScrollEventHandler(this.flowLayoutPanel1_Scroll);
            this.flowLayoutPanel1.ControlAdded += new System.Windows.Forms.ControlEventHandler(this.flowLayoutPanel1_ControlAdded);
            this.flowLayoutPanel1.ControlRemoved += new System.Windows.Forms.ControlEventHandler(this.flowLayoutPanel1_ControlRemoved);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.flowLayoutPanel1);
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(406, 612);
            this.panel1.TabIndex = 1;
            // 
            // lunaScrollBar1
            // 
            this.lunaScrollBar1.BackColorScrollBar = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.lunaScrollBar1.ForeColorHover = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            this.lunaScrollBar1.LargeChange = 10;
            this.lunaScrollBar1.Location = new System.Drawing.Point(386, 0);
            this.lunaScrollBar1.Margin = new System.Windows.Forms.Padding(0);
            this.lunaScrollBar1.Maximum = 100;
            this.lunaScrollBar1.Minimum = 0;
            this.lunaScrollBar1.MouseWheelBarPartitions = 10;
            this.lunaScrollBar1.Name = "lunaScrollBar1";
            this.lunaScrollBar1.Orientation = LunaForms.LunaScrollBar.LunaScrollOrientation.Vertical;
            this.lunaScrollBar1.ScrollbarSize = 20;
            this.lunaScrollBar1.Size = new System.Drawing.Size(20, 612);
            this.lunaScrollBar1.TabIndex = 2;
            this.lunaScrollBar1.Text = "lunaScrollBar1";
            this.lunaScrollBar1.UseSelectable = true;
            this.lunaScrollBar1.Scroll += new System.Windows.Forms.ScrollEventHandler(this.lunaScrollBar1_Scroll);
            // 
            // LunaItemList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.lunaScrollBar1);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "LunaItemList";
            this.Size = new System.Drawing.Size(406, 612);
            this.SizeChanged += new System.EventHandler(this.LunaItemList_SizeChanged);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Panel panel1;
        private LunaScrollBar lunaScrollBar1;
    }
}
