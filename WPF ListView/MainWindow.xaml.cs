using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Threading;

namespace WPF_ListView
{
    public partial class MainWindow : Window
    {
        private ObservableCollection<Model> items = new ObservableCollection<Model>();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            listView.ItemsSource = items;
            listView.Items.GroupDescriptions.Clear();
            listView.Items.GroupDescriptions.Add(new PropertyGroupDescription("FieldB"));

            AddItems(600);
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            AddItems(100);
        }

        private void AddItems(int count)
        {
            int c = items.Count;
            Random random = new Random();
            for (int i = 0; i < count; ++i)
            {
                Model model = new Model() { FieldA = i + c, FieldB = random.Next(2) };
                items.Add(model);
            }
        }
    }
}
