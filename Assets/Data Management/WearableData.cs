using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mono.Data.Sqlite;
using System.Data;
using System;

public class WearableData
{
    /*
     Attributes of Wearable:
        wearable_name : string
        wearable_description : string
        armor_amount : int
    */
    public string wearable_name{get; private set;}
    public string wearable_description{get; private set;}
    public int armor_amount{get; private set;}
    public WearableData(string id)
    {
        string conn = "URI=file:" + Application.dataPath + "/Data Management/data.db"; //Path to database.
        IDbConnection dbconn;
        dbconn = (IDbConnection)new SqliteConnection(conn);
        dbconn.Open(); //Open connection to the database.
        IDbCommand dbcmd = dbconn.CreateCommand();
        string sqlQuery = "SELECT wearable_name, wearable_description, armor_amount " + "FROM wearables" + " WHERE ID = \"" + id + "\";";
        dbcmd.CommandText = sqlQuery;
        IDataReader reader = dbcmd.ExecuteReader();
        while(reader.Read())
        {
            wearable_name = reader.GetString(0);
            wearable_description = reader.GetString(1);
            armor_amount = reader.GetInt32(2);

            //Debug.Log("wearable_name = " + wearable_name + "  wearable_description = " + wearable_description + "  armor_amount = " + armor_amount)
        }
        reader.Close();
        reader = null;
        dbcmd.Dispose();
        dbcmd = null;
        dbconn.Close();
        dbconn = null;
    }
}
