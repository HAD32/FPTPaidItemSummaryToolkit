using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DAL;
using DTO;
namespace FPTPaidItemSummaryToolkit
{
    public partial class GUI_DetailMonthlyRecord : Form
    {
        List<MonthlyPaidItemRecord> mpirList;
        public GUI_DetailMonthlyRecord(string staffAccount, List<MonthlyPaidItemRecord> mpirList)
        {
            InitializeComponent();
            this.mpirList = mpirList;
            lblLecturer.Text = staffAccount;
            generateData();
        }

        private void generateData()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Hệ đào tạo");
            dt.Columns.Add("Cơ sở");
            dt.Columns.Add("Tổng lương");
            foreach(MonthlyPaidItemRecord mpir in mpirList)
            {
                DataRow row = dt.NewRow();
                row["Hệ đào tạo"] = mpir.AcadLv.Code;
                row["Cơ sở"] = mpir.Campus;
                row["Tổng lương"] = mpir.mtpirList[0].Sum;
                dt.Rows.Add(row);
            }
            dtgDisplay.DataSource = dt;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
