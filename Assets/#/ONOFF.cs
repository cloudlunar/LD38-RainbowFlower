using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class ONOFF : MonoBehaviour {
    public Transform target;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        transform.DORotate(new Vector3(0, 0, 60 - transform.rotation.eulerAngles.z), 0.5f);
        target.DOScale(new Vector3(1- target.localScale.x, 1,1), 0.5f);
    }
}
