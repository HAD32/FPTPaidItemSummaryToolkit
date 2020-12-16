using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;

namespace DAL
{
    public class DAL_FinalCheck
    {
        private static DAL_FinalCheck instance;

        public static DAL_FinalCheck Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new DAL_FinalCheck();
                }
                return instance;
            }
            set { instance = value; }
        }

        public List<CheckResult> SummaryResult(MonthlyPaidItemRecord mpir)
        {
            List<CheckResult> CheckList = GenerateCheckList(mpir);
            foreach (MonthlyTeacherPaidItemRecord mtpir in mpir.mtpirList)
            {
                foreach (CheckResult result in CheckList)
                {
                    foreach (Pension p in mtpir.PensionList.pensionList)
                    {
                        if (result.Name.Trim().Equals(p.PensionName.Trim()))
                        {
                            float pensionValue = float.Parse(p.PensionValue);
                            if (pensionValue > result.Maximum)
                                result.Maximum = pensionValue;
                            if (result.Minimum == -1)
                                result.Minimum = pensionValue;
                            else if (pensionValue < result.Minimum)
                                result.Minimum = pensionValue;
                        }
                    }
                    foreach (PaidItem p in mtpir.PaidItemList)
                    {
                        if (result.Name.Trim().Equals(p.Name.Trim()))
                        {
                            if (p.Value > result.Maximum)
                                result.Maximum = p.Value;
                            if (result.Minimum == -1)
                                result.Minimum = p.Value;
                            else if (p.Value < result.Minimum)
                                result.Minimum = p.Value;
                        }
                    }
                }
            }
            return CheckList;
        }

        private List<CheckResult> GenerateCheckList(MonthlyPaidItemRecord mpir)
        {
            List<CheckResult> checkList = new List<CheckResult>();
            foreach (Pension p in mpir.mtpirList[0].PensionList.pensionList)
            {
                CheckResult check = new CheckResult(p.PensionName, -1, -1);
                try
                {
                    float testString = float.Parse(p.PensionValue);
                    checkList.Add(check);
                }
                catch (Exception) { }
            }
            foreach (PaidItem p in mpir.mtpirList[0].PaidItemList)
            {
                CheckResult check = new CheckResult(p.Name, -1, -1);
                checkList.Add(check);
            }
            return checkList;
        }
    }
}
