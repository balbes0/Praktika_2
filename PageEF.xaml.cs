﻿using Praktika_2.PraktikaodinDataSetTableAdapters;
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
        private PraktikaodinEntities praktikaodinEntity = new PraktikaodinEntities();
        string nametable = "";
        public PageEF(string selectedTable)
        {
            InitializeComponent();
            if (selectedTable != "")
            {
                nametable = selectedTable;
                if (selectedTable == "Color")
                {
                    DataGridEF.ItemsSource = praktikaodinEntity.Color.ToList();
                }
                else if (selectedTable == "Shoe")
                {
                    DataGridEF.ItemsSource = praktikaodinEntity.Shoe.ToList();
                }
                else if (selectedTable == "ShoeFactory")
                {
                    DataGridEF.ItemsSource = praktikaodinEntity.ShoeFactory.ToList();
                }
                else if (selectedTable == "Size")
                {
                    DataGridEF.ItemsSource = praktikaodinEntity.Size.ToList();
                }
                else if (selectedTable == "ShoeInventory")
                {
                    DataGridEF.ItemsSource = praktikaodinEntity.ShoeInventory.ToList();
                }
                else if (selectedTable == "ColorView")
                {
                    DataGridEF.ItemsSource = praktikaodinEntity.ColorView.ToList();
                }
                else if (selectedTable == "SizeView")
                {
                    DataGridEF.ItemsSource = praktikaodinEntity.SizeView.ToList();
                }
                else if (selectedTable == "ShoeView")
                {
                    DataGridEF.ItemsSource = praktikaodinEntity.ShoeView.ToList();
                }
                DataGridEF.Visibility = Visibility.Visible;
                LabelEF.Visibility = Visibility.Hidden;
            }
        }
        private void SaveChanges()
        {
            
        }
        public void AddNewData()
        {
            if (nametable == "Color")
            {
                Color color = new Color();
                color.Color1 = TextBoxEF.Text;
                praktikaodinEntity.Color.Add(color);
                praktikaodinEntity.SaveChanges();
                DataGridEF.ItemsSource = praktikaodinEntity.Color.ToList();
            }
            else if (nametable == "Shoe")
            {
                Shoe shoe = new Shoe();
                shoe.ShoeType = TextBoxEF.Text;
                praktikaodinEntity.Shoe.Add(shoe);
                praktikaodinEntity.SaveChanges();
                DataGridEF.ItemsSource = praktikaodinEntity.Shoe.ToList();
            }
            else if (nametable == "Size")
            {
                Size size = new Size();
                size.Size1 = Convert.ToInt32(TextBoxEF.Text);
                praktikaodinEntity.Size.Add(size);
                praktikaodinEntity.SaveChanges();
                DataGridEF.ItemsSource = praktikaodinEntity.Size.ToList();

            }
            else if (nametable == "ShoeFactory" || nametable == "ShoeInventory" || nametable == "ShoeView" || nametable == "SizeView" || nametable == "ColorView")
            {
                MessageBox.Show("Эту таблицу менять нельзя!");
            }
        }
        public void SaveChangesWithButton()
        {
            if (DataGridEF.SelectedItem != null)
            {
                if (nametable == "Color")
                {
                    var selected = DataGridEF.SelectedItem as Color;
                    TextBoxEF.Text = selected.Color1;
                    selected.Color1 = TextBoxEF.Text;
                    praktikaodinEntity.SaveChanges();
                    DataGridEF.ItemsSource = praktikaodinEntity.Color.ToList();
                }
                else if (nametable == "Shoe")
                {
                    var selected = DataGridEF.SelectedItem as Shoe;
                    TextBoxEF.Text = selected.ShoeType;
                    selected.ShoeType = TextBoxEF.Text;
                    praktikaodinEntity.SaveChanges();
                    DataGridEF.ItemsSource = praktikaodinEntity.Shoe.ToList();
                }
                else if (nametable == "Size")
                {
                    var selected = DataGridEF.SelectedItem as Size;
                    TextBoxEF.Text = selected.Size1.ToString();
                    selected.Size1 = Convert.ToInt32(TextBoxEF.Text);
                    praktikaodinEntity.SaveChanges();
                    DataGridEF.ItemsSource = praktikaodinEntity.Color.ToList();
                }
                else if (nametable == "ShoeFactory" || nametable == "ShoeInventory" || nametable == "ShoeView" || nametable == "SizeView" || nametable == "ColorView")
                {
                    MessageBox.Show("Эту таблицу менять нельзя!");
                }
            }
            praktikaodinEntity.SaveChanges();
        }
        public void DeleteWithButton()
        {
            if (DataGridEF.SelectedItem != null)
            {
                if (nametable == "Color")
                {
                    praktikaodinEntity.Color.Remove(DataGridEF.SelectedItem as Color);
                    praktikaodinEntity.SaveChanges();
                    DataGridEF.ItemsSource = praktikaodinEntity.Color.ToList();
                }
                else if (nametable == "Shoe")
                {
                    praktikaodinEntity.Shoe.Remove(DataGridEF.SelectedItem as Shoe);
                    praktikaodinEntity.SaveChanges();
                    DataGridEF.ItemsSource = praktikaodinEntity.Shoe.ToList();
                }
                else if (nametable == "Size")
                {
                    praktikaodinEntity.Size.Remove(DataGridEF.SelectedItem as Size);
                    praktikaodinEntity.SaveChanges();
                    DataGridEF.ItemsSource = praktikaodinEntity.Size.ToList();
                }
                else if (nametable == "ShoeFactory" || nametable == "ShoeInventory" || nametable == "ShoeView" || nametable == "SizeView" || nametable == "ColorView")
                {
                    MessageBox.Show("Эту таблицу менять нельзя!");
                }
            }
        }
    }
}