using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommonEnemy
{
    public string name { get; private set; }
    public int low_damage { get; private set; }
    public int high_damage { get; private set; }
    public CommonEnemy(string id)
    {
        string conn = "URI=file:" + Application.dataPath + "/data.db"; //Path to database.
        IDbConnection dbconn;
        dbconn = (IDbConnection)new SqliteConnection(conn);
        dbconn.Open(); //Open connection to the database.
        IDbCommand dbcmd = dbconn.CreateCommand();
        string sqlQuery = "SELECT enemy_name, low_damage, high_damage " + "FROM common_enemies";
        dbcmd.CommandText = sqlQuery;
        IDataReader reader = dbcmd.ExecuteReader();
        while (reader.Read())
        {
            string name = reader.GetString(0);
            int low_damage = reader.GetInt32(1);
            string high_damage = reader.GetInt32(2);

            Debug.Log("name = " + name + "  low_damage = " + damage + "  high_damage = " + high_damage);
        }
        reader.Close();
        reader = null;
        dbcmd.Dispose();
        dbcmd = null;
        dbconn.Close();
        dbconn = null;
    }
}
