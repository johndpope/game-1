using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class csAbillityStore : MonoBehaviour {
    //초기 능력치
    public int firstHp = 20;
    public int firstAtk = 5;
    public int firstDef = 3;
    public float firstSpd = 3.0f;
    //능력치 강화로 얼마나 오르는지
    public int upNumHp;
    public int upNumAtk;
    public int upNumDef;
    public float upNumSpd;
    //능력치 강화를 몇회 했는지
    public int cntHp;
    public int cntAtk;
    public int cntDef;
    public int cntSpd;
  
    ///////////////////////스크립트와 변수 위치 검토///////////////////
    public int playerHp;//피해를 받았을때
    public int playerAtk;//다른 방법으로 능력치가 바뀔경우
    public int playerDef;//다른 방법으로 능력치가 바뀔경우
    public float playerSpd;//다른 방법으로 능력치가 바뀔경우
    //////////////////////////////////////////////////////////////////
    //상태창
    //public GameObject equInven;
    public GameObject playerHpText;
    public GameObject playerAtkText;
    public GameObject playerDefText;
    public GameObject playerSpdText;
    public GameObject playerGoldText;

    public GameObject goldHpText;
    public GameObject goldAtkText;
    public GameObject goldDefText;
    public GameObject goldSpdText;

    public GameObject popClose;


    // Use this for initialization
    void Start ()
    {
        cntHp = StateManager.Instance.cntHp;
        cntAtk = StateManager.Instance.cntAtk;
        cntDef = StateManager.Instance.cntDef;
        cntSpd = StateManager.Instance.cntSpd;

        goldHpText.GetComponent<Text>().text = (cntHp * 100) + 100 + "골드";
        goldAtkText.GetComponent<Text>().text = (cntAtk * 100) + 100 + "골드";
        goldDefText.GetComponent<Text>().text = (cntDef * 100) + 100 + "골드";
        goldSpdText.GetComponent<Text>().text = (cntSpd * 100) + 100 + "골드";     
    }
	
	// Update is called once per frame
	void Update ()
    {
        //상태창 능력치 표시
        if (StateManager.Instance.firstGame.Equals(false))
        {
            StateManager.Instance.playGold = 90000;

            playerGoldText.GetComponent<Text>().text = "" + StateManager.Instance.playGold;

            StateManager.Instance.playHp = firstHp + upNumHp + playerHp;
            StateManager.Instance.playAtk = firstAtk;
            StateManager.Instance.playDef = firstDef;
            StateManager.Instance.playSpd = firstSpd;
            //StateManager.Instance.firstGameNum = 1;
        }
        playerHpText.GetComponent<Text>().text = "" + StateManager.Instance.playHp;
        playerAtkText.GetComponent<Text>().text = "" + StateManager.Instance.playAtk + " + " + StateManager.Instance.playUseAtk;
        playerDefText.GetComponent<Text>().text = "" + StateManager.Instance.playDef + " + " + StateManager.Instance.playUseDef;
        playerSpdText.GetComponent<Text>().text = "" + StateManager.Instance.playSpd + " + " + StateManager.Instance.playUseSpd;

        StateManager.Instance.playHpMax = StateManager.Instance.playHp;

        StateManager.Instance.cntHp = cntHp;
        StateManager.Instance.cntAtk = cntAtk;
        StateManager.Instance.cntDef = cntDef;
        StateManager.Instance.cntSpd = cntSpd;
    }

    public void up_Hp()
    {
        if (cntHp >= 20)
            return;        
        
        if (StateManager.Instance.playGold < (cntHp * 100) + 100)
        {
            popClose.SetActive(true);
            return;
        }
        StateManager.Instance.playGold -= (cntHp * 100) + 100;
        cntHp++;
        StateManager.Instance.playHp += 20;
        goldHpText.GetComponent<Text>().text = (cntHp * 100) + 100 + "골드";

        //playerHpText.GetComponent<Text>().text = "" + StateManager.Instance.playHp;
        if (cntHp >= 20)
            goldHpText.GetComponent<Text>().text = "최고 달성";
    }

    public void up_Atk()
    {
        if (cntAtk >= 20)        
            return;

        if (StateManager.Instance.playGold < (cntAtk * 100) + 100){
            popClose.SetActive(true);
            return;
        }
        StateManager.Instance.playGold -= (cntAtk * 100) + 100;
        StateManager.Instance.playAtk += 5;
        cntAtk++;
        goldAtkText.GetComponent<Text>().text = (cntAtk * 100) + 100 + "골드";
       // playerAtkText.GetComponent<Text>().text = "" + StateManager.Instance.playAtk;
        if (cntAtk >= 20)
            goldAtkText.GetComponent<Text>().text = "최고 달성";        
    }
    public void up_Def()
    {
        if (cntDef >= 20)       
            return;

        if (StateManager.Instance.playGold < (cntDef * 100) + 100){
            popClose.SetActive(true);
            return;
        }

        StateManager.Instance.playGold -= (cntDef * 100) + 100;
        StateManager.Instance.playDef += 3;
        cntDef++;
        goldDefText.GetComponent<Text>().text = (cntDef * 100) + 100 + "골드";
        //playerDefText.GetComponent<Text>().text = "" + StateManager.Instance.playDef;
        if (cntDef >= 20)
            goldDefText.GetComponent<Text>().text = "최고 달성";
        Debug.Log(upNumDef);
        Debug.Log(cntDef);
        
    }
    public void up_Spd()
    {
        if (cntSpd >= 10)
           return;

        if (StateManager.Instance.playGold < (cntSpd * 100) + 200)
        {
            popClose.SetActive(true);
            return;
        }

        StateManager.Instance.playGold -= (cntSpd * 100) + 200;
        StateManager.Instance.playSpd -= 0.5f;
        cntSpd++;
        goldSpdText.GetComponent<Text>().text = (cntSpd * 100) + 200 + "골드";
       // StateManager.Instance.playSpd = firstSpd + upNumSpd + playerSpd;
        if (cntSpd >= 10)
            goldSpdText.GetComponent<Text>().text = "최고 달성";
        Debug.Log(upNumSpd);
        Debug.Log(cntSpd);
    }

    public void offPopClose()
    {
        popClose.SetActive(false);
    }
}
