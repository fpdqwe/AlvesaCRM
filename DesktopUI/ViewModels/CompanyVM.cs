using DesktopUI.Commands;
using DesktopUI.Models;
using DesktopUI.Utilities;
using Domain.Entities;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace DesktopUI.ViewModels
{
    /// <summary>
    /// Navigation ViewModel between CRUD tools for user's company
    /// </summary>
	public class CompanyVM : INotifyPropertyChanged
	{
		// Fields
		private Company _current;
        private CompanyModel _model;
		private string _exception;


		// Properties
		public Company CurrentCompany
		{
			get => _current;
			set
			{
				_current = value;
				OnPropertyChanged(nameof(CurrentCompany));
			}
		}
		public string Name
		{
			get => _current.Name;
			set
			{
				_current.Name = value;
				OnPropertyChanged(nameof(Name));
			}
		}
		public string CompanyType
		{
			get
			{
				switch (_current.Type)
				{
					case Domain.Enums.CompanyType.Individual:
						return "Физ. лицо";
					case Domain.Enums.CompanyType.IE:
						return "ИП";
					case Domain.Enums.CompanyType.LE:
						return "Юр. лицо";
					default:
						throw new NotImplementedException();
				}
			}
			set
			{
				switch(value)
				{
					case "Физ. лицо":
						_current.Type = Domain.Enums.CompanyType.Individual;
						OnPropertyChanged(nameof(CompanyType));
						break;
					case "ИП":
						_current.Type = Domain.Enums.CompanyType.IE;
						OnPropertyChanged(nameof(CompanyType));
						break;
					case "Юр. лицо":
						_current.Type = Domain.Enums.CompanyType.LE;
						OnPropertyChanged(nameof(CompanyType));
						break;
					default:
						throw new NotImplementedException();
				}
			}
		}
		public string INN
		{
			get => _current.INN;
			set
			{
				_current.INN = value;
				OnPropertyChanged(nameof(INN));
			}
		}
		public string KPP
		{
			get => _current.KPP;
			set
			{
				_current.KPP = value;
				OnPropertyChanged(nameof(KPP));
			}
		}
		public string Adress
		{
			get => _current.Adress;
			set
			{
				_current.Adress = value;
				OnPropertyChanged(nameof(Adress));
			}
		}
		public string SettlementAccount
		{
			get => _current.SettlementAccount;
			set
			{
				_current.SettlementAccount = value;
				OnPropertyChanged(nameof(SettlementAccount));
			}
		}
		public string Exception
		{
			get => _exception;
			set
			{
				_exception = value;
				OnPropertyChanged(nameof(Exception));
			}
		}
		public string[] CompanyTypes
		{
			get => new[] { "Физ. лицо", "ИП", "Юр. лицо" };
		}

		// Ctors
		public CompanyVM()
        {
            _model = new CompanyModel();
            CurrentCompany = AuthService.CurrentUser.Company;
            UpdateCommand = new RelayCommand(Update);
			_exception = string.Empty;
        }

        // Commands
        public ICommand UpdateCommand { get; set; }
		public async void Update(object obj)
		{
			if (await _model.UpdateCompany(_current)) Exception = "Данные о вашей компании успешно обновлены.";
			else Exception = "Что-то пошло не так, попробуйте обновить данные позже...";
		}

		// Public methods



		// Private methods

		// INotifyPropertyChanged realization
		public event PropertyChangedEventHandler? PropertyChanged;
		public void OnPropertyChanged([CallerMemberName] string prop = "")
		{
			if (PropertyChanged != null)
				PropertyChanged(this, new PropertyChangedEventArgs(prop));
		}
	}
}
