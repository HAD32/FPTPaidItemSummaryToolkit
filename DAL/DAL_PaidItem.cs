using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;

namespace DAL
{
    public class DAL_PaidItem
    {
        private static DAL_PaidItem instance;

        public static DAL_PaidItem Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new DAL_PaidItem();
                }
                return instance;
            }
            set { instance = value; }
        }

        List<Object> paidItemsList = new List<Object>();

        public DataTable LoadDataGridView(string acaLevel, int paidItemType)
        {
            DataTable dt = new DataTable();
            List<Object> filterList = new List<Object>();
            try
            {
                paidItemsList = (List<Object>)DAL_DataSerializer.Instance.BinaryDeserialize(acaLevel + "PaidItem.sf");
                if(paidItemType == 0)
                {
                    dt.Columns.Add("ID");
                    dt.Columns.Add("Tên định mức");
                    dt.Columns.Add("Định mức giờ giảng");
                    dt.Columns.Add("Đơn giá");
                    dt.Columns.Add("Hệ đào tạo");
                    dt.Columns.Add("Loại định mức");

                    if (paidItemsList.Count > 0)
                    {
                        foreach (Object obj in paidItemsList)
                        {
                            if (obj.GetType().Name.Equals("PaidItem"))
                            {
                                string typeIDString = "";
                                DataRow row = dt.NewRow();
                                PaidItem p = (PaidItem)obj;
                                row["ID"] = p.Id;
                                row["Tên định mức"] = p.Name;
                                row["Định mức giờ giảng"] = p.Rate;
                                row["Đơn giá"] = p.UnitValue;
                                row["Hệ đào tạo"] = p.AcadLevelCode;
                                if (p.TypeId == 1)
                                {
                                    typeIDString = "Giờ giảng";
                                }
                                else if (p.TypeId == 2)
                                {
                                    typeIDString = "Đơn giá";
                                }
                                else if (p.TypeId == 3)
                                {
                                    typeIDString = "Quy đổi giờ giảng";
                                }
                                else if (p.TypeId == 4)
                                    typeIDString = "Trừ phạt";
                                row["Loại định mức"] = typeIDString;
                                dt.Rows.Add(row);
                            }
                        }
                    }
                    else
                    {
                        dt.Rows.Add("");
                    }
                }
                else
                {
                    filterList = devideListByAcaLevelNameAndPaidItemType(acaLevel, paidItemType);

                    dt.Columns.Add("ID");
                    dt.Columns.Add("Tên định mức");
                    dt.Columns.Add("Định mức giờ giảng");
                    dt.Columns.Add("Đơn giá");
                    dt.Columns.Add("Hệ đào tạo");
                    dt.Columns.Add("Loại định mức");

                    if (filterList.Count > 0)
                    {
                        foreach (Object obj in filterList)
                        {
                            if (obj.GetType().Name.Equals("PaidItem"))
                            {
                                string typeIDString = "";
                                DataRow row = dt.NewRow();
                                PaidItem p = (PaidItem)obj;
                                row["ID"] = p.Id;
                                row["Tên định mức"] = p.Name;
                                row["Định mức giờ giảng"] = p.Rate;
                                row["Đơn giá"] = p.UnitValue;
                                row["Hệ đào tạo"] = p.AcadLevelCode;
                                if (p.TypeId == 1)
                                {
                                    typeIDString = "Giờ giảng";
                                }
                                else if (p.TypeId == 2)
                                {
                                    typeIDString = "Đơn giá";
                                }
                                else if (p.TypeId == 3)
                                {
                                    typeIDString = "Quy đổi giờ giảng";
                                }
                                else if (p.TypeId == 4)
                                    typeIDString = "Trừ phạt";
                                row["Loại định mức"] = typeIDString;
                                dt.Rows.Add(row);
                            }
                        }
                    }
                    else
                    {
                        dt.Rows.Add("");
                    }
                }
                
            }
            catch (Exception ex)
            {
                dt.Columns.Clear();
                dt.Columns.Add("ID");
                dt.Columns.Add("Tên định mức");
                dt.Columns.Add("Định mức giờ giảng");
                dt.Columns.Add("Đơn giá");
                dt.Columns.Add("Hệ đào tạo");
                dt.Columns.Add("Loại định mức");
            }
            return dt;
        }

        public List<Object> devideListByAcaLevelNameAndPaidItemType(string acaLevelName, int paidItemType)
        {
            List<Object> filterListbyPaidItemType = new List<Object>();
            foreach (Object obj in paidItemsList)
            {
                if (obj.GetType().Name.Equals("PaidItem"))
                {
                    PaidItem p = (PaidItem)obj;
                    if (p.TypeId == paidItemType)
                    {
                        filterListbyPaidItemType.Add(p);
                    }
                }
            }

            return filterListbyPaidItemType;
        }

        public string GetAutoIncrementID(string acaLevelName)
        {
            string incrementId = "";
            int count = 1;

            List<string> listID = new List<string>();
            try
            {
                for (int i = 0; i < paidItemsList.Count; i++)
                {
                    if (paidItemsList.ElementAt(i).GetType().Name.Equals("PaidItem"))
                    {
                        PaidItem item = (PaidItem)paidItemsList.ElementAt(i);
                        listID.Add(item.Id);
                    }
                }

                while (true)
                {
                    incrementId = acaLevelName + count;
                    if (!listID.Contains(incrementId))
                    {
                        break;
                    }
                    count++;
                }
            }
            catch (Exception ex)
            {
                incrementId = acaLevelName + "1";
            }
            return incrementId;
        }

        public PaidItem GetOneAcaLevelById(string code)
        {
            PaidItem acaLv = new PaidItem();
            foreach (Object obj in paidItemsList)
            {
                PaidItem acaLv2 = (PaidItem)obj;
                if (acaLv2.Id.Equals(code))
                {
                    acaLv = acaLv2;
                    return acaLv;
                }
            }
            return acaLv;
        }

        public int checkCode(List<Object> list, string id)
        {
            list = paidItemsList;
            for (int i = 0; i < list.Count; i++)
            {
                if (list.ElementAt(i).GetType().Name.Equals("PaidItem"))
                {
                    PaidItem item = (PaidItem)list.ElementAt(i);
                    if (item.Id.Equals(id))
                    {
                        return i;
                    }
                }
            }
            return -1;
        }

        public PaidItemHeader GetPaidItemsHeader()
        {
            return (PaidItemHeader)paidItemsList.ElementAt(0);
        }

        //Insert, Delete, Update
        public bool Insert(string id, string name, float rate, float unitValue, int typeId, string acaLevelCode, string creatorCode, DateTime publishDate)
        {
            List<Object> testList;
            try
            {
                testList = new List<Object>();
                PaidItemHeader pih = new PaidItemHeader(creatorCode, DateTime.Now, acaLevelCode, publishDate, DateTime.Now, "ABC/31T", "","");
                testList = (List<Object>)DAL_DataSerializer.Instance.BinaryDeserialize(acaLevelCode + "PaidItem.sf");
                testList.RemoveAt(0);
                testList.Insert(0, pih);
                testList.Add(new PaidItem(id, name, rate, unitValue, typeId, acaLevelCode));
                DAL_DataSerializer.Instance.BinarySerialize(testList, acaLevelCode + "PaidItem.sf");
            }
            catch (Exception ex)
            {
                testList = new List<Object>();
                PaidItemHeader pih = new PaidItemHeader(creatorCode, DateTime.Now, acaLevelCode, DateTime.Now, DateTime.Now, "ABC/31T", "","");
                testList.Add(pih);
                testList.Add(new PaidItem(id, name, rate, unitValue, typeId, acaLevelCode));
                DAL_DataSerializer.Instance.BinarySerialize(testList, acaLevelCode + "PaidItem.sf");
            }
            return true;
        }

        public bool Update(string id, string name, float rate, float unitValue, int typeId, string acaLevelCode)
        {
            paidItemsList = (List<Object>)DAL_DataSerializer.Instance.BinaryDeserialize(acaLevelCode + "PaidItem.sf");
            for (int i = 0; i < paidItemsList.Count; i++)
            {
                if (checkCode(paidItemsList, id) != -1)
                    break;
            }
            int index = checkCode(paidItemsList, id);
            PaidItem item = (PaidItem)paidItemsList.ElementAt(index);
            item.Id = id;
            item.Name = name;
            item.Rate = rate;
            item.UnitValue = unitValue;
            item.TypeId = typeId;
            item.AcadLevelCode = acaLevelCode;

            DAL_DataSerializer.Instance.BinarySerialize(paidItemsList, acaLevelCode + "PaidItem.sf");
            return true;

        }

        public bool Delete(string id, string acaLv)
        {
            paidItemsList = (List<Object>)DAL_DataSerializer.Instance.BinaryDeserialize(acaLv + "PaidItem.sf");
            for (int i = 0; i < paidItemsList.Count; i++)
            {
                if (checkCode(paidItemsList, id) != -1)
                {
                    paidItemsList.RemoveAt(checkCode(paidItemsList, id));
                    DAL_DataSerializer.Instance.BinarySerialize(paidItemsList, acaLv + "PaidItem.sf");
                    return true;
                }
            }
            return false;

        }

        public PaidItem GetOnePaidItemByCode(string id)
        {
            PaidItem pItem = new PaidItem();
            foreach (Object obj in paidItemsList)
            {
                if (obj.GetType().Name.Equals("PaidItem"))
                {
                    PaidItem pI = (PaidItem)obj;
                    if (pI.Id.Equals(id))
                    {
                        pItem = pI;
                        return pItem;
                    }
                }
            }
            return pItem;
        }

        //serialize Now
        //public void serializeListImmediately(string acaLvCode, string creatorCode, DateTime createdDate, DateTime publishDate, DateTime activeDate)
        //{
        //    PaidItemHeader pih = new PaidItemHeader(creatorCode, createdDate, acaLvCode, publishDate,activeDate, "ABC/31T", "");
        //    paidItemsList.RemoveAt(0);
        //    paidItemsList.Insert(0, pih);
        //    DAL_DataSerializer.Instance.BinarySerialize(paidItemsList,  acaLvCode + "PaidItem.sf");
        //}
        
    }
}
