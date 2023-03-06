using System.Collections.Generic;
using MySqlConnector;
namespace ToDoList.Models
{
  public class Item
  {
    public string Description { get; set; }
    public int Id { get; }

    public Item(string description) //added this back in as an overload, like I was supposed to before.
    {
      Description = description;
    }
    public Item(string description, int id) //id wasn't added in lesson?
    {
      Description = description;
      Id = Id; //id wasn't added in lesson?
    }

    public static List<Item> GetAll()
    {
      List<Item> allItems = new List<Item> { };

      MySqlConnection conn = new MySqlConnection(DBConfiguration.ConnectionString);
      conn.Open(); //Each time we make a query, we need to open a new database connection

      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand; //Once our connection is open, we can construct our SQL query
      cmd.CommandText = "SELECT * FROM items;"; //Next, we'll add the actual text of our SQL command

      MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader; //Next, we need to create a Data Reader Object. It will be responsible for reading the data returned by our database in response to the "SELECT * FROM items;" command
      while (rdr.Read()) //A MySqlDataReader object has a built-in Read() method that reads results from the database one at a time and then advances to the next record. This method returns a boolean. If the method advances to the next object in the database, it returns true. If it reaches the end of the records that our query has returned, it returns false and our while loop ends
      {
        int itemId = rdr.GetInt32(0);
        string itemDescription = rdr.GetString(1);
        Item newItem = new Item(itemDescription, itemId);
        allItems.Add(newItem);
      } //In the while loop, we'll take each individual record from our database and translate that record into an Item object our application understands
      conn.Close(); //Communicating with a database is a resource-intensive process. For this reason, it's important to close our database connection when we're done. This allows the database to reallocate resources to respond to requests from other users. We can use a built-in Close() method to do this
      if (conn != null)
      {
        conn.Dispose();
      } //The Close() method is self-explanatory. We also include a conditional because on rare occasions, our database connection will fail to close properly. It's considered best practice to confirm it's fully closed. That's why we put the Dispose() method inside a conditional. This method will only run if conn is not null
      return allItems;
    }

    public static void ClearAll()
    {
      MySqlConnection conn = new MySqlConnection(DBConfiguration.ConnectionString); //We call new MySqlConnection(DBConfiguration.ConnectionString); to create our conn object and then call Open() on it to open the connection. Remember that DBConfiguration.ConnectionString is originally defined in DatabaseConfig.cs
      conn.Open();
      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = "DELETE FROM items;"; //Next, we'll create a new MySqlCommand object. It will include a SQL command to delete all rows from our items database table. We define the CommandText property of cmd as the SQL statement "DELETE FROM items;", which will clear the entire items table in our database.
      cmd.ExecuteNonQuery(); //SQL statements that modify data instead of querying and returning data are executed with the ExecuteNonQuery() method, as seen here. Ultimately, there are two ways we interact with databases: we can either modify or retrieve data. When we execute commands that modify the database, we use the ExecuteNonQuery() method. Commands that retrieve data use different methods like ExecuteReader(), which we used in our GetAll() method.
      conn.Close(); //Here we simply call Close() to close the connection. Our conditional confirms it's been successfully closed and destroys it if it's not.
      if (conn != null)
      {
        conn.Dispose();
      }
    }

    public static Item Find(int searchId)
    {
      // Temporarily returning placeholder item to get beyond compiler errors until we refactor to work with database.
      Item placeholderItem = new Item("placeholder item", 1);
      return placeholderItem;
    }
  }
}
