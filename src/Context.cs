using AppBlocks.Models.Commands;
using AppBlocks.Models.Extensions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AppBlocks.Models
{
    public static class Context
    {
        public static Dictionary<string, ICommand> Commands = new Dictionary<string, ICommand>();

        private static Assembly entryAssembly;
        public static Assembly EntryAssembly => entryAssembly ?? (entryAssembly = Assembly.GetEntryAssembly());

        public static string AppName { get; internal set; }

        private static string assemblyName;
        private static ICommand loadAppsCommand;

        //public static string AssemblyName => assemblyName ?? (assemblyName = EntryAssembly.GetName().Name);

        public static string AssemblyName
        {
            get
            {
                if (!string.IsNullOrEmpty(assemblyName)) return assemblyName;

                var results = EntryAssembly.GetName().Name;
                if (results.ToLower().EndsWith(".uwp")) results = results.Substring(0, results.Length - 4);
                assemblyName = results;

                return assemblyName;
            }
        }

        private static string id;
        public static string Id => id ?? "5E11C4A9-D602-EB11-A38D-BC9A78563412";//(id = !AssemblyName.ToLower().EndsWith(".uwp") ? AssemblyName : AssemblyName.Substring(0, AssemblyName.Length - 4));
        //public static string Id
        //{
        //    get
        //    {
        //        if (!string.IsNullOrEmpty(id)) return id;
        //        //var assembly = typeof(LoginViewModel).Assembly;
        //        //var attributes = assembly.GetCustomAttributes(typeof(GuidAttribute), true);
        //        //var attribute = (GuidAttribute)attributes?[0];
        //        //var id  = attribute.Value;
        //        //return id;
        //        //return Assembly.GetExecutingAssembly().FullName;

        //        //TODO: customattribute on the assembly with ProjectGuid?
        //        //var id = Assembly.GetEntryAssembly().GetName().Name;
        //        //var id = Name;
        //        id = AssemblyName;
        //        if (id.ToLower().EndsWith(".uwp")) id = id.Substring(0, id.Length - 4);
        //        //Trace.WriteLine($"********** {Name}-REFLECTION - to get id:{Id}");
        //        return id;
        //    }
        //}

        public static bool IsAuthenticated { get; set; }
        //public static INavigationService NavigationService { get; set; }
        //internal static ISerializerService SerializerService { get; set; }
        //private static ISettingsService SettingsService;

        //public static List<Library> Library { get; set; }

        public static Item Group { get; set; }
        public static ObservableCollection<Item> BodyList => Group?.Children?.GetChild("Home")?.Children?.Where(i => !i.Name.StartsWith("_")).ToObservableCollection();

        public static ObservableCollection<Item> MainMenuList => (Group?.Children ?? new List<Item> { new Item { Name = "Home", Title = "Home" } })?.Where(i => !i.Name.StartsWith("_")).ToObservableCollection();
        public static ObservableCollection<Item> UserMenuList => Group?.Children?.Where(i => !i.Name.StartsWith("_")).ToObservableCollection();
        /// <summary>
        /// NewsFeed - TODO:(Group.Children?.FirstOrDefault(i => i.Name == "Feed") ?? .. _/Feeds/collection
        /// </summary>
        public static ObservableCollection<Item> NewsFeed => (Group?.Children?.FirstOrDefault(i => i.Name == "News")?.Children ?? new List<Item> { new Item { Name = "Default", Title = "Welcome!" } })?.Where(i => !i.Name.StartsWith("_")).ToObservableCollection();

        public static event CurrentUserChanged OnCurrentUserChanged;

        public delegate void CurrentUserChanged(object sender, CurrentUserChangedEventArgs e);

        public static string Name => Assembly.GetCallingAssembly().FullName;

        public static string Version => Assembly.GetCallingAssembly().GetName().Version.ToString();

        //public static INavigation Navigation => Application.Current?.MainPage?.Navigation ?? Application.Current?.NavigationProxy;

        //public static void OnUnhandledException(object sender, ExceptionEventArgs e)
        //{
        //    Trace.WriteLine($"{AssemblyName}.OnUnhandledException:{e.Exception}");
        //}

        public static async Task<ObservableCollection<T>> GetItems<T>() where T : Item
        {
            return await GetItems<T>(string.Empty);
        }

        public static async Task<ObservableCollection<T>> GetItems<T>(string id) where T : Item
        {
            //Trace.WriteLine($"AppBlocks.Core.Views.MainViewModel.GetItems<{typeof(T)}>({id})");

            //var dataStore = new SqlData<T>(); // DependencyService.Get<RestData<T>>();

            //if (dataStore != null)
            //{
            //    //return (await dataStore.GetAsync(new Item().Source = source)).ToObservableCollection();
            //    //return (await dataStore.GetAsync<T>(new List<BaseItem>
            //    //{
            //    //    new BaseItem { Id = id }
            //    //})).ToObservableCollection();
            //    //return await dataStore.GetAsync<ObservableCollection<T>>(new List<BaseItem>
            //    //{
            //    //    new BaseItem { Id = id }
            //    //});
            //    return (await dataStore.GetAsync<T>(new List<Item>
            //    {
            //        new Item { Id = id }
            //    })).ToObservableCollection();
            //}
            return default;
        }

        public static void Init()
        {
            if (string.IsNullOrEmpty(AppName)) AppName = AssemblyName;
            //Trace.WriteLine($"{AssemblyName}.InitApp({App.Id})");
            //RegisterServices();

            //if (NavigationService == null)
            //{
            //    NavigationService = Services.DependencyService.Resolve<NavigationService>() ?? ViewModelLocator.Resolve<INavigationService>();
            //}
            //SettingsService = Services.DependencyService.Resolve<ISettingsService>() ?? ViewModelLocator.Resolve<ISettingsService>();
            //if (!SettingsService.MocksEnabled)
            //{
            //    ViewModelLocator.UpdateDependencies(SettingsService.MocksEnabled);
            //}
            //if (SerializerService == null)
            //{
            //    SerializerService = Xamarin.Forms.DependencyService.Resolve<ISerializerService>() ?? Services.DependencyService.Resolve<ISerializerService>(); // ViewModelLocator.Resolve<ISerializerService>();
            //    //SerializerService = Services.DependencyService.Resolve<Utf8JsonSerializerService>();// ?? ViewModelLocator.Resolve<ISerializerService>();
            //}
            //todo:UNCOMMENT
            //AppBlocks.Core.Current.OnCurrentUserChanged += OnCurrentUserChanged;

            //Core.Init.Analytics.Init();

            //if (Xamarin.Forms.Device.RuntimePlatform == Xamarin.Forms.Device.UWP)
            //{
            //    //InitNavigation();
            //}

            //SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = AppViewBackButtonVisibility.Visible;
            //SystemNavigationManager.GetForCurrentView().BackRequested += (s, a) =>
            //{
            //    Debug.WriteLine("BackRequested");
            //    if (Frame.CanGoBack)
            //    {
            //        Frame.GoBack();
            //        a.Handled = true;
            //    }
            //}

            //Rg.Plugins.Popup.Popup.Init();

            //Group = GetAsync<Group>().Result;

            //if (Library == null) Library = new List<Library>();

            //Commands.Add("AboutCommand", new Commands.AboutCommand());
            //Commands.Add("LoadAppsCommand", new Commands.LoadAppsCommand());
            //if (loadAppsCommand == null) loadAppsCommand = Services.DependencyService.Resolve<LoadAppsCommand>() ?? ViewModelLocator.Resolve<LoadAppsCommand>() ?? new LoadAppsCommand();
            if (loadAppsCommand == null) loadAppsCommand = Commands.ContainsKey("LoadAppsCommand") ? Commands["LoadAppsCommand"] : new LoadAppsCommand();
            //Commands.LoadAppsCommand.Execute(typeof(App));
            loadAppsCommand.Execute(typeof(Context));

            //SetMainPage();

            //Trace.WriteLine($"{AssemblyName}.InitApp().completed");
        }

        //public static Task InitNavigation()
        //{
        //    //var navigationService = ViewModelLocator.Resolve<INavigationService>();
        //    return NavigationService.InitializeAsync();
        //}

        public static void RegisterServices(Type[] types)
        {
            foreach(var t in types)
            {
                //Xamarin.Forms.DependencyService.Register<typeof(t)>();
                //Xamarin.Forms.DependencyService.Register<t>();
            }
            ////Trace.WriteLine($"{AssemblyName}.RegisterServices()");

            ////if (UseMockDataStore)
            ////    DependencyService.Register<MockDataStore>();
            ////else
            ////    DependencyService.Register<AzureDataStore>();

            //Xamarin.Forms.DependencyService.Register<NavigationService>();

            //Xamarin.Forms.DependencyService.Register<SettingsService>();

            //Xamarin.Forms.DependencyService.Register<SystemTextJsonSerializerService>();
            ////Xamarin.Forms.DependencyService.Register<Utf8JsonSerializerService>();
            ////Utf8JsonSerializerService, 
            ////DependencyService.Register<IDataStore<Member>();

            ////DependencyService.Register<IDataStore<Member>>();
            ////DependencyService.Register<IDataStore<Product>>();
            ////Xamarin.Forms.DependencyService.Register<RestData<Event>>();
            ////Xamarin.Forms.DependencyService.Register<RestData<Home>>();
            //Xamarin.Forms.DependencyService.Register<RestData<Item>>();
            ////Xamarin.Forms.DependencyService.Register<RestData<Group>>();
            ////Xamarin.Forms.DependencyService.Register<RestData<Library>>();
            ////Xamarin.Forms.DependencyService.Register<RestData<Member>>();
            ////Xamarin.Forms.DependencyService.Register<RestData<MenuBody>>();
            ////Xamarin.Forms.DependencyService.Register<RestData<Message>>();
            ////Xamarin.Forms.DependencyService.Register<RestData<News>>();
            ////Xamarin.Forms.DependencyService.Register<RestData<Note>>();
            ////Xamarin.Forms.DependencyService.Register<RestData<Officer>>();
            ////Xamarin.Forms.DependencyService.Register<RestData<Post>>();
            ////Xamarin.Forms.DependencyService.Register<RestData<Product>>();
            ////Xamarin.Forms.DependencyService.Register<RestData<Sponsor>>();
            ////Xamarin.Forms.DependencyService.Register<RestData<Testimonial>>();

            var commands = new string[] { "About", "Back", "Authenticate", "LoadApps", "Login", "Register" };
            foreach (var command in commands)
            {
                var commandType = Type.GetType($"AppBlocks.Core.Commands.{command}Command");
                if (commandType != null)
                {
                    var key = $"{command}Command";
                    if (!Commands.ContainsKey(key))
                    {
                        var instance = Activator.CreateInstance(commandType);
                        if (instance != null)
                        {
                            Commands.Add(key, (ICommand)instance);
                        }
                    }
                }
            }

            //Xamarin.Forms.DependencyService.Register<RegisterCommand>();
            //Xamarin.Forms.DependencyService.Register<LoginCommand>();
            //Xamarin.Forms.DependencyService.Register<>
            //var pages = { };
            //foreach(var page in pages)
            //{
            //    DependencyService.Register<RestData<typeof(page)>>();
            //}

            //DependencyService.Register<AtomDataStore>();
            //DependencyService.Register<FacebookDataStore>();

            //if (Library?.Count == 0)
            //{
            //    Library = (List<Library>)(GetItems<Library>().Result).ToList<Library>();
            //}
        }

        //public static void RegisterServices()
        //{
        //    DependencyService.Add
        //    //if (UseMockDataStore)
        //    //    DependencyService.Register<MockDataStore>();
        //    //else
        //    //    DependencyService.Register<AzureDataStore>();

        //    //DependencyService.Register<NavigationService>();

        //    //DependencyService.Register<SystemTextJsonSerializerService>();
        //    DependencyService.Register<Utf8JsonSerializerService>();

        //    //DependencyService.Register<IDataStore<Member>();

        //    //DependencyService.Register<IDataStore<Member>>();
        //    //DependencyService.Register<IDataStore<Product>>();
        //    DependencyService.Register<RestData<Event>>();
        //    DependencyService.Register<RestData<Home>>();
        //    DependencyService.Register<RestData<BaseItem>>();
        //    DependencyService.Register<RestData<Group>>();
        //    DependencyService.Register<RestData<Library>>();
        //    DependencyService.Register<RestData<Member>>();
        //    DependencyService.Register<RestData<MenuBody>>();
        //    DependencyService.Register<RestData<Message>>();
        //    DependencyService.Register<RestData<News>>();
        //    DependencyService.Register<RestData<Note>>();
        //    DependencyService.Register<RestData<Officer>>();
        //    DependencyService.Register<RestData<Post>>();
        //    DependencyService.Register<RestData<Product>>();
        //    DependencyService.Register<RestData<Sponsor>>();
        //    DependencyService.Register<RestData<Testimonial>>();
        //    //var pages = { };
        //    //foreach(var page in pages)
        //    //{
        //    //    DependencyService.Register<RestData<typeof(page)>>();
        //    //}

        //    //DependencyService.Register<AtomDataStore>();
        //    //DependencyService.Register<FacebookDataStore>();

        //    //if (Library?.Count == 0)
        //    //{
        //    //    Library = (List<Library>)(GetItems<Library>().Result).ToList<Library>();
        //    //}
        //}

        //private static async Task<ObservableCollection<T>> GetItems<T>(string id = null) where T : BaseItem
        //{
        //    Trace.WriteLine($"AppBlocks.Core.Views.MainViewModel.GetItems<{typeof(T)}>({id})");

        //    var dataStore = RestData<T>();

        //    if (dataStore != null)
        //    {
        //        //return (await dataStore.GetAsync(new Item().Source = source)).ToObservableCollection();
        //        //return (await dataStore.GetAsync<T>(new List<BaseItem>
        //        //{
        //        //    new BaseItem { Id = id }
        //        //})).ToObservableCollection();
        //        //return await dataStore.GetAsync<ObservableCollection<T>>(new List<BaseItem>
        //        //{
        //        //    new BaseItem { Id = id }
        //        //});
        //        return (await dataStore.GetAsync<T>(new List<BaseItem>
        //        {
        //            new BaseItem { Id = id }
        //        })).ToObservableCollection();
        //    }
        //    return default;
        //}

        //private static string[] pages = new string[] { "Home", "Library", "Empty", "Posts", "News", "Calendar", "Events", "Members", "Jeeps", "Messages", "Notes", "Products", "Shop", "ShopProducts", "Browse", "Officers", "Sponsors", "Testimonials", "AppSettings", "AppBlocks", "Features", "About", "Profile" };

        //public static void SetPages()
        //{

        //}

        //public static void MainPageTabs(TabbedPage tabs)
        //{
        //    //var pages = new string[] { "Home", "Posts", "News", "Calendar", "Events", "Members", "Jeeps", "Messages", "Notes", "Shop", "ShopProducts", "Browse", "Officers", "Sponsors", "Testimonials", "AppBlocks", "About", "Profile"};
        //     //var pages = new string[] { "Home", "Posts", "Calendar", "Members", "Jeeps", "Shop", "Officers", "Sponsors"};

        //    foreach (var p in pages)
        //    {
        //        var tabPage = GetPage(p);
        //        if (tabPage == null) continue;

        //        NavigationPage page = new NavigationPage(tabPage);
        //        //if (FeatureFlags.IconsEnabled || Device.RuntimePlatform == "iOS")
        //        //{
        //            //page.IconImageSource = "/Assets/app_settings.png";
        //         //   page.IconImageSource = Icons.Twitter;
        //        //}
        //        page.Title = p;// "Home";

        //        tabs.Children.Add(page);
        //    }
        //}

        public static bool ForceLogin = true;

        public static bool UserAuthenticated
        {
            get
            {
                return currentUser != null && !string.IsNullOrEmpty(currentUser.Id);
            }
        }

        private static Item currentUser;
        public static Item CurrentUser
        {
            get
            {
                //if (currentUser != null && !string.IsNullOrEmpty($"{currentUser.UserName}{currentUser.PasswordHash}")) return currentUser;
                if (currentUser != null && !string.IsNullOrEmpty($"{currentUser.Id}")) return currentUser;
                //currentUser = new Item(Preferences.Get(Settings.CurrentUserKey, null));

                if (currentUser != null && !string.IsNullOrEmpty($"{currentUser.Id}")) return currentUser;

                currentUser = new Item(Settings.CurrentUserKey);

                if (currentUser != null && !string.IsNullOrEmpty($"{currentUser.Id}")) return currentUser;

                var userFile = new Item(currentUser.Id);

                if (currentUser != null && !string.IsNullOrEmpty($"{currentUser.Id}")) return currentUser;

                //if (!Application.Current.Properties.ContainsKey(Settings.CurrentUserKey) || Application.Current?.Properties?[Settings.CurrentUserKey] == null) return null;
                //var cached = Application.Current?.Properties?[Settings.CurrentUserKey]?.ToString();
                //if (cached != null)
                //{
                //    currentUser = new Item(cached);
                //}
                return currentUser;
            }
            set
            {
                currentUser = value;
                if (currentUser == null)
                {
                    //Preferences.Remove(Settings.CurrentUserKey);
                    //Application.Current.Properties.Remove(Settings.CurrentUserKey);
                }
                else
                {
                    currentUser.ToFile<Item>();
                    var userJson = currentUser.ToJson();
                    //Preferences.Set(Settings.CurrentUserKey, userJson);
                    //Preferences.Set($"{Settings.CurrentUserKey}-UserName", currentUser.Id);

                    //Application.Current.Properties[Settings.CurrentUserKey] = userJson;
                    //Application.Current.Properties[$"{Settings.CurrentUserKey}-UserName"] = currentUser.Id;
                }

                //Application.Current.SavePropertiesAsync();

                OnCurrentUserChanged?.Invoke(currentUser, new CurrentUserChangedEventArgs(currentUser.Id));
                //SetProperty(ref currentUser, value);
                //Trace.WriteLine($"{AssemblyName}.CurrentUser.Set:{currentUser}");
            }
        }
    }
}