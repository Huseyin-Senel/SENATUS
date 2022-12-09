using Microsoft.Unity.VisualStudio.Editor;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Item;
using static UnityEditor.PlayerSettings;
using static UnityEditor.Progress;

public class Dukkan : MonoBehaviour
{
    [Header("Slotlar")]
    [SerializeField] public List<GameObject> slots;
    public GameObject itemPrefab;


    void Start()
    {

        int i = 0;
        foreach (Item.Items item in Enum.GetValues(typeof(Item.Items)))
        {
            createItem(item, slots[i]);
            i++;
        }
        createItem(Items.CanPotu, slots[i]);
        i++;
        createItem(Items.CanPotu, slots[i]);

    }




    public GameObject createItem(Items item, GameObject slot)
    {
        GameObject itemA = Instantiate(itemPrefab, slot.transform.position, Quaternion.identity, slot.transform);
        Slot slotScript = slot.GetComponent<Slot>();
        slotScript.setItem(itemA);

        switch (item)
        {
            case Items.ShieldLv1:
                itemA.GetComponent<Item>().SetAll("ShieldLv1", 1, Item.Types.Shield, Resources.LoadAll<Sprite>("legion 1")[14], 100, slot);
                break;
            case Items.ShieldLv2:
                itemA.GetComponent<Item>().SetAll("ShieldLv2", 2, Item.Types.Shield, Resources.LoadAll<Sprite>("legion 1")[15], 100, slot);
                break;
            case Items.ShieldLv3:
                itemA.GetComponent<Item>().SetAll("ShieldLv3", 3, Item.Types.Shield, Resources.LoadAll<Sprite>("legion 1")[19], 100, slot);
                break;
            case Items.ShieldLv4:
                itemA.GetComponent<Item>().SetAll("ShieldLv4", 4, Item.Types.Shield, Resources.LoadAll<Sprite>("legion 1")[23], 100, slot);
                break;
            case Items.ShieldLv5:
                itemA.GetComponent<Item>().SetAll("ShieldLv5", 5, Item.Types.Shield, Resources.LoadAll<Sprite>("legion 1")[11], 100, slot);
                break;
            case Items.ArmorLv1:
                itemA.GetComponent<Item>().SetAll("ArmorLv1", 1, Item.Types.Armor, Resources.LoadAll<Sprite>("legion 1")[21], 100, slot);
                break;
            case Items.ArmorLv2:
                itemA.GetComponent<Item>().SetAll("ArmorLv2", 2, Item.Types.Armor, Resources.LoadAll<Sprite>("legion 1")[20], 100, slot);
                break;
            case Items.ArmorLv3:
                itemA.GetComponent<Item>().SetAll("ArmorLv3", 3, Item.Types.Armor, Resources.LoadAll<Sprite>("legion 1")[22], 100, slot);
                break;
            case Items.BowLv1:
                itemA.GetComponent<Item>().SetAll("BowLv1", 2, Item.Types.Bow, Resources.LoadAll<Sprite>("legion 1")[6], 100, slot);
                break;
            case Items.BowLv2:
                itemA.GetComponent<Item>().SetAll("BowLv2", 4, Item.Types.Bow, Resources.LoadAll<Sprite>("legion 1")[10], 100, slot);
                break;
            case Items.SwordLv1:
                itemA.GetComponent<Item>().SetAll("SwordLv1", 1, Item.Types.Sword, Resources.LoadAll<Sprite>("legion 1")[2], 100, slot);
                break;
            case Items.SwordLv2:
                itemA.GetComponent<Item>().SetAll("SwordLv2", 2, Item.Types.Sword, Resources.LoadAll<Sprite>("legion 1")[0], 100, slot);
                break;
            case Items.SwordLv3:
                itemA.GetComponent<Item>().SetAll("SwordLv3", 3, Item.Types.Sword, Resources.LoadAll<Sprite>("legion 1")[1], 100, slot);
                break;
            case Items.SwordLv4:
                itemA.GetComponent<Item>().SetAll("SwordLv4", 4, Item.Types.Sword, Resources.LoadAll<Sprite>("legion 1")[3], 100, slot);
                break;
            case Items.SpearLv1:
                itemA.GetComponent<Item>().SetAll("SpearLv1", 1, Item.Types.Sword, Resources.LoadAll<Sprite>("legion 1")[8], 100, slot);
                break;
            case Items.SpearLv2:
                itemA.GetComponent<Item>().SetAll("SpearLv2", 2, Item.Types.Sword, Resources.LoadAll<Sprite>("legion 1")[9], 100, slot);
                break;
            case Items.SpearLv3:
                itemA.GetComponent<Item>().SetAll("SpearLv3", 3, Item.Types.Sword, Resources.LoadAll<Sprite>("legion 1")[5], 100, slot);
                break;
            case Items.SpearLv4:
                itemA.GetComponent<Item>().SetAll("SpearLv4", 4, Item.Types.Sword, Resources.LoadAll<Sprite>("legion 1")[4], 100, slot);
                break;
            case Items.AxeLv1:
                itemA.GetComponent<Item>().SetAll("AxeLv1", 2, Item.Types.Sword, Resources.LoadAll<Sprite>("legion 1")[12], 100, slot);
                break;
            case Items.AxeLv2:
                itemA.GetComponent<Item>().SetAll("AxeLv2", 3, Item.Types.Sword, Resources.LoadAll<Sprite>("legion 1")[13], 100, slot);
                break;
            case Items.CanPotu:
                itemA.GetComponent<Item>().SetAll("CanPotu", 50, Item.Types.Pot, Resources.Load<Sprite>("S_ItemNoOutline_PotionRed_01-1"), 100, slot);
                //itemA.GetComponent<RectTransform>()
                break;
            default:
                break;
        }

        return itemA;

        //Resources.LoadAll<Sprite>("2D Pixel Item Pack/Heavy Outline/S_ItemHeavyOutline_PotionRed_01.png")[15]

    }

}
