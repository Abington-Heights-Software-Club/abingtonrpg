using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mono.Data.Sqlite;
using System.Data;
using System;

public class CommonEnemyData
{
    /*
     Attributes of Common Enemy:
        name : string
        low_damage : int
        high_damage : int
     */
    public string name { get; private set; }
    public int low_damage { get; private set; }
    public int high_damage { get; private set; }
    public CommonEnemyData(string id)
    {
        string conn = "URI=file:" + Application.dataPath + "/Data Management/data.db"; //Path to database.
        IDbConnection dbconn;
        dbconn = (IDbConnection)new SqliteConnection(conn);
        dbconn.Open(); //Open connection to the database.
        IDbCommand dbcmd = dbconn.CreateCommand();
        string sqlQuery = "SELECT enemy_name, low_damage, high_damage " + "FROM common_enemies" + " WHERE ID = \"" + id + "\";";
        dbcmd.CommandText = sqlQuery;
        IDataReader reader = dbcmd.ExecuteReader();
        while (reader.Read())
        {
            name = reader.GetString(0);
            low_damage = reader.GetInt32(1);
            high_damage = reader.GetInt32(2);

            //Debug.Log("name = " + name + "  low_damage = " + low_damage + "  high_damage = " + high_damage);
        }
        reader.Close();
        reader = null;
        dbcmd.Dispose();
        dbcmd = null;
        dbconn.Close();
        dbconn = null;
    }
}
