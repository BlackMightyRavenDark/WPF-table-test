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
        private GridViewColumnHeader _lastHeaderClicked = null;
        private ListSortDirection _lastDirection = ListSortDirection.Ascending;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            listView.ItemsSource = items;
            //listView.Items.GroupDescriptions.Add(new PropertyGroupDescription("State"));

            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(listView.ItemsSource);
            PropertyGroupDescription groupDescription = new PropertyGroupDescription("State");
            view.GroupDescriptions.Add(groupDescription);

            DispatcherTimer timer = new DispatcherTimer();
            timer.Tick += (s, a) =>
            {
                if (items.Count > 0)
                {
                    Random random = new Random();
                    items[random.Next(items.Count)].State = random.Next(2);
                }
            };
            timer.Interval = TimeSpan.FromMilliseconds(500);
            timer.Start();
        }

        private void GridViewColumnHeaderClickedHandler(object sender, RoutedEventArgs e)
        {
            var headerClicked = e.OriginalSource as GridViewColumnHeader;
            ListSortDirection direction;

            if (headerClicked != null)
            {
                if (headerClicked.Role != GridViewColumnHeaderRole.Padding)
                {
                    if (headerClicked != _lastHeaderClicked)
                    {
                        direction = ListSortDirection.Ascending;
                    }
                    else
                    {
                        if (_lastDirection == ListSortDirection.Ascending)
                        {
                            direction = ListSortDirection.Descending;
                        }
                        else
                        {
                            direction = ListSortDirection.Ascending;
                        }
                    }

                    var columnBinding = headerClicked.Column.DisplayMemberBinding as Binding;
                    string sortBy = columnBinding?.Path.Path ?? headerClicked.Column.Header as string;
                    if (sortBy.Contains("/")) { sortBy = "Name"; }

                    Sort(sortBy, direction);

                    _lastHeaderClicked = headerClicked;
                    _lastDirection = direction;
                }
            }
        }

        private void Sort(string sortBy, ListSortDirection direction)
        {
            ICollectionView dataView =
              CollectionViewSource.GetDefaultView(listView.ItemsSource);

            dataView.SortDescriptions.Clear();
            SortDescription sd = new SortDescription(sortBy, direction);
            dataView.SortDescriptions.Add(sd);
            dataView.Refresh();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            int c = items.Count;
            Random random = new Random();
            for (int i = 0; i < 200; ++i)
            {
                Model model = new Model() { Index = i + c, State = random.Next(2) };
                items.Add(model);
            }

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
        }
    }
}
