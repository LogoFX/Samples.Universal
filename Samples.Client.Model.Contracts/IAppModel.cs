using System;
using LogoFX.Client.Mvvm.Model.Contracts;

namespace Samples.Client.Model.Contracts
{
    public interface IAppModel : IModel<Guid>, IEditableModel
    {
        bool IsNew { get; set; }
    }
}
