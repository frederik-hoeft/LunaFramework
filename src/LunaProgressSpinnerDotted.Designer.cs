﻿namespace LunaForms
{
    partial class LunaProgressSpinnerDotted
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
            this.SuspendLayout();
            // 
            // DottedProgressSpinner
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "DottedProgressSpinner";
            this.Size = new System.Drawing.Size(100, 100);
            this.SizeChanged += new System.EventHandler(this.DottedProgressSpinner_SizeChanged);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.DottedProgressSpinner_Paint);
            this.ResumeLayout(false);

        }

        #endregion
    }
}
