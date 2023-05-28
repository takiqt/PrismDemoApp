using Core;
using ModuleA.Views;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;
using System;

namespace ModuleA.ViewModels;

public class ViewAViewModel : BindableBase
{

    readonly IEventAggregator _eventAggregator;
    readonly IRegionManager _regionManager;

    public ViewAViewModel(IEventAggregator eventAggregator, IRegionManager regionManager)
    {
        _eventAggregator = eventAggregator;
        _regionManager = regionManager;
        IsEnabled = true;
        SendCommand = new DelegateCommand(SendActionHandler, () => IsEnabled).ObservesProperty(() => IsEnabled);
        _eventAggregator.GetEvent<ResetModuleAViewA>().Subscribe(ResetViewA);
    }

    private void ResetViewA()
    {
        _regionManager.RequestNavigate("SenderRegion", new Uri(nameof(ViewA), UriKind.Relative));
    }

    public DelegateCommand SendCommand { get; private set; }

    public string Message 
    {
        get { return _message; }
        set { SetProperty(ref _message, value); }
    }
    private string _message = string.Empty;

    public bool IsEnabled
    {
        get { return _isEnabled; }
        set { SetProperty(ref _isEnabled, value); }
    }
    private bool _isEnabled;
  
    private void SendActionHandler()
    {
        _eventAggregator.GetEvent<MessageSentEvent>().Publish(Message);
        Message = string.Empty;
    }
}
