using System.Collections.Generic;
using CustomerSupportPortal.Controllers;
using CustomerSupportPortal.Models;
using CustomerSupportPortal.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using Moq;
using Xunit;

namespace CustomerSupportPortal.Tests.Controllers
{
    public class AgentControllerTests
    {
        [Fact]
        public void Index_Returns_ViewResult()
        {
            var repoMock = new Mock<ISupportRequestRepository>();
            var controller = new AgentController(repoMock.Object);

            var result = controller.Index();

            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void AgentDashboard_Returns_ViewResult()
        {
            var repoMock = new Mock<ISupportRequestRepository>();
            var controller = new AgentController(repoMock.Object);

            var result = controller.AgentDashboard();

            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void GetPriorities_Returns_JsonResult_With_List()
        {
            var priorities = new List<Priority> { new Priority { PriorityId = 1, PriorityType = "High" } };
            var repoMock = new Mock<ISupportRequestRepository>();
            repoMock.Setup(r => r.GetPriorities()).Returns(priorities);

            var controller = new AgentController(repoMock.Object);

            var result = controller.GetPriorities();

            var json = Assert.IsType<JsonResult>(result);
            Assert.Equal(priorities, json.Value);
            repoMock.Verify(r => r.GetPriorities(), Times.Once);
        }

        [Fact]
        public void GetAllRequest_Uses_Repository_And_Returns_Json()
        {
            var requestResult = new RequestResult
            {
                TotalRecords = 1,
                Data = new List<RequestDetails> { new RequestDetails { Subject = "Test" } }
            };

            var repoMock = new Mock<ISupportRequestRepository>();
            repoMock.Setup(r => r.GetAllRequests(0, 10, "")).Returns(requestResult);

            var controller = new AgentController(repoMock.Object);

            // Setup HttpContext and Form values expected by the action
            var context = new DefaultHttpContext();
            var formDict = new Dictionary<string, StringValues>
            {
                ["search[value]"] = "",
                ["draw"] = "1"
            };
            context.Request.Form = new FormCollection(formDict);
            controller.ControllerContext = new ControllerContext
            {
                HttpContext = context
            };

            var result = controller.GetAllRequest(0, 10);

            var json = Assert.IsType<JsonResult>(result);
            // verify returned shape contains expected values
            var obj = json.Value as dynamic;
            Assert.Equal("1", (string)obj.draw);
            Assert.Equal(requestResult.TotalRecords, (int)obj.recordsTotal);
            repoMock.Verify(r => r.GetAllRequests(0, 10, ""), Times.Once);
        }
    }
}
