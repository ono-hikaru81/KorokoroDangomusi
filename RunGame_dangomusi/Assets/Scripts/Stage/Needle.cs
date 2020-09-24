﻿using RunGame.Stage;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Needle : MonoBehaviour
{
    GameObject playerObj;
    Player player;

    void Start() {
        playerObj = GameObject.Find("Player");
        player = playerObj.GetComponent<Player>();
        Destroy(gameObject, 10.0f);
    }

    void Update() {
        transform.Translate(0.0f, 0.005f, 0.0f, Space.Self);
    }

    void OnTriggerEnter2D(Collider2D collider) {
        if(collider.tag == "Player") {
            if(player.RotationMode == true) {
                Destroy(gameObject);
            }
        }
    }
}
