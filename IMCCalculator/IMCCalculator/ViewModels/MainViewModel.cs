using IMCCalculator.Interfaces;
using IMCCalculator.Resx;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace IMCCalculator.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private readonly IAlertService _alertService;

        private string _height;
        private string _weight;
        private string _imc;

        public string Height
        {
            get => _height;
            set => SetProperty(ref _height, value);
        }

        public string Weight
        {
            get => _weight;
            set => SetProperty(ref _weight, value);
        }

        public string Imc
        {
            get => _imc;
            set => SetProperty(ref _imc, value);
        }

        public Command CalculateCommand { get; }

        public MainViewModel()
        {
            _alertService = DependencyService.Resolve<IAlertService>();

            Title = AppResources.Lbl_Calculate_Your_IMC;
            CalculateCommand = new Command(async (object obj) => await OnCalculateClicked(obj));
        }

        public async Task OnCalculateClicked(object sender)
        {
            if (!HasValidDataEntries())
            {
                await _alertService.ShowMessage(AppResources.Title_Alert, AppResources.ALERT_PLEASE_FILL_ALL_FIELDS);
                return;
            }

            var height = double.Parse(_height);
            var weight = double.Parse(_weight);
            var imc = weight / (height * height);

            Imc = imc.ToString();
            await ShowAlertResult(imc);
        }

        private bool HasValidDataEntries()
        {
            return !string.IsNullOrEmpty(_height) && 
                !string.IsNullOrEmpty(_weight);
        }

        private async Task ShowAlertResult(double imc)
        {
            var resultMessage = "";

            if (imc < 18.5)
            {
                resultMessage = AppResources.RESULT_LOW_WEIGHT;
            }
            else if (imc >= 18.5 && imc < 24.9)
            {
                resultMessage = AppResources.RESULT_NORMAL_WEIGHT;
            }
            else if (imc >= 25 && imc <= 29.9)
            {
                resultMessage = AppResources.RESULT_OVER_WEIGHT;
            }
            else
            {
                resultMessage = AppResources.RESULT_OBESITY_WEIGHT;
            }

            await _alertService.ShowMessage(AppResources.Title_Result, resultMessage);
        }
    }
}
