using Newtonsoft.Json;
using StoreSln_OOP.Entities;
using System.Runtime.CompilerServices;

namespace StoreSln_OOP.DAL
{
    public class DALBase<T> where T : IContainsID
    {
        string? dataFilePath;
        private DALBase() { }
        public DALBase(string saveFileName)
        {
            dataFilePath = $@"{Global.DATA_DIR}/{saveFileName}";
        }

        public bool ReadData(out List<T> resInstances)
        {
            bool isSuccess = false;
            resInstances = new List<T>();
            string jsonData = String.Empty;
 
            if (dataFilePath == string.Empty)
            {
                return false;
            }

            try
            {
                using (StreamReader sr = new StreamReader(dataFilePath))
                {
                    jsonData = sr.ReadLine();
                }
                resInstances = JsonConvert.DeserializeObject<List<T>>(jsonData);
                
                if(resInstances != null)
                {
                    isSuccess = true;
                }
                else
                {
                    isSuccess = false;
                }
                
            }
            catch(Exception ex)
            {
                isSuccess = false;
                Console.WriteLine(ex.Message);
            }
            return isSuccess;
        }

        public bool SaveData(List<T> saveInstances)
        {
            bool isSuccess = false;
            string jsonData = JsonConvert.SerializeObject(saveInstances);

            if (!Directory.Exists(Global.DATA_DIR))
            {
                Directory.CreateDirectory(Global.DATA_DIR);
            }

            if (dataFilePath == string.Empty)
            {
                return false;
            }

            try
            {
                using (StreamWriter sw = new StreamWriter(dataFilePath, false))
                {
                    sw.WriteLine(jsonData);
                }
            }
            catch (Exception ex)
            {
                isSuccess = false;
                Console.WriteLine(ex.Message);
            }
            
            return isSuccess;
        }

        public bool Add(T addInstance)
        {
            List<T> instances = new List<T>();

            ReadData(out instances);

            int maxID = 0;
            if (instances.Any())
            {
                maxID = instances.Max(p => p.ID);
            }
            addInstance.ID = maxID + 1;
            instances.Add(addInstance);
            
            if(!SaveData(instances))
            {
                return false;
            }

            return true;
        }

        public bool Update(T updateInstance)
        {
            List<T> instances;

            ReadData(out instances);

            var index = instances.FindIndex(c => c.ID == updateInstance.ID);
            
            if(index == -1)
            {
                return false;
            }

            instances[index] = updateInstance;

            if (!SaveData(instances))
            {
                return false;
            }

            return true;
        }

        public bool RemoveAtID(int id)
        {
            List<T> instances = new List<T>();

            ReadData(out instances);

            var index = instances.FindIndex(c => c.ID == id);

            if (index == -1)
            {
                return false;
            }

            instances.RemoveAt(index);

            if (!SaveData(instances))
            {
                return false;
            }

            return true;
        }
    }
}
