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
    public partial class FGiaoVien : Form
    {
        SqlConnection Mycon = new SqlConnection(@"Data Source=PC\SQLEXPRESS;Initial Catalog=QuanLySV;User ID=sa;Password=123;");

        public FGiaoVien()
        {
            InitializeComponent();
        }

        private void FGiaoVien_Load(object sender, EventArgs e)
        {
            Load_DGV();
            LoadDL_Combobox();
        }

        private void Load_DGV()
        {
            chkGioitinh.Enabled = true;
            if (Mycon.State == ConnectionState.Open) Mycon.Close();
            Mycon.Open();
            string sql = "select * from GiaoVien";
            SqlDataAdapter AD = new SqlDataAdapter(sql, Mycon);
            DataTable dt = new DataTable();
            AD.Fill(dt);
            dgvGiaoVien.DataSource = dt;
            dgvGiaoVien.Columns[0].HeaderText = "Mã Giáo Viên";
            dgvGiaoVien.Columns[1].HeaderText = "Họ và Tên";
            dgvGiaoVien.Columns[2].HeaderText = "Giới Tính";
            dgvGiaoVien.Columns[3].HeaderText = "Ngày Sinh";
            dgvGiaoVien.Columns[4].HeaderText = "Mã Khoa";
            dgvGiaoVien.AllowUserToAddRows = false;
            dgvGiaoVien.EditMode = DataGridViewEditMode.EditProgrammatically;
            Mycon.Close();

        }

        private void dgvGiaoVien_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            txtMaGV.Text = dgvGiaoVien .CurrentRow.Cells["MA_GV"].Value.ToString();
            txtTenGV.Text = dgvGiaoVien.CurrentRow.Cells["TEN_GV"].Value.ToString();
            if (dgvGiaoVien.CurrentRow.Cells["GIOI_TINH"].Value.ToString() == "True")
                chkGioitinh.Checked = true;
            else chkGioitinh.Checked = false;
            dtpNgaySinh.Text = dgvGiaoVien.CurrentRow.Cells["NGAY_SINH"].Value.ToString();
            cbMaKhoa.Text = dgvGiaoVien.CurrentRow.Cells["MA_Khoa"].Value.ToString();
            btnSua.Enabled = true;
            btnXoa.Enabled = true;
        }

        private void LoadDL_Combobox()
        {
            string sql = "select MA_Khoa from Khoa";
            SqlDataAdapter adapter = new SqlDataAdapter(sql, Mycon);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            cbMaKhoa.DataSource = dt;
            cbMaKhoa.DisplayMember = "MA_Khoa";
            cbMaKhoa.ValueMember = "MA_Khoa";

        }
        private void LayTenGiaoVien()
        {
            if (Mycon.State == ConnectionState.Open) Mycon.Close();
            Mycon.Open();
            SqlCommand cmd = new SqlCommand("SELECT MA_GV FROM GiaoVien WHERE Ma_Khoa = @makhoa", Mycon);
            cmd.Parameters.AddWithValue("@makhoa", cbMaKhoa.SelectedValue.ToString());
            Mycon.Close();
            Load_DGV();
        }

        private void cbMaKhoa_SelectedIndexChanged(object sender, EventArgs e)
        {
            LayTenGiaoVien();
            Load_DGV();
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            if (Mycon.State == ConnectionState.Open) Mycon.Close(); // Sử dụng using để đảm bảo kết nối luôn được đóng
            {
                Mycon.Open();

                // Sử dụng tham số hóa để ngăn chặn SQL Injection
                string sql = "SELECT * FROM GiaoVien WHERE Ma_GV = @MaGV";
                using (SqlCommand cmd = new SqlCommand(sql, Mycon))
                {
                    cmd.Parameters.AddWithValue("@MaGV", txtMaGV.Text);

                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd)) // Sử dụng using cho SqlDataAdapter
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);

                        if (dt.Rows.Count == 0)
                        {
                            MessageBox.Show("Giáo viên không tìm thấy", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            dgvGiaoVien.DataSource = dt;
                            MessageBox.Show("Giáo viên đã tìm thấy", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        Mycon.Close();
                    }
                }
                // Không cần đóng kết nối Mycon ở đây vì using sẽ tự động đóng khi kết thúc khối lệnh
            }

        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (Mycon.State == ConnectionState.Open) Mycon.Close(); // Sử dụng using để đảm bảo kết nối luôn được đóng
            {
                try
                {
                    Mycon.Open();

                    // Kiểm tra sự tồn tại của giáo viên (sử dụng tham số hóa)
                    string sqlCheck = "SELECT COUNT(*) FROM GiaoVien WHERE MA_GV = @MaGV";
                    using (SqlCommand cmdCheck = new SqlCommand(sqlCheck, Mycon))
                    {
                        cmdCheck.Parameters.AddWithValue("@MaGV", txtMaGV.Text);
                        int count = (int)cmdCheck.ExecuteScalar();

                        if (count > 0)
                        {
                            MessageBox.Show("Giáo viên này đã tồn tại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning); // Đổi MessageBoxIcon thành Warning cho phù hợp
                            return; // Thoát khỏi hàm nếu giáo viên đã tồn tại
                        }
                    }

                    // Thêm giáo viên mới (sử dụng tham số hóa)
                    string sqlInsert = @"INSERT INTO GiaoVien (MA_GV, TEN_GV, GIOI_TINH, MA_Khoa, NGAY_SINH)
                            VALUES (@magv, @tengv, @gioitinh, @makhoa, @ngaysinh)";

                    using (SqlCommand cmdInsert = new SqlCommand(sqlInsert, Mycon))
                    {
                        cmdInsert.Parameters.AddWithValue("@magv", txtMaGV.Text);
                        cmdInsert.Parameters.AddWithValue("@tengv", txtTenGV.Text);
                        cmdInsert.Parameters.AddWithValue("@gioitinh", chkGioitinh.Checked);
                        cmdInsert.Parameters.AddWithValue("@makhoa", cbMaKhoa.Text);
                        cmdInsert.Parameters.AddWithValue("@ngaysinh", dtpNgaySinh.Value);

                        cmdInsert.ExecuteNonQuery();

                        MessageBox.Show("Đã lưu thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Load_DGV();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Đã xảy ra lỗi: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    // Ghi log lỗi ở đây (nếu cần)
                }
                // Không cần finally block vì using sẽ tự động đóng kết nối
            }

        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            try
            {
                Mycon.Open(); // Mở kết nối trước khi thực hiện truy vấn

                // Kiểm tra sự tồn tại của giáo viên
                string sqlCheck = "SELECT COUNT(*) FROM GiaoVien WHERE MA_GV = @magv";
                using (SqlCommand cmdCheck = new SqlCommand(sqlCheck, Mycon))
                {
                    cmdCheck.Parameters.AddWithValue("@magv", txtMaGV.Text);
                    int count = (int)cmdCheck.ExecuteScalar();

                    if (count > 0)
                    {
                        
                        int dong = dgvGiaoVien.CurrentRow.Index;
                        string magv = dgvGiaoVien.CurrentRow.Cells["MA_GV"].Value.ToString();

                        // Cập nhật thông tin giáo viên (sử dụng parameterized query)
                        string sqlUpdate = @"UPDATE GiaoVien 
                                 SET TEN_GV = @tengv, GIOI_TINH = @gioitinh, Ma_Khoa = @makhoa, NGAY_SINH = @ngaysinh 
                                 WHERE MA_GV = @magv";
                        using (SqlCommand cmdUpdate = new SqlCommand(sqlUpdate, Mycon))
                        {
                            cmdUpdate.Parameters.AddWithValue("@magv", magv); // Sử dụng mã giáo viên từ DataGridView
                            cmdUpdate.Parameters.AddWithValue("@tengv", txtTenGV.Text);
                            cmdUpdate.Parameters.AddWithValue("@gioitinh", chkGioitinh.Checked /*? "Nam" : "Nữ"*/); // Chuyển đổi giá trị checkbox sang chuỗi
                            cmdUpdate.Parameters.AddWithValue("@makhoa", cbMaKhoa.Text);
                            cmdUpdate.Parameters.AddWithValue("@ngaysinh", dtpNgaySinh.Value);

                            cmdUpdate.ExecuteNonQuery();
                            MessageBox.Show("Cập nhật thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            Load_DGV(); // Tải lại dữ liệu sau khi cập nhật
                        }
                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy giáo viên để cập nhật.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                MessageBox.Show($"Lỗi cơ sở dữ liệu: {sqlEx.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Đã xảy ra lỗi: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Mycon.Close(); // Đảm bảo kết nối luôn được đóng
                btnSua.Enabled = false;
                btnXoa.Enabled = false;
            }

        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (Mycon.State == ConnectionState.Open) Mycon.Close();
            Mycon.Open();
            foreach (DataGridViewRow selectedRow in dgvGiaoVien.SelectedRows)
            {
                string magv = selectedRow.Cells["MA_GV"].Value.ToString();
                string sqlDelete = "delete GiaoVien WHERE MA_GV = @magv";
                SqlCommand cmd = new SqlCommand(sqlDelete, Mycon);
                cmd.Parameters.AddWithValue("@magv", magv);
                cmd.ExecuteNonQuery();
                cmd.Dispose();
            }
            MessageBox.Show("Xóa thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Load_DGV();
            Mycon.Close();
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            DialogResult Traloi;
            Traloi = MessageBox.Show("Bạn có chắc chắn thoát không?", "Trả lời", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (Traloi == DialogResult.OK) Application.Exit();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            txtMaGV.Clear();
            txtTenGV.Clear();
            dtpNgaySinh.ResetText();
            chkGioitinh.Enabled = true;
            btnLuu.Enabled = true;
            btnThem.Enabled = false;
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            txtMaGV.Clear();
            txtTenGV.Clear();
            cbMaKhoa.ResetText();
            btnThem.Enabled = true;
            Load_DGV();
        }
    }
}
