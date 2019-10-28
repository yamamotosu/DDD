using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DDD.WinForm.ViewModels
{
    public abstract class ViewModelBase : INotifyPropertyChanged            //ViewModelBaseはINotifyPropertyChangedを継承している
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected bool SetProperty<T>(ref T field,
            T value, [CallerMemberName]string propertyName = null)
        {
            if (Equals(field, value))
            {
                return false;
            }

            field = value;
            var h = this.PropertyChanged;
            if (h != null)
            {
                h(this, new PropertyChangedEventArgs(propertyName));
            }

            return true;
        }

        public virtual DateTime GetDateTime()       //派生クラスでオーバーライドできるvirtualを使った日時型の情報を得るメソッド
        {
            return DateTime.Now;
        }
    }
}
