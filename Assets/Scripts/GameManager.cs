using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public ElementMenu configMenu;

    public static GameManager instance;

    public MapGenerator mapGenerator;

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hitInfo;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hitInfo))
            {
                if (hitInfo.collider.CompareTag("Rock"))
                {
                    Element clickedElement = hitInfo.collider.GetComponent<Element>();
                    if (clickedElement.type == ElementType.Trap)
                    {
                        Debug.LogError("you clicked a trap, pos : " + clickedElement.pos);
                        return;
                    }
                    else if(clickedElement.type == ElementType.Land)
                    {
                        Land land = clickedElement as Land;
                        if (land == null)
                            Debug.LogError("xxxx");
                        land.BeEffected(land, ElementType.Fire);
                    }
                    else
                    {
                        Debug.Log("this has some tools already");
                    }
                }
            }
        }
    }

    
}
