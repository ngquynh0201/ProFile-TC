using System;
using System.Windows.Forms;

namespace DAQLSinhVien
{
    public partial class FMain : Form
    {
        public FMain()
        {
            InitializeComponent();
        }

        

        private void FMain_Load(object sender, EventArgs e)
        {
            phanquyen();
        }
        private void phanquyen()
        {
            switch (Program.quyen)
            {
                case "Sinh viên":
                    
                    adminToolStripMenuItem.Text = "Sinh viên";
                    thôngTinKhoaToolStripMenuItem.Enabled = false;
                    ThôngtinGiaoVienToolStripMenuItem.Enabled = false;
                    thôngTinKhoaToolStripMenuItem.Enabled = false;
                    inDanhSáchGiáoViênToolStripMenuItem.Enabled = false;
                    inDanhSáchKhoaToolStripMenuItem.Enabled=false;
                    inDanhSáchSinhViênToolStripMenuItem.Enabled = false;
                    inDanhSáchKhoaToolStripMenuItem.Enabled = false; 
                    inDanhSáchĐiểmToolStripMenuItem.Enabled = false;
                    danhSáchMônHọcToolStripMenuItem.Enabled = false;
                    inDanhSáchMônHọcToolStripMenuItem.Enabled = false;
                    nhậpĐiểmSvToolStripMenuItem1.Enabled = false;
                    quảnLýKhoaToolStripMenuItem.Visible = false;
                    quảnLýGiáoViênToolStripMenuItem.Visible = false;
                    báoCáoToolStripMenuItem.Visible = false;
                    quảnLýKhoaToolStripMenuItem.Visible =false;
                    quảnLýMônHọcToolStripMenuItem.Visible = false;
                    break;
                case "Giáo viên":
                    adminToolStripMenuItem.Text = "Giáo Viên";
                    thôngTinKhoaToolStripMenuItem.Enabled = false;
                    inDanhSáchKhoaToolStripMenuItem.Enabled = false;
                    inDanhSáchMônHọcToolStripMenuItem.Enabled = false;
                    danhSáchMônHọcToolStripMenuItem.Enabled = false;
                    inDanhSáchSinhViênToolStripMenuItem.Enabled = false;
                    inDanhSáchGiáoViênToolStripMenuItem.Enabled = false;
                    đăngKýMônHọcToolStripMenuItem.Enabled = false;
                    ThôngtinGiaoVienToolStripMenuItem.Enabled = false;
                    quảnLýMônHọcToolStripMenuItem.Visible = false ;
                    quảnLýKhoaToolStripMenuItem.Visible = false;
                    inDanhSáchGiáoViênToolStripMenuItem .Visible = false;
                    inDanhSáchKhoaToolStripMenuItem .Visible = false;
                    inDanhSáchMônHọcToolStripMenuItem .Visible = false;
                    inDanhSáchSinhViênToolStripMenuItem.Visible = false;
                    break;
                case "Quản trị":
                    adminToolStripMenuItem.Text = "Quản trị";
                    break;
            }
        }

       

        private void đăngXuấtToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            FLogin FrmLogin = new FLogin();
            FrmLogin.Show();
            this.Hide();
        }

        private void thôngTinGiáoViênToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            FGiaoVien fGiaoVien = new FGiaoVien();
            fGiaoVien.Show();
        }

        private void nhậpĐiểmSinhViênToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FNhapDiem fNhapDiem = new FNhapDiem();
            fNhapDiem.Show();
        }

        private void thôngTinKhoaToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            FKhoa fKhoa = new FKhoa();
            fKhoa.Show();
        }

        private void thôngTinMônHọcToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FMonHoc fMonHoc = new FMonHoc();
            fMonHoc.Show();
        }

        private void thôngTinSinhVienToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FSinhVien FrmSinhVien = new FSinhVien();
            FrmSinhVien.Show();
        }

        private void đăngKýMônHọcToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            FDangKyMH fDangKyMH = new FDangKyMH();
            fDangKyMH.Show();
        }

        private void inDanhSáchSinhViênToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            FCRSinhVien fCRSinhVien = new FCRSinhVien();
            fCRSinhVien.Show();
        }

        private void inDanhSáchGiáoViênToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            FCRGiaoVien fCRGiaoVien = new FCRGiaoVien();
            fCRGiaoVien.Show();
        }

        private void inDanhSáchKhoaToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            FCRKhoa fCRKhoa = new FCRKhoa();
            fCRKhoa.Show();
        }

        private void inDanhSáchMônHọcToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            FCRMonHoc fCRMonHoc = new FCRMonHoc();
            fCRMonHoc.Show();
        }

        private void inDanhSáchĐiểmToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            FCRDiem fCRDiem = new FCRDiem();
            fCRDiem.Show();
        }
    }
}
