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
        private int childFormNumber = 0;
        public string staffCode = "";
        public GUI_Container(string staffCode)
        {
            InitializeComponent();
            this.staffCode = staffCode;

        }
        public GUI_Container()
        {
            InitializeComponent();

        }

        private void ShowNewForm(object sender, EventArgs e)
        {

            Form childForm = new Form();
            childForm.MdiParent = this;
            childForm.Text = "Window " + childFormNumber++;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            childForm.Show();
        }

        private void managePaidItemsToolStripMenuItem_Click(object sender, EventArgs e)
        {

            Form guiContainer = GUI_Container.ActiveForm;
            foreach (Form f in guiContainer.MdiChildren)
            {
                if (f.Name == "GUI_PaidItem")
                {
                    f.Activate();
                    return;
                }
            }
            GUI_PaidItem smform = new GUI_PaidItem(staffCode);
            smform.MdiParent = this;

            smform.FormBorderStyle = FormBorderStyle.None;
            smform.Dock = DockStyle.Fill;
            smform.Show();

            //GUI_PaidItem guiAcadLevel = new GUI_PaidItem(staffCode);
            //guiAcadLevel.MdiParent = this;
            //guiAcadLevel.Show();
        }

        private void mnsAcademic_Click(object sender, EventArgs e)
        {
            //GUI_AcademicLevel frmAM = new GUI_AcademicLevel();
            //frmAM.MdiParent = this;

            //frmAM.Show();
            Form guiContainer = GUI_Container.ActiveForm;
            foreach (Form f in guiContainer.MdiChildren)
            {
                if (f.Name == "GUI_AcademicLevel")
                {
                    f.Activate();
                    return;
                }
            }
            GUI_AcademicLevel guiAcadLevel = new GUI_AcademicLevel();
            guiAcadLevel.MdiParent = this;
            guiAcadLevel.Show();
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void mniSummary_Click(object sender, EventArgs e)
        {

        }
    }
}
