using RunGame.Stage;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.U2D;
using UnityEngine;
using UnityEngine.SceneManagement;

// <summary>
/// 敵の『モグラ』を表します。
/// </summary>
public class Mole : MonoBehaviour
{
    GameObject playerObj;

    Player player;

    Vector2 pos;

    bool burrowsPosTrigger = false;
    bool burrowsColTrigger = false;

    int hp = 3;

    public enum ActionPart
    {
        Wait, // 待機モーション
        Raid, // 戦闘モーション
        Death, // 死亡モーション
    }
    ActionPart Action = ActionPart.Wait;

    public enum LookingDirection {
        Left,
        Right
    }
    LookingDirection LDirection = LookingDirection.Left;

    // モグラの速度
    float speed_x = 4.0f;
    float speed_y = 0.0f;
    float speed_z = 0.0f;

    // 時間管理変数
    float motiontimer = 0.0f;
    float burrowstimer = 0.0f;

    Rigidbody2D rigidbody;
    BoxCollider2D boxCollider;

    int stopTimer = 0;
    Vector2 vec;

    // Start is called before the first frame update
    void Start()
    {
        vec = transform.position;
        boxCollider = GetComponent<BoxCollider2D>();
        rigidbody = GetComponent<Rigidbody2D>();
        playerObj = GameObject.Find("Player");
        player = playerObj.GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        TurnOver();
        switch (Action)
        {
            case ActionPart.Wait:
                WaitAction();
                break;
            case ActionPart.Raid:
                RaidAction();
                break;
            case ActionPart.Death:
                DeathAction();
                break;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (player.RotationMode == true)
            {
                hp -= 1;
                if(hp <= 1) {
                    speed_x *= 1.5f;
                }

                if(hp <= 0) {
                    Destroy(gameObject);
                    SceneManager.LoadScene("Ending");
                }
            }
        }

        if (collision.gameObject.tag == "Enemy") {
            speed_x *= -1;
            var scale = transform.localScale;
            scale.x *= -1;
            transform.localScale = scale;
        }
    }

    void OnTriggerEnter2D(Collider2D collider) {
        if (collider.tag == "Grounds") {
            speed_x *= -1;
            var scale = transform.localScale;
            scale.x *= -1;
            transform.localScale = scale;
        }
    }

    private void FixedUpdate() {
        if (vec.x == transform.position.x) {
            stopTimer++;
            if (stopTimer >= 10) {
                stopTimer = 0;
                speed_x *= -1;
                var scale = transform.localScale;
                scale.x *= -1;
                transform.localScale = scale;
            }
        }
        else {
            stopTimer = 0;
        }
        vec = transform.position;
    }

    void WaitAction()
    {

    }

    void RaidAction()
    {
        motiontimer += Time.deltaTime;
        burrowstimer += Time.deltaTime;
        switch (hp) {
            case 1:
            case 2:
                if (burrowstimer >= 10.0f) {
                    Burrows();
                }
                else if (motiontimer >= 2.0f) {
                    Throwing();
                    motiontimer = 0;
                }
                else {
                    Rush();
                }
                break;
            case 3:
                Rush();
                break;
        }
    }

    void DeathAction()
    {
        Destroy(gameObject);
    }

    void Rush() {
        var velocity = rigidbody.velocity;
        velocity.x = -speed_x;
        rigidbody.velocity = velocity;
    }

    void Throwing() {
        GameObject Dirt = (GameObject)Resources.Load("Prefabs/Dirt");
        Instantiate(Dirt, transform.position, Quaternion.identity);
    }

    void Burrows() {
        if(burrowsColTrigger == false) {
            boxCollider.isTrigger = true;
            pos = transform.position;
            burrowsColTrigger = true;
        }

        if (transform.position.y < pos.y - 5 && burrowsPosTrigger == false) {
            var position = transform.position;
            position.x = player.transform.position.x;
            transform.position = position;

            var velocity = rigidbody.velocity;
            velocity.y = 12;
            rigidbody.velocity = velocity;
            burrowsPosTrigger = true;
        }

        if (transform.position.y > pos.y + 5) {
            boxCollider.isTrigger = false;
            burrowsPosTrigger = false;
            burrowsColTrigger = false;
            burrowstimer = 0;
        }
    }

    void TurnOver() {
        if (player != null) {
            var scale = transform.localScale;
            if (player.transform.position.x < transform.position.x) {
                LDirection = LookingDirection.Left;
            }
            else if (player.transform.position.x > transform.position.x) {
                LDirection = LookingDirection.Right;
            }
            transform.localScale = scale;
        }
    }

    private void OnBecameVisible() {
        Action = ActionPart.Raid;
    }

}
