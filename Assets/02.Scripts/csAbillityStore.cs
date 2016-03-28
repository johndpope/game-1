using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class csAbillityStore : MonoBehaviour {
    //초기 능력치
    public int firstHp = 20;
    public int firstAtk = 5;
    public int firstDef = 3;
    public int firstSpd = 10;
    //능력치 강화로 얼마나 오르는지
    public int upNumHp;
    public int upNumAtk;
    public int upNumDef;
    public double upNumSpd;
    //능력치 강화를 몇회 했는지
    public int cntHp;
    public int cntAtk;
    public int cntDef;
    public double cntSpd;
    //돈
    public int playerGlod;

    ///////////////////////스크립트와 변수 위치 검토///////////////////
    public int playerHp;//피해를 받았을때
    public int playerAtk;//다른 방법으로 능력치가 바뀔경우
    public int playerDef;//다른 방법으로 능력치가 바뀔경우
    public double playerSpd;//다른 방법으로 능력치가 바뀔경우
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


    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        //상태창 능력치 표시
        playerHpText.GetComponent<Text>().text = "" + StateManager.Instance.playHp;
        playerAtkText.GetComponent<Text>().text = "" + StateManager.Instance.playAtk;
        playerDefText.GetComponent<Text>().text = "" + StateManager.Instance.playDef;
        playerSpdText.GetComponent<Text>().text = "" + StateManager.Instance.playSpd;
        playerGoldText.GetComponent<Text>().text = "" + StateManager.Instance.playGold;
        StateManager.Instance.playHp = firstHp + upNumHp+ playerHp;
        StateManager.Instance.playAtk = firstAtk + upNumAtk+ playerAtk;
        StateManager.Instance.playDef = firstDef + upNumDef+ playerDef;
        StateManager.Instance.playSpd = firstSpd + upNumSpd+ playerSpd;

        //소모 골드량 표시
        goldHpText.GetComponent<Text>().text = (cntHp * 100) + 100 + "\nGold";
        goldAtkText.GetComponent<Text>().text = (cntAtk * 100) + 100 + "\nGold";
        goldDefText.GetComponent<Text>().text = (cntDef * 100) + 100 + "\nGold";
        goldSpdText.GetComponent<Text>().text = (cntSpd * 100) + 100 + "\nGold";
        if (cntHp >= 20)
           goldHpText.GetComponent<Text>().text = "Sold Out";
        if (cntAtk >= 20)
            goldAtkText.GetComponent<Text>().text = "Sold Out";
        if (cntDef >= 20)
            goldDefText.GetComponent<Text>().text = "Sold Out";
        if (cntSpd >= 20)
            goldSpdText.GetComponent<Text>().text = "Sold Out";
    }
    public void up_Hp()
    {
        if (cntHp >= 20)        
            return;
        
        upNumHp += 20;
        cntHp++;
        StateManager.Instance.playGold -= 100;
        Debug.Log(upNumHp);
        Debug.Log(cntHp);
    }
    
    public void up_Atk()
    {
        if (cntAtk >= 20)        
            return;
        
        upNumAtk += 5;
        cntAtk++;
        Debug.Log(upNumAtk);
        Debug.Log(cntHp);
        
    }
    public void up_Def()
    {
        if (cntDef >= 20)       
            return;
        
        upNumDef += 3;
        cntDef++;
        Debug.Log(upNumDef);
        Debug.Log(cntDef);
        
    }
    public void up_Spd()
    {
        if (cntSpd >= 20)
           return;
        
        upNumSpd -= 0.5;
        cntSpd++;
        Debug.Log(upNumSpd);
        Debug.Log(cntSpd);
    }

}
