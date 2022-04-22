using Microsoft.VisualStudio.TestTools.UnitTesting;
using ERPLoginModule;
using ERPLoginModule.Controllers;
using ERPLoginModule.Service;
using Microsoft.AspNetCore.Mvc;
using BangOnERP.DataBase;
using AutoMapper;
using Moq;
using AutoFixture;
using System.Collections.Generic;
using System.Web;
using ERPLoginModule.Service.Contextual;

namespace ERPLoginTester.Controller
{
    [TestClass]
    class LoginControllerTest
    {
        private readonly IFixture _fixture;
        private readonly Mock<LoginI> _loginmock;
        private readonly LoginController _loginController;

      public  LoginControllerTest()
        {
            _fixture = new Fixture();
            _loginmock = _fixture.Freeze<Mock<LoginI>>();
            _loginController = new LoginController(_loginmock.Object);  //Creates the implementation in memory
        }

        [TestMethod]
        public void GetAllContentResultMatcher()
        {

            //Arrange
            var loginmock = _fixture.Create<List<LoginUser>>();
            _loginmock.Setup(x => x.GetAll()).ReturnsAsync(loginmock);

            //Act
            var result =_loginController.Get();

            //Assert
            Assert.IsInstanceOfType(result, typeof(Microsoft.AspNetCore.Mvc.OkObjectResult));
        }

        [TestMethod]
        public void GetByIdContentResultMatcher()
        {

            //Arrange
            var loginmock = _fixture.Create<List<LoginUser>>();
            _loginmock.Setup(x => x.GetAll()).ReturnsAsync(loginmock);

            //Act
            var result = _loginController.Get();

            //Assert
            Assert.IsInstanceOfType(result, typeof(Microsoft.AspNetCore.Mvc.OkObjectResult));
        }

    }
}
