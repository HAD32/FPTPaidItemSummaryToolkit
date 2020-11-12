using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using DTO;

namespace DAL
{
    public class DAL_Summary
    {
        private static DAL_Summary instance;

        public static DAL_Summary Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new DAL_Summary();
                }
                return instance;
            }
            set { instance = value; }
        }

        public string convertToUnSign(string s)
        {
            string stFormD = s.Normalize(NormalizationForm.FormD);
            StringBuilder sb = new StringBuilder();
            for (int ich = 0; ich < stFormD.Length; ich++)
            {
                System.Globalization.UnicodeCategory uc = System.Globalization.CharUnicodeInfo.GetUnicodeCategory(stFormD[ich]);
                if (uc != System.Globalization.UnicodeCategory.NonSpacingMark)
                {
                    sb.Append(stFormD[ich]);
                }
            }
            sb = sb.Replace('Đ', 'D');
            sb = sb.Replace('đ', 'd');
            return (sb.ToString().Normalize(NormalizationForm.FormD));
        }

        string stringStandardlize(string item)
        {
            item = item.Trim();
            item = Regex.Replace(item, @"\s+", " ");
            item = convertToUnSign(item);
            item = item.ToLower();
            return item;
        }

        public List<MonthlyTeacherPaidItemRecord> Search(string searchItem, List<MonthlyTeacherPaidItemRecord> mtpirList)
        {
            List<MonthlyTeacherPaidItemRecord> smtpir = new List<MonthlyTeacherPaidItemRecord>();

            foreach (MonthlyTeacherPaidItemRecord m in mtpirList)
            {
                Staff s = m.StaffInfo;
                if (stringStandardlize(s.Name).Contains(stringStandardlize(searchItem)))
                {
                    smtpir.Add(m);
                }
            }
            return smtpir;
        }

        public int GetMtpirIndex(MonthlyTeacherPaidItemRecord mtpir, List<MonthlyTeacherPaidItemRecord> mtpirList)
        {
            for (int i = 0; i < mtpirList.Count; i++)
            {
                Staff s = mtpirList[i].StaffInfo;
                Staff s2 = mtpir.StaffInfo;
                if (s.Id.Equals(s2.Id))
                    return i;
            }
            return -1;
        }

        public List<PaidItem> GetPaidItemList(string AcadLv)
        {
            List<Object> pitlAll = (List<Object>)DAL_DataSerializer.Instance.BinaryDeserialize(AcadLv + "PaidItem.sf");
            List<PaidItem> pitl = new List<PaidItem>();
            foreach (Object obj in pitlAll)
            {
                PaidItem p = new PaidItem();
                if (obj.GetType().Name.Equals("PaidItem"))
                {
                    pitl.Add((PaidItem)obj);
                }
            }
            return pitl;
        }

        public MonthlyTeacherPaidItemRecord GetMtpirByStaffAccount(string Account, List<MonthlyTeacherPaidItemRecord> mtpirList)
        {
            foreach (MonthlyTeacherPaidItemRecord m in mtpirList)
            {
                Staff s = m.StaffInfo;
                if (s.Account.Equals(Account))
                    return m;
            }
            return null;
        }
    }
}
