using System.Collections.ObjectModel;
using System;
using System.ComponentModel;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Input;

namespace WPF_ListView
{
    internal class Model : INotifyPropertyChanged, ICommand
    {
        public string IP { get; set; }
        public string Ping { get; set; }
        public string DNS { get; set; }
        public string MAC { get; set; }
        public string Manufacturer { get; set; }

        private Brush _backgroundColor;
        public Brush BackgroundColor
        {
            get => _backgroundColor;
            set
            {
                _backgroundColor = value;
                OnPropertyChanged();
            }
        }

        private CollectionViewSource cvs = new CollectionViewSource();
        private ObservableCollection<Model> items = new ObservableCollection<Model>();

        public ICollectionView View { get => cvs.View; }

        public event EventHandler CanExecuteChanged;
        public bool CanExecute(object parameter) => true;
        public void Execute(object parameter)
        {
            switch (parameter.ToString())
            {
                case "ButtonAdd_Click":
                    for (int i = 0; i < 20; ++i)
                    {
                        Model model = new Model() { IP = "1.1.1.1", Ping = "30ms", DNS = "XYZ", MAC = "2F:3C:5F:41:F9", Manufacturer = "Intel" };
                        items.Add(model);
                    }
                    cvs.Source = items;
                    OnPropertyChanged(nameof(View));
                    break;

                case "ButtonColorize_Click":
                    RandomizeColors();
                    break;
            }
        }

        private void RandomizeColors()
        {
            Random random = new Random();
            foreach (Model model in items)
            {
                Type brushesType = typeof(Brushes);
                PropertyInfo[] properties = brushesType.GetProperties();
                int randomInt = random.Next(properties.Length);
                model.BackgroundColor = (Brush)properties[randomInt].GetValue(null, null);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propName = "") =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
    }
}
