using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DMG : MonoBehaviour {
    float DMGCD = 0;
    private void Update()
    {
        DMGCD -= Time.deltaTime;
    }
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (DMGCD > 0) return;
        //踩//
        var rg = collision.rigidbody;

        if (rg && rg.GetComponent<PlayerControl>() != null)
        {
            print(rg.name + "被伤害" + name);
            rg.GetComponent<Creature>().HP -= 0.5f;
            DMGCD = 1;
            return;
        }

    }
}
