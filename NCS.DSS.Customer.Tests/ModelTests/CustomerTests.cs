﻿using System;
using NCS.DSS.Customer.ReferenceData;
using NUnit.Framework;

namespace NCS.DSS.Customer.Tests.ModelTests
{
    [TestFixture]
    public class CustomerTests
    {
        [Test]
        public void CustomerTests_PopulatesDefaultValues_WhenSetDefaultValuesIsCalled()
        {
            var customer = new Models.Customer();
            customer.SetDefaultValues();

            // Assert
            Assert.IsNotNull(customer.DateOfRegistration);
            Assert.IsNotNull(customer.LastModifiedDate);
            Assert.AreEqual(false, customer.OptInUserResearch);
            Assert.AreEqual(false, customer.OptInMarketResearch);
            Assert.AreEqual(Title.NotProvided, customer.Title);
            Assert.AreEqual(Gender.NotProvided, customer.Gender);
            Assert.AreEqual(IntroducedBy.NotProvided, customer.IntroducedBy);
        }

        [Test]
        public void CustomerTests_CheckDateOfRegistrationDoesNotGetPopulated_WhenSetDefaultValuesIsCalled()
        {
            var customer = new Models.Customer {DateOfRegistration = DateTime.MaxValue};
            
            customer.SetDefaultValues();

            // Assert
            Assert.AreEqual(DateTime.MaxValue, customer.DateOfRegistration);
        }

        [Test]
        public void CustomerTests_CheckLastModifiedDateDoesNotGetPopulated_WhenSetDefaultValuesIsCalled()
        {
            var customer = new Models.Customer {LastModifiedDate = DateTime.MaxValue};
            
            customer.SetDefaultValues();

            // Assert
            Assert.AreEqual(DateTime.MaxValue, customer.LastModifiedDate);
        }

        [Test]
        public void CustomerTests_CheckOptInUserResearchDoesNotGetPopulated_WhenSetDefaultValuesIsCalled()
        {
            var customer = new Models.Customer { OptInUserResearch = true };

            customer.SetDefaultValues();

            // Assert
            Assert.AreEqual(true, customer.OptInUserResearch);
        }

        [Test]
        public void CustomerTests_CheckOptInMarketResearchDoesNotGetPopulated_WhenSetDefaultValuesIsCalled()
        {
            var customer = new Models.Customer { OptInMarketResearch = true };

            customer.SetDefaultValues();

            // Assert
            Assert.AreEqual(true, customer.OptInMarketResearch);
        }

        [Test]
        public void CustomerTests_CheckTitleDoesNotGetPopulated_WhenSetDefaultValuesIsCalled()
        {
            var customer = new Models.Customer { Title = Title.Mr };

            customer.SetDefaultValues();

            // Assert
            Assert.AreEqual(Title.Mr, customer.Title);
        }

        [Test]
        public void CustomerTests_CheckGenderDoesNotGetPopulated_WhenSetDefaultValuesIsCalled()
        {
            var customer = new Models.Customer { Gender = Gender.Male };

            customer.SetDefaultValues();

            // Assert
            Assert.AreEqual(Gender.Male, customer.Gender);
        }

        [Test]
        public void CustomerTests_CheckIntroducedByDoesNotGetPopulated_WhenSetDefaultValuesIsCalled()
        {
            var customer = new Models.Customer { IntroducedBy = IntroducedBy .Charity};

            customer.SetDefaultValues();

            // Assert
            Assert.AreEqual(IntroducedBy.Charity, customer.IntroducedBy);
        }

        [Test]
        public void CustomerTests_CheckReasonForTerminationIsPopulated_WhenSetDefaultValuesIsCalled()
        {
            var customer = new Models.Customer { DateOfTermination = DateTime.UtcNow };

            customer.SetDefaultValues();

            // Assert
            Assert.AreEqual(ReasonForTermination.Other, customer.ReasonForTermination);
        }

       
    }
}