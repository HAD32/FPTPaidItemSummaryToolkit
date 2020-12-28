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
using System.IO;
using System.Configuration;
using System.Threading;
using MailKit.Net.Smtp;
using MailKit;
using MimeKit;

namespace FPTPaidItemSummaryToolkit
{
    public partial class GUI_MailSelection : Form
    {
        List<MonthlyPaidItemRecord> mpirList;
        List<Lecturer> lecturerList;
        List<Lecturer> checkedLecturerList = new List<Lecturer>();
        private string strAcadStaffAccount;
        private string strAcadDepartmentAddress = "";
        private string strInvigilationSupport;
        private string strCapstoneMarkSupport;
        private string strAttendaceChecker1;
        private string strAttendaceChecker2;
        private string strAttendanceConfirmName;
        DateTime deadline = DateTime.Now;

        public GUI_MailSelection(List<MonthlyPaidItemRecord> mpirList)
        {
            InitializeComponent();
            this.mpirList = mpirList;
            lecturerList = new List<Lecturer>();
            foreach (MonthlyPaidItemRecord mpir in mpirList)
            {
                foreach (MonthlyTeacherPaidItemRecord mtpir in mpir.mtpirList)
                {
                    Lecturer s = mtpir.LecturerInfo;
                    if (!lecturerList.Contains(s))
                    {
                        lecturerList.Add(s);
                        chkReceiverList.Items.Add(s);
                    }
                }
            }
            chkReceiverList.DisplayMember = "Account";
            chkReceiverList.ValueMember = "Email";
            strAcadStaffAccount = ConfigurationManager.AppSettings["strAcadStaffAccount"];
            strAcadDepartmentAddress = ConfigurationManager.AppSettings["strAcadDepartmentAddress"];
            strInvigilationSupport = ConfigurationManager.AppSettings["strInvigilationSupport"];
            strCapstoneMarkSupport = ConfigurationManager.AppSettings["strCapstoneMarkSupport"];
            strAttendaceChecker1 = ConfigurationManager.AppSettings["attendanceChecker1"];
            strAttendaceChecker2 = ConfigurationManager.AppSettings["attendanceChecker2"];
            strAttendanceConfirmName = ConfigurationManager.AppSettings["attendanceConfirmName"];
            checkBoxAllEmail.Checked = true;
        }

        private void checkBoxAllEmail_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxAllEmail.Checked == true)
            {
                for (int i = 0; i < chkReceiverList.Items.Count; i++)
                {
                    chkReceiverList.SetItemChecked(i, true);
                }
            }
            else
            {
                for (int i = 0; i < chkReceiverList.Items.Count; i++)
                {
                    chkReceiverList.SetItemChecked(i, false);
                }
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void chkEmailList_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            Lecturer s = (Lecturer)chkReceiverList.Items[e.Index];
            if (e.NewValue == CheckState.Checked)
            {
                if (checkedLecturerList.Contains(s))
                    return;
                checkedLecturerList.Add(s);
                lblCount.Text = checkedLecturerList.Count + "";
            }
            else if (e.NewValue == CheckState.Unchecked)
            {
                foreach (Lecturer staff in checkedLecturerList)
                {
                    if (s.Id.Equals(staff.Id))
                    {
                        checkedLecturerList.Remove(staff);
                        lblCount.Text = checkedLecturerList.Count + "";
                        return;
                    }
                }
            }
        }

        private void btnSearchEmail_Click(object sender, EventArgs e)
        {
            chkReceiverList.Items.Clear();
            foreach (Lecturer s in lecturerList)
            {
                string normalizedAccount = DAL_Summary.Instance.stringStandardlize(s.Account);
                string normalizedSearchContent = DAL_Summary.Instance.stringStandardlize(txtSearchContent.Text);
                if (normalizedAccount.Contains(normalizedSearchContent))
                {
                    chkReceiverList.Items.Add(s);
                }
            }
            chkReceiverList.DisplayMember = "Account";
            chkReceiverList.ValueMember = "Email";
            for (int i = 0; i < chkReceiverList.Items.Count; i++)
            {
                Lecturer s = (Lecturer)chkReceiverList.Items[i];
                foreach (Lecturer s2 in lecturerList)
                {
                    if (s.Id.Equals(s2.Id))
                    {
                        chkReceiverList.SetItemChecked(i, true);
                        break;
                    }
                }
            }
        }

        private void chkReceiverList_SelectedIndexChanged(object sender, EventArgs e)
        {
            webBrowser1.DocumentText = replaceVariable((Lecturer)chkReceiverList.SelectedItem);
        }

        private string replaceVariable(Lecturer s)
        {
            //List<PaidItem> paidItemsListByStaffId = getMonthlyPaidItemRecordByStaff(s);
            List<MonthlyPaidItemRecord> StaffAllRecord = getStaffListOfTeachingMpir(s);
            StringBuilder htmlTable = new StringBuilder("<table border=\"1\" style=\"border-collapse: collapse\">");
            htmlTable.Append("<tr><td>Academic Level (Hệ đào tạo)</td><td>Campus (Cơ sở đào tạo)</td><td>Payment (x1000d)</td></tr>");
            foreach (MonthlyPaidItemRecord mpir in StaffAllRecord)
            {
                htmlTable.Append("<tr><td>" + mpir.AcadLv.Code + "</td>" +
               "<td>" + mpir.Campus + "</td>" +
               "<td>" + mpir.mtpirList[0].Sum + "</td></tr>");
            }
            StringBuilder body = new StringBuilder();
            string body2 = htmlTable.ToString();
            string templateLocation;
            if (File.Exists(System.AppDomain.CurrentDomain.BaseDirectory + "EmailTemplate.html"))
                templateLocation = System.AppDomain.CurrentDomain.BaseDirectory + "EmailTemplate.html";
            else
                templateLocation = string.Format("{0}Resources\\EmailTemplate.html", Path.GetFullPath(Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, @"..\..\")));
            using (StreamReader reader = new StreamReader(templateLocation))
            {
                body = body.Append(reader.ReadToEnd());
                body = body.Replace("{StaffName}", s.Name);
                body = body.Replace("{ConfirmMonth}", mpirList[0].ToDate.ToString("MMMM"));
                body = body.Replace("{FromDate}", mpirList[0].FromDate.ToString("dd/MM/yyyy"));
                body = body.Replace("{ToDate}", mpirList[0].ToDate.ToString("dd/MM/yyyy"));
                body = body.Replace("{AcadStaffAccount}", strAcadStaffAccount);
                body = body.Replace("{AcadDepartAddress}", strAcadDepartmentAddress);
                body = body.Replace("{InvigilationSupport}", strInvigilationSupport);
                body = body.Replace("{CapstoneMarkSupport}", strCapstoneMarkSupport);
                body = body.Replace("{AttendanceChecker1}", strAttendaceChecker1);
                body = body.Replace("{AttendanceChecker2}", strAttendaceChecker2);
                body = body.Replace("{AttendanceConfirmName}", strAttendanceConfirmName);
                body = body.Replace("{Deadline}", DateTime.Now.ToString("dd/MM/yyyy"));
                body = body.Append(body2);
            }
            string bodyHTML = body.ToString();
            return bodyHTML;
        }

        private List<MonthlyPaidItemRecord> getStaffListOfTeachingMpir(Lecturer inputStaff)
        {
            List<MonthlyPaidItemRecord> staffDetailMonthlyRecords = new List<MonthlyPaidItemRecord>();
            foreach (MonthlyPaidItemRecord mpir in mpirList)
            {
                MonthlyPaidItemRecord newMpir = new MonthlyPaidItemRecord();
                foreach (MonthlyTeacherPaidItemRecord mtpir in mpir.mtpirList)
                {
                    Lecturer s = mtpir.LecturerInfo;
                    if (s.Account == inputStaff.Account)
                    {
                        newMpir.AcadLv = mpir.AcadLv;
                        newMpir.Campus = mpir.Campus;
                        newMpir.mtpirList = new List<MonthlyTeacherPaidItemRecord>();
                        newMpir.mtpirList.Add(mtpir);
                        staffDetailMonthlyRecords.Add(newMpir);
                        break;
                    }
                }
            }
            return staffDetailMonthlyRecords;
        }

        private void btnMail_Click(object sender, EventArgs e)
        {
            CheckForIllegalCrossThreadCalls = false;
            btnMail.Enabled = false;
            //get configured values from App.config 
            string strHost = ConfigurationManager.AppSettings["host"];
            int port = Int32.Parse(ConfigurationManager.AppSettings["port"]);
            string strFrom = ConfigurationManager.AppSettings["fromMail"];
            string strUserName = ConfigurationManager.AppSettings["userName"];
            string strFromPass = ConfigurationManager.AppSettings["password"];

            SmtpClient client = new SmtpClient();
            client.Timeout = 30000;
            client.AuthenticationMechanisms.Remove("XOAUTH2");

            //run email list and send to each mail
            foreach (Lecturer s in checkedLecturerList)
            {
                MimeMessage mail  = new MimeMessage();
                mail.From.Add(new MailboxAddress(strUserName, strFrom));
                mail.To.Add(new MailboxAddress(s.Name, s.Email));
                BodyBuilder builder = new BodyBuilder();
                builder.HtmlBody = replaceVariable(s);

                //define message header
                mail.Subject = "Gửi mail cho giảng viên";
                mail.Body = builder.ToMessageBody();
                bool boolSSL = true;
                try
                {
                    client.Connect(strHost, port, boolSSL);
                    client.Authenticate(strFrom, strFromPass);
                    client.Send(mail);
                }
                catch (Exception)
                {
                    MessageBox.Show("Sai email hoặc mật khẩu.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                Thread.Sleep(1);
                client.Disconnect(true);
            }
            MessageBox.Show("Đã gửi", "Thông báo");
            btnMail.Enabled = false;
        }

        private void btnChangeInfor_Click(object sender, EventArgs e)
        {
            GUI_SupportAndDeadline supportAndDeadline = new GUI_SupportAndDeadline();
            DialogResult result = supportAndDeadline.ShowDialog();
            if (result == DialogResult.OK)
            {
                strAcadStaffAccount = supportAndDeadline.StrAcadStaffAccount;
                strAcadDepartmentAddress = supportAndDeadline.StrAcadDepartmentAddress;
                strInvigilationSupport = supportAndDeadline.StrInvigilationSupport;
                strCapstoneMarkSupport = supportAndDeadline.StrCapstoneMarkSupport;
                deadline = supportAndDeadline.ConfirmDeadline;
                webBrowser1.DocumentText = replaceVariable((Lecturer)chkReceiverList.SelectedItem);
            }
        }
    }
}
