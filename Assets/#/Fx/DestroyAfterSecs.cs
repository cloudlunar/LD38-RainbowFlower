using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterSecs : MonoBehaviour {
    public float sec = 0f;
    public float disappearPro = 1;
    public int needEnd = 0;
    public int nowEnd = 0;
	void Start () {
        if (nowEnd > 0 && PressToStart.nowEnd != nowEnd)
        {
            Destroy(gameObject);
            return;
        }
        if (needEnd>0)
        {
            if((EventsLib.WinC&(1<<needEnd))==0)
            {
                Destroy(gameObject);
                return;
            }
        }
        if(Random.Range(0f,1f)> disappearPro)
        {
            return;
        }
        DOTween.Sequence().AppendInterval(sec).AppendCallback(() => Destroy(gameObject));
	}
}
