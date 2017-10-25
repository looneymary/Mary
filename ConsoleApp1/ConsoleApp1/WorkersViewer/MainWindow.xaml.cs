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

namespace WorkersViewer
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly BusinessLayerMethods _business;
        IEnumerable<Worker> workers;

        public enum ViewForm { Create = 0, Update = 1, View =2}

        public MainWindow()
        {            
            InitializeComponent();
            _business = new BusinessLayerMethods(new XmlRepository());
            InitializeGrid();
        }
        
        public void InitializeGrid()
        {
            workers = this._business.Get("Workers/*");
            this.DataGrid.ItemsSource = workers;
        }

        #region Buttons from right column

        #region Btn_Add
        private void HandleCheck_btn_add(object sender, RoutedEventArgs e)
        {
            Double_addMenu.Visibility = Visibility.Visible;
            Double_addMenu_text.Visibility = Visibility.Visible;
        }        

        private void HandleUnchecked_btn_add(object sender, RoutedEventArgs e)
        {
            Double_addMenu.Visibility = Visibility.Hidden;
            Double_addMenu_text.Visibility = Visibility.Hidden;
        }

        private void Btn_Dev_Add(object sender, RoutedEventArgs e)
        {
            DeveloperViewer dev = new DeveloperViewer(new Developer(), ViewForm.Create);
            dev.ShowDialog();
            this.InitializeGrid();
        }

        private void Btn_Office_Add(object sender, RoutedEventArgs e)
        {
            OfficeViewer office = new OfficeViewer(new OfficeWorker(), ViewForm.Create);
            office.ShowDialog();
            this.InitializeGrid();
        }
        #endregion
                      
        private void Btn_Update(object sender, RoutedEventArgs e)
        {
            if (this.DataGrid.SelectedItem is Developer)
            {
                DeveloperViewer window = new DeveloperViewer((Developer)this.DataGrid.SelectedItem, ViewForm.Update);
                window.ShowDialog();
                this.InitializeGrid();
            }
            else if (this.DataGrid.SelectedItem is OfficeWorker)
            {
                OfficeViewer window = new OfficeViewer((OfficeWorker)this.DataGrid.SelectedItem, MainWindow.ViewForm.Update);
                window.ShowDialog();
                this.InitializeGrid();
            }
            else
            {
                MessageBox.Show("Select item you want to update.");
            }
        }

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
        private void Menu_Save(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
            dlg.DefaultExt = ".xml";
            dlg.Filter = "Xml-documents (.xml)|*.xml";

            bool? resault = dlg.ShowDialog();

            if(resault == true)
            {
            }
        }

        private void Menu_Open(object sender, RoutedEventArgs e)
        {
        }

        private void Menu_Close(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        #endregion

        #region Menu item "Service"

        #region Menu item "Add"
        private void Menu_Add_Dev(object sender, RoutedEventArgs e)
        {
            this.Btn_Dev_Add(sender, e);
        }

        private void Menu_Add_Office(object sender, RoutedEventArgs e)
        {
            this.Btn_Office_Add(sender, e);
        }
        #endregion

        private void Menu_Update(object sender, RoutedEventArgs e)
        {
            this.Btn_Update(sender, e);
        }

        private void Menu_Delete(object sender, RoutedEventArgs e)
        {
            this.Btn_Delete(sender, e);
        }

        private void Menu_View(object sender, RoutedEventArgs e)
        {
            if (this.DataGrid.SelectedItem is Developer)
            {
                DeveloperViewer window = new DeveloperViewer((Developer)this.DataGrid.SelectedItem, ViewForm.View);
                window.ShowDialog();
                this.InitializeGrid();
            }
            else if (this.DataGrid.SelectedItem is OfficeWorker)
            {
                OfficeViewer window = new OfficeViewer((OfficeWorker)this.DataGrid.SelectedItem, MainWindow.ViewForm.View);
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
