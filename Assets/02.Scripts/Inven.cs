using UnityEngine;
using System.Collections;
using UnityEngine.UI;//접근 권한 얻기
using System;

public class Inven : MonoBehaviour
{

    public GameObject _EqInven;
    public GameObject _buttons;
    public GameObject _close;
    public GameObject _EmptyScroll; //빈 양피지
    public GameObject _WeaponScroll;
    public GameObject _ItemScroll;
    public GameObject _QuestScroll;
   
    //기술 스크롤들
    public GameObject _SkillScroll;
    public GameObject _MagicScroll;
    public GameObject _BuffScroll;
    public bool timestop;

    public GameObject SkButton;
    public GameObject MgButton;
    public GameObject BuButton;

    public GameObject ynPop;
    public GameObject rPop;
    public GameObject changePop;

    public GameObject durabilityText;
    Text dText;

    public GameObject useText;
    Text uText;

    private int wNum;
    private int aNum;
    private int bNum;

    bool armorItemUse;
    bool bootsItemUse;

    int d;

    int bUse;
    int aUse;

    int changeNum;

    public GameObject playerHpText;
    public GameObject playerAtkText;
    public GameObject playerDefText;
    public GameObject playerSpdText;
    public GameObject playerGoldText;

    public GameObject useWeapon;
    public GameObject useArmor;
    public GameObject useBoots;

    void Start()
    {

        armorItemUse = false;
        bootsItemUse = false;
        timestop = false;
        dText = durabilityText.GetComponent<Text>();
        uText = useText.GetComponent<Text>();
        StartCoroutine(playerData());
    }

    // Update is called once per frame
    void Update()
    {
        d = Int32.Parse(dText.text);

        if (csUseEquip.itemUsePopBool == true)
        {
            ynPop.SetActive(true);
            csUseEquip.itemUsePopBool = false;
        }


        //if (StateManager.Instance.weaponDurability[StateManager.Instance.wUse] == 0)
        //{
        //    dText.text = "0";
        //    StateManager.Instance.bagSize--;
        //    DestroyObject(StateManager.Instance.weaponSpace[StateManager.Instance.wUse]);
        //    useWeapon.GetComponent<Image>().sprite = (Sprite)Resources.Load("Base1", typeof(Sprite));
        //}
    }

    IEnumerator playerData()
    {
        playerGoldText.GetComponent<Text>().text = "" + StateManager.Instance.playGold;

        playerHpText.GetComponent<Text>().text = "" + StateManager.Instance.playHp;
        playerAtkText.GetComponent<Text>().text = "" + StateManager.Instance.playAtk + " + " + StateManager.Instance.playUseAtk;
        playerDefText.GetComponent<Text>().text = "" + StateManager.Instance.playDef + " + " + StateManager.Instance.playUseDef;
        playerSpdText.GetComponent<Text>().text = "" + StateManager.Instance.playSpd + " + " + StateManager.Instance.playUseSpd;
        yield return null;
        StartCoroutine(playerData());
    }
    public void onInven()//인벤토리 열기
    {
        _EqInven.SetActive(true);
        timestop = true;
        _EmptyScroll.SetActive(true);
        _buttons.SetActive(true);
        _close.SetActive(true);
        if (timestop == true)
        {
            Time.timeScale = 0;
            //GameObject.Find("InvenIcon").GetComponent<Button>().enabled = false;
        }
    }
    public void offInven()//인벤토리 닫기
    {
        _EqInven.SetActive(false);
        _buttons.SetActive(false);
        _close.SetActive(false);
        _WeaponScroll.SetActive(false);
        _SkillScroll.SetActive(false);
        _MagicScroll.SetActive(false);
        _BuffScroll.SetActive(false);
        _ItemScroll.SetActive(false);
        _QuestScroll.SetActive(false);
        _EmptyScroll.SetActive(false);
        timestop = false;
        if (timestop == false)
        {
            Time.timeScale = 1;
            // GameObject.Find("InvenIcon").GetComponent<Button>().enabled = true;
        }
    }


    public void playerState()
    {

    }
    //아이템버튼
    public void onItem()
    {
        _EmptyScroll.SetActive(false);
        _SkillScroll.SetActive(false);
        _MagicScroll.SetActive(false);
        _BuffScroll.SetActive(false);
        _ItemScroll.SetActive(true);
        _QuestScroll.SetActive(false);
        _WeaponScroll.SetActive(false);
        SkButton.SetActive(false);
        MgButton.SetActive(false);
        BuButton.SetActive(false);
    }
    //기술들버튼
    public void onScroll()
    {
        _EmptyScroll.SetActive(true);
        SkButton.SetActive(true);
        MgButton.SetActive(true);
        BuButton.SetActive(true);
        _SkillScroll.SetActive(false);
        _MagicScroll.SetActive(false);
        _BuffScroll.SetActive(false);
        _ItemScroll.SetActive(false);
        _QuestScroll.SetActive(false);
        _WeaponScroll.SetActive(false);
    }
    //기술버튼
    public void onSkill()
    {
        _EmptyScroll.SetActive(false);
        _SkillScroll.SetActive(true);
        _MagicScroll.SetActive(false);
        _BuffScroll.SetActive(false);
        _ItemScroll.SetActive(false);
        _QuestScroll.SetActive(false);
        _WeaponScroll.SetActive(false);
    }
    ////마법버튼
    public void onMagic()
    {
        _SkillScroll.SetActive(false);
        _MagicScroll.SetActive(true);
        _BuffScroll.SetActive(false);
        _ItemScroll.SetActive(false);
        _QuestScroll.SetActive(false);
        _WeaponScroll.SetActive(false);
    }
    ////버프버튼
    public void onBuff()
    {

        _SkillScroll.SetActive(false);
        _MagicScroll.SetActive(false);
        _BuffScroll.SetActive(true);
        _ItemScroll.SetActive(false);
        _QuestScroll.SetActive(false);
        _WeaponScroll.SetActive(false);
    }
    //퀘스트버튼
    public void onQuest()
    {
        _EmptyScroll.SetActive(false);
        _SkillScroll.SetActive(false);
        _MagicScroll.SetActive(false);
        _BuffScroll.SetActive(false);
        _ItemScroll.SetActive(false);
        _QuestScroll.SetActive(true);
        _WeaponScroll.SetActive(false);
        SkButton.SetActive(false);
        MgButton.SetActive(false);
        BuButton.SetActive(false);
    }
    //장비버튼
    public void onWeapon()
    {
        _EmptyScroll.SetActive(false);
        _SkillScroll.SetActive(false);
        _MagicScroll.SetActive(false);
        _BuffScroll.SetActive(false);
        _ItemScroll.SetActive(false);
        _QuestScroll.SetActive(false);
        _WeaponScroll.SetActive(true);
        SkButton.SetActive(false);
        MgButton.SetActive(false);
        BuButton.SetActive(false);
    }

    public void useItem()
    {
        switch(csUseEquip.equipNum)
        {
            case 1:
                weaponSet();
                csUseEquip.equipNum = 0;
                break;
            case 2:
                armorSet();
                if(armorItemUse==true)
                {
                    changeNum = 1;
                }
                csUseEquip.equipNum = 0;
                break;
            case 3:
                bootsSet();
                if (bootsItemUse == true)
                {
                    changeNum = 2;
                }
                csUseEquip.equipNum = 0;
                break;
        }
    }

    public void noUseItem(int i)
    {
        if(i == 0)
        {
            ynPop.SetActive(false);
        }
        else if (i == 1)
        {
            rPop.SetActive(false);
        }
    }

    public void mUseItem()
    {
        weaponSet2();
        ynPop.SetActive(false);
    }

    public void changeItem()
    {
        switch(changeNum)
        {
            case 1:
                StateManager.Instance.bagSize--;
                DestroyObject(StateManager.Instance.weaponSpace[aUse]);
                armorItemUse = false;
                armorSet();
                changePop.SetActive(false);
                break;
            case 2:
                StateManager.Instance.bagSize--;
                DestroyObject(StateManager.Instance.weaponSpace[bUse]);
                bootsItemUse = false;
                bootsSet();
                changePop.SetActive(false);
                break;
        }
       
    }

    public void noChangeItem()
    {
        changePop.SetActive(false);
    }


    private void weaponSet()
    {
        wNum = StateManager.Instance.bagNum;

        HMWeaponItem item = (HMWeaponItem)StateManager.Instance.weaponItems[wNum];

        if ( d == 0)
        {
            ynPop.SetActive(false);
            dText.text = StateManager.Instance.weaponDurability[wNum].ToString();
            StateManager.Instance.weaponSpace[wNum].GetComponent<Button>().enabled = false;
            StateManager.Instance.weaponSpace[wNum].transform.FindChild("weaponUseIcon").GetComponentInChildren<Image>().enabled = true;
            StateManager.Instance.playUseAtk = csUseEquip.attackPoint;
            StateManager.Instance.wUse = wNum;
            useWeapon.GetComponent<Image>().sprite = StateManager.Instance.weaponSpace[wNum].transform.FindChild("weaponImage").GetComponent<Image>().sprite;
        }

         else if(d > 0)
        {
            ynPop.SetActive(false);
            rPop.SetActive(true);
            uText.text = "내 구 가 " + d + "\n" + "사 용 하 시 겠 습 니 까?";
        }
    }

    private void weaponSet2()
    {
        wNum = StateManager.Instance.bagNum;
        rPop.SetActive(false);
        dText.text = StateManager.Instance.weaponDurability[wNum].ToString();
        DestroyObject(StateManager.Instance.weaponSpace[StateManager.Instance.wUse]);
        StateManager.Instance.bagSize--;
        StateManager.Instance.weaponSpace[wNum].GetComponent<Button>().enabled = false;
        StateManager.Instance.weaponSpace[wNum].transform.FindChild("weaponUseIcon").GetComponentInChildren<Image>().enabled = true;
        useWeapon.GetComponent<Image>().sprite = StateManager.Instance.weaponSpace[wNum].transform.FindChild("weaponImage").GetComponent<Image>().sprite;
        StateManager.Instance.playUseAtk = csUseEquip.attackPoint;
        StateManager.Instance.wUse = wNum;
    }

    private void armorSet()
    {
        if (armorItemUse == false)
        {
            aNum = StateManager.Instance.bagNum;
            ynPop.SetActive(false);
            StateManager.Instance.weaponSpace[aNum].GetComponent<Button>().enabled = false;
            StateManager.Instance.weaponSpace[aNum].transform.FindChild("armorUseIcon").GetComponentInChildren<Image>().enabled = true;
            StateManager.Instance.playUseDef = csUseEquip.defPoint;

            useArmor.GetComponent<Image>().sprite = StateManager.Instance.weaponSpace[aNum].transform.FindChild("armorImage").GetComponent<Image>().sprite;

            armorItemUse = true;
            aUse = aNum;
        }
        else if(armorItemUse == true)
        {
            ynPop.SetActive(false);
            changePop.SetActive(true);
        }
    }

    private void bootsSet()
    {
        if (bootsItemUse == false)
        {
            bNum = StateManager.Instance.bagNum;
            ynPop.SetActive(false);
            StateManager.Instance.weaponSpace[bNum].GetComponent<Button>().enabled = false;
            StateManager.Instance.weaponSpace[bNum].transform.FindChild("bootsUseIcon").GetComponentInChildren<Image>().enabled = true;
            StateManager.Instance.playUseSpd = csUseEquip.spdPoint;
            useBoots.GetComponent<Image>().sprite = StateManager.Instance.weaponSpace[bNum].transform.FindChild("bootsImage").GetComponent<Image>().sprite;

            bootsItemUse = true;
            bUse = bNum;
        }
        else if (bootsItemUse == true)
        {
            ynPop.SetActive(false);
            changePop.SetActive(true);
        }
    }
}
