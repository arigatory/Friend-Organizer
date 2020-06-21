using FriendOrganizer.Model;
using FriendOrganizer.UI.Data;
using FriendOrganizer.UI.Event;
using Prism.Events;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace FriendOrganizer.UI.ViewModel
{
    public class MainViewModel : ViewModelBase
    {

        private IFriendDetailViewModel _friendDetailViewModel;


        public MainViewModel(INavigationViewModel navigationViewModel, Func<IFriendDetailViewModel> friendDetailViewModelCreator, IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            NavigationViewModel = navigationViewModel;
            _friendDetailViewModelCreator = friendDetailViewModelCreator;

            _eventAggregator.GetEvent<OpenFriendDetailViewEvent>().Subscribe(OnOpenFriendDetailView);
        }



        public async Task LoadAsync()
        {
            await NavigationViewModel.LoadAsync();
        }

        private IEventAggregator _eventAggregator;

        public INavigationViewModel NavigationViewModel { get; }

        private Func<IFriendDetailViewModel> _friendDetailViewModelCreator;


        public IFriendDetailViewModel FriendDetailViewModel
        {
            get { return _friendDetailViewModel; }
            private set 
            { 
                _friendDetailViewModel = value;
                OnPropertyChanged();
            }
        }


        private async void OnOpenFriendDetailView(int friendId)
        {
            FriendDetailViewModel = _friendDetailViewModelCreator();
            await FriendDetailViewModel.LoadAsync(friendId);
        }
    }
}
