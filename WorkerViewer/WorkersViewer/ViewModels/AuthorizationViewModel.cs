using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WorkersViewer.Properties;
using WorkerViewer;
using WorkerViewer.Infrastructure;
using WorkerViewer.ViewModels;

namespace WorkersViewer.ViewModels
{
    class AuthorizationViewModel : BaseWorkerViewModel
    {
        private string _login;
        private string _password;
        private string _currentLaanguage;

        public string Login
        {
            get
            {
                return this._login;
            }
            set
            {
                this._login = value;
                RaisePropertyChanged(nameof(this.Login));
            }
        }
        public string Password
        {
            get
            {
                return this._password;
            }
            set
            {
                this._password = value;
                RaisePropertyChanged(nameof(this.Password));
            }
        }
        public string SelectedLanguage
        {
            get
            {
                return this._currentLaanguage;
            }
            set
            {
                this._currentLaanguage = value;
                RaisePropertyChanged(nameof(this.SelectedLanguage));
                ChangeLanguage();
            }
        }
        public ObservableCollection<KeyValuePair<string, string>> Languages { get; set; }
            
        public AuthorizationViewModel()
        {
            MakeSetOfLanguages();
            this.SelectedLanguage = this.Languages.First(k => k.Key == Thread.CurrentThread.CurrentUICulture.TwoLetterISOLanguageName).Key;
        }

        public ICommand SingInCommand => new CommandHandlerGeneric<Window>(SignIn, true);

        private void SignIn(Window window)
        {
            if (Login == ConfigurationManager.AppSettings["login"] && Password == ConfigurationManager.AppSettings["pass"])
            {
                var mainWindow = new MainWindow();
                window.Close();
                mainWindow.ShowDialog();
            }
            else if(Login != ConfigurationManager.AppSettings["login"])
            {
                MessageBox.Show(Resources.IncorrectLog);
            }
            else if(Password != ConfigurationManager.AppSettings["pass"])
            {
                MessageBox.Show(Resources.IncorrectPass);
            }
        }

        private void ChangeLanguage()
        {
            Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo(this.SelectedLanguage);
            Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo(this.SelectedLanguage);
        } 

        private void MakeSetOfLanguages()
        {
            var languages = Settings.Default.Languages;
            this.Languages = new ObservableCollection<KeyValuePair<string, string>>();
            foreach(var language in languages)
            {
                this.Languages.Add(new KeyValuePair<string, string>(language, Resources.ResourceManager.GetString(language)));
            }
        }
    }
}
