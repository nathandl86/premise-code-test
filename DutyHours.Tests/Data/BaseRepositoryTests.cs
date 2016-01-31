using DutyHours.Data;
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
    /// Base test class for the entity framework respositories. Contains the guts for 
    /// mocking and stubbing DbContext functionality
    /// </summary>
    [TestClass]
    public abstract class BaseRepositoryTests
    {
        protected IDutyHoursDbContext MockContext { get; set; }

        [TestInitialize]
        public void Initialize()
        {
            MockContext = BuildDbContext();
        }

        #region Data Stubs

        protected List<User> stubUserData = new List<User>
            {
                new User {Id = 1, FirstName="Test", LastName="1", Active = true },
                new User {Id = 2, FirstName="Test", LastName="1", Active = true },
                new User {Id = 3, FirstName="Test", LastName="1", Active = false },
                new User {Id = 4, FirstName="Test", LastName="1", Active = true, MiddleName="Foo" },
                new User {Id = 5, FirstName="Test", LastName="1", Active = true }
            };

        protected List<Institution> stubInstitutionData = new List<Institution>
            {
                new Institution { Id = 1, Name="Test State University" },
                new Institution { Id = 2, Name="University of Foo" }
            };

        protected List<InstitutionResident> stubInstResidentData = new List<InstitutionResident>
            {
                new InstitutionResident {Id = 1, InstitutionId=1, UserId = 1 },
                new InstitutionResident {Id = 2, InstitutionId=1, UserId = 2 },
                new InstitutionResident {Id = 3, InstitutionId=1, UserId = 3 },
                new InstitutionResident {Id = 4, InstitutionId=2, UserId = 1 },
                new InstitutionResident {Id = 5, InstitutionId=2, UserId = 4 }
            };

        protected List<ResidentShift> stubResidentShiftData = new List<ResidentShift>
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

        protected IDutyHoursDbContext BuildDbContext()
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
