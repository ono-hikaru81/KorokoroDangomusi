using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PosionBall : MonoBehaviour
{
    bool atackTrigger = false;

    Vector2 atackPos1;
    Vector2 atackPos2;

    GameObject spiderObj;
    Spider spider;

    private void Start() {
        spiderObj = GameObject.Find("Spider");
        spider = spiderObj.GetComponent<Spider>();
        atackPos1 = transform.position;
        atackPos1.y = transform.position.y - 0.5f;
        atackPos2 = transform.position;
        atackPos2.y = transform.position.y - 0.5f;
    }

    private void FixedUpdate() {
        if(atackTrigger == false) {
            if (spider.transform.position.x > transform.position.x) {
                transform.Translate(-0.215f, 0.0f, 0.0f);
            }
            else if (spider.transform.position.x < transform.position.x) {
                transform.Translate(0.215f, 0.0f, 0.0f);
            }
        }

        if (atackTrigger == true) {
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
        if (collision.gameObject.tag != "Trap") {
            atackTrigger = true;
            atackPos1.x = transform.position.x;
            atackPos2.x = transform.position.x;
            Destroy(gameObject, 0.3f);
        }
    }
}
