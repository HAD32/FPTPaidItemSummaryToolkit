using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    [Serializable]
    public class PaidItemHeader
    {
        public string CreatorName { get; set; }
        public DateTime CreatedDate { get; set; }
        public string AcademicLevel { get; set; }
        public DateTime PublishDate { get; set; }
        public DateTime ActiveDate { get; set; }
        public string Rule { get; set; }
        public string Note { get; set; }
        public string Key { get; set; }
        public PaidItemHeader()
        {
        }

        public PaidItemHeader(string creatorName, DateTime createdDate, string academicLevel, DateTime publishDate, DateTime activeDate, string rule, string note, string key)
        {
            CreatorName = creatorName;
            CreatedDate = createdDate;
            AcademicLevel = academicLevel;
            PublishDate = publishDate;
            ActiveDate = activeDate;
            Rule = rule;
            Note = note;
            Key = key;
        }
    }
}
