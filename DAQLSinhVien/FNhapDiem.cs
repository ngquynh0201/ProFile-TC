using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DAQLSinhVien
{
    public partial class FNhapDiem : Form
    {
        SqlConnection Mycon = new SqlConnection(@"Data Source=PC\SQLEXPRESS;Initial Catalog=QuanLySV;User ID=sa;Password=123;");

        public FNhapDiem()
        {
            InitializeComponent();
        }

        private void FNhapDiem_Load(object sender, EventArgs e)
        {
            LoadDLSV_Combobox();
            Load_DGV();
            LoadDL_MaMH();
        }

        private void LoadDLSV_Combobox()
        {
            string sql = "select MA_SV from SinhVien";
            SqlDataAdapter adapter = new SqlDataAdapter(sql, Mycon);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            cbMaSV.DataSource = dt;
            cbMaSV.DisplayMember = "MA_SV";
            cbMaSV.ValueMember = "MA_SV";
            LayTenSV();
        }

        private void LayTenSV()
        {
            if (Mycon.State == ConnectionState.Open) Mycon.Close();
            Mycon.Open();
            SqlCommand cmd = new SqlCommand("SELECT TEN_SV FROM SinhVien WHERE MA_SV = @masv", Mycon);
            cmd.Parameters.AddWithValue("@masv", cbMaSV.SelectedValue.ToString());
            SqlDataReader rd = cmd.ExecuteReader();
            if (rd.Read())
            {
                txtTenSV.Text = rd.GetString(0);
            }
            Mycon.Close();
        }
        private void cbMaSV_SelectedIndexChanged(object sender, EventArgs e)
        {
            LayTenSV();
            Load_DGV();
        }

        private void Load_DGV()
        {
            if (Mycon.State == ConnectionState.Open) Mycon.Close();
            Mycon.Open();
            string sql = "select * from Diem";
            SqlDataAdapter AD = new SqlDataAdapter(sql, Mycon);
            DataTable dt = new DataTable();
            AD.Fill(dt);
            dgvDiem.DataSource = dt;
            dgvDiem.Columns[0].HeaderText = "Mã Sinh Viên";
            dgvDiem.Columns[1].HeaderText = "Tên Sinh Viên";
            dgvDiem.Columns[2].HeaderText = "Mã Môn Học";
            dgvDiem.Columns[4].HeaderText = "Điểm Nhận Thức";
            dgvDiem.Columns[3].HeaderText = "Điểm Chuyên Cần";
            dgvDiem.Columns[5].HeaderText = "Điểm Giữa Kỳ";
            dgvDiem.Columns[6].HeaderText = "Điểm Cuối Kỳ";
            dgvDiem.Columns[7].HeaderText = "Điểm Trung Bình";
            dgvDiem.AllowUserToAddRows = false;
            dgvDiem.EditMode = DataGridViewEditMode.EditProgrammatically;
            dgvDiem.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvDiem.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            Mycon.Close();
        }
        int dong;
        private void dgvDiem_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }
        private void LoadDL_MaMH()
        {
            string sql = "select MA_MH from MonHoc";
            SqlDataAdapter adapter = new SqlDataAdapter(sql, Mycon);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            cbMaMH.DataSource = dt;
            cbMaMH.DisplayMember = "MA_MH";
            cbMaMH.ValueMember = "MA_MH";
        }


        private void btnLuu_Click(object sender, EventArgs e)
        {
            try
            {
                // Mở kết nối (nếu chưa mở)
                if (Mycon.State != ConnectionState.Open)
                {
                    Mycon.Open();
                }

                // Lấy chỉ số dòng hiện tại và các giá trị điểm từ DataGridView
                int dong = dgvDiem.CurrentRow.Index;
                float diemNT, diemCC, diemGK, diemCK; 
                
                // Kiểm tra tính hợp lệ của các giá trị điểm nhập vào
                if (!float.TryParse(txtDNT.Text, out diemNT) ||
                    !float.TryParse(txtDCC.Text, out diemCC) ||
                    !float.TryParse(txtDGK.Text, out diemGK) ||
                    !float.TryParse(txtDCK.Text, out diemCK))
                      
                {
                    MessageBox.Show("Vui lòng nhập đúng định dạng điểm số.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Tính điểm trung bình (DTB)
                float diemTB = (diemNT * 1f + diemCC * 1f + diemGK * 2f + diemCK * 6f) / 10;

                // Lấy mã sinh viên và mã môn học từ DataGridView và ComboBox
                string maSV = dgvDiem.Rows[dong].Cells[0].Value.ToString();

                string maMH = cbMaMH.SelectedValue.ToString();


                // Cập nhật điểm vào cơ sở dữ liệu (CSDL)
                string sqlUpdate = @"UPDATE Diem
                         SET DIEM_NT = @DiemNT,
                             DIEM_CC = @DiemCC,
                             DIEM_GK = @DiemGK,
                             DIEM_CK = @DiemCK,
                             DIEM_TB = @DiemTB
                         WHERE MA_SV = '" + maSV+"'"; 

                using (SqlCommand cmd = new SqlCommand(sqlUpdate, Mycon))
                {
                    // Sử dụng tham số hóa để ngăn chặn SQL Injection
                    cmd.Parameters.AddWithValue("@DiemNT", diemNT);
                    cmd.Parameters.AddWithValue("@DiemCC", diemCC);
                    cmd.Parameters.AddWithValue("@DiemGK", diemGK);
                    cmd.Parameters.AddWithValue("@DiemCK", diemCK);
                    cmd.Parameters.AddWithValue("@DiemTB", diemTB);

                    // Thực thi truy vấn UPDATE
                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    { 
                        MessageBox.Show("Lưu thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy bản ghi để cập nhật.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                // Xử lý lỗi SQL (ví dụ: khóa chính trùng lặp, vi phạm ràng buộc,...)
                MessageBox.Show($"Lỗi cơ sở dữ liệu: {sqlEx.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                // Xử lý các lỗi chung khác
                MessageBox.Show($"Đã xảy ra lỗi: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                // Đóng kết nối
                Mycon.Close();

                              
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Load_DGV();
        }
        
        private void dgvDiem_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            dong = e.RowIndex;
            cbMaSV.Text = dgvDiem.Rows[dong].Cells[0].Value.ToString();
            txtTenSV.Text = dgvDiem.Rows[dong].Cells[1].Value.ToString();
            cbMaMH.Text = dgvDiem.Rows[dong].Cells[2].Value.ToString();
            txtDNT.Text = dgvDiem.Rows[dong].Cells[4].Value.ToString();
            txtDCC.Text = dgvDiem.Rows[dong].Cells[3].Value.ToString();
            txtDGK.Text = dgvDiem.Rows[dong].Cells[5].Value.ToString();
            txtDCK.Text = dgvDiem.Rows[dong].Cells[6].Value.ToString();
        }
    }
}
