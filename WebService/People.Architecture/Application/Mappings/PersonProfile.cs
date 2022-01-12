using AutoMapper;
using People.Architecture.Application.Features.People.Commands.AddPerson;
using People.Architecture.Application.Models;
using People.Architecture.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace People.Architecture.Application.Mappings
{
    [ExcludeFromCodeCoverage]
    public class PersonProfile : Profile
    {
        public PersonProfile()
        {
            CreateMap<AddPersonCommand, Person>();
            CreateMap<Person, PersonVm>();
        }
    }
}
