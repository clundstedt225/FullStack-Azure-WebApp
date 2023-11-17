namespace GUI_Pubs
{
    partial class frmMain
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
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.lvAuthors = new System.Windows.Forms.ListView();
            this.txtID = new System.Windows.Forms.TextBox();
            this.txtFirst = new System.Windows.Forms.TextBox();
            this.txtLast = new System.Windows.Forms.TextBox();
            this.addAuthorButton = new System.Windows.Forms.Button();
            this.deleteAuthorButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.phoneBox = new System.Windows.Forms.TextBox();
            this.addrBox = new System.Windows.Forms.TextBox();
            this.cityBox = new System.Windows.Forms.TextBox();
            this.stateBox = new System.Windows.Forms.TextBox();
            this.zipBox = new System.Windows.Forms.TextBox();
            this.radioYes = new System.Windows.Forms.RadioButton();
            this.radioNo = new System.Windows.Forms.RadioButton();
            this.lvBooks = new System.Windows.Forms.ListView();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.lvAllBooks = new System.Windows.Forms.ListView();
            this.label11 = new System.Windows.Forms.Label();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(22, 437);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(208, 28);
            this.button1.TabIndex = 0;
            this.button1.Text = "List Authors + Books";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(22, 398);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(208, 28);
            this.button2.TabIndex = 2;
            this.button2.Text = "Update Selected";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // lvAuthors
            // 
            this.lvAuthors.HideSelection = false;
            this.lvAuthors.Location = new System.Drawing.Point(244, 30);
            this.lvAuthors.Name = "lvAuthors";
            this.lvAuthors.Size = new System.Drawing.Size(439, 246);
            this.lvAuthors.TabIndex = 3;
            this.lvAuthors.UseCompatibleStateImageBehavior = false;
            this.lvAuthors.SelectedIndexChanged += new System.EventHandler(this.lvAuthors_SelectedIndexChanged);
            // 
            // txtID
            // 
            this.txtID.Location = new System.Drawing.Point(87, 30);
            this.txtID.MaxLength = 11;
            this.txtID.Name = "txtID";
            this.txtID.Size = new System.Drawing.Size(77, 20);
            this.txtID.TabIndex = 4;
            // 
            // txtFirst
            // 
            this.txtFirst.Location = new System.Drawing.Point(87, 61);
            this.txtFirst.Name = "txtFirst";
            this.txtFirst.Size = new System.Drawing.Size(151, 20);
            this.txtFirst.TabIndex = 5;
            // 
            // txtLast
            // 
            this.txtLast.Location = new System.Drawing.Point(87, 91);
            this.txtLast.Name = "txtLast";
            this.txtLast.Size = new System.Drawing.Size(151, 20);
            this.txtLast.TabIndex = 6;
            // 
            // addAuthorButton
            // 
            this.addAuthorButton.Location = new System.Drawing.Point(22, 359);
            this.addAuthorButton.Name = "addAuthorButton";
            this.addAuthorButton.Size = new System.Drawing.Size(208, 28);
            this.addAuthorButton.TabIndex = 7;
            this.addAuthorButton.Text = "Add New Author";
            this.addAuthorButton.UseVisualStyleBackColor = true;
            this.addAuthorButton.Click += new System.EventHandler(this.addAuthorButton_Click);
            // 
            // deleteAuthorButton
            // 
            this.deleteAuthorButton.Location = new System.Drawing.Point(22, 476);
            this.deleteAuthorButton.Name = "deleteAuthorButton";
            this.deleteAuthorButton.Size = new System.Drawing.Size(208, 28);
            this.deleteAuthorButton.TabIndex = 8;
            this.deleteAuthorButton.Text = "Delete Author";
            this.deleteAuthorButton.UseVisualStyleBackColor = true;
            this.deleteAuthorButton.Click += new System.EventHandler(this.deleteAuthorButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(24, 64);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 13);
            this.label1.TabIndex = 9;
            this.label1.Text = "First Name:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(23, 94);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(61, 13);
            this.label2.TabIndex = 10;
            this.label2.Text = "Last Name:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(63, 33);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(21, 13);
            this.label3.TabIndex = 11;
            this.label3.Text = "ID:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 125);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(81, 13);
            this.label4.TabIndex = 12;
            this.label4.Text = "Phone Number:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(36, 160);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(48, 13);
            this.label5.TabIndex = 13;
            this.label5.Text = "Address:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(49, 193);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(27, 13);
            this.label6.TabIndex = 14;
            this.label6.Text = "City:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(41, 227);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(35, 13);
            this.label7.TabIndex = 15;
            this.label7.Text = "State:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(23, 263);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(53, 13);
            this.label8.TabIndex = 16;
            this.label8.Text = "Zip Code:";
            // 
            // phoneBox
            // 
            this.phoneBox.Location = new System.Drawing.Point(87, 122);
            this.phoneBox.Name = "phoneBox";
            this.phoneBox.Size = new System.Drawing.Size(151, 20);
            this.phoneBox.TabIndex = 17;
            // 
            // addrBox
            // 
            this.addrBox.Location = new System.Drawing.Point(87, 157);
            this.addrBox.Name = "addrBox";
            this.addrBox.Size = new System.Drawing.Size(151, 20);
            this.addrBox.TabIndex = 18;
            // 
            // cityBox
            // 
            this.cityBox.Location = new System.Drawing.Point(87, 190);
            this.cityBox.Name = "cityBox";
            this.cityBox.Size = new System.Drawing.Size(151, 20);
            this.cityBox.TabIndex = 19;
            // 
            // stateBox
            // 
            this.stateBox.Location = new System.Drawing.Point(87, 224);
            this.stateBox.MaxLength = 2;
            this.stateBox.Name = "stateBox";
            this.stateBox.Size = new System.Drawing.Size(43, 20);
            this.stateBox.TabIndex = 20;
            // 
            // zipBox
            // 
            this.zipBox.Location = new System.Drawing.Point(87, 260);
            this.zipBox.MaxLength = 5;
            this.zipBox.Name = "zipBox";
            this.zipBox.Size = new System.Drawing.Size(63, 20);
            this.zipBox.TabIndex = 21;
            // 
            // radioYes
            // 
            this.radioYes.AutoSize = true;
            this.radioYes.Location = new System.Drawing.Point(44, 302);
            this.radioYes.Name = "radioYes";
            this.radioYes.Size = new System.Drawing.Size(86, 17);
            this.radioYes.TabIndex = 22;
            this.radioYes.TabStop = true;
            this.radioYes.Text = "Yes Contract";
            this.radioYes.UseVisualStyleBackColor = true;
            // 
            // radioNo
            // 
            this.radioNo.AutoSize = true;
            this.radioNo.Location = new System.Drawing.Point(136, 302);
            this.radioNo.Name = "radioNo";
            this.radioNo.Size = new System.Drawing.Size(82, 17);
            this.radioNo.TabIndex = 23;
            this.radioNo.TabStop = true;
            this.radioNo.Text = "No Contract";
            this.radioNo.UseVisualStyleBackColor = true;
            // 
            // lvBooks
            // 
            this.lvBooks.HideSelection = false;
            this.lvBooks.Location = new System.Drawing.Point(244, 302);
            this.lvBooks.Name = "lvBooks";
            this.lvBooks.Size = new System.Drawing.Size(439, 218);
            this.lvBooks.TabIndex = 24;
            this.lvBooks.UseCompatibleStateImageBehavior = false;
            this.lvBooks.SelectedIndexChanged += new System.EventHandler(this.listView1_SelectedIndexChanged);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(250, 9);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(43, 13);
            this.label9.TabIndex = 25;
            this.label9.Text = "Authors";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(253, 283);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(84, 13);
            this.label10.TabIndex = 26;
            this.label10.Text = "Books by author";
            // 
            // lvAllBooks
            // 
            this.lvAllBooks.HideSelection = false;
            this.lvAllBooks.Location = new System.Drawing.Point(689, 29);
            this.lvAllBooks.Name = "lvAllBooks";
            this.lvAllBooks.Size = new System.Drawing.Size(312, 490);
            this.lvAllBooks.TabIndex = 27;
            this.lvAllBooks.UseCompatibleStateImageBehavior = false;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(689, 13);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(51, 13);
            this.label11.TabIndex = 28;
            this.label11.Text = "All Books";
            // 
            // splitter1
            // 
            this.splitter1.Dock = System.Windows.Forms.DockStyle.Right;
            this.splitter1.Location = new System.Drawing.Point(1010, 0);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(3, 532);
            this.splitter1.TabIndex = 29;
            this.splitter1.TabStop = false;
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1013, 532);
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.lvAllBooks);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.lvBooks);
            this.Controls.Add(this.radioNo);
            this.Controls.Add(this.radioYes);
            this.Controls.Add(this.zipBox);
            this.Controls.Add(this.stateBox);
            this.Controls.Add(this.cityBox);
            this.Controls.Add(this.addrBox);
            this.Controls.Add(this.phoneBox);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.deleteAuthorButton);
            this.Controls.Add(this.addAuthorButton);
            this.Controls.Add(this.txtLast);
            this.Controls.Add(this.txtFirst);
            this.Controls.Add(this.txtID);
            this.Controls.Add(this.lvAuthors);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "frmMain";
            this.Text = "Lab 05";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.ListView lvAuthors;
        private System.Windows.Forms.TextBox txtID;
        private System.Windows.Forms.TextBox txtFirst;
        private System.Windows.Forms.TextBox txtLast;
        private System.Windows.Forms.Button addAuthorButton;
        private System.Windows.Forms.Button deleteAuthorButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox phoneBox;
        private System.Windows.Forms.TextBox addrBox;
        private System.Windows.Forms.TextBox cityBox;
        private System.Windows.Forms.TextBox stateBox;
        private System.Windows.Forms.TextBox zipBox;
        private System.Windows.Forms.RadioButton radioYes;
        private System.Windows.Forms.RadioButton radioNo;
        private System.Windows.Forms.ListView lvBooks;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ListView lvAllBooks;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Splitter splitter1;
    }
}

