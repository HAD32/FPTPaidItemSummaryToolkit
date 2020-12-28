using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FPTPaidItemSummaryToolkit
{
    public partial class GUI_SupportAndDeadline : Form
    {
        public string StrAcadStaffAccount { get; set; }
        public string StrAcadDepartmentAddress { get; set; }
        public string StrInvigilationSupport { get; set; }
        public string StrCapstoneMarkSupport { get; set; }
        public string StrAttendanceChecker1 { get; set; }
        public string StrAttendanceChecker2 { get; set; }
        public string StrAttendanceConfirmName { get; set; }
        public string StrSenderMail { get; set; }
        public string StrSenderPassword { get; set; }
        public string StrUsername { get; set; }
        public DateTime ConfirmDeadline { get; set; }

        public GUI_SupportAndDeadline()
        {
            InitializeComponent();
            txtAcadStaffAcc.Text = ConfigurationManager.AppSettings["strAcadStaffAccount"];
            txtAcadDepartmentAddress.Text = ConfigurationManager.AppSettings["strAcadDepartmentAddress"];
            txtInvigilationSupport.Text = ConfigurationManager.AppSettings["strInvigilationSupport"];
            txtCapstoneGradeSupport.Text = ConfigurationManager.AppSettings["strCapstoneMarkSupport"];
            txtAttendanceChecker1.Text = ConfigurationManager.AppSettings["attendanceChecker1"];
            txtAttendanceChecker2.Text = ConfigurationManager.AppSettings["attendanceChecker2"];
            txtAttendanceConfirmName.Text = ConfigurationManager.AppSettings["attendanceConfirmName"];
            txtSenderEmail.Text = ConfigurationManager.AppSettings["fromMail"];
            txtPassword.Text = ConfigurationManager.AppSettings["password"];
            txtUsername.Text = ConfigurationManager.AppSettings["userName"];
        }

        private static string GetSetting(string key)
        {
            return ConfigurationManager.AppSettings[key];
        }

        private static void SetSetting(string key, string value)
        {
            Configuration configuration = ConfigurationManager.OpenExeConfiguration(Application.ExecutablePath);
            configuration.AppSettings.Settings.Remove(key);
            configuration.AppSettings.Settings.Add(key, value);
            configuration.Save(ConfigurationSaveMode.Full, true);
            ConfigurationManager.RefreshSection("appSettings");
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            StrAcadStaffAccount = txtAcadStaffAcc.Text;
            StrAcadDepartmentAddress = txtAcadDepartmentAddress.Text;
            StrInvigilationSupport = txtInvigilationSupport.Text;
            StrCapstoneMarkSupport = txtCapstoneGradeSupport.Text;
            StrAttendanceChecker1 = txtAttendanceChecker1.Text;
            StrAttendanceChecker2 = txtAttendanceChecker2.Text;
            StrAttendanceConfirmName = txtAttendanceConfirmName.Text;
            StrSenderMail = txtSenderEmail.Text;
            StrSenderPassword = txtPassword.Text;
            StrUsername = txtUsername.Text;
            ConfirmDeadline = dtpConfirmDeadline.Value;
            this.DialogResult = DialogResult.OK;
            SetSetting("strAcadStaffAccount", txtAcadStaffAcc.Text);
            SetSetting("strAcadDepartmentAddress", txtAcadDepartmentAddress.Text);
            SetSetting("strInvigilationSupport", txtInvigilationSupport.Text);
            SetSetting("strCapstoneMarkSupport", txtCapstoneGradeSupport.Text);
            SetSetting("attendanceChecker1", txtAttendanceChecker1.Text);
            SetSetting("attendanceChecker2", txtAttendanceChecker2.Text);
            SetSetting("attendanceConfirmName", txtAttendanceConfirmName.Text);
            SetSetting("fromMail", txtSenderEmail.Text);
            SetSetting("password", txtPassword.Text);
            SetSetting("userName", txtUsername.Text);
            MessageBox.Show("Lưu thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
