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
using System.Text.RegularExpressions;

namespace FPTPaidItemSummaryToolkit
{
    public partial class GUI_AddMultipleFromExcel : Form
    {
        public MonthlyPaidItemRecord mpir { get; set; }
        string pensionName;
        int count = 0;
        List<PensionList> pl = (List<PensionList>)DAL_DataSerializer.Instance.BinaryDeserialize("Pension List\\PensionList.fs");

        public GUI_AddMultipleFromExcel(MonthlyPaidItemRecord mpirPassed, string columnName)
        {
            InitializeComponent();
            this.mpir = mpirPassed;
            this.pensionName = columnName;
            lblPaidItemName.Text = pensionName;
        }

        void GenerateData()
        {
            count = 0;
            txtResult.Clear();
            string originalData = txtData.Text.Trim();
            string[] RecordDataList = originalData.Split('\n');
            bool skipFirstLine = chkSkipFirstLine.Checked;
            foreach (string s in RecordDataList)
            {
                if (skipFirstLine)
                {
                    if (s.Equals(RecordDataList[0]))
                        continue;
                }
                string ms = s.Trim();
                ms = Regex.Replace(ms, @"\s+", " ");
                string[] separateString = ms.Split(' ');
                MonthlyTeacherPaidItemRecord mtpir = GetRecordByAccount(mpir, separateString[0].Trim());
                if (mtpir is object)
                {
                    List<Pension> pList = mtpir.PensionList.pensionList;
                    foreach(Pension p in pList)
                    {
                        if (p.PensionName.Equals(pensionName))
                        {
                            try
                            {
                                p.PensionValue = float.Parse(separateString[1]);
                                count++;
                            }
                            catch (Exception)
                            {
                                txtResult.AppendText("Dữ liệu của giảng viên " + separateString[0] + " không đúng (Phải điền số. Hiện tại: " + separateString[1] + ")\r\n");
                            }
                        }
                    }
                }
                else
                {
                    txtResult.AppendText("Không tìm thấy giảng viên " + separateString[0] + "\r\n");
                }
            }
            txtResult.AppendText("Đã có " + count + " (trên tổng " + (skipFirstLine ? RecordDataList.Length - 1 : RecordDataList.Length) + ") dữ liệu giảng viên cập nhật thành công.\r\n");
        }

        public MonthlyTeacherPaidItemRecord GetRecordByAccount(MonthlyPaidItemRecord m, string account)
        {
            List<MonthlyTeacherPaidItemRecord> mtpirList = m.mtpirList;
            foreach (MonthlyTeacherPaidItemRecord mtpir in mtpirList)
            {
                Staff s = mtpir.StaffInfo;
                if (s.Account.Equals(account))
                    return mtpir;
            }
            return null;
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            GenerateData();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
