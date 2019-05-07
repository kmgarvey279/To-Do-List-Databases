using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace ToDoList.Models
{
  public class Item
  {
    private string _description;
    private string _dueDate;
    // private int _id;
    // private static List<Item> _instances = new List<Item> {};

    public Item (string description, string dueDate)
    {
      _description = description;
      _dueDate = dueDate;
      // _instances.Add(this);
      // _id = _instances.Count;
    }

    public string GetDescription()
    {
      return _description;
    }

    public string GetDueDate()
    {
      return _dueDate;
    }

    public void SetDescription(string newDescription)
    {
      _description = newDescription;
    }

    public void SetDueDate(string newDueDate)
    {
      _dueDate = newDueDate;
    }

    public static List<Item> GetAll()
   {
     List<Item> allItems = new List<Item> { };
     MySqlConnection conn = DB.Connection();
     conn.Open();
     MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
     cmd.CommandText = @"SELECT * FROM items;";
     MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
     while(rdr.Read())
     {
       int itemId = rdr.GetInt32(0);
       string itemDescription = rdr.GetString(1);
       string dueDate = rdr.GetString(2);
       // Line below now only provides one argument!
       Item newItem = new Item(itemDescription, dueDate);
       allItems.Add(newItem);
     }
     conn.Close();
     if (conn != null)
     {
       conn.Dispose();
     }
     return allItems;
   }

    public static void ClearAll()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM items;";
      cmd.ExecuteNonQuery();
      conn.Close();
      if (conn != null)
      {
       conn.Dispose();
      }
    }

    public static List<Item> SortDesc()
    {
      List<Item>allItemsDesc = new List<Item> { };
      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM items ORDER BY dueDate DESC;";
      while(rdr.Read())
      {
        int itemId = rdr.GetInt32(0);
        string itemDescription = rdr.GetString(1);
        string dueDate = rdr.GetString(2);
        // Line below now only provides one argument!
        Item newItem = new Item(itemDescription, dueDate);
        allItems.Add(newItem);
      }
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return allItemsDesc;
      }


    public static Item Find(int searchId)
    {
      // Temporarily returning dummy item to get beyond compiler errors, until we refactor to work with database.
      Item dummyItem = new Item("dummy item", "dummy date");
      return dummyItem;
    }
    public int GetId()
    {
      // Temporarily returning dummy id to get beyond compiler errors, until we refactor to work with database.
      return 0;
    }

    // public int GetId()
    // {
    //   return _id;
    // }

    // public static List<Item> GetAll()
    // {
    //   return _instances;
    // }
    //
    // public static void ClearAll()
    // {
    //   _instances.Clear();
    // }

    // public static Item Find(int searchId)
    // {
    //   return _instances[searchId-1];
    // }

  }
}
