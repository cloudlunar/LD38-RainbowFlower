using Anima2D;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : Creature {
    public float[] attackCD = new[] { 5f };
    public string[] attackPreb=new[] { "Fire" };
    public Rigidbody2D rigid;
    public Collider2D cd;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        dir = -dir;
    }
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        cd = GetComponent<Collider2D>();
        ani = GetComponentInChildren<Animator>();
        attackFrame = (int)(attackFrame*Random.Range(0.6f, 1.4f));
        changeDirFrame = (int)(changeDirFrame *Random.Range(0.6f, 1.4f));
    }
    int dir = 1;
    public int attackFrame = 450, changeDirFrame = 300;
    int afc=0, cdfc=0;

	void FixedUpdate ()
    {
        if (Time.frameCount % attackFrame == afc)
        {
            Fire();
            afc = Random.Range(0, attackFrame);
        }
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
        if (Time.frameCount % changeDirFrame == cdfc)
        {
            dir = Random.Range(-1, 2);
            cdfc = Random.Range(0, changeDirFrame);
        }
        if (dir > 0.1f)
        {
            transform.rotation = Quaternion.identity;
            v0 = 1;
        }
        else if (dir < -0.1f)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
            v0 = -1;
        }
        v0 *= Creature.player.VL / 2f;
        var rv = rigid.velocity;
        rv.x = v0;
        rigid.velocity = rv;
    }



}
