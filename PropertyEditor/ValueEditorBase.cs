using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using System.ComponentModel;

namespace OpenMesh
{

    public class ValueEditorBase<T> : UserControl
    {

        public ValueEditorBase()
        {
        }

        public ValueEditorBase(Object EditingObject, PropertyInfo Property)
        {
            if (!typeof(T).IsAssignableFrom(Property.PropertyType))
                throw new ArgumentException("Property type mismatch.");
            if (!Property.CanRead | !Property.CanWrite)
                throw new ArgumentException("Property must be read-write.");
            this._Property = Property;
            this._PropertyName = Property.Name;
            this._EditingObject = EditingObject;
        }

        private void OnObjectPropertChanged(Object Sender, PropertyChangedEventArgs E)
        {
            if (E.PropertyName == this._PropertyName)
                this.OnPropertyChanged();
        }

        protected virtual void OnPropertyChanged()
        {
        }

        #region EditingObject Property
        private readonly Object _EditingObject;

        public Object EditingObject
        {
            get
            {
                return this._EditingObject;
            }
        }
        #endregion

        #region Property Property
        private readonly PropertyInfo _Property;

        public PropertyInfo Property
        {
            get
            {
                return this._Property;
            }
        }
        #endregion

        #region PropertyName Property
        public readonly String _PropertyName;

        public virtual String PropertyName
        {
            get
            {
                return this._PropertyName;
            }
        }
        #endregion

        #region PropertyValue Property
        protected T PropertyValue
        {
            get
            {
                return (T)this._Property.GetValue(this._EditingObject, EmptyObjArr);
            }
            set
            {
                this._Property.SetValue(this._EditingObject, value, EmptyObjArr);
            }
        }
        #endregion

        private static readonly Object[] EmptyObjArr = new Object[0];

    }

}
