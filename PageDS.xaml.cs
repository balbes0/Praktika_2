using System;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using Praktika_2.Praktika1DataSetTableAdapters;
using static MaterialDesignThemes.Wpf.Theme;

namespace Praktika_2
{
    public partial class PageDS : Page
    {
        ColorTableAdapter colorTableAdapter = new ColorTableAdapter();
        ShoeTableAdapter shoeTableAdapter = new ShoeTableAdapter();
        ShoeFactoryTableAdapter shoeFactoryTableAdapter = new ShoeFactoryTableAdapter();
        SizeTableAdapter sizeTableAdapter = new SizeTableAdapter();
        ShoeFactoryViewTableAdapter shoeFactoryViewTableAdapter = new ShoeFactoryViewTableAdapter();
        ShoeViewTableAdapter shoeViewTableAdapter = new ShoeViewTableAdapter();
        SizeViewTableAdapter sizeViewTableAdapter = new SizeViewTableAdapter();
        ColorViewTableAdapter colorViewTableAdapter = new ColorViewTableAdapter();
        public string editedText = "";
        string nametable = "";
        int selectedShoeTypeID;
        int selectedSizeID;
        int selectedColorID;

        public PageDS(string selectedTable)
        {
            InitializeComponent();
            if (selectedTable != "")
            {
                nametable = selectedTable;
                if (selectedTable == "Цвет")
                {
                    DataGridDS.ItemsSource = colorViewTableAdapter.GetData();
                }
                else if (selectedTable == "Обувь")
                {
                    DataGridDS.ItemsSource = shoeViewTableAdapter.GetData();
                }
                else if (selectedTable == "Обувная фабрика")
                {
                    DataGridDS.ItemsSource = shoeFactoryViewTableAdapter.GetData();
                    ShoeTypeIDCMBX.ItemsSource = shoeTableAdapter.GetData();
                    ShoeTypeIDCMBX.DisplayMemberPath = "ShoeType";
                    ColorIDCMBX.ItemsSource = colorTableAdapter.GetData();
                    ColorIDCMBX.DisplayMemberPath = "Цвет";
                    SizeIDCMBX.ItemsSource= sizeTableAdapter.GetData();
                    SizeIDCMBX.DisplayMemberPath = "Размер";
                    TextBoxGridDS.Visibility = Visibility.Hidden;
                    ShoeFactoryGridDS.Visibility = Visibility.Visible;
                }
                else if (selectedTable == "Размер")
                {
                    DataGridDS.ItemsSource = sizeViewTableAdapter.GetData();
                }
                if (selectedTable == "Объединенная таблица")
                {
                    DataGridDS.ItemsSource = shoeFactoryTableAdapter.GetData();
                }
                DataGridDS.Visibility = Visibility.Visible;
                LabelDS.Visibility = Visibility.Hidden;
            }
        }
        private void SaveChanges()
        {
            object id = (DataGridDS.SelectedItem as DataRowView).Row[0];
            if (nametable != "")
            {
                if (nametable == "Цвет")
                {
                    colorTableAdapter.UpdateQueryColor(editedText, Convert.ToInt32(id));
                    DataGridDS.ItemsSource = colorViewTableAdapter.GetData();
                }
                else if (nametable == "Обувь")
                {
                    shoeTableAdapter.UpdateQueryShoeType(editedText, Convert.ToInt32(id));
                    DataGridDS.ItemsSource = shoeViewTableAdapter.GetData();
                }
                else if (nametable == "Размер")
                {
                    int size = Convert.ToInt32(editedText);
                    sizeTableAdapter.UpdateQuerySize(size, Convert.ToInt32(id));
                    DataGridDS.ItemsSource = sizeViewTableAdapter.GetData();
                }
                else if (nametable == "Обувная фабрика")
                {
                    shoeFactoryTableAdapter.UpdateQueryShoeFactory(selectedShoeTypeID, selectedSizeID, selectedColorID, Convert.ToInt32(PriceTBX.Text), Convert.ToInt32(id));
                    DataGridDS.ItemsSource = shoeFactoryViewTableAdapter.GetData();
                }
                DataGridDS.Columns[0].Visibility = Visibility.Collapsed;
            }
        }
        public void AddNewData()
        {
            if (nametable != "")
            {   
                if (nametable == "Цвет")
                {
                    colorTableAdapter.InsertQueryColor(TextBoxDS.Text);
                    DataGridDS.ItemsSource = colorViewTableAdapter.GetData();
                }
                else if (nametable == "Обувь")
                {
                    shoeTableAdapter.InsertQueryShoeType(TextBoxDS.Text);
                    DataGridDS.ItemsSource = shoeViewTableAdapter.GetData();
                }
                else if (nametable == "Размер")
                {
                    int size = Convert.ToInt32(TextBoxDS.Text);
                    sizeTableAdapter.InsertQuerySize(size);
                    DataGridDS.ItemsSource = sizeViewTableAdapter.GetData();
                }
                else if (nametable == "Обувная фабрика")
                {
                    shoeFactoryTableAdapter.InsertQueryShoeFactory(selectedShoeTypeID, selectedSizeID, selectedColorID, Convert.ToInt32(PriceTBX.Text));
                    DataGridDS.ItemsSource = shoeFactoryViewTableAdapter.GetData();
                }
                DataGridDS.Columns[0].Visibility = Visibility.Collapsed;
            }
        }
        public void SaveChangesWithButton()
        {
            editedText = TextBoxDS.Text;
            SaveChanges();
        }
        public void DeleteWithButton()
        {
            object id = (DataGridDS.SelectedItem as DataRowView).Row[0];
            if (nametable != "")
            {
                if (nametable == "Цвет")
                {
                    colorTableAdapter.DeleteQueryColor(Convert.ToInt32(id));
                    DataGridDS.ItemsSource = colorViewTableAdapter.GetData();
                }
                else if (nametable == "Обувь")
                {
                    shoeTableAdapter.DeleteQueryShoeType(Convert.ToInt32(id));
                    DataGridDS.ItemsSource = shoeViewTableAdapter.GetData();
                }
                else if (nametable == "Размер")
                {
                    sizeTableAdapter.DeleteQuerySize(Convert.ToInt32(id));
                    DataGridDS.ItemsSource = sizeViewTableAdapter.GetData();
                }
                else if (nametable == "Обувная фабрика")
                {
                    shoeFactoryTableAdapter.DeleteQueryShoeFactory(Convert.ToInt32(id));
                    DataGridDS.ItemsSource = shoeFactoryViewTableAdapter.GetData();
                }
                DataGridDS.Columns[0].Visibility = Visibility.Collapsed;
            }
        }

        private void ShoeTypeIDCMBX_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataRowView selectedRow = (DataRowView)ShoeTypeIDCMBX.SelectedItem;
            selectedShoeTypeID = (int)selectedRow["ID_ShoeType"];
        }

        private void SizeIDCMBX_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataRowView selectedRow = (DataRowView)SizeIDCMBX.SelectedItem;
            selectedSizeID = (int)selectedRow["ID_Size"];
        }

        private void ColorIDCMBX_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataRowView selectedRow = (DataRowView)ColorIDCMBX.SelectedItem;
            selectedColorID = (int)selectedRow["ID_Color"];
        }

        private void DataGridDS_Loaded(object sender, RoutedEventArgs e)
        {
            if (nametable != "")
            {
                DataGridDS.Columns[0].Visibility = Visibility.Collapsed;
            }
            if (nametable == "Объединенная таблица")
            {
                DataGridDS.Columns[2].Visibility = Visibility.Collapsed;
                DataGridDS.Columns[4].Visibility = Visibility.Collapsed;
            }
        }
    }
}
