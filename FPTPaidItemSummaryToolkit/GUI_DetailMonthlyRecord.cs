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
            this.dtgDisplay.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Xuất trang tính hiện tại ra file Excel?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                SaveFileDialog saveDialog = new SaveFileDialog();
                saveDialog.Title = "Xuất ra file Excel";
                saveDialog.FileName = lblLecturer.Text + "_" + "DetailSummary_" + DateTime.Now.ToString("ddMMyy");
                saveDialog.Filter = "Excel files (*.xlsx)|*.xlsx";
                saveDialog.FilterIndex = 2;
                if (saveDialog.ShowDialog() == DialogResult.OK)
                {
                    Microsoft.Office.Interop.Excel._Application app = new Microsoft.Office.Interop.Excel.Application();
                    Microsoft.Office.Interop.Excel._Workbook workbook = app.Workbooks.Add(Type.Missing);
                    Microsoft.Office.Interop.Excel._Worksheet worksheet = null;
                    app.DisplayAlerts = false;
                    worksheet = workbook.Sheets["Sheet1"];
                    worksheet = workbook.ActiveSheet;
                    worksheet.Name = lblLecturer.Text + "_" + "DetailSummary";
                    for (int i = 1; i < dtgDisplay.Columns.Count + 1; i++)
                    {
                        worksheet.Cells[1, i] = dtgDisplay.Columns[i - 1].HeaderText;
                    }
                    for (int i = 0; i < dtgDisplay.Rows.Count; i++)
                    {
                        for (int j = 0; j < dtgDisplay.Columns.Count; j++)
                        {
                            worksheet.Cells[i + 2, j + 1] = dtgDisplay.Rows[i].Cells[j].Value.ToString();
                        }
                    }
                    try
                    {
                        workbook.SaveAs(saveDialog.FileName, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                        Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("File excel đang được sử dụng, xin hãy tắt trước khi ghi đè.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    app.Quit();
                    MessageBox.Show("Xuất thành file excel thành công.", "Thông báo");
                }
            }
        }
    }
}
