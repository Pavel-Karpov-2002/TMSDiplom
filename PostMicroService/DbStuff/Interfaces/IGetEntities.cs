namespace PostMicroService.DbStuff.Interfaces
{
    public interface IGetEntities<DbModel> where DbModel : IBaseModel
    {
        public DbModel? GetById(int id);
        public IEnumerable<DbModel> GetAll();
    }
}
