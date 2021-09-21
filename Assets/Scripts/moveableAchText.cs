using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveableAchText : MonoBehaviour
{
	void Update ()
    {
        if (gameObject.name.Contains("(Clone)"))
            gameObject.name = "achText";
        this.transform.SetAsLastSibling();
        if (this.transform.localPosition.x <= 180)
            StartCoroutine(WaitForDestroy());
        else
            this.transform.position -= new Vector3(0.05f, 0, 0);
    }

    IEnumerator WaitForDestroy()
    {
        yield return new WaitForSeconds(5);
        Player.achSpawned = false;
        Destroy(gameObject);
    }
}
