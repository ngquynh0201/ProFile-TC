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
    public partial class FCRSinhVien : Form
    {

        public FCRSinhVien()
        {
            InitializeComponent();
        }

        

        private void crystalReportViewer1_Load_1(object sender, EventArgs e)
        {
            SqlConnection Mycon = new SqlConnection(@"Data Source=PC\SQLEXPRESS;Initial Catalog=QuanLySV;User ID=sa;Password=123;");
            string sql = "Select * from SinhVien";
            //Tạo đối tượng SqlDataAdapter
            SqlDataAdapter Adapter = new SqlDataAdapter(sql, Mycon);
            //Tạo đối tượng DataTable
            DataTable DTable = new DataTable();
            //Đổ dữ liệu từ SqlDataAdapter vào DataTable
            Adapter.Fill(DTable);
            //Khởi tạo đối tượng Report
            CRSinhVien RPSV = new CRSinhVien();
            //Đổ dữ liệu từ DataTable vào Report
            RPSV.SetDataSource(DTable);
            //Đưa dữ liệu lên đối tượng Crystal Report trên Form
            crystalReportViewer1.ReportSource = RPSV;
        }
    }
}
