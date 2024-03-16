using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Media;

namespace WPF_ListView
{
    internal class Model : INotifyPropertyChanged
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

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propName = "") =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
    }
}
