using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mono.Data.Sqlite;
using System.Data;
using System;

public class RoomData
{
    /*
     Attributes of Room:
        room_name : string
        npcs : string
        items : string
    */
    public string room_name{get; private set;}
    public string npcs{get; private set;}
    public string items{get; private set;}
    public RoomData(string id)
    {
        string conn = "URI=file:" + Application.dataPath + "/Data Management/data.db"; //Path to database.
        IDbConnection dbconn;
        dbconn = (IDbConnection)new SqliteConnection(conn);
        dbconn.Open(); //Open connection to the database.
        IDbCommand dbcmd = dbconn.CreateCommand();
        string sqlQuery = "SELECT room_name, NPCs, items " + "FROM rooms" + " WHERE ID = \"" + id + "\";";
        dbcmd.CommandText = sqlQuery;
        IDataReader reader = dbcmd.ExecuteReader();
        while(reader.Read())
        {
            room_name = reader.GetString(0);
            npcs = reader.GetString(1);
            items = reader.GetString(2);

            //Debug.Log("room_name = " + room_name + "  npcs = " + npcs + "  items = " + items)
        }
        reader.Close();
        reader = null;
        dbcmd.Dispose();
        dbcmd = null;
        dbconn.Close();
        dbconn = null;
    }
}
