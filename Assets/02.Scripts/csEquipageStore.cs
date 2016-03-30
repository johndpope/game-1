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
    private string mExplain;

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
    public string Explain
    {
        get { return mExplain; }
        set { mExplain = value; }
    }
}

class HMWeaponItem : HMItem
{
    private int mAttackPoint;
    private int mDurability;

    public int AttackPoint
    {
        get { return mAttackPoint; }
        set { mAttackPoint = value; }
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

class HMBootsItem : HMItem
{
    private int mSpd;

    public int Spd
    {
        get { return mSpd; }
        set { mSpd = value; }
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

    private ArrayList wItems;
    private ArrayList aItems;
    private ArrayList bItems;

    public TextAsset textAsset;

    public GameObject grid;

    GameObject weaponSetObj = null;
    GameObject setObj = null;

    public GameObject WeaponUse;
    public GameObject weaponNameText;
    public GameObject weaponDurabilityText;
    public GameObject weaponImage;

    public GameObject armorUse;
    public GameObject armorNameText;
    public GameObject armorImage;

    int wNum;
    ArrayList itemInfos;
    ArrayList itemInfosA;
    ArrayList itemInfosB;
    public int playerGold;
    public GameObject playerGoldText;
    Sprite d;

    string itemDescFormat = "이름: {0}\n설명: {1}\n{2}: {3}";
    string itemPrice = "{0}\n골드";

    void LoadAssetfromJson()
    {
        wItems = new ArrayList();
        aItems = new ArrayList();
        bItems = new ArrayList();

        Hashtable itemTable = (Hashtable)HMJson.objectFromJsonString(textAsset.text);

        foreach (String itemName in itemTable.Keys)
        {
            itemInfos = (ArrayList)itemTable["weapon"];
            itemInfosA = (ArrayList)itemTable["armor"];
            itemInfosB = (ArrayList)itemTable["boots"];
            //Debug.Log("[Item " + itemName + "]" + "\n");

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

                wItems.Add(weaponItem);
            }

            foreach (Hashtable itemInfo in itemInfosA)
            {
                String name = (String)itemInfo["name"];
                String price = (String)itemInfo["price"];
                String defPoint = (String)itemInfo["armorDef"];

                HMArmorItem armorItem = new HMArmorItem();
                armorItem.Name = name;
                armorItem.Price = Int32.Parse(price);
                armorItem.Def = Int32.Parse(defPoint);
                aItems.Add(armorItem);
            }

            foreach (Hashtable itemInfo in itemInfosB)
            {
                String name = (String)itemInfo["name"];
                String price = (String)itemInfo["price"];
                String bootsSpd = (String)itemInfo["bootsSpd"];

                HMBootsItem bootsItem = new HMBootsItem();
                bootsItem.Name = name;
                bootsItem.Price = Int32.Parse(price);
                bootsItem.Spd = Int32.Parse(bootsSpd);
                bItems.Add(bootsItem);
            }
        }
    }



    void Start()
    {
        LoadAssetfromJson();
 
        for (int i = 0; i < itemInfos.Count; i++)
        {
            this._setupWeapon(i);
        }
        for (int i = 0; i < itemInfosA.Count; i++)
        {
            this._setupArmor(i);
        }
        for (int i = 0; i < itemInfosB.Count; i++)
        {
            this._setupBoots(i);
        }
        playerGold += 900;
        StateManager.Instance.playGold = playerGold;

        StateManager.Instance.weaponItems = wItems;
        StateManager.Instance.armorItems = aItems;
        StateManager.Instance.bootItems = bItems;

    }

    void Update()
    {
        playerGoldText.GetComponent<Text>().text = ": " + StateManager.Instance.playGold;
    }

    public void equipageWeapon()
    {
        equipageNum = 1;

        for (int i = 0; i < 6; i++)
        {
            weaponPoolSet[i].SetActive(true);
            armorPoolSet[i].SetActive(false);
            bootPoolSet[i].SetActive(false);
        }
        
    }

    public void equipageArmor()
    {
        equipageNum = 2;
        for (int i = 0; i < 6; i++)
        {
            armorPoolSet[i].SetActive(true);
            bootPoolSet[i].SetActive(false);
            weaponPoolSet[i].SetActive(false);
        }
    }
    public void equipageBoot()
    {
        equipageNum = 3;
        for (int i = 0; i < 6; i++)
        {
            armorPoolSet[i].SetActive(false);
            bootPoolSet[i].SetActive(true);
            weaponPoolSet[i].SetActive(false);
        }
    }

    //무기 사기 위한 버튼
    public void onClickWeaponButton(int num)
    {
        //ArrayList 싱글톤으로 값을 저장후 비교할것
        if (StateManager.Instance.playGold >= ((num * 2) + 1) * 100)
        {
            if (num >= 0 && num <= 2)
            {
                StateManager.Instance.playGold -= (num + 1) * 100;
                this._itemWeapon(weaponSetObj, num, weaponPoolSet[num].name);
            }
            else if (num >= 3 && num <= 5)
            {
                StateManager.Instance.playGold -= ((num * 2) + 1) * 100;
                this._itemWeapon(weaponSetObj, num, weaponPoolSet[num].name);
            }
        }
    }

    public void onClickArmorButton(int num)
    {
        this._itemArmor(setObj, num, armorPoolSet[num].name);
    }

    public void onClickBootsButton(int num)
    {
        Debug.Log(num);
        this._itemBoots(setObj, num, bootPoolSet[num].name);
    }

    private void _itemWeapon(GameObject gameObj, int itemIndex, String weaponName)
    {
        if (StateManager.Instance.bagSize == 5)
        {
            return;
        }
        StateManager.Instance.bagSize++;
        StateManager.Instance.bagNum = wNum;
        HMWeaponItem item = (HMWeaponItem)wItems[itemIndex];
        weaponDurabilityText.GetComponent<Text>().text = "내구도: " + item.Durability.ToString();
        weaponNameText.GetComponent<Text>().text = item.Name + " 공격력: " + item.AttackPoint.ToString();
        //리소스에 이미지 추가되면 주석 지울것 
        //weaponImage.GetComponent<Image>().sprite = (Sprite)Resources.Load( item.Image, typeof(Sprite));
        gameObj = Instantiate(WeaponUse) as GameObject;
        gameObj.transform.SetParent(grid.transform);
        gameObj.transform.localScale = new Vector3(1, 1, 1);


        for (wNum = 0; wNum < 5; wNum++)
        {
            if (StateManager.Instance.weaponSpace[wNum] == null)
            {
                gameObj.name = weaponName + wNum;
                StateManager.Instance.weaponDurability[wNum] = item.Durability;
                StateManager.Instance.weaponSpace[wNum] = gameObj;
                return;
            }
        }

    }

    private void _setupWeapon(int itemIndex)
    {
        HMWeaponItem item = (HMWeaponItem)wItems[itemIndex];
        
        string str;
        string strGold;
        string gold = item.Price.ToString();
        strGold = String.Format(itemPrice, gold);

        string str1 = item.Name;
        string str2 = item.Explain;
        string str3 = "공격력: ";
        string str4 = item.AttackPoint.ToString();
      
        // 문자열 포맷을 지정하여 저장
        str = String.Format(itemDescFormat, str1, str2, str3, str4);
        

        weaponPoolPriceText.GetComponent<Text>().text = strGold;

        weaponPoolNameText.GetComponent<Text>().text = str;

        weaponPoolSet[itemIndex] = Instantiate(weaponPool) as GameObject;
        weaponPoolSet[itemIndex].transform.SetParent(gridPool.transform);
        weaponPoolSet[itemIndex].transform.localScale = new Vector3(1, 1, 1);
        
        weaponPoolSet[itemIndex].name = "Weapon" + (10 * itemIndex + 10);
        weaponPoolSet[itemIndex].GetComponent<Button>().onClick.AddListener(delegate { onClickWeaponButton(itemIndex); });
        if (itemIndex == 3)
        {
            weaponPoolSet[itemIndex].name = "Weapon" + 50;
        }

        if (itemIndex == 4)
        {
            weaponPoolSet[itemIndex].name = "Weapon" + 55;
        }

        if (itemIndex == 5)
        {
            weaponPoolSet[itemIndex].name = "Weapon" + 3;
        }
        weaponPoolSet[itemIndex].SetActive(false);
    }

    private void _setupArmor(int itemIndex)
    {
        HMArmorItem item = (HMArmorItem)aItems[itemIndex];

        string strGold;
        string gold = item.Price.ToString();
        strGold = String.Format(itemPrice, gold);

        string str;
        string str1 = item.Name;
        string str2 = "";
        string str3 = "방어력: ";
        string str4 = item.Def.ToString();
        // 문자열 포맷을 지정하여 저장
        str = String.Format(itemDescFormat, str1, str2, str3, str4);

        armorPoolPriceText.GetComponent<Text>().text = strGold;

        armorPoolNameText.GetComponent<Text>().text = str;

        armorPoolSet[itemIndex] = Instantiate(armorPool) as GameObject;
        armorPoolSet[itemIndex].transform.SetParent(gridPool.transform);
        armorPoolSet[itemIndex].transform.localScale = new Vector3(1, 1, 1);
        armorPoolSet[itemIndex].name = "Armor" + (itemIndex * 10 + 10);

        armorPoolSet[itemIndex].GetComponent<Button>().onClick.AddListener(delegate { onClickArmorButton(itemIndex); });

        armorPoolSet[itemIndex].SetActive(false);
    }


    private void _itemArmor(GameObject gameObj, int itemIndex, String armorName)
    {
        if (StateManager.Instance.bagSize == 5)
        {
            return;
        }
        StateManager.Instance.bagSize++;
        StateManager.Instance.bagNum = wNum;
        HMArmorItem item = (HMArmorItem)aItems[itemIndex];

        armorNameText.GetComponent<Text>().text = item.Name + "\n" + " 방어력: " + item.Def.ToString();

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
    }

    private void _setupBoots(int itemIndex)
    {
        HMBootsItem item = (HMBootsItem)bItems[itemIndex];

        string strGold;
        string gold = item.Price.ToString();
        strGold = String.Format(itemPrice, gold);

        string str;
        string str1 = item.Name;
        string str2 = "";
        string str3 = "속도: ";
        string str4 = item.Spd.ToString();
        // 문자열 포맷을 지정하여 저장
        str = String.Format(itemDescFormat, str1, str2, str3, str4);

        armorPoolPriceText.GetComponent<Text>().text = strGold;

        armorPoolNameText.GetComponent<Text>().text = str;

        bootPoolSet[itemIndex] = Instantiate(armorPool) as GameObject;
        bootPoolSet[itemIndex].transform.SetParent(gridPool.transform);
        bootPoolSet[itemIndex].transform.localScale = new Vector3(1, 1, 1);
        bootPoolSet[itemIndex].name = "Boots" + (itemIndex * 10 + 10);

        bootPoolSet[itemIndex].GetComponent<Button>().onClick.AddListener(delegate { onClickBootsButton(itemIndex); });

        bootPoolSet[itemIndex].SetActive(false);
    }

    private void _itemBoots(GameObject gameObj, int itemIndex, String bootsName)
    {
        if (StateManager.Instance.bagSize == 5)
        {
            return;
        }
        StateManager.Instance.bagSize++;
        StateManager.Instance.bagNum = wNum;
        HMArmorItem item = (HMArmorItem)aItems[itemIndex];

        armorNameText.GetComponent<Text>().text = item.Name + "\n" + " 방어력: " + item.Def.ToString();

        //armorImage.GetComponent<Image>().sprite = (Sprite)Resources.Load("WeaponBase", typeof(Sprite));
        gameObj = Instantiate(armorUse) as GameObject;
        gameObj.transform.SetParent(grid.transform);
        gameObj.transform.localScale = new Vector3(1, 1, 1);
        for (wNum = 0; wNum < 5; wNum++)
        {
            if (StateManager.Instance.weaponSpace[wNum] == null)
            {
                gameObj.name = bootsName + wNum;
                StateManager.Instance.weaponSpace[wNum] = gameObj;
                return;
            }
        }
    }
}
