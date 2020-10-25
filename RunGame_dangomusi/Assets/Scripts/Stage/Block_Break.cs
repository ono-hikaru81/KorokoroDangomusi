using RunGame.Stage;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Block_Break : MonoBehaviour
{
    float alpha = 1.0f;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if ( GameObject.Find( "Player" ).GetComponent<Player>().RotationMode == true )
            {
                GetComponent<Animator>().SetTrigger( "AnimationTrigger" );
                GetComponent<BoxCollider2D>().isTrigger = true;
                Destroy(gameObject, 1.0f);
            }
        }
    }
}
