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
        public string formula { get; set; }
        public MonthlyPaidItemRecord mpir { get; set; }
        public string inputItem;
        bool flag = false;
        public GUI_CreateFormula(MonthlyPaidItemRecord mpir, string columnName)
        {
            InitializeComponent();
            this.mpir = mpir;
            lblColumn.Text = columnName;
            inputItem = columnName;
            foreach(Pension p in mpir.mtpirList[0].PensionList.pensionList)
            {
                lstColumn.Items.Add(p.PensionName);
                if (p.PensionName.Trim().Equals(inputItem.Trim()))
                {
                    if (p.PensionFormula is object)
                        txtFormula.Text = p.PensionFormula;
                }
            }
            foreach (PaidItem p in mpir.mtpirList[0].PaidItemList)
            {
                lstColumn.Items.Add(p.Name);
            }
        }

        private void lstColumn_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (flag)
                return;
            txtFormula.AppendText(" "+lstColumn.SelectedItem.ToString().Trim() + " ");
            flag = true;
        }

        private void txtFormula_TextChanged(object sender, EventArgs e)
        {
            if (!flag)
                return;
            string shortText = txtFormula.Text.Trim();
            if (shortText.EndsWith("+") || shortText.EndsWith("-") || shortText.EndsWith("*") || shortText.EndsWith(":"))
            {
                flag = false;
            }
            else
                flag = true;
        }

        private void btnAppend_Click(object sender, EventArgs e)
        {
            this.formula = txtFormula.Text;
            if(formula.Trim().EndsWith("+")|| formula.Trim().EndsWith("-") || formula.Trim().EndsWith("*") || formula.Trim().EndsWith(":"))
            {
                ToolTip tt = new ToolTip();
                tt.IsBalloon = true;
                tt.UseFading = true;
                tt.Show("Công thức không hợp lệ, xin hãy kiểm tra lại.", txtFormula, 60, -50, 2000);
                txtFormula.Focus();
                return;
            }
            if (!ValidateFormula(formula))
                return;
            this.DialogResult = DialogResult.OK;
            foreach(MonthlyTeacherPaidItemRecord m in mpir.mtpirList)
            {
                foreach(Pension p in m.PensionList.pensionList)
                {
                    if (p.PensionName.Equals(inputItem))
                    {
                        p.PensionValue = ResolveFormula(formula, m);
                        p.PensionFormula = formula;
                        break;
                    }
                }
            }
            MessageBox.Show("Nhập dữ liệu thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Close();
        }

        bool ValidateFormula(string formula)
        {
            string testString = string.Concat(formula.Split(default(string[]), StringSplitOptions.RemoveEmptyEntries));
            if (testString.EndsWith("+") || testString.EndsWith("-") || testString.EndsWith("*") || testString.EndsWith(":"))
            {
                ToolTip tt = new ToolTip();
                tt.IsBalloon = true;
                tt.UseFading = true;
                tt.Show("Công thức không hợp lệ, xin hãy kiểm tra lại.", txtFormula, 60, -50, 2000);
                txtFormula.Focus();
                return false;
            }
            char[] operators = { '+', '-', '*', ':' }; 
            char[] FormulaInChar = testString.ToCharArray();
            for(int i=0; i < FormulaInChar.Length; i++)
            {
                if (operators.Contains<char>(FormulaInChar[i]))
                {
                    if (operators.Contains<char>(FormulaInChar[i + 1]))
                    {
                        ToolTip tt = new ToolTip();
                        tt.IsBalloon = true;
                        tt.UseFading = true;
                        tt.Show("Công thức không hợp lệ, xin hãy kiểm tra lại.", txtFormula, 60, -50, 2000);
                        txtFormula.Focus();
                        return false;
                    }
                }
            }
            return true;
        }

        private float ResolveFormula(string formula, MonthlyTeacherPaidItemRecord m)
        {
            string[] variables = formula.Split(new char[] { '+', '-', '*', ':' });
            List<char> operators = new List<char>();
            foreach (char c in formula)
            {
                if (c.Equals('+') || c.Equals('-') || c.Equals('*'))
                {
                    operators.Add(c);
                }
                else if (c.Equals(':'))
                    operators.Add('/');
            }
            string finalFormula = "";
            for (int i = 0; i < variables.Length; i++)
            {
                bool found = false;
                foreach(Pension p in m.PensionList.pensionList)
                {
                    if (p.PensionName.Trim().Equals(variables[i].Trim()))
                    {
                        finalFormula += p.PensionValue;
                        try
                        {
                            finalFormula += operators[i];
                        }
                        catch (ArgumentOutOfRangeException) { }
                        found = true;
                    }
                }
                if (found)
                    continue;
                foreach (PaidItem p in m.PaidItemList)
                {
                    if (p.Name.Trim().Equals(variables[i].Trim()))
                    {
                        finalFormula += p.Value; 
                        try
                        {
                            finalFormula += operators[i]; 
                        }
                        catch (ArgumentOutOfRangeException) { }
                        found = true;
                    }
                }
                if (found)
                    continue;
                if (!found)
                {
                    try
                    {
                        float value = float.Parse(variables[i]);
                        finalFormula += value;
                        try
                        {
                            finalFormula += operators[i];
                        }
                        catch (ArgumentOutOfRangeException) { }
                    }
                    catch (FormatException) { }
                }
            }
            NCalc.Expression expression = new Expression(finalFormula);
            string r = expression.Evaluate().ToString();
            return float.Parse(r); 
        }

        private void btnDivide_Click(object sender, EventArgs e)
        {
            if (!flag)
                return;
            txtFormula.AppendText(":");
            flag = false;
        }

        private void btnMultiply_Click(object sender, EventArgs e)
        {
            if (!flag)
                return;
            txtFormula.AppendText("*");
            flag = false;
        }

        private void btnMinus_Click(object sender, EventArgs e)
        {
            if (!flag)
                return;
            txtFormula.AppendText("-");
            flag = false;
        }

        private void btnPlus_Click(object sender, EventArgs e)
        {
            if (!flag)
                return;
            txtFormula.AppendText("+");
            flag = false;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
