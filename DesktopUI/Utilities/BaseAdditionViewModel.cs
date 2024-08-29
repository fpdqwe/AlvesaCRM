using DesktopUI.Commands;
using Domain.Entities;
using System.Windows.Input;

namespace DesktopUI.Utilities
{
	public abstract class BaseAdditionViewModel<T> : BaseViewModel where T : IDbEntity
	{
		protected T _value;

		public T Value
		{
			get => _value;
			set
			{
				_value = value;
				OnPropertyChanged(nameof(Value));
			}
		}

        protected BaseAdditionViewModel()
        {
            AddCommand = new RelayCommand(Add);
        }

		public ICommand AddCommand { get; set; }

		protected virtual void Add(object obj)
		{
			throw new NotImplementedException();
		}
    }
}
