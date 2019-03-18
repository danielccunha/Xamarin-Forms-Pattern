using MyProject.UITests.Base;
using NUnit.Framework;
using System;
using System.Linq;
using Query = System.Func<Xamarin.UITest.Queries.AppQuery, Xamarin.UITest.Queries.AppQuery>;

namespace MyProject.UITests.Pages
{
    public class MainPage : BasePage
    {
        private readonly Query AddProductButton;
        private readonly Query FirstProduct;
        private readonly Query ProductsList;
        private readonly Query ProductsListChildren;
        private readonly Query RemoveButton;

        private int ProductsCount
        {
            get
            {
                App.WaitForElement(ProductsList);
                return App.Query(ProductsListChildren).Length;
            }
        }

        protected override PlatformQuery Trait => new PlatformQuery
        {
            Android = x => x.Marked("ProductsPage"),
            iOS = x => x.Marked("ProductsPage"),
        };

        public MainPage()
        {
            AddProductButton = x => x.Property("Text", "Add");
            FirstProduct = x => x.Marked("ProductsList").Child(index: 0);
            ProductsList = x => x.Marked("ProductsList");
            ProductsListChildren = x => x.Marked("ProductsList").Child();
            RemoveButton = x => x.Property("Text", "Remove");
        }

        public MainPage AddProduct()
        {
            App.WaitForElement(ProductsList);
            App.WaitForElement(AddProductButton);

            App.Tap(AddProductButton);

            return this;
        }

        public MainPage RemoveProduct()
        {
            if (ProductsCount == 0)
                return this;

            App.TouchAndHold(FirstProduct);
            App.WaitForElement(RemoveButton);
            App.Tap(RemoveButton);

            return this;
        }

        public MainPage VerifyProductsCountIsEqualTo(int count)
        {
            App.WaitForElement(ProductsList);

            Assert.That(ProductsCount, Is.EqualTo(count));

            return this;
        }
    }
}
