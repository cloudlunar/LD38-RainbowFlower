using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fx : MonoBehaviour {
    public Creature from;
    public float hpa=-0.5f, sana=0,vla=0,lifetime=5f;
    void Start()
    {
        DOTween.Sequence().AppendInterval(lifetime).AppendCallback(() => Destroy(gameObject));
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        var x = collision.gameObject.GetComponent<Creature>();
        if(x!=null&&x!=from)
        {
            Creature.player.HP = Mathf.Clamp(Creature.player.HP + hpa, 0, 10);
            Creature.player.VL = Mathf.Clamp(Creature.player.VL + vla, 0, 10);
            Creature.player.SAN = Mathf.Clamp(Creature.player.SAN + hpa, 0, 10);
        }
    }
}
