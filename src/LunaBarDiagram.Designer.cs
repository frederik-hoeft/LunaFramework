namespace LunaForms
{
    partial class LunaBarDiagram
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
            // LunaGraph
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.MinimumSize = new System.Drawing.Size(300, 150);
            this.Name = "LunaGraph";
            this.Size = new System.Drawing.Size(472, 274);
            this.SizeChanged += new System.EventHandler(this.LunaGraph_SizeChanged);
            this.Click += new System.EventHandler(this.LunaGraph_Click);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.LunaGraph_Paint);
            this.ResumeLayout(false);

        }

        #endregion
    }
}
