﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using DAL;
using DTO;

namespace FPTPaidItemSummaryToolkit
{
    public partial class GUI_UserInfoManagement : Form
    {
        User u;
        private bool isSaved = false;

        public GUI_UserInfoManagement()
        {
            InitializeComponent();
            try
            {
                u = (User)DAL_DataSerializer.Instance.BinaryDeserialize("UserInfo\\User.fs");
                txtCode.Text = u.Id;
                txtEmail.Text = u.Email;
                isSaved = true;
            }
            catch (Exception)
            {
                u = new User();
                isSaved = false;
            }
        }

        bool ValidateLogin()
        {
            string regexStr = @"^[\w!#$%&'*+\-/=?\^_`{|}~]+(\.[\w!#$%&'*+\-/=?\^_`{|}~]+)*" + "@"
                                   + @"((([\-\w]+\.)+[a-zA-Z]{2,4})|(([0-9]{1,3}\.){3}[0-9]{1,3}))$";
            errorProvider1.Clear();
            if (txtCode.Text.Equals(""))
            {
                errorProvider1.SetError(txtCode, "Không để trống trường này");
                txtCode.Focus();
                return false;
            }

            if (txtEmail.Text.Equals(""))
            {
                errorProvider1.SetError(txtEmail, "Không để trống trường này");
                txtEmail.Focus();
                return false;
            }
            else if (!Regex.IsMatch(txtEmail.Text, regexStr))
            {
                errorProvider1.SetError(txtEmail, "Không đúng định dạng email.");
                txtEmail.Focus();
                return false;
            }
            return true;
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (ValidateLogin())
            {
                DAL_DataSerializer.Instance.BinarySerialize(u, "UserInfo\\User.fs");
                isSaved = true;
                MessageBox.Show("Lưu thông tin người dùng thành công", "Thông báo",MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}