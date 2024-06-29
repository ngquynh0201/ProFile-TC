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
    public partial class FKhoa : Form
    {
        SqlConnection Mycon = new SqlConnection(@"Data Source=PC\SQLEXPRESS;Initial Catalog=QuanLySV;User ID=sa;Password=123;");

        public FKhoa()
        {
            InitializeComponent();
        }

        private void FKhoa_Load(object sender, EventArgs e)
        {
            Load_DGV();
        }

        private void Load_DGV()
        {
            chkGioitinh.Enabled = true;
            string sql = "Select * FROM Khoa";
            SqlDataAdapter ad = new SqlDataAdapter(sql, Mycon);
            DataTable dt = new DataTable();
            ad.Fill(dt);
            dgvKhoa.DataSource = dt;
            dgvKhoa.Columns[0].HeaderText = "Mã Khoa";
            dgvKhoa.Columns[1].HeaderText = "Tên Khoa";
            dgvKhoa.Columns[2].HeaderText = "Trưởng Khoa";
            dgvKhoa.Columns[3].HeaderText = "Giới Tính";
            dgvKhoa.Columns[4].HeaderText = "Số Điện Thoại";
            dgvKhoa.AllowUserToAddRows = false;
            dgvKhoa.EditMode = DataGridViewEditMode.EditProgrammatically;
        }

        private void dgvKhoa_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            txtMaKhoa.Text = dgvKhoa.CurrentRow.Cells["MA_KHOA"].Value.ToString();
            txtTenKhoa.Text = dgvKhoa.CurrentRow.Cells["TEN_KHOA"].Value.ToString();
            txtTruongKhoa.Text = dgvKhoa.CurrentRow.Cells["TRUONG_KHOA"].Value.ToString();
            if (dgvKhoa.CurrentRow.Cells["GIOI_TINH"].Value.ToString() == "True")
                chkGioitinh.Checked = true;
            else chkGioitinh.Checked = false;
            txtSDT.Text = dgvKhoa.CurrentRow.Cells["DIEN_THOAI"].Value.ToString();
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            if (Mycon.State == ConnectionState.Closed)
                Mycon.Open();
            string sql = "SELECT * FROM Khoa WHERE (MA_KHOA = '" + txtMaKhoa.Text + "') ";
            SqlDataAdapter adapter = new SqlDataAdapter(sql, Mycon);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            if (dt.Rows.Count == 0)
            {
                MessageBox.Show("Mã khoa không  tìm thấy", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                dgvKhoa.DataSource = dt;
                MessageBox.Show("Mã khoa đã tìm thấy", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            Mycon.Close();
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

                // Kiểm tra sự tồn tại của khoa (sử dụng parameterized query)
                string sqlCheck = "SELECT COUNT(*) FROM Khoa WHERE MA_KHOA = @makhoa";
                using (SqlCommand cmdCheck = new SqlCommand(sqlCheck, Mycon))
                {
                    cmdCheck.Parameters.AddWithValue("@makhoa", txtMaKhoa.Text);
                    int count = (int)cmdCheck.ExecuteScalar();

                    if (count > 0)
                    {
                        MessageBox.Show("Khoa này đã tồn tại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return; // Thoát khỏi phương thức nếu khoa đã tồn tại
                    }
                }

                // Thêm khoa mới (sử dụng parameterized query)
                string sqlInsert = @"INSERT INTO Khoa (MA_KHOA, TEN_KHOA, DIEN_THOAI, GIOI_TINH, TRUONG_KHOA)
                         VALUES (@makhoa, @tenkhoa, @dienthoai, @gioitinh, @truongkhoa)";
                using (SqlCommand cmdInsert = new SqlCommand(sqlInsert, Mycon))
                {
                    cmdInsert.Parameters.AddWithValue("@makhoa", txtMaKhoa.Text);
                    cmdInsert.Parameters.AddWithValue("@tenkhoa", txtTenKhoa.Text);
                    cmdInsert.Parameters.AddWithValue("@truongkhoa", txtTruongKhoa.Text);
                    cmdInsert.Parameters.AddWithValue("@gioitinh", chkGioitinh.Checked ); // Chuyển sang chuỗi
                    cmdInsert.Parameters.AddWithValue("@dienthoai", txtSDT.Text);

                    cmdInsert.ExecuteNonQuery();
                    MessageBox.Show("Đã lưu thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Load_DGV();
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
                Mycon.Close();
                btnLuu.Enabled = false;
                btnThem.Enabled = true;
            }

        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            {
                try
                {
                    if (Mycon.State == ConnectionState.Open)
                        Mycon.Close();
                    Mycon.Open();

                    string sql = "select count(*) from Khoa where Ma_Khoa = '" + txtMaKhoa.Text + "'";
                    SqlCommand cmd = new SqlCommand(sql, Mycon);
                    int count = (int)cmd.ExecuteScalar();

                    if (count > 0)
                    {

                        int dong = dgvKhoa.CurrentRow.Index;
                        string makhoa = dgvKhoa.Rows[dong].Cells[0].Value.ToString();
                        string sqlupdate = @"update Khoa set Ma_Khoa = @makhoa, TEN_KHOA = @tenkhoa, GIOI_TINH=
                            @gioitinh, DIEN_THOAI = @dienthoai, TRUONG_KHOA= @truongkhoa
                                            WHERE Ma_Khoa = '" + makhoa + "'";
                        SqlCommand cmd1 = new SqlCommand(sqlupdate, Mycon);
                        cmd1.Parameters.AddWithValue("@makhoa", txtMaKhoa.Text);
                        cmd1.Parameters.AddWithValue("@tenkhoa", txtTenKhoa.Text);
                        cmd1.Parameters.AddWithValue("@truongkhoa", txtTruongKhoa.Text);
                        cmd1.Parameters.AddWithValue("@gioitinh", chkGioitinh.Checked);
                        cmd1.Parameters.AddWithValue("@dienthoai", txtSDT.Text);
                        cmd1.ExecuteNonQuery();
                        cmd1.Dispose();
                        MessageBox.Show("Cập nhật thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Load_DGV();
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Đã xảy ra lỗi: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    Mycon.Close();
                    btnSua.Enabled = false;
                    btnXoa.Enabled = false;
                }
            }

        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (Mycon.State == ConnectionState.Open) Mycon.Close();
            Mycon.Open();
            foreach (DataGridViewRow selectedRow in dgvKhoa.SelectedRows)
            {
                string makhoa = selectedRow.Cells["Ma_Khoa"].Value.ToString();
                string sqlDelete = "delete Khoa WHERE Ma_Khoa = @makhoa";
                SqlCommand cmd = new SqlCommand(sqlDelete, Mycon);
                cmd.Parameters.AddWithValue("@makhoa", makhoa);
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
            txtMaKhoa.Clear();
            txtTenKhoa.Clear();
            txtTruongKhoa.Clear();
            chkGioitinh.Enabled = true;
            txtSDT.Clear();
            btnLuu.Enabled = true;
            btnThem.Enabled = false;
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            txtMaKhoa.Clear();
            txtTenKhoa.Clear();
            txtTruongKhoa.Clear();
            txtSDT.Clear();
            Load_DGV();
        }
    }
}
