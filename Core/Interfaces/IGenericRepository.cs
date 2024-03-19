using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.Specifications;

namespace Core.Interfaces
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        // jpa the benefit of using generics is that you can use this interface to
        //  return a list or specific record for 100s of possible entities.
        Task<T> GetByIdAsync(int id);  // jpa return any type by id
        Task<IReadOnlyList<T>> ListAllAsync(); //jpa return a list of any type

        Task<T> GetEntityWithSpec(ISpecification<T> spec);
        Task<IReadOnlyList<T>> ListAsync(ISpecification<T> spec);
    }
}