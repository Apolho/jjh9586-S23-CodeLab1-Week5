using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacles : MonoBehaviour
{
    public float forceAmount = 20;

    public bool side = true;
    
    private Rigidbody2D rb2D;
    // Start is called before the first frame update
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
    }


    // Update is called once per frame
    void Update()
    {
        if (side == true) //if the bool is true
        {
            rb2D.AddForce(Vector2.left * forceAmount); //the obstacle will move to the side
        }
        else
        {
            rb2D.AddForce(-Vector2.up * forceAmount); //obstacle will move up and down
        }
        rb2D.velocity *= 0.02f;
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.name.Contains("Wall"))
        {
            forceAmount *= -1;//when it collides with wall, it will move the opposite direction
        }

        if (col.gameObject.name.Contains("Player"))
        {
            GameManager.instance.GetComponent<ASCIILevelLoader>().ResetPlayer();
            //when it collides with player, it resets the player position
        }
    }
}
