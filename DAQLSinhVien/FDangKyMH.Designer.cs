namespace DAQLSinhVien
{
    partial class FDangKyMH
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dtpNgayDK = new System.Windows.Forms.DateTimePicker();
            this.cmbMaMH = new System.Windows.Forms.ComboBox();
            this.cmbMaSV = new System.Windows.Forms.ComboBox();
            this.txtTenMH = new System.Windows.Forms.TextBox();
            this.txtMaDK = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnDK = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.dgvDKH = new System.Windows.Forms.DataGridView();
            this.label6 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDKH)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.groupBox1.Controls.Add(this.dtpNgayDK);
            this.groupBox1.Controls.Add(this.cmbMaMH);
            this.groupBox1.Controls.Add(this.cmbMaSV);
            this.groupBox1.Controls.Add(this.txtTenMH);
            this.groupBox1.Controls.Add(this.txtMaDK);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(33, 35);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(448, 345);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Thông tin đăng ký";
            // 
            // dtpNgayDK
            // 
            this.dtpNgayDK.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpNgayDK.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpNgayDK.Location = new System.Drawing.Point(224, 254);
            this.dtpNgayDK.Name = "dtpNgayDK";
            this.dtpNgayDK.Size = new System.Drawing.Size(200, 30);
            this.dtpNgayDK.TabIndex = 9;
            // 
            // cmbMaMH
            // 
            this.cmbMaMH.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbMaMH.FormattingEnabled = true;
            this.cmbMaMH.Location = new System.Drawing.Point(224, 155);
            this.cmbMaMH.Name = "cmbMaMH";
            this.cmbMaMH.Size = new System.Drawing.Size(155, 33);
            this.cmbMaMH.TabIndex = 8;
            this.cmbMaMH.SelectedIndexChanged += new System.EventHandler(this.cmbMaMH_SelectedIndexChanged);
            // 
            // cmbMaSV
            // 
            this.cmbMaSV.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbMaSV.FormattingEnabled = true;
            this.cmbMaSV.Location = new System.Drawing.Point(224, 103);
            this.cmbMaSV.Name = "cmbMaSV";
            this.cmbMaSV.Size = new System.Drawing.Size(155, 33);
            this.cmbMaSV.TabIndex = 7;
            // 
            // txtTenMH
            // 
            this.txtTenMH.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTenMH.Location = new System.Drawing.Point(224, 203);
            this.txtTenMH.Name = "txtTenMH";
            this.txtTenMH.Size = new System.Drawing.Size(200, 30);
            this.txtTenMH.TabIndex = 6;
            // 
            // txtMaDK
            // 
            this.txtMaDK.Enabled = false;
            this.txtMaDK.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMaDK.Location = new System.Drawing.Point(224, 44);
            this.txtMaDK.Name = "txtMaDK";
            this.txtMaDK.Size = new System.Drawing.Size(155, 30);
            this.txtMaDK.TabIndex = 5;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(29, 254);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(151, 25);
            this.label5.TabIndex = 4;
            this.label5.Text = "Ngày đăng ký:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(29, 203);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(145, 25);
            this.label4.TabIndex = 3;
            this.label4.Text = "Tên môn học:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(29, 155);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(143, 25);
            this.label3.TabIndex = 2;
            this.label3.Text = "Mã môn học: ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(29, 103);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(147, 25);
            this.label2.TabIndex = 1;
            this.label2.Text = "Mã sinh viên: ";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(29, 49);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(137, 25);
            this.label1.TabIndex = 0;
            this.label1.Text = "Mã đăng ký: ";
            // 
            // btnDK
            // 
            this.btnDK.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.btnDK.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDK.Location = new System.Drawing.Point(67, 408);
            this.btnDK.Name = "btnDK";
            this.btnDK.Size = new System.Drawing.Size(115, 37);
            this.btnDK.TabIndex = 1;
            this.btnDK.Text = "Đăng ký";
            this.btnDK.UseVisualStyleBackColor = false;
            this.btnDK.Click += new System.EventHandler(this.btnDK_Click);
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.Location = new System.Drawing.Point(210, 408);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(127, 37);
            this.button2.TabIndex = 2;
            this.button2.Text = "Làm mới";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.button3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button3.Location = new System.Drawing.Point(365, 408);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(105, 37);
            this.button3.TabIndex = 3;
            this.button3.Text = "Thoát";
            this.button3.UseVisualStyleBackColor = false;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // dgvDKH
            // 
            this.dgvDKH.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDKH.Location = new System.Drawing.Point(514, 49);
            this.dgvDKH.Name = "dgvDKH";
            this.dgvDKH.RowHeadersWidth = 51;
            this.dgvDKH.RowTemplate.Height = 24;
            this.dgvDKH.Size = new System.Drawing.Size(766, 290);
            this.dgvDKH.TabIndex = 4;
            this.dgvDKH.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvDKH_CellClick);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(660, 9);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(328, 32);
            this.label6.TabIndex = 5;
            this.label6.Text = "DANH SÁCH ĐĂNG KÝ";
            // 
            // FDangKyMH
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.ClientSize = new System.Drawing.Size(1322, 489);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.dgvDKH);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.btnDK);
            this.Controls.Add(this.groupBox1);
            this.Name = "FDangKyMH";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FDangKyMH";
            this.Load += new System.EventHandler(this.FDangKyMH_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDKH)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DateTimePicker dtpNgayDK;
        private System.Windows.Forms.ComboBox cmbMaMH;
        private System.Windows.Forms.ComboBox cmbMaSV;
        private System.Windows.Forms.TextBox txtTenMH;
        private System.Windows.Forms.TextBox txtMaDK;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnDK;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.DataGridView dgvDKH;
        private System.Windows.Forms.Label label6;
    }
}