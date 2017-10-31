using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using DataAccess;
using DataAccess.Models;
using BusinessLayer;
using System.IO;
using System.Collections.ObjectModel;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace WorkersViewer
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public WorkerService _business;
        WorkWithXml xml = new WorkWithXml();
        ValidXml valid = new ValidXml();        
                
        public string xmlFile = Config._xmlPath;

        XmlRepository rep = new XmlRepository();

        IEnumerable<Worker> workers;

        public enum ViewForm { Create = 0, Update = 1, View =2}

        public MainWindow()
        {            
            InitializeComponent();
            _business = new WorkerService(new XmlRepository(), xmlFile);
            InitializeGrid();
        }
        
        public void InitializeGrid()
        {
            workers = this._business.Get("Workers/*");
            this.DataGrid.ItemsSource = workers;
        }

        #region Buttons from right column

        #region Btn_Add
        /// <summary>
        /// Double menu for button Add is visible
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HandleCheck_btn_add(object sender, RoutedEventArgs e)
        {
            Double_addMenu.Visibility = Visibility.Visible;
            Double_addMenu_text.Visibility = Visibility.Visible;
        }        

        /// <summary>
        /// Double menu for button Add is hidden
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HandleUnchecked_btn_add(object sender, RoutedEventArgs e)
        {
            Double_addMenu.Visibility = Visibility.Hidden;
            Double_addMenu_text.Visibility = Visibility.Hidden;
        }

        /// <summary>
        /// Click for button right "Add developer" - view form to add new developer
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_Dev_Add(object sender, RoutedEventArgs e)
        {
            DeveloperViewer dev = new DeveloperViewer(new Developer(), ViewForm.Create, xmlFile);
            dev.ShowDialog();
            this.InitializeGrid();
        }

        /// <summary>
        /// Click for button right "Add office worker" - view form to add new office worker
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_Office_Add(object sender, RoutedEventArgs e)
        {
            OfficeViewer office = new OfficeViewer(new OfficeWorker(), ViewForm.Create, xmlFile);
            office.ShowDialog();
            this.InitializeGrid();
        }
        #endregion

        /// <summary>
        /// Click for right button "Update" - view form to update worker's info
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_Update(object sender, RoutedEventArgs e)
        {
            if (this.DataGrid.SelectedItem is Developer)
            {
                DeveloperViewer window = new DeveloperViewer((Developer)this.DataGrid.SelectedItem, ViewForm.Update, xmlFile);
                window.ShowDialog();
                this.InitializeGrid();
            }
            else if (this.DataGrid.SelectedItem is OfficeWorker)
            {
                OfficeViewer window = new OfficeViewer((OfficeWorker)this.DataGrid.SelectedItem, MainWindow.ViewForm.Update, xmlFile);
                window.ShowDialog();
                this.InitializeGrid();
            }
            else
            {
                MessageBox.Show("Select item you want to update.");
            }
        }

        /// <summary>
        /// Click for button "Delete" - delete chosen worker
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_Delete(object sender, RoutedEventArgs e)
        {
            Worker worker = (Worker)this.DataGrid.SelectedItem;
            if(worker != null)
            {
                var result = MessageBox.Show(this, "Are you shure you want to delete this item?", "Warning", MessageBoxButton.OKCancel,
                                MessageBoxImage.Asterisk);

                if (result == MessageBoxResult.OK && worker != null)
                {
                    this._business.Delete(worker._id);
                    this.InitializeGrid();
                }
            }
            else
            {
                MessageBox.Show("Select item you want to delete.");
            }
        }
        #endregion

        #region MainWindow menu

        #region Menu item "File"
        /// <summary>
        /// Click for button "Save" - show the dialog box to save the file
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Menu_Save(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog
            {
                DefaultExt = ".xml",
                Filter = "Xml-documents (.xml)|*.xml"
            };

            bool? result = dlg.ShowDialog();

            if(result == true)
            {
                string stringForSave = xml.SerializeObject(new XmlRepository(result));
                var stringReader = new StringReader(stringForSave);
                XDocument xmlDocument = XDocument.Load(stringReader);
                xmlDocument.Save(dlg.FileName);
            }
        }

        /// <summary>
        /// Click for button "Open" - show the dialog box to choose new file
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Menu_Open(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog
            {
                DefaultExt = ".xml",
                Filter = "Xml-documents (.xml)|*.xml"
            };

            bool? result = dlg.ShowDialog();

            if(result == true && dlg.FileName != string.Empty)
            {
                xmlFile = dlg.FileName;
                _business = null;
                _business = new WorkerService(new XmlRepository(), dlg.FileName);
                InitializeGrid();
            }
        }

        /// <summary>
        /// Click for button "Close" - close the program
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Menu_Close(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        #endregion

        #region Menu item "Service"

        #region Menu item "Add"
        /// <summary>
        /// Click for button "Add" - "Developer" - call method Btn_Dev_Add
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Menu_Add_Dev(object sender, RoutedEventArgs e)
        {
            this.Btn_Dev_Add(sender, e);
        }

        /// <summary>
        /// Click for button "Add" - "Office worker" - call method Btn_Office_Add
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Menu_Add_Office(object sender, RoutedEventArgs e)
        {
            this.Btn_Office_Add(sender, e);
        }
        #endregion

        /// <summary>
        /// Click for button "Update" - call method Btn_Update
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Menu_Update(object sender, RoutedEventArgs e)
        {
            this.Btn_Update(sender, e);
        }

        /// <summary>
        /// Click for button "Delete" - call method Btn_Delete
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Menu_Delete(object sender, RoutedEventArgs e)
        {
            this.Btn_Delete(sender, e);
        }

        /// <summary>
        /// Click for button "View" - view form with worker's info
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Menu_View(object sender, RoutedEventArgs e)
        {
            if (this.DataGrid.SelectedItem is Developer)
            {
                DeveloperViewer window = new DeveloperViewer((Developer)this.DataGrid.SelectedItem, ViewForm.View, xmlFile);
                window.ShowDialog();
                this.InitializeGrid();
            }
            else if (this.DataGrid.SelectedItem is OfficeWorker)
            {
                OfficeViewer window = new OfficeViewer((OfficeWorker)this.DataGrid.SelectedItem, MainWindow.ViewForm.View, xmlFile);
                window.ShowDialog();
                this.InitializeGrid();
            }
            else
            {
                MessageBox.Show("Select item you want to view.");
            }
        }
        #endregion

        #region Menu item "Tools"
        /// <summary>
        /// Click for button "Resynchronize" - update main window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Menu_Resynchronize(object sender, RoutedEventArgs e)
        {
            workers = null;
            workers = this._business.Get("Workers/*");
            InitializeGrid();
        }
        #endregion

        #endregion
    }
}
