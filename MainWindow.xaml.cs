using System.Collections.Generic;
using System.Data;
using System.Windows;
using System.Windows.Controls;

namespace Praktika_2
{
    public partial class MainWindow : Window
    {
        private PageDS currentPageDS;
        private PageEF currentPageEF;
        string selectedPage;
        string selectedTable = "";
        PraktikaodinDataSet dataSet;
        List<string> tableNames = new List<string>();
        public MainWindow()
        {
            InitializeComponent();
            dataSet = new PraktikaodinDataSet();
            DataTableCollection tables = dataSet.Tables;
            foreach (DataTable table in tables)
            {
                tableNames.Add(table.TableName);
            }
            ComboBoxChangeTable.ItemsSource = tableNames;
        }
        private void NextPage_Click(object sender, RoutedEventArgs e)
        {
            selectedPage = "EntityFramework";
            NavigateToPageEF(selectedTable);
            LabelChangeFramework.Visibility = Visibility.Hidden;
        }
        private void PagePreviousDS_Click(object sender, RoutedEventArgs e)
        {
            selectedPage = "DataSet";
            NavigateToPageDS(selectedTable);
            LabelChangeFramework.Visibility = Visibility.Hidden;
        }

        private void ComboBoxChangeTable_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedTable = ComboBoxChangeTable.Items[ComboBoxChangeTable.SelectedIndex] as string;
            if (selectedTable == "ShoeView" || selectedTable == "SizeView" || selectedTable == "ColorView" || selectedTable == "ShoeInventory")
            {
                AddButton.Visibility = Visibility.Collapsed;
                DeleteDataButton.Visibility = Visibility.Collapsed;
                SaveChangesButton.Visibility = Visibility.Collapsed;
            }
            else
            {
                AddButton.Visibility = Visibility.Visible;
                DeleteDataButton.Visibility = Visibility.Visible;
                SaveChangesButton.Visibility = Visibility.Visible;
            }
            if (selectedPage == "DataSet")
            {
                NavigateToPageDS(selectedTable);
            }
            else if (selectedPage == "EntityFramework")
            {
                NavigateToPageEF(selectedTable);
            }
        }
        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            if (selectedPage == "DataSet")
            {
                currentPageDS.AddNewData();
            }
            else if (selectedPage == "EntityFramework")
            {
                currentPageEF.AddNewData();
            }
        }
        private void SaveChangesButton_Click(object sender, RoutedEventArgs e)
        {
            if (selectedPage == "DataSet")
            {
                if (currentPageDS != null)
                {
                    currentPageDS?.SaveChangesWithButton();
                }
            }
            else if (selectedPage == "EntityFramework")
            {
                if (currentPageEF != null)
                {
                    currentPageEF?.SaveChangesWithButton();
                }
            }
        }
        private void DeleteDataButton_Click(object sender, RoutedEventArgs e)
        {
            if (selectedPage == "DataSet")
            {
                if (currentPageDS != null)
                {
                    currentPageDS?.DeleteWithButton();
                }
            }
            else if (selectedPage == "EntityFramework")
            {
                if (currentPageEF != null)
                {
                    currentPageEF?.DeleteWithButton();
                }
            }
        }
        private void NavigateToPageDS(string selectedTable)
        {
            currentPageDS = new PageDS(selectedTable);
            PageFrame.Content = currentPageDS;
        }
        private void NavigateToPageEF(string selectedTable)
        {
            currentPageEF = new PageEF(selectedTable);
            PageFrame.Content = currentPageEF;
        }
    }
}
