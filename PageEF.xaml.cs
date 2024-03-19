using System.Data;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using static MaterialDesignThemes.Wpf.Theme;
using System.Runtime.CompilerServices;
using System.Windows.Media.TextFormatting;

namespace Praktika_2
{
    public partial class PageEF : Page
    {
        private Praktika1Entities praktika1Entity = new Praktika1Entities();
        int idShoeType;
        int idSize;
        int idColor;
        string nametable = "";
        public PageEF(string selectedTable)
        {
            InitializeComponent();
            if (selectedTable != "")
            {
                GridTextBoxEF.Visibility = Visibility.Visible;
                DataGridEF.Visibility = Visibility.Visible;
                LabelEF.Visibility = Visibility.Hidden;
                nametable = selectedTable;
                if (selectedTable == "Color")
                {
                    DataGridEF.ItemsSource = praktika1Entity.ColorView.ToList();
                }
                else if (selectedTable == "Shoe")
                {
                    DataGridEF.ItemsSource = praktika1Entity.ShoeView.ToList();
                }
                else if (selectedTable == "ShoeFactory")
                {
                    DataGridEF.ItemsSource = praktika1Entity.ShoeFactoryView.ToList();
                    SizeIDCMBX.ItemsSource = praktika1Entity.Size.ToList();
                    SizeIDCMBX.DisplayMemberPath = "Size1";
                    ColorIDCMBX.ItemsSource = praktika1Entity.Color.ToList();
                    ColorIDCMBX.DisplayMemberPath = "Color1";
                    ShoeTypeIDCMBX.ItemsSource = praktika1Entity.Shoe.ToList();
                    ShoeTypeIDCMBX.DisplayMemberPath = "ShoeType";
                    GridTextBoxEF.Visibility = Visibility.Hidden;
                    GridComboBoxesEF.Visibility = Visibility.Visible;
                }
                else if (selectedTable == "Size")
                {
                    DataGridEF.ItemsSource = praktika1Entity.SizeView.ToList();
                }
            }
        }
        public void AddNewData()
        {
            if (nametable == "Color")
            {
                Color color = new Color();
                color.Color1 = TextBoxEF.Text;
                praktika1Entity.Color.Add(color);
                praktika1Entity.SaveChanges();
                DataGridEF.ItemsSource = praktika1Entity.ColorView.ToList();
            }
            else if (nametable == "Shoe")
            {
                Shoe shoe = new Shoe();
                shoe.ShoeType = TextBoxEF.Text;
                praktika1Entity.Shoe.Add(shoe);
                praktika1Entity.SaveChanges();
                DataGridEF.ItemsSource = praktika1Entity.ShoeView.ToList();
            }
            else if (nametable == "Size")
            {
                Size size = new Size();
                size.Size1 = Convert.ToInt32(TextBoxEF.Text);
                praktika1Entity.Size.Add(size);
                praktika1Entity.SaveChanges();
                DataGridEF.ItemsSource = praktika1Entity.SizeView.ToList();
            }
            else if (nametable == "ShoeFactory")
            {
                ShoeFactory shoeFactory = new ShoeFactory();
                shoeFactory.ShoeType_ID = (idShoeType);
                shoeFactory.Size_ID = (idSize);
                shoeFactory.Color_ID = (idColor);
                shoeFactory.Price = (Convert.ToInt32(PriceTBX.Text));
                praktika1Entity.ShoeFactory.Add(shoeFactory);
                praktika1Entity.SaveChanges();
                DataGridEF.ItemsSource = praktika1Entity.ShoeFactoryView.ToList();
            }
        }
        public void SaveChangesWithButton()
        {
            if (DataGridEF.SelectedItem != null)
            {
                if (nametable == "Color")
                {
                    var selected = DataGridEF.SelectedItem as ColorView;
                    Color colorToSave = praktika1Entity.Color.FirstOrDefault(size => size.ID_Color == selected.Номер__цвета);
                    colorToSave.Color1 = TextBoxEF.Text;
                    praktika1Entity.SaveChanges();
                    DataGridEF.ItemsSource = praktika1Entity.ColorView.ToList();
                }
                else if (nametable == "Shoe")
                {
                    var selected = DataGridEF.SelectedItem as ShoeView;
                    Shoe shoeToSave = praktika1Entity.Shoe.FirstOrDefault(s => s.ID_ShoeType == selected.Номер__обуви);
                    shoeToSave.ShoeType = TextBoxEF.Text;
                    praktika1Entity.SaveChanges();
                    DataGridEF.ItemsSource = praktika1Entity.ShoeView.ToList();
                }
                else if (nametable == "Size")
                {
                    var selected = DataGridEF.SelectedItem as SizeView;
                    Size sizeToSave = praktika1Entity.Size.FirstOrDefault(size => size.ID_Size == selected.Номер__размера);
                    sizeToSave.Size1 = Convert.ToInt32(TextBoxEF.Text);
                    praktika1Entity.SaveChanges();
                    DataGridEF.ItemsSource = praktika1Entity.ColorView.ToList();
                }
                else if (nametable == "ShoeFactory")
                {
                    var selected = DataGridEF.SelectedItem as ShoeFactoryView;
                    ShoeFactory shoeFactoryToSave = praktika1Entity.ShoeFactory.FirstOrDefault(sf => sf.ID_Product == selected.Номер__продукта);
                    shoeFactoryToSave.ShoeType_ID = (idShoeType);
                    shoeFactoryToSave.Size_ID = (idSize);
                    shoeFactoryToSave.Color_ID = (idColor);
                    shoeFactoryToSave.Price = Convert.ToInt32(PriceTBX.Text);
                    praktika1Entity.SaveChanges();
                    DataGridEF.ItemsSource = praktika1Entity.ShoeFactoryView.ToList();
                }
            }
            praktika1Entity.SaveChanges();
        }
        public void DeleteWithButton()
        {
            if (nametable == "Color")
            {
                ColorView selectedItem = DataGridEF.SelectedItem as ColorView;
                Color colorToDelete = praktika1Entity.Color.FirstOrDefault(color => color.ID_Color == selectedItem.Номер__цвета);
                praktika1Entity.Color.Remove(colorToDelete);
                praktika1Entity.SaveChanges();
                DataGridEF.ItemsSource = praktika1Entity.ColorView.ToList();
            }
            else if (nametable == "Shoe")
            {
                ShoeView selectedItem = DataGridEF.SelectedItem as ShoeView;
                Shoe shoeToDelete = praktika1Entity.Shoe.FirstOrDefault(shoe => shoe.ID_ShoeType == selectedItem.Номер__обуви);
                praktika1Entity.Shoe.Remove(shoeToDelete);
                praktika1Entity.SaveChanges();
                DataGridEF.ItemsSource = praktika1Entity.ShoeView.ToList();
            }
            else if (nametable == "Size")
            {
                SizeView selectedItem = DataGridEF.SelectedItem as SizeView;
                Size sizeToDelete = praktika1Entity.Size.FirstOrDefault(size => size.ID_Size == selectedItem.Номер__размера);
                praktika1Entity.Size.Remove(sizeToDelete);
                praktika1Entity.SaveChanges();
                DataGridEF.ItemsSource = praktika1Entity.SizeView.ToList();
            }
            else if (nametable == "ShoeFactory")
            {
                ShoeFactoryView selectedItem = DataGridEF.SelectedItem as ShoeFactoryView;
                ShoeFactory shoeFactoryToDelete = praktika1Entity.ShoeFactory.FirstOrDefault(sf => sf.ID_Product == selectedItem.Номер__продукта);
                praktika1Entity.ShoeFactory.Remove(shoeFactoryToDelete);
                praktika1Entity.SaveChanges();
                DataGridEF.ItemsSource = praktika1Entity.ShoeFactoryView.ToList();
            }
        }

        private void ShoeTypeIDCMBX_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedItem = ShoeTypeIDCMBX.SelectedItem as Shoe;
            idShoeType = praktika1Entity.Shoe.Where(s => s.ShoeType == selectedItem.ShoeType).Select(s => s.ID_ShoeType).FirstOrDefault();
        }

        private void SizeIDCMBX_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedItem = SizeIDCMBX.SelectedItem as Size;
            idSize = praktika1Entity.Size.Where(size => size.Size1 == selectedItem.Size1).Select(size => size.ID_Size).FirstOrDefault();
        }

        private void ColorIDCMBX_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedItem = ColorIDCMBX.SelectedItem as Color;
            idColor = praktika1Entity.Color.Where(color => color.Color1 == selectedItem.Color1).Select(color => color.ID_Color).FirstOrDefault();
        }
    }
}
