using Newtonsoft.Json;
using StoreSln_OOP;
using StoreSln_OOP.Entities;
using System.Runtime.CompilerServices;

namespace StoreSln_OOP.DAL
{
    public abstract class DALBase
    {
        protected string? dataFilePath;
        protected DALBase()
        {
        }

        public static bool ReadData(string dataFilePath, out List<IContainsID> resInstances)
        {
            bool isSuccess = false;
            resInstances = new List<IContainsID>();
            string jsonData = String.Empty;

            try
            {
                using (StreamReader sr = new StreamReader(dataFilePath))
                {
                    jsonData = sr.ReadLine();
                }
                resInstances = JsonConvert.DeserializeObject<List<IContainsID>>(jsonData);
                
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

        public static bool SaveData(string dataFilePath, List<IContainsID> saveInstances)
        {
            bool isSuccess = false;
            string jsonData = JsonConvert.SerializeObject(saveInstances);

            if (!Directory.Exists(Global.DATA_DIR))
            {
                Directory.CreateDirectory(Global.DATA_DIR);
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

        public static bool Add(string dataFilePath, IContainsID addInstance)
        {
            List<IContainsID> instances;
            
            if(!ReadData(dataFilePath, out instances))
            {
                return false;
            }

            int maxID = 0;
            if (instances.Any())
            {
                maxID = instances.Max(p => p.ID);
            }
            addInstance.ID = maxID + 1;
            instances.Add(addInstance);
            
            if(!SaveData(dataFilePath, instances))
            {
                return false;
            }

            return true;
        }

        public static bool Update(string dataFilePath, IContainsID updateInstance)
        {
            List<IContainsID> instances;

            if (!ReadData(dataFilePath, out instances))
            {
                return false;
            }

            var index = instances.FindIndex(c => c.ID == updateInstance.ID);
            
            if(index == -1)
            {
                return false;
            }

            instances[index] = updateInstance;

            if (!SaveData(dataFilePath, instances))
            {
                return false;
            }

            return true;
        }

        public static bool DeleteAtID(string dataFilePath, int id)
        {
            List<IContainsID> instances;

            if (!ReadData(dataFilePath, out instances))
            {
                return false;
            }

            var index = instances.FindIndex(c => c.ID == id);

            if (index == -1)
            {
                return false;
            }

            instances.RemoveAt(index);

            if (!SaveData(dataFilePath, instances))
            {
                return false;
            }

            return true;
        }
    }
}
