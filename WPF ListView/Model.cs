using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace WPF_ListView
{
    internal class Model : INotifyPropertyChanged
    {
        public int FieldA { get => _fieldA; set { _fieldA = value; OnPropertyChanged(); } }
        public int FieldB { get => _fieldB; set { _fieldB = value; OnPropertyChanged(); } }

        private int _fieldA;
        private int _fieldB;

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propName = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
    }
}
