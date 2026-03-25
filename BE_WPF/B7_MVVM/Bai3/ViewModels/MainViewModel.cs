using Bai3.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Bai3.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private BaseViewModel _currentViewModel;

        public BaseViewModel CurrentViewModel
        {
            get => _currentViewModel;
            set
            {
                _currentViewModel = value;
                OnPropertyChanged();
            }
        }

        public ThanhToanViewModel ThanhToanVM { get; }
        public ThongKeViewModel ThongKeVM { get; }

        public ICommand MoThanhToanCommand { get; }
        public ICommand MoThongKeCommand { get; }

        public MainViewModel()
        {
            var service = new QuanLyThanhToanService();

            ThanhToanVM = new ThanhToanViewModel(service);
            ThongKeVM = new ThongKeViewModel(service);

            _currentViewModel = ThanhToanVM;

            MoThanhToanCommand = new RelayCommand(_ => CurrentViewModel = ThanhToanVM);
            MoThongKeCommand = new RelayCommand(_ => CurrentViewModel = ThongKeVM);
        }
    }
}
