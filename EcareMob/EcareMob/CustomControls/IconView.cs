using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace EcareMob.CustomControls
{
    public class IconView:View
    {
        public static readonly BindableProperty ForegroundProperty = BindableProperty.Create<IconView, Color>(p => p.Foreground, default(Color));

        public Color Foreground
        {
            get
            {
                return (Color)GetValue(ForegroundProperty);
            }
            set
            {
                SetValue(ForegroundProperty, value);
            }
        }



        public static readonly BindableProperty SourceProperty = BindableProperty.Create<IconView, string>(p => p.Source, default(string));

        public string Source
        {
            get
            {
                return (string)GetValue(SourceProperty);
            }
            set
            {
                SetValue(SourceProperty, value);
            }
        }

    }
}
