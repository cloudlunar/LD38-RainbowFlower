using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class JumpAI : AI
{
    public override void Fire()
    {
        print("Fire!!");
        rigid.AddForce(300 * Vector2.up);
        player.ani.SetTrigger("jump");
    }
}