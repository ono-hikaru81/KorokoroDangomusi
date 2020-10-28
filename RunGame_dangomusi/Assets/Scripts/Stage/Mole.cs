using RunGame.Stage;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
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

    public AudioClip SE_damage;
    public AudioClip SE_death;

    public enum ActionPart
    {
        Wait, // 待機モーション
        Raid, // 戦闘モーション
        Death, // 死亡モーション
    }
    public ActionPart Action = ActionPart.Wait;

    public enum LookingDirection {
        Left,
        Right
    }
    LookingDirection LDirection = LookingDirection.Left;

    // モグラの速度
    public float speed_x = 4.0f;
    float speed_y = 0.0f;
    float speed_z = 0.0f;

    // 時間管理変数
    float motiontimer = 0.0f;

    Rigidbody2D rigidbody;
    BoxCollider2D boxCollider;

    public bool invincible = false;

    public float throwingWait = 1.0f;
    public float burrowsWait = 3.0f;

    bool wait = false;

    public AudioClip bossBgm;

    bool onceLoad = false;

    // Start is called before the first frame update
    void Start()
    {
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

    void OnCollisionEnter2D ( Collision2D collision ) { 
        if ( collision.gameObject.tag == "Wall" ||
             collision.gameObject.tag == "Enemy" ) {
            speed_x *= -1;
            var scale = transform.localScale;
            scale.x *= -1;
            transform.localScale = scale;
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
                if (motiontimer >= 7.0f) {
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
        StartCoroutine( "Dead" );
        GameObject Explosion = ( GameObject ) Resources.Load( "Prefabs/explosion" );
        var pos = transform.position;
        if(UnityEngine.Random.Range(0, 10) == 0 ) {
            GetComponent<AudioSource>().clip = SE_death;
            GetComponent<AudioSource>().Play();
            Instantiate( Explosion, new Vector3( pos.x + UnityEngine.Random.Range( -0.5f, 0.5f ), pos.y + UnityEngine.Random.Range( -0.5f, 0.5f ) ), Quaternion.identity );
        }
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

    void OnBecameVisible() {
        Action = ActionPart.Raid;
        if ( onceLoad == false ) {
            var bgmAudio = Camera.main.GetComponent<AudioSource>();
            bgmAudio.clip = bossBgm;
            onceLoad = true;
            bgmAudio.Play();
        }
    }

    IEnumerator InvincibleTime() {
        invincible = true;
        Action = ActionPart.Wait;
        yield return new WaitForSeconds(3.0f);
        GetComponent<Animator>().SetBool( "takeDamage", false );
        Action = ActionPart.Raid;
        invincible = false;
    }

    IEnumerator ThrowingCoroutines() {
        GetComponent<Animator>().SetBool("isThrow", true);
        yield return new WaitForSeconds(0.5f);
        GetComponent<Animator>().SetBool("isThrow", false);
        GameObject Dirt = (GameObject)Resources.Load("Prefabs/Dirt");
        var pos = transform.position;
        Instantiate(Dirt, new Vector3(pos.x, pos.y + 1), Quaternion.identity);
        wait = false; 
    }

    IEnumerator BurrowsCoroutines() {
        GetComponent<Animator>().SetBool("isBurrow_In", true);
        yield return new WaitForSeconds(1.0f);
        GetComponent<Animator>().SetBool("isBurrow_In", false);
        // プレイヤーのx座標に移動して飛び出す
        if (transform.position.y < pos.y - 5) {
            GetComponent<Animator>().SetBool("isBurrow_Out", true);
            var position = transform.position;
            position.x = player.transform.position.x;
            position.y = pos.y - 3;
            transform.position = position;

            var velocity = rigidbody.velocity;
            velocity.y = 15;
            rigidbody.velocity = velocity;
            wait = false;
            GetComponent<Animator>().SetBool("isBurrow_Out", false);
        }
    }

    IEnumerator Dead () {
        yield return new WaitForSeconds( 5.0f );
        Destroy( gameObject );
        SceneManager.LoadScene( "HappyEndMovie" );
    }
}
