using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EffectManager : MonoBehaviour
{
    public ParticleSystem bombEffect;
    private ParticleSystem effectList;

    private Dictionary<Vector2, ParticleSystem> parList;

    private void Start()
    {
        parList = new Dictionary<Vector2, ParticleSystem>();
    }

    public void PlayEffect(ElementType type, List<Element> list)
    {
        if (type == ElementType.Bomb)
        {
            foreach (var item in list)
            {
                ParticleSystem temParticle = GetEffect(item.pos);
                if(temParticle!=null)
                    temParticle.Play();
            }
        }
    }

    private ParticleSystem GetEffect(Vector2 targetPos)
    {
        ParticleSystem go = Instantiate(bombEffect);
        go.transform.position = targetPos;
        if (parList.ContainsKey(targetPos))
        {
            return null;
        }
        
        parList.Add(targetPos, go);
        return go;
    }

    public void RemoveElement(Vector2 targetPos)
    {
        parList.Remove(targetPos);
    }

    public Animator textPrefab;
    private Animator animator;
    public void PlayTextAnimation(Vector2 targetPos, string content)
    {
        if (animator != null)
        {
            animator.GetComponentInChildren<Text>().text = content;
            return;
        }
        animator = Instantiate(textPrefab);
        textPrefab.GetComponentInChildren<Text>().text = content;
        animator.transform.position = targetPos;
    }
}
