using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loading : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        SpriteRenderer floor1 = GetComponent<SpriteRenderer>();
        floor1.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
