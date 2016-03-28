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
//제으슨 파일 수정수 할것
//class BuffItem : HMItem
//{
//    private int mUpPoint;

//    public int UpPoint
//    {
//        get { return mUpPoint; }
//        set { mUpPoint = value; }
//    }
//}


public class csitemManager : MonoBehaviour
{
    public TextAsset textAsset;
    private ArrayList potionItems;

    void Start()
    {

        potionItems = new ArrayList();
        StateManager.Instance.skillScrollItems = new ArrayList();
        //StateManager.Instance.magicScrollItems = new ArrayList();
        //StateManager.Instance.buffScrollItems = new ArrayList();

        Hashtable itemTable = (Hashtable)HMJson.objectFromJsonString(textAsset.text);

        foreach (String itemName in itemTable.Keys)
        {
            ArrayList itemInfosP = (ArrayList)itemTable["potion"];         //포션 ArrayList
            ArrayList itemInfosS = (ArrayList)itemTable["skillScroll"];   //기술 ArrayList
            //ArrayList itemInfosM = (ArrayList)itemTable["magicScroll"];   //마법 ArrayList
            //ArrayList itemInfosB = (ArrayList)itemTable["buffScroll"];    //보조 ArrayList
            Debug.Log("[Item " + itemName + "]" + "\n");

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

                potionItems.Add(potionItem);
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
                Debug.Log(skillItem.Name);
                Debug.Log(skillItem.SpecialAbility);
            }
        }

    }
	
	void Update ()
    {
	    
	}
}
