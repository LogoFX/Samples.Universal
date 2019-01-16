using System;
using LogoFX.Client.Mvvm.Model.Contracts;

namespace Samples.Client.Model.Contracts
{
    public interface IAppModel : IModel<int>, IEditableModel
    {
        bool IsNew { get; set; }
    }
}
