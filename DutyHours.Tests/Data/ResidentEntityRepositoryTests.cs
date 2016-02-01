using DutyHours.EntityData.Mappers;
using DutyHours.EntityData.Repsitories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhino.Mocks;
using System.Linq;

namespace DutyHours.Tests.Data
{
    /// <summary>
    /// Unit test for resident respository. Acts against a mocked entity framework
    /// DbContext.
    /// </summary>
    [TestClass]
    public class ResidentEntityRepositoryTests : BaseRepositoryTests
    {
        [TestMethod]
        public void FindById_ValidId()
        {
            //Arrange
            var resId = 1;
            var mapper = new Mapper();
            var repo = new ResidentEntityRepository(MockContext, mapper);

            //Act
            var result = repo.FindById(resId);

            //Assert
            Assert.IsFalse(result.HasError);
            Assert.IsNotNull(result.Result);
            Assert.AreEqual(result.Result.Id, resId);
        }

        [TestMethod]
        public void FindById_InvalidId()
        {
            //Arrange
            var userId = -1;
            var mapper = MockRepository.GenerateStub<IMapper>();
            var repo = new ResidentEntityRepository(MockContext, mapper);

            //Act
            var result = repo.FindById(userId);

            //Assert
            Assert.IsFalse(result.HasError);
            Assert.IsNull(result.Result);
        }

        [TestMethod]
        public void FindShiftsByResidentId_ValidId()
        {
            //Arrange
            var resId = 1;
            var mapper = new Mapper();
            var repo = new ResidentEntityRepository(MockContext, mapper);

            //Act
            var result = repo.FindShiftsByResidentId(resId);

            //Assert
            Assert.IsFalse(result.HasError);
            Assert.IsNotNull(result.Result);
            Assert.AreEqual(result.Result.Count(), stubResidentShiftData.Where(rs => rs.InstitutionResidentId == resId).Count());
        }

        [TestMethod]
        public void FindShiftsByResidentId_InvalidId()
        {
            //Arrange
            var resId = -1;
            var mapper = new Mapper();
            var repo = new ResidentEntityRepository(MockContext, mapper);

            //Act
            var result = repo.FindShiftsByResidentId(resId);

            //Assert
            Assert.IsFalse(result.HasError);
            Assert.IsNotNull(result.Result);
            Assert.IsFalse(result.Result.Any());
        }
        
    }
}
