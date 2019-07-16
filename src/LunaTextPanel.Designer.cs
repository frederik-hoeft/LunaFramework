namespace LunaForms
{
    partial class LunaTextPanel
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.lunaScrollBar1 = new LunaForms.LunaScrollBar();
            this.panel1.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.flowLayoutPanel1);
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(200, 100);
            this.panel1.TabIndex = 0;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoScroll = true;
            this.flowLayoutPanel1.Controls.Add(this.label1);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(200, 100);
            this.flowLayoutPanel1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.MaximumSize = new System.Drawing.Size(200, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 32);
            this.label1.TabIndex = 0;
            this.label1.Text = "text";
            this.label1.TextChanged += new System.EventHandler(this.label1_TextChanged);
            this.label1.MouseEnter += new System.EventHandler(this.label1_MouseEnter);
            // 
            // lunaScrollBar1
            // 
            this.lunaScrollBar1.BackColorScrollBar = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.lunaScrollBar1.ForeColorHover = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            this.lunaScrollBar1.LargeChange = 10;
            this.lunaScrollBar1.Location = new System.Drawing.Point(180, 0);
            this.lunaScrollBar1.Margin = new System.Windows.Forms.Padding(0);
            this.lunaScrollBar1.Maximum = 100;
            this.lunaScrollBar1.Minimum = 0;
            this.lunaScrollBar1.MouseWheelBarPartitions = 10;
            this.lunaScrollBar1.Name = "lunaScrollBar1";
            this.lunaScrollBar1.Orientation = LunaForms.LunaScrollBar.LunaScrollOrientation.Vertical;
            this.lunaScrollBar1.ScrollbarSize = 20;
            this.lunaScrollBar1.Size = new System.Drawing.Size(20, 100);
            this.lunaScrollBar1.TabIndex = 1;
            this.lunaScrollBar1.Text = "lunaScrollBar1";
            this.lunaScrollBar1.UseSelectable = true;
            this.lunaScrollBar1.Scroll += new System.Windows.Forms.ScrollEventHandler(this.lunaScrollBar1_Scroll);
            // 
            // LunaTextPanel
            // 
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.lunaScrollBar1);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "LunaTextPanel";
            this.Size = new System.Drawing.Size(200, 100);
            this.SizeChanged += new System.EventHandler(this.LunaFlowLayoutPanel_SizeChanged);
            this.panel1.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private LunaScrollBar lunaScrollBar1;
        private System.Windows.Forms.Label label1;
    }
}
