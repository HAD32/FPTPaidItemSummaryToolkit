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

        private void ManagePaidItemsToolStripMenuItem_Click(object sender, EventArgs e)
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

        private void MnsAcademic_Click(object sender, EventArgs e)
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

        private void CloseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void MniSummary_Click(object sender, EventArgs e)
        {
            Form guiContainer = GUI_Container.ActiveForm;
            foreach (Form f in guiContainer.MdiChildren)
            {
                if (f.Name == "GUI_Summary")
                {
                    f.Activate();
                    return;
                }
            }
            GUI_Summary guiSummary = new GUI_Summary(u);
            guiSummary.MdiParent = this;
            guiSummary.FormBorderStyle = FormBorderStyle.None;
            guiSummary.Dock = DockStyle.Fill;
            guiSummary.Show();
        }

        private void UserInfoManagement_Click(object sender, EventArgs e)
        {
            Form guiContainer = GUI_Container.ActiveForm;
            foreach (Form f in guiContainer.MdiChildren)
            {
                if (f.Name == "GUI_UserInfoManagement")
                {
                    f.Activate();
                    return;
                }
            }
            GUI_UserInfoManagement guiUserinfo = new GUI_UserInfoManagement();
            guiUserinfo.MdiParent = this;
            guiUserinfo.Show();
        }

        private void mniPension_Click(object sender, EventArgs e)
        {
            Form guiContainer = GUI_Container.ActiveForm;
            foreach (Form f in guiContainer.MdiChildren)
            {
                if (f.Name == "GUI_PensionManagement")
                {
                    f.Activate();
                    return;
                }
            }
            GUI_PensionManagement guiPension = new GUI_PensionManagement();
            guiPension.MdiParent = this;
            guiPension.Show();
        }
    }
}
