// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace Azure.Functions.NodeJs.Tests.E2E
{
    public class HttpEndToEndTests 
    {
        [Theory]
        [InlineData("HttpTrigger", "?name=Test", HttpStatusCode.OK, "Hello Test")]
        [InlineData("HttpTrigger", "?name=John&lastName=Doe", HttpStatusCode.OK, "Hello John")]
        [InlineData("HttpTriggerThrows", "", HttpStatusCode.InternalServerError, "")]
        [InlineData("HttpTrigger", "", HttpStatusCode.BadRequest, "Please pass a name on the query string or in the request body")]
        public async Task HttpTriggerTests(string functionName, string queryString, HttpStatusCode expectedStatusCode, string expectedMessage)
        {
            // TODO: Verify exception on 500 after https://github.com/Azure/azure-functions-host/issues/3589
            Assert.True(await Utilities.InvokeHttpTrigger(functionName, queryString, expectedStatusCode, expectedMessage));
        }
    }
}