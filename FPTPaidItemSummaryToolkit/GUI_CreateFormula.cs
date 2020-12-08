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
using NCalc;
namespace FPTPaidItemSummaryToolkit
{
    public partial class GUI_CreateFormula : Form
    {
        char[] operators = { '+', '-', '*', '/' };
        public string formula { get; set; }
        public MonthlyPaidItemRecord mpir { get; set; }
        public string inputItem;
        List<string> selectedItems = new List<string>();
        private bool ignoreSelectedIndexChanged;

        public GUI_CreateFormula(MonthlyPaidItemRecord mpir, string columnName)
        {
            InitializeComponent();
            this.mpir = mpir;
            lblColumn.Text = columnName;
            inputItem = columnName;
            foreach(Pension p in mpir.mtpirList[0].PensionList.pensionList)
            {
                selectedItems.Add(p.PensionName.Trim());
                if (p.PensionName.Trim().Equals(inputItem.Trim()))
                {
                    if (p.PensionFormula is object)
                        txtFormula.Text = p.PensionFormula;
                }
            }
            foreach (PaidItem p in mpir.mtpirList[0].PaidItemList)
            {
                selectedItems.Add(p.Name.Trim());
            }
            ignoreSelectedIndexChanged = true;
            lstColumn.DataSource = selectedItems;
            lstColumn.SelectedIndex = -1;
            ignoreSelectedIndexChanged = false;
        }

        private void lstColumn_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ignoreSelectedIndexChanged) return;
            string shortText = txtFormula.Text.Trim();
            if (string.IsNullOrWhiteSpace(shortText))
            {
                txtFormula.AppendText(" " + lstColumn.SelectedItem.ToString().Trim() + " ");
            }
            if (shortText.EndsWith("+") || shortText.EndsWith("-") || shortText.EndsWith("*") || shortText.EndsWith("/")|| shortText.EndsWith("("))
            {
                txtFormula.AppendText(" " + lstColumn.SelectedItem.ToString().Trim() + " ");
            }
        }

        private void txtFormula_TextChanged(object sender, EventArgs e)
        {
        }

        private void btnAppend_Click(object sender, EventArgs e)
        {
            this.formula = txtFormula.Text.Trim();
            foreach(MonthlyTeacherPaidItemRecord m in mpir.mtpirList)
            {
                foreach(Pension p in m.PensionList.pensionList)
                {
                    if (p.PensionName.Equals(inputItem))
                    {
                        try
                        {
                            p.PensionValue = ResolveFormula(formula, m);
                        }
                        catch (NCalc.EvaluationException)
                        {
                            ToolTip tt = new ToolTip();
                            tt.IsBalloon = true;
                            tt.UseFading = true;
                            tt.Show("Công thức không hợp lệ, xin hãy kiểm tra lại.", txtFormula, 60, -50, 2000);
                            txtFormula.Focus();
                            return;
                        }
                        
                        p.PensionFormula = formula;
                        break;
                    }
                }
            }
            this.DialogResult = DialogResult.OK;
            MessageBox.Show("Nhập dữ liệu thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Close();
        }

        private float ResolveFormula(string formula, MonthlyTeacherPaidItemRecord m)
        {
            foreach(string s in selectedItems)
            {
                bool found = false;
                foreach (Pension p in m.PensionList.pensionList)
                {
                    if (p.PensionName.Trim().Equals(s))
                    {
                        formula = formula.Replace(s, p.PensionValue.ToString());
                        found = true;
                    }
                }
                if (found)
                    continue;
                foreach (PaidItem p in m.PaidItemList)
                {
                    if (p.Name.Trim().Equals(s))
                    {
                        formula = formula.Replace(s, p.Value.ToString());
                        found = true;
                    }
                }
            }
            NCalc.Expression expression = new Expression(formula);
            string r = expression.Evaluate().ToString();
            return float.Parse(r);
        }

        private void btnDivide_Click(object sender, EventArgs e)
        {
            string shortText = txtFormula.Text.Trim();
            if (shortText.EndsWith("+") || shortText.EndsWith("-") || shortText.EndsWith("*") || shortText.EndsWith("/"))
            {
                return;
            }
            txtFormula.AppendText("/");
        }

        private void btnMultiply_Click(object sender, EventArgs e)
        {
            string shortText = txtFormula.Text.Trim();
            if (shortText.EndsWith("+") || shortText.EndsWith("-") || shortText.EndsWith("*") || shortText.EndsWith("/"))
            {
                return;
            }
            txtFormula.AppendText("*");
        }

        private void btnMinus_Click(object sender, EventArgs e)
        {
            string shortText = txtFormula.Text.Trim();
            if (shortText.EndsWith("+") || shortText.EndsWith("-") || shortText.EndsWith("*") || shortText.EndsWith("/"))
            {
                return;
            }
            txtFormula.AppendText("-");
        }

        private void btnPlus_Click(object sender, EventArgs e)
        {
            string shortText = txtFormula.Text.Trim();
            if (shortText.EndsWith("+") || shortText.EndsWith("-") || shortText.EndsWith("*") || shortText.EndsWith("/"))
            {
                return;
            }
            txtFormula.AppendText("+");
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
