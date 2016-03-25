using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;
using itemPool;

class HMItem
{
    private string mName;
    private int mPrice;
    private string mImage;

    public string Name
    {
        get { return mName; }
        set { mName = value; }
    }

    public int Price
    {
        get { return mPrice; }
        set { mPrice = value; }
    }

    public string Image
    {
        get { return mImage; }
        set { mImage = value; }
    }

}

class HMWeaponItem : HMItem
{
    private int mAttackPoint;
    private string mExplain;
    private int mDurability;

    public int AttackPoint
    {
        get { return mAttackPoint; }
        set { mAttackPoint = value; }
    }

    public string Explain
    {
        get { return mExplain; }
        set { mExplain = value; }
    }

    public int Durability
    {
        get { return mDurability; }
        set { mDurability = value; }
    }
}

class HMArmorItem : HMItem
{
    private int mDef;
   
    public int Def
    {
        get { return mDef; }
        set { mDef = value; }
    }
}

public class csEquipageStore : MonoBehaviour
{
    //장비 상점 설정 값
    public int equipageNum;
    //장비 상점 그리드
    public GameObject gridPool;

    //상점칸에 무기 생성 필요 오브젝트
    public GameObject weaponPool;
    public GameObject weaponPoolNameText;
    public GameObject weaponPoolPriceText;
   
    //상점칸에 갑옷 생성 필요 오브젝트
    public GameObject armorPool;
    public GameObject armorPoolNameText;
    public GameObject armorPoolPriceText;
    

    public GameObject[] weaponPoolSet = new GameObject[5];
    public GameObject[] armorPoolSet = new GameObject[5];
    public GameObject[] bootPoolSet = new GameObject[5];

    private ArrayList mItems;
    private ArrayList aItems;

    public TextAsset textAsset;

    public GameObject grid;

    GameObject weaponSetObj = null;
    GameObject armorSetObj = null;

    public GameObject WeaponUse;
    public GameObject weaponNameText;
    public GameObject weaponDurabilityText;
    public GameObject weaponImage;

    public GameObject armorUse;
    public GameObject armorNameText;
    public GameObject armorImage;

    int wNum;

    public int playerGold;
    public GameObject playerGoldText;
    Sprite d;
    void LoadAssetfromJson()
    {
        mItems = new ArrayList();
        aItems = new ArrayList();
        Hashtable itemTable = (Hashtable)HMJson.objectFromJsonString(textAsset.text);

        foreach (String itemName in itemTable.Keys)
        {
            ArrayList itemInfos = (ArrayList)itemTable["weapon"];
            ArrayList itemInfosA = (ArrayList)itemTable["armor"];

            Debug.Log("[Item " + itemName + "]" + "\n");

            foreach (Hashtable itemInfo in itemInfos)
            {
                String name = (String)itemInfo["name"];
                String price = (String)itemInfo["weaponPrice"];
                String atkPoint = (String)itemInfo["weaponATK"];
                String durability = (String)itemInfo["weaponDurability"];
                String explain = (String)itemInfo["weaponExplain"];

                HMWeaponItem weaponItem = new HMWeaponItem();
                weaponItem.Name = name;
                weaponItem.Price = Int32.Parse(price);
                weaponItem.AttackPoint = Int32.Parse(atkPoint);
                weaponItem.Durability = Int32.Parse(durability);
                weaponItem.Explain = explain;

                mItems.Add(weaponItem);
            }

            foreach (Hashtable itemInfo in itemInfosA)
            {
                String name = (String)itemInfo["name"];
                String price = (String)itemInfo["armorPrice"];
                String defPoint = (String)itemInfo["armorDef"];

                HMArmorItem armorItem = new HMArmorItem();
                armorItem.Name = name;
                armorItem.Price = Int32.Parse(price);
                armorItem.Def = Int32.Parse(defPoint);
                aItems.Add(armorItem);
            }
        }
    }



    void Start()
    {
        LoadAssetfromJson();
        for (int i = 0; i < 3; i++)
        {
            this._setupWeapon(i);
            this._setupArmor(i);
        }

        playerGold += 500;
        playerGoldText.GetComponent<Text>().text = ": " + playerGold;
    }

    void Update()
    {
        StateManager.Instance.playGold = playerGold;


    }

    public void equipageWeapon()
    {
        equipageNum = 1;

        for (int i = 0; i < 3; i++)
        {
            weaponPoolSet[i].SetActive(true);
            armorPoolSet[i].SetActive(false);
            //bootPoolSet[i].SetActive(false);
        }
    }

    public void equipageArmor()
    {
        equipageNum = 2;
        for (int i = 0; i < 3; i++)
        {
            armorPoolSet[i].SetActive(true);
            //bootPoolSet[i].SetActive(false);
            weaponPoolSet[i].SetActive(false);
        }
    }
    public void equipageBoot()
    {
        equipageNum = 3;
        for (int i = 0; i < 3; i++)
        {
            armorPoolSet[i].SetActive(false);
            //bootPoolSet[i].SetActive(false);
            weaponPoolSet[i].SetActive(false);
        }
    }

    public void onClickWeaponButton()
    {
        this._itemWeapon(weaponSetObj, 0);
    }
    public void onClickWeaponButton50()
    {
        this._itemWeapon(weaponSetObj, 1);
    }
    public void onClickWeaponButton3()
    {
        this._itemWeapon(weaponSetObj, 2);
    }

    //public void onClickWeaponButton(int num)
    //{
    //    this._itemWeapon(weaponSetObj, num);
    //}
    public void onClickArmorButton(int num)
    {
        Debug.Log(num);
        
        this._itemArmor(armorSetObj, num, armorPoolSet[num].name);
    }

    private void _itemWeapon(GameObject gameObj, int itemIndex)
    {
        StateManager.Instance.bagNum = wNum;
        HMWeaponItem item = (HMWeaponItem)mItems[itemIndex];
        weaponDurabilityText.GetComponent<Text>().text = "내구도: " + item.Durability.ToString();
        weaponNameText.GetComponent<Text>().text = item.Name + " 공격력: " + item.AttackPoint.ToString();
        weaponImage.GetComponent<Image>().sprite = (Sprite)Resources.Load("WeaponBase", typeof(Sprite));
        gameObj = Instantiate(WeaponUse) as GameObject;
        gameObj.transform.SetParent(grid.transform);
        gameObj.transform.localScale = new Vector3(1, 1, 1);

        switch (itemIndex)
        {
            case 0:
                for (wNum = 0; wNum < 5; wNum++)
                {
                    if (StateManager.Instance.weaponSpace[wNum] == null)
                    {
                        gameObj.name = "Weapon10" + wNum;
                        StateManager.Instance.weaponDurability[wNum] = item.Durability;
                        StateManager.Instance.weaponSpace[wNum] = gameObj;
                        return;
                    }
                }
                break;
            case 1:
                for (wNum = 0; wNum < 5; wNum++)
                {
                    if (StateManager.Instance.weaponSpace[wNum] == null)
                    {
                        gameObj.name = "Weapon50" + wNum;
                        StateManager.Instance.weaponDurability[wNum] = item.Durability;
                        StateManager.Instance.weaponSpace[wNum] = gameObj;
                        return;
                    }

                }
                break;
        }
        //wNum++;
    }

    private void _setupWeapon(int itemIndex)
    {
        HMWeaponItem item = (HMWeaponItem)mItems[itemIndex];
        weaponPoolPriceText.GetComponent<Text>().text = item.Price.ToString() + "\n" + "골드";
        weaponPoolNameText.GetComponent<Text>().text = "이름: " + item.Name + "\n" + "설명: " + item.Explain + "\n" + "공격력: " + item.AttackPoint.ToString();
        weaponPoolSet[itemIndex] = Instantiate(weaponPool) as GameObject;
        weaponPoolSet[itemIndex].transform.SetParent(gridPool.transform);
        weaponPoolSet[itemIndex].transform.localScale = new Vector3(1, 1, 1);

        //weaponPoolSet[itemIndex].name = "Weapon" + (10 * itemIndex + 10);
        //weaponPoolSet[itemIndex].GetComponent<Button>().onClick.AddListener(delegate { onClickWeaponButton(itemIndex); });

        // UnityAction onClickAction = new UnityAction(onClickWeaponButton10);
        if (itemIndex == 0)
        {
            weaponPoolSet[itemIndex].name = "Weapon" + 10;
            weaponPoolSet[itemIndex].GetComponent<Button>().onClick.AddListener(onClickWeaponButton);
        }
        if (itemIndex == 1)
        {
            weaponPoolSet[itemIndex].name = "Weapon" + 50;
            weaponPoolSet[itemIndex].GetComponent<Button>().onClick.AddListener(onClickWeaponButton50);
        }
        if (itemIndex == 2)
        {
            weaponPoolSet[itemIndex].name = "Weapon" + 3;
            weaponPoolSet[itemIndex].GetComponent<Button>().onClick.AddListener(onClickWeaponButton3);
        }
        weaponPoolSet[itemIndex].SetActive(false);
    }
    private void _setupArmor(int itemIndex)
    {
        HMArmorItem item = (HMArmorItem)aItems[itemIndex];
       
        armorPoolPriceText.GetComponent<Text>().text = item.Price.ToString() + "\n" + "골드";
        armorPoolNameText.GetComponent<Text>().text = "이름: " + item.Name + "\n" + "설명: " + "\n" + "방어력: " + item.Def.ToString();
        armorPoolSet[itemIndex] = Instantiate(armorPool) as GameObject;
        armorPoolSet[itemIndex].transform.SetParent(gridPool.transform);
        armorPoolSet[itemIndex].transform.localScale = new Vector3(1, 1, 1);
        armorPoolSet[itemIndex].name = "Armor" + (itemIndex * 10 + 10);

        armorPoolSet[itemIndex].GetComponent<Button>().onClick.AddListener(delegate { onClickArmorButton(itemIndex); });

        armorPoolSet[itemIndex].SetActive(false);
    }


    private void _itemArmor(GameObject gameObj, int itemIndex, String armorName)
    {
        StateManager.Instance.bagNum = wNum;
        HMArmorItem item = (HMArmorItem)aItems[itemIndex];

        armorNameText.GetComponent<Text>().text = item.Name + " 방어력: " + item.Def.ToString();
        //armorImage.GetComponent<Image>().sprite = (Sprite)Resources.Load("WeaponBase", typeof(Sprite));
        gameObj = Instantiate(armorUse) as GameObject;
        gameObj.transform.SetParent(grid.transform);
        gameObj.transform.localScale = new Vector3(1, 1, 1);
        for (wNum = 0; wNum < 5; wNum++)
        {
            if (StateManager.Instance.weaponSpace[wNum] == null)
            {
                gameObj.name = armorName + wNum;
                StateManager.Instance.weaponSpace[wNum] = gameObj;
                return;
            }
        }
        switch (itemIndex)
        {
            case 0:
               
                break;
            case 1:
             
                break;
        }
        //wNum++;
    }
}
