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
            GameManager.instance.effectManager.RemoveElement(GetComponent<Element>().pos);
        }
        GameObject.Destroy(this.gameObject);
    }
}
