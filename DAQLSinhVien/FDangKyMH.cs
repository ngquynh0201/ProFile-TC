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
    public partial class FDangKyMH : Form
    {
        SqlConnection Mycon = new SqlConnection(@"Data Source=PC\SQLEXPRESS;Initial Catalog=QuanLySV;User ID=sa;Password=123;");
        public FDangKyMH()
        {
            InitializeComponent();
        }

        private void FDangKyMH_Load(object sender, EventArgs e)
        {
            Load_DGV();
            LoadDL_MaSV();
            LoadDL_MaMH();
        }

        private void LoadDL_MaSV()
        {
            string sql = "select MA_SV from SinhVien";
            SqlDataAdapter adapter = new SqlDataAdapter(sql, Mycon);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            cmbMaSV.DataSource = dt;
            cmbMaSV.DisplayMember = "MA_SV";
            cmbMaSV.ValueMember = "MA_SV";
        }

        private void LoadDL_MaMH()
        {
            string sql = "select MA_MH from MonHoc";
            SqlDataAdapter adapter = new SqlDataAdapter(sql, Mycon);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            cmbMaMH.DataSource = dt;
            cmbMaMH.DisplayMember = "MA_MH";
            cmbMaMH.ValueMember = "MA_MH";
            LayTenMH();
        }
        private void LayTenMH()
        {
            if (Mycon.State == ConnectionState.Open) Mycon.Close();
            Mycon.Open();
            SqlCommand cmd = new SqlCommand("SELECT TEN_MH FROM MONHOC WHERE MA_MH = @mamh", Mycon);
            cmd.Parameters.AddWithValue("@mamh", cmbMaMH.SelectedValue.ToString());
            SqlDataReader rd = cmd.ExecuteReader();
            if (rd.Read())
            {
                txtTenMH.Text = rd.GetString(0);
            }
            Mycon.Close();
        }

        private void Load_DGV()
        {
            if (Mycon.State == ConnectionState.Open) Mycon.Close();
            Mycon.Open();
            string sql = "select * from DangKyHoc";
            SqlDataAdapter AD = new SqlDataAdapter(sql, Mycon);
            DataTable dt = new DataTable();
            AD.Fill(dt);
            dgvDKH.DataSource = dt;
            dgvDKH.Columns[0].HeaderText = "Mã đăng ký";
            dgvDKH.Columns[1].HeaderText = "Mã sinh viên";
            dgvDKH.Columns[2].HeaderText = "Mã môn học";
            dgvDKH.Columns[3].HeaderText = "Tên môn học";
            dgvDKH.Columns[4].HeaderText = "Ngày dạy học";
            dgvDKH.AllowUserToAddRows = false;
            dgvDKH.EditMode = DataGridViewEditMode.EditProgrammatically;
            Mycon.Close();

        }


        private void cmbMaMH_SelectedIndexChanged(object sender, EventArgs e)
        {
            LayTenMH();
            Load_DGV();
        }



        private void btnDK_Click(object sender, EventArgs e)
        {
            {
                try
                {
                    if (Mycon.State == ConnectionState.Open)
                        Mycon.Close();
                    Mycon.Open();

                    string sql = "select count(*) from DangKyHoc where MA_DK = '" + txtMaDK.Text + "'";
                    SqlCommand cmd = new SqlCommand(sql, Mycon);
                    int count = (int)cmd.ExecuteScalar();

                    if (count > 0)
                    {
                        MessageBox.Show("Đăng Ký Học đã tồn tại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        string sqlinsert = @"insert into DangKyHoc (MA_SV,MA_MH,TEN_MH,NgayDK)
                        values ( @masv, @mamh, @tenmh ,@ngaydangky)";
                        SqlCommand cmd1 = new SqlCommand(sqlinsert, Mycon);
                        cmd1.Parameters.AddWithValue("@masv", cmbMaSV.Text);
                        cmd1.Parameters.AddWithValue("@mamh", cmbMaMH.Text);
                        cmd1.Parameters.AddWithValue("@tenmh", txtTenMH.Text);
                        cmd1.Parameters.AddWithValue("@ngaydangky", dtpNgayDK.Text);
                        cmd1.ExecuteNonQuery();
                        cmd1.Dispose();
                        MessageBox.Show("Đã lưu thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                }
            }
        }
        int dong;
        private void dgvDKH_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            dong = e.RowIndex;
            txtMaDK.Text = dgvDKH.Rows[dong].Cells[0].Value.ToString();
            cmbMaSV.Text = dgvDKH.Rows[dong].Cells[1].Value.ToString();
            cmbMaMH.Text = dgvDKH.Rows[dong].Cells[2].Value.ToString();
            txtTenMH.Text = dgvDKH.Rows[dong].Cells[3].Value.ToString();
            dtpNgayDK.Text = dgvDKH.Rows[dong].Cells[4].Value.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            txtMaDK.Clear();
            txtTenMH.Clear();
            cmbMaMH.ResetText();
            cmbMaSV.ResetText();
            dtpNgayDK.ResetText();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DialogResult Traloi;
            Traloi = MessageBox.Show("Bạn có chắc chắn thoát không?", "Trả lời", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (Traloi == DialogResult.OK) Application.Exit();
        }
    }
}

 