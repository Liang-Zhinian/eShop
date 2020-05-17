using eShop.Core.Services.Marketing;
using eShop.Core.Services.Settings;
using eShop.Core.ViewModels;
using eShop.Core.ViewModels.Base;
using eShop.UnitTests.Mocks;
using System.Threading.Tasks;
using Xunit;

namespace eShop.UnitTests.ViewModels
{
    public class MarketingViewModelTests
    {
        public MarketingViewModelTests()
        {
            ViewModelLocator.UpdateDependencies(true);
            ViewModelLocator.RegisterSingleton<ISettingsService, MockSettingsService>();
        }

        [Fact]
        public void GetCampaignsIsNullTest()
        {
            var settingsService = new MockSettingsService();
            var campaignService = new CampaignMockService();
            var campaignViewModel = new CampaignViewModel(settingsService, campaignService);
            Assert.Null(campaignViewModel.Campaigns);
        }

        [Fact]
        public async Task GetCampaignsIsNotNullTest()
        {
            var settingsService = new MockSettingsService();
            var campaignService = new CampaignMockService();
            var campaignViewModel = new CampaignViewModel(settingsService, campaignService);

            await campaignViewModel.InitializeAsync(null);

            Assert.NotNull(campaignViewModel.Campaigns);
        }

        [Fact]
        public void GetCampaignDetailsCommandIsNotNullTest()
        {
            var settingsService = new MockSettingsService();
            var campaignService = new CampaignMockService();
            var campaignViewModel = new CampaignViewModel(settingsService, campaignService);
            Assert.NotNull(campaignViewModel.GetCampaignDetailsCommand);
        }

        [Fact]
        public void GetCampaignDetailsByIdIsNullTest()
        {
            var settingsService = new MockSettingsService();
            var campaignService = new CampaignMockService();
            var campaignViewModel = new CampaignDetailsViewModel(settingsService, campaignService);
            Assert.Null(campaignViewModel.Campaign);
        }

        [Fact]
        public async Task GetCampaignDetailsByIdIsNotNullTest()
        {
            var settingsService = new MockSettingsService();
            var campaignService = new CampaignMockService();
            var campaignDetailsViewModel = new CampaignDetailsViewModel(settingsService, campaignService);

            await campaignDetailsViewModel.InitializeAsync(1);

            Assert.NotNull(campaignDetailsViewModel.Campaign);
        }
    }
}