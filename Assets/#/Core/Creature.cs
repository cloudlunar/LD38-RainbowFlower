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
    void Awake()
    {
        ani = GetComponent<Animator>();
    }
    public void OnCollisionEnter2D(Collision2D collision)
    {
        
        //踩//
        var rg = collision.rigidbody;
        float rc = rg?rg.velocity.y:0f;
        if (rg&&rc<0)
        {
                print(rg.name+"踩了"+name+rc+"#"+HP);
                HP -= 0.5f ;
                return;
            
        }
        else
        {

            if (rg && rg.GetComponent<PlayerControl>() != null)
            {
                print(rg.name + "被伤害" + name);
                rg.GetComponent<Creature>().HP -= 0.5f;
                return;
            }
        }
        //跌落//
        int c = (int)collision.relativeVelocity.y / 10;
        if (c > 0)
        {
            print(collision.collider.name + "撞击了" + name);
            HP -= 0.5f * c;
        }
        
    }
    private void Update()
    {
        if(transform.position.x<0)
        
            transform.DOMoveX(299,0);
        
        if(transform.position.x>300)
            transform.DOMoveX(1, 0);
       // print("ST" + usingST);
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
        if(abilities.Contains("Attack")&&ST>30)
        {
            ani.SetTrigger("attack");
            var g = Instantiate(Resources.Load("Ice"), transform.position, Quaternion.identity) as GameObject;
            Physics2D.IgnoreCollision(g.GetComponent<Collider2D>(), GetComponent<Collider2D>());
            g.GetComponent<Rigidbody2D>().AddForce(400 * Vector3.right*(transform.rotation.eulerAngles.y>90?-1:1)/3f*VL);
            ST -= 30;

        }
    }
	
}
