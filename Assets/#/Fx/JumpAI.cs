using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class JumpAI : AI
{
    int cnt = 0;
    public override void Fire()
    {
        ++cnt;
        rigid.AddForce(300 * Vector2.up);
        player.ani.SetTrigger("jump");
        if(name== "Dragon" )

        {
            var d = (Creature.player.transform.position - transform.position).magnitude;
            print(d);
            if(d < 10f&&cnt%20!=0){
                var g = Instantiate(Resources.Load("Fire"), transform.position, Quaternion.identity) as GameObject;
                Physics2D.IgnoreCollision(g.GetComponent<Collider2D>(), cd);
                g.GetComponent<Rigidbody2D>().AddForce(400 * (Creature.player.transform.position - transform.position).normalized);
            }else if(cnt%10==0){
                HP = Mathf.Clamp(HP + 0.5f, 0, 10f);
            }
        }
    }
}