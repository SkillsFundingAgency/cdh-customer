﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using NCS.DSS.Customer.ReferenceData;
using NCS.DSS.Customer.Validation;
using NUnit.Framework;

namespace NCS.DSS.Customer.Tests.ValidationTests
{
    [TestFixture]
    public class ValidateTests
    {
        private IValidate _validate;

        [SetUp]
        public void Setup()
        {
            _validate = new Validate();
        }

        [Test]
        public void ValidateTests_ReturnValidationResult_WhenGivenNameAndFamilyNameIsNotPopulatedForPost()
        {
            var customer = new Models.Customer();

            var result = _validate.ValidateResource(customer, true);

            // Assert
            Assert.IsInstanceOf<List<ValidationResult>>(result);
            Assert.IsNotNull(result);
            //Changed to 5 as PriorityGroups are now required
            Assert.AreEqual(5, result.Count);
        }

        [Test]
        public void ValidateTests_ReturnValidationResult_WhenGivenNameIsNotPopulatedForPost()
        {
            var customer = new Models.Customer { GivenName = "John", PriorityGroups = new List<PriorityCustomer> { PriorityCustomer.AdultsWhoHaveBeenUnemployedForMoreThan12Months } };

            var result = _validate.ValidateResource(customer, true);

            // Assert
            Assert.IsInstanceOf<List<ValidationResult>>(result);
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count);
        }

        [Test]
        public void ValidateTests_ReturnValidationResult_WhenFamilyNameIsNotPopulatedForPost()
        {
            var customer = new Models.Customer { FamilyName = "Smith", PriorityGroups = new List<PriorityCustomer> { PriorityCustomer.AdultsWhoHaveBeenUnemployedForMoreThan12Months } };

            var result = _validate.ValidateResource(customer, true);

            // Assert
            Assert.IsInstanceOf<List<ValidationResult>>(result);
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count);
        }

        [Test]
        public void ValidateTests_ReturnValidationResult_WhenDateOfTerminationNotPopulatedButReasonForTerminationHasAValueForPost()
        {
            var customer = new Models.Customer { GivenName = "John", FamilyName = "Smith", ReasonForTermination = ReasonForTermination.CustomerChoice, PriorityGroups = new List<PriorityCustomer> { PriorityCustomer.AdultsWhoHaveBeenUnemployedForMoreThan12Months } };

            var result = _validate.ValidateResource(customer, true);

            // Assert
            Assert.IsInstanceOf<List<ValidationResult>>(result);
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count);
        }

        [Test]
        public void ValidateTests_ReturnValidationResult_WhenUniqueLearnerNumberIsNotValid()
        {
            var customer = new Models.Customer { GivenName = "John", FamilyName = "Smith", UniqueLearnerNumber = "10000000000", PriorityGroups = new List<PriorityCustomer> { PriorityCustomer.AdultsWhoHaveBeenUnemployedForMoreThan12Months } };            

            var result = _validate.ValidateResource(customer, false);

            // Assert
            Assert.IsInstanceOf<List<ValidationResult>>(result);
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count);
        }

        [Test]
        public void ValidateTests_ReturnNoValidationResult_WhenUniqueLearnerNumberIsValid()
        {
            var customer = new Models.Customer { GivenName = "John", FamilyName = "Smith", UniqueLearnerNumber = "5000000000", PriorityGroups = new List<PriorityCustomer> { PriorityCustomer.AdultsWhoHaveBeenUnemployedForMoreThan12Months } };

            var result = _validate.ValidateResource(customer, false);

            // Assert
            Assert.IsInstanceOf<List<ValidationResult>>(result);
            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Count);
        }

        [Test]
        public void ValidateTests_ReturnValidationResult_WhenDateOfRegistrationIsInTheFuture()
        {
            var customer = new Models.Customer { GivenName = "John", FamilyName = "Smith", DateOfRegistration = DateTime.MaxValue, PriorityGroups = new List<PriorityCustomer> { PriorityCustomer.AdultsWhoHaveBeenUnemployedForMoreThan12Months } };

            var result = _validate.ValidateResource(customer, false);

            // Assert
            Assert.IsInstanceOf<List<ValidationResult>>(result);
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count);
        }

        [Test]
        public void ValidateTests_ReturnValidationResult_WhenUserIsYoungerThan13()
        {
            var customer = new Models.Customer { GivenName = "John", FamilyName = "Smith", DateofBirth = DateTime.UtcNow.AddYears(-12), PriorityGroups = new List<PriorityCustomer> { PriorityCustomer.AdultsWhoHaveBeenUnemployedForMoreThan12Months } };

            var result = _validate.ValidateResource(customer, false);

            // Assert
            Assert.IsInstanceOf<List<ValidationResult>>(result);
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count);
        }

        [Test]
        public void ValidateTests_ReturnValidationResult_WhenTitleIsNotValid()
        {
            var customer = new Models.Customer { GivenName = "John", FamilyName = "Smith", Title = (Title) 100, PriorityGroups = new List<PriorityCustomer> { PriorityCustomer.AdultsWhoHaveBeenUnemployedForMoreThan12Months } };

            var result = _validate.ValidateResource(customer, false);

            // Assert
            Assert.IsInstanceOf<List<ValidationResult>>(result);
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count);
        }

        [Test]
        public void ValidateTests_ReturnValidationResult_WhenGenderIsNotValid()
        {
            var customer = new Models.Customer { GivenName = "John", FamilyName = "Smith", Gender = (Gender) 100, PriorityGroups = new List<PriorityCustomer> { PriorityCustomer.AdultsWhoHaveBeenUnemployedForMoreThan12Months } };

            var result = _validate.ValidateResource(customer, false);

            // Assert
            Assert.IsInstanceOf<List<ValidationResult>>(result);
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count);
        }

        [Test]
        public void ValidateTests_ReturnValidationResult_WhenReasonForTerminationIsNotValid()
        {
            var customer = new Models.Customer { GivenName = "John", FamilyName = "Smith", ReasonForTermination = (ReasonForTermination) 100, DateOfTermination = DateTime.Now, PriorityGroups = new List<PriorityCustomer> { PriorityCustomer.AdultsWhoHaveBeenUnemployedForMoreThan12Months } };

            var result = _validate.ValidateResource(customer, false);

            // Assert
            Assert.IsInstanceOf<List<ValidationResult>>(result);
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count);
        }

        [Test]
        public void ValidateTests_ReturnValidationResult_WhenIntroducedByIsNotValid()
        {
            var customer = new Models.Customer { GivenName = "John", FamilyName = "Smith", IntroducedBy = (IntroducedBy) 100, PriorityGroups = new List<PriorityCustomer> { PriorityCustomer.AdultsWhoHaveBeenUnemployedForMoreThan12Months } };

            var result = _validate.ValidateResource(customer, false);

            // Assert
            Assert.IsInstanceOf<List<ValidationResult>>(result);
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count);
        }

        [Test]
        public void ValidateTests_ReturnValidationResult_WhenDateOfTerminationIsInTheFuture()
        {
            var customer = new Models.Customer { GivenName = "John", FamilyName = "Smith", DateOfTermination = DateTime.MaxValue, PriorityGroups = new List<PriorityCustomer> { PriorityCustomer.AdultsWhoHaveBeenUnemployedForMoreThan12Months } };

            var result = _validate.ValidateResource(customer, false);

            // Assert
            Assert.IsInstanceOf<List<ValidationResult>>(result);
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count);
        }

        [Test]
        public void ValidateTests_ReturnValidationResult_WhenLastModifiedDateIsInTheFuture()
        {
            var customer = new Models.Customer { GivenName = "John", FamilyName = "Smith", LastModifiedDate = DateTime.MaxValue, PriorityGroups = new List<PriorityCustomer> { PriorityCustomer.AdultsWhoHaveBeenUnemployedForMoreThan12Months } };

            var result = _validate.ValidateResource(customer, false);

            // Assert
            Assert.IsInstanceOf<List<ValidationResult>>(result);
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count);
        }

    }
}