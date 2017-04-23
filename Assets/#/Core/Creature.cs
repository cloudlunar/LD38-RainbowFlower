using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Creature : MonoBehaviour {
    public float HP=3, SAN=3, INT=3, VL=3,LV=1;
    public float ST = 100;
    public List<string> abilities=new List<string>();
    public static Creature player;
    public float usingST = 0;
    public Animator ani;
    void Start()
    {
        ani = GetComponent<Animator>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        print(collision.relativeVelocity + "(" + collision.relativeVelocity.magnitude+","+collision.gameObject.name+"@"+name+")");

        //踩//
        var rg = collision.rigidbody;
        if (rg) {
            int rc = (int)rg.velocity.y / 4;
            if (rc < 0)
            {
                HP += 0.5f * rc;
                return;
            }
        }
        //跌落//
        int c = (int)collision.relativeVelocity.y / 10;
        if (c > 0)
        {
            HP -= 0.5f * c;
        }
        
    }
    private void Update()
    {
        if(transform.position.x<0)
        
            transform.DOMoveX(299,0);
        
        if(transform.position.x>300)
            transform.DOMoveX(1, 0);
        print("ST" + usingST);
        if(usingST>0)
        {
            ST = Mathf.Clamp(ST- 25f * 20f / (HP+SAN) * Time.deltaTime* usingST,0,100);
        }else
        {
            ST = Mathf.Clamp(ST+12.5f / 5f * SAN * Time.deltaTime, 0,100);
        }
        usingST = 0;
    }
    public virtual void Fire()
    {
        ani.SetTrigger("attack");
    }
	
}
