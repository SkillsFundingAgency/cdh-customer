﻿using System;
using System.Collections.Specialized;
using System.IO;
using System.Net;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using NCS.DSS.Customer.Cosmos.Provider;
using NCS.DSS.Customer.PostCustomerHttpTrigger.Service;
using NSubstitute;
using NUnit.Framework;

namespace NCS.DSS.Customer.Tests.ServiceTests
{

    [TestFixture]
    public class PostCustomerHttpTriggerTests
    {
        private IPostCustomerHttpTriggerService _customerHttpTriggerService;
        private IDocumentDBProvider _documentDbProvider;
        private Models.Customer _customer;

        [SetUp]
        public void Setup()
        {
            _documentDbProvider = Substitute.For<IDocumentDBProvider>();
            _customerHttpTriggerService = Substitute.For<PostCustomerHttpTriggerService>(_documentDbProvider);
            _customer = Substitute.For<Models.Customer>();
        }

        [Test]
        public async Task PostCustomersHttpTriggerServiceTests_CreateAsync_ReturnsNullWhenCustomerJsonIsNullOrEmpty()
        {
            // Act
            var result = await _customerHttpTriggerService.CreateNewCustomerAsync(null);

            // Assert
            Assert.IsNull(result);
        }

        [Test]
        public async Task PostCustomersHttpTriggerServiceTests_CreateAsync_ReturnsResourceWhenUpdated()
        {
            const string documentServiceResponseClass = "Microsoft.Azure.Documents.DocumentServiceResponse, Microsoft.Azure.DocumentDB.Core, Version=2.2.1.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35";
            const string dictionaryNameValueCollectionClass = "Microsoft.Azure.Documents.Collections.DictionaryNameValueCollection, Microsoft.Azure.DocumentDB.Core, Version=2.2.1.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35";

            var resourceResponse = new ResourceResponse<Document>(new Document());
            var documentServiceResponseType = Type.GetType(documentServiceResponseClass);

            const BindingFlags flags = BindingFlags.NonPublic | BindingFlags.Instance;

            var headers = new NameValueCollection { { "x-ms-request-charge", "0" } };

            var headersDictionaryType = Type.GetType(dictionaryNameValueCollectionClass);

            var headersDictionaryInstance = Activator.CreateInstance(headersDictionaryType, headers);

            var arguments = new[] { Stream.Null, headersDictionaryInstance, HttpStatusCode.Created, null };

            var documentServiceResponse = documentServiceResponseType.GetTypeInfo().GetConstructors(flags)[0].Invoke(arguments);

            var responseField = typeof(ResourceResponse<Document>).GetTypeInfo().GetField("response", flags);

            responseField?.SetValue(resourceResponse, documentServiceResponse);

            _documentDbProvider.CreateCustomerAsync(_customer).Returns(Task.FromResult(resourceResponse).Result);

            // Act
            var result = await _customerHttpTriggerService.CreateNewCustomerAsync(_customer);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<Models.Customer>(result);

        }

    }
}