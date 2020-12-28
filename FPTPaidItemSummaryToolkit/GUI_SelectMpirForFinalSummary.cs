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
    public partial class GUI_SelectMpirForFinalSummary : Form
    {
        List<MonthlyPaidItemRecord> mpirList;
        List<MonthlyPaidItemRecord> selectedMpirList = new List<MonthlyPaidItemRecord>();
        public GUI_SelectMpirForFinalSummary(List<MonthlyPaidItemRecord> mpirList)
        {
            InitializeComponent();
            this.mpirList = mpirList;
            foreach(MonthlyPaidItemRecord mpir in mpirList)
            {
                chkMpir.Items.Add(mpir.AcadLv.Code+"_"+mpir.Campus);
            }
            chkSelectAll.Checked = true;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            foreach(int index in chkMpir.CheckedIndices)
            {
                selectedMpirList.Add(mpirList[index]);
            }
            GUI_FinalSummary fSumForm = new GUI_FinalSummary(selectedMpirList);
            fSumForm.ShowDialog();
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void chkSelectAll_CheckedChanged(object sender, EventArgs e)
        {
            if (chkSelectAll.Checked == true)
            {
                for (int i = 0; i < chkMpir.Items.Count; i++)
                {
                    chkMpir.SetItemChecked(i, true);
                }
            }
            else
            {
                for (int i = 0; i < chkMpir.Items.Count; i++)
                {
                    chkMpir.SetItemChecked(i, false);
                }
            }
        }
    }
}
