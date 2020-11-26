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
    public partial class GUI_PaidItem : Form
    {
        List<AcademicLevel> academicList = new List<AcademicLevel>();
        User u;
        public GUI_PaidItem(User u)
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
            academicList = (List<AcademicLevel>)DAL_DataSerializer.Instance.BinaryDeserialize("AcademicLevels.sf");

            cbbAcaLevel.ValueMember = "Code";
            cbbAcaLevel.DisplayMember = "Name";
            cbbAcaLevel.DataSource = academicList;
            //load();
            DoubleBuffer.DoubleBuffered(dataGridView1, true);
        }

        


        //load function
        void loadHeader()
        {
            try
            {
                //DAL_PaidItem.Instance.serializeListImmediately(cbbPaidItemType.SelectedValue.ToString(), staffCode);
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
                PaidItemHeader head = new PaidItemHeader("", DateTime.Now, "", DateTime.Now, DateTime.Now, "", "","");

            }
        }

        void load()
        {
            txtID.Text = "";
            txtName.Text = "";
            txtHourRate.Text = "";
            txtUnitValue.Text = "";
            if ((cbbPaidItemType.SelectedIndex +1).ToString().Equals("1") || (cbbPaidItemType.SelectedIndex + 1).ToString().Equals("3"))
            {
                txtUnitValue.Text = "0";
            }
            if ((cbbPaidItemType.SelectedIndex + 1).ToString().Equals("2") || (cbbPaidItemType.SelectedIndex + 1).ToString().Equals("4"))
            {
                txtHourRate.Text = "0";
            }
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

        public bool IsNumber(string pText)
        {
            Regex regex = new Regex(@"^[-+]?[0-9]*\.?[0-9]+$");
            return regex.IsMatch(pText);
        }

        //Add, Edit, Delete
        private void btnAdd_Click(object sender, EventArgs e)
        {
            errorProvider1.Clear();
            if (txtHourRate.Text.Equals("") )
            {
                errorProvider1.SetError(txtHourRate, "Không được để trống định mức giờ giảng");
                txtHourRate.Focus();
                return;
            }
            if (txtUnitValue.Text.Equals(""))
            {
                errorProvider1.SetError(txtUnitValue, "Không được để trống đơn giá");
                txtUnitValue.Focus();
                return;
            }
            if (!IsNumber(txtHourRate.Text.Trim()))
            {
                errorProvider1.SetError(txtHourRate, "Chỉ được phép điền số. Vui lòng nhập lại");
                txtHourRate.Focus();
                return;
            }
            if (!IsNumber(txtUnitValue.Text.Trim()))
            {
                errorProvider1.SetError(txtUnitValue, "Chỉ được phép điền số. Vui lòng nhập lại");
                txtUnitValue.Focus();
                return;
            }
            else
            {
                float hourRate = float.Parse(txtHourRate.Text.Trim());
                float unitValue = float.Parse(txtUnitValue.Text.Trim());
                string id = DAL_PaidItem.Instance.GetAutoIncrementID(cbbAcaLevel.SelectedValue.ToString().Trim());
                string acalv = cbbAcaLevel.SelectedValue.ToString().Trim();
                int paidItemType = Int32.Parse(cbbPaidItemType.SelectedValue.ToString().Trim());
                if (!txtName.Text.Trim().Equals(""))
                {
                    if (DAL_PaidItem.Instance.Insert(id, txtName.Text, hourRate, unitValue, paidItemType, acalv, u.Id, dtpPublishDate.Value))
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

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            errorProvider1.Clear();
            if (!IsNumber(txtHourRate.Text))
            {
                errorProvider1.SetError(txtHourRate, "Chỉ được phép điền số. Vui lòng nhập lại");
                txtHourRate.Focus();
            }
            else if (!IsNumber(txtUnitValue.Text))
            {
                errorProvider1.SetError(txtUnitValue, "Chỉ được phép điền số. Vui lòng nhập lại");
                txtUnitValue.Focus();
            }
            else
            {
                float hourRate = float.Parse(txtHourRate.Text);
                float unitValue = float.Parse(txtUnitValue.Text);

                if (DAL_PaidItem.Instance.Update(txtID.Text, txtName.Text, hourRate, unitValue,
                            Int32.Parse(cbbPaidItemType.SelectedValue.ToString()), cbbAcaLevel.SelectedValue.ToString()))
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

        private void btnDelete_Click(object sender, EventArgs e)
        {
            errorProvider1.Clear();
            DialogResult result = MessageBox.Show("Do you want to delete " + txtName.Text + "?",
                                               "Confirmation Box", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                if (DAL_PaidItem.Instance.Delete(txtID.Text, cbbAcaLevel.SelectedValue.ToString()))
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
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1) return;
            
            if (dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
            {
                dataGridView1.CurrentRow.Selected = true;
                txtID.Text = dataGridView1.Rows[e.RowIndex].Cells["ID"].FormattedValue.ToString();
                txtName.Text = dataGridView1.Rows[e.RowIndex].Cells["Tên định mức"].FormattedValue.ToString();
                txtHourRate.Text = dataGridView1.Rows[e.RowIndex].Cells["Định mức giờ giảng"].Value.ToString();
                txtUnitValue.Text = dataGridView1.Rows[e.RowIndex].Cells["Đơn giá"].Value.ToString();
                cbbAcaLevel.SelectedValue = dataGridView1.Rows[e.RowIndex].Cells["Hệ đào tạo"].Value.ToString();
                if(dataGridView1.Rows[e.RowIndex].Cells["Loại định mức"].FormattedValue.ToString().Equals("Giờ giảng"))
                {
                    cbbPaidItemType.SelectedIndex = 0;
                }
                else if(dataGridView1.Rows[e.RowIndex].Cells["Loại định mức"].FormattedValue.ToString().Equals("Đơn giá"))
                {
                    cbbPaidItemType.SelectedIndex = 1;
                }
                else if(dataGridView1.Rows[e.RowIndex].Cells["Loại định mức"].FormattedValue.ToString().Equals("Quy đổi giờ giảng"))
                {
                    cbbPaidItemType.SelectedIndex = 2;
                }
                btnDelete.Enabled = true;
                btnUpdate.Enabled = true;
                txtName.ReadOnly = false;
                txtHourRate.ReadOnly = false;
                txtUnitValue.ReadOnly = false;
            }
            if (dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString().Equals(""))
            {
                btnDelete.Enabled = false;
                btnUpdate.Enabled = false;
            }
            btnAdd.Enabled = false;
            
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //Combobox Change
        private void cbbPaidItemType_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if ((cbbPaidItemType.SelectedIndex + 1).ToString().Equals("1") || (cbbPaidItemType.SelectedIndex + 1).ToString().Equals("3"))
            //{
            //    txtUnitValue.Text = "0";
            //    txtUnitValue.ReadOnly = true;
            //    txtHourRate.Text = "";
            //}
            //else
            //{
            //    txtUnitValue.ReadOnly = false;
            //}

            //if ((cbbPaidItemType.SelectedIndex + 1).ToString().Equals("2") || (cbbPaidItemType.SelectedIndex + 1).ToString().Equals("4"))
            //{
            //    txtHourRate.Text = "0";
            //    txtHourRate.ReadOnly = true;
            //    txtUnitValue.Text = "";
            //}
            //else
            //{
            //    txtHourRate.ReadOnly = false;
            //}
        }

        private void txtUnitValue_TextChanged(object sender, EventArgs e)
        {
            btnAdd.Enabled = true;
        }

        private void txtHourRate_TextChanged(object sender, EventArgs e)
        {
            btnAdd.Enabled = true;
        }

        private void txtName_TextChanged(object sender, EventArgs e)
        {
            btnAdd.Enabled = true;
        }

        private void btnFilter_Click(object sender, EventArgs e)
        {
            errorProvider1.Clear();
            load();
            if ((cbbPaidItemType.SelectedIndex + 1).ToString().Equals("5"))
            {
                txtName.ReadOnly = true;
                txtHourRate.ReadOnly = true;
                txtUnitValue.ReadOnly = true;
            }
            else
            {
                if ((cbbPaidItemType.SelectedIndex + 1).ToString().Equals("1") || (cbbPaidItemType.SelectedIndex + 1).ToString().Equals("3"))
                {
                    txtUnitValue.Text = "0";
                    txtUnitValue.ReadOnly = true;
                    txtHourRate.Text = "";
                }
                else
                {
                    txtUnitValue.ReadOnly = false;
                    txtName.ReadOnly = false;
                }

                if ((cbbPaidItemType.SelectedIndex + 1).ToString().Equals("2") || (cbbPaidItemType.SelectedIndex + 1).ToString().Equals("4"))
                {
                    txtHourRate.Text = "0";
                    txtHourRate.ReadOnly = true;
                    txtUnitValue.Text = "";
                }
                else
                {
                    txtHourRate.ReadOnly = false;
                    txtName.ReadOnly = false;
                }
            }
            
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            List<Object> list = new List<object>();
            string Key = "";
            GUI_SetKey passForm = new GUI_SetKey();
            DialogResult result = passForm.ShowDialog();
            if (result == DialogResult.OK)
            {
                Key = passForm.Key;
            }
            else
                return;

            try
            {
                list = (List<Object>)DAL_DataSerializer.Instance.BinaryDeserialize(cbbAcaLevel.SelectedValue.ToString() + "PaidItem.sf");
                PaidItemHeader paidItemHeader = new PaidItemHeader(lblCreaterName.Text, dtpCreatedDate.Value, lblAcaLevelName.Text, dtpPublishDate.Value,
                    dtpEffectiveDate.Value, txtRule.Text, txtNote.Text, Key);
                list.RemoveAt(0);
                list.Insert(0, paidItemHeader);
                SaveFileDialog saveDialog = new SaveFileDialog();
                saveDialog.Title = "Save files";
                saveDialog.FileName = lblAcaLevelName.Text + "PaidItem" + dtpCreatedDate.Value.ToString("ddMMyyyy");
                saveDialog.Filter = "Encrypted files (*.sf)|*.sf";
                saveDialog.FilterIndex = 2;
                if (saveDialog.ShowDialog() == DialogResult.OK)
                {

                    DAL_DataSerializer.Instance.BinarySerialize(list, saveDialog.FileName);
                    load();
                    MessageBox.Show("Lưu thành công.", "Thông báo");
                }
            }
            catch
            {
                PaidItemHeader paidItemHeader = new PaidItemHeader(lblCreaterName.Text, dtpCreatedDate.Value, lblAcaLevelName.Text, dtpPublishDate.Value,
                    dtpEffectiveDate.Value, txtRule.Text, txtNote.Text, Key);
                list.RemoveAt(0);
                list.Insert(0, paidItemHeader);
                SaveFileDialog saveDialog = new SaveFileDialog();
                saveDialog.Title = "Save files";
                saveDialog.FileName = lblAcaLevelName.Text + "PaidItem" + dtpCreatedDate.Value.ToString("ddMMyyyy");
                saveDialog.Filter = "Encrypted files (*.sf)|*.sf";
                saveDialog.FilterIndex = 2;
                if (saveDialog.ShowDialog() == DialogResult.OK)
                {

                    DAL_DataSerializer.Instance.BinarySerialize(list, saveDialog.FileName);
                    load();
                    MessageBox.Show("Lưu thành công.", "Thông báo");
                }
            }
            
                
            
        }

        private void GUI_PaidItem_Load(object sender, EventArgs e)
        {
            if ((cbbPaidItemType.SelectedIndex + 1).ToString().Equals("1") || (cbbPaidItemType.SelectedIndex + 1).ToString().Equals("3"))
            {
                txtUnitValue.Text = "0";
                txtUnitValue.ReadOnly = true;
            }
            if ((cbbPaidItemType.SelectedIndex + 1).ToString().Equals("2") || (cbbPaidItemType.SelectedIndex + 1).ToString().Equals("4"))
            {
                txtHourRate.Text = "0";
                txtHourRate.ReadOnly = true;
            }
            btnAdd.Enabled = false;
            btnUpdate.Enabled = false;
            btnDelete.Enabled = false;
        }
    }
}
