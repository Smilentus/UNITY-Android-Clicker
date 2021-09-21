using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveableMoneyText : MonoBehaviour
{
	void Update ()
    {
        if (gameObject.name.Contains("(Clone)"))
            gameObject.name = "plusMoneyText";
        this.transform.SetAsLastSibling();
        if (this.transform.localPosition.y >= -400)
            Destroy(gameObject);
        else
            this.transform.position += new Vector3(0, 0.03f, 0);
    }
}
