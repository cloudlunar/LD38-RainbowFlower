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
    public float range = 10;
    Vector3 initPos;
    public void OnCollisionEnter2D(Collision2D collision)
    {
        base.OnCollisionEnter2D(collision);
        dir = -dir;
    }
    void Start()
    {
        initPos = transform.position;
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
        if (SAN <= 0 || HP <= 0 || VL <= 0 || INT <= 0)
        {
            Instantiate(Resources.Load("DestroyPS"), transform.position, transform.rotation);
            foreach(var x in GetComponentsInChildren<SpriteMeshInstance>())
            {
                DOTween.ToAlpha(()=>x.color,(r)=>x.color=r,0,1f);
            }
            var gb = gameObject;
            DOTween.Sequence().AppendInterval(1).AppendCallback(() => Destroy(gb));
            if (Random.Range(0f, 1f) < 0.8f)
            {
                var r=Instantiate(Resources.Load("heart"), transform.position, transform.rotation) as GameObject;
                r.GetComponent<Rigidbody2D>().AddForce(50 * Random.insideUnitCircle);
            }
            if (Random.Range(0f, 1f) < 0.2f)
            {
                var r = Instantiate(Resources.Load("heart"), transform.position, transform.rotation) as GameObject;
                r.GetComponent<Rigidbody2D>().AddForce(50 * Random.insideUnitCircle);
            }
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
        if (transform.position.x < initPos.x-range/2) { dir = 1; }
        if (transform.position.x > initPos.x + range/2) { dir = -1; }
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
        v0 *= VL / 2f;
        var rv = rigid.velocity;
        rv.x = v0;
        rigid.velocity = rv;
    }



}
