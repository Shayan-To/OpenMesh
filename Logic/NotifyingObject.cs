using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace OpenMesh
{

    public abstract class NotifyingObject : INotifyPropertyChanged
    {

        #region PropertyChanged Event
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, e);
            }
        }

        protected void NotifyPropertyChanged(String PropertyName)
        {
            this.OnPropertyChanged(new PropertyChangedEventArgs(PropertyName));
        }
        #endregion

    }

}
