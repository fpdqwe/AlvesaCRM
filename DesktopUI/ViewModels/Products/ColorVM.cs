using DesktopUI.Utilities;
using Model = DesktopUI.Models.ProductModel;
using Color = Domain.Entities.Product.ProductColor;
using Size = Domain.Entities.Product.ProductSize;
using System.Collections.ObjectModel;
using System.Windows.Input;
using DesktopUI.Commands;

namespace DesktopUI.ViewModels.Products
{
    public class ColorVM : BaseViewModel
    {
        // Fields
        private Color _color;
        private ObservableCollection<SizeVM> _sizes;
        private SizeVM _selectedSize;
        private Model _model;
        private int _index;
        private bool _isContentVisible = false;

        public delegate void ColorEvent(ColorVM color);
        public event ColorEvent ColorUpdated;
        public event ColorEvent ColorDeleted;
		// Properties
        public int Index
        {
            get => _index;
            set
            {
                if (_index != value)
                {
                    _index = value;
                    OnPropertyChanged(nameof(Index));
                }
            }
        }
		public Color Color
        {
            get => _color;
            set
            {
                OnPropertyChanged(nameof(Color));
            }
        }
        public ObservableCollection<SizeVM> Sizes
        {
            get => _sizes;
            set
            {
                OnPropertyChanged(nameof(Sizes));
            }
        }
        public SizeVM SelectedSize
        {
            get => _selectedSize;
            set
            {
                _selectedSize = value;
                OnPropertyChanged(nameof(SelectedSize));
            }
        }
        public bool IsContentVisible
        {
            get => _isContentVisible;
            set
            {
                _isContentVisible = value;
                OnPropertyChanged(nameof(IsContentVisible));
            }
        }
        // Ctors
        public ColorVM(Color color, Model model, int index)
        {
            Index = index;
            InitCommands();
            _color = color;
            _model = model;
			_sizes = GetSizeVMCollection(color.Sizes);
            CheckQuantity();
        }

        // Commands
        public ICommand AddSizeCommand { get; set; }
		public ICommand UpdateColorCommand { get; set; }
		public ICommand DeleteColorCommand { get; set; }

        // Commands realization
        private async void AddSize(object parameter)
        {
            var newSize = _model.GenerateNewSize(Color);
            if (!await _model.AddSize(newSize)) return;
            Sizes.Add(new SizeVM(newSize, _model, Sizes.Count + 1));
        }
		private async void UpdateColor(object parameter)
		{
			await _model.UpdateColor(Color);
            ColorUpdated?.Invoke(this);
		}
		private async void DeleteColor(object parameter)
		{
			await _model.DeleteColor(Color);
            ColorDeleted?.Invoke(this);
		}

		// Private methods and utilities
        private void OnSizeUpdated(SizeVM size)
        {
            OnPropertyChanged(nameof(Sizes));
        }
        private void OnSizeDeleted(SizeVM size)
        {
            Sizes.RemoveAt(size.Index - 1);
            OnPropertyChanged(nameof(Sizes));
            size.SizeDeleted -= OnSizeDeleted;
            size.SizeUpdated -= OnSizeUpdated;
        }
        private void InitCommands()
        {
            AddSizeCommand = new RelayCommand(AddSize);
            UpdateColorCommand = new RelayCommand(UpdateColor);
            DeleteColorCommand = new RelayCommand(DeleteColor);
        }
        private void CheckQuantity()
        {
            int summ = 0;
            foreach(var size in Sizes)
            {
                summ += size.Size.Quantity;
            }
            if (Color.TotalQuantity != summ)
            {
                Color.TotalQuantity = summ;
                _model.UpdateColor(Color);
            }
        }
		private ObservableCollection<SizeVM> GetSizeVMCollection(List<Size> sizes)
		{
			var result = new ObservableCollection<SizeVM>();
            int i = 1;
			foreach (var size in sizes)
			{
                var newVM = new SizeVM(size, _model, i);
                newVM.SizeDeleted += OnSizeDeleted;
                newVM.SizeUpdated += OnSizeUpdated;
				result.Add(newVM);
                i++;
			}
			return result;
		}

        // Public methods
        public void AppendNewSize()
        {
            AddSize(null);
        }
	}
}
