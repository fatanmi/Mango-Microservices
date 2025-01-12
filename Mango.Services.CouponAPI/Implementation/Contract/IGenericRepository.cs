using System.Linq.Expressions;
using X.PagedList;
using Mango.Services.CouponAPI.Implementation.Repository;
using Microsoft.EntityFrameworkCore;
namespace Mango.Services.CouponAPI.Implementation.Contract
{
    public interface IGenericRepository<T, TContext> where T : class where TContext : DbContext
    {
        Task<T> GetAsync(Expression<Func<T, bool>> expression = null, List<string> includes = null);
        Task<IPagedList<T>> GetAllAsync(RequestParams requestParams = null, Expression<Func<T, bool>> expression = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderby = null, List<string> includes = null);

        Task DeleteAsync(int Id);
        void UpdateAsync(T Entity);
        Task<T> Insert(T Entity);

    }
}
