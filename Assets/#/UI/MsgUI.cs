using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MsgUI : MonoBehaviour {
    public static Queue<string> conts=new Queue<string>();
    public static Queue<string> titles = new Queue<string>();
    public static Queue<VoidDeg> callbacks = new Queue<VoidDeg>();
    Text titleUI, contUI;
    Image spr;
    public delegate void VoidDeg();
    void Start()
    {
        spr = GetComponent<Image>();
        spr.raycastTarget = false;
        spr.DOFade(0,0);
        titleUI = transform.Find("Title").GetComponent<Text>();

        contUI = transform.Find("Text").GetComponent<Text>();
        titleUI.text = contUI.text = "";
        
    }
    public static void Show(string cont,string title,VoidDeg callback=null)
    {
        conts.Enqueue(cont);
        titles.Enqueue(title);
        callbacks.Enqueue(callback);
    }
    bool isShown = false;
    void ShowAct()
    {
        isShown = true;
        DOTween.Sequence().Append(spr.DOFade(0.8f, 0.3f)).AppendCallback(() =>
        {
            spr.raycastTarget = true;
            titleUI.text = titles.Dequeue();
            contUI.text = conts.Dequeue();
        });
    }
    public void Hide()
    {
        spr.raycastTarget = false;
        titleUI.text = contUI.text = "";
        VoidDeg c=null;
        if (callbacks.Count >= 1) 
             c = callbacks.Dequeue();
        DOTween.Sequence().Append(spr.DOFade(0,0.3f)).AppendCallback(()=> {

            if (c!=null)
                c();
            isShown = false;
        });

    }
    public void FixedUpdate()
    {
        if(conts.Count>0&& isShown == false)
        {
            ShowAct();
        }
    }
}
