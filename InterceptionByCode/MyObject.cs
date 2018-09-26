using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using InterceptionByCode.Annotations;

namespace InterceptionByCode
{
    public class MyObject : MarshalByRefObject
    {
        public String Name { get; set; }
    }

    public class NotifyPropertyChangedBeavior : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
