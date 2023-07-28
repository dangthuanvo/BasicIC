using Repository.EF;
using Repository.Interfaces;

namespace Repository.Repositories
{
    public class BasicICRepository<T> : BaseRepositorySql<T>, IRepositorySql<T> where T : class
    {
        public BasicICRepository() : base(new M03_BasicEntities())
        {
        }
    }
}
