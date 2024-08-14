using DesktopUI.Commands;
using DesktopUI.Models;
using DesktopUI.Utilities;
using Domain.Entities;
using System.ComponentModel;
using System.Windows.Input;

namespace DesktopUI.ViewModels
{
	public class CompanyVM : INotifyPropertyChanged
	{
        private CompanyModel Model { get; set; }
        public Company CurrentCompany { get; private set; }
		public event PropertyChangedEventHandler? PropertyChanged;

        public CompanyVM()
        {
            Model = new CompanyModel();
            CurrentCompany = AuthService.CurrentUser.Company;
            ChangeCompanyCommand = new RelayCommand(ChangeCommand);
        }

        public ICommand ChangeCompanyCommand;

        public void ChangeCommand(object obj)
        {
            Model.ChangeCompanyType(CurrentCompany);
        }
    }
}
