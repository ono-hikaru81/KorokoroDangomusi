using RunGame.Stage;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PosionBall : MonoBehaviour
{
    bool atackTrigger = false;

    float speed = 0.215f;

    Vector2 atackPos1;
    Vector2 atackPos2;

    GameObject spiderObj;
    Spider spider;
    Rigidbody2D rigidbody;

    GameObject playerObj;
    Player player;
    public enum LookingDirection {
        Left,
        Right
    }
    LookingDirection LDirection = LookingDirection.Left;


    private void Start() {
        rigidbody = GetComponent<Rigidbody2D>();
        spiderObj = GameObject.Find("Spider");
        spider = spiderObj.GetComponent<Spider>();
        playerObj = GameObject.Find("Player");
        if (playerObj != null) {
            player = playerObj.GetComponent<Player>();
        }

        TurnOver();
    }

    private void FixedUpdate() {
        if(atackTrigger == false) {
            if (LDirection == LookingDirection.Left) {
                transform.Translate(-speed, 0, 0);
            }
            else if(LDirection == LookingDirection.Right) {
                transform.Translate(speed, 0, 0);
            }
        }
        else if (atackTrigger == true) {
            GameObject Poison = (GameObject)Resources.Load("Prefabs/poison");
            if (Poison != null) {
                Instantiate(Poison, atackPos1, Quaternion.identity);
                Instantiate(Poison, atackPos2, Quaternion.identity);

                atackPos1.x += 0.1f;
                atackPos2.x -= 0.1f;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "Grounds") {
            rigidbody.bodyType = RigidbodyType2D.Static;
            atackTrigger = true;
            atackPos1.y = transform.position.y - 0.2f;
            atackPos2.y = transform.position.y - 0.2f;
            atackPos1.x = transform.position.x;
            atackPos2.x = transform.position.x;
            Destroy(gameObject, 0.3f);
        }
    }

    void TurnOver() {
        if (playerObj != null) {
            if (player.transform.position.x < transform.position.x + 0.5f) {
                LDirection = LookingDirection.Left;
            }
            else if (player.transform.position.x > transform.position.x - 0.5f) {
                LDirection = LookingDirection.Right;
            }
        }
    }
}
