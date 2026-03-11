using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace Bai2_5.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        // Sự kiện PropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            // Kiểm tra xem có handler nào đăng ký chưa
            if (PropertyChanged != null)
            {
                // Tạo đối tượng EventArgs với tên property
                PropertyChangedEventArgs args =
                    new PropertyChangedEventArgs(propertyName);
                // Gọi tất cả các handler đã đăng ký
                PropertyChanged(this, args);
            }
        }
    }
}

