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
        List<MonthlyTeacherPaidItemRecord> mtpirFinalList = new List<MonthlyTeacherPaidItemRecord>();
        List<string> accountList = new List<string>();

        public GUI_FinalSummary(List<MonthlyPaidItemRecord> mpirList)
        {
            InitializeComponent();
            this.mpirList = mpirList;
            MakeFinalList();
            ConstructDatatable();
        }

        void ConstructDatatable()
        {
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
                r["HĐLĐ"] = s.Type;
                r["Bộ môn khác"] = s.Major;
                r["Tổng lương"] = record.Sum;
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

        void MakeFinalList()
        {
            foreach(MonthlyPaidItemRecord mpir in mpirList)
            {
                foreach(MonthlyTeacherPaidItemRecord mtpir in mpir.mtpirList)
                {
                    Staff s = mtpir.StaffInfo;
                    if (!accountList.Contains(s.Account))
                    {
                        accountList.Add(s.Account);
                        mtpirFinalList.Add(new MonthlyTeacherPaidItemRecord("", s, null, "", mtpir.Sum));
                    }
                    else
                    {
                        MonthlyTeacherPaidItemRecord m = DAL_Summary.Instance.GetMtpirByStaffAccount(s.Account,mtpirFinalList);
                        m.Sum += mtpir.Sum;
                    }
                }
            }
        }


    }
}
