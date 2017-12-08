using BusinessLayer;
using DataAccess;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Xml.Linq;
using WorkerViewer.Infrastructure;

namespace WorkerViewer.ViewModels
{
    public class WorkerViewModel : BaseWorkerViewModel
    {
        protected WorkerService _business;
        // Field to save the chosen worker
        private BaseWorkerViewModel _currentItem;

        public ObservableCollection<BaseWorkerViewModel> Workers { get; set; }

        // Property for _currentItem
        public BaseWorkerViewModel SelectedItem
        {
            get
            {
                return this._currentItem;
            }
            set
            {
                if(this._currentItem != value)
                {
                    this._currentItem = value;
                    RaisePropertyChanged(nameof(this.SelectedItem));
                }
            }
        }
        
        public WorkerViewModel()
        {
            this._business = new WorkerService(new XmlRepository());
            Workers = new ObservableCollection<BaseWorkerViewModel>(this._business.Get("Workers/*").Select(Mapper.MapEntityToModel));
            SelectedItem = null;
        }

        #region Commands
        public ICommand AddDeveloperCommand => new CommandHandler(AddDeveloper, true);
        public ICommand AddOfficeCommand => new CommandHandler(AddOffice, true);
        public ICommand ViewWorkerCommand => new CommandHandler(ViewWorker, true);
        public ICommand DeleteCommand => new CommandHandler(Delete, true);
        public ICommand UpdateCommand => new CommandHandler(Update, true);
        public ICommand ResynchronizeCommand => new CommandHandlerGeneric<MainWindow>(Resynchronize, true);
        public ICommand SaveDocumentCommand => new CommandHandler(SaveDocument, true);
        public ICommand OpenDocumentCommand => new CommandHandlerGeneric<MainWindow>(OpenDocument, true);
        #endregion

        #region Methods
        /// <summary>
        /// Add new office worker
        /// </summary>
        private void AddDeveloper()
        {
            var dev = base._xmlOpenFile != null ? new DeveloperViewModel(base._xmlOpenFile) : new DeveloperViewModel()
            {
                IsReadOnly = false,
                CreateorUpdate = CreateOrUpdate.Create
            };

            DeveloperViewer viewer = new DeveloperViewer
            {
                DataContext = dev
            };
            viewer.ShowDialog();
            if(dev.IsCreate) this.Workers.Add(dev);
        }

        /// <summary>
        /// Add new developer
        /// </summary>
        private void AddOffice()
        {
            var office = base._xmlOpenFile != null ? new OfficeWorkerViewModel(base._xmlOpenFile) : new OfficeWorkerViewModel()
            {
                IsReadOnly = false,
                CreateorUpdate = CreateOrUpdate.Create
            };

            OfficeViewer viewer = new OfficeViewer
            {
                DataContext = office
            };
            viewer.ShowDialog();
            if(office.IsCreate) this.Workers.Add(office);
        }

        /// <summary>
        /// Show info of chosen worker
        /// </summary>
        private void ViewWorker()
        {
            if(SelectedItem is DeveloperViewModel)
            {
                var viewModel = (DeveloperViewModel)this.SelectedItem;
                viewModel.IsReadOnly = true;
                viewModel.BeginEdit();

                DeveloperViewer dev = new DeveloperViewer();
                dev.BtnDevSave.Visibility = Visibility.Hidden;
                dev.DataContext = viewModel;
                dev.ShowDialog();

            }
            else if(SelectedItem is OfficeWorkerViewModel)
            {
                var viewModel = (OfficeWorkerViewModel)this.SelectedItem;
                viewModel.IsReadOnly = true;
                viewModel.BeginEdit();

                OfficeViewer office = new OfficeViewer();
                office.BtnOfficeSave.Visibility = Visibility.Hidden;
                office.DataContext = viewModel;
                office.ShowDialog();
            }
        }    

        /// <summary>
        /// Delete chosen worker
        /// </summary>
        private void Delete()
        {
            // To save selected item and then remove from xml
            var removeItem = SelectedItem;
            this._business.Delete((Mapper.MapModelToEntity(removeItem))._id);
            this.Workers.Remove(SelectedItem);            
        }

        /// <summary>
        /// Update chosen worker
        /// </summary>
        private void Update()
        {
            if (SelectedItem is OfficeWorkerViewModel)
            {
                var office = (OfficeWorkerViewModel)this.SelectedItem;
                // Send chosen item to "Office" window
                office.SelectedWorker = office;
                office.IsReadOnly = false;
                office._xmlOpenFile = base._xmlOpenFile;
                // Save the original values
                office.BeginEdit();
                office.CreateorUpdate = CreateOrUpdate.Update;

                OfficeViewer view = new OfficeViewer
                {
                    DataContext = office
                };
                view.ShowDialog();
            }
            if(SelectedItem is DeveloperViewModel)
            {
                var dev = (DeveloperViewModel)this.SelectedItem;
                // Send chosen item to "Developer" window
                dev.SelectedWorker = dev;
                dev.IsReadOnly = false;
                // Save the original values
                dev.BeginEdit();
                dev.CreateorUpdate = CreateOrUpdate.Update;

                DeveloperViewer view = new DeveloperViewer
                {
                    DataContext = dev
                };
                view.ShowDialog();
            }
        }

        /// <summary>
        /// Update the main window
        /// </summary>
        /// <param name="window"></param>
        private void Resynchronize(MainWindow window)
        {
            Workers = new ObservableCollection<BaseWorkerViewModel>(this._business.Get("Workers/*").Select(Mapper.MapEntityToModel));
            window.DataGrid.ItemsSource = Workers;
        }

        /// <summary>
        /// Show the dialog box to save xml-document 
        /// </summary>
        private void SaveDocument()
        {
            Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog
            {
                DefaultExt = ".xml",
                Filter = "Xml-documents (.xml)|*.xml"
            };

            bool? result = dlg.ShowDialog();

            if (result == true)
            {
                WorkWithXml xml = new WorkWithXml();
                try
                {
                    string stringForSave = xml.SerializeObject(new XmlRepository(result, base._xmlOpenFile));
                    var stringReader = new StringReader(stringForSave);
                    XDocument xmlDocument = XDocument.Load(stringReader);
                    xmlDocument.Save(dlg.FileName);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Xml-document is not valid and can't be opened.\nError: " + ex.Message);
                }
            }
        }

        /// <summary>
        /// Show the dialog box to open new xml-document
        /// </summary>
        /// <param name="window"></param>
        private void OpenDocument(MainWindow window)
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog
            {
                DefaultExt = ".xml",
                Filter = "Xml-documents (.xml)|*.xml"
            };

            bool? result = dlg.ShowDialog();

            if (result == true && dlg.FileName != string.Empty)
            {                
                string xmlFile = dlg.FileName;
                base._xmlOpenFile = xmlFile;
                try
                {
                    this._business = new WorkerService(new XmlRepository(), dlg.FileName);
                    Workers = new ObservableCollection<BaseWorkerViewModel>(this._business.Get("Workers/*").Select(Mapper.MapEntityToModel));
                    window.DataGrid.ItemsSource = Workers;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Xml-document is not valid and can't be opened.\nError: " + ex.Message);
                }    
                    
            }
        }
        #endregion
    }
}
