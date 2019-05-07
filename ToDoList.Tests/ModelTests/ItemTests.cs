using Microsoft.VisualStudio.TestTools.UnitTesting;
using ToDoList.Models;
using System.Collections.Generic;
using System;

namespace ToDoList.Tests
{
  [TestClass]
  public class ItemTest : IDisposable
  {
      public void Dispose()
      {
        Item.ClearAll();
      }

      public ItemTest()
      {
        DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=to_do_list_test;";
      }


    [TestMethod]
    public void ItemConstructor_CreatesInstanceOfItem_Item()
    {
      string description = "Walk the dog.";
      string dueDate = "2019-12-04";
      Item newItem = new Item(description, dueDate);
      Assert.AreEqual(typeof(Item), newItem.GetType());
    }

    [TestMethod]
    public void GetDescription_ReturnsDescription_String()
    {
      string description = "Walk the dog.";
      string dueDate = "2019-12-04";
      Item newItem = new Item(description, dueDate);
      string result = newItem.GetDescription();
      Assert.AreEqual(description, result);
    }

    [TestMethod]
    public void SetDescription_SetDescription_String()
    {
      string description = "Walk the dog.";
      string dueDate = "2019-12-04";
      Item newItem = new Item(description, dueDate);
      string updatedDescription = "Do the dishes";
      newItem.SetDescription(updatedDescription);
      string result = newItem.GetDescription();
      Assert.AreEqual(updatedDescription, result);
    }

    [TestMethod]
    public void GetDueDate_ReturnsDueDate_String()
    {
      string description = "Walk the dog.";
      string dueDate = "2019-12-04";
      Item newItem = new Item(description, dueDate);
      string result = newItem.GetDueDate();
      Assert.AreEqual(dueDate, result);
    }

    [TestMethod]
    public void SetDueDate_SetDueDate_String()
    {
      string description = "Walk the dog.";
      string dueDate = "2019-12-04";
      Item newItem = new Item(description, dueDate);
      string updatedDueDate = "Do the dishes";
      newItem.SetDueDate(updatedDueDate);
      string result = newItem.GetDueDate();
      Assert.AreEqual(updatedDueDate, result);
    }

    [TestMethod]
    public void GetAll_ReturnsEmptyListFromDatabase_ItemList()
    {
      List<Item> newList = new List<Item> { };
      List<Item> result = Item.GetAll();
      CollectionAssert.AreEqual(newList, result);
    }
    //
    // [TestMethod]
    // public void GetAll_ReturnsItems_ItemList()
    // {
    //   string description01 = "Walk the dog";
    //   string description02 = "Wash the dishes";
    //   Item newItem1 = new Item(description01);
    //   Item newItem2 = new Item(description02);
    //   List<Item> newList = new List<Item> { newItem1, newItem2 };
    //   List<Item> result = Item.GetAll();
    //   CollectionAssert.AreEqual(newList, result);
    // }
    //
    // [TestMethod]
    // public void GetId_ItemsInstantiateWithAnIdAndGetterReturns_Int()
    // {
    //   string description = "Walk the dog.";
    //   Item newItem = new Item(description);
    //   int result = newItem.GetId();
    //   Assert.AreEqual(1, result);
    // }
    //
    // [TestMethod]
    // public void Find_ReturnsCorrectItem_Item()
    // {
    //   string description01 = "Walk the dog";
    //   string description02 = "Wash the dishes";
    //   Item newItem1 = new Item(description01);
    //   Item newItem2 = new Item(description02);
    //   Item result = Item.Find(2);
    //   Assert.AreEqual(newItem2, result);
    // }
  }
}
