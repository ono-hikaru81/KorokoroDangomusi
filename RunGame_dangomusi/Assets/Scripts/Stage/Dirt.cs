using RunGame.Stage;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.AssetImporters;
using UnityEngine;

public class Dirt : MonoBehaviour
{
    Rigidbody2D rigidbody;

    GameObject playerObj;
    Player player;
    GameObject moleObj;
    Mole mole;

    float speed = 6;

    private void Start() {
        Destroy(gameObject, 5);
        moleObj = GameObject.Find("Mole");
        if(moleObj != null) {
            mole = moleObj.GetComponent<Mole>();
        }
        playerObj = GameObject.Find("Player");
        if (playerObj != null) {
            player = playerObj.GetComponent<Player>();
        }
        rigidbody = GetComponent<Rigidbody2D>();
        if(mole.hp <= 1) {
            speed *= 1.5f;
        }
        var velocity = rigidbody.velocity;
        velocity.y = 5;
        if (player != null) {
            if (player.transform.position.x < transform.position.x) {
                velocity.x = -speed;
            }
            else {
                velocity.x = speed;
            }
        }
        rigidbody.velocity = velocity;
    }

    private void OnTriggerEnter2D(Collider2D collider) {
        if(collider.tag == "Player" || 
           collider.tag == "Grounds") {
            Destroy(gameObject);
        }
    }
}
