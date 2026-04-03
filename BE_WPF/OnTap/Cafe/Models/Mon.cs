using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cafe.ViewModels;

namespace Cafe.Models
{
    public class Mon : BaseViewModel
    {
        public string TenMon { get; set; }

        public decimal Gia { get; set; }  

        private bool _isSelected;
        public bool IsSelected
        {
            get => _isSelected;
            set
            {
                _isSelected = value;

                // ✔ FIX: dùng hàm đúng chuẩn MVVM
                OnPropertyChanged(nameof(IsSelected));
            }
        }
    }
}
