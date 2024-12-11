namespace Easy_mode_Desktop
{
    partial class Emailsend
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Emailsend));
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.label5 = new System.Windows.Forms.Label();
            this.letter_txt = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.subject_txt = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.password_txt = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.to_txt = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.command_txt = new System.Windows.Forms.TextBox();
            this.from_txt = new System.Windows.Forms.TextBox();
            this.command_aftertxt = new System.Windows.Forms.TextBox();
            this.timer2 = new System.Windows.Forms.Timer(this.components);
            this.opt2 = new System.Windows.Forms.TextBox();
            this.opt3 = new System.Windows.Forms.TextBox();
            this.voicelabel = new System.Windows.Forms.Label();
            this.contact_txt = new System.Windows.Forms.TextBox();
            this.filedirectory_txt = new System.Windows.Forms.TextBox();
            this.filename_txt = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(11, 231);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(44, 13);
            this.label5.TabIndex = 21;
            this.label5.Text = "Писмо:";
            // 
            // letter_txt
            // 
            this.letter_txt.Location = new System.Drawing.Point(65, 234);
            this.letter_txt.Name = "letter_txt";
            this.letter_txt.Size = new System.Drawing.Size(526, 20);
            this.letter_txt.TabIndex = 20;
            this.letter_txt.KeyDown += new System.Windows.Forms.KeyEventHandler(this.letter_txt_KeyDown);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(11, 188);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(32, 13);
            this.label4.TabIndex = 19;
            this.label4.Text = "Име:";
            // 
            // subject_txt
            // 
            this.subject_txt.Location = new System.Drawing.Point(65, 181);
            this.subject_txt.Name = "subject_txt";
            this.subject_txt.Size = new System.Drawing.Size(526, 20);
            this.subject_txt.TabIndex = 18;
            this.subject_txt.KeyDown += new System.Windows.Forms.KeyEventHandler(this.subject_txt_KeyDown);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(11, 80);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(48, 13);
            this.label3.TabIndex = 17;
            this.label3.Text = "Парола:";
            // 
            // password_txt
            // 
            this.password_txt.Location = new System.Drawing.Point(65, 73);
            this.password_txt.Name = "password_txt";
            this.password_txt.PasswordChar = '*';
            this.password_txt.Size = new System.Drawing.Size(526, 20);
            this.password_txt.TabIndex = 16;
            this.password_txt.KeyDown += new System.Windows.Forms.KeyEventHandler(this.password_txt_KeyDown);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(11, 133);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(25, 13);
            this.label2.TabIndex = 15;
            this.label2.Text = "До:";
            // 
            // to_txt
            // 
            this.to_txt.Location = new System.Drawing.Point(65, 126);
            this.to_txt.Name = "to_txt";
            this.to_txt.Size = new System.Drawing.Size(526, 20);
            this.to_txt.TabIndex = 14;
            this.to_txt.KeyDown += new System.Windows.Forms.KeyEventHandler(this.to_text_KeyDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 47);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(23, 13);
            this.label1.TabIndex = 13;
            this.label1.Text = "От:";
            // 
            // command_txt
            // 
            this.command_txt.Location = new System.Drawing.Point(65, 12);
            this.command_txt.Name = "command_txt";
            this.command_txt.Size = new System.Drawing.Size(526, 20);
            this.command_txt.TabIndex = 12;
            this.command_txt.KeyDown += new System.Windows.Forms.KeyEventHandler(this.command_txt_KeyDown);
            // 
            // from_txt
            // 
            this.from_txt.Location = new System.Drawing.Point(65, 44);
            this.from_txt.Name = "from_txt";
            this.from_txt.Size = new System.Drawing.Size(526, 20);
            this.from_txt.TabIndex = 22;
            this.from_txt.KeyDown += new System.Windows.Forms.KeyEventHandler(this.from_txt_KeyDown);
            // 
            // command_aftertxt
            // 
            this.command_aftertxt.Location = new System.Drawing.Point(65, 260);
            this.command_aftertxt.Name = "command_aftertxt";
            this.command_aftertxt.Size = new System.Drawing.Size(526, 20);
            this.command_aftertxt.TabIndex = 24;
            this.command_aftertxt.KeyDown += new System.Windows.Forms.KeyEventHandler(this.command_after_txt_KeyDown);
            // 
            // timer2
            // 
            this.timer2.Tick += new System.EventHandler(this.timer2_Tick);
            // 
            // opt2
            // 
            this.opt2.Location = new System.Drawing.Point(65, 155);
            this.opt2.Name = "opt2";
            this.opt2.Size = new System.Drawing.Size(526, 20);
            this.opt2.TabIndex = 25;
            this.opt2.KeyDown += new System.Windows.Forms.KeyEventHandler(this.opt2txt_KeyDown);
            // 
            // opt3
            // 
            this.opt3.Location = new System.Drawing.Point(65, 208);
            this.opt3.Name = "opt3";
            this.opt3.Size = new System.Drawing.Size(526, 20);
            this.opt3.TabIndex = 26;
            this.opt3.KeyDown += new System.Windows.Forms.KeyEventHandler(this.opt3txt_KeyDown);
            // 
            // voicelabel
            // 
            this.voicelabel.AutoSize = true;
            this.voicelabel.Location = new System.Drawing.Point(13, 278);
            this.voicelabel.Name = "voicelabel";
            this.voicelabel.Size = new System.Drawing.Size(35, 13);
            this.voicelabel.TabIndex = 27;
            this.voicelabel.Text = "label6";
            // 
            // contact_txt
            // 
            this.contact_txt.Location = new System.Drawing.Point(65, 100);
            this.contact_txt.Name = "contact_txt";
            this.contact_txt.Size = new System.Drawing.Size(526, 20);
            this.contact_txt.TabIndex = 28;
            this.contact_txt.KeyDown += new System.Windows.Forms.KeyEventHandler(this.contact_txt_KeyDown);
            // 
            // filedirectory_txt
            // 
            this.filedirectory_txt.Location = new System.Drawing.Point(65, 286);
            this.filedirectory_txt.Name = "filedirectory_txt";
            this.filedirectory_txt.Size = new System.Drawing.Size(526, 20);
            this.filedirectory_txt.TabIndex = 29;
            // 
            // filename_txt
            // 
            this.filename_txt.Location = new System.Drawing.Point(65, 313);
            this.filename_txt.Name = "filename_txt";
            this.filename_txt.Size = new System.Drawing.Size(526, 20);
            this.filename_txt.TabIndex = 30;
            this.filename_txt.KeyDown += new System.Windows.Forms.KeyEventHandler(this.filename_txt_KeyDown);
            // 
            // Emailsend
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(619, 330);
            this.Controls.Add(this.filename_txt);
            this.Controls.Add(this.filedirectory_txt);
            this.Controls.Add(this.contact_txt);
            this.Controls.Add(this.voicelabel);
            this.Controls.Add(this.opt3);
            this.Controls.Add(this.opt2);
            this.Controls.Add(this.command_aftertxt);
            this.Controls.Add(this.from_txt);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.letter_txt);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.subject_txt);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.password_txt);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.to_txt);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.command_txt);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Emailsend";
            this.Text = "Имейл (изпращане на съобщение) - Дигитална достъпност";
            this.Load += new System.EventHandler(this.Emailsend_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox letter_txt;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox subject_txt;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox password_txt;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox to_txt;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox command_txt;
        private System.Windows.Forms.TextBox from_txt;
        private System.Windows.Forms.TextBox command_aftertxt;
        private System.Windows.Forms.Timer timer2;
        private System.Windows.Forms.TextBox opt2;
        private System.Windows.Forms.TextBox opt3;
        private System.Windows.Forms.Label voicelabel;
        private System.Windows.Forms.TextBox filedirectory_txt;
        private System.Windows.Forms.TextBox filename_txt;
        private System.Windows.Forms.TextBox contact_txt;
    }
}