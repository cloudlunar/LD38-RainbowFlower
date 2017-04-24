using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PressToStart : MonoBehaviour {
    public static int nowEnd = 0;
    public static bool inited = false;
    private void Start()
    {
        inited = true;
        for (int i=1;i<=7;++i)
        {
            var gb = GameObject.Find("RF (" + i + ")");
            var btn = gb.GetComponentInChildren<Button>();
            if((EventsLib.WinC&(1<<i))==0)
            {
                var b = btn.colors;
                b.normalColor = new Color(1, 1, 1, 0.5f);
                b.highlightedColor = Color.white ;
                btn.colors = b;
                if (((EventsLib.WinC & (1 << (i-1))) == 0)&&i!=1)
                {
                    btn.interactable = false;
                }
            }
        }
    }
    
	public void GameStart(int end)
    {
        nowEnd = end;
        AudioManager.ins.PlayAudio("A/s" + end);
        DOTween.Sequence().AppendInterval(0.3f).AppendCallback(()=>
        SceneManager.LoadScene(1))
        ;
    }
}
