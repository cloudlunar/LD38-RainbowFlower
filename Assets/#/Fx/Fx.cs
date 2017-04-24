using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fx : MonoBehaviour {
    public Creature from;
    public float hpa=-0.5f, sana=0,vla=0,lifetime=5f;
    void Start()
    {
        var gb = gameObject;
        DOTween.Sequence().AppendInterval(lifetime).AppendCallback(() => { if (gb) Destroy(gb); });
    }
    void Update()
    {
        var v = GetComponent<Rigidbody2D>().velocity;
        var e = transform.rotation.eulerAngles;
        e.z = Mathf.Atan2(v.y, v.x) / Mathf.PI * 180f;
        transform.rotation = Quaternion.Euler(e);
        print(e.z);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        var x = collision.gameObject.GetComponent<Creature>();
        if(x!=null&&x!=from)
        {
            x.HP = Mathf.Clamp(x.HP + hpa, 0, 10);
            x.VL = Mathf.Clamp(x.VL + vla, 0, 10);
           x.SAN = Mathf.Clamp(x.SAN + hpa, 0, 10);
            Destroy(gameObject);
            Instantiate(Resources.Load("Explosion"), collision.contacts[0].point, transform.rotation);
            
        }
    }
}
