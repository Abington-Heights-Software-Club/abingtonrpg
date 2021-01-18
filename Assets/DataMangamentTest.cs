using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mono.Data.Sqlite;
using System.Data;
using System;
public class DataMangamentTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        string conn = "URI=file:" + Application.dataPath + "/TestDataBase.db"; //Path to database.
        IDbConnection dbconn;
        dbconn = (IDbConnection)new SqliteConnection(conn);
        dbconn.Open(); //Open connection to the database.
        IDbCommand dbcmd = dbconn.CreateCommand();
        string sqlQuery = "SELECT name, damage, description " + "FROM Weapon";
        dbcmd.CommandText = sqlQuery;
        IDataReader reader = dbcmd.ExecuteReader();
        while (reader.Read())
        {
            string name = reader.GetString(0);
            int damage = reader.GetInt32(1);
            string description = reader.GetString(2);

            Debug.Log("name = " + name + "  damage = " + damage + "  description = " + description);
        }
        reader.Close();
        reader = null;
        dbcmd.Dispose();
        dbcmd = null;
        dbconn.Close();
        dbconn = null;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
