using RunGame.Stage;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spring : MonoBehaviour
{
    public float springPower = 10;

    GameObject playerObj;
    Player player;

    private void Start() {
        playerObj = GameObject.Find("Player");
        player = playerObj.GetComponent<Player>();
    }

    public void OnTriggerEnter2D(Collider2D collider) {
        if(collider.tag == "Player") {
            if(player.RotationMode == false) {
                return;
            }
        }
        var velocity = collider.attachedRigidbody.velocity;
        velocity.y = springPower;
        collider.attachedRigidbody.velocity = velocity;
    }
}
