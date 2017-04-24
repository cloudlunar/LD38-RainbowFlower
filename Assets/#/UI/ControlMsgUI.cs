using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ControlMsgUI : MonoBehaviour {
    static Text controlMsgUI;
    void Start () {
        if (!PressToStart.inited)
            SceneManager.LoadScene(0);
        controlMsgUI= GetComponent<Text>();

    }
	public static void Show(string text)
    {
        controlMsgUI.text = text;
        controlMsgUI.DOFade(1, 0.3f);
    }
    public static void Hide(string text)
    {
        if (controlMsgUI.text == text)
        {
            controlMsgUI.DOFade(0, 0.3f);
        }
    }
}
