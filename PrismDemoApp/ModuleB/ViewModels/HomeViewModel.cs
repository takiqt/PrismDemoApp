using Core;
using ModuleB.Views;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;
using System;

namespace ModuleB.ViewModels;

public class HomeViewModel : BindableBase
{
    public HomeViewModel(IRegionManager regionManager, IEventAggregator eventAggregator) 
    { 
        _eventAggregator = eventAggregator;
        _regionManager = regionManager;
        ResetCommand = new DelegateCommand(ChangeViews, () => true);
    }

    public DelegateCommand ResetCommand { get; private set; }

    readonly IRegionManager _regionManager;
    readonly IEventAggregator _eventAggregator;

    private void ChangeViews()
    {
        _eventAggregator.GetEvent<ResetModuleAViewA>().Publish();
        _regionManager.RequestNavigate("RecieverRegion", new Uri(nameof(ViewB), UriKind.Relative));
    }
}
