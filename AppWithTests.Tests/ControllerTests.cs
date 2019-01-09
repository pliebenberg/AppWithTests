using AppWithTests.Controllers;
using AppWithTests.Interfaces;
using AppWithTests.Models;
using AppWithTests.Repositories;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;

namespace AppWithTests.Tests
{
    public class ControllerTests
    {
        [Fact]
        public void IndexViewHas2Products()
        {
            //make a controller
            var productRepository = new ProductRepository();
            var controller = new HomeController(productRepository);

            //test index eaction method returns a view
            var viewResult = Assert.IsType<ViewResult>(controller.Index());

            //test index action method view model is List<Product>
            var model = Assert.IsType<List<Product>>(viewResult.Model);

            //test tgat undec action methid returns 2 models
            int expected = 2;
            int actual = model.Count;
            Assert.Equal(expected, actual);
        }
        [Fact]
        public void ProductDetail1IsNamedApples()
        {
            var productRepository = new ProductRepository();
            var controller = new HomeController(productRepository);

            //test index eaction method returns a view
            var viewResult = Assert.IsType<ViewResult>(controller.Details(1));

            //test index action method view model is List<Product>
            var model = Assert.IsType<Product>(viewResult.Model);

            //test that under action method returns 2 models
            string expected = "apples";
            string actual = model.Name.ToLower();
            Assert.Equal(expected, actual);

        }

        [Fact]
        public void UnitTestProductList()
        {
            // 1. Create instance of fake repo using IProductRepository interface.
            var mockProductRepo = new Mock<IProductRepository>();

            // 2. Set up return data for ProductList() method.
            mockProductRepo.Setup(mpr => mpr.ProductList())
                .Returns(new List<Product>{
                    new Product(), new Product(), new Product()
                });

            // 3. Define controller instance with mock repository instance.
            var controller = new HomeController(mockProductRepo.Object);

            // 4. Make your test Assertions 
            // Check if it returns a view
            var result = Assert.IsType<ViewResult>(controller.Index());

            // Check that the model returned to the view is 'List<Product>'.
            var model = Assert.IsType<List<Product>>(result.Model);

            // Ensure count of objects is 3.
            int expected = 3;
            int actual = model.Count;
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void UnitTestProductDetails()
        {
            // 1. Create instance of fake repo using IProductRepository interface.
            var mockProductRepo = new Mock<IProductRepository>();

            // 2. Set up return data for ProductList() method.
            mockProductRepo.Setup(mpr => mpr.ProductDetail(It.IsAny<int>()))
                .Returns(new Product {ID=1, Name="Apricots", Price=1.23m});

            // 3. Define controller instance with mock repository instance.
            var controller = new HomeController(mockProductRepo.Object);

            // 4. Make your test Assertions 
            // Check if it returns a view
            var result = Assert.IsType<ViewResult>(controller.Details(1));

            // Check that the model returned to the view is 'List<Product>'.
            var model = Assert.IsType<Product>(result.Model);

            // Ensure count of objects is 3.
            string expected = "Apricots";
            string actual = model.Name;
            Assert.Equal(expected, actual);
        }

    }
}
