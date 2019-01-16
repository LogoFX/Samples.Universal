using System.Windows.Input;
using Caliburn.Micro;
using LogoFX.Client.Mvvm.Commanding;

namespace Samples.Universal.Client.Presentation.Shell.ViewModels
{
    public sealed class WarehouseItemCommandsViewModel : PropertyChangedBase
    {
        private readonly IMainViewModel _mainViewModel;

        public WarehouseItemCommandsViewModel(IMainViewModel mainViewModel, ICommand applyCommand, ICommand cancelChangesCommand)
        {
            _mainViewModel = mainViewModel;
            ApplyCommand = applyCommand;
            CancelChangesCommand = cancelChangesCommand;
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

        public ICommand ApplyCommand { get; }

        public ICommand CancelChangesCommand { get; }
    }
}