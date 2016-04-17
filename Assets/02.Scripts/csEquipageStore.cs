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
    private string mWeaponName;
    public string WeaponName
    {
        get { return mWeaponName; }
        set { mWeaponName = value; }
    }
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
    private string mArmorName;

    public string ArmorName
    {
        get { return mArmorName; }
        set { mArmorName = value; }
    }
    public int Def
    {
        get { return mDef; }
        set { mDef = value; }
    }
}

class HMBootsItem : HMItem
{
    private float mSpd;
    private string mBootsName;

    public string BootsName
    {
        get { return mBootsName; }
        set { mBootsName = value; }
    }
    public float Spd
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
    public GameObject weaponBackgroundImage;
   
    //상점칸에 갑옷 생성 필요 오브젝트
    public GameObject armorPool;
    public GameObject armorPoolNameText;
    public GameObject armorPoolPriceText;
    public GameObject armorBackgroundImage;



    public GameObject[] weaponPoolSet;
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

    public GameObject bootsUse;
    public GameObject bootsNameText;
    public GameObject bootsImage;

    int wNum;
    ArrayList itemInfos;
    ArrayList itemInfosA;
    ArrayList itemInfosB;

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
                String weaponExplain = (String)itemInfo["weaponExplain"];
                String weaponName = (String)itemInfo["weaponName"];
                String weaponImage = (String)itemInfo["weaponImage"];

                HMWeaponItem weaponItem = new HMWeaponItem();
                weaponItem.Name = name;
                weaponItem.Price = Int32.Parse(price);
                weaponItem.AttackPoint = Int32.Parse(atkPoint);
                weaponItem.Durability = Int32.Parse(durability);
                weaponItem.Explain = weaponExplain;
                weaponItem.WeaponName = weaponName;
                weaponItem.Image = weaponImage;
                wItems.Add(weaponItem);
            }

            foreach (Hashtable itemInfo in itemInfosA)
            {
                String name = (String)itemInfo["name"];
                String price = (String)itemInfo["price"];
                String aromorExplain = (String)itemInfo["armorExplain"];
                String defPoint = (String)itemInfo["armorDef"];
                String armorName = (String)itemInfo["armorName"];
                String armorImage = (String)itemInfo["armorImage"];

                HMArmorItem armorItem = new HMArmorItem();
                armorItem.Name = name;
                armorItem.Price = Int32.Parse(price);
                armorItem.Def = Int32.Parse(defPoint);
                armorItem.ArmorName = armorName;
                armorItem.Explain = aromorExplain;
                armorItem.Image = armorImage;
                aItems.Add(armorItem);
            }

            foreach (Hashtable itemInfo in itemInfosB)
            {
                String name = (String)itemInfo["name"];
                String price = (String)itemInfo["price"];
                String bootsExplain = (String)itemInfo["bootsExplain"];
                String bootsSpd = (String)itemInfo["bootsSpd"];
                String bootsName = (String)itemInfo["bootsName"];
                String bootsImage = (String)itemInfo["bootsImage"];

                HMBootsItem bootsItem = new HMBootsItem();
                bootsItem.Name = name;
                bootsItem.Price = Int32.Parse(price);
                bootsItem.Spd = float.Parse(bootsSpd);
                bootsItem.Explain = bootsExplain;
                bootsItem.BootsName = bootsName;
                bootsItem.Image = bootsImage;
                bItems.Add(bootsItem);
            }
        }
    }



    void Start()
    {
        LoadAssetfromJson();

        weaponPoolSet = new GameObject[itemInfos.Count];
        armorPoolSet = new GameObject[itemInfosA.Count];
        bootPoolSet = new GameObject[itemInfosB.Count];

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
       
        StateManager.Instance.weaponItems = wItems;
        StateManager.Instance.armorItems = aItems;
        StateManager.Instance.bootItems = bItems;

    }

    void Update()
    {

        playerGoldText.GetComponent<Text>().text = ": " + StateManager.Instance.playGold;
        //StateManager.Instance.weaponItemGet = weaponPoolSet;

        playerGoldText.GetComponent<Text>().text = "" + StateManager.Instance.playGold;

    }

    public void equipageWeapon()
    {
        equipageNum = 1;

        for (int i = 0; i < 7; i++)
        {
            weaponPoolSet[i].SetActive(true);
            armorPoolSet[i].SetActive(false);
            bootPoolSet[i].SetActive(false);
        }
        
    }

    public void equipageArmor()
    {
        equipageNum = 2;
        for (int i = 0; i < 7; i++)
        {
            armorPoolSet[i].SetActive(true);
            bootPoolSet[i].SetActive(false);
            weaponPoolSet[i].SetActive(false);
        }
    }
    public void equipageBoot()
    {
        equipageNum = 3;
        for (int i = 0; i < 7; i++)
        {
            armorPoolSet[i].SetActive(false);
            bootPoolSet[i].SetActive(true);
            weaponPoolSet[i].SetActive(false);
        }
    }

    //무기 사기 위한 버튼
    public void onClickWeaponButton(int num)
    {
        if (StateManager.Instance.bagSize == 5)
        {
            return;
        }
        HMWeaponItem item = (HMWeaponItem)wItems[num];
        if (StateManager.Instance.playGold >= item.Price)
        {
            StateManager.Instance.playGold -= item.Price;
            this._itemWeapon(weaponSetObj, num, weaponPoolSet[num].name);
        }
    }

    public void onClickArmorButton(int num)
    {
        if (StateManager.Instance.bagSize == 5)
        {
            return;
        }

        HMArmorItem item = (HMArmorItem)aItems[num];
        if (StateManager.Instance.playGold >= item.Price)
        {
            StateManager.Instance.playGold -= item.Price;
            this._itemArmor(setObj, num, armorPoolSet[num].name);
            armorPoolSet[num].transform.FindChild("soldOut").GetComponentInChildren<Image>().enabled = true;
            armorPoolSet[num].GetComponentInChildren<Button>().enabled = false;
        }
    }

    public void onClickBootsButton(int num)
    {
        if (StateManager.Instance.bagSize == 5)
        {
            return;
        }
        HMBootsItem item = (HMBootsItem)bItems[num];
        if (StateManager.Instance.playGold >= item.Price)
        {
            StateManager.Instance.playGold -= item.Price;
            this._itemBoots(setObj, num, bootPoolSet[num].name);
            bootPoolSet[num].transform.FindChild("soldOut").GetComponentInChildren<Image>().enabled = true;
            bootPoolSet[num].GetComponentInChildren<Button>().enabled = false;
        }
    }

    private void _itemWeapon(GameObject gameObj, int itemIndex, String weaponName)
    {
        if (StateManager.Instance.bagSize == 5)
        {
            return;
        }
        StateManager.Instance.bagSize++;
        //StateManager.Instance.bagNum = wNum;
        HMWeaponItem item = (HMWeaponItem)wItems[itemIndex];
        weaponDurabilityText.GetComponent<Text>().text = "내구도: " + item.Durability.ToString();
        weaponNameText.GetComponent<Text>().text = item.Name + " 공격력: " + item.AttackPoint.ToString();

        weaponImage.GetComponent<Image>().sprite = (Sprite)Resources.Load( item.Image, typeof(Sprite));

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

    public void _setupWeapon(int itemIndex)
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

        weaponBackgroundImage.GetComponent<Image>().sprite = (Sprite)Resources.Load(item.Image, typeof(Sprite));

        weaponPoolSet[itemIndex] = Instantiate(weaponPool) as GameObject;
        weaponPoolSet[itemIndex].transform.SetParent(gridPool.transform);
        weaponPoolSet[itemIndex].transform.localScale = new Vector3(1, 1, 1);

        weaponPoolSet[itemIndex].name = item.WeaponName;

        weaponPoolSet[itemIndex].GetComponent<Button>().onClick.AddListener(delegate { onClickWeaponButton(itemIndex); });
        
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
        string str2 = item.Explain;
        string str3 = "방어력: ";
        string str4 = item.Def.ToString();
        // 문자열 포맷을 지정하여 저장
        str = String.Format(itemDescFormat, str1, str2, str3, str4);

        armorPoolPriceText.GetComponent<Text>().text = strGold;
        armorPoolNameText.GetComponent<Text>().text = str;
        armorBackgroundImage.GetComponent<Image>().sprite = (Sprite)Resources.Load(item.Image, typeof(Sprite));

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

        //StateManager.Instance.bagNum = wNum;

        HMArmorItem item = (HMArmorItem)aItems[itemIndex];

        armorNameText.GetComponent<Text>().text = item.Name + "\n" + " 방어력: " + item.Def.ToString();
        armorImage.GetComponent<Image>().sprite = (Sprite)Resources.Load(item.Image, typeof(Sprite));

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
        string str2 = item.Explain;
        string str3 = "속도: ";
        string str4 = item.Spd.ToString();
        // 문자열 포맷을 지정하여 저장
        str = String.Format(itemDescFormat, str1, str2, str3, str4);

        armorPoolPriceText.GetComponent<Text>().text = strGold;
        armorPoolNameText.GetComponent<Text>().text = str;
        armorBackgroundImage.GetComponent<Image>().sprite = (Sprite)Resources.Load(item.Image, typeof(Sprite));

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
        //StateManager.Instance.bagNum = wNum;

        HMBootsItem item = (HMBootsItem)bItems[itemIndex];

        bootsNameText.GetComponent<Text>().text = item.Name + "\n" + " 속도: " + item.Spd.ToString();

        bootsImage.GetComponent<Image>().sprite = (Sprite)Resources.Load(item.Image, typeof(Sprite));

        gameObj = Instantiate(bootsUse) as GameObject;
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
