using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    [Serializable]
    public class PensionList
    {
        public string pensionListName { get; set; }
        public List<Pension> pensionList { get; set; }
        public AcademicLevel academicLevel { get; set; }

        public PensionList()
        {
        }

        public PensionList(string pensionListName, List<Pension> pensionList, AcademicLevel academicLevels)
        {
            this.pensionListName = pensionListName;
            this.pensionList = pensionList;
            this.academicLevel = academicLevels;
        }
    }
}
