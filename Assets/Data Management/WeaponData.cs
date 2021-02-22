using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mono.Data.Sqlite;
using System.Data;
using System;

public class WeaponData
{
    /*
     Attributes of Weapon:
        weapon_name : string
        class_type : string
        weapon_type : string
        low_damage : int
        high_damage :int
        description : string
        catchphrase : string
        poison_percent : float
        poison_damage : int
        poison_length : int
        critical_percent : float
        critical_damage : int
        damage_boost : float
        defense_boost : int
        paralyze_percent : float
        paralyze_length : int
        healing_amount : int
     */
     public string weapon_name {get; private set;}
     public string class_type {get; private set;}
     public string weapon_type {get; private set;}
     public int low_damage {get; private set;}
     public int high_damage {get; private set;}
     public string description {get; private set;}
     public string catchphrase {get; private set;}
     public float poison_percent {get; private set;}
     public int poison_damage {get; private set;}
     public int poison_length {get; private set;}
     public float critical_percent {get; private set;}
     public int critical_damage {get; private set;}
     public float  damage_boost {get; private set;}
     public int defense_boost {get; private set;}
     public float paralyze_percent {get; private set;}
     public int paralyze_length {get; private set;}
     public int healing_amount {get; private set;}

     public WeaponData(string id)
     {
        string conn = "URI=file:" + Application.dataPath + "/Data Management/data.db"; //Path to database.
        IDbConnection dbconn;
        dbconn = (IDbConnection)new SqliteConnection(conn);
        dbconn.Open(); //Open connection to the database.
        IDbCommand dbcmd = dbconn.CreateCommand();
        string sqlQuery = "SELECT weapon_name, class, type, low_damage, high_damage, description, catchphrase, poison_percent, poison_damage, poison_length, critical_percent, critical_damage, damage_boost, defense_boost, paralyze_percent, paralyze_length, healing_amount " + "FROM weapons" + " WHERE ID = \"" + id + "\";";
        dbcmd.CommandText = sqlQuery;
        IDataReader reader = dbcmd.ExecuteReader();
        while (reader.Read())
        {
            weapon_name = reader.GetString(0);
            class_type = reader.GetString(1);
            weapon_type = reader.GetString(2);
            low_damage = reader.GetInt32(3);
            high_damage = reader.GetInt32(4);
            description = reader.GetString(5);
            catchphrase = reader.GetString(6);
            poison_percent = (float)reader.GetDouble(7);
            poison_damage = reader.GetInt32(8);
            poison_length = reader.GetInt32(9);
            critical_percent = (float)reader.GetDouble(10);
            critical_damage = reader.GetInt32(11);
            damage_boost = (float)reader.GetDouble(12);
            defense_boost = reader.GetInt32(13);
            paralyze_percent = (float)reader.GetDouble(14);
            paralyze_length = reader.GetInt32(15);
            healing_amount = reader.GetInt32(16);

            Debug.Log("weapon_name = " + weapon_name + "  class_type = " + class_type + "  weapon_type = " + weapon_type
            + "  low_damage = " + low_damage + "  high_damage = " + high_damage + "  description = " + description
            + "  catchphrase = " + catchphrase + "  poison_percent = " + poison_percent + "  poison_damage = " + poison_damage
            + "  poison_length = " + poison_length + "  critical_percent = " + critical_percent + "  critical_damage = " + critical_damage
            + "  damage_boost = " + damage_boost + "  defense_boost = " + defense_boost + "  paralyze_percent = " + paralyze_percent
            + "  paralyze_length = " + paralyze_length + "  healing_amount = " + healing_amount);
        }
        reader.Close();
        reader = null;
        dbcmd.Dispose();
        dbcmd = null;
        dbconn.Close();
        dbconn = null;
     }

}
