using GSI.Controllers;
using GSI.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GSI.Tests
{
    public class HomeControllerTests
    {
        [TestClass]
        public class ProductControllerTest
        {
            private readonly InventarioproyectogsiContext _context;

            [TestMethod]
            public void TestDetailsView()
            {
                var controller = new ProductoController(_context);
                var result = controller.Details(2) as ViewResult;
                Assert.AreEqual("Details", result.ViewName);

            }
        }
    }
}