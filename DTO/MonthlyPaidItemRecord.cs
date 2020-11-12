using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    [Serializable]
    public class MonthlyPaidItemRecord
    {
        //header: creator, acadlv, paiditemFileheader,
        public string Name { get; set; }
        public User CreatorInfo { get; set; }
        public string AcadLv { get; set; }
        public string Campus { get; set; }
        public string StaffFileLocation { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public DateTime LastModifiedDate { get; set; }
        public PaidItemHeader PaidItemFileHeader { get; set; }
        public List<MonthlyTeacherPaidItemRecord> mtpirList { get; set; }

        public MonthlyPaidItemRecord()
        {
        }

        public MonthlyPaidItemRecord(string name, User creatorInfo, string acadLv, string campus, string staffFileLocation, 
            DateTime fromDate, DateTime toDate, DateTime lastModifiedDate, PaidItemHeader paidItemFileHeader, List<MonthlyTeacherPaidItemRecord> mtpirList)
        {
            Name = name;
            CreatorInfo = creatorInfo;
            AcadLv = acadLv;
            Campus = campus;
            StaffFileLocation = staffFileLocation;
            FromDate = fromDate;
            ToDate = toDate;
            LastModifiedDate = lastModifiedDate;
            PaidItemFileHeader = paidItemFileHeader;
            this.mtpirList = mtpirList;
        }
    }
}
