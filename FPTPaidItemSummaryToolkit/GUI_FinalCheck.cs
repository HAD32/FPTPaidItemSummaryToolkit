using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DTO;

namespace FPTPaidItemSummaryToolkit
{
    public partial class GUI_FinalCheck : Form
    {
        MonthlyPaidItemRecord mpir;
        public GUI_FinalCheck(MonthlyPaidItemRecord mpir)
        {
            InitializeComponent();
            this.mpir = mpir;
            generateData(mpir);
        }

        private void generateData(MonthlyPaidItemRecord mpir)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Hạng mục");
            dt.Columns.Add("Lớn nhất (MAX)");
            dt.Columns.Add("Nhỏ nhất (MIN)");
            List<CheckResult> summary = SummaryResult(mpir);
            foreach(CheckResult check in summary)
            {
                DataRow row = dt.NewRow();
                row["Hạng mục"] = check.Name;
                row["Lớn nhất (MAX)"] = check.Maximum == -1 ? 0 : check.Maximum;
                row["Nhỏ nhất (MIN)"] = check.Minimum == -1 ? 0 : check.Minimum;
                dt.Rows.Add(row);
            }

            dtgDisplay.DataSource = dt;
            this.dtgDisplay.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.dtgDisplay.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.dtgDisplay.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }

        private List<CheckResult> SummaryResult(MonthlyPaidItemRecord mpir)
        {
            List<CheckResult> CheckList = generateCheckList();
            foreach(MonthlyTeacherPaidItemRecord mtpir in mpir.mtpirList)
            {
                foreach(CheckResult result in CheckList)
                {
                    foreach(Pension p in mtpir.PensionList.pensionList)
                    {
                        if (result.Name.Trim().Equals(p.PensionName.Trim()))
                        {
                            float pensionValue = float.Parse(p.PensionValue);
                            if (pensionValue > result.Maximum)
                                result.Maximum = pensionValue;
                            if (pensionValue < result.Minimum)
                                result.Minimum = pensionValue;
                        }
                    }
                    foreach(PaidItem p in mtpir.PaidItemList)
                    {
                        if (result.Name.Trim().Equals(p.Name.Trim()))
                        {
                            if (p.Value > result.Maximum)
                                result.Maximum = p.Value;
                            if (p.Value < result.Minimum)
                                result.Minimum = p.Value;
                        }
                    }
                }
            }
            return CheckList;
        }

        private List<CheckResult> generateCheckList()
        {
            List<CheckResult> checkList = new List<CheckResult>();
            foreach (Pension p in mpir.mtpirList[0].PensionList.pensionList)
            {
                CheckResult check = new CheckResult(p.PensionName, -1, -1);
                try
                {
                    float testString = float.Parse(p.PensionValue);
                    checkList.Add(check);
                }
                catch (Exception) { }
            }
            foreach (PaidItem p in mpir.mtpirList[0].PaidItemList)
            {
                CheckResult check = new CheckResult(p.Name, -1, -1);
                checkList.Add(check);
            }
            return checkList;
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            AcademicLevel AcademicLv = mpir.AcadLv;
            string campus = mpir.Campus;
            DialogResult result = MessageBox.Show("Xuất trang tính hiện tại ra file Excel?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if(result == DialogResult.Yes)
            {
                SaveFileDialog saveDialog = new SaveFileDialog();
                saveDialog.Title = "Xuất ra file Excel";
                saveDialog.FileName = AcademicLv.Code + "_" + campus + "_FinalCheck_" + DateTime.Now.ToString("ddMMyy");
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
                    worksheet.Name = AcademicLv.Code + "_" + campus;
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
