using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Extensions.Configuration; //new for sql  From this namespace, we'll use the ConfigurationBuilder(); method to load appsettings.json with our app's configurations
using System.Collections.Generic;
using ToDoList.Models;
using System;

namespace ToDoList.Tests
{
  [TestClass]
  public class ItemTests : IDisposable
  {

    public IConfiguration Configuration { get; set; } //new for sql. Contains all of the data in appsettings.json
    public void Dispose()
    {
      Item.ClearAll();
    }

    public ItemTests() //new for sql
    {
      IConfigurationBuilder builder = new ConfigurationBuilder()
          .AddJsonFile("appsettings.json");
      Configuration = builder.Build();
      DBConfiguration.ConnectionString = Configuration["ConnectionStrings:TestConnection"]; //We set DBConfiguration.ConnectionString to the "TestConnection" database connection string, which we access through the Configuration object. This overrides the DBConfiguration.ConnectionString we set in DatabaseConfig.cs, ensuring that our tests are connected to our test database, not our development database
    }
    // [TestMethod]
    // public void ItemConstructor_CreatesInstanceOfItem_Item()
    // {
    //   Item newItem = new Item("test");
    //   Assert.AreEqual(typeof(Item), newItem.GetType());
    // }

    // [TestMethod]
    // public void GetDescription_ReturnsDescription_String()
    // {
    //   //Arrange
    //   string description = "Walk the dog.";

    //   //Act
    //   Item newItem = new Item(description);
    //   string result = newItem.Description;

    //   //Assert
    //   Assert.AreEqual(description, result);
    // }

    // [TestMethod]
    // public void SetDescription_SetDescription_String()
    // {
    //   //Arrange
    //   string description = "Walk the dog.";
    //   Item newItem = new Item(description);

    //   //Act
    //   string updatedDescription = "Do the dishes";
    //   newItem.Description = updatedDescription;
    //   string result = newItem.Description;

    //   //Assert
    //   Assert.AreEqual(updatedDescription, result);
    // }

    [TestMethod]
    public void GetAll_ReturnsEmptyList_ItemList()
    {
      // Arrange
      List<Item> newList = new List<Item> { };

      // Act
      List<Item> result = Item.GetAll();

      // Assert
      CollectionAssert.AreEqual(newList, result);
    }

    [TestMethod]
    public void GetAll_ReturnsItems_ItemList()
    {
      //Arrange
      string description01 = "Walk the dog";
      string description02 = "Wash the dishes";
      Item newItem1 = new Item(description01);
      newItem1.Save(); 
      Item newItem2 = new Item(description02);
      newItem2.Save();
      List<Item> newList = new List<Item> { newItem1, newItem2 };

      //Act
      List<Item> result = Item.GetAll();

      //Assert
      CollectionAssert.AreEqual(newList, result);
    }

    [TestMethod]
    public void Find_ReturnsCorrectItem_Item()
    {
      //Arrange
      string description01 = "Walk the dog";
      string description02 = "Wash the dishes";
      Item newItem1 = new Item(description01);
      newItem1.Save();
      Item newItem2 = new Item(description02);
      newItem2.Save();
      //Act
      Item foundItem = Item.Find(newItem1.Id);
      //Assert
      Assert.AreEqual(newItem2, foundItem);
    }

    [TestMethod]
    public void Equals_ReturnsTrueIfDescriptionsAreTheSame_Item()
    {
      // Arrange, Act
      Item firstItem = new Item("Mow the lawn");
      Item secondItem = new Item("Mow the lawn");

      // Assert
      Assert.AreEqual(firstItem, secondItem);
    }
    [TestMethod]
  public void Save_SavesToDatabase_ItemList()
  {
    //Arrange
    Item testItem = new Item("Mow the lawn");

    //Act
    testItem.Save();
    List<Item> result = Item.GetAll();
    List<Item> testList = new List<Item>{testItem};

    //Assert
    CollectionAssert.AreEqual(testList, result);
  }



  }
}