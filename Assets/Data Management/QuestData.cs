using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mono.Data.Sqlite;
using System.Data;
using System;

public class QuestData
{
    /*
     Attributes of Quest:
        name : string
        description : string
        xp_gained : int
     */
    public string name { get; private set; }
    public string description { get; private set; }
    public int xp_gained { get; private set; }
    public QuestData(string id)
    {
        string conn = "URI=file:" + Application.dataPath + "/Data Management/data.db"; //Path to database.
        IDbConnection dbconn;
        dbconn = (IDbConnection)new SqliteConnection(conn);
        dbconn.Open(); //Open connection to the database.
        IDbCommand dbcmd = dbconn.CreateCommand();
        string sqlQuery = "SELECT quest_name, quest_description, xp_gained " + "FROM quests" + " WHERE ID = \"" + id + "\";";
        dbcmd.CommandText = sqlQuery;
        IDataReader reader = dbcmd.ExecuteReader();
        while (reader.Read())
        {
            name = reader.GetString(0);
            description = reader.GetString(1);
            xp_gained = reader.GetInt32(2);

            Debug.Log("name = " + name + "  description = " + description + "  xp_gained = " + xp_gained);
        }
        reader.Close();
        reader = null;
        dbcmd.Dispose();
        dbcmd = null;
        dbconn.Close();
        dbconn = null;
    }
}
