using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EventTrigger : MonoBehaviour {
    public int needEnding = 0;
    public string ids =  "0" ;
    int[] id = new[] { 0};
    public float cd = 0;
    float curcd = 0;
    public bool needPress = false;
    public bool once = true;
    public string text = "LB Interact";
   
    private void Start()
    {
        if (needEnding > 0)
        {
            if ((EventsLib.WinC & (1 << needEnding)) == 0)
            {
                Destroy(this);
                return;
            }
        }
        List<int> newid=new List<int>();
        foreach(var x in GetComponents<EventTrigger>())
        {
            foreach (var y in x.ids.Split(','))
            {
                if (y.Contains("-"))
                {
                    int a = int.Parse(y.Split('-')[0]);
                    int b = int.Parse(y.Split('-')[1]);
                    for (int i = a; i <= b; ++i) newid.Add(i);
                }
                else
                {
                    newid.Add(int.Parse(y));
                }
            }
        }
       
        id = newid.Distinct().ToArray();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerControl>() != null)
        {
            ControlMsgUI.Show(text);
            ++cnt;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {

        if (collision.gameObject.GetComponent<PlayerControl>() != null)
        {
            if(cnt==1)
                ControlMsgUI.Hide(text);
            --cnt;
        }
    }
    int cnt = 0;
    private void Update()
    {
        if(cnt>0){
            curcd += Time.deltaTime;
            print(name+":"+curcd);
            if((curcd>cd&&!needPress || Input.GetButtonDown("Interact")&&needPress)&& curcd < 1000000)
            {
                curcd = 1000003;
                EventsLib.lib[id[Random.Range(0,id.Length)]].Invoke(gameObject);
                if(once)
                {

                    ControlMsgUI.Hide(text);
                    if(GetComponent<ParticleSystem>()!=null)
                    {
                        GetComponent<ParticleSystem>().Play();
                    }
                    var gb = gameObject;
                    DOTween.Sequence().Append(GetComponent<SpriteRenderer>().DOFade(0f, 0.3f)).AppendCallback(() => { Destroy(gb); });
                }
            }
        }else
        {
            curcd = 0;
        }          
    }
}
