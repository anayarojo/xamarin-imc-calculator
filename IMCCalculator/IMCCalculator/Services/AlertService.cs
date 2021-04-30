using Acr.UserDialogs;
using IMCCalculator.Interfaces;
using IMCCalculator.Resx;
using IMCCalculator.Services;
using System.Threading.Tasks;
using Xamarin.Forms;

[assembly: Dependency(typeof(AlertService))]
namespace IMCCalculator.Services
{
    public class AlertService : IAlertService
    {
        public async Task ShowMessage(string header, string message)
        {
            var config = new AlertConfig()
            {
                Title = header,
                Message = message,
                OkText = AppResources.Btn_OK,
            };

            await UserDialogs.Instance.AlertAsync(config);
        }
    }
}
