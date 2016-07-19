using CommercialRecords.Controls;
using CommercialRecords.ViewModels.DataVMs.Settings;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using CommercialRecords.ViewModels;
using System;
using Windows.UI.Xaml;

namespace CommercialRecords.Common
{
    class CrsAuthentication
    {
        public enum SESSION_STATUS { LOG_OUT, TIME_OUT, LOG_IN }

        public class SessionControlVM : VMBase
        {
            private UserVM currentUser = null;
            public UserVM CurrentUser
            {
                get
                {
                    return currentUser;
                }
                set
                {
                    currentUser = value;
                    RaisePropertyChanged("CurrentUser");
                }
            }

            private DateTime timeoutDate = DateTime.Now;
            public DateTime TimeoutDate
            {
                get
                {
                    return timeoutDate;
                }
                set
                {
                    timeoutDate = value;
                    RaisePropertyChanged("TimeoutDate");
                }
            }

            private DateTime lastLoginDate = DateTime.Now;
            public DateTime LastLoginDate
            {
                get
                {
                    return lastLoginDate;
                }
                set
                {
                    lastLoginDate = value;
                    RaisePropertyChanged("LastLoginDate");
                }
            }

            private SESSION_STATUS sessionStatus = SESSION_STATUS.LOG_OUT;
            public SESSION_STATUS SessionStatus
            {
                get
                {
                    if (sessionStatus.Equals(SESSION_STATUS.LOG_IN))
                    {
                        TimeSpan diff = DateTime.Now - TimeoutDate;

                        if (diff.Seconds > 20)
                        {
                            SessionStatus = SESSION_STATUS.TIME_OUT;
                            return SESSION_STATUS.TIME_OUT;
                        }
                    }

                    return sessionStatus;
                }
                set
                {
                    sessionStatus = value;
                    RaisePropertyChanged("SessionStatus");
                }
            }
        }

        private SessionControlVM sessionControl;
        public SessionControlVM SessionControl
        {
            get
            {
                return sessionControl;
            }
        }

        private static CrsAuthentication instance;

        public class UserAuthControlVM : VMBase
        {
            private ObservableCollection<UserVM> users;
            public ObservableCollection<UserVM> Users
            {
                get
                {
                    return users;
                }
                set
                {
                    users = value;
                    RaisePropertyChanged("Users");
                }
            }

            private UserVM selectedUser = null;
            public UserVM SelectedUser
            {
                get
                {
                    return selectedUser;
                }
                set
                {
                    selectedUser = value;
                    RaisePropertyChanged("SelectedUser");
                }
            }

            private string password;
            public string Password
            {
                get
                {
                    return password;
                }
                set
                {
                    password = value;
                    RaisePropertyChanged("Password");
                }
            }

            private readonly ICommand cancelLoginCmd;
            public ICommand CancelLoginCmd
            {
                get
                {
                    return cancelLoginCmd;
                }
            }

            private readonly ICommand loginCmd;
            public ICommand LoginCmd
            {
                get
                {
                    return loginCmd;
                }
            }

            private CrsUserAuthentication userAuthentication;

            public UserAuthControlVM(CrsUserAuthentication userAuthentication)
            {
                this.userAuthentication = userAuthentication;
                loginCmd = new ICommandImp(loginCmd_handler);
                cancelLoginCmd = new ICommandImp(cancelLoginCmd_handler);
                setUsers();
                Password = string.Empty;
            }

            private void loginCmd_handler(object obj)
            {
                CrsAuthentication authInstance = CrsAuthentication.getInstance();
                if (Password.Equals(SelectedUser.Password))
                {
                    userAuthentication.IsOpen = false;
                    authInstance.sessionControl.CurrentUser = SelectedUser;
                    authInstance.sessionControl.SessionStatus = SESSION_STATUS.LOG_IN;
                    authInstance.sessionControl.LastLoginDate = DateTime.Now;
                    authInstance.sessionControl.TimeoutDate = DateTime.Now;
                }

                Password = string.Empty;
            }

            private void cancelLoginCmd_handler(object obj)
            {
                userAuthentication.IsOpen = false;
            }

            private async Task setUsers()
            {
                Users = new ObservableCollection<UserVM>(await UserVM.getList<UserVM>());

                UserVM currentUser = CrsAuthentication.getInstance().SessionControl.CurrentUser;

                if (null != currentUser)
                {
                    foreach (UserVM user in Users)
                    {
                        if (currentUser.Id.Equals(user.Id))
                        {
                            SelectedUser = user;
                            break;
                        }
                    }
                }

                if (null == SelectedUser)
                    SelectedUser = Users[0];
            }
        }

        private CrsAuthentication()
        {
            sessionControl = new SessionControlVM();
        }

        private Panel container;
        CrsUserAuthentication userAuthUC = null;

        private void updateCurrentContainer()
        {
            container = CommonUIFunctions.getPageMainPanel();
        }

        public void updateUserAuthUC()
        {
            for (int i = 0; i < container.Children.Count; ++i)
            {
                if (container.Children[i] is CrsUserAuthentication)
                {
                    userAuthUC = (CrsUserAuthentication)container.Children[i];
                    break;
                }
            }
        }

        public static CrsAuthentication getInstance()
        {
            if (null == instance)
            {
                instance = new CrsAuthentication();
            }

            return instance;
        }

        public void showAuthentication()
        {
            updateCurrentContainer();
            updateUserAuthUC();

            if (userAuthUC == null)
            {
                userAuthUC = new CrsUserAuthentication();
                userAuthUC.IsOpen = true;
                container.Children.Add(userAuthUC);
            }
            else if (!userAuthUC.IsOpen)
            {
                userAuthUC.IsOpen = true;
                userAuthUC.cleanForm();
            }
        }

        public void hideAuthentication()
        {
            updateCurrentContainer();
            updateUserAuthUC();

            if (null != userAuthUC && userAuthUC.IsOpen)
            {
                userAuthUC.IsOpen = false;
            }
        }

        public int getPermission(int permission)
        {
            if (null == SessionControl.CurrentUser)
                return 0;

            switch (SessionControl.CurrentUser.Role)
            {
                case "1":
                    return (permission & 192) >> 6;
                case "2":
                    return (permission & 48) >> 4;
                case "3":
                    return (permission & 12) >> 2;
                case "4":
                    return permission & 3;
                default:
                    return 0;
            }
        }

        public void updateTimeoutDate()
        {
            SessionControl.TimeoutDate = DateTime.Now;
        }
    }
}
