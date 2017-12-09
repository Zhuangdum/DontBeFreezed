using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ElementMenu", menuName = "Map/ElementMenu")]
public class ElementMenu : ScriptableObject
{
    public List<ElementConfig> configList;
}
