using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Core.Specifications
{
    // jpa - All this is set-up to replace the include statements previously used
    // in the ProductRepository.cs
    public class BaseSpecification<T> : ISpecification<T>
    {
        public BaseSpecification()
        {
            // contructor 1 no parameters
            // used to get include clauses for required navigation data.
        }
        public BaseSpecification(Expression<Func<T, bool>> criteria)
        {
            // costructor 2 with params
            // used to get specific records that meet specified criterea
            Criteria = criteria;
        }
        public Expression<Func<T, bool>> Criteria { get; }

        public List<Expression<Func<T, object>>> Includes { get; }
              = new List<Expression<Func<T, object>>>();

        protected void AddInclude(Expression<Func<T, object>> includeExpression)
        {
            Includes.Add(includeExpression);
        }
    }
}