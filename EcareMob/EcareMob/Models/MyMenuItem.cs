using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using Xamarin.Forms;

namespace EcareMob.Models
{
    public class MyMenuItem : INotifyPropertyChanged
    {
            public string Title { get; set; }
            public string Icon { get; set; }
            public string Uri { get; set; }
            public Type TargetType { get; set; }
            public MenuType MenuType { get; set; }

            private Color _fColor;
            public Color FColor
            {
                get { return _fColor; }
                set
                {
                    if (value.Equals(_fColor)) return;
                    _fColor = value;
                    OnPropertyChanged(nameof(FColor));
                }
            }


            public event PropertyChangedEventHandler PropertyChanged;

            protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public enum MenuType
        {
            Contact,
            Settings,
            Profile,
            Contracts,
            Logout
        }
}
