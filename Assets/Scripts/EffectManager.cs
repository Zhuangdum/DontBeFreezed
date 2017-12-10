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
//                GameManager.instance.mapGenerator.GetTargetElement(item.pos).PlayEffect();
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

    public Sprite bombSprite;
    public Sprite fuelSprite;
    public Sprite fireSprite;
    public void PlayTextAnimation(Vector2 targetPos, int type)
    {
        animator = Instantiate(textPrefab);
        Image image = textPrefab.GetComponentInChildren<Image>();
        image.sprite = null;
        if(type == 1)
            image.sprite = bombSprite;
        else if(type == 2)
            image.sprite = fuelSprite;
        else
            image.sprite = fireSprite;
        animator.transform.position = targetPos;
    }
}
