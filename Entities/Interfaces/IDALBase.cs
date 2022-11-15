namespace StoreSln_OOP.Entities
{
    public interface IDALBase
    {
        IDALBase GetDALFunction();
        bool ReadData(out List<IContainsID> resInstances);
        bool SaveData(List<IContainsID> saveInstances);
        bool Add(IContainsID addInstance);
        bool Update(IContainsID updateInstance);
        bool DeleteAtID(int id);
    }
}
