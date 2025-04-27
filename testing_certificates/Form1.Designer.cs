namespace testing_certificates
{
    partial class Form1
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
            this.CadetListDGV = new System.Windows.Forms.DataGridView();
            this.SearchTXT = new System.Windows.Forms.TextBox();
            this.SearchCB = new System.Windows.Forms.ComboBox();
            this.button2 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label0 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.GiveCertificateBTN = new System.Windows.Forms.Button();
            this.CertificateTypeCB = new System.Windows.Forms.ComboBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.CadetListDGV)).BeginInit();
            this.SuspendLayout();
            // 
            // CadetListDGV
            // 
            this.CadetListDGV.AllowUserToAddRows = false;
            this.CadetListDGV.AllowUserToDeleteRows = false;
            this.CadetListDGV.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.CadetListDGV.Location = new System.Drawing.Point(13, 70);
            this.CadetListDGV.Name = "CadetListDGV";
            this.CadetListDGV.ReadOnly = true;
            this.CadetListDGV.Size = new System.Drawing.Size(794, 480);
            this.CadetListDGV.TabIndex = 0;
            this.CadetListDGV.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.CadetListDGV_CellClick);
            // 
            // SearchTXT
            // 
            this.SearchTXT.Location = new System.Drawing.Point(12, 24);
            this.SearchTXT.Name = "SearchTXT";
            this.SearchTXT.Size = new System.Drawing.Size(570, 20);
            this.SearchTXT.TabIndex = 1;
            this.SearchTXT.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // SearchCB
            // 
            this.SearchCB.FormattingEnabled = true;
            this.SearchCB.Items.AddRange(new object[] {
            "Name",
            "Student No",
            "Section",
            "Academic Year",
            "Rank",
            "Platoon",
            "Battalion"});
            this.SearchCB.Location = new System.Drawing.Point(589, 22);
            this.SearchCB.Name = "SearchCB";
            this.SearchCB.Size = new System.Drawing.Size(121, 21);
            this.SearchCB.TabIndex = 2;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(716, 20);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 4;
            this.button2.Text = "Search";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(825, 86);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(54, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Full Name";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(825, 99);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(32, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Email";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(825, 112);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(21, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "SY";
            // 
            // label0
            // 
            this.label0.AutoSize = true;
            this.label0.Location = new System.Drawing.Point(825, 73);
            this.label0.Name = "label0";
            this.label0.Size = new System.Drawing.Size(61, 13);
            this.label0.TabIndex = 8;
            this.label0.Text = "Student No";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(825, 125);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(32, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "Class";
            // 
            // GiveCertificateBTN
            // 
            this.GiveCertificateBTN.Location = new System.Drawing.Point(828, 239);
            this.GiveCertificateBTN.Name = "GiveCertificateBTN";
            this.GiveCertificateBTN.Size = new System.Drawing.Size(116, 23);
            this.GiveCertificateBTN.TabIndex = 10;
            this.GiveCertificateBTN.Text = "Give Certificate";
            this.GiveCertificateBTN.UseVisualStyleBackColor = true;
            this.GiveCertificateBTN.Click += new System.EventHandler(this.GiveCertificateBTN_Click);
            // 
            // CertificateTypeCB
            // 
            this.CertificateTypeCB.FormattingEnabled = true;
            this.CertificateTypeCB.Items.AddRange(new object[] {
            "Completion",
            "Excellence"});
            this.CertificateTypeCB.Location = new System.Drawing.Point(828, 212);
            this.CertificateTypeCB.Name = "CertificateTypeCB";
            this.CertificateTypeCB.Size = new System.Drawing.Size(121, 21);
            this.CertificateTypeCB.TabIndex = 11;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(13, 50);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(570, 20);
            this.textBox1.TabIndex = 12;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(825, 196);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(93, 13);
            this.label5.TabIndex = 13;
            this.label5.Text = "Type of Certificate";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(982, 562);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.CertificateTypeCB);
            this.Controls.Add(this.GiveCertificateBTN);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label0);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.SearchCB);
            this.Controls.Add(this.SearchTXT);
            this.Controls.Add(this.CadetListDGV);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.CadetListDGV)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView CadetListDGV;
        private System.Windows.Forms.TextBox SearchTXT;
        private System.Windows.Forms.ComboBox SearchCB;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label0;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button GiveCertificateBTN;
        private System.Windows.Forms.ComboBox CertificateTypeCB;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label5;
    }
}

