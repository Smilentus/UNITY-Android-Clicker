using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBackground : MonoBehaviour
{
    public GameObject Background;
    public static float _moveSpeed = 0.07f;

    // Движение фона
    void Update()
    {
        if (_moveSpeed > 100)
            _moveSpeed = 100f;
        if(gameObject.name.Contains("(Clone)"))
            gameObject.name = "Background";
        if(transform.position.y <= -13f)
        {
            Instantiate(Background, new Vector3(0f, 13f, 10f), Quaternion.identity);
            Destroy(gameObject);
        }
        transform.position -= new Vector3(0, _moveSpeed * Time.deltaTime, 0);
    }
}
