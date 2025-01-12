using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using X.PagedList;
using Mango.Services.CouponAPI.Implementation.Contract;
using System.ComponentModel.DataAnnotations;

namespace Mango.Services.CouponAPI.Implementation.Repository
{
    public class GenericRepository<T, TContext> : IGenericRepository<T, TContext> where T : class where TContext : DbContext
    {
        private readonly TContext _DbContext;
        private readonly DbSet<T> _Db;

        public GenericRepository(TContext Context)
        {
            _DbContext = Context;
            _Db = _DbContext.Set<T>();
        }

        public async Task<IPagedList<T>> GetAllAsync(RequestParams requestParams = null, Expression<Func<T, bool>> expression = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderby = null, List<string> includes = null)
        {

            IQueryable<T> Query = _Db;

            if (expression != null)
            {
                Query = Query.Where(expression);
            }

            if (includes != null)
            {
                foreach (var include in includes)
                {
                    Query = Query.Include(include);
                }
            }
            if (orderby != null)
            {
                Query = orderby(Query);
            }

            int TotalCount = await Query.CountAsync();
            int PageNumber = requestParams?.PageNumber ?? 1;
            int PageSize = 50;
            //int PageSize = requestParams?.PageSize ?? requestParams.MaxPageSize;
            //PageSize = PageSize > 0 ? PageSize : requestParams.MaxPageSize;

            List<T> Items = await Query.Skip((PageNumber - 1) * PageSize)
                                   .Take(PageSize)
                                   .AsNoTracking()
                                   .ToListAsync();
            return new StaticPagedList<T>(Items, PageNumber, PageSize, TotalCount);

        }

        public Task<T> GetAsync(Expression<Func<T, bool>> expression = null, List<string> includes = null)
        {
            IQueryable<T> Query = _Db;
            if (includes != null)
            {
                foreach (var Item in includes)
                {
                    Query = Query.Include(Item);
                }
            }
            if (expression != null)
            {

                return Query.AsNoTracking().FirstOrDefaultAsync(expression);
            }
            return Query.AsNoTracking().FirstOrDefaultAsync();

        }

        public async Task<T> Insert(T Entity)
        {
            var Result = await _Db.AddAsync(Entity);
            await _DbContext.SaveChangesAsync();

            return Result.Entity;
        }

        public void UpdateAsync(T Entity)
        {
            _Db.Attach(Entity);
            _DbContext.Entry(Entity).State = EntityState.Modified;
        }
        public async Task DeleteAsync(int Id)
        {
            // Infer the primary key property dynamically



            // Query using the dynamically determined key property name
            T entity = await _Db.FirstOrDefaultAsync(e => EF.Property<int>(e, "CouponID") == Id);

            if (entity != null)
            {
                _Db.Remove(entity);
                await _DbContext.SaveChangesAsync();
            }
        }

    }
}
