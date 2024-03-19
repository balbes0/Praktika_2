using System;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using Praktika_2.Praktika1DataSetTableAdapters;

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
                if (selectedTable == "Color")
                {
                    DataGridDS.ItemsSource = colorViewTableAdapter.GetData();
                }
                else if (selectedTable == "Shoe")
                {
                    DataGridDS.ItemsSource = shoeViewTableAdapter.GetData();
                }
                else if (selectedTable == "ShoeFactory")
                {
                    DataGridDS.ItemsSource = shoeFactoryViewTableAdapter.GetData();
                    ShoeTypeIDCMBX.ItemsSource = shoeTableAdapter.GetData();
                    ShoeTypeIDCMBX.DisplayMemberPath = "ShoeType";
                    ColorIDCMBX.ItemsSource = colorTableAdapter.GetData();
                    ColorIDCMBX.DisplayMemberPath = "Color";
                    SizeIDCMBX.ItemsSource= sizeTableAdapter.GetData();
                    SizeIDCMBX.DisplayMemberPath = "Size";
                    TextBoxGridDS.Visibility = Visibility.Hidden;
                    ShoeFactoryGridDS.Visibility = Visibility.Visible;
                }
                else if (selectedTable == "Size")
                {
                    DataGridDS.ItemsSource = sizeViewTableAdapter.GetData();
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
                    DataGridDS.ItemsSource = colorViewTableAdapter.GetData();
                }
                else if (nametable == "Shoe")
                {
                    shoeTableAdapter.UpdateQueryShoeType(editedText, Convert.ToInt32(id));
                    DataGridDS.ItemsSource = shoeViewTableAdapter.GetData();
                }
                else if (nametable == "Size")
                {
                    int size = Convert.ToInt32(editedText);
                    sizeTableAdapter.UpdateQuerySize(size, Convert.ToInt32(id));
                    DataGridDS.ItemsSource = sizeViewTableAdapter.GetData();
                }
                else if (nametable == "ShoeFactory")
                {
                    shoeFactoryTableAdapter.UpdateQueryShoeFactory(Convert.ToInt32(ShoeTypeIDCMBX.Text), Convert.ToInt32(SizeIDCMBX.Text), Convert.ToInt32(ColorIDCMBX.Text), Convert.ToInt32(PriceTBX.Text), Convert.ToInt32(id));
                    DataGridDS.ItemsSource = shoeFactoryViewTableAdapter.GetData();
                }
            }
        }
        public void AddNewData()
        {
            if (nametable == "Color")
            {
                colorTableAdapter.InsertQueryColor(TextBoxDS.Text);
                DataGridDS.ItemsSource = colorViewTableAdapter.GetData();
            }
            else if (nametable == "Shoe")
            {
                shoeTableAdapter.InsertQueryShoeType(TextBoxDS.Text);
                DataGridDS.ItemsSource = shoeViewTableAdapter.GetData();
            }
            else if (nametable == "Size")
            {
                int size = Convert.ToInt32(TextBoxDS.Text);
                sizeTableAdapter.InsertQuerySize(size);
                DataGridDS.ItemsSource = sizeViewTableAdapter.GetData();
            }
            else if (nametable == "ShoeFactory")
            {
                shoeFactoryTableAdapter.InsertQueryShoeFactory(selectedShoeTypeID, selectedSizeID, selectedColorID, Convert.ToInt32(PriceTBX.Text));
                DataGridDS.ItemsSource= shoeFactoryViewTableAdapter.GetData();
            }
            else if (nametable == "ShoeInventory" || nametable == "ShoeView" || nametable == "SizeView" || nametable == "ColorView")
            {
                MessageBox.Show("Эту таблицу менять нельзя!");
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
                if (nametable == "Color")
                {
                    colorTableAdapter.DeleteQueryColor(Convert.ToInt32(id));
                    DataGridDS.ItemsSource = colorViewTableAdapter.GetData();
                }
                else if (nametable == "Shoe")
                {
                    shoeTableAdapter.DeleteQueryShoeType(Convert.ToInt32(id));
                    DataGridDS.ItemsSource = shoeViewTableAdapter.GetData();
                }
                else if (nametable == "Size")
                {
                    sizeTableAdapter.DeleteQuerySize(Convert.ToInt32(id));
                    DataGridDS.ItemsSource = sizeViewTableAdapter.GetData();
                }
                else if (nametable == "ShoeFactory")
                {
                    shoeFactoryTableAdapter.DeleteQueryShoeFactory(Convert.ToInt32(id));
                    DataGridDS.ItemsSource = shoeFactoryViewTableAdapter.GetData();
                }
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
    }
}
