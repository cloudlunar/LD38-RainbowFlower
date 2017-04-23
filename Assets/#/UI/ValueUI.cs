using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ValueUI : MonoBehaviour {
    public Creature viewee;
    ValueUIItem hpv, sanv, intv, vltv;
    Image stv,red;
    void Start()
    {
        hpv = transform.Find("HP_v").GetComponent<ValueUIItem>();
        sanv = transform.Find("SAN_v").GetComponent<ValueUIItem>();
        intv = transform.Find("INT_v").GetComponent<ValueUIItem>();
        vltv = transform.Find("VLT_v").GetComponent<ValueUIItem>();
        stv = transform.Find("ST_v").GetComponent<Image>();
        red = GameObject.Find("RedHover").GetComponent<Image>();
    }
    float s = 1,d=-1;
	void Update () {
        if (viewee == null)
            viewee = Creature.player;
        if(viewee.HP< hpv.value)
        {
            red.DOKill();
            DOTween.Sequence().Append(red.DOColor(Color.red*0.6f, 0.3f)).Append(red.DOFade(0, 1f));
        }
        if (viewee.SAN < sanv.value)
        {
            red.DOKill();
            DOTween.Sequence().Append(red.DOColor(new Color(0.8f, 0f, 1f, 0.6f), 0.3f)).Append(red.DOFade(0, 1f));
        }
        if (viewee.VL < vltv.value)
        {
            red.DOKill();
            DOTween.Sequence().Append(red.DOColor(new Color(0, 0, 0.7f, 0.6f) , 0.3f)).Append(red.DOFade(0, 1f));
        }

        if (viewee.INT < intv.value)
        {
            red.DOKill();
            DOTween.Sequence().Append(red.DOColor(new Color(0,0.5f,0,0.6f), 0.3f)).Append(red.DOFade(0, 1f));
        }
        hpv.value = viewee.HP;        
        sanv.value = viewee.SAN;
        intv.value = viewee.INT;
        vltv.value = viewee.VL;
        var fm =  viewee.ST / 100f;
        fm = 1-fm*fm;
        stv.fillAmount = fm;
        var c= Color.Lerp(Color.green, Color.red, fm);
        
        if (fm>0.95f)
        {
            s += d * Time.deltaTime*3;
            if (s < 0) d = 1;
            if (s > 1) d = -1;
            c.a = s;
        }
        stv.color = c;
    }
}
