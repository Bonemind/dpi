namespace WorkEstimateReplier
{
    partial class ReplierForm
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
            this.txtNotes = new System.Windows.Forms.TextBox();
            this.lstOpenRequests = new System.Windows.Forms.ListBox();
            this.lstClosedRequests = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblWorktype = new System.Windows.Forms.Label();
            this.lblDescription = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.dtpDone = new System.Windows.Forms.DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.btnSend = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtNotes
            // 
            this.txtNotes.Location = new System.Drawing.Point(501, 202);
            this.txtNotes.Name = "txtNotes";
            this.txtNotes.Size = new System.Drawing.Size(185, 20);
            this.txtNotes.TabIndex = 0;
            // 
            // lstOpenRequests
            // 
            this.lstOpenRequests.DisplayMember = "Worktype";
            this.lstOpenRequests.FormattingEnabled = true;
            this.lstOpenRequests.Location = new System.Drawing.Point(12, 22);
            this.lstOpenRequests.Name = "lstOpenRequests";
            this.lstOpenRequests.Size = new System.Drawing.Size(435, 316);
            this.lstOpenRequests.TabIndex = 1;
            this.lstOpenRequests.SelectedIndexChanged += new System.EventHandler(this.lstOpenRequests_SelectedIndexChanged);
            // 
            // lstClosedRequests
            // 
            this.lstClosedRequests.DisplayMember = "Displayname";
            this.lstClosedRequests.FormattingEnabled = true;
            this.lstClosedRequests.Location = new System.Drawing.Point(1151, 38);
            this.lstClosedRequests.Name = "lstClosedRequests";
            this.lstClosedRequests.Size = new System.Drawing.Size(120, 316);
            this.lstClosedRequests.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(498, 55);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Worktype";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(498, 92);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Description";
            // 
            // lblWorktype
            // 
            this.lblWorktype.AutoSize = true;
            this.lblWorktype.Location = new System.Drawing.Point(566, 55);
            this.lblWorktype.Name = "lblWorktype";
            this.lblWorktype.Size = new System.Drawing.Size(60, 13);
            this.lblWorktype.TabIndex = 5;
            this.lblWorktype.Text = "Description";
            // 
            // lblDescription
            // 
            this.lblDescription.AutoSize = true;
            this.lblDescription.Location = new System.Drawing.Point(566, 92);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(60, 13);
            this.lblDescription.TabIndex = 6;
            this.lblDescription.Text = "Description";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(498, 186);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Notes";
            // 
            // dtpDone
            // 
            this.dtpDone.Location = new System.Drawing.Point(501, 163);
            this.dtpDone.Name = "dtpDone";
            this.dtpDone.Size = new System.Drawing.Size(200, 20);
            this.dtpDone.TabIndex = 8;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(498, 147);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(82, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "Estimated Done";
            // 
            // btnSend
            // 
            this.btnSend.Location = new System.Drawing.Point(668, 255);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(75, 23);
            this.btnSend.TabIndex = 11;
            this.btnSend.Text = "Send";
            this.btnSend.UseVisualStyleBackColor = true;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // ReplierForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1309, 417);
            this.Controls.Add(this.btnSend);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.dtpDone);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lblDescription);
            this.Controls.Add(this.lblWorktype);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lstClosedRequests);
            this.Controls.Add(this.lstOpenRequests);
            this.Controls.Add(this.txtNotes);
            this.Name = "ReplierForm";
            this.Text = "Form1";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.ReplierForm_FormClosed);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtNotes;
        private System.Windows.Forms.ListBox lstOpenRequests;
        private System.Windows.Forms.ListBox lstClosedRequests;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblWorktype;
        private System.Windows.Forms.Label lblDescription;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker dtpDone;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnSend;
    }
}

