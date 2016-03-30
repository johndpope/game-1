using UnityEngine;
using System.Collections;



public class csUseEquip : MonoBehaviour
{

    public static bool itemUsePopBool;
    public static int equipNum;

    private ArrayList wItems;
    private ArrayList aItems;
    private ArrayList bItems;
   
    void Start()
    {
        equipNum = 0;
        itemUsePopBool = false;
        wItems = StateManager.Instance.weaponItems;
        aItems = StateManager.Instance.armorItems;
        bItems = StateManager.Instance.bootItems;
    }
	
	// Update is called once per frame
	void Update ()
    {
	
	}

    public void EquipWeapon()
    {
        for (int j = 0; j < wItems.Count; j++)
        {
            HMWeaponItem item = (HMWeaponItem)wItems[j];
            for (int i = 0; i < 10; i++)
            {
                if (gameObject.name == item.WeaponName + i)
                {
                    equipNum = 1;
                    itemUsePopBool = true;
                    StateManager.Instance.bagNum = i;
                }
            }
        }
    }

    public void EquipArmor()
    {
        for (int j = 0; j < wItems.Count; j++)
        {
            HMArmorItem item = (HMArmorItem)wItems[j];
            for (int i = 0; i < 10; i++)
            {
                if (gameObject.name == item.ArmorName + i)
                {
                    equipNum = 2;
                    itemUsePopBool = true;
                    StateManager.Instance.bagNum = i;
                }
            }
        }
    }

    public void EquipBoots()
    {
        for (int j = 0; j < wItems.Count; j++)
        {
            HMBootsItem item = (HMBootsItem)wItems[j];
            for (int i = 0; i < 10; i++)
            {
                if (gameObject.name == item.BootsName + i)
                {
                    equipNum = 3;
                    itemUsePopBool = true;
                    StateManager.Instance.bagNum = i;
                }
            }
        }
    }
}
