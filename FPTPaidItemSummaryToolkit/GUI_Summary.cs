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
                if (mpir.AcadLv.Equals(acadLv))
                    mpirListByAcadLv.Add(mpir);
            }
            return mpirListByAcadLv;
        }

        public MonthlyPaidItemRecord GetMpir(string AcadLv, string Campus)
        {
            foreach(MonthlyPaidItemRecord mpir in mpirList)
            {
                if (mpir.AcadLv.Equals(AcadLv) && mpir.Campus.Equals(Campus))
                    return mpir;
            }
            return null;
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
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
                            cbxAcadLv.Items.Add(mpir.AcadLv);
                    }
                    cbxAcadLv.SelectedIndex = 0;
                    lblFile.Text = fbDialog.SelectedPath;
                    btnShow.Enabled = true;
                    btnSearch.Enabled = true;
                    btnSave.Enabled = true;
                }
            }
        }

        private void cbxAcadLv_SelectedIndexChanged(object sender, EventArgs e)
        {
            cbxCampus.Items.Clear();
            List<MonthlyPaidItemRecord> mpirListByAcadLv = GetSummaryFileByAcadLv(cbxAcadLv.SelectedItem.ToString());
            foreach (MonthlyPaidItemRecord mpir in mpirListByAcadLv)
            {
                cbxCampus.Items.Add(mpir.Campus);
            }
            cbxCampus.SelectedIndex = 0;
        }

        public void ConstructDatatable(List<MonthlyTeacherPaidItemRecord> mtpirList)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("ACC");
            dt.Columns.Add("Mã NV");
            dt.Columns.Add("Tên NV");
            dt.Columns.Add("Email");
            dt.Columns.Add("HĐLĐ");
            dt.Columns.Add("Bộ môn khác");

            dt.Columns.Add("Bậc lương quản lý");
            dt.Columns.Add("Tiền lương quản lý");

            dt.Columns.Add("Bậc F");
            dt.Columns.Add("Tiền F");
            dt.Columns.Add("Định mức giờ giảng theo LCB");
            dt.Columns.Add("Lương cơ bản");
            dt.Columns.Add("Khoản bổ sung quản lý");

            if(currentMpir.Campus.Equals("HOALAC"))
                dt.Columns.Add("Hỗ trợ dạy Hòa Lạc");
            dt.Columns.Add("Tổng lương giảng dạy FPTUHN");
            //Lương cơ bản thực tế
            //Khoản bổ sung
            //Bù/trừ
            //Tiền trưởng môn
            //Hỗ trợ dạy Hòa Lạc
    
            dt.Columns.Add("Tính bổ sung 50% các giờ không vượt đã tính 50% vào cuối kỳ");
            dt.Columns.Add("Tổng lương tháng");

            dt.Columns.Add("Tạm ứng để nhận LCB");
            dt.Columns.Add("Trừ tiền tạm ứng tháng trước");
            dt.Columns.Add("Tiền lương tháng sau khi cộng/trừ tiền tạm ứng");

            dt.Columns.Add("Số giờ dạy trên 132h");
            
            

            List <MonthlyTeacherPaidItemRecord> teacherRecords = mtpirList;
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
            dt.Rows.Add(r);
            dt.Columns.Add("Ghi chú");
            
            foreach (MonthlyTeacherPaidItemRecord record in teacherRecords)
            {
                float teachingHour = 0;
                float sum=0;
                float HLpension = 0;
                r = dt.NewRow();
                Staff s = record.StaffInfo;
                r["ACC"] = s.Account;
                r["Mã NV"] = s.Id;
                r["Tên NV"] = s.Name;
                r["Email"] = s.Email;
                r["HĐLĐ"] = s.Type;
                r["Bộ môn khác"] = s.Major;
                foreach (PaidItem p in record.PaidItemList)
                {
                    r[p.Name] = p.Value;
                    if (p.TypeId == 1 || p.TypeId == 3)
                        sum += p.Value * p.Rate; //Need to multiply with teacher's own rate
                    else if (p.TypeId == 2)
                        sum += p.Value * p.UnitValue;
                    else
                        sum -= p.Value * p.UnitValue;
                    if(p.TypeId == 1)
                    {
                        teachingHour += p.Value;
                    }
                }
                r["Số giờ dạy trên 132h"] = teachingHour > 132 ? (teachingHour - 132) : 0;
                try
                {
                    HLpension = (teachingHour * 10) > 500 ? 500 : (teachingHour * 10);
                    r["Hỗ trợ dạy Hòa Lạc"] = HLpension;
                }
                catch (Exception) {
                    HLpension = 0; 
                }
                record.Sum = sum + HLpension ;
                r["Ghi chú"] = record.Note;
                r["Tổng lương giảng dạy FPTUHN"] = record.Sum;
                dt.Rows.Add(r);
            }
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
            currentMpir = GetMpir(cbxAcadLv.SelectedItem.ToString(), cbxCampus.SelectedItem.ToString());
            ConstructDatatable(currentMpir.mtpirList);
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            List<MonthlyTeacherPaidItemRecord> smtpirList = DAL_Summary.Instance.Search(txtSearch.Text, currentMpir.mtpirList);
            ConstructDatatable(smtpirList);
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
                    List<PaidItem> pitl = DAL_Summary.Instance.GetPaidItemList(cbxAcadLv.SelectedItem.ToString());

                    foreach (PaidItem p in pitl)
                    {
                        Object obj = row.Cells[p.Name].Value;
                        if (obj is object)
                        {
                            if (!String.IsNullOrWhiteSpace(obj.ToString()))
                                p.Value = float.Parse(obj.ToString());
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
            SaveFileDialog saveDialog = new SaveFileDialog();
            saveDialog.Title = "Lưu file";
            saveDialog.FileName = "Summary_" + DateTime.Now.ToString("ddMMyy")+"_"+u.Id;
            saveDialog.Filter = "Encrypted files (*.sf)|*.sf";
            if (saveDialog.ShowDialog() == DialogResult.OK)
            {
                DAL_DataSerializer.Instance.BinarySerialize(mpirList, saveDialog.FileName);   
            }
        }

        private void btnLoadFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog openDialog = new OpenFileDialog();
            DialogResult result = openDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                mpirList = (List<MonthlyPaidItemRecord>)DAL_DataSerializer.Instance.BinaryDeserialize(openDialog.FileName);
                if (mpirList.Count > 0)
                {
                    foreach (MonthlyPaidItemRecord mpir in mpirList)
                    {
                        if (!cbxAcadLv.Items.Contains(mpir.AcadLv))
                            cbxAcadLv.Items.Add(mpir.AcadLv);
                    }
                    cbxAcadLv.SelectedIndex = 0;
                    lblFile.Text = openDialog.FileName;
                    btnShow.Enabled = true;
                    btnSearch.Enabled = true;
                    btnSave.Enabled = true;
                }
                MessageBox.Show("Thành công.");
            }
        }

        private void btnSummary_Click(object sender, EventArgs e)
        {
            GUI_FinalSummary fSumForm = new GUI_FinalSummary(mpirList);
            fSumForm.ShowDialog();
        }
    }
}
