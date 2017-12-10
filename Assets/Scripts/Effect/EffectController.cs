using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectController : MonoBehaviour
{
    public float destoryTime = 0.3f;
    public ElementType type;
    private void Start()
    {
        Invoke("DestorySelf", destoryTime);
    }

    private void DestorySelf()
    {
        if (type == ElementType.Bomb)
        {
            gameObject.SetActive(false);
        }
        else
        {
            GameObject.Destroy(this.gameObject);
        }
    }
}
