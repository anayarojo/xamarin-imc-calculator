using System.Threading.Tasks;

namespace IMCCalculator.Interfaces
{
    public interface IAlertService
    {
        Task ShowMessage(string header, string message);
    }
}
