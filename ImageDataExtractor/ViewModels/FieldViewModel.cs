using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;

namespace ImageDataExtractor.ViewModels
{
    public class FieldViewModel : INotifyPropertyChanged
    {
        private string _label;
        private UIElement _fieldTypeInputControl;
        private bool? _isRequired;

        public event PropertyChangedEventHandler PropertyChanged;

        public string Label
        {
            get { return _label; }
            set
            {
                if (_label != value)
                {
                    _label = value;
                    OnPropertyChanged();
                }
            }
        }

        public UIElement FieldTypeInputControl
        {
            get { return _fieldTypeInputControl; }
            set
            {
                if (_fieldTypeInputControl != value)
                {
                    _fieldTypeInputControl = value;
                    OnPropertyChanged();
                }
            }
        }

        public bool? IsRequired
        {
            get { return _isRequired; }
            set
            {
                if (_isRequired != value)
                {
                    _isRequired = value;
                    OnPropertyChanged();
                }
            }
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
