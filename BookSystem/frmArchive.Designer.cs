namespace BookSystem
{
    partial class frmArchive
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmArchive));
            this.panel2 = new System.Windows.Forms.Panel();
            this.btn_Minimize = new System.Windows.Forms.Button();
            this.pictureBox5 = new System.Windows.Forms.PictureBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.dgvArchives = new System.Windows.Forms.DataGridView();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvArchives)).BeginInit();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(16)))), ((int)(((byte)(59)))));
            this.panel2.Controls.Add(this.btn_Minimize);
            this.panel2.Controls.Add(this.pictureBox5);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.btnClose);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Margin = new System.Windows.Forms.Padding(4);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(604, 35);
            this.panel2.TabIndex = 17;
            // 
            // btn_Minimize
            // 
            this.btn_Minimize.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btn_Minimize.BackgroundImage")));
            this.btn_Minimize.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btn_Minimize.Dock = System.Windows.Forms.DockStyle.Right;
            this.btn_Minimize.FlatAppearance.BorderSize = 0;
            this.btn_Minimize.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Minimize.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btn_Minimize.Location = new System.Drawing.Point(544, 0);
            this.btn_Minimize.Margin = new System.Windows.Forms.Padding(4);
            this.btn_Minimize.Name = "btn_Minimize";
            this.btn_Minimize.Size = new System.Drawing.Size(30, 35);
            this.btn_Minimize.TabIndex = 17;
            this.btn_Minimize.UseVisualStyleBackColor = true;
            // 
            // pictureBox5
            // 
            this.pictureBox5.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox5.BackgroundImage")));
            this.pictureBox5.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox5.Location = new System.Drawing.Point(-2, 0);
            this.pictureBox5.Name = "pictureBox5";
            this.pictureBox5.Size = new System.Drawing.Size(41, 35);
            this.pictureBox5.TabIndex = 16;
            this.pictureBox5.TabStop = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Lucida Fax", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(174)))), ((int)(((byte)(184)))), ((int)(((byte)(254)))));
            this.label4.Location = new System.Drawing.Point(45, 9);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(154, 18);
            this.label4.TabIndex = 1;
            this.label4.Text = "Booklat - Archives";
            // 
            // btnClose
            // 
            this.btnClose.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnClose.BackgroundImage")));
            this.btnClose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnClose.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnClose.FlatAppearance.BorderSize = 0;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Location = new System.Drawing.Point(574, 0);
            this.btnClose.Margin = new System.Windows.Forms.Padding(4);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(30, 35);
            this.btnClose.TabIndex = 0;
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // dgvArchives
            // 
            this.dgvArchives.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(15)))), ((int)(((byte)(59)))));
            this.dgvArchives.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvArchives.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvArchives.Location = new System.Drawing.Point(12, 51);
            this.dgvArchives.Name = "dgvArchives";
            this.dgvArchives.Size = new System.Drawing.Size(547, 215);
            this.dgvArchives.TabIndex = 18;
            // 
            // btnClear
            // 
            this.btnClear.Font = new System.Drawing.Font("Lucida Fax", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClear.Location = new System.Drawing.Point(462, 272);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(129, 23);
            this.btnClear.TabIndex = 19;
            this.btnClear.Text = "CLEAR ARCHIVE";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnRefresh
            // 
            this.btnRefresh.BackColor = System.Drawing.Color.Transparent;
            this.btnRefresh.BackgroundImage = global::BookSystem.Properties.Resources.refresh;
            this.btnRefresh.FlatAppearance.BorderSize = 0;
            this.btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRefresh.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnRefresh.Location = new System.Drawing.Point(565, 51);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(26, 26);
            this.btnRefresh.TabIndex = 20;
            this.btnRefresh.UseVisualStyleBackColor = false;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // frmArchive
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(15)))), ((int)(((byte)(59)))));
            this.BackgroundImage = global::BookSystem.Properties.Resources.White_and_Light_Blue_Illustrative_Creative_Literature_Project_Presentation;
            this.ClientSize = new System.Drawing.Size(604, 307);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.dgvArchives);
            this.Controls.Add(this.panel2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmArchive";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Booklat -Archives";
            this.Load += new System.EventHandler(this.frmArchive_Load);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvArchives)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btn_Minimize;
        private System.Windows.Forms.PictureBox pictureBox5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.DataGridView dgvArchives;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnRefresh;
    }
}