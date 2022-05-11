using Models.Settings;
using Services.Interfaces;

namespace Services.Services
{
    public class GeneralConfigService : IGeneralConfigService
    {
        private readonly bool _showImagePreview;
        private readonly bool _showWorkSafeOnly;

        public GeneralConfigService(GeneralConfig generalConfig)
        {
            _showImagePreview = generalConfig.ShowImagePreview;
            _showImagePreview = generalConfig.WorkSafeOnlyBoards;
        }

        public bool ShowImagePreview() => _showImagePreview;
        public bool ShowWorkSafeOnlyBoards() => _showWorkSafeOnly;
    }
}
