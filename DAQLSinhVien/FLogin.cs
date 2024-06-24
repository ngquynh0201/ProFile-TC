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
    public partial class FLogin : Form
    {
        SqlConnection Mycon = new SqlConnection(@"Data Source=PC\SQLEXPRESS;Initial Catalog=QuanLySV;User ID=sa;Password=123;");
        public FLogin()
        {
            InitializeComponent();
        }
        public static string QUYEN = "";
        private string LAYQUYEN()
        {
            string Q = "";
            try
            {
                if (Mycon.State == ConnectionState.Closed)
                    Mycon.Open();
                string sql = "select QUYEN from TAIKHOAN where (TEN_TK = '" + txtTenDN.Text + "') and(MAT_KHAU = '" + txtMK.Text + "')";
                SqlDataAdapter Myadapter = new SqlDataAdapter(sql, Mycon);
                DataTable MyTable = new DataTable();
                Myadapter.Fill(MyTable);
                if (MyTable != null)
                {
                    foreach (DataRow MyRow in MyTable.Rows)
                        Q = MyRow["QUYEN"].ToString();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Lỗi đăng nhập");
            }
            finally
            {

                Mycon.Close();
            }
            return Q;
        }

        private void btnDN_Click(object sender, EventArgs e)
        {
            if (Mycon.State == ConnectionState.Closed)
                Mycon.Open();
            QUYEN = LAYQUYEN();
            Program.quyen = QUYEN;
            if (QUYEN != "")
            {
                MessageBox.Show("Ban đã đăng nhập với quyền " + QUYEN, "Thông báo");

                FMain f = new FMain();
                this.Hide();
                f.ShowDialog();
                this.Show();

            }
            else
            {
                MessageBox.Show("Tên đăng nhập hoặc mật khẩu không đúng ? ", "Thông báo", MessageBoxButtons.OKCancel,
               MessageBoxIcon.Information);

                txtTenDN.ResetText();
                txtMK.ResetText();
                this.txtTenDN.Focus();
            }
            Mycon.Close();
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            DialogResult Traloi;
            Traloi = MessageBox.Show("Bạn có chắc chắn thoát không?", "Trả lời", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (Traloi == DialogResult.OK) Application.Exit();
        }
    }
}
