using Moq;
using MyProject.Contracts.Services.General;

namespace MyProject.UnitTests.Base
{
    public abstract class BaseViewModelTestFixture : BaseRepositoryTestFixture
    {
        protected Mock<INavigationService> NavigationService { get; set; }
        protected Mock<IDialogService> DialogService { get; set; }

        public BaseViewModelTestFixture() : base()
        {
            NavigationService = new Mock<INavigationService>();
            DialogService = new Mock<IDialogService>();
        }
    }
}
