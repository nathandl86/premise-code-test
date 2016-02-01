using DutyHours.EntityData.Mappers;
using DutyHours.EntityData.Repsitories;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using System.Linq;

namespace DutyHours.Tests.Data
{
    [TestClass]
    public class InstitutionEntityRepositoryTests : BaseRepositoryTests
    {
        [TestMethod]
        public void FindByInstitutionId_ValidId()
        {
            //Arrange
            var institutionId = 1;
            var mapper = new Mapper();
            var repo = new InstitutionEntityRepository(MockContext,mapper);

            //Act
            var results = repo.FindResidentsByInstitutionId(institutionId);

            //Assert
            Assert.IsFalse(results.HasError);
            Assert.IsNotNull(results.Result);
            Assert.IsTrue(results.Result.Any());
            Assert.AreEqual(results.Result.ToList().Count(),
                stubInstResidentData.Where(ir => ir.InstitutionId == institutionId).Count());
        }

        [TestMethod]
        public void FindByInstitutionId_InvalidId()
        {
            //Arrange
            var institutionId = -1;
            var mapper = new Mapper();
            var repo = new InstitutionEntityRepository(MockContext, mapper);

            //Act
            var result = repo.FindResidentsByInstitutionId(institutionId);

            //Assert
            Assert.IsFalse(result.HasError);
            Assert.IsNotNull(result.Result);
            Assert.IsFalse(result.Result.Any());
        }
    }
}
