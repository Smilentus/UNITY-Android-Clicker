using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveableText : MonoBehaviour
{
    // Движение текста 
    // (Профессиональный программист - это когда ты не идёшь в инет за решением, 
    // а думаешь сам, как реализовать ту или иную задачу! :D (c) Димас
    System.Random rnd = new System.Random();

    private void Update()
    {
        if (gameObject.name.Contains("(Clone)"))
            gameObject.name = "PlusText";
        this.transform.position += new Vector3(0, 0.07f, 0);
        if(this.transform.localPosition.y >= rnd.Next(240,270))
            Destroy(gameObject);
    }
}
