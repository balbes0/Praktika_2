using System;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using Praktika_2.PraktikaodinDataSetTableAdapters;

namespace Praktika_2
{
    public partial class PageDS : Page
    {
        ColorTableAdapter colorTableAdapter = new ColorTableAdapter();
        ShoeTableAdapter shoeTableAdapter = new ShoeTableAdapter();
        ShoeFactoryTableAdapter shoeFactoryTableAdapter = new ShoeFactoryTableAdapter();
        SizeTableAdapter sizeTableAdapter = new SizeTableAdapter();
        ShoeInventoryTableAdapter shoeInventoryTableAdapter = new ShoeInventoryTableAdapter();
        ShoeViewTableAdapter shoeViewTableAdapter = new ShoeViewTableAdapter();
        SizeViewTableAdapter sizeViewTableAdapter = new SizeViewTableAdapter();
        ColorViewTableAdapter colorViewTableAdapter = new ColorViewTableAdapter();
        public string editedText = "";
        string nametable = "";

        public PageDS(string selectedTable)
        {
            InitializeComponent();
            if (selectedTable != "")
            {
                nametable = selectedTable;
                if (selectedTable == "Color")
                {
                    DataGridDS.ItemsSource = colorTableAdapter.GetData();
                }
                else if (selectedTable == "Shoe")
                {
                    DataGridDS.ItemsSource = shoeTableAdapter.GetData();
                }
                else if (selectedTable == "ShoeFactory")
                {
                    DataGridDS.ItemsSource = shoeFactoryTableAdapter.GetData();
                    ShoeTypeIDCMBX.ItemsSource = shoeTableAdapter.GetData();
                    ShoeTypeIDCMBX.DisplayMemberPath = "ID_ShoeType";
                    ColorIDCMBX.ItemsSource = colorTableAdapter.GetData();
                    ColorIDCMBX.DisplayMemberPath = "ID_Color";
                    SizeIDCMBX.ItemsSource= sizeTableAdapter.GetData();
                    SizeIDCMBX.DisplayMemberPath = "ID_Size";
                    TextBoxGridDS.Visibility = Visibility.Hidden;
                    ShoeFactoryGridDS.Visibility = Visibility.Visible;
                }
                else if (selectedTable == "Size")
                {
                    DataGridDS.ItemsSource = sizeTableAdapter.GetData();
                }
                else if (selectedTable == "ShoeInventory")
                {
                    DataGridDS.ItemsSource = shoeInventoryTableAdapter.GetData();
                }
                else if (selectedTable == "ColorView")
                {
                    DataGridDS.ItemsSource = colorViewTableAdapter.GetData();
                }
                else if (selectedTable == "SizeView")
                {
                    DataGridDS.ItemsSource = sizeViewTableAdapter.GetData();
                }
                else if (selectedTable == "ShoeView")
                {
                    DataGridDS.ItemsSource = shoeViewTableAdapter.GetData();
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
                if (nametable == "Color")
                {
                    colorTableAdapter.UpdateQueryColor(editedText, Convert.ToInt32(id));
                }
                else if (nametable == "Shoe")
                {
                    shoeTableAdapter.UpdateQueryShoeType(editedText, Convert.ToInt32(id));
                }
                else if (nametable == "Size")
                {
                    int size = Convert.ToInt32(editedText);
                    sizeTableAdapter.UpdateQuerySize(size, Convert.ToInt32(id));
                }
                else if (nametable == "ShoeFactory")
                {
                    shoeFactoryTableAdapter.UpdateQueryShoeFactory(Convert.ToInt32(ShoeTypeIDCMBX.Text), Convert.ToInt32(SizeIDCMBX.Text), Convert.ToInt32(ColorIDCMBX.Text), Convert.ToInt32(PriceTBX.Text), Convert.ToInt32(id));
                }
                else if (nametable == "ShoeInventory" || nametable == "ShoeView" || nametable == "SizeView" || nametable == "ColorView")
                {
                    MessageBox.Show("Эту таблицу менять нельзя!");
                }
            }
        }
        public void AddNewData()
        {
            if (nametable == "Color")
            {
                colorTableAdapter.InsertQueryColor(TextBoxDS.Text);
                DataGridDS.ItemsSource = colorTableAdapter.GetData();
            }
            else if (nametable == "Shoe")
            {
                shoeTableAdapter.InsertQueryShoeType(TextBoxDS.Text);
                DataGridDS.ItemsSource = shoeTableAdapter.GetData();
            }
            else if (nametable == "Size")
            {
                int size = Convert.ToInt32(TextBoxDS.Text);
                sizeTableAdapter.InsertQuerySize(size);
                DataGridDS.ItemsSource = sizeTableAdapter.GetData();
            }
            else if (nametable == "ShoeFactory")
            {
                shoeFactoryTableAdapter.InsertQueryShoeFactory(Convert.ToInt32(ShoeTypeIDCMBX.Text), Convert.ToInt32(SizeIDCMBX.Text), Convert.ToInt32(ColorIDCMBX.Text), Convert.ToInt32(PriceTBX.Text));
                DataGridDS.ItemsSource= shoeFactoryTableAdapter.GetData();
            }
            else if (nametable == "ShoeInventory" || nametable == "ShoeView" || nametable == "SizeView" || nametable == "ColorView")
            {
                MessageBox.Show("Эту таблицу менять нельзя!");
            }
        }
        private void DataGridDS_CellEditEnding_1(object sender, DataGridCellEditEndingEventArgs e)
        {
            DataGridCell cell = e.Column.GetCellContent(e.Row).Parent as DataGridCell;
            TextBox textBox = cell.Content as TextBox;
            editedText = textBox.Text;
            SaveChanges();
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
                if (nametable == "Color")
                {
                    colorTableAdapter.DeleteQueryColor(Convert.ToInt32(id));
                    DataGridDS.ItemsSource = colorTableAdapter.GetData();
                }
                else if (nametable == "Shoe")
                {
                    shoeTableAdapter.DeleteQueryShoeType(Convert.ToInt32(id));
                    DataGridDS.ItemsSource = shoeTableAdapter.GetData();
                }
                else if (nametable == "Size")
                {
                    int size = Convert.ToInt32(editedText);
                    sizeTableAdapter.DeleteQuerySize(Convert.ToInt32(id));
                    DataGridDS.ItemsSource = sizeTableAdapter.GetData();
                }
                else if (nametable == "ShoeFactory")
                {
                    shoeFactoryTableAdapter.DeleteQueryShoeFactory(Convert.ToInt32(id));
                    DataGridDS.ItemsSource = shoeFactoryTableAdapter.GetData();
                }
                else if (nametable == "ShoeInventory" || nametable == "ShoeView" || nametable == "SizeView" || nametable == "ColorView")
                {
                    MessageBox.Show("Эту таблицу менять нельзя!");
                }
            }
        }
    }
}
