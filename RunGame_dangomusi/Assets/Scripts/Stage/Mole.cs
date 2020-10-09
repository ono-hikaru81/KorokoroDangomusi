using RunGame.Stage;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.U2D;
using UnityEngine;
using UnityEngine.Rendering.VirtualTexturing;
using UnityEngine.SceneManagement;

// <summary>
/// 敵の『モグラ』を表します。
/// </summary>
public class Mole : MonoBehaviour
{
    GameObject playerObj;

    Player player;

    Vector2 pos;

    public int hp = 3;

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

    Rigidbody2D rigidbody;
    BoxCollider2D boxCollider;

    int stopTimer = 0;
    Vector2 vec;

    public bool invincible = false;

    float throwingWait = 1.0f;
    float burrowsWait = 3.0f;

    bool wait = false;

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
        

        if (transform.position.y > pos.y + 3) {
            boxCollider.isTrigger = false;
            invincible = false;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (player.RotationMode == true && invincible == false)
            {
                hp -= 1;
                StartCoroutine("InvincibleTime");
                if(hp <= 1) {
                    throwingWait = 0.5f;
                    speed_x *= 1.5f;
                    burrowsWait = 1.0f;
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

    private void FixedUpdate() {
        if (invincible == false && wait == false) {
            if (vec.x == transform.position.x) {
                stopTimer++;
                if (stopTimer >= 5) {
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
    }

    void WaitAction()
    {

    }

    void RaidAction()
    {
        motiontimer += Time.deltaTime;
        switch (hp) {
            case 1:
            case 2:
                // 5秒ごとにランダムで投擲攻撃or突き攻撃を行う
                if (motiontimer >= 5.0f) {
                    int temp = UnityEngine.Random.Range(0, 2);
                    if (temp == 1) {
                        Burrows();
                    }
                    else if(temp == 0){
                        Throwing();
                    }
                    wait = true;
                    motiontimer = 0;
                }
                else {
                    if (wait == false) {
                        Rush();
                    }
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
        StartCoroutine("ThrowingCoroutines");
    }

    void Burrows() {
        // 地面に潜る
        invincible = true;
        boxCollider.isTrigger = true;
        pos = transform.position;
        StartCoroutine("BurrowsCoroutines");
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

    IEnumerator InvincibleTime() {
        invincible = true;
        Action = ActionPart.Wait;
        yield return new WaitForSeconds(3.0f);
        Action = ActionPart.Raid;
        invincible = false;
    }

    IEnumerator ThrowingCoroutines() {
        yield return new WaitForSeconds(throwingWait);
        GameObject Dirt = (GameObject)Resources.Load("Prefabs/Dirt");
        var pos = transform.position;
        Instantiate(Dirt, new Vector3(pos.x, pos.y + 1), Quaternion.identity);
        wait = false;
    }

    IEnumerator BurrowsCoroutines() {
        yield return new WaitForSeconds(burrowsWait);
        // プレイヤーのx座標に移動して飛び出す
        if (transform.position.y < pos.y - 5) {
            var position = transform.position;
            position.x = player.transform.position.x;
            position.y = pos.y - 3;
            transform.position = position;

            var velocity = rigidbody.velocity;
            velocity.y = 20;
            rigidbody.velocity = velocity;
            wait = false;
        }
    }
}
