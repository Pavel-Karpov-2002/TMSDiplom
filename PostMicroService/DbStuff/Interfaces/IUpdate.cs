namespace PostMicroService.DbStuff.Interfaces
{
    public interface IUpdate<DbModel>
    {
        public DbModel Update(DbModel entity);
    }
}
