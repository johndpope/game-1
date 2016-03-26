using UnityEngine;
using System.Collections;
using UnityEngine.UI;//접근 권한 얻기
using System;

public class Inven : MonoBehaviour
{

    public GameObject _EqInven;
    public GameObject _buttons;
    public GameObject _close;
    public GameObject _WeaponScroll;
    public GameObject _ItemScroll;
    public GameObject _QuestScroll;
    public GameObject _SkillScroll;
    public bool timestop;

    public GameObject ynPop;
    public GameObject rPop;

    public GameObject durabilityText;
    Text dText;

    public GameObject useText;
    Text uText;

    private int wNum;

    int d;

    int use;
    void Start()
    {
        timestop = false;
        dText = durabilityText.GetComponent<Text>();
        uText = useText.GetComponent<Text>();
        dText.text = StateManager.Instance.weaponDurability[use].ToString();
    }

    // Update is called once per frame
    void Update()
    {
        d = Int32.Parse(dText.text);

        if (csWeapom.itemUsePopBool == true)
        {
            Debug.Log("들어옴");
            ynPop.SetActive(true);
            csWeapom.itemUsePopBool = false;
        }

        if(Input.GetButtonDown("Jump"))
        {
            if(StateManager.Instance.weaponDurability[wNum] == 0)
            {
                dText.text = "0";
                DestroyObject(StateManager.Instance.weaponSpace[wNum]);
            }
            StateManager.Instance.weaponDurability[wNum] -= 1;
            dText.text = StateManager.Instance.weaponDurability[wNum].ToString();
        }
    }

    public void onInven()//인벤토리 열기
    {
        _EqInven.SetActive(true);
        timestop = true;
        _WeaponScroll.SetActive(true);
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
        _ItemScroll.SetActive(false);
        _QuestScroll.SetActive(false);
        timestop = false;
        if (timestop == false)
        {
            Time.timeScale = 1;
            // GameObject.Find("InvenIcon").GetComponent<Button>().enabled = true;
        }
    }


    public void PlayerHp()
    {
        //GameObject.Find("Hp_On").GetComponent<Image>().fillAmount -= Time.deltaTime;
        //if(GameObject.Find("Hp_On").GetComponent<Image>().fillAmount == 0)
        //{
        //    GameObject.Find("Hp_On").GetComponent<Image>().fillAmount = 1;
        //}
    }
    public void onItem()
    {
        _SkillScroll.SetActive(false);
        _ItemScroll.SetActive(true);
        _QuestScroll.SetActive(false);
        _WeaponScroll.SetActive(false);
    }
    public void onSkill()
    {
        _SkillScroll.SetActive(true);
        _ItemScroll.SetActive(false);
        _QuestScroll.SetActive(false);
        _WeaponScroll.SetActive(false);
    }
    public void onQuest()
    {
        _SkillScroll.SetActive(false);
        _ItemScroll.SetActive(false);
        _QuestScroll.SetActive(true);
        _WeaponScroll.SetActive(false);
    }
    public void onWeapon()
    {
        _WeaponScroll.SetActive(true);
        _SkillScroll.SetActive(false);
        _ItemScroll.SetActive(false);
        _QuestScroll.SetActive(false);
        //껏다 켜기
    }

    public void useItem()
    {
        int num = csWeapom.weaponNum;

        switch (num)
        {
            case 0:
                weaponSet(num);
                //ynPop.SetActive(false);
                break;
            case 1:
                weaponSet(num);
                //ynPop.SetActive(false);
                break;
            case 2:
                weaponSet(num);
                //ynPop.SetActive(false);
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

    private void weaponSet(int num)
    {
        
        wNum = StateManager.Instance.bagNum;

        if ( d == 0)
        {
            ynPop.SetActive(false);
            dText.text = StateManager.Instance.weaponDurability[wNum].ToString();
            StateManager.Instance.weaponSpace[wNum].GetComponent<Button>().enabled = false;
            StateManager.Instance.weaponSpace[wNum].transform.FindChild("weaponUseIcon").GetComponentInChildren<Image>().enabled = true;
            use = wNum;
            Debug.Log(use);
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
        Debug.Log("들어옴");
        rPop.SetActive(false);
        dText.text = StateManager.Instance.weaponDurability[wNum].ToString();
        DestroyObject(StateManager.Instance.weaponSpace[use]);
        StateManager.Instance.weaponSpace[wNum].GetComponent<Button>().enabled = false;
        StateManager.Instance.weaponSpace[wNum].transform.FindChild("weaponUseIcon").GetComponentInChildren<Image>().enabled = true;
        use = wNum;
    }
}
