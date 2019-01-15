using System;
using LogoFX.Client.Mvvm.Model;
using Samples.Client.Model.Contracts;

namespace Samples.Client.Model
{
    class AppModel : EditableModel<Guid>, IAppModel
    {
        public bool IsNew { get; set; }
    }
}
