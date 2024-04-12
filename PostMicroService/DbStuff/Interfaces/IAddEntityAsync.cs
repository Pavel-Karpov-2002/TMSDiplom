namespace PostMicroService.DbStuff.Interfaces
{
    public interface IAddEntityAsync<DbModel> where DbModel : IBaseModel
    {
        public Task AddAsync(DbModel entity);
    }
}
