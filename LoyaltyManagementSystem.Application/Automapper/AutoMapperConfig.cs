using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoyaltyManagementSystem.Application.Automapper
{
    public class AutoMapperConfig
    {
        public static MapperConfiguration RegisterMappings()
        {
            return new MapperConfiguration(config =>
                {
                    config.AddProfile(new DomainToViewModelMappingProfile());
                    config.AddProfile(new ViewModelToDomainMappingProfile());
                } 
            );
        }
    }
}
