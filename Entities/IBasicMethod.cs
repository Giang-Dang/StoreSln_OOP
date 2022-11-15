namespace StoreSln_OOP.Entities
{
    interface IBasicMethod
    {
        bool Add();
        bool Read();
        bool Update(int id, IBasicMethod modifiedObject);
        bool Delete(int id);
    }
}
