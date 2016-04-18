using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class csSaveLord : MonoBehaviour
{  
    //아이템 구입시 인벤토리 셋팅 오브젝트
    public GameObject itemUseGrid;  //아이템 그리드
    public GameObject skillUseGrid; //스킬 그리드
    public GameObject magicUseGrid; //마법 그리드
    public GameObject buffUseGrid;  //버프 그리드
    //아이템 인벤토리 셋팅
    public GameObject itemUseSetObj;
    public GameObject itemUseName;
    public GameObject itemUseExplain;
    public GameObject itemUseImage;
   

    GameObject itemUse;

    Text itemUseNameText;
    Text itemUseExplainText;

    public GameObject grid;

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


    public void lord()
    {
        itemUseNameText = itemUseName.GetComponent<Text>();
        itemUseExplainText = itemUseExplain.GetComponent<Text>();

        for (int i = 0; i < StateManager.Instance.potionNum.Length; i++)
        {            
            StateManager.Instance.potionNum[i] = PlayerPrefs.GetInt("potion" + (i+1).ToString(), 0);
            PotionItem item = (PotionItem)StateManager.Instance.potionItems[i];
            if (StateManager.Instance.potionNum[i] > 0)
            {
                itemUseNameText.text = "이 름: " + item.Name;
                itemUseExplainText.text = "설 명: " + item.Explain;
                itemUseImage.GetComponent<Image>().sprite = (Sprite)Resources.Load(item.Image, typeof(Sprite));

                GameObject itemUseSet = Instantiate(itemUseSetObj) as GameObject;
                itemUseSet.transform.SetParent(itemUseGrid.transform);
                itemUseSet.transform.localScale = new Vector3(1, 1, 1);
                itemUseSet.name = "Potion" + i;

                StateManager.Instance.potionItemBag[i] = itemUseSet;
                StateManager.Instance.potionItemBag[i].transform.FindChild("ScrollUseCut").GetComponent<Text>().text = "보 유" + "\n" + StateManager.Instance.potionNum[i] + " 개";
            }
        }

        for (int i = 0; i < StateManager.Instance.SkscrollNum.Length; i++)
        {
            StateManager.Instance.SkscrollNum[i] = PlayerPrefs.GetInt("skill" + (i + 1).ToString(), 0);
            SkillItem item = (SkillItem)StateManager.Instance.skillScrollItems[i];
            if (StateManager.Instance.SkscrollNum[i] > 0)
            {
                itemUseNameText.text = "이 름: " + item.Name;
                itemUseExplainText.text = "설 명: " + item.Explain;
                itemUseImage.GetComponent<Image>().sprite = (Sprite)Resources.Load(item.Image, typeof(Sprite));

                GameObject itemUseSet = Instantiate(itemUseSetObj) as GameObject;
                itemUseSet.transform.SetParent(skillUseGrid.transform);
                itemUseSet.transform.localScale = new Vector3(1, 1, 1);
                itemUseSet.name = "Skill" + i;

                StateManager.Instance.SkScrollBag[i] = itemUseSet;
                StateManager.Instance.SkScrollBag[i].transform.FindChild("ScrollUseCut").GetComponent<Text>().text = "보 유" + "\n" + StateManager.Instance.SkscrollNum[i] + " 개";
            }
        }

        for (int i = 0; i < StateManager.Instance.MgscrollNum.Length; i++)
        {
            StateManager.Instance.MgscrollNum[i] = PlayerPrefs.GetInt("magic" + (i + 1).ToString(), 0);
            MagicItem item = (MagicItem)StateManager.Instance.magicScrollItems[i];
            if (StateManager.Instance.MgscrollNum[i] > 0)
            {
                itemUseNameText.text = "이 름: " + item.Name;
                itemUseExplainText.text = "설 명: " + item.Explain;
                itemUseImage.GetComponent<Image>().sprite = (Sprite)Resources.Load(item.Image, typeof(Sprite));

                GameObject itemUseSet = Instantiate(itemUseSetObj) as GameObject;
                itemUseSet.transform.SetParent(magicUseGrid.transform);
                itemUseSet.transform.localScale = new Vector3(1, 1, 1);
                itemUseSet.name = "Magic" + i;

                StateManager.Instance.MgScrollBag[i] = itemUseSet;
                StateManager.Instance.MgScrollBag[i].transform.FindChild("ScrollUseCut").GetComponent<Text>().text = "보 유" + "\n" + StateManager.Instance.MgscrollNum[i] + " 개";
            }
        }

        for (int i = 0; i < StateManager.Instance.BufscrollNum.Length; i++)
        {
            StateManager.Instance.BufscrollNum[i] = PlayerPrefs.GetInt("buff" + (i + 1).ToString(), 0);
            BuffItem item = (BuffItem)StateManager.Instance.buffScrollItems[i];
            if (StateManager.Instance.BufscrollNum[i] > 0)
            {
                itemUseNameText.text = "이 름: " + item.Name;
                itemUseExplainText.text = "설 명: " + item.Explain;
                itemUseImage.GetComponent<Image>().sprite = (Sprite)Resources.Load(item.Image, typeof(Sprite));

                GameObject itemUseSet = Instantiate(itemUseSetObj) as GameObject;
                itemUseSet.transform.SetParent(magicUseGrid.transform);
                itemUseSet.transform.localScale = new Vector3(1, 1, 1);
                itemUseSet.name = "Buff" + i;

                StateManager.Instance.BufScrollBag[i] = itemUseSet;
                StateManager.Instance.BufScrollBag[i].transform.FindChild("ScrollUseCut").GetComponent<Text>().text = "보 유" + "\n" + StateManager.Instance.BufscrollNum[i] + " 개";
            }
        }

        int playUseAtk = PlayerPrefs.GetInt("playUseAtk");
        int  playUseDef = PlayerPrefs.GetInt("playUseDef");
        float playUseSpd = PlayerPrefs.GetFloat("playUseSpd");

        if (playUseAtk !=0)
        {
            if(playUseAtk.Equals(5))
            {
                Weapon(0);
            }
            if (playUseAtk.Equals(8))
            {
                Weapon(0);
            }
            if (playUseAtk.Equals(5))
            {
                Weapon(0);
            }
            if (playUseAtk.Equals(5))
            {
                Weapon(0);
            }
        }
    }

    private void Weapon(int num)
    {
        if (StateManager.Instance.bagSize == 5)
        {
            return;
        }
        StateManager.Instance.bagSize++;

        HMWeaponItem item = (HMWeaponItem)StateManager.Instance.weaponItems[num];

        weaponDurabilityText.GetComponent<Text>().text = "내구도: " + item.Durability.ToString();
        weaponNameText.GetComponent<Text>().text = item.Name + " 공격력: " + item.AttackPoint.ToString();

        weaponImage.GetComponent<Image>().sprite = (Sprite)Resources.Load(item.Image, typeof(Sprite));

        GameObject gameObj = Instantiate(WeaponUse) as GameObject;
        gameObj.transform.SetParent(grid.transform);
        gameObj.transform.localScale = new Vector3(1, 1, 1);


        for (int wNum = 0; wNum < 5; wNum++)
        {
            if (StateManager.Instance.weaponSpace[wNum] == null)
            {
                gameObj.name = item.Name + wNum;
                StateManager.Instance.weaponDurability[wNum] = item.Durability;
                StateManager.Instance.weaponSpace[wNum] = gameObj;
                return;
            }
        }
    }


    public void SaveData()
    {
        PlayerPrefs.SetInt("potion1", StateManager.Instance.potionNum[0]);
        PlayerPrefs.SetInt("potion2", StateManager.Instance.potionNum[1]);
        PlayerPrefs.SetInt("potion3", StateManager.Instance.potionNum[2]);
        PlayerPrefs.SetInt("potion4", StateManager.Instance.potionNum[3]);
        PlayerPrefs.SetInt("potion5", StateManager.Instance.potionNum[4]);

        PlayerPrefs.SetInt("skill1", StateManager.Instance.SkscrollNum[0]);
        PlayerPrefs.SetInt("skill2", StateManager.Instance.SkscrollNum[1]);
        PlayerPrefs.SetInt("skill3", StateManager.Instance.SkscrollNum[2]);
        PlayerPrefs.SetInt("skill4", StateManager.Instance.SkscrollNum[3]);

        PlayerPrefs.SetInt("magic1", StateManager.Instance.MgscrollNum[0]);
        PlayerPrefs.SetInt("magic2", StateManager.Instance.MgscrollNum[1]);
        PlayerPrefs.SetInt("magic3", StateManager.Instance.MgscrollNum[2]);
        PlayerPrefs.SetInt("magic4", StateManager.Instance.MgscrollNum[3]);

        PlayerPrefs.SetInt("buff1", StateManager.Instance.BufscrollNum[0]);
        PlayerPrefs.SetInt("buff2", StateManager.Instance.BufscrollNum[1]);
        PlayerPrefs.SetInt("buff3", StateManager.Instance.BufscrollNum[2]);
        PlayerPrefs.SetInt("buff4", StateManager.Instance.BufscrollNum[3]);
        PlayerPrefs.SetInt("buff5", StateManager.Instance.BufscrollNum[4]);
        PlayerPrefs.SetInt("buff6", StateManager.Instance.BufscrollNum[5]);

        PlayerPrefs.SetInt("playGold", StateManager.Instance.playGold);
        PlayerPrefs.SetFloat("playHp", StateManager.Instance.playHp);
        PlayerPrefs.SetFloat("playHpMax", StateManager.Instance.playHpMax);
        PlayerPrefs.SetFloat("playAtk", StateManager.Instance.playAtk);
        PlayerPrefs.SetFloat("playDef", StateManager.Instance.playDef);
        PlayerPrefs.SetFloat("playSpd", StateManager.Instance.playSpd);

        PlayerPrefs.SetInt("bagSize", StateManager.Instance.bagSize);

        PlayerPrefs.SetInt("firstGame", StateManager.Instance.firstGameNum);

        PlayerPrefs.SetInt("cntHp", StateManager.Instance.cntHp);
        PlayerPrefs.SetInt("cntAtk", StateManager.Instance.cntAtk);
        PlayerPrefs.SetInt("cntDef", StateManager.Instance.cntDef);
        PlayerPrefs.SetInt("cntSpd", StateManager.Instance.cntSpd);

        PlayerPrefs.SetInt("playUseAtk", StateManager.Instance.playUseAtk);
        PlayerPrefs.SetInt("playUseDef", StateManager.Instance.playUseDef);
        PlayerPrefs.SetFloat("playUseSpd", StateManager.Instance.playUseSpd);

}
}
