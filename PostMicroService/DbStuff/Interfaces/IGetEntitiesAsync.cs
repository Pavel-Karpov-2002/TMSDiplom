namespace PostMicroService.DbStuff.Interfaces
{
    public interface IGetEntitiesAsync<DbModel>
    {
        public Task<DbModel?>? GetByIdAsync(int id);
        public Task<List<DbModel>> GetAllAsync();
    }
}
