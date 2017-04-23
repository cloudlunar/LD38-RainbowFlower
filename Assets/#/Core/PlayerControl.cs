using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerControl : MonoBehaviour {
    Rigidbody2D rigid;
    Collider2D cd;
    Creature player;
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        cd = GetComponent<Collider2D>();
        player = Creature.player = GetComponent<Creature>();
        player.abilities.Add("Jump");
        
       // player.abilities.Add("Fly");
       player.abilities.Add("Swim");

    }
    float lastJumpTime = 0;
    void Update() {
        if (!Creature.player) return;
        //Update Death//
        if(player.SAN<=0||player.HP<=0||player.VL<=0||player.INT<=0)
        {
            SceneManager.LoadScene("GameOver");
        }

        //Update Move
        float v0 = 0;
        if (Input.GetAxis("Horizontal") > 0.1f) {
            transform.rotation = Quaternion.identity;
            v0 = 1;
        }
        else if (Input.GetAxis("Horizontal") < -0.1f)
        {
            transform.rotation=Quaternion.Euler(0, 180, 0);
            v0 = -1;
        }
        v0 *= Creature.player.VL;
        var rv = rigid.velocity;
        rv.x = v0;

        player.ani.SetFloat("speed",Mathf.Abs(v0));
        if (Water.inWater == player)
            rv.x *= 0.5f;
        //Update Jump
        if (player.abilities.Contains("Jump")&&Input.GetButtonDown("Jump"))
        {
            var x=Physics2D.RaycastAll(cd.bounds.max, Vector2.down, cd.bounds.size.y + 0.1f, 1 << 8);
            var y = Physics2D.RaycastAll(cd.bounds.min, Vector2.down, 0.1f, 1 << 8);
            var z = Physics2D.RaycastAll(cd.bounds.center,Vector2.down, cd.bounds.extents.y +0.1f, 1 << 8);
            if ((x.Length>0||y.Length>0||z.Length>0)&Time.time-lastJumpTime>0.3f)
            {
                lastJumpTime = Time.time;
                rigid.AddForce(300 * Vector2.up);
                player.ani.SetTrigger("jump");

            }
        }
        bool fly = false;
        if ((player.abilities.Contains("Fly")|| player.abilities.Contains("Swim")&&Water.inWater==player) && Input.GetAxis("Vertical") > 0.1f )

        {
            if (player.ST > 0)
            {
                rv.y = Creature.player.VL;
                rigid.velocity = rv;
            }
            if (player.abilities.Contains("Swim") && Water.inWater == player)
            {
                player.usingST += 0.15f;
            }
            else
            {
                player.usingST++;
                fly = true;
            }
            
        }
        if (player.abilities.Contains("Swim") && Input.GetAxis("Vertical") <- 0.1f && Water.inWater == player)

        {
            if (player.ST > 0)
            {
                rv.y =- Creature.player.VL*0.5f;
                rigid.velocity = rv*0.5f;
            }
            player.usingST+=0.15f;
        }


        player.ani.SetBool("swim", Water.inWater == player);
        player.ani.SetBool("fly",fly);

        rigid.velocity = rv;
        if (Input.GetButtonDown("Fire"))
            player.Fire();
    }
}
