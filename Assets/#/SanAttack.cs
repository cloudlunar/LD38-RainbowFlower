using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SanAttack : MonoBehaviour {
    bool isEnter = false;
    float xDmg = 1f;
    Sequence seq;
    void OnTriggerExit2D(Collider2D other)
    {
        var hm = other.GetComponent<PlayerControl>();
        if (hm != null)
        {
            if (seq != null) seq.Kill();
            seq = DOTween.Sequence();

            seq.AppendInterval(0.1f).AppendCallback(() =>
            {
                isEnter = false;
                cd = 1f;
            });
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        var hm = other.GetComponent<PlayerControl>();
        if (hm != null)
        {
            if (seq != null) seq.Kill();
            isEnter = true;
        }
    }
    float cd = 1f;
    void Update () {
        if (isEnter)
        {
            cd -= Time.deltaTime;
            if(cd<0)
            {
                if (Creature.player.INT > 8) return;
                cd = Creature.player.INT * 2;
                Creature.player.SAN -= 0.5f*xDmg;

            }

        }
	}
}
