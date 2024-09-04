using DesktopUI.Commands;
using DesktopUI.Models;
using DesktopUI.Utilities;
using DesktopUI.Utilities.Services;
using DesktopUI.ViewModels.Employee;
using Domain.Entities;
using System.Windows.Input;

namespace DesktopUI.ViewModels
{
	/// <summary>
	/// This ViewModel is navigation between CRUD tools for User entity 
	/// </summary>
	public class EmployeeVM : BaseViewModel
	{
        // Fields
        private static readonly string ADDITION_TITLE = "Добавление сотрудника";
        private static readonly string CARD_TITLE = "Информация о сотруднике";
        private static readonly string TABLE_TITLE = "Список сотрудников";
		private EmployeeModel _model;
		private object _navigationVM;
        private string _title;

        // Properties
        public string Title
        {
            get => _title;
            set
            {
                if(_title != value)
                {
                    _title = value;
                    OnPropertyChanged(nameof(Title));
                }
            }
        }
        public object ObserveMode
        {
            get => _navigationVM;
            set
            {
                _navigationVM = value;
                OnPropertyChanged(nameof(ObserveMode));
            }
        }
		// Ctors
		public EmployeeVM()
        {
            _model = new EmployeeModel();
            AdditionCommand = new RelayCommand(Addition);
            CardCommand = new RelayCommand(Card);
            TableCommand = new RelayCommand(Table);
            ObserveMode = new TableVM();

            EmployeeService.MainRequested += OnMainRequested;
            EmployeeService.EmployeeChanged += OnEmployeeChanged;
        }

        // Commands
        public ICommand AdditionCommand { get; set; }
        public ICommand CardCommand { get; set; }
        public ICommand TableCommand { get; set; }


		private void Addition(object obj)
        {
            if (ObserveMode is AdditionVM) return;
            ObserveMode = new AdditionVM();
            Title = ADDITION_TITLE;
        }
        private void Card(object obj)
        {
            if (ObserveMode is CardVM) return;
            ObserveMode = new CardVM();
            Title = CARD_TITLE;
        }
        private void Table(object obj)
        {
            if (ObserveMode is TableVM) return;
            ObserveMode = new TableVM();
            Title = TABLE_TITLE;
        }

        // Public Methods

        // Private Methods
        private void OnMainRequested()
        {
            if (ObserveMode is TableVM) return;
            ObserveMode = new TableVM();
            Title = TABLE_TITLE;
        }
        private void OnEmployeeChanged(User user)
        {
            Card(user);
        }
    }
}
