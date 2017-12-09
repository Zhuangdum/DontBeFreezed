using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ElementConfig", menuName = "Map/Element")]
public class ElementConfig : ScriptableObject
{
    public GameObject prefab;
    public ElementType type;
    public int radius;
    public Color color;
}

public enum ElementType
{
    Land = 0,
    Default = 1,
    Fire = 99,
    Stone1 = 11,
    Stone2 = 12,
    Fuel = 32,
    Trap = 41,
    House = 16,
    House1 = 15,
    Wood = 20,
    Boomb = 31,
    Treasure = 42
}