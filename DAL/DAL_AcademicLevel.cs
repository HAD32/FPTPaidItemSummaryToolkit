using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;

namespace DAL
{
    public class DAL_AcademicLevel
    {
        private static DAL_AcademicLevel instance;

        public static DAL_AcademicLevel Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new DAL_AcademicLevel();
                }
                return instance;
            }
            set { instance = value; }
        }

        public string GetFileLocation(string fileName)
        {
            return Path.Combine(Environment.CurrentDirectory, @"Files\", fileName);
        }

        public AcademicLevel GetOneAcaLevelByCode(string code, List<AcademicLevel> list)
        {
            AcademicLevel acaLv = new AcademicLevel();
            foreach (AcademicLevel acaLv2 in list)
            {
                if (acaLv2.Code.Equals(code))
                {
                    acaLv = acaLv2;
                    return acaLv;
                }
            }
            return acaLv;
        }

        public int checkCode(List<AcademicLevel> list, string code)
        {
            for (int i = 0; i < list.Count; i++)
            {
                if (list.ElementAt(i).Code.Equals(code))
                {
                    return i;
                }
            }
            return -1;
        }

        public bool Insert(List<AcademicLevel> list, string code,string name, string description, List<Campus> campusList)
        {
            if (list is object)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    if (checkCode(list, code) != -1)
                        return false;
                }
            }
            else
            {
                list = new List<AcademicLevel>();
            }
            list.Add(new AcademicLevel(code, name, description, campusList));
            try
            {
                DAL_DataSerializer.Instance.BinarySerialize(list, "Academic Levels\\AcademicLevel.fs");
            }
            catch (Exception)
            {
                Directory.CreateDirectory(Environment.CurrentDirectory + "\\Academic Levels");
                DAL_DataSerializer.Instance.BinarySerialize(list, "Academic Levels\\AcademicLevel.fs");
            }
            return true;
        }

        public bool Update(List<AcademicLevel> list, string code, string name ,string description)
        {

            for (int i = 0; i < list.Count; i++)
            {
                if (checkCode(list, code) != -1)
                    break;

            }

            list.ElementAt(checkCode(list, code)).Code = code;
            list.ElementAt(checkCode(list, code)).Name = name;
            list.ElementAt(checkCode(list, code)).Description = description;
            return true;
        }

        public bool Delete(List<AcademicLevel> list, string code)
        {
            for (int i = 0; i < list.Count; i++)
            {
                if (checkCode(list, code) != -1)
                {
                    list.RemoveAt(checkCode(list, code));
                    return true;
                }
            }
            return false;

        }

    }
}
