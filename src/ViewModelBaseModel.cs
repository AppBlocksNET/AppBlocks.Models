using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AppBlocks.Models
{
    /// <summary>
    /// ViewModelBaseModel
    /// </summary>
    public class ViewModelBaseModel : ExtendedBindableObject
    {
        private bool isRefreshing = false;
        public bool IsRefreshing
        {
            get { return isRefreshing; }
            set { SetProperty(ref isRefreshing, value); }
        }

        private string title = string.Empty;
        public string Title
        {
            get { return title; }
            set { SetProperty(ref title, value); }
        }

        #region INotifyPropertyChanged
        /// <summary>
        /// PropertyChanged
        /// </summary>
        public new event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// NotifyPropertyChanged
        /// </summary>
        /// <param name="propertyName"></param>
        protected void NotifyPropertyChanged([CallerMemberName] string propertyName = null)
        {
            Trace.WriteLine($"{this}.NotifyPropertyChanged({propertyName}):{System.DateTime.Now.ToShortTimeString()}");
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// SetProperty
        /// </summary>
        /// <typeparam name="TData"></typeparam>
        /// <param name="storage"></param>
        /// <param name="value"></param>
        /// <param name="propertyName"></param>
        protected void SetProperty<TData>(ref TData storage, TData value, [CallerMemberName] string propertyName = "")
        {
            Trace.WriteLine($"{this}.SetProperty({propertyName}):{value}");
            if (storage.Equals(value)) return;
            storage = value;
            RaisePropertyChanged(() => propertyName);
        }

        //protected bool SetProperty<T>(ref T backingStore, T value,
        //    [CallerMemberName] string propertyName = "",
        //    Action onChanged = null)
        //{
        //    if (EqualityComparer<T>.Default.Equals(backingStore, value))
        //        return false;

        //    backingStore = value;
        //    onChanged?.Invoke();
        //    NotifyPropertyChanged(propertyName);
        //    return true;
        //}
#endregion

        public ICommand AboutCommand { get; set; }
        public ICommand AppSettingsCommand { get; set; }
        public ICommand AppBlocksCommand { get; set; }
        public ICommand LoginCommand { get; set; }
        public ICommand LogoutCommand { get; set; }
        public ICommand ProfileCommand { get; set; }

        public ViewModelBaseModel()
        {
            //Initialize();
        }

        //public ViewModelBase(INavigationService NavigationService = null)
        //{
        //    Initialize(NavigationService);
        //}
        public virtual Task InitializeAsync(object navigationData)
        {
            return Task.FromResult(false);
        }
    
        //private void Initialize(INavigationService NavigationService = null)
        //{
        //    //if (Navigation == null) Navigation = Application.Current?.MainPage?.Navigation ?? Application.Current.NavigationProxy;
        //    if (NavigationService == null) NavigationService = ViewModelLocator.Resolve<INavigationService>();

        //    AppName = Application.Current.ToString();
        //    if (AppName.EndsWith(".App")) AppName = AppName.Substring(0, AppName.Length - 4);
        //    Trace.WriteLine($"AppName:{AppInfo.Name}");

        //    //Username = User.CurrentUser.Username;
        //    Trace.WriteLine($"Username:{AppBlocks.Current.CurrentUser.Username}");

        //    AboutCommand = Commands.AboutCommand;
        //    AppBlocksCommand = Commands.AppBlocksCommand;
        //    LoginCommand = Commands.LoginCommand;
        //    LogoutCommand = Commands.LogoutCommand;
        //    ProfileCommand = Commands.ProfileCommand;
        //    AppSettingsCommand = Commands.AppSettingsCommand;

        //    DialogService = ViewModelLocator.Resolve<IDialogService>();
        //    var settingsService = ViewModelLocator.Resolve<ISettingsService>();

        //    GlobalSetting.Instance.BaseIdentityEndpoint = settingsService.IdentityEndpointBase;
        //    GlobalSetting.Instance.BaseGatewayShoppingEndpoint = settingsService.GatewayShoppingEndpointBase;
        //    GlobalSetting.Instance.BaseGatewayMarketingEndpoint = settingsService.GatewayMarketingEndpointBase;

        //    if (string.IsNullOrEmpty(Title))
        //    {
        //        Title = ToString();
        //        var lastIndex = Title.LastIndexOf(".");
        //        if (lastIndex > -1) Title = Title.Remove(0, lastIndex + 1);
        //        if (Title.EndsWith("]") && !Title.StartsWith("["))
        //        {
        //            Title = Title.Substring(0, Title.Length - 1);
        //            if (Title != "Home") Title += "s";
        //        }
        //        if (Title.ToLower().EndsWith("viewmodel")) Title = Title.Substring(0, Title.Length - 9);
        //    }
        //}

        
    }
}