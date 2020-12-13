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
using System.Reflection;

namespace FPTPaidItemSummaryToolkit
{
    public partial class GUI_PaidItemManagement : Form
    {
        List<AcademicLevel> academicList = new List<AcademicLevel>();
        User u;

        //protected override CreateParams CreateParams
        //{
        //    get
        //    {
        //        CreateParams cp = base.CreateParams;
        //        cp.ExStyle |= 0x02000000;
        //        return cp;
        //    }
        //}

        public GUI_PaidItemManagement(User u)
        {
            InitializeComponent();
            this.u = u;
            cbbPaidItemType.Items.Clear();
            List<PaitItemType> list = new List<PaitItemType>();
            PaitItemType p1 = new PaitItemType("1", "Giờ giảng");
            PaitItemType p2 = new PaitItemType("2", "Đơn giá");
            PaitItemType p3 = new PaitItemType("3", "Quy đổi giờ giảng");
            PaitItemType p5 = new PaitItemType("4", "Trừ phạt");
            PaitItemType p4 = new PaitItemType("0", "Tất cả");

            list.Add(p1);
            list.Add(p2);
            list.Add(p3);
            list.Add(p5);
            list.Add(p4);
            cbbPaidItemType.DataSource = list;
            cbbPaidItemType.ValueMember = "Id";
            cbbPaidItemType.DisplayMember = "Name";

            academicList.Clear();
            academicList = (List<AcademicLevel>)DAL_DataSerializer.Instance.BinaryDeserialize("Academic Levels\\AcademicLevel.fs");

            cbbAcaLevel.ValueMember = "Code";
            cbbAcaLevel.DisplayMember = "Name";
            cbbAcaLevel.DataSource = academicList;
            load();
            
        }

        //load function
        /// <summary>
        /// Load Paid Item Header function
        /// </summary>
        void loadHeader()
        {
            try
            {
                PaidItemHeader head = DAL_PaidItem.Instance.GetPaidItemsHeader();
                lblAcaLevelName.Text = head.AcademicLevel;
                dtpEffectiveDate.Text = head.ActiveDate.ToString();
                dtpPublishDate.Text = head.PublishDate.ToString();
                txtRule.Text = head.Rule;
                lblCreaterName.Text = head.CreatorName;
                dtpCreatedDate.Text = head.CreatedDate.ToString();
                txtNote.Text = head.Note;
            }
            catch
            {
                PaidItemHeader head = new PaidItemHeader("", DateTime.Now, "", DateTime.Now, DateTime.Now, "", "");
            }
        }

        /// <summary>
        /// Load form after add, edit, delete
        /// </summary>
        void load()
        {
            txtID.Text = "";
            txtName.Text = "";
            txtUnit.Text = "";
            btnAdd.Enabled = false;
            btnUpdate.Enabled = false;
            btnDelete.Enabled = false;
            if (cbbAcaLevel.Items.Count > 0 && cbbPaidItemType.Items.Count > 0)
            {
                string acalv = cbbAcaLevel.SelectedValue.ToString();
                int paidItemType = Int32.Parse(cbbPaidItemType.SelectedValue.ToString());
                dataGridView1.DataSource = DAL_PaidItem.Instance.LoadDataGridView(acalv, paidItemType);
            }
            loadHeader();
        }

        /// <summary>
        /// Check a string is number or not by regex 
        /// </summary>
        /// <param name="pText"></param>
        /// <returns></returns>
        public bool IsNumber(string pText)
        {
            Regex regex = new Regex(@"^[-+]?[0-9]*\.?[0-9]+$");
            return regex.IsMatch(pText);
        }

        //Add, Edit, Delete function
        /// <summary>
        /// "Thêm" button click event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAdd_Click(object sender, EventArgs e)
        {
            errorProvider1.Clear();
            if (!IsNumber(txtUnit.Text.Trim()))
            {
                errorProvider1.SetError(txtUnit, "Chỉ được phép điền số. Vui lòng nhập lại");
                txtUnit.Focus();
                return;
            }
            if (txtName.Text.Equals("")){
                errorProvider1.SetError(txtName, "Xin vui lòng không để trống trường này.");
                txtName.Focus();
                return;
            }
            else
            {
                float unitValue = 0;
                float hourRate = 0;
                if ((cbbPaidItemType.SelectedIndex + 1).ToString().Equals("1") || (cbbPaidItemType.SelectedIndex + 1).ToString().Equals("3"))
                {
                    hourRate = float.Parse(txtUnit.Text.Trim());
                }
                else if ((cbbPaidItemType.SelectedIndex + 1).ToString().Equals("2") || (cbbPaidItemType.SelectedIndex + 1).ToString().Equals("4"))
                {
                    unitValue = float.Parse(txtUnit.Text.Trim());
                }
                string id = DAL_PaidItem.Instance.GetAutoIncrementID(cbbAcaLevel.SelectedValue.ToString().Trim());
                string acalv = cbbAcaLevel.SelectedValue.ToString().Trim();
                int paidItemType = Int32.Parse(cbbPaidItemType.SelectedValue.ToString().Trim());
                if (!txtName.Text.Trim().Equals(""))
                {
                    if (DAL_PaidItem.Instance.Insert(id, txtName.Text.Trim(), hourRate, unitValue, paidItemType, acalv, u.Id, dtpPublishDate.Value))
                    {
                        MessageBox.Show("Thêm thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Lỗi! Định mức bị trùng ID! Vui lòng nhập lại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                load();
            }
            
        }

        /// <summary>
        /// "Sửa" button click event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            errorProvider1.Clear();
            if (!IsNumber(txtUnit.Text.Trim()))
            {
                errorProvider1.SetError(txtUnit, "Chỉ được phép điền số. Vui lòng nhập lại");
                txtUnit.Focus();
                return;
            }
            else
            {
                float unitValue = 0;
                float hourRate = 0;
                if ((cbbPaidItemType.SelectedIndex + 1).ToString().Equals("1") || (cbbPaidItemType.SelectedIndex + 1).ToString().Equals("3"))
                {
                    hourRate = float.Parse(txtUnit.Text.Trim());
                }
                else if ((cbbPaidItemType.SelectedIndex + 1).ToString().Equals("2") || (cbbPaidItemType.SelectedIndex + 1).ToString().Equals("4"))
                {
                    unitValue = float.Parse(txtUnit.Text.Trim());
                }
                if (DAL_PaidItem.Instance.Update(txtID.Text.Trim(), txtName.Text.Trim(), hourRate, unitValue,
                            Int32.Parse(cbbPaidItemType.SelectedValue.ToString().Trim()), cbbAcaLevel.SelectedValue.ToString().Trim()))
                {
                    MessageBox.Show("Sửa thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Không sửa được", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                load();
            }
        }

        /// <summary>
        /// "Xóa" button click event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDelete_Click(object sender, EventArgs e)
        {
            errorProvider1.Clear();
            DialogResult result = MessageBox.Show("Bạn có muốn xóa " + txtName.Text + "?",
                                               "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                if (DAL_PaidItem.Instance.Delete(txtID.Text.Trim(), cbbAcaLevel.SelectedValue.ToString().Trim()))
                {
                    MessageBox.Show("Xóa thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Xóa không thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            load();
        }

        //load data grid view
        /// <summary>
        /// click cell of datagridview
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1 || e.ColumnIndex == -1) return;
            
            if (dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null )
            {
                dataGridView1.CurrentRow.Selected = true;

                txtID.Text = dataGridView1.Rows[e.RowIndex].Cells["ID"].FormattedValue.ToString();
                txtName.Text = dataGridView1.Rows[e.RowIndex].Cells["Tên định mức"].FormattedValue.ToString();
                cbbAcaLevel.SelectedValue = dataGridView1.Rows[e.RowIndex].Cells["Hệ đào tạo"].Value.ToString();
                cbbPaidItemType.Text = dataGridView1.Rows[e.RowIndex].Cells["Loại định mức"].FormattedValue.ToString();
                lblUnit.Text = cbbPaidItemType.Text;

                if ((cbbPaidItemType.SelectedIndex + 1).ToString().Equals("1"))
                {
                    lblUnit.Text = "Giờ giảng:";
                    txtUnit.Text = dataGridView1.Rows[e.RowIndex].Cells["Định mức giờ giảng"].FormattedValue.ToString();
                }
                else if ((cbbPaidItemType.SelectedIndex + 1).ToString().Equals("2") || (cbbPaidItemType.SelectedIndex + 1).ToString().Equals("4"))
                {
                    lblUnit.Text = "Đơn giá:";
                    txtUnit.Text = dataGridView1.Rows[e.RowIndex].Cells["Đơn giá"].FormattedValue.ToString();
                }
                else if ((cbbPaidItemType.SelectedIndex + 1).ToString().Equals("3"))
                {
                    lblUnit.Text = "Định mức quy đổi giờ giảng:";
                    txtUnit.Text = dataGridView1.Rows[e.RowIndex].Cells["Định mức giờ giảng"].FormattedValue.ToString();
                }
                txtName.ReadOnly = false;
                txtUnit.ReadOnly = false;
                btnDelete.Enabled = true;
                btnUpdate.Enabled = true;
            }
            if (dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString().Equals(""))
            {
                btnDelete.Enabled = false;
                btnUpdate.Enabled = false;
            }
            btnAdd.Enabled = false;
        }

        /// <summary>
        /// "Đóng" button click event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        
        private void txtName_TextChanged(object sender, EventArgs e)
        {
            btnAdd.Enabled = true;
            txtUnit.ReadOnly = false;
        }

        /// <summary>
        /// "Hiển thị" button click event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnShow_Click(object sender, EventArgs e)
        {
            /**
             * 
             */
            errorProvider1.Clear();
            load();
            if ((cbbPaidItemType.SelectedIndex + 1).ToString().Equals("1"))
            {
                lblUnit.Text = "Định mức giờ giảng:";
            }
            else if((cbbPaidItemType.SelectedIndex + 1).ToString().Equals("2") || (cbbPaidItemType.SelectedIndex + 1).ToString().Equals("4"))
            {
                lblUnit.Text = "Đơn giá:";
            }
            else if ((cbbPaidItemType.SelectedIndex + 1).ToString().Equals("3"))
            {
                lblUnit.Text = "Định mức quy đổi giờ giảng:";
            }
            txtName.ReadOnly = false;
            txtUnit.ReadOnly = false;
        }

        /// <summary>
        /// "Lưu" button click event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            List<Object> list = (List<Object>)DAL_DataSerializer.Instance.BinaryDeserialize("Paid Item Files\\" + cbbAcaLevel.SelectedValue.ToString() + "PaidItem.fs"); ;
            PaidItemHeader paidItemHeader = new PaidItemHeader(lblCreaterName.Text, dtpCreatedDate.Value, lblAcaLevelName.Text, dtpPublishDate.Value,
                    dtpEffectiveDate.Value, txtRule.Text, txtNote.Text);                
            list.RemoveAt(0);
            list.Insert(0, paidItemHeader);
            SaveFileDialog saveDialog = new SaveFileDialog();
            saveDialog.Title = "Save files";
            saveDialog.FileName = lblAcaLevelName.Text + "PaidItem" + dtpCreatedDate.Value.ToString("ddMMyyyy");
            saveDialog.Filter = "Encrypted files (*.fs)|*.fs";
            saveDialog.FilterIndex = 2;
            if (saveDialog.ShowDialog() == DialogResult.OK)
            {
                DAL_DataSerializer.Instance.BinarySerialize(list, saveDialog.FileName);
                load();
                MessageBox.Show("Lưu thành công.", "Thông báo");
            }
        }

        /// <summary>
        /// Form Load when show form 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GUI_PaidItem_Load(object sender, EventArgs e)
        {
            btnAdd.Enabled = false;
            btnUpdate.Enabled = false;
            btnDelete.Enabled = false;
        }

        private void txtUnit_TextChanged(object sender, EventArgs e)
        {
            btnAdd.Enabled = true;
        }

        private void GUI_PaidItem_FormClosing(object sender, FormClosingEventArgs e)
        {

        }

        private void cbbPaidItemType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ((cbbPaidItemType.SelectedIndex + 1).ToString().Equals("1"))
            {
                lblUnit.Text = "Định mức giờ giảng:";
            }
            else if ((cbbPaidItemType.SelectedIndex + 1).ToString().Equals("2") || (cbbPaidItemType.SelectedIndex + 1).ToString().Equals("4"))
            {
                lblUnit.Text = "Đơn giá:";
            }
            else if ((cbbPaidItemType.SelectedIndex + 1).ToString().Equals("3"))
            {
                lblUnit.Text = "Định mức quy đổi giờ giảng:";
            }
            txtName.Text = "";
            txtUnit.Text = "";
        }
    }
}
