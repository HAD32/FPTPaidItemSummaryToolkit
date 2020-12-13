﻿using System;
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
using System.IO;

namespace FPTPaidItemSummaryToolkit
{
    public partial class GUI_PensionManagement : Form
    {
        List<PensionList> plList = new List<PensionList>();
        List<AcademicLevel> acadList = (List<AcademicLevel>)DAL_DataSerializer.Instance.BinaryDeserialize("Academic Levels\\AcademicLevel.fs");

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

        public GUI_PensionManagement()
        {
            InitializeComponent();
            load();
        }

        public void load()
        {
            plList = (List<PensionList>)DAL_DataSerializer.Instance.BinaryDeserialize("Pension List\\PensionList.fs");
            if (plList is null)
            {
                plList = new List<PensionList>();
                foreach(AcademicLevel acad in acadList)
                {
                    PensionList pl = new PensionList();
                    pl.pensionListName = acad.Code;
                    pl.academicLevel = acad;
                    pl.pensionList = new List<Pension>();
                    plList.Add(pl);
                }
                lstPensionList.DataSource = plList;
                lstPensionList.DisplayMember = "pensionListName";
            }
            else
            {
                foreach (AcademicLevel acad in acadList)
                {
                    bool found = false;
                    foreach(PensionList pl in plList)
                    {
                        if (acad.Code.Trim().Equals(pl.pensionListName.Trim()))
                        {
                            found = true;
                        }
                    }
                    if (!found)
                    {
                        PensionList pl = new PensionList();
                        pl.pensionListName = acad.Code;
                        pl.academicLevel = acad;
                        pl.pensionList = new List<Pension>();
                        plList.Add(pl);
                    } 
                }
                lstPensionList.DataSource = plList;
                lstPensionList.DisplayMember = "pensionListName";
            }
        }

        private void reloadItem()
        {
            lstItems.Items.Clear();
            dtgDisplay.Columns.Clear();
            PensionList pl;
            if (lstPensionList.Items.Count > 0)
                pl = (PensionList)lstPensionList.SelectedItem;
            else
                pl = new PensionList();
            if (pl.pensionList is object)
            {
                DataTable dt = new DataTable();
                foreach (Pension p in pl.pensionList)
                {
                    lstItems.Items.Add(p);
                    lstItems.DisplayMember = "PensionName";
                    dt.Columns.Add(p.PensionName);
                }
                dtgDisplay.DataSource = dt;
            }
        }
    

        private void btnAddItems_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtItemName.Text))
            {
                errorProvider1.Clear();
                errorProvider1.SetIconAlignment(this.txtItemName, ErrorIconAlignment.MiddleRight);
                errorProvider1.SetIconPadding(this.txtItemName, -20);
                errorProvider1.SetError(txtItemName, "Không để trống trường này");
                txtItemName.Focus();
                return;
            }
            string[] pensionRange = txtItemName.Text.Split(new[] {'\n'},StringSplitOptions.RemoveEmptyEntries);

            int count = 0;
            foreach (string s in pensionRange)
            {
                bool check = false;
                Pension p = new Pension();
                p.PensionName = s.Trim();
                foreach (PensionList pl in plList)
                {
                    PensionList selectedItem = (PensionList)lstPensionList.SelectedItem;
                    if (pl.pensionListName.Trim().Equals(selectedItem.pensionListName.Trim()))
                    {
                        List<Pension> pensionList = pl.pensionList;
                        if (pensionList is null)
                            pensionList = new List<Pension>();
                        else
                            foreach (Pension pen in pl.pensionList)
                            {
                                if (pen.PensionName.Trim().Equals(p.PensionName.Trim()))
                                {
                                    check = true;
                                    break;
                                }
                            }
                        if (!check)
                        {
                            pensionList.Add(p);
                            lstItems.Items.Add(p);
                            count++;
                        }
                        txtItemName.Text = "";
                        pl.pensionList = pensionList;
                    }
                }
            }
            MessageBox.Show("Đã thêm " + count + "/" + pensionRange.Length + " mục","Thông báo",MessageBoxButtons.OK,MessageBoxIcon.Information);
            try
            {
                DAL_DataSerializer.Instance.BinarySerialize(plList, "Pension List\\PensionList.fs");
            }
            catch (Exception)
            {
                Directory.CreateDirectory(Path.GetDirectoryName(Application.ExecutablePath) + "\\Pension List");
                DAL_DataSerializer.Instance.BinarySerialize(plList, "Pension List\\PensionList.fs");
            }
            reloadItem();
        }
        private void lstPensionList_SelectedIndexChanged(object sender, EventArgs e)
        {
            reloadItem();
        }
        
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            DAL_DataSerializer.Instance.BinarySerialize(plList, "Pension List\\PensionList.fs");
            MessageBox.Show("Lưu thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        int indexItem, indexItem2;
        private void lstItems_Click(object sender, EventArgs e)
        {
            this.lstItems.MouseUp -= new System.Windows.Forms.MouseEventHandler(this.List_RightClick2);
            this.lstItems.MouseUp += new System.Windows.Forms.MouseEventHandler(this.List_RightClick2);
        }
        
        private void List_RightClick2(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                indexItem2 = this.lstItems.IndexFromPoint(e.Location);
                ContextMenuStrip cm = new ContextMenuStrip();
                cm.Items.Add("Đổi tên");
                cm.Items.Add("Xóa");
                cm.Show(this, this.PointToClient(MousePosition));
                cm.ItemClicked += new ToolStripItemClickedEventHandler(ModifyList2);
            }
        }

        private void ModifyList2(object sender, ToolStripItemClickedEventArgs e)
        {
            string selectedItem = e.ClickedItem.Text;
            PensionList selectedPl = (PensionList)lstPensionList.SelectedItem;
            Pension selectedP = (Pension)lstItems.SelectedItem;
            switch (selectedItem)
            {
                case "Đổi tên":
                    GUI_SimpleModifyForm modForm = new GUI_SimpleModifyForm(selectedP.PensionName);
                    DialogResult modResult = modForm.ShowDialog();
                    if (modResult == DialogResult.OK)
                    {
                        foreach (PensionList pl in plList)
                        {
                            if (selectedPl.pensionListName.Trim().Equals(pl.pensionListName.Trim()))
                            {
                                foreach(Pension p in pl.pensionList)
                                {
                                    if (p.PensionName.Equals(selectedP.PensionName))
                                    {
                                        p.PensionName = modForm.newName.Trim();
                                        MessageBox.Show("Đổi tên thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        break;
                                    }
                                }
                            }
                        }
                    }
                    break;
                case "Xóa":
                    DialogResult result = MessageBox.Show("Bạn có muốn xóa mục này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result == DialogResult.Yes)
                    {
                        foreach (PensionList pl in plList)
                        {
                            if (selectedPl.pensionListName.Trim().Equals(pl.pensionListName.Trim()))
                            {
                                foreach (Pension p in pl.pensionList)
                                {
                                    if (p.PensionName.Trim().Equals(selectedP.PensionName.Trim()))
                                    {
                                        pl.pensionList.Remove(p);
                                        MessageBox.Show("Xóa thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        break;
                                    }
                                }
                            }
                        }
                    }
                    break;
            }
            DAL_DataSerializer.Instance.BinarySerialize(plList, "Pension List\\PensionList.fs");
            reloadItem();
        }

    }
}