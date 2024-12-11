namespace Easy_mode_Desktop.Forms.StartUp_system.Setup
{
    partial class LicenseEnter
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
            this.label1 = new System.Windows.Forms.Label();
            this.licenseKeyTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.masterKeyTextBox = new System.Windows.Forms.TextBox();
            this.closeButton = new System.Windows.Forms.Button();
            this.activateButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AccessibleDescription = "Завършихте успешно първоначалната настройка";
            this.label1.AccessibleName = "Завършихте успешно първоначалната настройка";
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Century Gothic", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(173, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(417, 33);
            this.label1.TabIndex = 7;
            this.label1.Text = "Моля, въведете Вашия лиценз";
            // 
            // licenseKeyTextBox
            // 
            this.licenseKeyTextBox.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.licenseKeyTextBox.Location = new System.Drawing.Point(132, 119);
            this.licenseKeyTextBox.Name = "licenseKeyTextBox";
            this.licenseKeyTextBox.Size = new System.Drawing.Size(656, 27);
            this.licenseKeyTextBox.TabIndex = 8;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(32, 122);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(94, 19);
            this.label2.TabIndex = 9;
            this.label2.Text = "Лиц. ключ:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(13, 167);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(113, 19);
            this.label3.TabIndex = 11;
            this.label3.Text = "Админ ключ:";
            // 
            // masterKeyTextBox
            // 
            this.masterKeyTextBox.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.masterKeyTextBox.Location = new System.Drawing.Point(132, 164);
            this.masterKeyTextBox.Name = "masterKeyTextBox";
            this.masterKeyTextBox.Size = new System.Drawing.Size(656, 27);
            this.masterKeyTextBox.TabIndex = 10;
            // 
            // closeButton
            // 
            this.closeButton.AccessibleDescription = "Бутон за отказване на процедурата за първоначална настройка и затваряне на \"Дигит" +
    "ална достъпност\"";
            this.closeButton.AccessibleName = "Отказ";
            this.closeButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.closeButton.Font = new System.Drawing.Font("Century Gothic", 9.75F);
            this.closeButton.Location = new System.Drawing.Point(580, 410);
            this.closeButton.Name = "closeButton";
            this.closeButton.Size = new System.Drawing.Size(101, 28);
            this.closeButton.TabIndex = 13;
            this.closeButton.Text = "Отказ";
            this.closeButton.UseVisualStyleBackColor = true;
            this.closeButton.Click += new System.EventHandler(this.CloseButton_Click);
            // 
            // activateButton
            // 
            this.activateButton.AccessibleDescription = "Бутон, чрез който се дава начало на проца на конфигурация на \"Дигитална достъпнос" +
    "т\"";
            this.activateButton.AccessibleName = "Започване на начална настройка на \"Дигитална достъпност\"";
            this.activateButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.activateButton.Font = new System.Drawing.Font("Century Gothic", 9.75F);
            this.activateButton.Location = new System.Drawing.Point(687, 410);
            this.activateButton.Name = "activateButton";
            this.activateButton.Size = new System.Drawing.Size(101, 28);
            this.activateButton.TabIndex = 12;
            this.activateButton.Text = "Активиране";
            this.activateButton.UseVisualStyleBackColor = true;
            this.activateButton.Click += new System.EventHandler(this.activateButton_Click);
            // 
            // LicenseEnter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.closeButton);
            this.Controls.Add(this.activateButton);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.masterKeyTextBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.licenseKeyTextBox);
            this.Controls.Add(this.label1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "LicenseEnter";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "LicenseEnter";
            this.Load += new System.EventHandler(this.LicenseEnter_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox licenseKeyTextBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox masterKeyTextBox;
        private System.Windows.Forms.Button closeButton;
        private System.Windows.Forms.Button activateButton;
    }
}