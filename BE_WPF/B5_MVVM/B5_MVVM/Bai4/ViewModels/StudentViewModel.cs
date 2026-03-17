using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using Bai4.Models;

namespace Bai4.ViewModels;

public class StudentViewModel : BaseViewModel
{
    private readonly ObservableCollection<Student> _students;
    private readonly RelayCommand _deleteStudentCommand;
    private readonly RelayCommand _addStudentCommand;
    private bool _isAscending = true;
    private Student? _selectedStudent;
    private string _newName = string.Empty;
    private string _newAge = string.Empty;
    private string _searchText = string.Empty;

    public StudentViewModel()
    {
        _students = new ObservableCollection<Student>
        {
            new Student { Name = "An", Age = 20 },
            new Student { Name = "Binh", Age = 18 },
            new Student { Name = "Chi", Age = 19 }
        };

        _students.CollectionChanged += Students_CollectionChanged;

        StudentsView = CollectionViewSource.GetDefaultView(_students);
        StudentsView.Filter = FilterStudents;
        StudentsView.SortDescriptions.Add(
            new SortDescription(nameof(Student.Age), ListSortDirection.Ascending));

        _addStudentCommand = new RelayCommand(_ => AddStudent());
        _deleteStudentCommand = new RelayCommand(_ => DeleteStudent(), _ => SelectedStudent is not null);

        AddStudentCommand = _addStudentCommand;
        DeleteStudentCommand = _deleteStudentCommand;
        ClearInputCommand = new RelayCommand(_ => ClearInput());
        ToggleSortByAgeCommand = new RelayCommand(_ => ToggleSortByAge());
    }

    public ICollectionView StudentsView { get; }

    public ICommand AddStudentCommand { get; }

    public ICommand DeleteStudentCommand { get; }

    public ICommand ClearInputCommand { get; }

    public ICommand ToggleSortByAgeCommand { get; }

    public int StudentCount => _students.Count;

    public Student? SelectedStudent
    {
        get => _selectedStudent;
        set
        {
            if (_selectedStudent == value)
            {
                return;
            }

            _selectedStudent = value;
            OnPropertyChanged();
            _deleteStudentCommand.RaiseCanExecuteChanged();

            if (_selectedStudent is not null)
            {
                NewName = _selectedStudent.Name;
                NewAge = _selectedStudent.Age.ToString();
            }
        }
    }

    public string NewName
    {
        get => _newName;
        set
        {
            if (_newName == value)
            {
                return;
            }

            _newName = value;
            OnPropertyChanged();
        }
    }

    public string NewAge
    {
        get => _newAge;
        set
        {
            if (_newAge == value)
            {
                return;
            }

            _newAge = value;
            OnPropertyChanged();
        }
    }

    public string SearchText
    {
        get => _searchText;
        set
        {
            if (_searchText == value)
            {
                return;
            }

            _searchText = value;
            OnPropertyChanged();
            StudentsView.Refresh();
        }
    }

    private void Students_CollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
    {
        OnPropertyChanged(nameof(StudentCount));
    }

    private bool FilterStudents(object item)
    {
        if (item is not Student student)
        {
            return false;
        }

        if (string.IsNullOrWhiteSpace(SearchText))
        {
            return true;
        }

        return student.Name.Contains(SearchText, StringComparison.OrdinalIgnoreCase);
    }

    private void AddStudent()
    {
        string name = NewName.Trim();
        if (string.IsNullOrWhiteSpace(name))
        {
            MessageBox.Show("Ten sinh vien khong duoc de trong.");
            return;
        }

        if (!int.TryParse(NewAge, out int age) || age <= 0)
        {
            MessageBox.Show("Tuoi phai la so nguyen duong.");
            return;
        }

        if (SelectedStudent is null)
        {
            _students.Add(new Student { Name = name, Age = age });
        }
        else
        {
            SelectedStudent.Name = name;
            SelectedStudent.Age = age;
            StudentsView.Refresh();
        }

        ClearInput();
    }

    private void DeleteStudent()
    {
        if (SelectedStudent is null)
        {
            return;
        }

        _students.Remove(SelectedStudent);
        ClearInput();
    }

    private void ClearInput()
    {
        SelectedStudent = null;
        NewName = string.Empty;
        NewAge = string.Empty;
    }

    private void ToggleSortByAge()
    {
        StudentsView.SortDescriptions.Clear();

        if (_isAscending)
        {
            StudentsView.SortDescriptions.Add(
                new SortDescription(nameof(Student.Age), ListSortDirection.Descending));
        }
        else
        {
            StudentsView.SortDescriptions.Add(
                new SortDescription(nameof(Student.Age), ListSortDirection.Ascending));
        }

        _isAscending = !_isAscending;
    }
}
