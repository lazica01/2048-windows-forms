namespace _2048
{
    partial class Form2048
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // Form2048
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Name = "Form2048";
            this.Text = "Form2048";
            this.Load += new System.EventHandler(this.Form2048_Load);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Form2048_MouseUp);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Form2048_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Form2048_MouseMove);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form2048_KeyDown);
            this.ResumeLayout(false);

        }

        #endregion

    }
}

