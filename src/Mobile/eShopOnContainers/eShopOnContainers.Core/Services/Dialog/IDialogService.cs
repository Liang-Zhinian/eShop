using System.Threading.Tasks;

namespace eShop.Services
{
    public interface IDialogService
    {
        Task ShowAlertAsync(string message, string title, string buttonLabel);
    }
}
