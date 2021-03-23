using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivatorScript : MonoBehaviour
{   
    public KeyCode key;
    SpriteRenderer sr;
    bool active = false;
    GameObject note;
    Color old;

   void Awake () {
          sr = GetComponent<SpriteRenderer>();
   }

    void Start()
    {
       old=sr.color; 
    }

    // Update is called once per frame
    void Update()
    {
     if(Input.GetKeyDown(key)&&active){
        Destroy(note);
        StartCoroutine(isPressed());
     }   
    }

    void OnTriggerEnter2D(Collider2D col){
        active=true;
        if(col.gameObject.tag=="Note")
            note=col.gameObject;
    }

    void OnTriggerExit2D(Collider2D col){
        active=false;
    }

    IEnumerator isPressed(){
        sr.color=new Color(0,0,0);
        yield return new WaitForSeconds(0.05f);
        sr.color=old;
    }
}
