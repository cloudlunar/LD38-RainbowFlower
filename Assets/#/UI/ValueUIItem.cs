using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ValueUIItem : MonoBehaviour {

    public int num = 1;
    public float value=0;
	void Update () {
        string x="";
        for(int i=0;i< (int)value;++i)
        {
            x = x + "<sprite="+num+">";
        }
        if(value-(int)value>0.4)
        {

            x = x + "<sprite=" + (num+1) + ">";
        }
        GetComponent<TextMeshProUGUI>().text = x;

	}
}
