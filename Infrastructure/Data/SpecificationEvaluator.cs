
using Core.Entities;
using Core.Specifications;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{

    // jpa we are using TEntity instead of T just to make clear that the placeholder 
    // should be an entity.

    public class SpecificationEvaluator<TEntity> where TEntity : BaseEntity
    {
        // jpa this method is static so no need to instantate the class in order to use it.
        public static IQueryable<TEntity> GetQuery(IQueryable<TEntity> inputQuery,
                       ISpecification<TEntity> spec)
        {
            var query = inputQuery;

            if (spec.Criteria != null)
            {
                query = query.Where(spec.Criteria); // p => p.productTypeId == id
            }

            if (spec.OrderBy != null)
            {
                query = query.OrderBy(spec.OrderBy);
            }

            if (spec.OrderByDescending != null)
            {
                query = query.OrderByDescending(spec.OrderByDescending);
            }

            // paging operators need to come after filter and sorting operators - order
            // here is important
            if (spec.IsPagingEnabled)
            {
                query = query.Skip(spec.Skip).Take(spec.Take);
            }

            query = spec.Includes.Aggregate(query, (current, include) => current.Include(include));

            return query;

        }
    }
}