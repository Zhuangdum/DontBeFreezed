using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public ElementMenu configMenu;

    public static GameManager instance;

    public MapGenerator mapGenerator;

    public UIManager uiManager;

    private Dictionary<ElementType, int> tools;
    public int fireCount = 3;
    public int fuelCount = 3;
    public int boombCount = 3;
    private void Awake()
    {
        instance = this;
        tools = new Dictionary<ElementType, int>();
        tools.Add(ElementType.Fire, fireCount);
        tools.Add(ElementType.Fuel, fuelCount);
        tools.Add(ElementType.Bomb, boombCount);
        uiManager.Init();
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
                        
                        if (tools.ContainsKey(uiManager.handType) && tools[uiManager.handType] > 0)
                        {
                            Land land = clickedElement as Land;
                            if (land == null)
                            {
                                Debug.LogError("xxxx");
                                return;
                            }
                            land.BeEffected(land, uiManager.handType);
                            tools[uiManager.handType]--;
                            Debug.Log(" 使用道具类型： "+ uiManager.handType+"  道具数量： "+tools[uiManager.handType]);
                        }
                        else
                        {
                            Debug.Log(" 道具数量不足 "+ uiManager.handType);
                        }
                    }
                    else
                    {
                        Debug.Log("this place already have something");
                    }
                }
            }
        }
    }

    
}
