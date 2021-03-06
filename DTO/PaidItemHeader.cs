﻿using System;
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
        public DateTime ActiveDate { get; set; }
        public string Rule { get; set; }
        public string Note { get; set; }

        public PaidItemHeader()
        {
        }

        public PaidItemHeader(string creatorName, DateTime createdDate, string academicLevel, DateTime activeDate, string rule, string note)
        {
            CreatorName = creatorName;
            CreatedDate = createdDate;
            AcademicLevel = academicLevel;
            ActiveDate = activeDate;
            Rule = rule;
            Note = note;
        }
    }
}
