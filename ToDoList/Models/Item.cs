using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System;

namespace ToDoList.Models
{
  public class Item
  {
    private string _description;
    private DateTime _dueDate;
    private int _id;

    public Item (string description, DateTime dueDate, int id = 0)
    {
      _description = description;
      _dueDate = dueDate;
      _id = id;
    }

    public string GetDescription()
    {
      return _description;
    }

    public DateTime GetDueDate()
    {
      return _dueDate;
    }

    public void SetDescription(string newDescription)
    {
      _description = newDescription;
    }

    public void SetDueDate(DateTime newDueDate)
    {
      _dueDate = newDueDate;
    }

    public int GetId ()
    {
      return _id;
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
       DateTime dueDate = rdr.GetDateTime(2);
       // Line below now only provides one argument!
       Item newItem = new Item(itemDescription, dueDate, itemId);
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
      MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
      while(rdr.Read())
      {
        int itemId = rdr.GetInt32(0);
        string itemDescription = rdr.GetString(1);
        DateTime dueDate = rdr.GetDateTime(2);
        Item newItem = new Item(itemDescription, dueDate);
        allItemsDesc.Add(newItem);
      }
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return allItemsDesc;
      }


    public static Item Find(int id)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM 'items' WHERE id = @thisId;";
      MySqlParameter thisId = new MySqlParameter();
      thisId.ParameterName = "@thisId";
      thisId.Value = id;
      cmd.Parameters.Add(thisId);
      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      int itemId = 0;
      string itemDescription = "";
      DateTime itemDueDate = new DateTime();
      while (rdr.Read())
      {
        itemId = rdr.GetInt32(0);
        itemDescription = rdr.GetString(1);
        itemDueDate = rdr.GetDateTime(2);
      }
      Item foundItem= new Item(itemDescription, itemDueDate, itemId);
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return foundItem;
    }

    public override bool Equals(System.Object otherItem)
    {
      if (!(otherItem is Item))
      {
        return false;
      }
      else
      {
        Item newItem = (Item) otherItem;
        bool idEquality = (this.GetId() == newItem.GetId());
        bool descriptionEquality = (this.GetDescription() == newItem.GetDescription());
        bool dueDateEquality = (this.GetDueDate() == newItem.GetDueDate());
        return (idEquality && descriptionEquality && dueDateEquality);
      }
    }

    public void Save()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"INSERT INTO items (description) VALUES (@ItemDescription);";
      MySqlParameter description = new MySqlParameter();
      description.ParameterName = "@ItemDescription";
      description.Value = this._description;
      cmd.Parameters.Add(description);
      cmd.CommandText = @"INSERT INTO items (dueDate) VALUES (@ItemDueDate);";
      MySqlParameter dueDate = new MySqlParameter();
      dueDate.ParameterName = "@ItemDueDate";
      dueDate.Value = this._dueDate;
      cmd.Parameters.Add(dueDate);
      cmd.ExecuteNonQuery();
      _id = (int) cmd.LastInsertedId;
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }

  }
}
