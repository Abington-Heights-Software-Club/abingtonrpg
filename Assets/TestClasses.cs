﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestClasses : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //CommonEnemyData enemy = new CommonEnemyData("chromebook_n22");
        //Debug.Log(enemy.name);
        QuestData quest = new QuestData("getting_started");
        Debug.Log(quest.description);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
