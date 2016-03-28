﻿using UnityEngine;
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
    private bool mRestoration;
    private float mAtkUp;
    private float mDefUp;
    private float mSpdUp;
    private float mDefDown;

    public float HpUp_Mul
    {
        get { return mHpUp_Mul; }
        set { mHpUp_Mul = value; }
    }
    public bool Restoration
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

    GameObject[] itemPoolSet = new GameObject[15];

    public GameObject itemNameText;
    public GameObject itemExplainText;
    public GameObject itemPriceText;
    public GameObject itemImageText;
    public GameObject itemCutText;

    void LoadAssetfromJson()
    {

        StateManager.Instance.potionItems = new ArrayList();
        StateManager.Instance.skillScrollItems = new ArrayList();
        StateManager.Instance.magicScrollItems = new ArrayList();
        StateManager.Instance.buffScrollItems = new ArrayList();

        Hashtable itemTable = (Hashtable)HMJson.objectFromJsonString(textAsset.text);

        foreach (String itemName in itemTable.Keys)
        {
            ArrayList itemInfosP = (ArrayList)itemTable["potion"];         //포션 ArrayList
            ArrayList itemInfosS = (ArrayList)itemTable["skillScroll"];   //기술 ArrayList
            ArrayList itemInfosM = (ArrayList)itemTable["magicScroll"];   //마법 ArrayList
            ArrayList itemInfosB = (ArrayList)itemTable["buffScroll"];    //보조 ArrayList
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
                buffItem.Restoration = bool.Parse(Restoration);
                buffItem.Image = image;
                buffItem.Explain = explain;
                StateManager.Instance.buffScrollItems.Add(buffItem);
            }
        }

    }

    void Start()
    {
        LoadAssetfromJson();

        for (int i = 0; i < 19; i++)
        {
                this._setupPontion(i);
        }
    }

    void Update ()
    {
	    
	}

    public void onPontion()
    {
        for (int i = 0; i < 5; i++)
        {
            itemPoolSet[i].SetActive(true);
        }
        for (int i = 5; i < 9; i++)
        {
            itemPoolSet[i].SetActive(false);
        }

        for (int i = 9; i < 13; i++)
        {
            itemPoolSet[i].SetActive(false);
        }
        for (int i = 13; i < 19; i++)
        {
            itemPoolSet[i].SetActive(false);
        }
    }

    public void onSkill()
    {
        for (int i = 0; i < 5; i++)
        {
            itemPoolSet[i].SetActive(false);
        }
        for (int i = 5; i < 9; i++)
        {
            itemPoolSet[i].SetActive(true);
        }
        for (int i = 9; i < 13; i++)
        {
            itemPoolSet[i].SetActive(false);
        }
        for (int i = 13; i < 19; i++)
        {
            itemPoolSet[i].SetActive(false);
        }

    }

    public void onMagic()
    {
        for (int i = 0; i < 5; i++)
        {
            itemPoolSet[i].SetActive(false);
        }
        for (int i = 5; i < 9; i++)
        {
            itemPoolSet[i].SetActive(false);
        }

        for (int i = 9; i < 13; i++)
        {
            itemPoolSet[i].SetActive(true);
        }
        for (int i = 13; i < 19; i++)
        {
            itemPoolSet[i].SetActive(false);
        }
    }

    public void onBuff()
    {
        for (int i = 0; i < 5; i++)
        {
            itemPoolSet[i].SetActive(false);
        }
        for (int i = 5; i < 9; i++)
        {
            itemPoolSet[i].SetActive(false);
        }

        for (int i = 9; i < 13; i++)
        {
            itemPoolSet[i].SetActive(false);
        }
        for (int i = 13; i < 19; i++)
        {
            itemPoolSet[i].SetActive(true);
        }

    }

    public void onClockPotion(int num)
    {
        Debug.Log(num);
    }

    private void _setupPontion(int itemIndex)
    {
        if (itemIndex >= 0 && itemIndex <= 4)
        {
            PotionItem item = (PotionItem)StateManager.Instance.potionItems[itemIndex];
            itemPriceText.GetComponent<Text>().text = item.Price.ToString() + "\n" + "골드";
            itemNameText.GetComponent<Text>().text = "이 름: " + item.Name;
            itemExplainText.GetComponent<Text>().text = "설명: " + item.Explain;
            itemPoolSet[itemIndex] = Instantiate(itemPool) as GameObject;

            itemPoolSet[itemIndex].transform.SetParent(itemGrid.transform);
            itemPoolSet[itemIndex].transform.localScale = new Vector3(1, 1, 1);

            itemPoolSet[itemIndex].name = "Potion" + (itemIndex + 1);
            itemPoolSet[itemIndex].GetComponent<Button>().onClick.AddListener(delegate { onClockPotion(itemIndex); });
        }

        else if (itemIndex >= 5 && itemIndex <= 8)
        {
            SkillItem item = (SkillItem)StateManager.Instance.skillScrollItems[itemIndex];
            itemPriceText.GetComponent<Text>().text = item.Price.ToString() + "\n" + "골드";
            itemNameText.GetComponent<Text>().text = "이 름: " + item.Name;
            itemExplainText.GetComponent<Text>().text = "설명: " + item.AttackUpPoint * 100 + "% 공 격 력 " + item.Explain;
            itemPoolSet[itemIndex] = Instantiate(itemPool) as GameObject;

            itemPoolSet[itemIndex].transform.SetParent(itemGrid.transform);
            itemPoolSet[itemIndex].transform.localScale = new Vector3(1, 1, 1);

            itemPoolSet[itemIndex].name = "SkillScroll" + (itemIndex + 1);
            itemPoolSet[itemIndex].GetComponent<Button>().onClick.AddListener(delegate { onClockPotion(itemIndex); });
        }

        else if (itemIndex >= 9 && itemIndex <= 12)
        {
            MagicItem item = (MagicItem)StateManager.Instance.magicScrollItems[itemIndex];
            itemPriceText.GetComponent<Text>().text = item.Price.ToString() + "\n" + "골드";
            itemNameText.GetComponent<Text>().text = "이 름: " + item.Name;
            itemExplainText.GetComponent<Text>().text = "설명: " + item.AttactPoint * 100 + "% 공 격 력 " + item.Explain;
            itemPoolSet[itemIndex] = Instantiate(itemPool) as GameObject;

            itemPoolSet[itemIndex].transform.SetParent(itemGrid.transform);
            itemPoolSet[itemIndex].transform.localScale = new Vector3(1, 1, 1);

            itemPoolSet[itemIndex].name = "MagicScroll" + (itemIndex + 1);
            itemPoolSet[itemIndex].GetComponent<Button>().onClick.AddListener(delegate { onClockPotion(itemIndex); });
        }
        else if (itemIndex >= 13 && itemIndex <= 18)
        {
            BuffItem item = (BuffItem)StateManager.Instance.buffScrollItems[itemIndex];
            itemPriceText.GetComponent<Text>().text = item.Price.ToString() + "\n" + "골드";
            itemNameText.GetComponent<Text>().text = "이 름: " + item.Name;
            itemExplainText.GetComponent<Text>().text = "설명: " + item.Explain;
            itemPoolSet[itemIndex] = Instantiate(itemPool) as GameObject;

            itemPoolSet[itemIndex].transform.SetParent(itemGrid.transform);
            itemPoolSet[itemIndex].transform.localScale = new Vector3(1, 1, 1);

            itemPoolSet[itemIndex].name = "BuffScroll" + (itemIndex + 1);
            itemPoolSet[itemIndex].GetComponent<Button>().onClick.AddListener(delegate { onClockPotion(itemIndex); });
        }
        itemPoolSet[itemIndex].SetActive(false);
    }
}
