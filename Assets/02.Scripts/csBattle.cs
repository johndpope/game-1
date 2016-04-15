﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class csBattle : MonoBehaviour 
{
    public GameObject atkUp;
    public GameObject atkDown;
    public GameObject defUp;
    public GameObject defDown;
    public GameObject spdUp;
    public GameObject spdDown;
    public GameObject origin;
    public GameObject berserker;

    public GameObject atkUpPop;
    public GameObject defUpPop;
    public GameObject spdUpPop;
    public GameObject berserkerPop;
    public GameObject originPop;

    private ArrayList pItems;
    private ArrayList sScroll;
    private ArrayList mScroll;
    private ArrayList bScroll;

    SkillItem sItem;
    MagicItem mItem;
    BuffItem bItem;
    PotionItem pItem;


    public Scrollbar pcc;
    public GameObject[] eccObj = new GameObject[3];
    public Scrollbar[] ecc = new Scrollbar[3];

    public float pTimer;
    float pTimer2;
    public static float[] eTimer = new float[3];

    //float pTimer2;
    public static float[] eTimer2 = new float[3];

    bool s;
  
    public GameObject atkBtn;
    public GameObject skillBtn;
    public GameObject itemBtn;

    public GameObject runBtn;
    public GameObject player;
    public GameObject player2D;

    public GameObject timer;

    public GameObject playPos;

    private int damRock;

    public GameObject[] battlePos = new GameObject[3];
    public GameObject battleCameraObj;
    Camera battelCamera = new Camera();

    private bool attEnemyBool;
    public float enemySpd;
    public float enemyro;

    public GameObject touchEvent;
    //공격시 보이는 텍스트
    public GameObject battleTextObj;
    Text battleText;

    public GameObject itemPop;
    
    public GameObject scrollPop;
    public GameObject skillPop;
    public GameObject magicPop;
    public GameObject buffPop;

    public GameObject buttons;

    //몬스터방어력 감소
    public static float monsterDef;
    public static int defDownEnemy;
    //몬스터 스피드 감소
    public static float monsterSpd;
    public static int spdDownEnemy;
    //몬스터 공격력 감소
    public static float monsterAtk;
    public static int atkDownEnemy;

    bool nAtk;
    bool sAtk;
    bool mAtk;
    bool bSelf;
    bool pUse;

    public static int turn;

    float playAtk;
    float playDef;
    float playSpd;

    float playOriginAtk;
    float playOriginDef;
    float playOriginSpd;

    float playBerserkerAtk;
    float playBerserkerDef;

    int atkTurn;
    int defTurn;
    int spdTurn;
    int originTurn;
    int berserkerTurn;

    int atkPotionTurn;
    int defPotionTurn;
    int spdPotionTurn;

    float playPotionAtk;
    float playPotionDef;
    float playPotionSpd;

    public GameObject joystick;
    public GameObject invenBtn;

    public GameObject atkPotion;
    public GameObject defPotion;
    public GameObject spdPotion;

    public GameObject pop;

    public GameObject playPopAtk;
    public GameObject playPopDef;
    public GameObject playPopSpd;

    Text playPopAtkText;
    Text playPopDefText;
    Text playPopSpdText;

    int monsterNum;

    public GameObject useWeapon;

    Sprite useWeaponSpeite;

    public GameObject weaponText;
    Text weaponTextD;

  
    void OnEnable()
    {

        if (StateManager.Instance.useWeapon != null)
        {
            useWeapon.GetComponent<Image>().sprite = StateManager.Instance.useWeapon;
        }
       

        weaponTextD = weaponText.GetComponent<Text>();
        playPopAtkText = playPopAtk.GetComponent<Text>();
        playPopDefText = playPopDef.GetComponent<Text>();
        playPopSpdText = playPopSpd.GetComponent<Text>();
        turn = 0;
        pItems = StateManager.Instance.potionItems;
        sScroll = StateManager.Instance.skillScrollItems;
        mScroll = StateManager.Instance.magicScrollItems;
        bScroll = StateManager.Instance.buffScrollItems;

        attEnemyBool = false;
        enemySpd = 0.1f;
        enemyro = 5.0f;

        pTimer = StateManager.Instance.playSpd;
        //eTimer = StateManager.Instance.monsterSpd;

        battelCamera = battleCameraObj.GetComponent<Camera>();
        battleText = battleTextObj.GetComponent<Text>();

        monsterNum = StateManager.Instance.monsterNum;
        pTimer2 = pTimer;
        for(int i=0; i<StateManager.Instance.monsterNum;i++)
        {
            eTimer2[i] = eTimer[i];
            Debug.Log(eTimer2[i]);
            Debug.Log(eTimer[i]);
        }

        StartCoroutine(playerData());

    }
	
	void Update ()
    {
        sItem = (SkillItem)sScroll[StateManager.Instance.useItemNum];
        mItem = (MagicItem)mScroll[StateManager.Instance.useItemNum];
        bItem = (BuffItem)bScroll[StateManager.Instance.useItemNum];
        pItem = (PotionItem)pItems[StateManager.Instance.useItemNum];

        weaponTextD.text = StateManager.Instance.weaponDurability[StateManager.Instance.wUse].ToString();

        if (damRock == 2)
        {
            pcc.value = 0;
            StateManager.Instance.timerIsActive = false;
            s = false;
            GameObject.Find("battleRock").transform.position = new Vector3(0, 0, 0);
            GameObject.Find("battleRock").SetActive(false);
            damRock = 0;
            StateManager.Instance.objBlocked = false;
            battelCamera.enabled = false;
            timer.SetActive(false);
            joystick.GetComponent<Image>().enabled = true;
            invenBtn.SetActive(true);
            player2D.transform.position = new Vector3(13.5f,0,-30);
            gameObject.SetActive(false);
        }

        if (s == true)
        {
            player2D.transform.Translate(0, 0, -0.2f);

            if (player2D.transform.position.z <= -27.0f)
            {
                player2D.transform.position = playPos.transform.position;
                s = false;
                pcc.value = 0;
                damRock++;
                StateManager.Instance.timerIsActive = true;
            }
        }

        PlayerBattle();

        if(StateManager.Instance.useItemBool.Equals(true))
        {
            if(StateManager.Instance.useItemName.Equals("Skill"))
            {
                scrollPop.SetActive(false);
                touchEvent.SetActive(true);
                sAtk = true;
                StartCoroutine(BattleText());
                skillPop.SetActive(false);
                buttons.SetActive(false);
                StateManager.Instance.useItemBool = false;
                Debug.Log("스킬이 눌림");
            }

            if (StateManager.Instance.useItemName.Equals("Magic"))
            {
                scrollPop.SetActive(false);
                touchEvent.SetActive(true);
                mAtk = true;
                magicPop.SetActive(false);
                buttons.SetActive(false);
                StartCoroutine(BattleText());
                StateManager.Instance.useItemBool = false;
                Debug.Log("마법이 눌림");
            }

            if (StateManager.Instance.useItemName.Equals("Buff"))
            {
                scrollPop.SetActive(false);
                touchEvent.SetActive(true);
                bSelf = true;
                buffPop.SetActive(false);
                buttons.SetActive(false);
                StartCoroutine(BattleText());
                StateManager.Instance.useItemBool = false;
                Debug.Log("버프이 눌림");
            }

            if (StateManager.Instance.useItemName.Equals("Potion"))
            {
                touchEvent.SetActive(true);
                itemPop.SetActive(false);
                pUse = true;
                StartCoroutine(BattleText());
                StateManager.Instance.useItemBool = false;
                Debug.Log("포션이 눌림");
            }
        }
        if (atkUp.activeSelf.Equals(true) && (atkTurn + 3) == turn)
        {
            playAtk = 0;
            atkUp.SetActive(false);
            atkUpPop.SetActive(false);
        }

        if (defUp.activeSelf.Equals(true) && (defTurn + 3) == turn)
        {
            playDef=0;
            defUp.SetActive(false);
            defUpPop.SetActive(false);
        }

        if (spdUp.activeSelf.Equals(true) && (spdTurn + 3) == turn)
        {
            pTimer += playSpd;
            pTimer2 += playSpd;
            playSpd = 0;
            spdUp.SetActive(false);
            spdUpPop.SetActive(false);
        }

        if(origin.activeSelf.Equals(true) && (originTurn + 2) == turn)
        {
            pTimer2 += playOriginSpd;
            pTimer += playOriginSpd;
            playOriginAtk = 0;
            playOriginDef = 0;
            playOriginSpd = 0;
            originPop.SetActive(false);
            origin.SetActive(false);
        }

        if (berserker.activeSelf.Equals(true) && (berserkerTurn + 2) == turn)
        {
            playBerserkerAtk = 0;
            playBerserkerDef = 0;
            berserkerPop.SetActive(false);
            berserker.SetActive(false);
        }

        if(atkPotion.activeSelf.Equals(true) && atkPotionTurn +2 == turn)
        {
            atkPotion.SetActive(false);
            playPotionAtk = 0;
        }
        if (defPotion.activeSelf.Equals(true) && defPotionTurn + 2 == turn)
        {
            defPotion.SetActive(false);
            playPotionDef = 0;
        }
        if (spdPotion.activeSelf.Equals(true) && spdPotionTurn + 2 == turn)
        {
            spdPotion.SetActive(false);
            pTimer2 += playPotionSpd;
            pTimer += playPotionSpd;
            playPotionSpd = 0;
        }

        if (monsterNum.Equals(0) && StateManager.Instance.monsterBattle.Equals(true))
        {
            finish();
           
        }
        TimerCut();
        
    }

    public void choiseBox(int num)
    {

    }


    private void finish()
    {
        
    }

    public void finishYes()
    {
        pop.SetActive(false);
        battelCamera.enabled = false;
        timer.SetActive(false);
        joystick.GetComponent<Image>().enabled = true;
        invenBtn.SetActive(true);
        StateManager.Instance.timerIsActive = false;
        player2D.transform.position = new Vector3(13.5f, 0, -30);
        StateManager.Instance.monsterNum = 0;
        gameObject.SetActive(false);
    }

    IEnumerator playerData()
    {
        //playerGoldText.GetComponent<Text>().text = "" + StateManager.Instance.playGold;
      
        //playerHpText.GetComponent<Text>().text = "" + StateManager.Instance.playHp;
        playPopAtkText.text = ": " + (StateManager.Instance.playAtk  + StateManager.Instance.playUseAtk  + (playAtk+playBerserkerAtk+playOriginAtk) + playPotionAtk);
        playPopDefText.text = ": " + (StateManager.Instance.playDef  + StateManager.Instance.playUseDef  + (playDef - playBerserkerDef + playOriginAtk)  + playPotionDef);
        playPopSpdText.text = ": " + (StateManager.Instance.playSpd  - StateManager.Instance.playUseSpd  - (playSpd  + playOriginSpd)  - playPotionSpd);
        yield return null;
        StartCoroutine(playerData());
    }

    public void TimerCut()
    {
        if (StateManager.Instance.timerIsActive == true)
        {
            timer.SetActive(true);
            joystick.GetComponent<Image>().enabled = false;
            invenBtn.SetActive(false);
            pop.SetActive(true);

            if (timer.activeSelf == true)
            {
                //돌과 싸울경우 시간 감소
                if (StateManager.Instance.objBlocked == true)
                {
                    pTimer = pTimer / 2;
                    pTimer -= Time.deltaTime;
                    pcc.value += Time.deltaTime / StateManager.Instance.playSpd;

                    if (pTimer <= 0 /*&& boom < 1*/)
                    {
                        ObjBreak();
                    }
                }
                //몬스터와 싸울경우
                if(StateManager.Instance.monsterBattle==true)
                {
                    pTimer -= Time.deltaTime;
                    pcc.value += Time.deltaTime / pTimer2;
                    //pcc.value += Time.deltaTime / pTimer;
                    //Debug.Log(pTimer + "    " + "넘버");
                    //Debug.Log(pTimer2 + "    " + "넘버");
                    if (pTimer <= 0)
                    {
                        pTimer = 0;
                        atkBtn.SetActive(true);
                        skillBtn.SetActive(true);
                        itemBtn.SetActive(true);
                        runBtn.SetActive(true);
                        StateManager.Instance.timerIsActive = false;
                    }

                    for (int i = 0; i < StateManager.Instance.monsterNum; i++)
                    {
                        //if (StateManager.Instance.monster[i] == null)
                        //{
                        //    i++;
                        //    if(i >= StateManager.Instance.monsterNum)
                        //    {
                        //        return;
                        //    }
                        //    //Debug.Log(i + "              전투 끝");
                        //}
                        if (eccObj[i].activeSelf.Equals(true))
                        {
                            eTimer[i] -= Time.deltaTime;
                            ecc[i].value += Time.deltaTime / eTimer2[i];
                           //Debug.Log(eTimer2[i] + "    " + "넘버" + i);
                           // Debug.Log(eTimer[i] + "    " + "넘버" + i);

                            if (eTimer[i] <= 0)
                            {

                                if (StateManager.Instance.monster[i].transform.position.x <= playPos.transform.position.x)
                                {
                                    StateManager.Instance.monster[i].transform.LookAt(player2D.transform.position);
                                    StateManager.Instance.monsterBattle = false;
                                    StartCoroutine(EnemyAtt(i));
                                }

                                else
                                {
                                    ecc[i].value = 1;
                                    Vector3 dir1 = playPos.transform.position - StateManager.Instance.monster[i].transform.position;
                                    dir1.y = 0.0f; //높이                
                                    dir1.Normalize(); // Normalize()백터3함수 x,z를  정규화
                                    StateManager.Instance.monster[i].transform.Translate(0, 0, enemySpd);
                                    StateManager.Instance.monster[i].transform.rotation = Quaternion.Lerp(StateManager.Instance.monster[i].transform.rotation, Quaternion.LookRotation(dir1), enemyro * Time.deltaTime);
                                }

                            }
                        }
                    }
                }
            }
        }
    }

    public void atk()
    {
        pcc.value = 0;
        atkBtn.SetActive(false);
        skillBtn.SetActive(false);
        itemBtn.SetActive(false);
        runBtn.SetActive(false);
        nAtk = true;
        StartCoroutine(BattleText());
        StateManager.Instance.normalAtk = true;
        touchEvent.SetActive(true);
    }

    public void Scroll()
    {
        pcc.value = 0;

        atkBtn.SetActive(false);
        skillBtn.SetActive(false);
        itemBtn.SetActive(false);
        runBtn.SetActive(false);
        scrollPop.SetActive(true);
        buttons.SetActive(true);
        StateManager.Instance.scrollAtk = true;
    }

    public void PotionBtn()
    {
        pcc.value = 0;

        atkBtn.SetActive(false);
        skillBtn.SetActive(false);
        itemBtn.SetActive(false);
        runBtn.SetActive(false);
        itemPop.SetActive(true);
        StateManager.Instance.potionUse = true;
    }

    private void ObjBreak()
    {
        player2D.transform.Translate(0, 0, -0.2f);

        if(player2D.transform.position.x >= 21.3f)
        {
            pTimer = 0;            
            StateManager.Instance.timerIsActive = false;
            pTimer = StateManager.Instance.playSpd;
            StartCoroutine("PlayPos");
            
        }
    }
    IEnumerator PlayPos()
    {
        if(StateManager.Instance.weaponSpace[StateManager.Instance.wUse] != null && StateManager.Instance.useWeapon != null)
        {
            StateManager.Instance.weaponDurability[StateManager.Instance.wUse]--;
            StateManager.Instance.dText.GetComponent<Text>().text = StateManager.Instance.weaponDurability[StateManager.Instance.wUse].ToString();
            StateManager.Instance.weaponSpace[StateManager.Instance.wUse].transform.FindChild("weaponDurabilityText").GetComponent<Text>().text = "내구도: " + StateManager.Instance.weaponDurability[StateManager.Instance.wUse].ToString();
        }
        else
        {
            StateManager.Instance.playHp -= 5;
            Debug.Log(StateManager.Instance.playHp);
        }
       
        yield return new WaitForSeconds(1.0f);
        if(damRock != 2 && StateManager.Instance.objBlocked == true)
        {
            s = true;
        }
       
    }

    IEnumerator EnemyAtt(int num)
    {
        //   if (StateManager.Instance.monster[num].name == slime.Name + num && ecc[num].value != 0)
        //  {
        if (StateManager.Instance.monsterAtk[num] - (StateManager.Instance.playDef + playPotionDef + playDef + playOriginDef - playBerserkerDef) <= 0)
        {
            StateManager.Instance.playHp -= 1;
            Debug.Log(StateManager.Instance.playHp);
        }
        else
        {
            StateManager.Instance.playHp -= (StateManager.Instance.monsterAtk[num] - (StateManager.Instance.playDef + playPotionDef + playDef));
            Debug.Log(StateManager.Instance.playHp);
        }

        //}
        StateManager.Instance.monster[num].transform.FindChild("mo").GetComponent<main1>().ani(1);
        yield return new WaitForSeconds(1.0f);
        enemyPos(num);
    }

    private void enemyPos(int num)
    {
        //if (StateManager.Instance.monster[num].name == slime.Name + num)
        //{
            eTimer[num] = eTimer2[num]; //Random.Range(slime.MonsterMinSpd, slime.MonsterMaxSpd + 1);
        Debug.Log(eTimer[num]);
            ecc[num].value = 0;
        //}
        StateManager.Instance.monster[num].transform.position = battlePos[num].transform.position;
        StateManager.Instance.monster[num].transform.FindChild("mo").GetComponent<main1>().ani(0);
        StateManager.Instance.monsterBattle = true;
    }


    private void PlayerBattle()
    {
        if (StateManager.Instance.playerBattleBool.Equals(true))
        {     
            if (player2D.transform.position.x + 2 >= StateManager.Instance.monster[StateManager.Instance.atkEnemyNum].transform.position.x)//터치로 넘버값 받아오기
            {
               
                StartCoroutine(PlayerAtk());              
                StateManager.Instance.playerBattleBool = false;
            }

            else
            {
                Vector3 dir1 = player2D.transform.position - StateManager.Instance.monster[StateManager.Instance.atkEnemyNum].transform.position;
                dir1.y = 0.0f; //높이                
                dir1.Normalize(); // Normalize()백터3함수 x,z를  정규화
                player2D.transform.Translate(0, 0, -enemySpd);
                player2D.transform.rotation = Quaternion.Lerp(player2D.transform.rotation, Quaternion.LookRotation(dir1), enemyro * Time.deltaTime);
            }
        }
        if(StateManager.Instance.playerMagicBool.Equals(true))
        {
            StartCoroutine(playerMagic());
            StateManager.Instance.playerMagicBool = false;
        }
        if(StateManager.Instance.playerbuffBool.Equals(true))
        {
            StartCoroutine(Playerbuff());
            StateManager.Instance.playerbuffBool = false;
        }
        if (StateManager.Instance.playerPotionBool.Equals(true))
        {
            StartCoroutine(PlayerPotion());
            StateManager.Instance.playerPotionBool = false;
        }
    }

    IEnumerator PlayerPotion()
    {
        Potion();
        yield return new WaitForSeconds(1.5f);
        pTimer = pTimer2;
        turn++;
        StateManager.Instance.timerIsActive = true;
        StateManager.Instance.monsterBattle = true;
    }

    IEnumerator playerMagic()
    {
        Magic();


        if (StateManager.Instance.monsterHp[StateManager.Instance.atkEnemyNum] <= 0)
        {
            monsterNum--;
            eccObj[StateManager.Instance.atkEnemyNum].SetActive(false);
            StateManager.Instance.monster[StateManager.Instance.atkEnemyNum].transform.position = new Vector3(0, 0, 0);
            StateManager.Instance.monster[StateManager.Instance.atkEnemyNum].SetActive(false);
        }
        
        yield return new WaitForSeconds(1.5f);
        pTimer = pTimer2;
        turn++;
        StateManager.Instance.timerIsActive = true;
        StateManager.Instance.monsterBattle = true;
    }

    IEnumerator Playerbuff()
    {
        Buff();
        yield return new WaitForSeconds(1.5f);
        pTimer = pTimer2;
        turn++;
        StateManager.Instance.timerIsActive = true;
        StateManager.Instance.monsterBattle = true;
    }

    IEnumerator PlayerAtk()
    {
        if (StateManager.Instance.weaponSpace[StateManager.Instance.wUse] != null &&  StateManager.Instance.useWeapon != null)
        {
            StateManager.Instance.weaponDurability[StateManager.Instance.wUse]--;
            StateManager.Instance.dText.GetComponent<Text>().text = StateManager.Instance.weaponDurability[StateManager.Instance.wUse].ToString();
            StateManager.Instance.weaponSpace[StateManager.Instance.wUse].transform.FindChild("weaponDurabilityText").GetComponent<Text>().text = "내구도: " + StateManager.Instance.weaponDurability[StateManager.Instance.wUse].ToString();

            if(StateManager.Instance.skillAtk.Equals(true))
            {
                Skill();
            }
            else
            {
                StateManager.Instance.monsterHp[StateManager.Instance.atkEnemyNum] -= (StateManager.Instance.playAtk + StateManager.Instance.playUseAtk + playPotionAtk + playOriginAtk + playAtk + playBerserkerAtk) - StateManager.Instance.monsterDef[StateManager.Instance.atkEnemyNum];
            }

            if (StateManager.Instance.monsterHp[StateManager.Instance.atkEnemyNum] <= 0)
            {
                eccObj[StateManager.Instance.atkEnemyNum].SetActive(false);
                monsterNum--;
                StateManager.Instance.monster[StateManager.Instance.atkEnemyNum].transform.position = new Vector3(0, 0, 0);
                StateManager.Instance.monster[StateManager.Instance.atkEnemyNum].SetActive(false);
            }
        }
        else
        {
            if (StateManager.Instance.skillAtk.Equals(true))
            {
                StateManager.Instance.playHp -= 5;
                Skill();
            }
            else
            {
                StateManager.Instance.playHp -= 5;
                StateManager.Instance.monsterHp[StateManager.Instance.atkEnemyNum] -= (StateManager.Instance.playAtk + StateManager.Instance.playUseAtk + playPotionAtk + playOriginAtk + playAtk + playBerserkerAtk) - StateManager.Instance.monsterDef[StateManager.Instance.atkEnemyNum];
            }
            if (StateManager.Instance.monsterHp[StateManager.Instance.atkEnemyNum] <= 0)
            {
                monsterNum--;
                eccObj[StateManager.Instance.atkEnemyNum].SetActive(false);
                StateManager.Instance.monster[StateManager.Instance.atkEnemyNum].transform.position = new Vector3(0, 0, 0);
                StateManager.Instance.monster[StateManager.Instance.atkEnemyNum].SetActive(false);
            }
        }
        yield return new WaitForSeconds(1.0f);
        pTimer = pTimer2;
        turn++;
        StateManager.Instance.timerIsActive = true;
        StateManager.Instance.monsterBattle = true;
        player2D.transform.position = new Vector3(playPos.transform.position.x - 1.5f, playPos.transform.position.y, playPos.transform.position.z);
        player2D.transform.LookAt(playPos.transform.position);
        player2D.transform.rotation = new Quaternion(0,-0.5f, 0, 0.8f);
    }

    IEnumerator BattleText()
    {
        if (nAtk.Equals(true))
        {
            battleText.text = "일 반 공 격  할  \n 적 을  클 릭 하 세 요.";
            battleTextObj.SetActive(true);
        }
        if(sAtk.Equals(true))
        {
            battleText.text = sItem.Name + " 할  \n 적 을  클 릭 하 세 요.";
            battleTextObj.SetActive(true);
        }
        if (mAtk.Equals(true))
        {
            battleText.text = mItem.Name + " 할  \n 적 을  클 릭 하 세 요.";
            battleTextObj.SetActive(true);
        }

        if (bSelf.Equals(true))
        {
            battleText.text = bItem.Name + " 할  \n 캐 릭 터 를  클 릭 하 세 요.";
            battleTextObj.SetActive(true);
        }

        if (pUse.Equals(true))
        {
            battleText.text = pItem.Name + " 할  \n 캐 릭 터 를  클 릭 하 세 요.";
            battleTextObj.SetActive(true);
        }
        yield return new WaitForSeconds(1.0f);
        nAtk = false;
        sAtk = false;
        mAtk = false;
        bSelf = false;
        pUse = false;
        battleTextObj.SetActive(false);
    }

    private void Skill()
    {
        StateManager.Instance.skillAtk = false;
        switch (StateManager.Instance.useItemNum)
        {
            case 0:
                //강격
                Debug.Log(sItem.Name);
                //코루틴으로 이펙트 넣을것  StartCoroutine(이펙트 함수명(StateManager.Instance.useItemNum))
                int ca = Random.Range(0, 10);
                if (ca == 5)
                {
                    StateManager.Instance.monsterHp[StateManager.Instance.atkEnemyNum] -= (sItem.SpecialAbility * ((StateManager.Instance.playAtk + StateManager.Instance.playUseAtk + playPotionAtk + playAtk+ playBerserkerAtk + playOriginAtk) * sItem.AttackUpPoint)) - StateManager.Instance.monsterDef[StateManager.Instance.atkEnemyNum];
                }
                else
                {
                    StateManager.Instance.monsterHp[StateManager.Instance.atkEnemyNum] -= ((StateManager.Instance.playAtk + StateManager.Instance.playUseAtk + playPotionAtk + playAtk + playBerserkerAtk + playOriginAtk) * sItem.AttackUpPoint) - StateManager.Instance.monsterDef[StateManager.Instance.atkEnemyNum];
                }

                StateManager.Instance.SkscrollNum[StateManager.Instance.useItemNum]--;
                if(StateManager.Instance.SkscrollNum[StateManager.Instance.useItemNum]==0)
                {
                    DestroyObject(StateManager.Instance.SkScrollBag[StateManager.Instance.useItemNum]);
                }
                //StateManager.Instance.useItemAtkBool = false;
                break;
            case 1:
                Debug.Log(sItem.Name);
                //회전배기 광역
                //코루틴으로 이펙트 넣을것  StartCoroutine(이펙트 함수명(StateManager.Instance.useItemNum))

                for (int i = 0; i < StateManager.Instance.monsterNum; i++)
                {
                    StateManager.Instance.monsterHp[i] -= ((StateManager.Instance.playAtk + StateManager.Instance.playUseAtk + playPotionAtk + playAtk + playBerserkerAtk+ playOriginAtk) * sItem.AttackUpPoint) - StateManager.Instance.monsterDef[i];
                }
                StateManager.Instance.SkscrollNum[StateManager.Instance.useItemNum]--;
                if (StateManager.Instance.SkscrollNum[StateManager.Instance.useItemNum] == 0)
                {
                    DestroyObject(StateManager.Instance.SkScrollBag[StateManager.Instance.useItemNum]);
                }
                //StateManager.Instance.useItemAtkBool = false;
                break;
            case 2:
                Debug.Log(sItem.Name);
                //무모한돌진 넉백과 적 시간 증가
                //코루틴으로 이펙트 넣을것  StartCoroutine(이펙트 함수명(StateManager.Instance.useItemNum))

                eTimer[StateManager.Instance.atkEnemyNum] += sItem.SpecialAbility;
                StateManager.Instance.monsterHp[StateManager.Instance.atkEnemyNum] -= ((StateManager.Instance.playAtk + StateManager.Instance.playUseAtk + playPotionAtk + playAtk + playBerserkerAtk + playOriginAtk) * sItem.AttackUpPoint) - StateManager.Instance.monsterDef[StateManager.Instance.atkEnemyNum];
                StateManager.Instance.monster[StateManager.Instance.atkEnemyNum].transform.position = battlePos[StateManager.Instance.atkEnemyNum].transform.position;
                
                StateManager.Instance.SkscrollNum[StateManager.Instance.useItemNum]--;
                if (StateManager.Instance.SkscrollNum[StateManager.Instance.useItemNum] == 0)
                {
                    DestroyObject(StateManager.Instance.SkScrollBag[StateManager.Instance.useItemNum]);
                }
                //StateManager.Instance.useItemAtkBool = false;
                break;

            case 3:
                Debug.Log(sItem.Name);
                //가르기 방어감소
                //코루틴으로 이펙트 넣을것  StartCoroutine(이펙트 함수명(StateManager.Instance.useItemNum))

                defDownEnemy = StateManager.Instance.atkEnemyNum;
                monsterDef = StateManager.Instance.monsterDef[defDownEnemy];

                StateManager.Instance.monster[StateManager.Instance.atkEnemyNum].transform.FindChild("defdown").GetComponent<SpriteRenderer>().enabled = true;
                StateManager.Instance.monster[StateManager.Instance.atkEnemyNum].transform.FindChild("defdown").GetComponent<csDefDown>().enabled = true;

                StateManager.Instance.monsterDef[StateManager.Instance.atkEnemyNum] = StateManager.Instance.monsterDef[StateManager.Instance.atkEnemyNum] * sItem.SpecialAbility;                
                StateManager.Instance.monsterHp[StateManager.Instance.atkEnemyNum] -= ((StateManager.Instance.playAtk + StateManager.Instance.playUseAtk + playPotionAtk + playAtk + playBerserkerAtk + playOriginAtk) * sItem.AttackUpPoint) - StateManager.Instance.monsterDef[StateManager.Instance.atkEnemyNum];

                StateManager.Instance.SkscrollNum[StateManager.Instance.useItemNum]--;
                if (StateManager.Instance.SkscrollNum[StateManager.Instance.useItemNum] == 0)
                {
                    DestroyObject(StateManager.Instance.SkScrollBag[StateManager.Instance.useItemNum]);
                }
                //StateManager.Instance.useItemAtkBool = false;
                break;
        }
    }

    private void Magic()
    {
        StateManager.Instance.MagicAtk = false;
        switch (StateManager.Instance.useItemNum)
        {
            case 0:
                //얼음화살 슬로우
                //코루틴으로 이펙트 넣을것  StartCoroutine(이펙트 함수명(StateManager.Instance.useItemNum))
                spdDownEnemy = StateManager.Instance.atkEnemyNum;
                monsterSpd = StateManager.Instance.monsterSpd[spdDownEnemy];

                StateManager.Instance.monster[StateManager.Instance.atkEnemyNum].transform.FindChild("spddown").GetComponent<SpriteRenderer>().enabled = true;
                StateManager.Instance.monster[StateManager.Instance.atkEnemyNum].transform.FindChild("spddown").GetComponent<csSpdDown>().enabled = true;

                StateManager.Instance.monsterSpd[StateManager.Instance.atkEnemyNum] = StateManager.Instance.monsterSpd[StateManager.Instance.atkEnemyNum] + (StateManager.Instance.monsterSpd[StateManager.Instance.atkEnemyNum] * mItem.SpdDownPoint);
                eTimer2[StateManager.Instance.atkEnemyNum] = StateManager.Instance.monsterSpd[StateManager.Instance.atkEnemyNum];
                if (eTimer[StateManager.Instance.atkEnemyNum] >= 0)
                {
                    eTimer[StateManager.Instance.atkEnemyNum] = StateManager.Instance.monsterSpd[StateManager.Instance.atkEnemyNum] - eTimer[StateManager.Instance.atkEnemyNum];
                }

                StateManager.Instance.monsterHp[StateManager.Instance.atkEnemyNum] -= ((StateManager.Instance.playAtk + StateManager.Instance.playUseAtk + playPotionAtk + playAtk + playBerserkerAtk + playOriginAtk) * mItem.AttactPoint) - StateManager.Instance.monsterDef[StateManager.Instance.atkEnemyNum];
                StateManager.Instance.MgscrollNum[StateManager.Instance.useItemNum]--;
                if (StateManager.Instance.MgscrollNum[StateManager.Instance.useItemNum] == 0)
                {
                    DestroyObject(StateManager.Instance.MgScrollBag[StateManager.Instance.useItemNum]);
                }
                break;
            case 1:
                //불화살
                //코루틴으로 이펙트 넣을것  StartCoroutine(이펙트 함수명(StateManager.Instance.useItemNum))

                StateManager.Instance.monsterHp[StateManager.Instance.atkEnemyNum] -= ((StateManager.Instance.playAtk + StateManager.Instance.playUseAtk + playPotionAtk + playAtk + playBerserkerAtk + playOriginAtk) * mItem.AttactPoint) - StateManager.Instance.monsterDef[StateManager.Instance.atkEnemyNum];

                StateManager.Instance.MgscrollNum[StateManager.Instance.useItemNum]--;
                if (StateManager.Instance.MgscrollNum[StateManager.Instance.useItemNum] == 0)
                {
                    DestroyObject(StateManager.Instance.MgScrollBag[StateManager.Instance.useItemNum]);
                }

                break;
            case 2:
                //불기둥
                //코루틴으로 이펙트 넣을것  StartCoroutine(이펙트 함수명(StateManager.Instance.useItemNum))

                for (int i = 0; i < StateManager.Instance.monsterNum; i++)
                {
                    StateManager.Instance.monsterHp[i] -= ((StateManager.Instance.playAtk + StateManager.Instance.playUseAtk + playPotionAtk + playAtk + playBerserkerAtk + playOriginAtk) * mItem.AttactPoint) - StateManager.Instance.monsterDef[i];
                }
                StateManager.Instance.MgscrollNum[StateManager.Instance.useItemNum]--;
                if (StateManager.Instance.MgscrollNum[StateManager.Instance.useItemNum] == 0)
                {
                    DestroyObject(StateManager.Instance.MgScrollBag[StateManager.Instance.useItemNum]);
                }
                break;
            case 3:
                //대지의 화살
                //코루틴으로 이펙트 넣을것  StartCoroutine(이펙트 함수명(StateManager.Instance.useItemNum))
                atkDownEnemy = StateManager.Instance.atkEnemyNum;
                monsterAtk = StateManager.Instance.monsterAtk[atkDownEnemy];

                StateManager.Instance.monster[StateManager.Instance.atkEnemyNum].transform.FindChild("atkdown").GetComponent<SpriteRenderer>().enabled = true;
                StateManager.Instance.monster[StateManager.Instance.atkEnemyNum].transform.FindChild("atkdown").GetComponent<csAtkDown>().enabled = true;

                StateManager.Instance.monsterAtk[StateManager.Instance.atkEnemyNum] = StateManager.Instance.monsterAtk[StateManager.Instance.atkEnemyNum] * mItem.AtkDownPoint;
                StateManager.Instance.monsterHp[StateManager.Instance.atkEnemyNum] -= ((StateManager.Instance.playAtk + StateManager.Instance.playUseAtk + playPotionAtk + playAtk + playBerserkerAtk + playOriginAtk) * mItem.AttactPoint) - StateManager.Instance.monsterDef[StateManager.Instance.atkEnemyNum];

                StateManager.Instance.MgscrollNum[StateManager.Instance.useItemNum]--;
                if (StateManager.Instance.MgscrollNum[StateManager.Instance.useItemNum] == 0)
                {
                    DestroyObject(StateManager.Instance.MgScrollBag[StateManager.Instance.useItemNum]);
                }
                break;
        }
    }

    private void Buff()
    {
        StateManager.Instance.buffUse = false;
        switch (StateManager.Instance.useItemNum)
        {
            case 0:
                //힘의 스크롤
                //코루틴으로 이펙트 넣을것  StartCoroutine(이펙트 함수명(StateManager.Instance.useItemNum))
                playAtk += (StateManager.Instance.playAtk * bItem.AtkUp);
                atkUp.SetActive(true);
                atkTurn = turn;
                atkUpPop.SetActive(true);
                StateManager.Instance.BufscrollNum[StateManager.Instance.useItemNum]--;
                if (StateManager.Instance.BufscrollNum[StateManager.Instance.useItemNum] == 0)
                {
                    DestroyObject(StateManager.Instance.BufScrollBag[StateManager.Instance.useItemNum]);
                }
                break;
            case 1:
                //인내의 스크롤
                //코루틴으로 이펙트 넣을것  StartCoroutine(이펙트 함수명(StateManager.Instance.useItemNum))
                playDef += (StateManager.Instance.playDef * bItem.DefUp);
                defUp.SetActive(true);
                defTurn = turn;
                defUpPop.SetActive(true);
                StateManager.Instance.BufscrollNum[StateManager.Instance.useItemNum]--;
                if (StateManager.Instance.BufscrollNum[StateManager.Instance.useItemNum] == 0)
                {
                    DestroyObject(StateManager.Instance.BufScrollBag[StateManager.Instance.useItemNum]);
                }
                break;
            case 2:
                //신속의 스크롤
                //코루틴으로 이펙트 넣을것  StartCoroutine(이펙트 함수명(StateManager.Instance.useItemNum))
                playSpd += bItem.SpdUp;
                spdUp.SetActive(true);
                spdTurn = turn;
                pTimer2 -= bItem.SpdUp;
                spdUpPop.SetActive(true);
                StateManager.Instance.BufscrollNum[StateManager.Instance.useItemNum]--;
                if (StateManager.Instance.BufscrollNum[StateManager.Instance.useItemNum] == 0)
                {
                    DestroyObject(StateManager.Instance.BufScrollBag[StateManager.Instance.useItemNum]);
                }
                break;
            case 3:
                //기원의 서
                //코루틴으로 이펙트 넣을것  StartCoroutine(이펙트 함수명(StateManager.Instance.useItemNum))

                playOriginAtk += (StateManager.Instance.playAtk * bItem.AtkUp);
                playOriginDef += (StateManager.Instance.playDef * bItem.DefUp);
                playOriginSpd += bItem.SpdUp;

                pTimer2 -= bItem.SpdUp;

                originTurn = turn;
                origin.SetActive(true);
                originPop.SetActive(true);
                StateManager.Instance.BufscrollNum[StateManager.Instance.useItemNum]--;
                if (StateManager.Instance.BufscrollNum[StateManager.Instance.useItemNum] == 0)
                {
                    DestroyObject(StateManager.Instance.BufScrollBag[StateManager.Instance.useItemNum]);
                }
                break;
            case 4:
                //광폭의 서
                //코루틴으로 이펙트 넣을것  StartCoroutine(이펙트 함수명(StateManager.Instance.useItemNum))

                playBerserkerAtk += (StateManager.Instance.playAtk * bItem.AtkUp);

                playBerserkerDef += (StateManager.Instance.playDef * bItem.DefUp);

                berserkerTurn = turn;
                berserker.SetActive(true);
                berserkerPop.SetActive(true);
                StateManager.Instance.BufscrollNum[StateManager.Instance.useItemNum]--;
                if (StateManager.Instance.BufscrollNum[StateManager.Instance.useItemNum] == 0)
                {
                    DestroyObject(StateManager.Instance.BufScrollBag[StateManager.Instance.useItemNum]);
                }
                break;
            case 5:
                //고대의치유서
                //코루틴으로 이펙트 넣을것  StartCoroutine(이펙트 함수명(StateManager.Instance.useItemNum))
                //회복 이펙트를 확실히 넣을것

                StateManager.Instance.playHp += (StateManager.Instance.playHp * bItem.HpUp_Mul);

                StateManager.Instance.BufscrollNum[StateManager.Instance.useItemNum]--;
                if (StateManager.Instance.BufscrollNum[StateManager.Instance.useItemNum] == 0)
                {
                    DestroyObject(StateManager.Instance.BufScrollBag[StateManager.Instance.useItemNum]);
                }
                break;
        }
    }

    private void Potion()
    {
        StateManager.Instance.potionUse = false;
        switch (StateManager.Instance.useItemNum)
        {
            case 0:
                //하 회 물
                //코루틴으로 이펙트 넣을것  StartCoroutine(이펙트 함수명(StateManager.Instance.useItemNum))
                StateManager.Instance.playHp += pItem.UpPoint;

                StateManager.Instance.potionNum[StateManager.Instance.useItemNum]--;
                if (StateManager.Instance.potionNum[StateManager.Instance.useItemNum] == 0)
                {
                    DestroyObject(StateManager.Instance.potionItemBag[StateManager.Instance.useItemNum]);
                }
                break;
            case 1:
                //상 회 물
                //코루틴으로 이펙트 넣을것  StartCoroutine(이펙트 함수명(StateManager.Instance.useItemNum))
                StateManager.Instance.playHp += pItem.UpPoint;

                StateManager.Instance.potionNum[StateManager.Instance.useItemNum]--;
                if (StateManager.Instance.potionNum[StateManager.Instance.useItemNum] == 0)
                {
                    DestroyObject(StateManager.Instance.potionItemBag[StateManager.Instance.useItemNum]);
                }
                break;
            case 2:
                //공 증 물
                //코루틴으로 이펙트 넣을것  StartCoroutine(이펙트 함수명(StateManager.Instance.useItemNum))

                atkPotion.SetActive(true);
                playPotionAtk = pItem.UpPoint;
                atkPotionTurn = turn;

                StateManager.Instance.potionNum[StateManager.Instance.useItemNum]--;
                if (StateManager.Instance.potionNum[StateManager.Instance.useItemNum] == 0)
                {
                    DestroyObject(StateManager.Instance.potionItemBag[StateManager.Instance.useItemNum]);
                }
                break;
            case 3:
                //돌 물
                //코루틴으로 이펙트 넣을것  StartCoroutine(이펙트 함수명(StateManager.Instance.useItemNum))

                defPotion.SetActive(true);
                playPotionDef = pItem.UpPoint;
                defPotionTurn = turn;

                StateManager.Instance.potionNum[StateManager.Instance.useItemNum]--;
                if (StateManager.Instance.potionNum[StateManager.Instance.useItemNum] == 0)
                {
                    DestroyObject(StateManager.Instance.potionItemBag[StateManager.Instance.useItemNum]);
                }
                break;
            case 4:
                //속 물
                //코루틴으로 이펙트 넣을것  StartCoroutine(이펙트 함수명(StateManager.Instance.useItemNum))

                spdPotion.SetActive(true);
                playPotionSpd = pItem.UpPoint;
                spdPotionTurn = turn;
                pTimer2 -= pItem.UpPoint;

                StateManager.Instance.potionNum[StateManager.Instance.useItemNum]--;
                if (StateManager.Instance.potionNum[StateManager.Instance.useItemNum] == 0)
                {
                    DestroyObject(StateManager.Instance.potionItemBag[StateManager.Instance.useItemNum]);
                }
                break;
        }
    }
}
