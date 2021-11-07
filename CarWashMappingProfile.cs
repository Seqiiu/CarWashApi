using AutoMapper;
using CarWashApi.Models;
using DataBase.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarWashApi
{
    public class CarWashMappingProfile : Profile
    {
        public CarWashMappingProfile()
        {
            CreateMap<CarWash, CarWashDto>()
                .ForMember(m => m.City, c => c.MapFrom(s => s.Address.City))
                .ForMember(m => m.Street, c => c.MapFrom(s => s.Address.Street))
                .ForMember(m => m.PostalCode, c => c.MapFrom(s => s.Address.PostalCode));

            CreateMap<Service, ServicesDto>();

            CreateMap<Order, OrderDto>();

            CreateMap<Employer, EmployerDto>()
                .ForMember(m => m.NamePosition, c => c.MapFrom(s => s.Position.Name));
            
            CreateMap<Client, ClientDto>();
        }
    }
}
