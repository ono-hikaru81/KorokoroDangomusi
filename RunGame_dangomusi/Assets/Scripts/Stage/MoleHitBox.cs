using RunGame.Stage;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

public class MoleHitBox : MonoBehaviour {

    Player player;
    Mole mole;

    void Start () {
        player = GameObject.Find( "Player" ).GetComponent<Player>();
        mole = GameObject.Find( "Mole" ).GetComponent<Mole>();
    }

    void OnCollisionEnter2D ( Collision2D collision ) {
        if ( collision.gameObject.tag == "Player" ) {
            if ( player.RotationMode == true && mole.invincible == false ) {
                mole.hp -= 1;

                mole.GetComponent<AudioSource>().clip = mole.SE_damage;
                mole.GetComponent<AudioSource>().Play();

                mole.GetComponent<Animator>().SetBool( "takeDamage", true );

                mole.StartCoroutine( "InvincibleTime" );
                if ( mole.hp <= 1 ) {
                    mole.throwingWait = 0.5f;
                    mole.speed_x *= 1.5f;
                    mole.burrowsWait = 1.0f;
                }

                if ( mole.hp <= 0 ) {
                    mole.Action = Mole.ActionPart.Death;
                }
            }
        }
    }
}
