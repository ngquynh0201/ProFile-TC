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
    public partial class FDayHoc : Form
    {
        SqlConnection Mycon = new SqlConnection(@"Data Source=PC\SQLEXPRESS;Initial Catalog=QuanLySV;User ID=sa;Password=123;");

        public FDayHoc()
        {
            InitializeComponent();
        }

        private void FDayHoc_Load(object sender, EventArgs e)
        {
            Load_DGV();
            LoadDL_Combobox();
            LoadDLSV_Combobox();
        }

        private void Load_DGV()
        {
            if (Mycon.State == ConnectionState.Open) Mycon.Close();
            Mycon.Open();
            string sql = "select * from Dayhoc";
            SqlDataAdapter AD = new SqlDataAdapter(sql, Mycon);
            DataTable dt = new DataTable();
            AD.Fill(dt);
            dgvDayHoc.DataSource = dt;
            dgvDayHoc.Columns[0].HeaderText = "ID dạy học";
            dgvDayHoc.Columns[1].HeaderText = "Mã giáo viên";
            dgvDayHoc.Columns[2].HeaderText = "Mã môn học";
            dgvDayHoc.AllowUserToAddRows = false;
            dgvDayHoc.EditMode = DataGridViewEditMode.EditProgrammatically;
            Mycon.Close();
        }
        int dong;
        private void dgvDayHoc_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            {
                dong = e.RowIndex;
                txtIdDayHoc.Text = dgvDayHoc.Rows[dong].Cells[0].Value.ToString();
                cbMaGV.Text = dgvDayHoc.Rows[dong].Cells[1].Value.ToString();
                cbMaMH.Text = dgvDayHoc.Rows[dong].Cells[2].Value.ToString();
            }
        }

        private void LoadDL_Combobox()
        {
            string sql = "select Ma_GV from GiaoVien";
            SqlDataAdapter adapter = new SqlDataAdapter(sql, Mycon);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            cbMaGV.DataSource = dt;
            cbMaGV.DisplayMember = "Ma_GV";
            cbMaGV.ValueMember = "Ma_GV";
            
        }

        private void LoadDLSV_Combobox()
        {
            string sql = "select MA_MH from MonHoc";
            SqlDataAdapter adapter = new SqlDataAdapter(sql, Mycon);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            cbMaMH.DataSource = dt;
            cbMaMH.DisplayMember = "MA_MH";
            cbMaMH.ValueMember = "MA_MH";
            
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            txtIdDayHoc.Enabled = false;
            btnLuu.Enabled = true;
            btnThem.Enabled = false;
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            try
            {
                if (Mycon.State == ConnectionState.Open)
                    Mycon.Close();
                Mycon.Open();

                string sql = "select count(*) from DayHoc where ID_DayHoc = '" + txtIdDayHoc.Text + "'";
                SqlCommand cmd = new SqlCommand(sql, Mycon);
                int count = (int)cmd.ExecuteScalar();

                if (count > 0)
                {
                    MessageBox.Show("Dậy học đã tồn tại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    string sqlinsert = @"insert into DayHoc (MA_MH,MA_GV)
                            values ( @mamh, @magv)";
                    SqlCommand cmd1 = new SqlCommand(sqlinsert, Mycon);

                    cmd1.Parameters.AddWithValue("@mamh", cbMaMH.Text);
                    cmd1.Parameters.AddWithValue("@magv", cbMaGV.Text);

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


        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (Mycon.State == ConnectionState.Open) Mycon.Close();
            Mycon.Open();
            foreach (DataGridViewRow selectedRow in dgvDayHoc.SelectedRows)
            {
                string iddayhoc = selectedRow.Cells["ID_DAYHOC"].Value.ToString();
                string sqlDelete = "delete DayHoc WHERE ID_DAYHOC = @iddayhoc";
                SqlCommand cmd = new SqlCommand(sqlDelete, Mycon);
                cmd.Parameters.AddWithValue("@iddayhoc", iddayhoc);
                cmd.ExecuteNonQuery();
                cmd.Dispose();
            }
            MessageBox.Show("Xóa thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Load_DGV();
            Mycon.Close();
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            txtIdDayHoc.Clear();
            cbMaGV.ResetText();
            cbMaMH.ResetText();
            btnThem.Enabled = true;
            Load_DGV();
        }
    }
}
