using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web.Mvc;
using ShopBridge.Models;
using System.Web.Routing;
using ShopBridge.Controllers;
using System.IO;
using Rhino.Mocks;
using System.Web;

namespace ShopBridge.Tests.Controllers
{
    /// <summary>
    /// Summary description for ItemControllerTest
    /// </summary>
    [TestClass]
    public class ItemControllerTest
    {
        public ItemControllerTest()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        
        int itemID;

        [TestInitialize()]
        public void MyTestInitialize()
        {
            // For all the below test cases to be executed together,
            // please assign the itemID variable with the next Identity 
            // value of 'itemID' column in table 'itemTbl'
            // Example : 
            // If the query DBCC CHECKIDENT('itemTbl') returns current value : 9,
            // Assign value 10 to the itemID variable before running the test.
            // The following test cases are lined up in a way that
            // a new Item gets created, checked and then deleted.
            // Boundary cases are also tested.
            itemID = 1;
        }

        [TestMethod]
        public void _1_CreateItem_ItemModel_ValidModelState()
        {
            //Arrange
            var controller = new ItemController();
            var context = MockRepository.GenerateStub<HttpContextBase>();
            var request = MockRepository.GenerateStub<HttpRequestBase>();
            var files = MockRepository.GenerateStub<HttpFileCollectionBase>();
            var file = MockRepository.GenerateStub<HttpPostedFileBase>();
            
            // Create mock image in Files.Request
            byte[] img = File.ReadAllBytes(Directory.GetParent(Directory.GetParent(Directory.GetCurrentDirectory()).FullName).FullName
                + @"\Resources\Image.png");
            Stream stream = new MemoryStream(img);
            // Create model
            itemTbl item = new itemTbl();
            item.itemName = "Test1";
            item.itemDescription = "Test Desc1";
            item.itemPrice = 100;

            //Act
            context.Stub(x => x.Request).Return(request);
            file.Stub(x => x.FileName).Return("ItemImage");
            file.Stub(x => x.ContentLength).Return(img.Length);
            file.Stub(x => x.InputStream).Return(stream);
            files.Stub(x => x.Count).Return(1);
            files.Stub(x => x["ItemImage"]).Return(file);
            request.Stub(x => x.Files).Return(files);
            controller.ControllerContext = new ControllerContext(context, new RouteData(), controller);

            var result = controller.Create(item) as PartialViewResult;
            var itemModel = result.Model;
            var itemList = itemModel as itemTbl;

            stream.Close();

            //Assert
            Assert.AreEqual(true, result.ViewData.ModelState.IsValid);
        }

        [TestMethod]
        public void _2_ViewItems_Void_IntItemCount()
        {
            //Arrange
            var controller = new ItemController();

            //Act
            var result = controller.ViewItems() as PartialViewResult;
            var itemModel = result.Model;
            var itemList = itemModel as List<itemTbl>;
            var count = itemList.Count;
            //itemID = itemList[0].itemID;

            //Assert
            Assert.AreEqual(1, count);
        }

        [TestMethod]
        public void _3_Delete_NonExistingItemID_RowCountNotDecremented()
        {
            //Arrange
            var controller = new ItemController();

            //Act
            var result = controller.DeleteItem(0) as PartialViewResult;
            var itemModel = result.Model;
            var itemList = itemModel as List<itemTbl>;
            var count = itemList.Count;

            //Assert
            Assert.AreEqual(1, count);
        }

        [TestMethod]
        public void _4_Details_ItemID_ItemName()
        {
            //Arrange
            var controller = new ItemController();

            //Act
            var result = controller.Details(itemID) as ViewResult;
            var itemModel = result.Model;
            var itemList = itemModel as itemTbl;

            //Assert
            Assert.AreEqual("Test1", itemList.itemName);
        }

        [TestMethod]
        public void _5_Details_ItemID_ItemPrice()
        {
            //Arrange
            var controller = new ItemController();

            //Act
            var result = controller.Details(itemID) as ViewResult;
            var itemModel = result.Model;
            var itemList = itemModel as itemTbl;

            //Assert
            Assert.AreEqual(100, itemList.itemPrice);
        }

        [TestMethod]
        public void _6_DeleteItem_ItemID_RemainingItemCount()
        {
            //Arrange
            var controller = new ItemController();

            //Act
            var result = controller.DeleteItem(itemID) as PartialViewResult;
            var itemModel = result.Model;
            var itemList = itemModel as List<itemTbl>;
            var count = itemList.Count;

            //Assert
            Assert.AreEqual(0, count);
        }

        [TestMethod]
        public void _7_Index_Void_IntItemCount()
        {
            //Arrange
            var controller = new ItemController();

            //Act
            var result = controller.Index() as ViewResult;
            var itemModel = result.Model;
            var itemList = itemModel as List<itemTbl>;
            var count = itemList.Count;

            //Assert
            Assert.AreEqual(0, count);
        }

        [TestMethod]
        public void _8_CreatePartial_Void_ViewName()
        {
            //Arrange
            var controller = new ItemController();

            //Act
            var result = controller.CreatePartial() as PartialViewResult;
            var itemModel = result.Model;
            var itemList = itemModel as itemTbl;

            //Assert
            Assert.AreEqual("_CreateItem", result.ViewName);
        }

        [TestMethod]
        public void _9_Details_NonExistingItemID_HTTPError404()
        {
            //Arrange
            var controller = new ItemController();

            //Act
            var result = controller.Details(0) as HttpNotFoundResult;

            //Assert
            Assert.AreEqual(404, result.StatusCode);
        }

        [TestMethod]
        public void _10_Details_NullItemID_HTTPBadRequest()
        {
            //Arrange
            var controller = new ItemController();

            //Act
            var result = controller.Details(null) as HttpStatusCodeResult;

            //Assert
            Assert.AreEqual(400, result.StatusCode);
        }

        [TestMethod]
        public void _11_CreateItem_FileNotUploaded_InvalidModelState()
        {
            //Arrange
            var controller = new ItemController();
            var context = MockRepository.GenerateStub<HttpContextBase>();
            var request = MockRepository.GenerateStub<HttpRequestBase>();
            var files = MockRepository.GenerateStub<HttpFileCollectionBase>();

            itemTbl item = new itemTbl();
            item.itemName = "Test1";
            item.itemDescription = "Test Desc1";
            item.itemPrice = 100;

            //Act
            context.Stub(x => x.Request).Return(request);
            files.Stub(x => x.Count).Return(0);
            request.Stub(x => x.Files).Return(files);
            controller.ControllerContext = new ControllerContext(context, new RouteData(), controller);

            var result = controller.Create(item) as PartialViewResult;
            var itemModel = result.Model;
            var itemList = itemModel as itemTbl;

            //Assert
            Assert.AreEqual(false, result.ViewData.ModelState.IsValid);
        }

        [TestMethod]
        public void _12_CreateItem_InvalidImage_InvalidModelState()
        {
            //Arrange
            var controller = new ItemController();
            var context = MockRepository.GenerateStub<HttpContextBase>();
            var request = MockRepository.GenerateStub<HttpRequestBase>();
            var files = MockRepository.GenerateStub<HttpFileCollectionBase>();

            itemTbl item = new itemTbl();
            item.itemName = "Test1";
            item.itemDescription = "Test Desc1";
            item.itemPrice = 100;

            //Act
            context.Stub(x => x.Request).Return(request);
            files.Stub(x => x.Count).Return(1);
            request.Stub(x => x.Files).Return(files);
            controller.ControllerContext = new ControllerContext(context, new RouteData(), controller);

            var result = controller.Create(item) as PartialViewResult;
            var itemModel = result.Model;
            var itemList = itemModel as itemTbl;

            //Assert
            Assert.AreEqual(false, result.ViewData.ModelState.IsValid);
        }
    }
}
