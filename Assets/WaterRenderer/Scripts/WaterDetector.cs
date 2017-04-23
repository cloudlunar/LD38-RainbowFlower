using UnityEngine;
using System.Collections;

public class WaterDetector : MonoBehaviour
{

    void OnTriggerEnter2D(Collider2D Hit)
    {
        if (Hit.GetComponent<Rigidbody2D>() != null)
        {
            var vy = Hit.GetComponent<Rigidbody2D>().velocity.y * Hit.GetComponent<Rigidbody2D>().mass / 40f;
            if (vy == 0) vy = 0.1f;
            transform.parent.GetComponent<Water>().Splash(transform.position.x, vy);
        }
    }

    /*void OnTriggerStay2D(Collider2D Hit)
    {
        //print(Hit.name);
        if (Hit.rigidbody2D != null)
        {
            int points = Mathf.RoundToInt(Hit.transform.localScale.x * 15f);
            for (int i = 0; i < points; i++)
            {
                transform.parent.GetComponent<Water>().Splish(Hit.transform.position.x - Hit.transform.localScale.x + i * 2 * Hit.transform.localScale.x / points, Hit.rigidbody2D.mass * Hit.rigidbody2D.velocity.x / 10f / points * 2f);
            }
        }
    }*/

}
