using MyProject.UITests.Base;
using MyProject.UITests.Pages;
using NUnit.Framework;
using Xamarin.UITest;

namespace MyProject.UITests.Tests
{
    public class MainPageTests : BaseTestFixture
    {
        public MainPageTests(Platform platform) : base(platform)
        {

        }

        [Test]
        public void AddProductToProductsCollection()
        {
            new MainPage()
                .AddProduct()
                .VerifyProductsCountIsEqualTo(1);
        }

        [Test]
        public void RemoveProductFromProductsCollection()
        {
            new MainPage()
                .AddProduct()
                .RemoveProduct()
                .VerifyProductsCountIsEqualTo(0);
        }
    }
}
