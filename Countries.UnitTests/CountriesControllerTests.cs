using Application.Interfaces;
using Countries.API.Controllers;
using Countries.Models.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace Countries.UnitTests
{
    public class CountriesControllerTests
    {
        [Fact]
        public async Task Create_ReturnsBadRequest_GivenInvalidModel()
        {
            // Arrange
            var mockRepo = new Mock<ICountriesService>();

            var controller = new CountriesController(mockRepo.Object);
            controller.ModelState.AddModelError("error", "some error");

            // Act            
            var result = await controller.PostCountry(country: null);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result.Result);
        }
        
        [Fact]
        public async Task GetCountry_IsOfType_ActionResult_Country()
        {
            // Arrange
            var mockRepo = new Mock<ICountriesService>();
            var controller = new CountriesController(mockRepo.Object);            

            // Act
            var result = await controller.GetCountry(45);

            // Assert
            Assert.IsType<ActionResult<Country>>(result);            
        }       
    }
}
