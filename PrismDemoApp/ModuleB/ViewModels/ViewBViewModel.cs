using Core;
using ModuleB.Views;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.ObjectModel;

namespace ModuleB.ViewModels;

public class ViewBViewModel : BindableBase
{
    
    IRegionManager _region;

    public ViewBViewModel(IEventAggregator eventAggregator, IRegionManager region)
    {
        ChangeViewCommand = new DelegateCommand(GetMethod, () => true);
        eventAggregator.GetEvent<MessageSentEvent>().Subscribe(message =>
        {
            Messages.Add($"{message} : {DateTime.Now}");  
        }, ThreadOption.PublisherThread, true, message => message != string.Empty);

        _region = region;
    }

    private void GetMethod()
    {
        _region.RequestNavigate("SenderRegion", new Uri(nameof(Home), UriKind.Relative));
        _region.RequestNavigate("RecieverRegion", new Uri(nameof(Home), UriKind.Relative));
    }

    public DelegateCommand ChangeViewCommand { get; private set; }

    private ObservableCollection<string> _messages = new();

    public ObservableCollection<string> Messages
    {
        get { return _messages; }
        set { SetProperty(ref _messages, value); }
    }
}
