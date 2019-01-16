using System;
using LogoFX.Client.Mvvm.Model;
using Samples.Client.Model.Contracts;

namespace Samples.Client.Model
{
    class AppModel : EditableModel<int>, IAppModel
    {
        public bool IsNew { get; set; }
    }
}
