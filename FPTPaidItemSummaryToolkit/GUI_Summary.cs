using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DTO;
using DAL;

namespace FPTPaidItemSummaryToolkit
{
    public partial class GUI_Summary : Form
    {
        List<MonthlyPaidItemRecord> mpirList = new List<MonthlyPaidItemRecord>();
        MonthlyPaidItemRecord currentMpir = new MonthlyPaidItemRecord();
        User u;
        List<PensionList> plList = (List<PensionList>)DAL_DataSerializer.Instance.BinaryDeserialize("Pension List\\PensionList.fs");
        string savedLocation = "";

        //enhance drawing performance
        //protected override CreateParams CreateParams
        //{
        //    get
        //    {
        //        CreateParams cp = base.CreateParams;
        //        cp.ExStyle |= 0x02000000;
        //        return cp;
        //    }
        //}

        public GUI_Summary(User u)
        {
            InitializeComponent();
            this.u = u;
            lblCreatorName.Text = u.Id;
            lblCreatedDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            lblEmail.Text = u.Email;
        }

        public List<MonthlyPaidItemRecord> GetAllSummaryFile(string folderPath)
        {
            List<MonthlyPaidItemRecord> mpirListFromFiles = new List<MonthlyPaidItemRecord>();
            string[] filePaths = Directory.GetFiles(folderPath);
            int successCount = 0;
            foreach(string path in filePaths)
            {
                MonthlyPaidItemRecord mpir = new MonthlyPaidItemRecord();
                try
                {
                    mpir = (MonthlyPaidItemRecord)DAL_DataSerializer.Instance.BinaryDeserialize(path);
                    successCount++;
                }
                catch (Exception)
                {
                    continue;
                }
                mpirListFromFiles.Add(mpir);
            }
            if (successCount == 0)
                MessageBox.Show("Không có file nào hợp lệ. Xin kiểm tra lại.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else
                MessageBox.Show("Nhập thành công "+successCount+" trên "+filePaths.Length+" file trong thư mục.","Thông báo",MessageBoxButtons.OK,MessageBoxIcon.Information);
            return mpirListFromFiles;
        }

        public List<MonthlyPaidItemRecord> GetSummaryFileByAcadLv(string acadLv)
        {
            List<MonthlyPaidItemRecord> mpirListByAcadLv = new List<MonthlyPaidItemRecord>();
            foreach(MonthlyPaidItemRecord mpir in mpirList)
            {
                if (mpir.AcadLv.Code.Equals(acadLv))
                    mpirListByAcadLv.Add(mpir);
            }
            return mpirListByAcadLv;
        }

        public MonthlyPaidItemRecord GetMpir(string AcadLv, string Campus)
        {
            foreach(MonthlyPaidItemRecord mpir in mpirList)
            {
                if (mpir.AcadLv.Code.Equals(AcadLv) && mpir.Campus.Equals(Campus))
                    return mpir;
            }
            return null;
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            cbxAcadLv.Items.Clear();
            cbxCampus.Items.Clear();
            FolderBrowserDialog fbDialog = new FolderBrowserDialog();
            DialogResult result = fbDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                mpirList = GetAllSummaryFile(fbDialog.SelectedPath);
                if (mpirList.Count > 0)
                {
                    foreach (MonthlyPaidItemRecord mpir in mpirList)
                    {
                        if (!cbxAcadLv.Items.Contains(mpir.AcadLv))
                        {
                            cbxAcadLv.Items.Add(mpir.AcadLv);
                        }
                        cbxAcadLv.DisplayMember = "Code";
                    }
                    cbxAcadLv.SelectedIndex = 0;
                    lblFile.Text = fbDialog.SelectedPath;
                    btnShow.Enabled = true;
                    btnSave.Enabled = true;
                    savedLocation = "";
                }
            }
            btnSummary.Enabled = true;
        }

        private void cbxAcadLv_SelectedIndexChanged(object sender, EventArgs e)
        {
            cbxCampus.Items.Clear();
            AcademicLevel academic = (AcademicLevel)cbxAcadLv.SelectedItem;
            List<MonthlyPaidItemRecord> mpirListByAcadLv = GetSummaryFileByAcadLv(academic.Code);
            foreach (MonthlyPaidItemRecord mpir in mpirListByAcadLv)
            {
                cbxCampus.Items.Add(mpir.Campus);
            }
            cbxCampus.SelectedIndex = 0;
        }

        public void ConstructDatatable(List<MonthlyTeacherPaidItemRecord> mtpirList)
        {
            AcademicLevel academicLevel = (AcademicLevel)cbxAcadLv.SelectedItem;
            string staffListPath = Environment.CurrentDirectory + "\\Staff List\\" + academicLevel.Code + ".xlsx";
            DataTable staffInfo = new DataTable();
            if (File.Exists(staffListPath))
                staffInfo = DAL_Summary.Instance.GetDataFromStaffList(staffListPath);
            else
            {
                MessageBox.Show("Không tìm thấy file danh sách giảng viên.", "Thông báo",MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            DataTable dt = new DataTable();
            dt.Columns.Add("ACC");
            dt.Columns.Add("Mã NV");
            dt.Columns.Add("Tên NV");
            dt.Columns.Add("Email");
            dt.Columns.Add("HĐLĐ");
            dt.Columns.Add("Bộ môn khác");
            List<MonthlyTeacherPaidItemRecord> teacherRecords = mtpirList;
            PensionList PensionList = teacherRecords[0].PensionList;
            if (PensionList is object)
            {
                if(PensionList.pensionList is object)
                    foreach (Pension pen in PensionList.pensionList)
                    {
                        dt.Columns.Add(pen.PensionName);
                    }
            }
            else if (plList is object)
            {
                foreach (PensionList p in plList)
                {
                    if (p.pensionListName.Trim().Equals(currentMpir.AcadLv.Code.Trim()))
                    {
                        foreach (Pension pen in p.pensionList)
                        {
                            dt.Columns.Add(pen.PensionName);
                        }
                        break;
                    }
                }
            }

            List<PaidItem> PaidItemList = teacherRecords[0].PaidItemList;
            DataRow r;
            r = dt.NewRow();
            foreach (PaidItem p in PaidItemList)
            {
                dt.Columns.Add(p.Name);
                switch (p.TypeId)
                {
                    case 1:
                        r[p.Name] = "Số giờ dạy tính hệ số " + p.Rate;
                        break;
                    case 2:
                        r[p.Name] = p.UnitValue + ".000 đ/đơn vị";
                        break;
                    case 3:
                        r[p.Name] = "U hệ số " + p.Rate;
                        break;
                    case 4:
                        r[p.Name] = "-" + p.UnitValue + ".000 đ";
                        break;
                }
            }
            dt.Columns.Add("Tổng").SetOrdinal(dt.Columns.Count-1);
            dt.Rows.Add(r);
            foreach (MonthlyTeacherPaidItemRecord record in teacherRecords)
            {
                r = dt.NewRow();
                Staff s = record.StaffInfo;
                r["ACC"] = s.Account;
                r["Mã NV"] = s.Id;
                r["Tên NV"] = s.Name;
                r["Email"] = s.Email;
                r["HĐLĐ"] = s.Type;
                r["Bộ môn khác"] = s.Major;
                r["Tổng"] = record.Sum;
                foreach (PaidItem p in record.PaidItemList)
                {
                    r[p.Name] = p.Value;
                }

                Staff staff = record.StaffInfo;
                if (record.PensionList is object)
                {
                    foreach (Pension p in record.PensionList.pensionList)
                    {
                        r[p.PensionName] = p.PensionValue;
                    }
                }
                else
                {
                    record.PensionList = new PensionList();
                    record.PensionList.pensionList = new List<Pension>();
                    if (plList is object)
                        foreach (PensionList pl in plList)
                        {
                            if (pl.pensionListName.Trim().Equals(currentMpir.AcadLv.Code.Trim()))
                            {
                                foreach (Pension p in pl.pensionList)
                                {
                                    Pension newP = new Pension(p.PensionName, "0");
                                    foreach (DataRow dtrow in staffInfo.Rows)
                                    {
                                        if (dtrow["Account"].ToString().Trim().ToLower().Equals(staff.Account.Trim().ToLower()))
                                        {
                                            try
                                            {
                                                string ss = dtrow[newP.PensionName].ToString();
                                                newP.PensionValue = ss;
                                                r[newP.PensionName] = newP.PensionValue;
                                            }
                                            catch (Exception)
                                            {
                                                continue;
                                            }
                                        }
                                    }
                                    record.PensionList.pensionList.Add(newP);
                                }
                            }
                        }
                }
                dt.Rows.Add(r);
                try
                {
                    record.Sum = float.Parse(r["Tổng"].ToString());
                }
                catch (FormatException)
                {
                    record.Sum = 0;
                }
            }
            dtgDisplay.DataSource = null;
            dtgDisplay.DataSource = dt;
            dtgDisplay.Columns["ACC"].ReadOnly = true;
            dtgDisplay.Columns["Mã NV"].ReadOnly = true;
            dtgDisplay.Columns["Tên NV"].ReadOnly = true;
            dtgDisplay.Columns["Email"].ReadOnly = true;
            dtgDisplay.Columns["HĐLĐ"].ReadOnly = true;
            dtgDisplay.Columns["Bộ môn khác"].ReadOnly = true;
            foreach (DataGridViewColumn column in dtgDisplay.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
            this.FillRecordNo();
        }
        
        private void FillRecordNo()
        {
            dtgDisplay.RowHeadersWidth = 60;
            for (int i = 1; i < this.dtgDisplay.Rows.Count; i++)
            {
                this.dtgDisplay.Rows[i].HeaderCell.Value = (i).ToString();
            }
        }

        private void btnShow_Click(object sender, EventArgs e)
        {
            AcademicLevel academic = (AcademicLevel)cbxAcadLv.SelectedItem;
            currentMpir = GetMpir(academic.Code, cbxCampus.SelectedItem.ToString());
            ConstructDatatable(currentMpir.mtpirList);
            btnExport.Enabled = true;
            btnSearch.Enabled = true;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            List<MonthlyTeacherPaidItemRecord> smtpirList = DAL_Summary.Instance.Search(txtSearch.Text, currentMpir.mtpirList);
            if(smtpirList.Count>0)
                ConstructDatatable(smtpirList);
            else
            {
                MessageBox.Show("Không tìm tháy kết quả nào.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        void tempSave()
        {
            List<MonthlyTeacherPaidItemRecord> mtpirList = currentMpir.mtpirList;
            foreach (DataGridViewRow row in dtgDisplay.Rows)
            {
                if (row.Cells["ACC"].Value is object)
                {
                    if (row.Cells["ACC"].Value.ToString().Equals(""))
                        continue;
                }
                if (row.Cells["ACC"].Value is object && !row.Cells["ACC"].Value.ToString().Equals(""))
                {
                    MonthlyTeacherPaidItemRecord mtpir = DAL_Summary.Instance.GetMtpirByStaffAccount(row.Cells["ACC"].Value.ToString(), mtpirList);
                    List<PaidItem> pitl = mtpir.PaidItemList;
                    foreach (PaidItem p in pitl)
                    {
                        Object obj = row.Cells[p.Name].Value;
                        if (obj is object)
                        {
                            if (!String.IsNullOrWhiteSpace(obj.ToString()))
                                p.Value = float.Parse(obj.ToString());
                        }
                    }
                    if(mtpir.PensionList is object)
                    {
                        List<Pension> penList = mtpir.PensionList.pensionList;
                        foreach (Pension p in penList)
                        {
                            Object obj = row.Cells[p.PensionName].Value;
                            if (obj is object)
                            {
                                if (!String.IsNullOrWhiteSpace(obj.ToString()))
                                    p.PensionValue = obj.ToString();
                            }
                        }
                    }
                    mtpir.PaidItemList = pitl;
                    int index = DAL_Summary.Instance.GetMtpirIndex(mtpir, mtpirList);
                    mtpirList.RemoveAt(index);
                    mtpirList.Insert(index, mtpir);
                }
            }
        }

        private void dtgDisplay_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            tempSave();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            List<MonthlyTeacherPaidItemRecord> smtpirList = DAL_Summary.Instance.Search(txtSearch.Text, currentMpir.mtpirList);
            ConstructDatatable(smtpirList);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(savedLocation))
            {
                SaveFileDialog saveDialog = new SaveFileDialog();
                saveDialog.Title = "Lưu file";
                saveDialog.FileName = "Summary_" + DateTime.Now.ToString("ddMMyy") + "_" + u.Id;
                saveDialog.Filter = "Encrypted files (*.fs)|*.fs";
                if (saveDialog.ShowDialog() == DialogResult.OK)
                {
                    DAL_DataSerializer.Instance.BinarySerialize(mpirList, saveDialog.FileName);
                    MessageBox.Show("Lưu file tổng hợp thành công");
                    savedLocation = saveDialog.FileName;
                }
            }
            else
            {
                DAL_DataSerializer.Instance.BinarySerialize(mpirList, savedLocation);
                MessageBox.Show("Lưu file tổng hợp thành công");
            }

            string history = u.Id + "|" +Path.GetFileName(savedLocation)+ "|" + DateTime.Now;
            List<string> historyList;
            historyList = (List<string>)DAL_DataSerializer.Instance.BinaryDeserialize("History.fs");
            if(historyList is null)
                historyList = new List<string>();
            historyList.Add(history);
            DAL_DataSerializer.Instance.BinarySerialize(historyList, "History.fs");         
        }

        private void btnLoadFile_Click(object sender, EventArgs e)
        {
            cbxAcadLv.Items.Clear();
            cbxCampus.Items.Clear();
            OpenFileDialog openDialog = new OpenFileDialog();
            openDialog.Title = "Chọn file tổng hợp";
            openDialog.Filter = "Encrypted files (*.fs)|*.fs";
            DialogResult result = openDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                try
                {
                    mpirList = (List<MonthlyPaidItemRecord>)DAL_DataSerializer.Instance.BinaryDeserialize(openDialog.FileName);
                }
                catch (Exception)
                {
                    MessageBox.Show("Không đúng file tổng hợp. Xin hãy chọn lại.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (mpirList.Count > 0)
                {
                    foreach (MonthlyPaidItemRecord mpir in mpirList)
                    {
                        if (!cbxAcadLv.Items.Contains(mpir.AcadLv))
                        {
                            cbxAcadLv.Items.Add(mpir.AcadLv);
                        }
                        cbxAcadLv.DisplayMember = "Code";
                    }
                    cbxAcadLv.SelectedIndex = 0;
                    lblFile.Text = openDialog.FileName;
                    btnShow.Enabled = true;
                    btnSearch.Enabled = true;
                    btnSave.Enabled = true;
                }
                savedLocation = openDialog.FileName;
                MessageBox.Show("Nhập file tổng hợp thành công.","Thông báo",MessageBoxButtons.OK,MessageBoxIcon.Information);
            }
            btnSummary.Enabled = true;
        }

        private void btnSummary_Click(object sender, EventArgs e)
        {
            GUI_FinalSummary fSumForm = new GUI_FinalSummary(mpirList);
            fSumForm.ShowDialog();
        }

        string columnName;
        private void dtgDisplay_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                int columnIndex = dtgDisplay.CurrentCell.ColumnIndex;
                columnName = dtgDisplay.Columns[columnIndex].Name;
                ContextMenuStrip cm = new ContextMenuStrip();
                cm.Items.Add("Nhập dữ liệu");
                cm.Items.Add("Gán công thức");
                cm.Show(this, this.PointToClient(MousePosition));
                cm.ItemClicked += new ToolStripItemClickedEventHandler(ToolTipsFunction);
            }
        }

        private void ToolTipsFunction(object sender, ToolStripItemClickedEventArgs e)
        {
            string selectedItem = e.ClickedItem.Text;
            switch (selectedItem)
            {
                case "Nhập dữ liệu":
                    InsertMultipleFromExcel();
                    break;
                case "Gán công thức":
                    CalculateByFormula(currentMpir);
                    break;
            }
        }

        private void InsertMultipleFromExcel()
        {
            GUI_AddMultipleFromExcel ExcelImportForm = new GUI_AddMultipleFromExcel(currentMpir, columnName);
            DialogResult result = ExcelImportForm.ShowDialog();
            if (result == DialogResult.Cancel)
            {
                this.currentMpir = ExcelImportForm.mpir;
                ConstructDatatable(currentMpir.mtpirList);
            }
            tempSave();
        }

        private void CalculateByFormula(MonthlyPaidItemRecord mpir)
        {
            GUI_CreateFormula CalculateForm = new GUI_CreateFormula(mpir, columnName);
            DialogResult result = CalculateForm.ShowDialog();
            if(result == DialogResult.OK)
            {
                this.currentMpir = CalculateForm.mpir;
                ConstructDatatable(currentMpir.mtpirList);
            }
            tempSave();
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            AcademicLevel AcademicLv = (AcademicLevel)cbxAcadLv.SelectedItem;
            string campus = cbxCampus.SelectedItem.ToString();
            DialogResult result = MessageBox.Show("Xuất trang tính hiện tại ra file Excel?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if(result == DialogResult.Yes)
            {
                SaveFileDialog saveDialog = new SaveFileDialog();
                saveDialog.Title = "Xuất ra file Excel";
                saveDialog.FileName = AcademicLv.Code + "_" + campus + "_Summary_" + DateTime.Now.ToString("ddMMyy");
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
