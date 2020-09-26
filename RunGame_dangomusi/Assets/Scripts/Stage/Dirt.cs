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

    private void Start() {
        Destroy(gameObject, 10);
        playerObj = GameObject.Find("Player");
        player = playerObj.GetComponent<Player>();
        rigidbody = GetComponent<Rigidbody2D>();
        var velocity = rigidbody.velocity;
        velocity.y = 5;
        if (player != null) {
            if (player.transform.position.x < transform.position.x) {
                velocity.x = -Random.Range(4.0f, 7.0f);
            }
            else {
                velocity.x = Random.Range(4.0f, 7.0f);
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
