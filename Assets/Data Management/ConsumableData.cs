using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mono.Data.Sqlite;
using System.Data;
using System;

public class ConsumableData
{
    /*
     Attributes of Consumable:
        consumable_name : string
        consumable_description : string
        health_restore : int
        energy_restore : int
    */
    public string consumable_name{get; private set;}
    public string consumable_description{get; private set;}
    public int health_restore{get; private set;}
    public int energy_restore{get; private set;}
    public ConsumableData(string id)
    {
        string conn = "URI=file:" + Application.dataPath + "/Data Management/data.db"; //Path to database.
        IDbConnection dbconn;
        dbconn = (IDbConnection)new SqliteConnection(conn);
        dbconn.Open(); //Open connection to the database.
        IDbCommand dbcmd = dbconn.CreateCommand();
        string sqlQuery = "SELECT consumable_name, consumable_description, health_restore, energy_restore " + "FROM consumables" + " WHERE ID = \"" + id + "\";";
        dbcmd.CommandText = sqlQuery;
        IDataReader reader = dbcmd.ExecuteReader();
        while(reader.Read())
        {
            consumable_name = reader.GetString(0);
            consumable_description = reader.GetString(1);
            health_restore = reader.GetInt32(2);
            energy_restore = reader.GetInt32(3);

            //Debug.Log("consumable_name = " + consumable_name + "  consumable_description = " + consumable_description + "  health_restore = " + health_restore + "  energy_restore = " + energy_restore)
        }
        reader.Close();
        reader = null;
        dbcmd.Dispose();
        dbcmd = null;
        dbconn.Close();
        dbconn = null;
    }
}
