using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterFade : MonoBehaviour {

    public float fadeto = 0, fadefrom = 1;
    private void Start()
    {

        GetComponent<SpriteRenderer>().DOFade(fadefrom, 0.3f);

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.GetComponent<PlayerControl>())
        GetComponent<SpriteRenderer>().DOFade(fadeto, 0.3f);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {

        if (collision.gameObject.GetComponent<PlayerControl>())
            GetComponent<SpriteRenderer>().DOFade(fadefrom, 0.3f);
    }
#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        var b = GetComponent<Collider2D>().bounds;
        Gizmos.color = Color.green * 0.4f;
        Gizmos.DrawCube(b.center, b.size);
    }
#endif
}
