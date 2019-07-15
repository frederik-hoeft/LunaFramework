namespace LunaForms
{
    partial class EditField
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.advancedTextBox1 = new LunaForms.AdvancedTextBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.advancedImageButton1 = new LunaForms.AdvancedImageButton();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.advancedImageButton1, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(476, 111);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.advancedTextBox1);
            this.panel1.Controls.Add(this.textBox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(436, 111);
            this.panel1.TabIndex = 0;
            // 
            // advancedTextBox1
            // 
            this.advancedTextBox1.BackColor = System.Drawing.Color.White;
            this.advancedTextBox1.BackgroundColor = System.Drawing.Color.White;
            this.advancedTextBox1.ColorFocus = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(96)))), ((int)(((byte)(49)))));
            this.advancedTextBox1.ColorNormal = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(33)))), ((int)(((byte)(33)))));
            this.advancedTextBox1.DefaultValue = "Enter some text...";
            this.advancedTextBox1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.advancedTextBox1.ForeColorFocus = System.Drawing.Color.Black;
            this.advancedTextBox1.ForeColorNormal = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(33)))), ((int)(((byte)(33)))));
            this.advancedTextBox1.Location = new System.Drawing.Point(0, 77);
            this.advancedTextBox1.Name = "advancedTextBox1";
            this.advancedTextBox1.Size = new System.Drawing.Size(436, 34);
            this.advancedTextBox1.TabIndex = 1;
            this.advancedTextBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.advancedTextBox1.TextValue = "TextTextBox";
            this.advancedTextBox1.UseColoredCaret = true;
            this.advancedTextBox1.UseDefaultValue = true;
            this.advancedTextBox1.UseSystemPasswordChar = false;
            this.advancedTextBox1.SizeChanged += new System.EventHandler(this.AdvancedTextBox1_SizeChanged);
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.Color.White;
            this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.textBox1.Enabled = false;
            this.textBox1.Font = new System.Drawing.Font("Century Gothic", 8F);
            this.textBox1.Location = new System.Drawing.Point(0, 0);
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(436, 20);
            this.textBox1.TabIndex = 0;
            this.textBox1.Text = "TextTitle";
            this.textBox1.SizeChanged += new System.EventHandler(this.TextBox1_SizeChanged);
            // 
            // advancedImageButton1
            // 
            this.advancedImageButton1.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.advancedImageButton1.ImageHover = null;
            this.advancedImageButton1.ImageNormal = null;
            this.advancedImageButton1.Location = new System.Drawing.Point(436, 75);
            this.advancedImageButton1.Margin = new System.Windows.Forms.Padding(0);
            this.advancedImageButton1.Name = "advancedImageButton1";
            this.advancedImageButton1.Size = new System.Drawing.Size(40, 36);
            this.advancedImageButton1.TabIndex = 1;
            // 
            // EditField
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "EditField";
            this.Size = new System.Drawing.Size(476, 111);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel1;
        private AdvancedTextBox advancedTextBox1;
        private System.Windows.Forms.TextBox textBox1;
        private AdvancedImageButton advancedImageButton1;
    }
}
