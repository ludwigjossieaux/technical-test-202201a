using People.Architecture.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace People.Architecture.Application.Contracts
{
    public interface IPersonRepository
    {
        Task<IReadOnlyList<Person>> ListAsync(string nomFilter, string prenomFilter);
        Task<Person> GetAsync(Guid id);
        Task<Guid> AddAsync(Person entity);
        Task UpdateAsync(Person entity);
        Task DeleteAsync(Person entity);
    }
}
