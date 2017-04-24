using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class musicplayer : MonoBehaviour {
    AudioSource ass;
    public  float l=0, r=0;
    public  bool ex = false;
	// Use this for initialization
	void Start () {
        ass = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
        var inrange = (l < Creature.player.transform.position.x && Creature.player.transform.position.x < r);
        if (ex) inrange = !inrange;
        if (inrange) ass.volume = Mathf.Clamp01(ass.volume + 0.3f*Time.deltaTime);
        else ass.volume = Mathf.Clamp01(ass.volume -0.3f*Time.deltaTime);
        if(ass.volume<0.1&& ass.isPlaying)
        { ass.Stop();
        }else if(!ass.isPlaying)
        {
            ass.Play();
        }
    }
#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(new Vector3(l, -9999, 0), new Vector3(l, 9999, 0));
        Gizmos.DrawLine(new Vector3(r, -9999, 0), new Vector3(r, 9999, 0));
    }
#endif
}
