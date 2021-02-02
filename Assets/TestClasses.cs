using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestClasses : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //CommonEnemy enemy = new CommonEnemy("chromebook_n22");
        //Debug.Log(enemy.name);
        Weapon poison_vial = new Weapon("test_tube");
        Debug.Log(poison_vial.weapon_name);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
