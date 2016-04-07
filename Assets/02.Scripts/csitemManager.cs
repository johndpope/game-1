using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;
using itemPool;


class PotionItem : HMItem
{
    private int mUpPoint;

    public int UpPoint
    {
        get { return mUpPoint; }
        set { mUpPoint = value; }
    }
}

class SkillItem : HMItem
{
    private float mAttackUpPoint;
    private float mSpecialAbility;

    public float AttackUpPoint
    {
        get { return mAttackUpPoint; }
        set { mAttackUpPoint = value; }
    }
    public float SpecialAbility
    {
        get { return mSpecialAbility; }
        set { mSpecialAbility = value; }
    }
}

class MagicItem : HMItem
{
    private float mAttactPoint;
    private float mSpdDownPoint;
    private float mAtkDownPoint;

    public float AttactPoint
    {
        get { return mAttactPoint; }
        set { mAttactPoint = value; }
    }

    public float SpdDownPoint
    {
        get { return mSpdDownPoint; }
        set { mSpdDownPoint = value; }
    }

    public float AtkDownPoint
    {
        get { return mAtkDownPoint; }
        set { mAtkDownPoint = value; }
    }
}

class BuffItem : HMItem
{
    private float mHpUp_Mul;
    private int mRestoration;
    private float mAtkUp;
    private float mDefUp;
    private float mSpdUp;
    private float mDefDown;

    public float HpUp_Mul
    {
        get { return mHpUp_Mul; }
        set { mHpUp_Mul = value; }
    }
    public int Restoration
    {
        get { return mRestoration; }
        set { mRestoration = value; }
    }
    public float AtkUp
    {
        get { return mAtkUp; }
        set { mAtkUp = value; }
    }
    public float DefUp
    {
        get { return mDefUp; }
        set { mDefUp = value; }

    }
    public float SpdUp
    {
        get { return mSpdUp; }
        set { mSpdUp = value; }

    }
    public float DefDown
    {
        get { return mDefDown; }
        set { mDefDown = value; }

    }
}

public class csitemManager : MonoBehaviour
{
    public TextAsset textAsset;

    public GameObject itemPool;

    public GameObject itemGrid;

    GameObject[] potionPoolSet;
    GameObject[] SkScrollPoolSet;
    GameObject[] MgscrollPoolSet;
    GameObject[] BufScrollPoolSet;

    GameObject[] potionItemBag;
    GameObject[] SkScrollBag;
    GameObject[] MgScrollBag;
    GameObject[] BufScrollBag;

    int[] potionNum;
    int[] SkscrollNum;
    int[] MgscrollNum;
    int[] BufscrollNum;

    //아이템 상점 셋팅 오브젝트
    public GameObject itemNameText;
    public GameObject itemExplainText;
    public GameObject itemPriceText;
    public GameObject itemImage;
    public GameObject itemCutText;

    //아이템 구입시 인벤토리 셋팅 오브젝트
    public GameObject itemUseGrid;  //아이템 그리드
    public GameObject skillUseGrid; //스킬 그리드

    //아이템 인벤토리 셋팅
    public GameObject itemUseSetObj;
    public GameObject itemUseName;
    public GameObject itemUseExplain;
    public GameObject itemUseImage;


    GameObject itemUse;

    Text itemUseNameText;
    Text itemUseExplainText;

    public GameObject goldTextObj;
    Text goldText;

    int playGold;

    public GameObject popClose;
    public GameObject scrollbar;

    ArrayList itemInfosP;
    ArrayList itemInfosS;
    ArrayList itemInfosM;
    ArrayList itemInfosB;

    void LoadAssetfromJson()
    {

        StateManager.Instance.potionItems = new ArrayList();
        StateManager.Instance.skillScrollItems = new ArrayList();
        StateManager.Instance.magicScrollItems = new ArrayList();
        StateManager.Instance.buffScrollItems = new ArrayList();

        Hashtable itemTable = (Hashtable)HMJson.objectFromJsonString(textAsset.text);

        foreach (String itemName in itemTable.Keys)
        {
            itemInfosP = (ArrayList)itemTable["potion"];         //포션 ArrayList
            itemInfosS = (ArrayList)itemTable["skillScroll"];   //기술 ArrayList
            itemInfosM = (ArrayList)itemTable["magicScroll"];   //마법 ArrayList
            itemInfosB = (ArrayList)itemTable["buffScroll"];    //보조 ArrayList
            //Debug.Log("[Item " + itemName + "]" + "\n");

            foreach (Hashtable itemInfo in itemInfosP)
            {
                String name = (String)itemInfo["name"];
                String price = (String)itemInfo["price"];
                String upPoint = (String)itemInfo["up"];
                String image = (String)itemInfo["potionImage"];
                String explain = (String)itemInfo["Explain"];

                PotionItem potionItem = new PotionItem();
                potionItem.Name = name;
                potionItem.Price = Int32.Parse(price);
                potionItem.UpPoint = Int32.Parse(upPoint);
                potionItem.Image = image;
                potionItem.Explain = explain;

                StateManager.Instance.potionItems.Add(potionItem);
            }

            foreach (Hashtable itemInfo in itemInfosS)
            {
                String name = (String)itemInfo["name"];
                String price = (String)itemInfo["price"];
                String atkPoint = (String)itemInfo["atk"];
                String specialAbility = (String)itemInfo["specialAbility"];
                String image = (String)itemInfo["skillScrollImage"];
                String explain = (String)itemInfo["Explain"];

                SkillItem skillItem = new SkillItem();
                skillItem.Name = name;
                skillItem.Price = Int32.Parse(price);
                skillItem.AttackUpPoint = float.Parse(atkPoint);
                skillItem.SpecialAbility = float.Parse(specialAbility);
                skillItem.Image = image;
                skillItem.Explain = explain;
                StateManager.Instance.skillScrollItems.Add(skillItem);
            }

            foreach (Hashtable itemInfo in itemInfosM)
            {
                String name = (String)itemInfo["name"];
                String price = (String)itemInfo["price"];
                String atkPoint = (String)itemInfo["atk"];
                String spdDown = (String)itemInfo["spdDown"];
                String atkDown = (String)itemInfo["atkDown"];
                String image = (String)itemInfo["magicScrollImage"];
                String explain = (String)itemInfo["Explain"];

                MagicItem magicItem = new MagicItem();
                magicItem.Name = name;
                magicItem.Price = Int32.Parse(price);
                magicItem.AttactPoint = float.Parse(atkPoint);
                magicItem.AtkDownPoint = float.Parse(atkDown);
                magicItem.SpdDownPoint = float.Parse(spdDown);
                magicItem.Image = image;
                magicItem.Explain = explain;
                StateManager.Instance.magicScrollItems.Add(magicItem);
            }

            foreach (Hashtable itemInfo in itemInfosB)
            {

                String name = (String)itemInfo["name"];
                String price = (String)itemInfo["price"];
                String HpUp_Mul = (String)itemInfo["hpUp_Mul"];
                String AtkUp = (String)itemInfo["atkUp"];
                String DefUp = (String)itemInfo["defUp"];
                String SpdUp = (String)itemInfo["spdUp"];
                String DefDown = (String)itemInfo["defDown"];
                String Restoration = (String)itemInfo["restoration"];
                String image = (String)itemInfo["buffScrollImage"];
                String explain = (String)itemInfo["Explain"];

                BuffItem buffItem = new BuffItem();
                buffItem.Name = name;
                buffItem.Price = Int32.Parse(price);
                buffItem.HpUp_Mul = float.Parse(HpUp_Mul);
                buffItem.AtkUp = float.Parse(AtkUp);
                buffItem.DefUp = float.Parse(DefUp);
                buffItem.SpdUp = float.Parse(SpdUp);
                buffItem.DefDown = float.Parse(DefDown);
                buffItem.Restoration = Int32.Parse(Restoration);
                buffItem.Image = image;
                buffItem.Explain = explain;


                StateManager.Instance.buffScrollItems.Add(buffItem);
            }
        }

    }

    void Start()
    {
        LoadAssetfromJson();

        scrollbar.SetActive(false);

        potionNum = new int[itemInfosP.Count];
        SkscrollNum = new int[itemInfosS.Count];
        MgscrollNum = new int[itemInfosM.Count];
        BufscrollNum = new int[itemInfosB.Count];

        potionItemBag = new GameObject[itemInfosP.Count];
        SkScrollBag = new GameObject[itemInfosS.Count];
        MgScrollBag = new GameObject[itemInfosM.Count];
        BufScrollBag = new GameObject[itemInfosB.Count];


        potionPoolSet = new GameObject[itemInfosP.Count];
        SkScrollPoolSet = new GameObject[itemInfosS.Count];
        MgscrollPoolSet = new GameObject[itemInfosM.Count];
        BufScrollPoolSet = new GameObject[itemInfosB.Count];

        for (int i = 0; i < itemInfosP.Count; i++)
        {
            this._setupPontion(i);

        }
        for (int i = 0; i < itemInfosS.Count; i++)
        {
            this._setupSkScroll(i);

        }
        for (int i = 0; i < itemInfosM.Count; i++)
        {
            this._setupMgScroll(i);

        }
        for (int i = 0; i < itemInfosB.Count; i++)
        {
            this._setupBufScroll(i);

        }
        playGold = StateManager.Instance.playGold;

        goldText = goldTextObj.GetComponent<Text>();

        itemUseNameText = itemUseName.GetComponent<Text>();
        itemUseExplainText = itemUseExplain.GetComponent<Text>();

    }

    void Update()
    {
        goldText.text = StateManager.Instance.playGold.ToString();
        StateManager.Instance.potionNum = potionNum;
        StateManager.Instance.SkscrollNum = SkscrollNum;
        StateManager.Instance.MgscrollNum = MgscrollNum;
        StateManager.Instance.BufscrollNum = BufscrollNum;

        StateManager.Instance.potionItemBag = potionItemBag;
        StateManager.Instance.SkScrollBag = SkScrollBag;
        StateManager.Instance.MgScrollBag = MgScrollBag;
        StateManager.Instance.BufScrollBag = BufScrollBag;
    }

    public void offPopClose()
    {
        popClose.SetActive(false);
    }

    public void onPontion()
    {
        for (int i = 0; i < potionPoolSet.Length; i++)
        {
            potionPoolSet[i].SetActive(true);         
        }

        for (int i = 0; i < SkScrollPoolSet.Length; i++)
        {
            SkScrollPoolSet[i].SetActive(false);          
        }

        for (int i = 0; i < MgscrollPoolSet.Length; i++)
        {
            MgscrollPoolSet[i].SetActive(false);        
        }

        for (int i = 0; i < BufScrollPoolSet.Length; i++)
        {
            BufScrollPoolSet[i].SetActive(false);          
        }

        scrollbar.SetActive(true);

    }

    public void onSkill()
    {
        for (int i = 0; i < potionPoolSet.Length; i++)
        {
            potionPoolSet[i].SetActive(false);
        }

        for (int i = 0; i < SkScrollPoolSet.Length; i++)
        {
            SkScrollPoolSet[i].SetActive(true);
        }

        for (int i = 0; i < MgscrollPoolSet.Length; i++)
        {
            MgscrollPoolSet[i].SetActive(false);
        }

        for (int i = 0; i < BufScrollPoolSet.Length; i++)
        {
            BufScrollPoolSet[i].SetActive(false);
        }
        scrollbar.SetActive(true);
    }

    public void onMagic()
    {
        for (int i = 0; i < potionPoolSet.Length; i++)
        {
            potionPoolSet[i].SetActive(false);
        }

        for (int i = 0; i < SkScrollPoolSet.Length; i++)
        {
            SkScrollPoolSet[i].SetActive(false);
        }

        for (int i = 0; i < MgscrollPoolSet.Length; i++)
        {
            MgscrollPoolSet[i].SetActive(true);
        }

        for (int i = 0; i < BufScrollPoolSet.Length; i++)
        {
            BufScrollPoolSet[i].SetActive(false);
        }
        scrollbar.SetActive(true);
    }

    public void onBuff()
    {
        for (int i = 0; i < potionPoolSet.Length; i++)
        {
            potionPoolSet[i].SetActive(false);
        }

        for (int i = 0; i < SkScrollPoolSet.Length; i++)
        {
            SkScrollPoolSet[i].SetActive(false);
        }

        for (int i = 0; i < MgscrollPoolSet.Length; i++)
        {
            MgscrollPoolSet[i].SetActive(false);
        }

        for (int i = 0; i < BufScrollPoolSet.Length; i++)
        {
            BufScrollPoolSet[i].SetActive(true);
        }
        scrollbar.SetActive(true);
    }

    public void onClockPotion(int num)
    {        
        BuyPotionItem(num, itemUse);       
    }

    public void onClockSkScroll(int num)
    {
        BuySkscrollItem(num, itemUse);
    }

    public void onClockMgScroll(int num)
    {
        BuyMgScrollItem(num, itemUse);
    }

    public void onClockBufScroll(int num)
    {
        BuyBufScrollItem(num, itemUse);
    }


    private void _setupPontion(int itemIndex)
    {               
        PotionItem item = (PotionItem)StateManager.Instance.potionItems[itemIndex];
        itemPriceText.GetComponent<Text>().text = item.Price.ToString() + "\n" + "골 드";
        itemNameText.GetComponent<Text>().text = "이 름: " + item.Name;
        itemExplainText.GetComponent<Text>().text = "설 명: " + item.Explain;
        itemImage.GetComponent<Image>().sprite = (Sprite)Resources.Load(item.Image, typeof(Sprite));

        potionPoolSet[itemIndex] = Instantiate(itemPool) as GameObject;
        potionPoolSet[itemIndex].transform.SetParent(itemGrid.transform);
        potionPoolSet[itemIndex].transform.localScale = new Vector3(1, 1, 1);

        potionPoolSet[itemIndex].name = "Potion" + (itemIndex + 1);
        potionPoolSet[itemIndex].GetComponent<Button>().onClick.AddListener(delegate { onClockPotion(itemIndex); });

        potionPoolSet[itemIndex].SetActive(false);
    }

    private void _setupSkScroll(int itemIndex)
    {      
        SkillItem item = (SkillItem)StateManager.Instance.skillScrollItems[itemIndex];
        itemPriceText.GetComponent<Text>().text = item.Price.ToString() + "\n" + "골 드";
        itemNameText.GetComponent<Text>().text = "이 름: " + item.Name;
        itemExplainText.GetComponent<Text>().text = "설 명: " + item.AttackUpPoint * 100 + "% 공 격 력 " + item.Explain;
        itemImage.GetComponent<Image>().sprite = (Sprite)Resources.Load(item.Image, typeof(Sprite));

        SkScrollPoolSet[itemIndex] = Instantiate(itemPool) as GameObject;

        SkScrollPoolSet[itemIndex].transform.SetParent(itemGrid.transform);
        SkScrollPoolSet[itemIndex].transform.localScale = new Vector3(1, 1, 1);

        SkScrollPoolSet[itemIndex].name = "SkillScroll" + (itemIndex + 1);

        SkScrollPoolSet[itemIndex].GetComponent<Button>().onClick.AddListener(delegate { onClockSkScroll(itemIndex); });

        SkScrollPoolSet[itemIndex].SetActive(false);        
    }
    private void _setupMgScroll(int itemIndex)
    {
        MagicItem item = (MagicItem)StateManager.Instance.magicScrollItems[itemIndex];
        itemPriceText.GetComponent<Text>().text = item.Price.ToString() + "\n" + "골 드";
        itemNameText.GetComponent<Text>().text = "이 름: " + item.Name;
        itemExplainText.GetComponent<Text>().text = "설 명: " + item.AttactPoint * 100 + "% 공 격 력 " + item.Explain;
        itemImage.GetComponent<Image>().sprite = (Sprite)Resources.Load(item.Image, typeof(Sprite));

        MgscrollPoolSet[itemIndex] = Instantiate(itemPool) as GameObject;

        MgscrollPoolSet[itemIndex].transform.SetParent(itemGrid.transform);
        MgscrollPoolSet[itemIndex].transform.localScale = new Vector3(1, 1, 1);

        MgscrollPoolSet[itemIndex].name = "MagicScroll" + (itemIndex + 1);
        MgscrollPoolSet[itemIndex].GetComponent<Button>().onClick.AddListener(delegate { onClockMgScroll(itemIndex); });

        MgscrollPoolSet[itemIndex].SetActive(false);
    }
    private void _setupBufScroll(int itemIndex)
    {
        BuffItem item = (BuffItem)StateManager.Instance.buffScrollItems[itemIndex];
        itemPriceText.GetComponent<Text>().text = item.Price.ToString() + "\n" + "골 드";
        itemNameText.GetComponent<Text>().text = "이 름: " + item.Name;
        itemExplainText.GetComponent<Text>().text = "설 명: " + item.Explain;
        itemImage.GetComponent<Image>().sprite = (Sprite)Resources.Load(item.Image, typeof(Sprite));

        BufScrollPoolSet[itemIndex] = Instantiate(itemPool) as GameObject;
        BufScrollPoolSet[itemIndex].transform.SetParent(itemGrid.transform);
        BufScrollPoolSet[itemIndex].transform.localScale = new Vector3(1, 1, 1);

        BufScrollPoolSet[itemIndex].name = "BuffScroll" + (itemIndex + 1);
        BufScrollPoolSet[itemIndex].GetComponent<Button>().onClick.AddListener(delegate { onClockBufScroll(itemIndex); });

        BufScrollPoolSet[itemIndex].SetActive(false);
    }



    private void BuyPotionItem(int itemIndex, GameObject itemUseSet)
    {
        //포션
        PotionItem item = (PotionItem)StateManager.Instance.potionItems[itemIndex];

        if (StateManager.Instance.playGold >= item.Price)
        {
            if (potionNum[itemIndex] == 0)
            {
                itemUseNameText.text = "이 름: " + item.Name;
                itemUseExplainText.text = "설 명: " + item.Explain;
                itemUseImage.GetComponent<Image>().sprite = (Sprite)Resources.Load(item.Image, typeof(Sprite));

                itemUseSet = Instantiate(itemUseSetObj) as GameObject;
                itemUseSet.transform.SetParent(itemUseGrid.transform);
                itemUseSet.transform.localScale = new Vector3(1, 1, 1);
                itemUseSet.name = "Potion" + itemIndex;

                potionItemBag[itemIndex] = itemUseSet;
            }
            StateManager.Instance.playGold -= item.Price;
            potionNum[itemIndex]++;
            potionPoolSet[itemIndex].transform.FindChild("Scrollcnt").GetComponent<Text>().text = "보 유 갯 수:" + potionNum[itemIndex] + " 개";
            potionItemBag[itemIndex].transform.FindChild("ScrollUseCut").GetComponent<Text>().text = "보 유" + "\n" + potionNum[itemIndex] + " 개";
        }
        else
        {
            popClose.SetActive(true);
        }
    }

    private void BuySkscrollItem(int itemIndex, GameObject itemUseSet)
    {
        //스킬
        SkillItem item = (SkillItem)StateManager.Instance.skillScrollItems[itemIndex];
        if (StateManager.Instance.playGold >= item.Price)
        {
            if (SkscrollNum[itemIndex] == 0)
            {
                itemUseNameText.text = "이 름: " + item.Name;
                itemUseExplainText.text = "설 명: " + item.Explain;
                itemUseImage.GetComponent<Image>().sprite = (Sprite)Resources.Load(item.Image, typeof(Sprite));

                itemUseSet = Instantiate(itemUseSetObj) as GameObject;
                itemUseSet.transform.SetParent(skillUseGrid.transform);
                itemUseSet.transform.localScale = new Vector3(1, 1, 1);
                itemUseSet.name = "Skill" + itemIndex;
                SkScrollBag[itemIndex] = itemUseSet;
            }
            StateManager.Instance.playGold -= item.Price;
            SkscrollNum[itemIndex]++;
            SkScrollPoolSet[itemIndex].transform.FindChild("Scrollcnt").GetComponent<Text>().text = "보 유 갯 수:" + SkscrollNum[itemIndex] + " 개";
            SkScrollBag[itemIndex].transform.FindChild("ScrollUseCut").GetComponent<Text>().text = "보 유" + "\n" + SkscrollNum[itemIndex] + " 개";
        }
        else
        {
            popClose.SetActive(true);
        }


    }
    private void BuyMgScrollItem(int itemIndex, GameObject itemUseSet)
    {
        //마법
        MagicItem item = (MagicItem)StateManager.Instance.magicScrollItems[itemIndex];
        if (StateManager.Instance.playGold >= item.Price)
        {
            if (MgscrollNum[itemIndex] == 0)
            {
                itemUseNameText.text = "이 름: " + item.Name;
                itemUseExplainText.text = "설 명: " + item.Explain;
                itemUseImage.GetComponent<Image>().sprite = (Sprite)Resources.Load(item.Image, typeof(Sprite));

                itemUseSet = Instantiate(itemUseSetObj) as GameObject;
                itemUseSet.transform.SetParent(skillUseGrid.transform);
                itemUseSet.transform.localScale = new Vector3(1, 1, 1);
                itemUseSet.name = "Magic" + itemIndex;
                MgScrollBag[itemIndex] = itemUseSet;
            }
            StateManager.Instance.playGold -= item.Price;
            MgscrollNum[itemIndex]++;
            MgscrollPoolSet[itemIndex].transform.FindChild("Scrollcnt").GetComponent<Text>().text = "보 유 갯 수:" + MgscrollNum[itemIndex] + " 개";
            MgScrollBag[itemIndex].transform.FindChild("ScrollUseCut").GetComponent<Text>().text = "보 유" + "\n" + MgscrollNum[itemIndex] + " 개";
        }
        else
        {
            popClose.SetActive(true);
        }


    }
    private void BuyBufScrollItem(int itemIndex, GameObject itemUseSet)
    {       
        //보조 마법
        BuffItem item = (BuffItem)StateManager.Instance.buffScrollItems[itemIndex];
        if (StateManager.Instance.playGold >= item.Price)
        {
            if (BufscrollNum[itemIndex] == 0)
            {
                itemUseNameText.text = "이 름: " + item.Name;
                itemUseExplainText.text = "설 명: " + item.Explain;
                itemUseImage.GetComponent<Image>().sprite = (Sprite)Resources.Load(item.Image, typeof(Sprite));

                itemUseSet = Instantiate(itemUseSetObj) as GameObject;
                itemUseSet.transform.SetParent(skillUseGrid.transform);
                itemUseSet.transform.localScale = new Vector3(1, 1, 1);
                itemUseSet.name = "Buff" + itemIndex;
                BufScrollBag[itemIndex] = itemUseSet;
            }
            StateManager.Instance.playGold -= item.Price;
            BufscrollNum[itemIndex]++;
            BufScrollPoolSet[itemIndex].transform.FindChild("Scrollcnt").GetComponent<Text>().text = "보 유 갯 수:" + BufscrollNum[itemIndex] + " 개";
            BufScrollBag[itemIndex].transform.FindChild("ScrollUseCut").GetComponent<Text>().text = "보 유" + "\n" + BufscrollNum[itemIndex] + " 개";
        }
        else
        {
            popClose.SetActive(true);
        }
    }
}

