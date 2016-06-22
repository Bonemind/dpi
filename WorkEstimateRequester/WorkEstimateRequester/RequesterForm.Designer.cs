namespace WorkEstimateRequester
{
    partial class RequesterForm
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
            this.txtWorktype = new System.Windows.Forms.TextBox();
            this.btnSend = new System.Windows.Forms.Button();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.lstOpen = new System.Windows.Forms.ListBox();
            this.lstClosed = new System.Windows.Forms.ListBox();
            this.lblEstimatedDone = new System.Windows.Forms.Label();
            this.lblNotes = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // txtWorktype
            // 
            this.txtWorktype.Location = new System.Drawing.Point(245, 31);
            this.txtWorktype.Name = "txtWorktype";
            this.txtWorktype.Size = new System.Drawing.Size(302, 20);
            this.txtWorktype.TabIndex = 0;
            // 
            // btnSend
            // 
            this.btnSend.Location = new System.Drawing.Point(427, 176);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(75, 23);
            this.btnSend.TabIndex = 1;
            this.btnSend.Text = "Send";
            this.btnSend.UseVisualStyleBackColor = true;
            this.btnSend.Click += new System.EventHandler(this.button1_Click);
            // 
            // txtDescription
            // 
            this.txtDescription.Location = new System.Drawing.Point(245, 67);
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Size = new System.Drawing.Size(302, 20);
            this.txtDescription.TabIndex = 2;
            // 
            // lstOpen
            // 
            this.lstOpen.FormattingEnabled = true;
            this.lstOpen.Location = new System.Drawing.Point(12, 31);
            this.lstOpen.Name = "lstOpen";
            this.lstOpen.Size = new System.Drawing.Size(209, 342);
            this.lstOpen.TabIndex = 3;
            this.lstOpen.SelectedIndexChanged += new System.EventHandler(this.lstOpen_SelectedIndexChanged);
            // 
            // lstClosed
            // 
            this.lstClosed.FormattingEnabled = true;
            this.lstClosed.Location = new System.Drawing.Point(688, 31);
            this.lstClosed.Name = "lstClosed";
            this.lstClosed.Size = new System.Drawing.Size(209, 342);
            this.lstClosed.TabIndex = 4;
            this.lstClosed.SelectedIndexChanged += new System.EventHandler(this.lstClosed_SelectedIndexChanged);
            // 
            // lblEstimatedDone
            // 
            this.lblEstimatedDone.AutoSize = true;
            this.lblEstimatedDone.Location = new System.Drawing.Point(242, 108);
            this.lblEstimatedDone.Name = "lblEstimatedDone";
            this.lblEstimatedDone.Size = new System.Drawing.Size(35, 13);
            this.lblEstimatedDone.TabIndex = 5;
            this.lblEstimatedDone.Text = "label1";
            // 
            // lblNotes
            // 
            this.lblNotes.AutoSize = true;
            this.lblNotes.Location = new System.Drawing.Point(242, 139);
            this.lblNotes.Name = "lblNotes";
            this.lblNotes.Size = new System.Drawing.Size(35, 13);
            this.lblNotes.TabIndex = 6;
            this.lblNotes.Text = "label2";
            // 
            // RequesterForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(909, 395);
            this.Controls.Add(this.lblNotes);
            this.Controls.Add(this.lblEstimatedDone);
            this.Controls.Add(this.lstClosed);
            this.Controls.Add(this.lstOpen);
            this.Controls.Add(this.txtDescription);
            this.Controls.Add(this.btnSend);
            this.Controls.Add(this.txtWorktype);
            this.Name = "RequesterForm";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.RequesterForm_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtWorktype;
        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.TextBox txtDescription;
        private System.Windows.Forms.ListBox lstOpen;
        private System.Windows.Forms.ListBox lstClosed;
        private System.Windows.Forms.Label lblEstimatedDone;
        private System.Windows.Forms.Label lblNotes;
    }
}

