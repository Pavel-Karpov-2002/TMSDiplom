namespace PostMicroService.DbStuff.Interfaces
{
    public interface IDeleteEntity<DbModel> where DbModel : IBaseModel
    {
        public void DeleteById(int id);
        public void DeleteByEntity(DbModel entity);
    }
}
