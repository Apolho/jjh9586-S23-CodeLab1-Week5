using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.name.Contains("Player"))
        {
            Debug.Log("Hit");
            GameManager.instance.GetComponent<ASCIILevelLoader>().Scoring(); //when hits player, calls the score function in the other script
            Destroy(gameObject); //destroys the collectible
        }
    }
}
