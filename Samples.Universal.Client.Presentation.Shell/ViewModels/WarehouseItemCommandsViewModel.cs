using System.Windows.Input;
using Caliburn.Micro;
using LogoFX.Client.Mvvm.Commanding;

namespace Samples.Universal.Client.Presentation.Shell.ViewModels
{
    public sealed class WarehouseItemCommandsViewModel : PropertyChangedBase
    {
        private readonly IMainViewModel _mainViewModel;

        public WarehouseItemCommandsViewModel(IMainViewModel mainViewModel, ICommand applyCommand)
        {
            _mainViewModel = mainViewModel;
            _applyCommand = applyCommand;
        }

        private IActionCommand _newCommand;
        public ICommand NewCommand =>
            CommandFactory.GetCommand(ref _newCommand, _mainViewModel.NewWarehouseItem);

        private IActionCommand _deleteCommand;
        public ICommand DeleteCommand
            => CommandFactory
                .GetCommand(ref _deleteCommand, _mainViewModel.DeleteSelectedItem,
                    () => _mainViewModel.ActiveWarehouseItem?.WarehouseItem.Model.IsNew == false)
                .RequeryOnPropertyChanged(_mainViewModel, () => _mainViewModel.ActiveWarehouseItem);

        private readonly ICommand _applyCommand;
        public ICommand ApplyCommand => _applyCommand;
    }
}