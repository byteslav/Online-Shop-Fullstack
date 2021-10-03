using System;
using csharp_dapper_example.Controllers;
using csharp_dapper_example.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Xunit;
using Xunit.Abstractions;

namespace TestProject1
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            // Arrange
            HomeController controller = new HomeController();
 
            // Act
            ViewResult result = controller.Index() as ViewResult;
            // Assert
            Assert.Equal("Hello!", result?.ViewData["Message"]);
            Assert.NotNull(result);
            Assert.Equal("Index", result.ViewName);
        }
    }
}