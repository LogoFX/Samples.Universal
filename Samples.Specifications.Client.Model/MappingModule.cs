using System.Composition;
using AutoMapper;
using LogoFX.Samples.Specifications.Client.Model.Mappers;
using Solid.Practices.Modularity;

namespace LogoFX.Samples.Specifications.Client.Model
{
    [Export(typeof(ICompositionModule))]
    class MappingModule : IPlainCompositionModule
    {
        public void RegisterModule()
        {
            Mapper.Initialize(x => x.AddProfile<MappingProfile>());
        }
    }
}