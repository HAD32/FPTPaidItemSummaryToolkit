using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DAL_DataSerializer
    {
        private static DAL_DataSerializer instance;

        public static DAL_DataSerializer Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new DAL_DataSerializer();
                }
                return instance;
            }
            set { instance = value; }
        }

        public bool BinarySerialize(object data, string filePath)
        {
            try
            {
                FileStream fileStream;
                BinaryFormatter binaryFormatter = new BinaryFormatter();
                if (File.Exists(filePath))
                    File.Delete(filePath);
                fileStream = File.Create(filePath);
                binaryFormatter.Serialize(fileStream, data);
                fileStream.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return true;
        }

        public object BinaryDeserialize(string filePath)
        {
            object obj = null;
            FileStream fileStream;
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            if (File.Exists(filePath))
            {
                fileStream = File.OpenRead(filePath);
                obj = binaryFormatter.Deserialize(fileStream);
                fileStream.Close();
            }

            return obj;
        }
    }
}
