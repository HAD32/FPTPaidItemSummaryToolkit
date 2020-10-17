using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FPTPaidItemSummaryToolkit
{
    public partial class GUI_Container : Form
    {
        public GUI_Container()
        {
            InitializeComponent();
        }

        private void managePaidItemsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GUI_PaidItem frmPaidItem = new GUI_PaidItem();
            frmPaidItem.MdiParent = this;
            frmPaidItem.Show();
            frmPaidItem.Top = 0;
            frmPaidItem.Left = 0;
        }

        private void mnsAcademic_Click(object sender, EventArgs e)
        {
            GUI_AcademicLevel frmAM = new GUI_AcademicLevel();
            frmAM.MdiParent = this;

            frmAM.Show();
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
