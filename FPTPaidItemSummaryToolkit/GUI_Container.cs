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
    public partial class GUI_Container : Form
    {
        User u;
        private int childFormNumber = 0;
        public GUI_Container(User u)
        {
            InitializeComponent();
            this.u = u;
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
            GUI_PaidItem smform = new GUI_PaidItem(u);
            smform.MdiParent = this;

            smform.FormBorderStyle = FormBorderStyle.None;
            smform.Dock = DockStyle.Fill;
            smform.Show();
        }

        private void mnsAcademic_Click(object sender, EventArgs e)
        {
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
            GUI_Summary guiSummary = new GUI_Summary(u);
            guiSummary.MdiParent = this;
            guiSummary.FormBorderStyle = FormBorderStyle.None;
            guiSummary.Dock = DockStyle.Fill;
            guiSummary.Show();
        }
    }
}
