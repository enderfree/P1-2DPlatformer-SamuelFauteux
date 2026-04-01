using UnityEngine;

[CreateAssetMenu(fileName = "Skin", menuName = "Skin")]
public class Skin: ScriptableObject
{
    [SerializeField] private string skinName;
    [SerializeField] private string description;
    [SerializeField] private Material material;

    public  string Name { get { return skinName; } }

    public  string Description { get { return description; } }

    public  Material Material { get { return material; } }
}