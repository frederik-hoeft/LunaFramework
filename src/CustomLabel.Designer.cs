namespace LunaForms
{
    partial class CustomLabel
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
            this.LabelHeader = new System.Windows.Forms.Label();
            this.RichLabel = new System.Windows.Forms.RichTextBox();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 339F));
            this.tableLayoutPanel1.Controls.Add(this.LabelHeader, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.RichLabel, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(339, 207);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // LabelHeader
            // 
            this.LabelHeader.AutoSize = true;
            this.LabelHeader.BackColor = System.Drawing.Color.White;
            this.LabelHeader.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LabelHeader.Font = new System.Drawing.Font("Century Gothic", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LabelHeader.Location = new System.Drawing.Point(3, 0);
            this.LabelHeader.Name = "LabelHeader";
            this.LabelHeader.Size = new System.Drawing.Size(333, 30);
            this.LabelHeader.TabIndex = 0;
            this.LabelHeader.Text = "Header";
            this.LabelHeader.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // RichLabel
            // 
            this.RichLabel.BackColor = System.Drawing.Color.White;
            this.RichLabel.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.RichLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.RichLabel.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RichLabel.Location = new System.Drawing.Point(3, 33);
            this.RichLabel.Name = "RichLabel";
            this.RichLabel.ReadOnly = true;
            this.RichLabel.Size = new System.Drawing.Size(333, 171);
            this.RichLabel.TabIndex = 1;
            this.RichLabel.Text = "Content";
            // 
            // CustomLabel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "CustomLabel";
            this.Size = new System.Drawing.Size(339, 207);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label LabelHeader;
        private System.Windows.Forms.RichTextBox RichLabel;
    }
}
