using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mono.Data.Sqlite;
using System.Data;
using System;

public class PlayableCharacterData
{
    /*
      Attributes of Playable Character:
         name : string
         base_low_damage : int
         base_high_damage : int
         base_health : int
         base_xp_cap : int
         levelUp_damage : int
         levelUp_health : int
         levelUp_xp_cap : int
         energy_special_one : int
      */
    public string name { get; private set; }
    public int base_low_damage { get; private set; }
    public int base_high_damage { get; private set; }
    public int base_health { get; private set; }
    public int base_xp_cap { get; private set; }
    public int levelUp_damage { get; private set; }
    public int levelUp_health { get; private set; }
    public int levelUp_xp_cap { get; private set; }
    public int energy_special_one { get; private set; }
    public PlayableCharacterData(string id)
    {
        string conn = "URI=file:" + Application.dataPath + "/Data Management/data.db"; //Path to database.
        IDbConnection dbconn;
        dbconn = (IDbConnection)new SqliteConnection(conn);
        dbconn.Open(); //Open connection to the database.
        IDbCommand dbcmd = dbconn.CreateCommand();
        string sqlQuery = "SELECT character_name, base_low_damage, base_high_damage, base_health, base_xp_cap, levelUp_damage, levelUp_health, levelUp_xp_cap, energy_special_one " + "FROM playable_characters" + " WHERE ID = \"" + id + "\";";
        dbcmd.CommandText = sqlQuery;
        IDataReader reader = dbcmd.ExecuteReader();
        while (reader.Read())
        {
            name = reader.GetString(0);
            base_low_damage = reader.GetInt32(1);
            base_high_damage = reader.GetInt32(2);
            base_health = reader.GetInt32(3);
            base_xp_cap = reader.GetInt32(4);
            levelUp_damage = reader.GetInt32(5);
            levelUp_health = reader.GetInt32(6);
            levelUp_xp_cap = reader.GetInt32(7);
            energy_special_one = reader.GetInt32(8);
            //Debug.Log("name = " + name + "  base_low_damage = " + base_low_damage + "  base_high_damage = " + base_high_damage + " base_health = " + base_health + " base_xp_cap = " + base_xp_cap + " levelUp_damage = " + levelUp_damage + " levelUp_health = " + levelUp_health + " levelUp_xp_cap = " + levelUp_xp_cap + " energy_special_one = " + energy_special_one);
        }
        reader.Close();
        reader = null;
        dbcmd.Dispose();
        dbcmd = null;
        dbconn.Close();
        dbconn = null;
    }
}
