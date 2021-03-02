using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public static float speed = 0.1f;
    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizInput = Input.GetAxis("Horizontal");
        float vertInput = Input.GetAxis("Vertical");

        Debug.Log(horizInput + " " + vertInput);

        rb.MovePosition(new Vector2(transform.position.x, transform.position.y) + new Vector2(speed * horizInput, speed * vertInput));
    }
}
