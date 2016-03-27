using AutoMapper;
using JetBrains.Annotations;
using Samples.Universal.Client.Model.Mappers;
using Solid.Practices.Modularity;

namespace Samples.Universal.Client.Model
{   
    [UsedImplicitly]
    class MappingModule : IPlainCompositionModule
    {
        public void RegisterModule()
        {
            Mapper.Initialize(x => x.AddProfile<MappingProfile>());
        }
    }
}