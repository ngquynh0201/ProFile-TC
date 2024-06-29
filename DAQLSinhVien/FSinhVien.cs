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
    public partial class FSinhVien : Form
    {
        SqlConnection Mycon = new SqlConnection(@"Data Source=PC\SQLEXPRESS;Initial Catalog=QuanLySV;User ID=sa;Password=123;");

        public FSinhVien()
        {
            InitializeComponent();
        }

        private void FSinhVien_Load(object sender, EventArgs e)
        {
            Load_DGV();
            LoadDL_Combobox();
            LoadDLNamHoc_Combobox();
        }
        private void Load_DGV()
        {
            
            chkGioitinh.Enabled = true;
            if (Mycon.State == ConnectionState.Open) Mycon.Close();
            Mycon.Open();
            string sql = "select * from SinhVien";
            SqlDataAdapter AD = new SqlDataAdapter(sql, Mycon);
            DataTable dt = new DataTable();
            AD.Fill(dt);
            dgvSinhVien.DataSource = dt;
            dgvSinhVien.Columns[0].HeaderText = "Mã sinh viên";
            dgvSinhVien.Columns[1].HeaderText = "Mã Lớp";
            dgvSinhVien.Columns[2].HeaderText = "Họ và tên";
            dgvSinhVien.Columns[3].HeaderText = "Địa chi";
            dgvSinhVien.Columns[4].HeaderText = "Giới Tính";
            dgvSinhVien.Columns[5].HeaderText = "Số Điện Thoại";
            dgvSinhVien.Columns[6].HeaderText = "Ngày sinh";
            dgvSinhVien.Columns[7].HeaderText = "Năm Học";
            dgvSinhVien.Columns[8].HeaderText = "Học Kỳ";
            dgvSinhVien.AllowUserToAddRows = false;
            dgvSinhVien.EditMode = DataGridViewEditMode.EditProgrammatically;
            dgvSinhVien.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvSinhVien.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            Mycon.Close();
        }

        private void dgvSinhVien_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            txtMaSV.Text = dgvSinhVien.CurrentRow.Cells["MA_SV"].Value.ToString();
            cbMaLop.Text = dgvSinhVien.CurrentRow.Cells["MA_LOP"].Value.ToString();
            txtTenSV.Text = dgvSinhVien.CurrentRow.Cells["TEN_SV"].Value.ToString();
            txtDiaChi.Text = dgvSinhVien.CurrentRow.Cells["DIA_CHI"].Value.ToString();
            if (dgvSinhVien.CurrentRow.Cells["GIOI_TINH"].Value.ToString() == "True")
                chkGioitinh.Checked = true;
            else chkGioitinh.Checked = false;

            txtSDT.Text = dgvSinhVien.CurrentRow.Cells["DIEN_THOAI"].Value.ToString();
            dtpNgaysinh.Text = dgvSinhVien.CurrentRow.Cells["NGAY_SINH"].Value.ToString();

            cbNamHoc.Text = dgvSinhVien.CurrentRow.Cells["NAM_HOC"].Value.ToString();
            txtHK.Text = dgvSinhVien.CurrentRow.Cells["HOC_KY"].Value.ToString();
            btnSua.Enabled = true;
            btnXoa.Enabled = true;
        }

        private void LoadDL_Combobox()
        {
            string sql = "select MA_LOP from Lop";
            SqlDataAdapter adapter = new SqlDataAdapter(sql, Mycon);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            cbMaLop.DataSource = dt;
            cbMaLop.DisplayMember = "MA_LOP";
            cbMaLop.ValueMember = "MA_LOP";
            LayTenSinhVien();

        }
        private void LayTenSinhVien()
        {
            if (Mycon.State == ConnectionState.Open) Mycon.Close();
            Mycon.Open();
            SqlCommand cmd = new SqlCommand("SELECT MA_SV FROM SinhVien WHERE MA_LOP = @malop", Mycon);
            cmd.Parameters.AddWithValue("@malop", cbMaLop.SelectedValue.ToString());
            SqlDataReader rd = cmd.ExecuteReader();
            if (rd.Read())
            {
                txtMaSV.Text = rd.GetString(0);
            }
            Mycon.Close();
            Load_DGV();
        }

        private void cbMaLop_SelectedIndexChanged(object sender, EventArgs e)
        {
            Load_DGV();
            LayTenSinhVien();
        }

        private void LoadDLNamHoc_Combobox()
        {
            string sql = "select NAM_HOC from NIENKHOA";
            SqlDataAdapter adapter = new SqlDataAdapter(sql, Mycon);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            cbNamHoc.DataSource = dt;
            cbNamHoc.DisplayMember = "NAM_HOC";
            cbNamHoc.ValueMember = "NAM_HOC";
            LayTenHocKy();
        }
        private void LayTenHocKy()
        {
            if (Mycon.State == ConnectionState.Open) Mycon.Close();
            Mycon.Open();
            SqlCommand cmd = new SqlCommand("SELECT HOC_KY FROM NIENKHOA WHERE NAM_HOC = @namhoc", Mycon);
            cmd.Parameters.AddWithValue("@namhoc", cbNamHoc.SelectedValue.ToString());
            SqlDataReader rd = cmd.ExecuteReader();
            if (rd.Read())
            {
                txtHK.Text = rd.GetString(0);
            }
            Mycon.Close();
            Load_DGV() ;
        }

        private void cbNamHoc_SelectedIndexChanged(object sender, EventArgs e)
        {
            Load_DGV();
            LayTenHocKy();
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            try
            {
                // Kiểm tra trạng thái kết nối và mở nếu cần
                if (Mycon.State == ConnectionState.Closed)
                {
                    Mycon.Open();
                }

                // Sử dụng parameterized query để ngăn chặn SQL Injection
                string sql = "SELECT * FROM SinhVien WHERE MA_SV = @masv";

                using (SqlCommand cmd = new SqlCommand(sql, Mycon))
                {
                    cmd.Parameters.AddWithValue("@masv", txtMaSV.Text);

                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);

                        if (dt.Rows.Count == 0)
                        {
                            MessageBox.Show("Sinh viên không tìm thấy", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            dgvSinhVien.DataSource = dt;
                            MessageBox.Show("Sinh viên đã tìm thấy", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
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
                // Đóng kết nối trong khối finally để đảm bảo kết nối luôn được đóng
                Mycon.Close();
            }

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

                // Kiểm tra xem sinh viên đã tồn tại chưa (sử dụng parameterized query)
                string sqlCheck = "SELECT COUNT(*) FROM SinhVien WHERE MA_SV = @masv";
                using (SqlCommand cmdCheck = new SqlCommand(sqlCheck, Mycon))
                {
                    cmdCheck.Parameters.AddWithValue("@masv", txtMaSV.Text);

                    int count = (int)cmdCheck.ExecuteScalar();

                    if (count > 0)
                    {
                        MessageBox.Show("Sinh viên này đã tồn tại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return; // Thoát khỏi phương thức nếu sinh viên đã tồn tại
                    }
                }

                // Thực hiện thêm sinh viên mới (sử dụng parameterized query)
                string sqlInsert = @"INSERT INTO SinhVien (MA_SV, MA_LOP, TEN_SV, DIA_CHI, GIOI_TINH, DIEN_THOAI, NGAY_SINH, NAM_HOC, HOC_KY)
                         VALUES (@masv, @malop, @tensv, @diachi, @gioitinh, @dienthoai, @ngaysinh, @namhoc, @hocky)";

                using (SqlCommand cmdInsert = new SqlCommand(sqlInsert, Mycon))
                {
                    // Validate input data (Thêm đoạn code kiểm tra dữ liệu đầu vào ở đây)

                    cmdInsert.Parameters.AddWithValue("@masv", txtMaSV.Text);
                    cmdInsert.Parameters.AddWithValue("@malop", cbMaLop.Text);
                    cmdInsert.Parameters.AddWithValue("@tensv", txtTenSV.Text);
                    cmdInsert.Parameters.AddWithValue("@diachi", txtDiaChi.Text);
                    cmdInsert.Parameters.AddWithValue("@gioitinh", chkGioitinh.Checked /*? "Nam" : "Nữ"*/); // Chuyển đổi checkbox sang chuỗi
                    cmdInsert.Parameters.AddWithValue("@dienthoai", txtSDT.Text);
                    cmdInsert.Parameters.AddWithValue("@ngaysinh", dtpNgaysinh.Value);
                    cmdInsert.Parameters.AddWithValue("@namhoc", cbNamHoc.Text);
                    cmdInsert.Parameters.AddWithValue("@hocky", txtHK.Text);

                    cmdInsert.ExecuteNonQuery();
                    MessageBox.Show("Đã lưu thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Load_DGV();
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
                // Đảm bảo kết nối luôn được đóng
                Mycon.Close();
                btnLuu.Enabled = false;
                btnThem.Enabled = true;
            }

        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            try
            {
                // Mở kết nối (nếu chưa mở)
                if (Mycon.State != ConnectionState.Open)
                {
                    Mycon.Open();
                }

                // Kiểm tra sự tồn tại của sinh viên (sử dụng parameterized query)
                string sqlCheck = "SELECT COUNT(*) FROM SinhVien WHERE MA_SV = @masv";
                using (SqlCommand cmdCheck = new SqlCommand(sqlCheck, Mycon))
                {
                    cmdCheck.Parameters.AddWithValue("@masv", txtMaSV.Text);
                    int count = (int)cmdCheck.ExecuteScalar();

                    if (count > 0)
                    {
                        // Lấy thông tin sinh viên từ DataGridView (đảm bảo sử dụng tên cột chính xác)
                        int dong = dgvSinhVien.CurrentRow.Index;
                        string masv = dgvSinhVien.CurrentRow.Cells["MA_SV"].Value.ToString();

                        // Thực hiện cập nhật thông tin sinh viên (sử dụng parameterized query)
                        string sqlUpdate = @"UPDATE SinhVien 
                                 SET MA_LOP = @malop, TEN_SV = @tensv, DIA_CHI = @diachi, 
                                     GIOI_TINH = @gioitinh, DIEN_THOAI = @dienthoai, 
                                     NGAY_SINH = @ngaysinh, NAM_HOC = @namhoc, HOC_KY = @hocky
                                 WHERE MA_SV = @masv";

                        using (SqlCommand cmdUpdate = new SqlCommand(sqlUpdate, Mycon))
                        {
                            // Gán giá trị cho các tham số
                            cmdUpdate.Parameters.AddWithValue("@masv", masv); // Sử dụng mã SV từ DataGridView
                            cmdUpdate.Parameters.AddWithValue("@malop", cbMaLop.Text);
                            cmdUpdate.Parameters.AddWithValue("@tensv", txtTenSV.Text);
                            cmdUpdate.Parameters.AddWithValue("@diachi", txtDiaChi.Text);
                            cmdUpdate.Parameters.AddWithValue("@gioitinh", chkGioitinh.Checked );
                            cmdUpdate.Parameters.AddWithValue("@dienthoai", txtSDT.Text);
                            cmdUpdate.Parameters.AddWithValue("@ngaysinh", dtpNgaysinh.Value);
                            cmdUpdate.Parameters.AddWithValue("@namhoc", cbNamHoc.Text);
                            cmdUpdate.Parameters.AddWithValue("@hocky", txtHK.Text);
                            cmdUpdate.ExecuteNonQuery();
                            MessageBox.Show("Cập nhật thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            Load_DGV(); // Tải lại dữ liệu sau khi cập nhật
                        }
                    }
                    else
                    {
                        // Nếu sinh viên không tồn tại, bạn có thể thông báo cho người dùng
                        MessageBox.Show("Không tìm thấy sinh viên để cập nhật.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                // Đảm bảo kết nối luôn được đóng
                Mycon.Close();
                btnXoa.Enabled = false;
                btnSua.Enabled = false;
            }


        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (Mycon.State == ConnectionState.Open) Mycon.Close();
            Mycon.Open();
            foreach (DataGridViewRow selectedRow in dgvSinhVien.SelectedRows)
            {
                string masv = selectedRow.Cells["MA_SV"].Value.ToString();
                string sqlDelete = "delete SinhVien WHERE MA_SV = @masv";
                SqlCommand cmd = new SqlCommand(sqlDelete, Mycon);
                cmd.Parameters.AddWithValue("@masv", masv);
                cmd.ExecuteNonQuery();
                cmd.Dispose();
            }
            MessageBox.Show("Xóa thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Load_DGV();
            Mycon.Close();
            btnXoa.Enabled = false;
            btnSua.Enabled = false;
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            DialogResult Traloi;
            Traloi = MessageBox.Show("Bạn có chắc chắn thoát không?", "Trả lời", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (Traloi == DialogResult.OK) Application.Exit();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            txtMaSV.Clear();
            txtTenSV.Clear();
            txtSDT.Clear();
            txtDiaChi.Clear();
            txtHK.Clear(); 
            chkGioitinh.Checked = true;
            btnLuu.Enabled = true;
           
            btnThem.Enabled = false;
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            
            txtMaSV.Clear();
            txtTenSV.Clear();
            dtpNgaysinh.ResetText();
            txtSDT.Clear();
            txtDiaChi.Clear();
            txtHK.Clear();
            cbMaLop.ResetText();
            cbNamHoc.ResetText();
            btnLuu.Enabled = true;
            Load_DGV();
        }
    }
}
