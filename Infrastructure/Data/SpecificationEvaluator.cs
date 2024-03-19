using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.Specifications;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure.Internal;


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

            query = spec.Includes.Aggregate(query, (current, include) => current.Include(include));

            return query;

        }
    }
}