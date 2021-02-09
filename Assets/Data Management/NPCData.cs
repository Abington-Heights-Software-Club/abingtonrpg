using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mono.Data.Sqlite;
using System.Data;
using System;

public class NPCData
{
    /*
     Attributes of NPC:
        name : string
        dialogue : string
     */
    public string name { get; private set; }
    public string dialogue { get; private set; }
    public NPCData(string id)
    {
        string conn = "URI=file:" + Application.dataPath + "/Data Management/data.db"; //Path to database.
        IDbConnection dbconn;
        dbconn = (IDbConnection)new SqliteConnection(conn);
        dbconn.Open(); //Open connection to the database.
        IDbCommand dbcmd = dbconn.CreateCommand();
        string sqlQuery = "SELECT npc_name, dialogue " + "FROM NPCs" + " WHERE ID = \"" + id + "\";";
        dbcmd.CommandText = sqlQuery;
        IDataReader reader = dbcmd.ExecuteReader();
        while (reader.Read())
        {
            name = reader.GetString(0);
            dialogue = reader.GetString(1);

            //Debug.Log("name = " + name + "  dialogue = " + dialogue);
        }
        reader.Close();
        reader = null;
        dbcmd.Dispose();
        dbcmd = null;
        dbconn.Close();
        dbconn = null;
    }
}
