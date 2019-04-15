using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Text;

namespace EcareMob.Helpers
{
    public class ObservableRangeCollection<T> : ObservableCollection<T>
    {
        public void AddRange(IEnumerable<T> collection)
        {
            foreach (var i in collection) Items.Add(i);
            OnCollectionChanged(
                new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
        }
    }
}
