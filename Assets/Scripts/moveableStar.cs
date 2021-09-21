using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveableStar : MonoBehaviour
{
    public float speed = 7f;

    // Движение звезды
    void Update()
    {
        if (this.gameObject.name.Contains("(Clone)"))
            this.gameObject.name = "neonStar";
        if (transform.position.y <= -6)
            Destroy(gameObject);
        transform.position -= new Vector3(0, speed * Time.deltaTime, 0);
    }
}
