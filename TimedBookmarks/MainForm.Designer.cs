namespace TimedBookmarks
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.btReset = new System.Windows.Forms.Button();
            this.btSave = new System.Windows.Forms.Button();
            this.lvBookmarks = new System.Windows.Forms.ListView();
            this.ch1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.cbCapture = new System.Windows.Forms.CheckBox();
            this.tbYoutube = new System.Windows.Forms.TextBox();
            this.lbYoutube = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btReset
            // 
            this.btReset.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btReset.Location = new System.Drawing.Point(12, 313);
            this.btReset.Name = "btReset";
            this.btReset.Size = new System.Drawing.Size(75, 23);
            this.btReset.TabIndex = 4;
            this.btReset.Text = "Reset";
            this.btReset.UseVisualStyleBackColor = true;
            this.btReset.Click += new System.EventHandler(this.ResetEvent);
            // 
            // btSave
            // 
            this.btSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btSave.Enabled = false;
            this.btSave.Location = new System.Drawing.Point(253, 313);
            this.btSave.Name = "btSave";
            this.btSave.Size = new System.Drawing.Size(75, 23);
            this.btSave.TabIndex = 5;
            this.btSave.Text = "Save";
            this.btSave.UseVisualStyleBackColor = true;
            this.btSave.Click += new System.EventHandler(this.SaveEvent);
            // 
            // lvBookmarks
            // 
            this.lvBookmarks.Activation = System.Windows.Forms.ItemActivation.OneClick;
            this.lvBookmarks.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvBookmarks.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.ch1});
            this.lvBookmarks.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lvBookmarks.LabelWrap = false;
            this.lvBookmarks.Location = new System.Drawing.Point(15, 42);
            this.lvBookmarks.MultiSelect = false;
            this.lvBookmarks.Name = "lvBookmarks";
            this.lvBookmarks.Size = new System.Drawing.Size(313, 239);
            this.lvBookmarks.TabIndex = 1;
            this.lvBookmarks.UseCompatibleStateImageBehavior = false;
            this.lvBookmarks.View = System.Windows.Forms.View.Details;
            this.lvBookmarks.SelectedIndexChanged += new System.EventHandler(this.LinkSelected);
            // 
            // ch1
            // 
            this.ch1.Text = "Bookmarks";
            this.ch1.Width = 285;
            // 
            // cbCapture
            // 
            this.cbCapture.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbCapture.Appearance = System.Windows.Forms.Appearance.Button;
            this.cbCapture.Location = new System.Drawing.Point(13, 13);
            this.cbCapture.Name = "cbCapture";
            this.cbCapture.Size = new System.Drawing.Size(315, 23);
            this.cbCapture.TabIndex = 0;
            this.cbCapture.Text = "Configure capture button!";
            this.cbCapture.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.cbCapture.UseVisualStyleBackColor = true;
            this.cbCapture.Click += new System.EventHandler(this.CaptureEvent);
            // 
            // tbYoutube
            // 
            this.tbYoutube.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.tbYoutube.Location = new System.Drawing.Point(87, 287);
            this.tbYoutube.Name = "tbYoutube";
            this.tbYoutube.Size = new System.Drawing.Size(241, 20);
            this.tbYoutube.TabIndex = 3;
            this.tbYoutube.WordWrap = false;
            this.tbYoutube.TextChanged += new System.EventHandler(this.UpdateLink);
            // 
            // lbYoutube
            // 
            this.lbYoutube.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lbYoutube.AutoSize = true;
            this.lbYoutube.Location = new System.Drawing.Point(12, 290);
            this.lbYoutube.Name = "lbYoutube";
            this.lbYoutube.Size = new System.Drawing.Size(69, 13);
            this.lbYoutube.TabIndex = 2;
            this.lbYoutube.Text = "Youtube link:";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(340, 348);
            this.Controls.Add(this.lbYoutube);
            this.Controls.Add(this.tbYoutube);
            this.Controls.Add(this.cbCapture);
            this.Controls.Add(this.lvBookmarks);
            this.Controls.Add(this.btSave);
            this.Controls.Add(this.btReset);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.Text = "Timed Bookmarks 1.0";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.BeforeCloseEvent);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btReset;
        private System.Windows.Forms.Button btSave;
        private System.Windows.Forms.ListView lvBookmarks;
        private System.Windows.Forms.CheckBox cbCapture;
        private System.Windows.Forms.TextBox tbYoutube;
        private System.Windows.Forms.Label lbYoutube;
        private System.Windows.Forms.ColumnHeader ch1;
    }
}

