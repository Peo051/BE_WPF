using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using System.Globalization;
using System.Text.RegularExpressions;
using Hotel.Models;
using System.Collections.ObjectModel;

namespace Hotel.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        public string Name { get; set; }
        public string Address { get; set; }

        // ===== SỐ NGÀY =====
        private string _days;
        public string Days
        {
            get => _days;
            set
            {
                if (Regex.IsMatch(value ?? "", @"^\d*$"))
                {
                    _days = value;
                    OnPropertyChanged(nameof(Days));
                }
            }
        }

        public int DaysValue => int.TryParse(Days, out int d) ? d : 0;

        // ===== ROOM =====
        public ObservableCollection<RoomItem> Rooms { get; set; }

        // ===== DATA =====
        public ObservableCollection<ServiceItem> Amenities { get; set; }
        public ObservableCollection<ServiceItem> Services { get; set; }

        // ===== OUTPUT =====
        private double _total;
        public string Total => FormatMoney(_total);

        private int _totalCustomer;
        public int TotalCustomer
        {
            get => _totalCustomer;
            set { _totalCustomer = value; OnPropertyChanged(nameof(TotalCustomer)); }
        }

        private double _totalRevenue;
        public string TotalRevenue => FormatMoney(_totalRevenue);

        //Trường hợp không dùng Private
        //public double TotalRevenue
        //{
        //    get
        //    {
        //        return DanhSachHoaDon.Sum(x => x.TongTien);
        //    }
        //}

        public ICommand CalculateCommand { get; set; }
        public ICommand NewCommand { get; set; }
        public ICommand ExitCommand { get; set; }

        public MainViewModel()
        {
            // ===== ROOM =====
            Rooms = new ObservableCollection<RoomItem>
            {
                new RoomItem { Name = "Phòng đơn", Price = 300000, IsSelected = true },
                new RoomItem { Name = "Phòng đôi", Price = 350000 },
                new RoomItem { Name = "Phòng ba", Price = 400000 }
            };

            //// ✔ đảm bảo DÙ UI hay CODE → luôn chỉ có 1 phòng được chọn
            foreach (var room in Rooms) ////gắn “listener” cho từng room → khi isselected thay đổi → mình biết ngay
            {
                room.PropertyChanged += (s, e) => //khi event xảy ra thì chạy đoạn code này
                {
                    if (e.PropertyName == nameof(RoomItem.IsSelected) && room.IsSelected)  //khi room này vừa được chọn
                    {
                        foreach (var r in Rooms)   //tắt tất cả room khác
                        {
                            if (r != room) r.IsSelected = false;
                        }
                    }
                };
            }

            // ===== TIỆN NGHI =====
            Amenities = new ObservableCollection<ServiceItem>
            {
                new ServiceItem { Name="Tivi", Price=10000},
                new ServiceItem { Name="Internet", Price=10000 },
                new ServiceItem { Name="Máy nước nóng", Price=10000}
            };

            // ===== DỊCH VỤ =====
            Services = new ObservableCollection<ServiceItem>
            {
                new ServiceItem { Name="Karaoke", Price=50000},
                new ServiceItem { Name="Ăn sáng", Price=15000}
            };

            CalculateCommand = new RelayCommand(ExecuteCalculate);
            NewCommand = new RelayCommand(ExecuteNew);
            ExitCommand = new RelayCommand(ExecuteExit);
        }

        private void ExecuteCalculate(object obj)
        {
            if (DaysValue <= 0)
            {
                MessageBox.Show("Số ngày phải > 0");
                return;
            }

            var selectedRoom = Rooms.FirstOrDefault(x => x.IsSelected); //LINQ
            double roomPrice = selectedRoom?.Price ?? 0;

            double total = roomPrice * DaysValue;

            // tiện nghi
            //LINQ = viết code để xử lý danh sách ngắn gọn
            total += Amenities.Where(x => x.IsSelected)
                              .Sum(x => x.Price * DaysValue);

            // dịch vụ
            total += Services.Where(x => x.IsSelected)
                             .Sum(x => x.Price * DaysValue);

            //double serviceTotal = 0;

            //foreach (var item in Services)
            //{
            //    if (item.IsSelected)
            //    {
            //        if (item.IsPerDay)
            //            serviceTotal += item.Price * DaysValue;
            //        else
            //            serviceTotal += item.Price;
            //    }
            //}

            //total += serviceTotal;

            _total = total;
            OnPropertyChanged(nameof(Total));

            TotalCustomer++;
            _totalRevenue += total;
            OnPropertyChanged(nameof(TotalRevenue));
        }

        private void ExecuteNew(object obj)
        {
            Name = "";
            Address = "";
            Days = "";

            // reset phòng
            foreach (var room in Rooms)
                room.IsSelected = false;

            Rooms.First().IsSelected = true;

            // reset checkbox
            foreach (var item in Amenities) item.IsSelected = false;
            foreach (var item in Services) item.IsSelected = false;

            _total = 0;

            OnPropertyChanged("");
        }

        private void ExecuteExit(object obj)
        {
            Application.Current.Shutdown();
        }

        private string FormatMoney(double money)
        {
            return money.ToString("N0", CultureInfo.GetCultureInfo("vi-VN")) + " VNĐ";
        }
    }

    //public class MainViewModel : BaseViewModel
    //{
    //    public string Name { get; set; }
    //    public string Address { get; set; }

    //    private string _days;
    //    public string Days
    //    {
    //        get => _days;
    //        set
    //        {
    //            if (Regex.IsMatch(value ?? "", @"^\d*$"))
    //            {
    //                _days = value;
    //                OnPropertyChanged(nameof(Days));
    //            }
    //        }
    //    }

    //    public int DaysValue => int.TryParse(Days, out int d) ? d : 0;

    //    // ===== ROOM =====
    //    public string SelectedRoom { get; set; } = "Phòng đơn";
    //    public ICommand SelectRoomCommand { get; set; }

    //    // ===== DATA =====
    //    public ObservableCollection<ServiceItem> Amenities { get; set; }
    //    public ObservableCollection<ServiceItem> Services { get; set; }

    //    // ===== OUTPUT =====
    //    private double _total;
    //    public string Total => FormatMoney(_total);

    //    private int _totalCustomer;
    //    public int TotalCustomer
    //    {
    //        get => _totalCustomer;
    //        set { _totalCustomer = value; OnPropertyChanged(nameof(TotalCustomer)); }
    //    }

    //    private double _totalRevenue;
    //    public string TotalRevenue => FormatMoney(_totalRevenue);

    //    public ICommand CalculateCommand { get; set; }
    //    public ICommand NewCommand { get; set; }
    //    public ICommand ExitCommand { get; set; }

    //    public MainViewModel()
    //    {
    //        //Loại phòng
    //        Rooms = new ObservableCollection<RoomItem>
    //        {
    //            new RoomItem { Name = "Phòng đơn", Price = 300000, IsSelected = true },
    //            new RoomItem { Name = "Phòng đôi", Price = 350000 },
    //            new RoomItem { Name = "Phòng ba", Price = 400000 }
    //        };
    //        // ✔ tiện nghi (theo ngày)
    //        Amenities = new ObservableCollection<ServiceItem>
    //        {
    //            new ServiceItem { Name="Tivi", Price=10000},
    //            new ServiceItem { Name="Internet", Price=10000 },
    //            new ServiceItem { Name="Máy nước nóng", Price=10000}
    //        };

    //        // ✔ dịch vụ (theo ngày)
    //        Services = new ObservableCollection<ServiceItem>
    //        {
    //            new ServiceItem { Name="Karaoke", Price=50000}, 
    //            new ServiceItem { Name="Ăn sáng", Price=15000}
    //        };

    //        CalculateCommand = new RelayCommand(ExecuteCalculate);
    //        NewCommand = new RelayCommand(ExecuteNew);
    //        ExitCommand = new RelayCommand(ExecuteExit);
    //        SelectRoomCommand = new RelayCommand(SelectRoom);
    //    }

    //    void SelectRoom(object obj)
    //    {
    //        SelectedRoom = obj.ToString();
    //    }

    //    private void ExecuteCalculate(object obj)
    //    {
    //        if (DaysValue <= 0)
    //        {
    //            MessageBox.Show("Số ngày phải > 0");
    //            return;
    //        }

    //        double roomPrice = SelectedRoom switch
    //        {
    //            "Phòng đơn" => 300000,
    //            "Phòng đôi" => 350000,
    //            "Phòng ba" => 400000,
    //            _ => 0 //_: "bất kỳ giá trị nào không khớp phía trên"
    //        };

    //        double total = roomPrice * DaysValue;
    //        //LINQ = viết code để xử lý danh sách ngắn gọn
    //        // tiện nghi
    //        total += Amenities.Where(x => x.IsSelected)
    //                          .Sum(x => x.Price * DaysValue);

    //        // Dịch vụ
    //        total += Services.Where(x => x.IsSelected)
    //                         .Sum(x => x.Price * DaysValue);

    //        //double serviceTotal = 0;

    //        //foreach (var item in Services)
    //        //{
    //        //    if (item.IsSelected)
    //        //    {
    //        //        if (item.IsPerDay)
    //        //            serviceTotal += item.Price * DaysValue;
    //        //        else
    //        //            serviceTotal += item.Price;
    //        //    }
    //        //}

    //        //total += serviceTotal;

    //        _total = total;
    //        OnPropertyChanged(nameof(Total));

    //        TotalCustomer++;
    //        _totalRevenue += total;
    //        OnPropertyChanged(nameof(TotalRevenue));
    //    }

    //    private void ExecuteNew(object obj)
    //    {
    //        Name = "";
    //        Address = "";
    //        Days = "";

    //        SelectedRoom = "Phòng đơn";

    //        foreach (var item in Amenities) item.IsSelected = false;
    //        foreach (var item in Services) item.IsSelected = false;

    //        _total = 0;

    //        OnPropertyChanged("");
    //    }

    //    private void ExecuteExit(object obj)
    //    {
    //        Application.Current.Shutdown();
    //    }

    //    private string FormatMoney(double money)
    //    {
    //        return money.ToString("N0", CultureInfo.GetCultureInfo("vi-VN")) + " VNĐ";
    //    }
    //}

    //public class MainViewModel : BaseViewModel
    //{
    //    public string Name { get; set; }
    //    public string Address { get; set; }

    //    // ✅ VALIDATE SỐ NGÀY
    //    private string _days;
    //    public string Days
    //    {
    //        get => _days;
    //        set
    //        {
    //            // chỉ cho nhập số
    //            if (Regex.IsMatch(value ?? "", @"^\d*$"))
    //            {
    //                _days = value;
    //                OnPropertyChanged(nameof(Days));
    //            }
    //        }
    //    }

    //    public int DaysValue => int.TryParse(Days, out int d) ? d : 0;

    //    // RADIO
    //    public bool IsSingle { get; set; } = true;
    //    public bool IsDouble { get; set; }
    //    public bool IsTriple { get; set; }

    //    public bool Tivi { get; set; }
    //    public bool Internet { get; set; }
    //    public bool HotWater { get; set; }

    //    public bool Karaoke { get; set; }
    //    public bool Breakfast { get; set; }

    //    // OUTPUT FORMAT TIỀN
    //    private double _total;
    //    public string Total => FormatMoney(_total);

    //    private int _totalCustomer;
    //    public int TotalCustomer
    //    {
    //        get => _totalCustomer;
    //        set { _totalCustomer = value; OnPropertyChanged(nameof(TotalCustomer)); }
    //    }

    //    private double _totalRevenue;
    //    public string TotalRevenue => FormatMoney(_totalRevenue);

    //    // COMMAND
    //    public ICommand CalculateCommand { get; set; }
    //    public ICommand NewCommand { get; set; }
    //    public ICommand ExitCommand { get; set; }

    //    public MainViewModel()
    //    {
    //        CalculateCommand = new RelayCommand(ExecuteCalculate);
    //        NewCommand = new RelayCommand(ExecuteNew);
    //        ExitCommand = new RelayCommand(ExecuteExit);
    //    }

    //    private void ExecuteCalculate(object obj)
    //    {
    //        if (DaysValue <= 0)
    //        {
    //            MessageBox.Show("Số ngày phải > 0");
    //            return;
    //        }

    //        double roomPrice = 0;

    //        if (IsSingle) roomPrice = 300000;
    //        else if (IsDouble) roomPrice = 350000;
    //        else if (IsTriple) roomPrice = 400000;

    //        double total = roomPrice * DaysValue;

    //        int tiệnNghi = (Tivi ? 1 : 0) + (Internet ? 1 : 0) + (HotWater ? 1 : 0);
    //        total += tiệnNghi * 10000 * DaysValue;

    //        if (Karaoke) total += 50000;
    //        if (Breakfast) total += 15000 * DaysValue;

    //        _total = total;
    //        OnPropertyChanged(nameof(Total));

    //        TotalCustomer++;
    //        _totalRevenue += total;
    //        OnPropertyChanged(nameof(TotalRevenue));
    //    }

    //    private void ExecuteNew(object obj)
    //    {
    //        Name = "";
    //        Address = "";
    //        Days = "";

    //        IsSingle = true;
    //        IsDouble = IsTriple = false;

    //        Tivi = Internet = HotWater = false;
    //        Karaoke = Breakfast = false;

    //        _total = 0;
    //        OnPropertyChanged(nameof(Total));
    //        OnPropertyChanged("");
    //    }

    //    private void ExecuteExit(object obj)
    //    {
    //        Application.Current.Shutdown();
    //    }

    //    // FORMAT TIỀN VNĐ
    //    private string FormatMoney(double money)
    //    {
    //        return money.ToString("N0", CultureInfo.GetCultureInfo("vi-VN")) + " VNĐ";
    //    }
    //}
}
