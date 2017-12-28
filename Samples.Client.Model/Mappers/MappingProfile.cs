using System;
using AutoMapper;
using Samples.Client.Data.Contracts.Dto;
using Samples.Client.Model.Contracts;

namespace Samples.Client.Model.Mappers
{
    class MappingProfile
    {       
        internal static void CreateWarehouseMaps(IMapperConfigurationExpression cfg)
        {
            CreateDomainObjectMap<WarehouseItemDto, IWarehouseItem, WarehouseItem>(cfg);
        }

        //TODO: put this piece of functionality into 
        //an external package, e.g. Model.Mapping.AutoMapper
        private static void CreateDomainObjectMap<TDto, TContract, TModel>(IMapperConfigurationExpression cfg)
                    where TModel : TContract
                    where TContract : class
        {
            CreateDomainObjectMap(cfg, typeof (TDto), typeof (TContract), typeof (TModel));
        }


        private static void CreateDomainObjectMap(IMapperConfigurationExpression cfg, Type dtoType, Type contractType, Type modelType)
        {
            cfg.CreateMap(dtoType, contractType).As(modelType);
            cfg.CreateMap(dtoType, modelType);
            cfg.CreateMap(contractType, dtoType);
            cfg.CreateMap(modelType, dtoType);
        }
    }
}
