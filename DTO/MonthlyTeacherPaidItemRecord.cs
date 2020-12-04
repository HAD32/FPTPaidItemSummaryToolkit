﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    [Serializable]
    public class MonthlyTeacherPaidItemRecord
    {
        public string Id { get; set; }
        public Staff StaffInfo { get; set; }
        public List<PaidItem> PaidItemList { get; set; }
        public PensionList PensionList { get; set; }
        public string Note { get; set; }
        public float Sum { get; set; }
        public MonthlyTeacherPaidItemRecord()
        {
        }

        public MonthlyTeacherPaidItemRecord(string id, Staff staffInfo, List<PaidItem> paidItemList, PensionList pList, string note, float sum)
        {
            Id = id;
            StaffInfo = staffInfo;
            PaidItemList = paidItemList;
            this.PensionList = pList;
            Note = note;
            Sum = sum;
        }
    }
}
