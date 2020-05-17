using eShop.Core.Models.Marketing;
using eShop.Core.Services.Marketing;
using eShop.Core.Services.Settings;
using eShop.Core.ViewModels.Base;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace eShop.Core.ViewModels
{
    public class CampaignViewModel : ViewModelBase
    {
        private readonly ISettingsService _settingsService;
        private readonly ICampaignService _campaignService;

        private ObservableCollection<CampaignItem> _campaigns;

        public CampaignViewModel(ISettingsService settingsService, ICampaignService campaignService)
        {
            _settingsService = settingsService;
            _campaignService = campaignService;
        }

        public ObservableCollection<CampaignItem> Campaigns
        {
            get => _campaigns;
            set
            {
                _campaigns = value;
                RaisePropertyChanged(() => Campaigns);
            }
        }

        public ICommand GetCampaignDetailsCommand => new Command<CampaignItem>(async (item) => await GetCampaignDetailsAsync(item));

        public override async Task InitializeAsync(object navigationData)
        {
            IsBusy = true;
            // Get campaigns by user
            Campaigns = await _campaignService.GetAllCampaignsAsync(_settingsService.AuthAccessToken);
            IsBusy = false;
        }

        private async Task GetCampaignDetailsAsync(CampaignItem campaign)
        {
            await NavigationService.NavigateToAsync<CampaignDetailsViewModel>(campaign.Id);
        }
    }
}