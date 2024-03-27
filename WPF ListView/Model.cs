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
    internal class Model : INotifyPropertyChanged
    {
        public int Index { get => _index; set { _index = value; OnPropertyChanged(); } }
        public int State { get => _state; set { _state = value; OnPropertyChanged(); } }

        private int _index;
        private int _state;

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propName = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
    }
}
