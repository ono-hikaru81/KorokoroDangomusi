using RunGame.Stage;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

// <summary>
/// 敵の『アリ』を表します。
/// </summary>
public class Ants : MonoBehaviour {
    GameObject playerObj;

    Player player;

    public bool isRotate = true;

    public enum ActionPart {
        Wait, // 待機モーション
        Raid, // 戦闘モーション
        Death // 死亡モーション
    }
    ActionPart Action = ActionPart.Wait;

    // アリの速度
    float speed_x = -2;
    float speed_y = 0.0f;
    float speed_z = 0.0f;

    int stopTimer = 0;
    Vector2 vec;

    Rigidbody2D rigidbody;
    
    public AudioClip SE_death;

    SpriteRenderer sprite;

    // Start is called before the first frame update
    void Start() {
        vec = transform.position;
        rigidbody = GetComponent<Rigidbody2D>();
        playerObj = GameObject.Find("Player");
        player = playerObj.GetComponent<Player>();
    }

    // Update is called once per frame
    void Update() {

        switch (Action) {
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

    private void FixedUpdate() {
        if (vec.x == transform.position.x && isRotate == true) {
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

        Rigidbody2D rigidbody2D = GetComponent<Rigidbody2D>();
        float rotation = rigidbody2D.rotation;
        if (Mathf.Abs(rotation) > 45.0f) {
            rigidbody2D.SetRotation(rigidbody2D.rotation > 0.0f ? 45.0f : -45.0f);
        }
    }


    void OnCollisionEnter2D(Collision2D collision) {
        if ( tag == "Enemy" ) {
            if ( collision.gameObject.tag == "Player" ) {
                if ( player.RotationMode == true ) {
                    GetComponent<AudioSource>().clip = SE_death;
                    GetComponent<AudioSource>().Play();
                    tag = "Dead";
                    GetComponent<Animator>().SetBool( "isDead", true );
                    GetComponent<BoxCollider2D>().isTrigger = true;
                    Action = ActionPart.Death;
                }
            }

            if ( collision.gameObject.tag == "Enemy" ) {
                speed_x *= -1;
                var scale = transform.localScale;
                scale.x *= -1;
                transform.localScale = scale;
            }
        }
    }

    void WaitAction()
    {

    }

    void RaidAction()
    {
        var velocity = rigidbody.velocity;
        velocity.x = speed_x;
        rigidbody.velocity = velocity;
    }

    void DeathAction()
    {
        sprite = GetComponent<SpriteRenderer>();
        sprite.color = new Color( sprite.color.r, sprite.color.g, sprite.color.b, sprite.color.a - 0.01f );
        if(sprite.color.a < 0 ) {
            Destroy( gameObject );
        }
    }

    // カメラの範囲内に入ったときに攻撃パートに切り替える
    private void OnBecameVisible() {
        Action = ActionPart.Raid;
    }
}
