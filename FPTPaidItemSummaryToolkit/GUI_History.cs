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

namespace FPTPaidItemSummaryToolkit
{
    public partial class GUI_History : Form
    {
        public GUI_History()
        {
            InitializeComponent();
            DataTable dt = new DataTable();
            dt.Columns.Add("Mã nhân viên");
            dt.Columns.Add("Tên file");
            dt.Columns.Add("Ngày chỉnh sửa");

            List<string> HistoryList = (List<string>)DAL_DataSerializer.Instance.BinaryDeserialize("History.fs");
            if(HistoryList is object)
                foreach(string s in HistoryList)
                {
                    DataRow row = dt.NewRow();
                    string[] items = s.Split('|');
                    row["Mã nhân viên"] = items[0];
                    row["Tên file"] = items[1];
                    row["Ngày chỉnh sửa"] = items[2];
                    dt.Rows.Add(row);
                }
            dtgHistory.DataSource = dt;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
