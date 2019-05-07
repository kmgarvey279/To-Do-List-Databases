using Microsoft.VisualStudio.TestTools.UnitTesting;
using ToDoList.Models;
using System.Collections.Generic;
using System;

namespace ToDoList.Tests
{
  [TestClass]
  public class CategoryTest : IDisposable
  {

    public void Dispose()
    {
      Category.ClearAll();
    }

    [TestMethod]
    public void CategoryConstructor_CreatesInstanceOfCategory_Category()
    {
      Category newCategory = new Category("test category");
      Assert.AreEqual(typeof(Category), newCategory.GetType());
    }

    [TestMethod]
    public void GetName_ReturnsName_String()
    {
      string name = "Test Category";
      Category newCategory = new Category(name);
      string result = newCategory.GetName();
      Assert.AreEqual(name, result);
    }

    [TestMethod]
    public void GetId_ReturnsCategoryId_Int()
    {
      string name = "Test Category";
      Category newCategory = new Category(name);
      int result = newCategory.GetId();
      Assert.AreEqual(1, result);
    }

    [TestMethod]
    public void GetAll_ReturnsAllCategoryObjects_CategoryList()
    {
      string name01 = "Work";
      string name02 = "School";
      Category newCategory1 = new Category(name01);
      Category newCategory2 = new Category(name02);
      List<Category> newList = new List<Category> { newCategory1, newCategory2 };
      List<Category> result = Category.GetAll();
      CollectionAssert.AreEqual(newList, result);
    }

    [TestMethod]
    public void Find_ReturnsCorrectCategory_Category()
    {
      string name01 = "Work";
      string name02 = "School";
      Category newCategory1 = new Category(name01);
      Category newCategory2 = new Category(name02);
      Category result = Category.Find(2);
      Assert.AreEqual(newCategory2, result);
    }

    [TestMethod]
    public void GetItems_ReturnsEmptyItemList_ItemList()
    {
      string name = "Work";
      Category newCategory = new Category(name);
      List<Item> newList = new List<Item> { };
      List<Item> result = newCategory.GetItems();
      CollectionAssert.AreEqual(newList, result);
    }

    [TestMethod]
    public void AddItem_AssociatesItemWithCategory_ItemList()
    {
      string description = "Walk the dog.";
      string dueDate = "2019-12-04";
      Item newItem = new Item(description, dueDate);
      List<Item> newList = new List<Item> { newItem };
      string name = "Work";
      Category newCategory = new Category(name);
      newCategory.AddItem(newItem);
      List<Item> result = newCategory.GetItems();
      CollectionAssert.AreEqual(newList, result);
    }

  }
}
