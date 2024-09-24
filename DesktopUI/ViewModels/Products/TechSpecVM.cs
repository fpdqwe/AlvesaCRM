using DesktopUI.Commands;
using DesktopUI.Models;
using DesktopUI.Utilities;
using Domain.Entities.Product;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using Model = DesktopUI.Models.ProductModel;

namespace DesktopUI.ViewModels.Products
{
    public class TechSpecVM : BaseViewModel
    {
        // Fields
        private const string COLORS_VISIBILITY_BTN_CONTENT_POSITIVE = "Скрыть детали";
        private const string COLORS_VISIBILITY_BTN_CONTENT_NEGATIVE = "Показать детали";
        private Model _model;
        private TechSpec _techSpec;
        private ObservableCollection<ColorVM> _colorVMs;
        private ColorVM _selectedColorVM;
        private bool _isContentVisible = false;
        private Visibility _isContentVisibleState = Visibility.Collapsed;
        private string _visibilityBtnContent = COLORS_VISIBILITY_BTN_CONTENT_NEGATIVE;
        // Properties
        public TechSpec TechSpec
        {
            get => _techSpec;
            set
            {
                _techSpec = value;
                OnPropertyChanged(nameof(TechSpec));
            }
        }
        public ObservableCollection<ColorVM> Colors
        {
            get => _colorVMs;
            set
            {
                _colorVMs = value;
                OnPropertyChanged(nameof(Colors));
            }
        }
        public ColorVM SelectedColor
        {
            get => _selectedColorVM;
            set
            {
                _selectedColorVM = value;
                OnPropertyChanged(nameof(SelectedColor));
            }
        }
        public int SequenceNum
        {
            get => _techSpec.SequenceNum;
            set
            {
                _techSpec.SequenceNum = value;
                OnPropertyChanged(nameof(SequenceNum));
            }
        }
        public string DateCreated
        {
            get => _techSpec.DateCreated.ToString();
            set
            {
                _techSpec.DateCreated = Convert.ToDateTime(value);
                OnPropertyChanged(nameof(DateCreated));
            }
        }
        public decimal Price
        {
            get => _techSpec.Price;
            set
            {
                _techSpec.Price = value;
                OnPropertyChanged(nameof(Price));
            }
        }
        public decimal ProductionPrice
        {
            get => _techSpec.ProductionPrice;
            set
            {
                _techSpec.ProductionPrice = value;
                OnPropertyChanged(nameof(ProductionPrice));
            }
        }
        public int TotalQuantity
        {
            get => _techSpec.TotalQuantity;
            set
            {
                _techSpec.TotalQuantity = value;
                OnPropertyChanged(nameof(TotalQuantity));
            }
        }
        public bool IsContentVisible
        {
            get => _isContentVisible;
            set
            {
                _isContentVisible = value;
                if (value) { 
                    IsContentVisibleState = Visibility.Visible; 
                    VisibilityBtnContent = COLORS_VISIBILITY_BTN_CONTENT_POSITIVE; 
                }
                else { 
                    IsContentVisibleState = Visibility.Collapsed; 
                    VisibilityBtnContent = COLORS_VISIBILITY_BTN_CONTENT_NEGATIVE; 
                }
                OnPropertyChanged(nameof(IsContentVisible));
            }
        }
        public Visibility IsContentVisibleState
        {
            get => _isContentVisibleState;
            set {
                _isContentVisibleState = value;
                OnPropertyChanged(nameof(IsContentVisibleState));
            }
        }
        public string VisibilityBtnContent
        {
            get => _visibilityBtnContent;
            set
            {
                _visibilityBtnContent = value;
                OnPropertyChanged(nameof(VisibilityBtnContent));
            }
        }
		// Ctors
		public TechSpecVM(TechSpec ts, Model model)
		{
			_model = model;
            TechSpec = ts;
            Colors = GetColorVMCollection(TechSpec.Colors);
            SelectedColor = Colors.First();
            CheckQuantity();
            SetCommands();
		}
		// Commands
		public ICommand AddColorCommand { get; set; }
        public ICommand CopyColorCommand { get; set; }
        public ICommand UpdateCommand {  get; set; }
        public ICommand DeleteCommand { get; set; }
        // Commands realization
        public async void AddColor(object obj)
        {
            var newColor = await _model.GenerateNewColor(_techSpec);
            if (!await _model.AddColor(newColor)) return;
            Colors.Add(new ColorVM(newColor, _model, Colors.Count + 1));
        }
        public async void CopyColor(object obj)
        {
            var newColor = await _model.GenerateNewColor(_techSpec);
            Colors.Add(new ColorVM(newColor, _model, Colors.Count + 1));
        }
        public async void Update(object obj)
        {
            await _model.UpdateTechSpec(_techSpec);
        }
        public void Delete(object obj)
        {

        }

        // Private methods
        private void OnColorDeleted(ColorVM color)
        {
			Colors.RemoveAt(color.Index - 1);
			OnPropertyChanged(nameof(Colors));
			color.ColorDeleted -= OnColorDeleted;
			color.ColorUpdated -= OnColorUpdated;
		}
        private void OnColorUpdated(ColorVM color)
        {
            OnPropertyChanged(nameof(Colors));
        }
        private void SetCommands()
        {
            AddColorCommand = new RelayCommand(AddColor);
            CopyColorCommand = new RelayCommand(CopyColor);
            UpdateCommand = new RelayCommand(Update);
            DeleteCommand = new RelayCommand(Delete);
        }
        private void CheckQuantity()
        {
            int summ = 0;
            foreach (var item in Colors)
            {
                summ += item.Color.TotalQuantity;
            }
            if (_techSpec.TotalQuantity != summ)
            {
                TechSpec.TotalQuantity = summ;
                _model.UpdateTechSpec(TechSpec);
            }
        }
        private ObservableCollection<ColorVM> GetColorVMCollection(List<ProductColor> colors)
        {
			var result = new ObservableCollection<ColorVM>();
			int i = 1;
			foreach (var color in _techSpec.Colors)
			{
				var newVM = new ColorVM(color, _model, i);
				newVM.ColorDeleted += OnColorDeleted;
				newVM.ColorUpdated += OnColorUpdated;
				result.Add(newVM);
				i++;
			}
			return result;
		}
    }
}
