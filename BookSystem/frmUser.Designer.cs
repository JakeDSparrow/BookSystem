﻿namespace BookSystem
{
    partial class frmUser
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmUser));
            this.lblBorrowbook = new System.Windows.Forms.Label();
            this.lblReturnBook = new System.Windows.Forms.Label();
            this.dgvBooks = new System.Windows.Forms.DataGridView();
            this.btnSearch = new System.Windows.Forms.Button();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.lblLogout = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBooks)).BeginInit();
            this.SuspendLayout();
            // 
            // lblBorrowbook
            // 
            this.lblBorrowbook.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblBorrowbook.Location = new System.Drawing.Point(12, 89);
            this.lblBorrowbook.Name = "lblBorrowbook";
            this.lblBorrowbook.Size = new System.Drawing.Size(100, 23);
            this.lblBorrowbook.TabIndex = 3;
            this.lblBorrowbook.Text = "Borrow Book";
            this.lblBorrowbook.Click += new System.EventHandler(this.lblBorrowbook_Click);
            // 
            // lblReturnBook
            // 
            this.lblReturnBook.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblReturnBook.Location = new System.Drawing.Point(127, 89);
            this.lblReturnBook.Name = "lblReturnBook";
            this.lblReturnBook.Size = new System.Drawing.Size(100, 23);
            this.lblReturnBook.TabIndex = 4;
            this.lblReturnBook.Text = "Return Book";
            this.lblReturnBook.Click += new System.EventHandler(this.lblReturnBook_Click);
            // 
            // dgvBooks
            // 
            this.dgvBooks.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvBooks.Location = new System.Drawing.Point(13, 116);
            this.dgvBooks.Name = "dgvBooks";
            this.dgvBooks.Size = new System.Drawing.Size(553, 265);
            this.dgvBooks.TabIndex = 5;
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(491, 90);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(75, 23);
            this.btnSearch.TabIndex = 7;
            this.btnSearch.Text = "Search";
            this.btnSearch.UseVisualStyleBackColor = true;
            // 
            // txtSearch
            // 
            this.txtSearch.Location = new System.Drawing.Point(253, 90);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(232, 20);
            this.txtSearch.TabIndex = 6;
            // 
            // lblLogout
            // 
            this.lblLogout.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.lblLogout.Font = new System.Drawing.Font("Lucida Fax", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLogout.Location = new System.Drawing.Point(505, 20);
            this.lblLogout.Name = "lblLogout";
            this.lblLogout.Size = new System.Drawing.Size(63, 23);
            this.lblLogout.TabIndex = 8;
            this.lblLogout.Text = "Log Out?";
            this.lblLogout.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblLogout.Click += new System.EventHandler(this.lblLogout_Click);
            // 
            // frmUser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(580, 393);
            this.Controls.Add(this.lblLogout);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.txtSearch);
            this.Controls.Add(this.dgvBooks);
            this.Controls.Add(this.lblReturnBook);
            this.Controls.Add(this.lblBorrowbook);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmUser";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmUser";
            ((System.ComponentModel.ISupportInitialize)(this.dgvBooks)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblBorrowbook;
        private System.Windows.Forms.Label lblReturnBook;
        private System.Windows.Forms.DataGridView dgvBooks;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Label lblLogout;
    }
}