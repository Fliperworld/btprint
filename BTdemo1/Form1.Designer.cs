namespace BTdemo1
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.MainMenu mainMenu1;

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
            this.mainMenu1 = new System.Windows.Forms.MainMenu();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.btnTest = new System.Windows.Forms.Button();
            this.txtLog = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.btnAgain = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.radioFP = new System.Windows.Forms.RadioButton();
            this.radioIPL = new System.Windows.Forms.RadioButton();
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this.menuItem2 = new System.Windows.Forms.MenuItem();
            this.mnuExit = new System.Windows.Forms.MenuItem();
            this.mnuDirectPrint = new System.Windows.Forms.MenuItem();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainMenu1
            // 
            this.mainMenu1.MenuItems.Add(this.menuItem1);
            this.mainMenu1.MenuItems.Add(this.menuItem2);
            // 
            // listBox1
            // 
            this.listBox1.Location = new System.Drawing.Point(3, 3);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(234, 58);
            this.listBox1.TabIndex = 0;
            // 
            // btnTest
            // 
            this.btnTest.Location = new System.Drawing.Point(171, 109);
            this.btnTest.Name = "btnTest";
            this.btnTest.Size = new System.Drawing.Size(66, 19);
            this.btnTest.TabIndex = 1;
            this.btnTest.Text = "test";
            this.btnTest.Click += new System.EventHandler(this.btnTest_Click);
            // 
            // txtLog
            // 
            this.txtLog.AcceptsReturn = true;
            this.txtLog.AcceptsTab = true;
            this.txtLog.Location = new System.Drawing.Point(3, 167);
            this.txtLog.Multiline = true;
            this.txtLog.Name = "txtLog";
            this.txtLog.ReadOnly = true;
            this.txtLog.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtLog.Size = new System.Drawing.Size(233, 98);
            this.txtLog.TabIndex = 2;
            this.txtLog.WordWrap = false;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(3, 134);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(69, 27);
            this.button1.TabIndex = 1;
            this.button1.Text = "print";
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnAgain
            // 
            this.btnAgain.Location = new System.Drawing.Point(78, 134);
            this.btnAgain.Name = "btnAgain";
            this.btnAgain.Size = new System.Drawing.Size(83, 27);
            this.btnAgain.TabIndex = 1;
            this.btnAgain.Text = "print again";
            this.btnAgain.Click += new System.EventHandler(this.btnAgain_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.panel1.Controls.Add(this.radioFP);
            this.panel1.Controls.Add(this.radioIPL);
            this.panel1.Location = new System.Drawing.Point(6, 68);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(231, 35);
            // 
            // radioFP
            // 
            this.radioFP.Location = new System.Drawing.Point(104, 7);
            this.radioFP.Name = "radioFP";
            this.radioFP.Size = new System.Drawing.Size(90, 23);
            this.radioFP.TabIndex = 0;
            this.radioFP.Text = "FingerPrint";
            // 
            // radioIPL
            // 
            this.radioIPL.Checked = true;
            this.radioIPL.Location = new System.Drawing.Point(8, 7);
            this.radioIPL.Name = "radioIPL";
            this.radioIPL.Size = new System.Drawing.Size(90, 23);
            this.radioIPL.TabIndex = 0;
            this.radioIPL.Text = "IPL";
            // 
            // menuItem1
            // 
            this.menuItem1.MenuItems.Add(this.mnuExit);
            this.menuItem1.Text = "File";
            // 
            // menuItem2
            // 
            this.menuItem2.MenuItems.Add(this.mnuDirectPrint);
            this.menuItem2.Text = "Extra";
            // 
            // mnuExit
            // 
            this.mnuExit.Text = "Exit";
            // 
            // mnuDirectPrint
            // 
            this.mnuDirectPrint.Text = "Direct Print";
            this.mnuDirectPrint.Click += new System.EventHandler(this.mnuDirectPrint_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.txtLog);
            this.Controls.Add(this.btnAgain);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnTest);
            this.Controls.Add(this.listBox1);
            this.Menu = this.mainMenu1;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.Text = "Form1";
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Button btnTest;
        private System.Windows.Forms.TextBox txtLog;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btnAgain;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RadioButton radioFP;
        private System.Windows.Forms.RadioButton radioIPL;
        private System.Windows.Forms.MenuItem menuItem1;
        private System.Windows.Forms.MenuItem mnuExit;
        private System.Windows.Forms.MenuItem menuItem2;
        private System.Windows.Forms.MenuItem mnuDirectPrint;
    }
}

