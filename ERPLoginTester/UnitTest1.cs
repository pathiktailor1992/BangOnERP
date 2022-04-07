using Microsoft.VisualStudio.TestTools.UnitTesting;
using ERPLoginModule;
using ERPLoginModule.Controllers;
using ERPLoginModule.Service;
using Microsoft.AspNetCore.Mvc;
using BangOnERP.DataBase;
using AutoMapper;
using Moq;
using System.Collections.Generic;

namespace ERPLoginTester
{
    [TestClass]
    public class UnitTest1
    {
      //  private LoginI loginRepo;
        private readonly IMapper _mapper;
        private readonly Mock<LoginRepository> loginRepo;
        private readonly LoginController _controller;

        public UnitTest1()
        {
            
        }

        [TestMethod]
        public void TestMethod1()
        {

            //Arrange
            var mockRepo = new Mock<LoginRepository>();
            mockRepo.Setup(repo => repo.GetAll());
               // .ReturnsAsync(GetTestSessions());
            var controller = new LoginController(mockRepo.Object);

            //Act
            // var data = controller.Get();

            //Assert
            Assert.AreEqual(1,0, "Values not matching correctly");
        }

      
    }
}
