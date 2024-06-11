using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Item", menuName = "Scriptable object/Item")]
public class Item : ScriptableObject
{

    public string itemName;
    public Sprite itemSprite;
    public GameObject itemPrefab;

}
