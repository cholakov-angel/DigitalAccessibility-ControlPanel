
namespace Easy_mode_Desktop
{
    partial class SettingsConfirm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingsConfirm));
            this.command_txt = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // command_txt
            // 
            this.command_txt.Location = new System.Drawing.Point(13, 13);
            this.command_txt.Name = "command_txt";
            this.command_txt.Size = new System.Drawing.Size(286, 20);
            this.command_txt.TabIndex = 0;
            this.command_txt.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox1_KeyDown);
            // 
            // SettingsConfirm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(311, 66);
            this.Controls.Add(this.command_txt);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SettingsConfirm";
            this.Text = "Потвърждаване на отварянето на настройки";
            this.Load += new System.EventHandler(this.SettingsConfirm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox command_txt;
    }
}