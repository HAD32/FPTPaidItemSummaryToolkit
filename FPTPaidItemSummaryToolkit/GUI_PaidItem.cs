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
    public partial class GUI_PaidItem : Form
    {
        List<AcademicLevel> academicList = new List<AcademicLevel>();

        public string staffCode = "";
        public GUI_PaidItem(string staffCode)
        {
            InitializeComponent();
            this.staffCode = staffCode;
            cbbPaidItemType.Items.Clear();
            List<PaitItemType> list = new List<PaitItemType>();
            PaitItemType p1 = new PaitItemType("1", "Giờ giảng");
            PaitItemType p2 = new PaitItemType("2", "Đơn giá");
            PaitItemType p3 = new PaitItemType("3", "Quy đổi giờ giảng");
            list.Add(p1);
            list.Add(p2);
            list.Add(p3);
            cbbPaidItemType.DataSource = list;
            cbbPaidItemType.ValueMember = "Id";
            cbbPaidItemType.DisplayMember = "Name";

            academicList.Clear();
            academicList = (List<AcademicLevel>)DAL_DataSerializer.Instance.BinaryDeserialize("AcademicLevels.sf");

            cbbAcaLevel.ValueMember = "Code";
            cbbAcaLevel.DisplayMember = "Code";
            cbbAcaLevel.DataSource = academicList;
            load();
        }

        //load function
        void loadHeader()
        {
            try
            {
                PaidItemHeader head = DAL_PaidItem.Instance.GetPaidItemsHeader();
                lblAcaLevelName.Text = head.AcademicLevel;
                lblEffectiveDate.Text = head.ActiveDate.ToString();
                txtRule.Text = head.Rule;
                lblCreaterName.Text = head.CreatorName;
                lblCreatedDate.Text = head.CreatedDate.ToString();
                txtNote.Text = head.Note;
            }
            catch
            {
                PaidItemHeader head = new PaidItemHeader("", DateTime.Now, "", DateTime.Now, "", "");

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
                txtUnitValue.Text = "1";
            }
            if ((cbbPaidItemType.SelectedIndex + 1).ToString().Equals("2"))
            {
                txtHourRate.Text = "1";
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

        //Add, Edit, Delete
        private void btnAdd_Click(object sender, EventArgs e)
        {
            errorProvider1.Clear();
            if (txtHourRate.Text.Equals("") )
            {
                errorProvider1.SetError(txtHourRate, "Không được để trống định mức giờ giảng");
                txtHourRate.Focus();
            }
            else if (txtUnitValue.Text.Equals(""))
            {
                errorProvider1.SetError(txtUnitValue, "Không được để trống đơn giá");
                txtHourRate.Focus();
            }
            else
            {
                float hourRate = float.Parse(txtHourRate.Text);
                float unitValue = float.Parse(txtUnitValue.Text);
                string id = DAL_PaidItem.Instance.GetAutoIncrementID(cbbAcaLevel.SelectedValue.ToString());
                string acalv = cbbAcaLevel.SelectedValue.ToString();
                int paidItemType = Int32.Parse(cbbPaidItemType.SelectedValue.ToString());
                if (!txtName.Text.Trim().Equals(""))
                {
                    if (DAL_PaidItem.Instance.Insert(id, txtName.Text, hourRate, unitValue, paidItemType, acalv, staffCode))
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

        private void btnDelete_Click(object sender, EventArgs e)
        {
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
                txtHourRate.Text = dataGridView1.Rows[e.RowIndex].Cells["Định mức giờ giảng"].FormattedValue.ToString();
                txtUnitValue.Text = dataGridView1.Rows[e.RowIndex].Cells["Đơn giá"].FormattedValue.ToString();
                cbbAcaLevel.SelectedValue = dataGridView1.Rows[e.RowIndex].Cells["Hệ đào tạo"].FormattedValue.ToString();
                cbbPaidItemType.SelectedValue = dataGridView1.Rows[e.RowIndex].Cells["Loại định mức"].FormattedValue.ToString();
            }
            btnUpdate.Enabled = true;
            btnAdd.Enabled = false;
            btnDelete.Enabled = true;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //Combobox Change
        private void cbbAcaLevel_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbbAcaLevel.Items.Count > 0 && cbbPaidItemType.Items.Count > 0)
            {
                string acaLv = cbbAcaLevel.SelectedValue.ToString();
                string paidItemType = cbbPaidItemType.SelectedValue.ToString();
                dataGridView1.DataSource = DAL_PaidItem.Instance.LoadDataGridView(acaLv, Int32.Parse(paidItemType));
            }
            loadHeader();
        }

        private void cbbPaidItemType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbbAcaLevel.Items.Count > 0 && cbbPaidItemType.Items.Count > 0)
            {
                string acaLv = cbbAcaLevel.SelectedValue.ToString();
                string paidItemType = cbbPaidItemType.SelectedValue.ToString();
                dataGridView1.DataSource = DAL_PaidItem.Instance.LoadDataGridView(acaLv,
                    Int32.Parse(paidItemType));
            }
            if ((cbbPaidItemType.SelectedIndex + 1).ToString().Equals("1") || (cbbPaidItemType.SelectedIndex + 1).ToString().Equals("3"))
            {
                txtUnitValue.Text = "1";
                txtUnitValue.ReadOnly = true;
                txtHourRate.Text = "";
            }
            else
            {
                txtUnitValue.ReadOnly = false;
            }

            if ((cbbPaidItemType.SelectedIndex + 1).ToString().Equals("2"))
            {
                txtHourRate.Text = "1";
                txtHourRate.ReadOnly = true;
                txtUnitValue.Text = "";
            }
            else
            {
                txtHourRate.ReadOnly = false;
            }
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
    }
}
