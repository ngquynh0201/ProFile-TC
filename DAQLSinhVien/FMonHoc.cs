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
    public partial class FMonHoc : Form
    {
        SqlConnection Mycon = new SqlConnection(@"Data Source=PC\SQLEXPRESS;Initial Catalog=QuanLySV;User ID=sa;Password=123;");

        public FMonHoc()
        {
            InitializeComponent();
        }

        private void FMonHoc_Load(object sender, EventArgs e)
        {
            Load_DGV();
        }

        int dong;
        private void Load_DGV()
        {
            if (Mycon.State == ConnectionState.Open) Mycon.Close();
            Mycon.Open();
            string sql = "select * from MonHoc";
            SqlDataAdapter AD = new SqlDataAdapter(sql, Mycon);
            DataTable dt = new DataTable();
            AD.Fill(dt);
            dgvMonHoc.DataSource = dt;
            dgvMonHoc.Columns[0].HeaderText = "Mã môn học";
            dgvMonHoc.Columns[1].HeaderText = "Tên môn học";
            dgvMonHoc.Columns[2].HeaderText = "Số Tín chỉ";
            dgvMonHoc.AllowUserToAddRows = false;
            dgvMonHoc.EditMode = DataGridViewEditMode.EditProgrammatically;
            Mycon.Close();
        }

        private void dgvMonHoc_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            dong = e.RowIndex;
            txtMaMH.Text = dgvMonHoc.Rows[dong].Cells[0].Value.ToString();
            txtTenMH.Text = dgvMonHoc.Rows[dong].Cells[1].Value.ToString();
            txtSoTC.Text = dgvMonHoc.Rows[dong].Cells[2].Value.ToString();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            txtMaMH.Clear();
            txtTenMH.Clear();
            txtSoTC.Clear();
            btnLuu.Enabled = true;
            btnThem.Enabled = false;
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            try
            {
                // Sử dụng khối using để tự động quản lý tài nguyên
                /*using (Mycon = new SqlConnection(connectionString)) */// Giả sử connectionString đã được định nghĩa
                using (SqlCommand cmd = new SqlCommand("select count(*) from MonHoc where MA_MH = @mamh", Mycon))
                {
                    // Mở kết nối đến cơ sở dữ liệu
                    Mycon.Open();

                    // Truy vấn tham số hóa để ngăn chặn tấn công SQL Injection
                    cmd.Parameters.AddWithValue("@mamh", txtMaMH.Text);
                    int count = (int)cmd.ExecuteScalar();

                    // Kiểm tra xem môn học đã tồn tại hay chưa
                    if (count > 0)
                    {
                        MessageBox.Show("Môn học đã tồn tại.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        // Nếu môn học chưa tồn tại, thực hiện thêm môn học mới
                        using (SqlCommand cmd1 = new SqlCommand(@"insert into MonHoc (MA_MH, TEN_MH,SO_TC) values (@mamh, @tenmh, @sotc)", Mycon))
                        {
                            // Tham số hóa cho câu lệnh insert
                            cmd1.Parameters.AddWithValue("@mamh", txtMaMH.Text);
                            cmd1.Parameters.AddWithValue("@tenmh", txtTenMH.Text);

                            // Kiểm tra đầu vào (ví dụ: đảm bảo SO_TC là một số nguyên)
                            if (int.TryParse(txtSoTC.Text, out int soTC))
                            {
                                cmd1.Parameters.AddWithValue("@sotc", soTC);
                                cmd1.ExecuteNonQuery();
                                MessageBox.Show("Lưu môn học thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                Load_DGV(); // Cập nhật lại danh sách môn học
                            }
                            else
                            {
                                MessageBox.Show("Số tín chỉ không hợp lệ.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Xử lý ngoại lệ
                MessageBox.Show($"Đã xảy ra lỗi: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                // Cân nhắc việc ghi lại chi tiết ngoại lệ để phân tích thêm
                btnLuu.Enabled = true;
                btnThem.Enabled = false;
            }

        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            try
            {
               /* using (Mycon = new SqlConnection(connectionString))*/ // Giả sử connectionString đã được định nghĩa
                using (SqlCommand cmd = new SqlCommand("select count (*) from MonHoc where MA_MH = @mamh", Mycon))
                {
                    Mycon.Open();

                    // Truy vấn tham số hóa để ngăn chặn SQL Injection
                    cmd.Parameters.AddWithValue("@mamh", txtMaMH.Text);
                    int count = (int)cmd.ExecuteScalar();

                    // Nếu môn học đã tồn tại, thực hiện cập nhật thông tin
                    if (count > 0)
                    {
                        int dong = dgvMonHoc.CurrentRow.Index;
                        string mamhCu = dgvMonHoc.Rows[dong].Cells[0].Value.ToString();

                        // Truy vấn update tham số hóa
                        using (SqlCommand cmd1 = new SqlCommand(@"UPDATE MonHoc SET MA_MH = @mamh, TEN_MH = @tenmh, SO_TC = @sotc WHERE MA_MH = @mamhCu", Mycon))
                        {
                            cmd1.Parameters.AddWithValue("@mamh", txtMaMH.Text);
                            cmd1.Parameters.AddWithValue("@tenmh", txtTenMH.Text);
                            cmd1.Parameters.AddWithValue("@sotc", txtSoTC.Text);
                            cmd1.Parameters.AddWithValue("@mamhCu", mamhCu);

                            // Thực hiện cập nhật
                            cmd1.ExecuteNonQuery();
                            MessageBox.Show("Cập nhật thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            Load_DGV();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Đã xảy ra lỗi: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                // Cân nhắc việc ghi log lỗi để phân tích thêm
                btnSua.Enabled = false;
                btnXoa.Enabled = false;
            }

        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (Mycon.State == ConnectionState.Open) Mycon.Close();
            Mycon.Open();
            foreach (DataGridViewRow selectedRow in dgvMonHoc.SelectedRows)
            {
                string mamh = selectedRow.Cells["MA_MH"].Value.ToString();
                string sqlDelete = "delete MonHoc WHERE MA_MH = @mamh";
                SqlCommand cmd = new SqlCommand(sqlDelete, Mycon);
                cmd.Parameters.AddWithValue("@mamh", mamh);
                cmd.ExecuteNonQuery();
                cmd.Dispose();
            }
            MessageBox.Show("Xóa thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Load_DGV();
            Mycon.Close();

        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            txtMaMH.Clear();
            txtTenMH.Clear();
            txtSoTC.Clear();
            Load_DGV();
        }
    }
}
