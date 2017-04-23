using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    public Vector3 offset = new Vector3(0, 0, 0);
	void Update () {
	if(Creature.player!=null)
        {
            transform.position = Creature.player.transform.position + offset;
        }
	}
}
