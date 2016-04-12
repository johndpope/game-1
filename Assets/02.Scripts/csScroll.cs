using UnityEngine;
using System.Collections;

public class csScroll : MonoBehaviour
{
    private ArrayList pItem;
    private ArrayList sScroll;
    private ArrayList mScroll;
    private ArrayList bScroll;

    void Start()
    {
        pItem = StateManager.Instance.potionItems;
        sScroll = StateManager.Instance.skillScrollItems;
        mScroll = StateManager.Instance.magicScrollItems;
        bScroll = StateManager.Instance.buffScrollItems;
    }
	
	void Update ()
    {
	
	}

    public void SkillScrollBtn()
    {
        for (int i = 0; i < sScroll.Count; i++)
        {
            if (gameObject.name == "Skill" + i)
            {
                SkillItem item = (SkillItem)sScroll[i];
                StateManager.Instance.useItemNum = i;
                //StateManager.Instance.useItemBool = true;
                StateManager.Instance.useItemName = "Skill";
                StateManager.Instance.skillAtk = true;
                Debug.Log(i); Debug.Log(item.Name);
                Debug.Log("스킬 아이템");
            }
        }
    }

    public void MagicScrollBtn()
    {
        for (int i = 0; i < mScroll.Count; i++)
        {
            if (gameObject.name == "Magic" + i)
            {
                MagicItem item = (MagicItem)mScroll[i];
                StateManager.Instance.useItemNum = i;
                StateManager.Instance.useItemBool = true;
                StateManager.Instance.useItemName = "Magic";
                StateManager.Instance.MagicAtk = true;
                Debug.Log("마법 아이템");
            }
        }
        
    }
    public void BuffScrollBtn()
    {
        for (int i = 0; i < bScroll.Count; i++)
        {
            if (gameObject.name == "Buff" + i)
            {
                BuffItem item = (BuffItem)bScroll[i];
                StateManager.Instance.useItemNum = i;
                StateManager.Instance.useItemBool = true;
                StateManager.Instance.useItemName = "Buff";
                StateManager.Instance.buffUse = true;
                Debug.Log("버프 아이템");
            }
        }

    }
    public void PotionItemBtn()
    {
        for (int i = 0; i < pItem.Count; i++)
        {
            if (gameObject.name == "Potion" + i)
            {
                PotionItem item = (PotionItem)pItem[i];
                StateManager.Instance.useItemNum = i;
                StateManager.Instance.useItemBool = true;
                StateManager.Instance.useItemName = "Potion";
                Debug.Log("포션 아이템");
            }
        }
    }
}
