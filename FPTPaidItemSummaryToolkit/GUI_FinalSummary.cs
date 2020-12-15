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
using DAL;

namespace FPTPaidItemSummaryToolkit
{
    public partial class GUI_FinalSummary : Form
    {
        List<MonthlyPaidItemRecord> mpirList;
        List<MonthlyTeacherPaidItemRecord> mtpirFinalList;
        List<string> accountList;

        //protected override CreateParams CreateParams
        //{
        //    get
        //    {
        //        CreateParams cp = base.CreateParams;
        //        cp.ExStyle |= 0x02000000;
        //        return cp;
        //    }
        //}

        public GUI_FinalSummary(List<MonthlyPaidItemRecord> mpirList)
        {
            InitializeComponent();
            this.mpirList = mpirList;
            MakeFinalList(mpirList);
            ConstructDatatable();
        }

        public void ConstructDatatable()
        {
            dtgDisplay.DataSource = null;
            DataTable dt = new DataTable();
            dt.Columns.Add("ACC");
            dt.Columns.Add("Mã NV");
            dt.Columns.Add("Tên NV");
            dt.Columns.Add("Email");
            dt.Columns.Add("HĐLĐ");
            dt.Columns.Add("Bộ môn khác");
            dt.Columns.Add("Tổng lương");
            DataRow r;
            foreach(MonthlyTeacherPaidItemRecord record in mtpirFinalList)
            {
                r = dt.NewRow();
                Staff s = record.StaffInfo;
                r["ACC"] = s.Account;
                r["Mã NV"] = s.Id;
                r["Tên NV"] = s.Name;
                r["Email"] = s.Email;
                r["Tổng lương"] = record.Sum;
                dt.Rows.Add(r);
            }
            dtgDisplay.DataSource = dt;
            foreach (DataGridViewColumn column in dtgDisplay.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
            this.FillRecordNo();
            for(int i = 0; i < dtgDisplay.Columns.Count; i++){
                this.dtgDisplay.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            }
            this.dtgDisplay.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }

        private void FillRecordNo()
        {
            dtgDisplay.RowHeadersWidth = 60;
            for (int i = 0; i < this.dtgDisplay.Rows.Count; i++)
            {
                this.dtgDisplay.Rows[i].HeaderCell.Value = (i+1).ToString();
            }
        }

        void MakeFinalList(List<MonthlyPaidItemRecord> mpirList)
        {
            mtpirFinalList =  new List<MonthlyTeacherPaidItemRecord>();
            accountList = new List<string>();
            foreach (MonthlyPaidItemRecord mpir in mpirList)
            {
                foreach(MonthlyTeacherPaidItemRecord mtpir in mpir.mtpirList)
                {
                    Staff s = mtpir.StaffInfo;
                    if (!accountList.Contains(s.Account))
                    {
                        accountList.Add(s.Account);
                        mtpirFinalList.Add(new MonthlyTeacherPaidItemRecord("", s, null,null, "", mtpir.Sum));
                    }
                    else
                    {
                        MonthlyTeacherPaidItemRecord m = DAL_Summary.Instance.GetMtpirByStaffAccount(s.Account,mtpirFinalList);
                        m.Sum += mtpir.Sum;
                    }
                }
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Xuất trang tính ra file Excel?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                SaveFileDialog saveDialog = new SaveFileDialog();
                saveDialog.Title = "Xuất ra file Excel";
                saveDialog.FileName ="Payment_Summary_" + DateTime.Now.ToString("ddMMyy");
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
                    worksheet.Name = "Summary";
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

        string selectedStaffAccount;

        private void btnGetDetail_Click(object sender, EventArgs e)
        {
            GUI_DetailMonthlyRecord detailForm = new GUI_DetailMonthlyRecord(selectedStaffAccount, getStaffListOfTeachingMpir());
            detailForm.Show();
        }

        private void dtgDisplay_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            int index = e.RowIndex;
            selectedStaffAccount = dtgDisplay.Rows[index].Cells["ACC"].Value.ToString().Trim();
        }

        private List<MonthlyPaidItemRecord> getStaffListOfTeachingMpir()
        {
            List<MonthlyPaidItemRecord> staffDetailMonthlyRecords = new List<MonthlyPaidItemRecord>();
            foreach (MonthlyPaidItemRecord mpir in mpirList)
            {
                MonthlyPaidItemRecord newMpir = new MonthlyPaidItemRecord();
                foreach (MonthlyTeacherPaidItemRecord mtpir in mpir.mtpirList)
                {
                    Staff s = mtpir.StaffInfo;
                    if (s.Account.Trim().Equals(selectedStaffAccount.Trim()))
                    {
                        newMpir.AcadLv = mpir.AcadLv;
                        newMpir.Campus = mpir.Campus;
                        newMpir.mtpirList = new List<MonthlyTeacherPaidItemRecord>();
                        newMpir.mtpirList.Add(mtpir);
                        staffDetailMonthlyRecords.Add(newMpir);
                        break;
                    }
                }
            }
            return staffDetailMonthlyRecords;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string searchText = DAL_Summary.Instance.stringStandardlize(txtSearch.Text);
            MakeFinalList(Search(mpirList, searchText));
            ConstructDatatable();
        }

        private List<MonthlyPaidItemRecord> Search(List<MonthlyPaidItemRecord> mpirList, string searchText)
        {
            List<MonthlyPaidItemRecord> filteredList = new List<MonthlyPaidItemRecord>();
            foreach (MonthlyPaidItemRecord mpir in mpirList)
            {
                MonthlyPaidItemRecord newMpir = new MonthlyPaidItemRecord();
                newMpir.mtpirList = new List<MonthlyTeacherPaidItemRecord>();
                foreach(MonthlyTeacherPaidItemRecord mtpir in mpir.mtpirList)
                {
                    Staff staff = mtpir.StaffInfo;
                    bool found = false;
                    if (rdbAccount.Checked)
                    {
                        string normalizedAccount = DAL_Summary.Instance.stringStandardlize(staff.Account);
                        found = normalizedAccount.Contains(searchText);
                    }
                    else
                    {
                        string normalizedName = DAL_Summary.Instance.stringStandardlize(staff.Name);
                        found = normalizedName.Contains(searchText);
                    }
                    if (found)
                    {
                        newMpir.mtpirList.Add(mtpir);
                    }
                }
                filteredList.Add(newMpir);
            }
            MessageBox.Show(filteredList.Count() + " - " + mpirList.Count());
            return filteredList;
        }
    }
}
