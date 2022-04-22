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
using System.Threading.Tasks;
using BangOnERP.GlobalEncryptionDecryption;

namespace ERPLoginTester.Controller
{
    
        [TestClass]
        public class LoginControllerTester
        {
            private readonly IFixture _fixture;
            private readonly Mock<LoginI> _loginmock;
            private readonly LoginController _loginController;

            public LoginControllerTester()
            {
                _fixture = new Fixture();
                _loginmock = _fixture.Freeze<Mock<LoginI>>();
                _loginController = new LoginController(_loginmock.Object);  //Creates the implementation in memory
            }


            [TestMethod]
            public async Task GetAllContentResultMatcher()
            {

                //Arrange
                var loginmock = _fixture.Create<List<LoginUser>>();
                _loginmock.Setup(x => x.GetAll()).ReturnsAsync(loginmock);

                //Act
                var result =await _loginController.Get().ConfigureAwait(false);

                //Assert
                Assert.IsInstanceOfType(result, typeof(Microsoft.AspNetCore.Mvc.OkObjectResult));
                Assert.IsNotNull(result);
                _loginmock.Verify(x => x.GetAll(), Times.Once());
            }

            [TestMethod]
            public async Task GetAllContentResultNotFound()
            {

                //Arrange

                List<LoginUser> expected = null;
                _loginmock.Setup(x => x.GetAll()).ReturnsAsync(expected);

                //Act
                var result =await _loginController.Get().ConfigureAwait(false);

                //Assert
                Assert.IsInstanceOfType(result, typeof(Microsoft.AspNetCore.Mvc.NotFoundResult));
                _loginmock.Verify(x => x.GetAll(), Times.Once());
            }
        
            [TestMethod]
            public async Task GetByIdContentResultFound()
            {

                //Arrange
                var loginmock = _fixture.Create<VwLoginLoginUser>();
                var id = _fixture.Create<int>();
                _loginmock.Setup(x => x.Edited(id.ToString())).ReturnsAsync(loginmock);

                //Act
                var result = await _loginController.Get(id.ToString()).ConfigureAwait(false);

                //Assert
                Assert.IsInstanceOfType(result, typeof(Microsoft.AspNetCore.Mvc.OkObjectResult));
                _loginmock.Verify(x => x.Edited(id.ToString()), Times.Once());
            }

            [TestMethod]
            public async Task GetByIdContentResultNotFound()
            {
                //Arrange
                var loginmock = _fixture.Create<VwLoginLoginUser>();
                var id = 0;
                _loginmock.Setup(x => x.Edited(id.ToString())).ReturnsAsync(loginmock);

                //Act
                var result = await _loginController.Get(id.ToString()).ConfigureAwait(false);

                //Assert
                Assert.IsInstanceOfType(result, typeof(Microsoft.AspNetCore.Mvc.BadRequestObjectResult));
                _loginmock.Verify(x => x.Edited(id.ToString()), Times.Never());
            }

            [TestMethod]
            public async Task CreateLogin_ShouldReturn_WhenValidRequest()
            {

                //Arrange
                _fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
                _fixture.Behaviors.Add(new OmitOnRecursionBehavior());
                var loginmock = _fixture.Create<LoginSuperLogin>();
                var response= _fixture.Create<string>();
                _loginmock.Setup(x => x.Inserted(loginmock)).ReturnsAsync(response);

                //Act
                var result = await _loginController.Post(loginmock).ConfigureAwait(false);

                //Assert
                Assert.IsInstanceOfType(result, typeof(Microsoft.AspNetCore.Mvc.CreatedAtRouteResult));
                _loginmock.Verify(x => x.Inserted(loginmock), Times.Once());
            }

            [TestMethod]
            public async Task CreateLogin_ShouldReturn_WhenInvalidRequest()
            {

                //Arrange
                _fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
                _fixture.Behaviors.Add(new OmitOnRecursionBehavior());
                var loginmock = _fixture.Create<LoginSuperLogin>();
                _loginController.ModelState.AddModelError("LoginActive", "LoginActiveRequired");
                var response = _fixture.Create<string>();
                _loginmock.Setup(x => x.Inserted(loginmock)).ReturnsAsync(response);

                //Act
                var result = await _loginController.Post(loginmock).ConfigureAwait(false);

                //Assert
                Assert.IsInstanceOfType(result, typeof(Microsoft.AspNetCore.Mvc.BadRequestObjectResult));
                _loginmock.Verify(x => x.Inserted(loginmock), Times.Never());
            }

        [TestMethod]
        public async Task UpdateLogin_ShouldReturn_WhenValidRequest()
        {

            //Arrange
            var loginmock = _fixture.Create<LoginUser>();
            var response = _fixture.Create<string>();
            _loginmock.Setup(x => x.Updated(loginmock)).ReturnsAsync(response);

            //Act
            var result = await _loginController.Put(loginmock).ConfigureAwait(false);

            //Assert
            Assert.IsInstanceOfType(result, typeof(Microsoft.AspNetCore.Mvc.OkObjectResult));
            _loginmock.Verify(x => x.Updated(loginmock), Times.Once());
        }

        [TestMethod]
        public async Task DeleteLogin_ShouldReturn_WhenValidRequest()
        {

            //Arrange
            var loginmock = _fixture.Create<List<LoginUser>>();
            var id = _fixture.Create<string>();
            _loginmock.Setup(x => x.Deleted(id)).ReturnsAsync(loginmock);

            //Act
            var result = await _loginController.Delete(id).ConfigureAwait(false);

            //Assert
            Assert.IsInstanceOfType(result, typeof(Microsoft.AspNetCore.Mvc.OkObjectResult));
            _loginmock.Verify(x => x.Deleted(id), Times.Once());
        }

        [TestMethod]
        public async Task DeleteLogin_ShouldNotReturn_WhenInValidRequest()
        {

            //Arrange
            var loginmock = _fixture.Create<List<LoginUser>>();
            var id = "";
            _loginmock.Setup(x => x.Deleted(id)).ReturnsAsync(loginmock);

            //Act
            var result = await _loginController.Delete(id).ConfigureAwait(false);

            //Assert
            Assert.IsInstanceOfType(result, typeof(Microsoft.AspNetCore.Mvc.BadRequestObjectResult));
            _loginmock.Verify(x => x.Deleted(id), Times.Never());
        }
    }
}
