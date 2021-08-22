using Countries.Infra.Data.Repositories.Custom;
using Countries.Infra.Data.Repositories.Generic;
using Countries.UnitTests.ResourcesDatabase;
using Moq;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Countries.UnitTests.Infastructure.Repositories.Tests.Countries
{
    public class CountriesRepositoryTests : BaseContextTest
    {
        [Fact]
        public async Task Countries_List_Success()
        {
            // Arrange           
            var unitOfWorkMock = new Mock<IUnitOfWork>();
           unitOfWorkMock.SetupGet(x => x.Context).Returns(base._context);
           
            // Act
            var repository = new CountriesRepository(unitOfWorkMock.Object);
            var result = await Task.Run(() => repository.Get(null, null, string.Empty));

            // Assert
            Assert.NotNull(result);
            Assert.True(condition: result.Any());
            Assert.True(result.Count() == 3);

            Assert.Equal("Francia", result.ElementAt(0).Name);
            Assert.Equal("Fr", result.ElementAt(0).Alpha_2);
            Assert.Equal("Fra", result.ElementAt(0).Alpha_3);
            Assert.Equal("250", result.ElementAt(0).NumericCode);
        }
    }
}
