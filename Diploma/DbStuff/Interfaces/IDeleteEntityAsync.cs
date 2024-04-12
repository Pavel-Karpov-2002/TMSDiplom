namespace Diploma.DbStuff.Interfaces
{
    public interface IDeleteEntityAsync<DbModel> where DbModel : IBaseModel
    {
        public Task DeleteByIdAsync(int id);
        public Task DeleteByEntityAsync(DbModel entity);
    }
}
