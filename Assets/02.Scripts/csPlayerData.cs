using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class csPlayerData : MonoBehaviour
{
    GameObject weaponObj;
    GameObject scrollObj;
    GameObject itemObj;

    public GameObject wGrid;
    public GameObject sGrid;
    public GameObject mGrid;
    public GameObject bGrid;
    public GameObject iGrid;

    GameObject[] weaponSpace;

    GameObject[] potionItemBag;
    GameObject[] SkScrollBag;
    GameObject[] MgScrollBag;
    GameObject[] BufScrollBag;

    public GameObject playerHpText;
    public GameObject playerAtkText;
    public GameObject playerDefText;
    public GameObject playerSpdText;
    public GameObject playerGoldText;

    public GameObject dText;

    public GameObject useWeapons;
    public GameObject useArmors;
    public GameObject useBoots;

    Image useWeapon;

    int i =0;
  
    void Awake()
    {
        if (StateManager.Instance.useWeapon != null)
        {
            useWeapons.GetComponent<Image>().sprite = StateManager.Instance.useWeapon;
        }
        if (StateManager.Instance.useArmor != null)
        {
            useArmors.GetComponent<Image>().sprite = StateManager.Instance.useArmor;
        }
        if (StateManager.Instance.useBoots != null)
        {
            useBoots.GetComponent<Image>().sprite = StateManager.Instance.useBoots;
        }

        if (StateManager.Instance.weaponSpace[StateManager.Instance.wUse] !=null)
        {
            useWeapon = StateManager.Instance.weaponSpace[StateManager.Instance.wUse].transform.FindChild("weaponUseIcon").GetComponentInChildren<Image>();
            if (useWeapon.enabled == true)
            {
                dText.GetComponent<Text>().text = StateManager.Instance.weaponDurability[StateManager.Instance.wUse].ToString();
            }
        }
        weaponSpace = StateManager.Instance.weaponSpace;

        potionItemBag = StateManager.Instance.potionItemBag;
        SkScrollBag = StateManager.Instance.SkScrollBag;
        MgScrollBag = StateManager.Instance.MgScrollBag;
        BufScrollBag = StateManager.Instance.BufScrollBag;

        StateManager.Instance.dText = dText;

        for (i = 0; i < weaponSpace.Length; i++)
        {
            if (weaponSpace[i] == null)
            {
                Debug.Log("들어옴" + i);
            }
            else if (weaponSpace[i] != null)
            {
                weaponObj = weaponSpace[i];
                weaponObj.transform.SetParent(wGrid.transform);
                weaponObj.transform.localScale = new Vector3(1, 1, 1);
            }
        }

        for (i = 0; i < potionItemBag.Length; i++)
        {
            if (potionItemBag[i] == null)
            {

            }
            else if (potionItemBag[i] != null)
            {
                itemObj = potionItemBag[i];
                itemObj.transform.SetParent(iGrid.transform);
                itemObj.transform.localScale = new Vector3(1, 1, 1);
            }
        }
        for (i = 0; i < SkScrollBag.Length; i++)
        {
            if (SkScrollBag[i] == null)
            {

            }
            else if (SkScrollBag[i] != null)
            {
                itemObj = SkScrollBag[i];
                itemObj.transform.SetParent(sGrid.transform);
                itemObj.transform.localScale = new Vector3(1, 1, 1);
            }
        }
        for (i = 0; i < MgScrollBag.Length; i++)
        {
            if (MgScrollBag[i] == null)
            {

            }
            else if (MgScrollBag[i] != null)
            {
                itemObj = MgScrollBag[i];
                itemObj.transform.SetParent(mGrid.transform);
                itemObj.transform.localScale = new Vector3(1, 1, 1);
            }
        }

        for (i = 0; i < BufScrollBag.Length; i++)
        {
            if (BufScrollBag[i] == null)
            {

            }
            else if (BufScrollBag[i] != null)
            {
                itemObj = BufScrollBag[i];
                itemObj.transform.SetParent(bGrid.transform);
                itemObj.transform.localScale = new Vector3(1, 1, 1);
            }
        }


        playerGoldText.GetComponent<Text>().text = "" + StateManager.Instance.playGold;

        playerHpText.GetComponent<Text>().text = "" + StateManager.Instance.playHp;
        playerAtkText.GetComponent<Text>().text = "" + StateManager.Instance.playAtk + " + " + StateManager.Instance.playUseAtk;
        playerDefText.GetComponent<Text>().text = "" + StateManager.Instance.playDef + " + " + StateManager.Instance.playUseDef;
        playerSpdText.GetComponent<Text>().text = "" + StateManager.Instance.playSpd + " + " + StateManager.Instance.playUseSpd;
        

        if (Application.loadedLevel == 1)
        {
            DestroyObject(GameObject.FindGameObjectWithTag("villageCanvas"));
        }
        if (Application.loadedLevel == 0)
        {
            DestroyObject(GameObject.FindGameObjectWithTag("Map"));
        }
    }
}
