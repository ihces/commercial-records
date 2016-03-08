using CommercialRecords.Common;
using CommercialRecords.Models;
using CommercialRecords.Models.Accounts;
using CommercialRecords.Models.Accounts.EnterpriseAccounts;
using CommercialRecords.Models.Goods;
using CommercialRecords.Models.IncomeNExpense;
using CommercialRecords.ViewModels.DataVMs.Accounts.EnterpriseAccounts;
using CommercialRecords.Views;
using System;
using System.Globalization;
using System.IO;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace CommercialRecords
{
    sealed partial class App : Application
    {
        public static string DBPath = string.Empty;
        public static StorageFolder
            ProfileImgFolder = null,
            GoodImgFolder = null,
            CategoryImgFolder = null,
            FirmImgFolder = null,
            CommonImgFolder = null;
        public const string
            PROFILE_IMG_FOLDER = "ProfilePhotos",
            GOOD_IMG_FOLDER = "GoodPhotos",
            CATEGORY_IMG_FOLDER = "CategoryPhotos",
            FIRM_IMG_FOLDER = "FirmPhotos",
            COMMON_IMG_FOLDER = "CommonPhotos";

        public static readonly string CurrencySymbol = CultureInfo.CurrentCulture.NumberFormat.CurrencySymbol;

        public class CRInitializer
        {
            public void startInit()
            {
                initAsyncs();
                initDatabase();
            }

            private async void initAsyncs()
            {
                ProfileImgFolder = await ApplicationData.Current.LocalFolder.CreateFolderAsync(PROFILE_IMG_FOLDER, CreationCollisionOption.OpenIfExists);
                GoodImgFolder = await ApplicationData.Current.LocalFolder.CreateFolderAsync(GOOD_IMG_FOLDER, CreationCollisionOption.OpenIfExists);
                CategoryImgFolder = await ApplicationData.Current.LocalFolder.CreateFolderAsync(CATEGORY_IMG_FOLDER, CreationCollisionOption.OpenIfExists);
                FirmImgFolder = await ApplicationData.Current.LocalFolder.CreateFolderAsync(FIRM_IMG_FOLDER, CreationCollisionOption.OpenIfExists);
                CommonImgFolder = await ApplicationData.Current.LocalFolder.CreateFolderAsync(COMMON_IMG_FOLDER, CreationCollisionOption.OpenIfExists);
            }

            private void initDatabase()
            {
                DBPath = Path.Combine(ApplicationData.Current.LocalFolder.Path, "commercial_records.db");

                // Create the tables if they don't exist
                using (var db = new SQLite.SQLiteConnection(DBPath))
                {
                    db.CreateTable<Good>();
                    db.CreateTable<Brand>();
                    db.CreateTable<Category>();
                    db.CreateTable<Actor>();
                    db.CreateTable<Transact>();
                    db.CreateTable<SaleEntry>();
                    db.CreateTable<PaymentEntry>();
                    db.CreateTable<EnterpriseAccount>();
                    db.CreateTable<CurrentAccount>();
                    db.CreateTable<EnterpriseAccTransact>();

                    EnterpriseAccountVM enterpriseMainAccount = new EnterpriseAccountVM();
                    enterpriseMainAccount.get(1);
                    if (!enterpriseMainAccount.Recorded)
                    {
                        enterpriseMainAccount.Name = CrsDictionary.getInstance().lookup("instanceLabels", "cashRegAcct1");
                        enterpriseMainAccount.CreateDate = DateTime.Now;
                        enterpriseMainAccount.Type = 0;
                        enterpriseMainAccount.save();
                    }
                }
            }
        }

        public App()
        {
            InitializeComponent();
            Suspending += OnSuspending;
        }

        protected override void OnLaunched(LaunchActivatedEventArgs e)
        {

#if DEBUG
            if (System.Diagnostics.Debugger.IsAttached)
            {
                this.DebugSettings.EnableFrameRateCounter = true;
            }
#endif

            Frame rootFrame = Window.Current.Content as Frame;

            // Do not repeat app initialization when the Window already has content,
            // just ensure that the window is active
            if (rootFrame == null)
            {
                // Create a Frame to act as the navigation context and navigate to the first page
                rootFrame = new Frame();
                // Set the default language
                rootFrame.Language = Windows.Globalization.ApplicationLanguages.Languages[0];

                rootFrame.NavigationFailed += OnNavigationFailed;

                if (e.PreviousExecutionState == ApplicationExecutionState.Terminated)
                {
                    //TODO: Load state from previously suspended application
                }

                // Place the frame in the current Window
                Window.Current.Content = rootFrame;
            }

            if (rootFrame.Content == null)
            {
                // When the navigation stack isn't restored navigate to the first page,
                // configuring the new page by passing required information as a navigation
                // parameter
                rootFrame.Navigate(typeof(MainPage), e.Arguments);
            }
            // Ensure the current window is active
            Window.Current.Activate();
        }

        /// <summary>
        /// Invoked when Navigation to a certain page fails
        /// </summary>
        /// <param name="sender">The Frame which failed navigation</param>
        /// <param name="e">Details about the navigation failure</param>
        void OnNavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            throw new Exception("Failed to load Page " + e.SourcePageType.FullName);
        }

        /// <summary>
        /// Invoked when application execution is being suspended.  Application state is saved
        /// without knowing whether the application will be terminated or resumed with the contents
        /// of memory still intact.
        /// </summary>
        /// <param name="sender">The source of the suspend request.</param>
        /// <param name="e">Details about the suspend request.</param>
        private void OnSuspending(object sender, SuspendingEventArgs e)
        {
            var deferral = e.SuspendingOperation.GetDeferral();
            //TODO: Save application state and stop any background activity
            deferral.Complete();
        }
    }
}
