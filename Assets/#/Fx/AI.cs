using Anima2D;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : Creature {
    public float[] attackCD = new[] { 5f };
    public string[] attackPreb=new[] { "Fire" };
    public Animator ani;
    Rigidbody2D rigid;
    Collider2D cd;
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        cd = GetComponent<Collider2D>();
        ani = GetComponentInChildren<Animator>();
    }
	void Update () {
        if (player.SAN <= 0 || player.HP <= 0 || player.VL <= 0 || player.INT <= 0)
        {
            Instantiate(Resources.Load("DestroyPS"), transform.position, transform.rotation);
            foreach(var x in GetComponentsInChildren<SpriteMeshInstance>())
            {
                DOTween.ToAlpha(()=>x.color,(r)=>x.color=r,0,2.5f);
            }
            DOTween.Sequence().AppendInterval(3).AppendCallback(() => Destroy(gameObject));
            Destroy(this);
            return;
        }

        //Update Move
        float v0 = 0;
        if (Input.GetAxis("Horizontal") > 0.1f)
        {
            transform.rotation = Quaternion.identity;
            v0 = 1;
        }
        else if (Input.GetAxis("Horizontal") < -0.1f)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
            v0 = -1;
        }
        v0 *= Creature.player.VL / 2f;
        var rv = rigid.velocity;
        rv.x = v0;
        rigid.velocity = rv;
        //Update Jump
        if (player.abilities.Contains("Jump") && Input.GetButtonDown("Jump"))
        {
            var x = Physics2D.RaycastAll(cd.bounds.center, Vector2.down, cd.bounds.extents.y + 0.1f, 1 << 8);
           // if (x.Length > 0 & Time.time - lastJumpTime > 0.3f)
            {
             //   lastJumpTime = Time.time;
                rigid.AddForce(300 * Vector2.up);
            }
        }
        if (Input.GetButtonDown("Fire"))
            player.Fire();
    }



}
