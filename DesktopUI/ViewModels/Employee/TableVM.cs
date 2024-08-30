using DesktopUI.Models;
using DesktopUI.Utilities;
using Domain.Entities;
using System.Collections.ObjectModel;

namespace DesktopUI.ViewModels.Employee
{
	public class TableVM : BaseViewModel
	{
		private List<User> _employee;
		private EmployeeModel _model;

		public ObservableCollection<User> Employee
		{
			get => new ObservableCollection<User>(_employee);
			set
			{
				_employee = value.ToList();
				OnPropertyChanged(nameof(Employee));
			}
		}

        public TableVM()
        {
            _model = new EmployeeModel();
			UpdateList();
        }
        private async void UpdateList()
		{
			if (_employee != null) _employee.Clear();
			else _employee = new List<User>();
			var newList = await _model.Read();
			Employee = new ObservableCollection<User>(newList);
		}
    }
}
