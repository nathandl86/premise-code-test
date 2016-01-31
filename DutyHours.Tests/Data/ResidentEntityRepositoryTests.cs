using DutyHours.Data;
using DutyHours.Data.Repsitories;
using DutyHours.Models.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhino.Mocks;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace DutyHours.Tests.Data
{
    /// <summary>
    /// Unit test for resident respository. Acts against a mocked entity framework
    /// DbContext.
    /// </summary>
    [TestClass]
    public class ResidentEntityRepositoryTests
    {
        private IDutyHoursDbContext _mockContext;

        [TestInitialize]
        public void Initialize()
        {
            _mockContext = BuildDbContext();
        }

        [TestMethod]
        public void FindByInstitutionId_ValidId()
        {
            //Arrange
            var institutionId = 1;
            var repo = new ResidentEntityRepository(_mockContext);

            //Act
            var results = repo.FindByInstitutionId(institutionId);

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
            var repo = new ResidentEntityRepository(_mockContext);

            //Act
            var result = repo.FindByInstitutionId(institutionId);

            //Assert
            Assert.IsFalse(result.HasError);
            Assert.IsNotNull(result.Result);
            Assert.IsFalse(result.Result.Any());
        }

        [TestMethod]
        public void FindById_ValidId()
        {
            //Arrange
            var resId = 1;
            var repo = new ResidentEntityRepository(_mockContext);

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
            var repo = new ResidentEntityRepository(_mockContext);

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
            var repo = new ResidentEntityRepository(_mockContext);

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
            var repo = new ResidentEntityRepository(_mockContext);

            //Act
            var result = repo.FindShiftsByResidentId(resId);

            //Assert
            Assert.IsFalse(result.HasError);
            Assert.IsNotNull(result.Result);
            Assert.IsFalse(result.Result.Any());
        }

        #region Data Stubs

        private List<User> stubUserData = new List<User>
            {
                new User {Id = 1, FirstName="Test", LastName="1", Active = true },
                new User {Id = 2, FirstName="Test", LastName="1", Active = true },
                new User {Id = 3, FirstName="Test", LastName="1", Active = false },
                new User {Id = 4, FirstName="Test", LastName="1", Active = true, MiddleName="Foo" },
                new User {Id = 5, FirstName="Test", LastName="1", Active = true }
            };

        private List<Institution> stubInstitutionData = new List<Institution>
            {
                new Institution { Id = 1, Name="Test State University" },
                new Institution { Id = 2, Name="University of Foo" }
            };

        private List<InstitutionResident> stubInstResidentData = new List<InstitutionResident>
            {
                new InstitutionResident {Id = 1, InstitutionId=1, UserId = 1 },
                new InstitutionResident {Id = 2, InstitutionId=1, UserId = 2 },
                new InstitutionResident {Id = 3, InstitutionId=1, UserId = 3 },
                new InstitutionResident {Id = 4, InstitutionId=2, UserId = 1 },
                new InstitutionResident {Id = 5, InstitutionId=2, UserId = 4 }
            };

        private List<ResidentShift> stubResidentShiftData = new List<ResidentShift>
        {
            new ResidentShift {
                InstitutionResidentId = 1,
                EntryDateTimeUtc = new DateTime(2016, 1, 1),
                StartDateTimeUtc = DateTime.Now.AddDays(-1),
                EndDateTimeUtc = DateTime.Now.AddHours(-5)
            },
            new ResidentShift{
                InstitutionResidentId = 1,
                EntryDateTimeUtc = new DateTime(2016, 1, 1),
                StartDateTimeUtc = DateTime.Now.AddDays(-5),
                EndDateTimeUtc = DateTime.Now.AddDays(-4).AddHours(-12)
            },

            new ResidentShift {
                InstitutionResidentId = 2,
                EntryDateTimeUtc = new DateTime(2016, 1, 1),
                StartDateTimeUtc = DateTime.Now.AddDays(-2),
                EndDateTimeUtc = DateTime.Now.AddDays(-1).AddHours(-5)
            },
            new ResidentShift{
                InstitutionResidentId = 2,
                EntryDateTimeUtc = new DateTime(2016, 1, 1),
                StartDateTimeUtc = DateTime.Now.AddDays(-4),
                EndDateTimeUtc = DateTime.Now.AddDays(-4).AddHours(-12)
            },

            new ResidentShift {
                InstitutionResidentId = 3,
                EntryDateTimeUtc = new DateTime(2016, 1, 1),
                StartDateTimeUtc = DateTime.Now.AddDays(-1),
                EndDateTimeUtc = DateTime.Now.AddHours(-5)
            },
            new ResidentShift{
                InstitutionResidentId = 3,
                EntryDateTimeUtc = new DateTime(2016, 1, 1),
                StartDateTimeUtc = DateTime.Now.AddDays(-5),
                EndDateTimeUtc = DateTime.Now.AddDays(-4).AddHours(-16)
            },

            new ResidentShift {
                InstitutionResidentId = 4,
                EntryDateTimeUtc = new DateTime(2016, 1, 1),
                StartDateTimeUtc = DateTime.Now.AddDays(-3),
                EndDateTimeUtc = DateTime.Now.AddHours(-5)
            },

            new ResidentShift{
                InstitutionResidentId = 5,
                EntryDateTimeUtc = new DateTime(2016, 1, 1),
                StartDateTimeUtc = DateTime.Now.AddDays(-5),
                EndDateTimeUtc = DateTime.Now.AddDays(-4).AddHours(-10)
            },
        };

        #endregion

        #region Mock DbContext

        private IDutyHoursDbContext BuildDbContext()
        {
            var mockContext = MockRepository.GenerateMock<IDutyHoursDbContext>();

            //setup user data
            mockContext.Stub(x => x.Users).PropertyBehavior();
            mockContext.Users = GetDbSet(stubUserData);


            //setup institution data
            mockContext.Stub(x => x.Institutions).PropertyBehavior();
            mockContext.Institutions = GetDbSet(stubInstitutionData);


            //setup institution resident data
            mockContext.Stub(x => x.InstitutionResidents).PropertyBehavior();
            mockContext.InstitutionResidents = GetDbSet(stubInstResidentData);

            //setup resident shift data
            mockContext.Stub(x => x.ResidentShifts).PropertyBehavior();
            mockContext.ResidentShifts = GetDbSet(stubResidentShiftData);

            return mockContext;
        }

        private static IDbSet<T> GetDbSet<T>(IList<T> data) where T : DataModelBase
        {
            var queryable = data.AsQueryable();
            var dbSet = MockRepository.GenerateMock<IDbSet<T>, IQueryable>();

            dbSet.Stub(m => m.Provider).Return(queryable.Provider);
            dbSet.Stub(m => m.Expression).Return(queryable.Expression);
            dbSet.Stub(m => m.ElementType).Return(queryable.ElementType);
            dbSet.Stub(m => m.GetEnumerator()).Return(queryable.GetEnumerator());

            return dbSet;
        }

        #endregion
    }
}
