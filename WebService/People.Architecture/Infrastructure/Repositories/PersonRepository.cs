using Microsoft.EntityFrameworkCore;
using People.Architecture.Application.Contracts;
using People.Architecture.Domain.Entities;
using People.Architecture.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace People.Architecture.Infrastructure.Repositories
{
    [ExcludeFromCodeCoverage]
    public class PersonRepository : IPersonRepository
    {
        private readonly PeopleDbContext _context;

        public PersonRepository(PeopleDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<Guid> AddAsync(Person entity)
        {
            _context.People.Add(entity);
            await _context.SaveChangesAsync();
            return entity.Id;
        }

        public async Task DeleteAsync(Person entity)
        {
            _context.People.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<Person> GetAsync(Guid id)
        {
            return await _context.People.FindAsync(id);
        }

        public async Task<IReadOnlyList<Person>> ListAsync(string nomFilter = "", string prenomFilter = "")
        {
            var query = _context.People.AsQueryable();
            if(nomFilter != "")
            {
                query = query.Where(x => EF.Functions.Like(x.Nom, $"%{nomFilter}%"));
            }
            if (nomFilter != "")
            {
                query = query.Where(x => EF.Functions.Like(x.Prenom, $"%{prenomFilter}%"));
            }
            return await query.ToListAsync();
        }

        public async Task UpdateAsync(Person entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
