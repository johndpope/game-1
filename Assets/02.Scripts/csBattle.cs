using UnityEngine;
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
    bool runFalse;
    bool runTrue;

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

    public GameObject finishPop;

    public GameObject cBox;
    public GameObject box1;
    public GameObject box2;
    public GameObject box3;

    public static int monsterGold;

    public GameObject finishGold;

    public GameObject poisonImage;
    public GameObject bleedingImage;
    public GameObject slowImage;

    float poisonDamage;
    float bleedingDamage;
    float slowDamage;

    int poisonTurn;
    int bleedingTurn;
    int slowTurn;

    int poisonNum;
    int bleedingNum;
    int slowNum;

    public GameObject fireBall;
    public GameObject iceBall;
    public GameObject earthBall;
    public GameObject flameStrike;

    Quaternion[] enemy = new Quaternion[3];

    public GameObject magicPos;
    public GameObject magicPosFlameStrike;

    bool playerMove;

    bool playerD;
    bool playerFinish;

    public GameObject gameOverText;
    public GameObject renewalGamePop;

    public GameObject playerAtk;
    public GameObject enemyAtk;

    public GameObject hardAtk;
    public GameObject spinAtk;
    public GameObject dashArk;
    public GameObject crossAtk;

    void OnEnable()
    {
        
        player2D.transform.FindChild("Lena").GetComponent<live2d_setting>().Ani(0);

        enemy[0] = new Quaternion(0, 0.8f, 0, 0.6f);
        enemy[1] = new Quaternion(0, 0.7f, 0, 0.7f);
        enemy[2] = new Quaternion(0, 0.6f, 0, 0.8f);

        weaponTextD = weaponText.GetComponent<Text>();
        if (StateManager.Instance.useWeapon != null)
        {
            useWeapon.GetComponent<Image>().sprite = StateManager.Instance.useWeapon;
            weaponTextD.text = "0";

            for (int j = 0; j < StateManager.Instance.weaponItems.Count; j++)
            {
                HMWeaponItem item = (HMWeaponItem)StateManager.Instance.weaponItems[j];
                for (int k = 0; k < StateManager.Instance.weaponSpace.Length; k++)
                {
                    // Debug.Log("무기 교체 들어옴");
                    if (StateManager.Instance.weaponSpace[StateManager.Instance.wUse].name.Equals((item.WeaponName + k.ToString())))
                    {
                        Debug.Log(StateManager.Instance.weaponSpace[StateManager.Instance.wUse].name);
                        Debug.Log("무기 교체 들어옴");
                        if (item.WeaponName.Equals("Weapon10"))
                        {
                            player2D.transform.FindChild("Lena").GetComponent<live2d_setting>().Weapon(0);
                        }
                        if (item.WeaponName.Equals("Weapon20"))
                        {
                            player2D.transform.FindChild("Lena").GetComponent<live2d_setting>().Weapon(1);
                        }
                        if (item.WeaponName.Equals("Weapon30"))
                        {
                            player2D.transform.FindChild("Lena").GetComponent<live2d_setting>().Weapon(2);
                        }
                        if (item.WeaponName.Equals("Weapon50"))
                        {
                            player2D.transform.FindChild("Lena").GetComponent<live2d_setting>().Weapon(3);
                            Debug.Log("무기 교체 들어옴");
                        }
                        if (item.WeaponName.Equals("Weapon55"))
                        {
                            player2D.transform.FindChild("Lena").GetComponent<live2d_setting>().Weapon(4);
                            Debug.Log("무기 교체 들어옴");
                        }
                        if (item.WeaponName.Equals("Weapon3"))
                        {
                            player2D.transform.FindChild("Lena").GetComponent<live2d_setting>().Weapon(5);
                        }
                    }
                }
            }
        }
        else
        {
            player2D.transform.FindChild("Lena").GetComponent<live2d_setting>().Weapon(6);
        }
        monsterGold = 0;
        poisonNum = 0;
        bleedingNum = 0;
        slowNum = 0;

        cBox.SetActive(false);
        box1.SetActive(true);
        box1.GetComponent<Button>().enabled = true;
        box2.SetActive(true);
        box2.GetComponent<Button>().enabled = true;
        box3.SetActive(true);
        box3.GetComponent<Button>().enabled = true;

        playPopAtkText = playPopAtk.GetComponent<Text>();
        playPopDefText = playPopDef.GetComponent<Text>();
        playPopSpdText = playPopSpd.GetComponent<Text>();
        turn = 0;
        pItems = StateManager.Instance.potionItems;
        sScroll = StateManager.Instance.skillScrollItems;
        mScroll = StateManager.Instance.magicScrollItems;
        bScroll = StateManager.Instance.buffScrollItems;

        attEnemyBool = false;
        enemySpd = 0.3f;
        enemyro = 5.0f;

        pTimer = StateManager.Instance.playSpd;
        //eTimer = StateManager.Instance.monsterSpd;

        battelCamera = battleCameraObj.GetComponent<Camera>();
        battleText = battleTextObj.GetComponent<Text>();

        monsterNum = StateManager.Instance.monsterNum;
        pTimer2 = pTimer;
        for (int i = 0; i < StateManager.Instance.monsterNum; i++)
        {
            eTimer2[i] = eTimer[i];
            Debug.Log(eTimer2[i]);
            Debug.Log(eTimer[i]);
        }

        player2D.transform.position = new Vector3(12.5f, 0, -30);
        player2D.transform.rotation = new Quaternion(0, -0.7f, 0, 0.7f);

        StartCoroutine(playerData());
    }
	
	void Update ()
    {
        sItem = (SkillItem)sScroll[StateManager.Instance.useItemNum];
        mItem = (MagicItem)mScroll[StateManager.Instance.useItemNum];
        bItem = (BuffItem)bScroll[StateManager.Instance.useItemNum];
        pItem = (PotionItem)pItems[StateManager.Instance.useItemNum];
        if (StateManager.Instance.useWeapon != null)
        {
            weaponTextD.text = StateManager.Instance.weaponDurability[StateManager.Instance.wUse].ToString();
        }

        if (damRock == 2)
        {
            pcc.value = 0;
            StateManager.Instance.timerIsActive = false;
            pop.SetActive(false);
            s = false;
            GameObject.Find("battleRock").transform.position = new Vector3(0, 0, 0);
            GameObject.Find("battleRock").SetActive(false);
            damRock = 0;
            StateManager.Instance.objBlocked = false;
            battelCamera.enabled = false;
            timer.SetActive(false);
            joystick.GetComponent<Image>().enabled = true;
            invenBtn.SetActive(true);
            player2D.transform.position = new Vector3(12.5f,0,-30);
            player2D.transform.rotation = new Quaternion(0, -0.7f, 0, 0.7f);
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
            StartCoroutine(finishC());
            playerFinish = true;
        }
        TimerCut();

        if (StateManager.Instance.playHp <= 0 && StateManager.Instance.monsterBattle.Equals(true))
        {
            StartCoroutine(finishC());
            playerD = true;
        }
        
    }

    IEnumerator finishC()
    {
        if(playerFinish.Equals(true))
        {
            StateManager.Instance.monsterBattle = false;
            yield return new WaitForSeconds(1.5f);
            player2D.transform.FindChild("Lena").GetComponent<live2d_setting>().Ani(5);
        }
        if(playerD.Equals(true))
        {
            StateManager.Instance.monsterBattle = false;
            playerD = false;
            player2D.transform.position = new Vector3(18, 0, -30);
            player2D.transform.FindChild("Lena").GetComponent<live2d_setting>().Ani(6);
            yield return new WaitForSeconds(1.5f);
            //게임 오버 띠우고
            gameOverText.SetActive(true);
            yield return new WaitForSeconds(1.5f);
<<<<<<< HEAD
            gameOverText.SetActive(false);
=======

>>>>>>> origin/master
            if (StateManager.Instance.potionNum[0] > 0 || StateManager.Instance.potionNum[1] > 0)
            {
                //포션 사용 해서 다시 할건지?
                renewalGamePop.SetActive(true);
            }
            else
            {
                GameObject.Find("Manager").GetComponent<csSaveLord>().SaveData();
                Application.LoadLevel(0);
            }
        }
        yield return new WaitForSeconds(1.5f);
        for (int i = 0; i < StateManager.Instance.monsterNum; i++)
        {
            eccObj[i].SetActive(true);
            ecc[i].value = 0;
            StateManager.Instance.monster[i].transform.FindChild("mo").GetComponent<main1>().ani(0);
            StateManager.Instance.monster[i].transform.position = new Vector3(0, 0, 0);
            StateManager.Instance.monster[i].SetActive(false);
        }

        StateManager.Instance.timerIsActive = false;
        timer.SetActive(false);

        if (playerFinish.Equals(true))
        {
            playerFinish = false;
            finish();
        }
    }

    public void renewalGameYes()
    {
        if(StateManager.Instance.potionNum[0] > 0)
        {
            StateManager.Instance.playHp += 20;
        }
        else if (StateManager.Instance.potionNum[1] > 0)
        {
            StateManager.Instance.playHp += 50;
        }
        renewalGamePop.SetActive(false);
        finishYes();
    }

    public void renewalGameNo()
    {
        GameObject.Find("Manager").GetComponent<csSaveLord>().SaveData();
        Application.LoadLevel(0);
    }

    private void finish()
    {
        monsterGold = (StateManager.Instance.slimeNum * 10)+ (StateManager.Instance.mimicNum* 15)+(StateManager.Instance.mimic2Num* 100)+(StateManager.Instance.ghostNum* 80)+(StateManager.Instance.pumkinNum* 50)+(StateManager.Instance.dungeonLevel * 50);
        finishPop.SetActive(true);
    }

    public void finishYes()
    {
        StateManager.Instance.slimeNum = 0;
        StateManager.Instance.mimicNum = 0;
        StateManager.Instance.mimic2Num = 0;
        StateManager.Instance.ghostNum = 0;
        StateManager.Instance.pumkinNum = 0;
        finishPop.SetActive(false);
        pop.SetActive(false);
        battelCamera.enabled = false;
        joystick.GetComponent<Image>().enabled = true;
        invenBtn.SetActive(true);
        player2D.transform.position = new Vector3(12.5f, 0, -30);
        player2D.transform.rotation = new Quaternion(0, -0.7f, 0, 0.7f);
        StateManager.Instance.monsterNum = 0;
        gameObject.SetActive(false);
    }

    IEnumerator playerData()
    {
        if (poisonImage.activeSelf.Equals(true) && (poisonTurn+1).Equals(turn))
        {
            poisonTurn++;
            poisonNum++;
            poisonDamage = StateManager.Instance.playHp * 0.1f;
            if(poisonDamage < 0)
            {
                poisonDamage = 1;
            }
            StateManager.Instance.playHp -= (int)poisonDamage;
            Debug.Log(StateManager.Instance.playHp + "독데미지 입은후");
            if(poisonNum.Equals(3))
            {
                poisonNum = 0;
                poisonImage.SetActive(false);
            }
        }

        if (bleedingImage.activeSelf.Equals(true) && (bleedingTurn + 1).Equals(turn))
        {
            bleedingTurn++;
            bleedingNum++;
            bleedingDamage = StateManager.Instance.playHp * 0.2f;
            if (bleedingDamage < 0)
            {
                bleedingDamage = 1;
            }
            StateManager.Instance.playHp -= (int)bleedingDamage;
            Debug.Log(StateManager.Instance.playHp + "출혈 데미지 입은후");
            if (poisonNum.Equals(3))
            {
                bleedingNum = 0;
                bleedingImage.SetActive(false);
            }
        }

        if (slowImage.activeSelf.Equals(true) && (bleedingTurn + 1).Equals(turn))
        {
            slowTurn++;
            slowNum++;
            slowDamage += 4;
           
            pTimer += slowDamage;

            if (slowNum.Equals(3))
            {
                slowDamage = 0;
                slowImage.SetActive(false);
            }
        }
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

            if (timer.activeSelf.Equals(true))
            {
                //돌과 싸울경우 시간 감소
                if (StateManager.Instance.objBlocked == true)
                {
                    pTimer -= Time.deltaTime * 10;
                    pcc.value += Time.deltaTime * 10 / pTimer2;

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
    public void run()
    {
        pcc.value = 0;
        runFalse = true;
        atkBtn.SetActive(false);
        skillBtn.SetActive(false);
        itemBtn.SetActive(false);
        runBtn.SetActive(false);
        StartCoroutine(RunM());
    }
    IEnumerator RunM()
    {
        if (runFalse.Equals(true))
        {
            battleText.text = "도 주  중 입 니 다.";
            battleTextObj.SetActive(true);
        }

        yield return new WaitForSeconds(1.0f);
        battleTextObj.SetActive(false);
        int num = Random.Range(1, 10);
        if (num.Equals(5))
        {
            battleText.text = "도 주 에  성 공 하 였 습 니 다.";
            battleTextObj.SetActive(true);
            yield return new WaitForSeconds(1.0f);
            battleTextObj.SetActive(false);
            for (int i = 0; i < StateManager.Instance.monsterNum; i++)
            {
                eccObj[i].SetActive(true);
                ecc[i].value = 0;
                StateManager.Instance.monster[i].transform.position = new Vector3(0, 0, 0);
                StateManager.Instance.monster[i].SetActive(false);
            }

            StateManager.Instance.timerIsActive = false;
            timer.SetActive(false);
            StateManager.Instance.slimeNum = 0;
            StateManager.Instance.mimicNum = 0;
            StateManager.Instance.mimic2Num = 0;
            StateManager.Instance.ghostNum = 0;
            StateManager.Instance.pumkinNum = 0;
            pop.SetActive(false);
            battelCamera.enabled = false;
            joystick.GetComponent<Image>().enabled = true;
            invenBtn.SetActive(true);
            player2D.transform.position = new Vector3(12.5f, 0, -30);
            player2D.transform.rotation = new Quaternion(0, -0.7f, 0, 0.7f);
            StateManager.Instance.monsterNum = 0;
            gameObject.SetActive(false);
        }
        else
        {
            battleText.text = "도 주 에  실 패 하 였 습 니 다.";
            battleTextObj.SetActive(true);
            yield return new WaitForSeconds(1.0f);
            battleTextObj.SetActive(false);
            pTimer = pTimer2;
            turn++;
            StateManager.Instance.timerIsActive = true;
            StateManager.Instance.monsterBattle = true;
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
        playerMove = true;
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
        
        if (StateManager.Instance.monster[num].name.Equals("Slime"))
        {
            int ran = Random.Range(1, 10);
            if (ran.Equals(4))
            {
                poisonTurn = turn;
                poisonImage.SetActive(true);
            }
        }
        if(StateManager.Instance.monster[num].name.Equals("Mimic")|| StateManager.Instance.monster[num].name.Equals("Mimic2"))
        {
            int ran = Random.Range(1, 10);
            if (ran.Equals(4))
            {
                bleedingTurn = turn;
                bleedingImage.SetActive(true);
            }
        }

        if (StateManager.Instance.monster[num].name.Equals("Ghost"))
        {
            int ran = Random.Range(1, 10);
            if (ran.Equals(4))
            {
                slowTurn = turn;
                slowImage.SetActive(true);
            }
        }

        if (StateManager.Instance.monster[num].name.Equals("Pumkin"))
        {
            int ran = Random.Range(1, 10);
            if (ran.Equals(4))
            {
                StateManager.Instance.playHp -= (StateManager.Instance.monsterAtk[num] - (StateManager.Instance.playDef + playPotionDef + playDef));
            }
        }

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

        StateManager.Instance.monster[num].transform.FindChild("mo").GetComponent<main1>().ani(1);
        yield return new WaitForSeconds(0.5f);
        GameObject enemyEff = Instantiate(enemyAtk) as GameObject;
        enemyEff.transform.position = new Vector3(StateManager.Instance.monster[num].transform.position.x-1, 1, StateManager.Instance.monster[num].transform.position.z);
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
            if (player2D.transform.position.x + 4 >= StateManager.Instance.monster[StateManager.Instance.atkEnemyNum].transform.position.x)//터치로 넘버값 받아오기
            {
               
                StartCoroutine(PlayerAtk());              
                StateManager.Instance.playerBattleBool = false;
            }

            else
            {
                StartCoroutine(PlayerMove());                
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

    IEnumerator PlayerMove()
    {
        if (playerMove.Equals(true))
        {
            player2D.transform.FindChild("Lena").GetComponent<live2d_setting>().Ani(4);
            playerMove = false;
        }
        yield return null;
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
        StartCoroutine(Magic());

        for (int i = 0; i < StateManager.Instance.monsterNum; i++)
        {

            if (StateManager.Instance.monster[i].GetComponent<BoxCollider>().Equals(false))
            {
                Debug.Log("들어옴");
                StateManager.Instance.monster[i].GetComponent<BoxCollider>().enabled = true;
            }
        }

        player2D.transform.FindChild("Lena").GetComponent<live2d_setting>().Ani(0);
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
                player2D.transform.FindChild("Lena").GetComponent<live2d_setting>().Ani(2);
                yield return new WaitForSeconds(0.5f);
                StartCoroutine(Skill());
                if (StateManager.Instance.monsterHp[StateManager.Instance.atkEnemyNum] <= 0)
                {
                    StateManager.Instance.monster[StateManager.Instance.atkEnemyNum].transform.FindChild("mo").GetComponent<main1>().ani(2);
                    eccObj[StateManager.Instance.atkEnemyNum].SetActive(false);
                    eTimer[StateManager.Instance.atkEnemyNum] = eTimer2[StateManager.Instance.atkEnemyNum];
                    monsterNum--;
                }
            }
            else
            {
                player2D.transform.FindChild("Lena").GetComponent<live2d_setting>().Ani(1);
                yield return new WaitForSeconds(0.5f);
                GameObject AtkEff = Instantiate(playerAtk) as GameObject;
                AtkEff.transform.position = new Vector3(player2D.transform.position.x + 3, 2, player2D.transform.position.z);
                StateManager.Instance.monsterHp[StateManager.Instance.atkEnemyNum] -= (StateManager.Instance.playAtk + StateManager.Instance.playUseAtk + playPotionAtk + playOriginAtk + playAtk + playBerserkerAtk) - StateManager.Instance.monsterDef[StateManager.Instance.atkEnemyNum];
                if (StateManager.Instance.monsterHp[StateManager.Instance.atkEnemyNum] <= 0)
                {
                    StateManager.Instance.monster[StateManager.Instance.atkEnemyNum].transform.FindChild("mo").GetComponent<main1>().ani(2);
                    eccObj[StateManager.Instance.atkEnemyNum].SetActive(false);
                    eTimer[StateManager.Instance.atkEnemyNum] = eTimer2[StateManager.Instance.atkEnemyNum];
                    monsterNum--;
                }
            }
        }
        else
        {
            if (StateManager.Instance.skillAtk.Equals(true))
            {
                player2D.transform.FindChild("Lena").GetComponent<live2d_setting>().Ani(2);
                yield return new WaitForSeconds(0.5f);
                StateManager.Instance.playHp -= 5;
                StartCoroutine(Skill());

                if (StateManager.Instance.monsterHp[StateManager.Instance.atkEnemyNum] <= 0)
                {
                    StateManager.Instance.monster[StateManager.Instance.atkEnemyNum].transform.FindChild("mo").GetComponent<main1>().ani(2);
                    eccObj[StateManager.Instance.atkEnemyNum].SetActive(false);
                    eTimer[StateManager.Instance.atkEnemyNum] = eTimer2[StateManager.Instance.atkEnemyNum];
                    monsterNum--;
                }
            }
            else
            {
                player2D.transform.FindChild("Lena").GetComponent<live2d_setting>().Ani(1);
                yield return new WaitForSeconds(0.5f);
                GameObject AtkEff = Instantiate(playerAtk) as GameObject;
                AtkEff.transform.position = new Vector3(player2D.transform.position.x + 3, 2, player2D.transform.position.z);
                StateManager.Instance.playHp -= 5;
                StateManager.Instance.monsterHp[StateManager.Instance.atkEnemyNum] -= (StateManager.Instance.playAtk + StateManager.Instance.playUseAtk + playPotionAtk + playOriginAtk + playAtk + playBerserkerAtk) - StateManager.Instance.monsterDef[StateManager.Instance.atkEnemyNum];

                if (StateManager.Instance.monsterHp[StateManager.Instance.atkEnemyNum] <= 0)
                {
                    StateManager.Instance.monster[StateManager.Instance.atkEnemyNum].transform.FindChild("mo").GetComponent<main1>().ani(2);
                    eccObj[StateManager.Instance.atkEnemyNum].SetActive(false);
                    eTimer[StateManager.Instance.atkEnemyNum] = eTimer2[StateManager.Instance.atkEnemyNum];
                    monsterNum--;
                }
            }
        }
        
        yield return new WaitForSeconds(1.5f);
        pTimer = pTimer2;
        turn++;
        StateManager.Instance.timerIsActive = true;
        StateManager.Instance.monsterBattle = true;
        player2D.transform.position = new Vector3(11.5f, 0, -30);
        player2D.transform.LookAt(playPos.transform.position);
        player2D.transform.rotation = new Quaternion(0,-0.7f, 0, 0.7f);
        player2D.transform.FindChild("Lena").GetComponent<live2d_setting>().Ani(0);
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

    IEnumerator Skill()
    {
        StateManager.Instance.skillAtk = false;
        switch (StateManager.Instance.useItemNum)
        {
            case 0:
                //강격
                Debug.Log(sItem.Name);
                //코루틴으로 이펙트 넣을것  StartCoroutine(이펙트 함수명(StateManager.Instance.useItemNum))
                GameObject HardAtk = Instantiate(hardAtk) as GameObject;
                HardAtk.transform.position = new Vector3(player2D.transform.position.x + 2, 2, player2D.transform.position.z);
                yield return new WaitForSeconds(2.0f);

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
                break;
            case 1:
                Debug.Log(sItem.Name);
                //회전배기 광역
                //코루틴으로 이펙트 넣을것  StartCoroutine(이펙트 함수명(StateManager.Instance.useItemNum))

                GameObject sAtk = Instantiate(spinAtk) as GameObject;
                sAtk.transform.position = new Vector3(player2D.transform.position.x + 2, 2, -30);
                yield return new WaitForSeconds(2.0f);

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
                GameObject cAtk = Instantiate(crossAtk) as GameObject;
                cAtk.transform.position = new Vector3(player2D.transform.position.x + 2, 2, player2D.transform.position.z);
                yield return new WaitForSeconds(2.0f);

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

    IEnumerator Magic()
    {
        StateManager.Instance.MagicAtk = false;
        player2D.transform.FindChild("Lena").GetComponent<live2d_setting>().Ani(3);
        switch (StateManager.Instance.useItemNum)
        {
            case 0:
                //얼음화살 슬로우
                //코루틴으로 이펙트 넣을것  StartCoroutine(이펙트 함수명(StateManager.Instance.useItemNum))
                for (int i = 0; i < StateManager.Instance.monsterNum; i++)
                {
                    StateManager.Instance.monster[i].GetComponent<BoxCollider>().enabled = false;
                }
                StateManager.Instance.monster[StateManager.Instance.atkEnemyNum].GetComponent<BoxCollider>().enabled = true;

                GameObject ice = Instantiate(iceBall) as GameObject;
                ice.transform.position = magicPos.transform.position;
                ice.transform.rotation = enemy[StateManager.Instance.atkEnemyNum];
                //ice.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                yield return new WaitForSeconds(2.0f);

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


                if (StateManager.Instance.monsterHp[StateManager.Instance.atkEnemyNum] <= 0)
                {
                    StateManager.Instance.monster[StateManager.Instance.atkEnemyNum].transform.FindChild("mo").GetComponent<main1>().ani(2);
                    monsterNum--;
                    eccObj[StateManager.Instance.atkEnemyNum].SetActive(false);
                    eTimer[StateManager.Instance.atkEnemyNum] = eTimer2[StateManager.Instance.atkEnemyNum];
                }
                break;
            case 1:
                //불화살
                //코루틴으로 이펙트 넣을것  StartCoroutine(이펙트 함수명(StateManager.Instance.useItemNum))
                for(int i=0; i<StateManager.Instance.monsterNum; i++)
                {
                    StateManager.Instance.monster[i].GetComponent<BoxCollider>().enabled = false;
                }
                StateManager.Instance.monster[StateManager.Instance.atkEnemyNum].GetComponent<BoxCollider>().enabled = true;

                GameObject fire = Instantiate(fireBall) as GameObject;
                fire.transform.position = magicPos.transform.position;
                fire.transform.rotation = enemy[StateManager.Instance.atkEnemyNum];
                //fire.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                yield return new WaitForSeconds(2.0f);
                if (StateManager.Instance.monsterHp[StateManager.Instance.atkEnemyNum] <= 0)
                {
                    StateManager.Instance.monster[StateManager.Instance.atkEnemyNum].transform.FindChild("mo").GetComponent<main1>().ani(2);
                    monsterNum--;
                    eccObj[StateManager.Instance.atkEnemyNum].SetActive(false);
                }
                StateManager.Instance.monsterHp[StateManager.Instance.atkEnemyNum] -= ((StateManager.Instance.playAtk + StateManager.Instance.playUseAtk + playPotionAtk + playAtk + playBerserkerAtk + playOriginAtk) * mItem.AttactPoint) - StateManager.Instance.monsterDef[StateManager.Instance.atkEnemyNum];

                StateManager.Instance.MgscrollNum[StateManager.Instance.useItemNum]--;
                if (StateManager.Instance.MgscrollNum[StateManager.Instance.useItemNum] == 0)
                {
                    DestroyObject(StateManager.Instance.MgScrollBag[StateManager.Instance.useItemNum]);
                }


                if (StateManager.Instance.monsterHp[StateManager.Instance.atkEnemyNum] <= 0)
                {
                    StateManager.Instance.monster[StateManager.Instance.atkEnemyNum].transform.FindChild("mo").GetComponent<main1>().ani(2);
                    monsterNum--;
                    eccObj[StateManager.Instance.atkEnemyNum].SetActive(false);
                    eTimer[StateManager.Instance.atkEnemyNum] = eTimer2[StateManager.Instance.atkEnemyNum];
                }
                break;
            case 2:
                //불기둥
                //코루틴으로 이펙트 넣을것  StartCoroutine(이펙트 함수명(StateManager.Instance.useItemNum))
                GameObject strike = Instantiate(flameStrike) as GameObject;
                strike.transform.position = magicPosFlameStrike.transform.position;
                strike.transform.rotation = enemy[StateManager.Instance.atkEnemyNum];
                //fire.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                yield return new WaitForSeconds(2.0f);

                for (int i = 0; i < StateManager.Instance.monsterNum; i++)
                {
                    StateManager.Instance.monsterHp[i] -= ((StateManager.Instance.playAtk + StateManager.Instance.playUseAtk + playPotionAtk + playAtk + playBerserkerAtk + playOriginAtk) * mItem.AttactPoint) - StateManager.Instance.monsterDef[i];

                    if (StateManager.Instance.monsterHp[i] <= 0)
                    {
                        Debug.Log("들어옴");
                        StateManager.Instance.monster[i].transform.FindChild("mo").GetComponent<main1>().ani(2);
                        monsterNum--;
                        eccObj[i].SetActive(false);
                        eTimer[i] = eTimer2[i];
                    }
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

                for (int i = 0; i < StateManager.Instance.monsterNum; i++)
                {
                    StateManager.Instance.monster[i].GetComponent<BoxCollider>().enabled = false;
                }
                StateManager.Instance.monster[StateManager.Instance.atkEnemyNum].GetComponent<BoxCollider>().enabled = true;

                GameObject earth = Instantiate(earthBall) as GameObject;
                earth.transform.position = magicPos.transform.position;
                earth.transform.rotation = enemy[StateManager.Instance.atkEnemyNum];
                //earth.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                yield return new WaitForSeconds(2.0f);
               
                StateManager.Instance.monster[StateManager.Instance.atkEnemyNum].transform.FindChild("atkdown").GetComponent<SpriteRenderer>().enabled = true;
                StateManager.Instance.monster[StateManager.Instance.atkEnemyNum].transform.FindChild("atkdown").GetComponent<csAtkDown>().enabled = true;

                StateManager.Instance.monsterAtk[StateManager.Instance.atkEnemyNum] = StateManager.Instance.monsterAtk[StateManager.Instance.atkEnemyNum] * mItem.AtkDownPoint;
                StateManager.Instance.monsterHp[StateManager.Instance.atkEnemyNum] -= ((StateManager.Instance.playAtk + StateManager.Instance.playUseAtk + playPotionAtk + playAtk + playBerserkerAtk + playOriginAtk) * mItem.AttactPoint) - StateManager.Instance.monsterDef[StateManager.Instance.atkEnemyNum];

                StateManager.Instance.MgscrollNum[StateManager.Instance.useItemNum]--;
                if (StateManager.Instance.MgscrollNum[StateManager.Instance.useItemNum] == 0)
                {
                    DestroyObject(StateManager.Instance.MgScrollBag[StateManager.Instance.useItemNum]);
                }

                if (StateManager.Instance.monsterHp[StateManager.Instance.atkEnemyNum] <= 0)
                {
                    StateManager.Instance.monster[StateManager.Instance.atkEnemyNum].transform.FindChild("mo").GetComponent<main1>().ani(2);
                    monsterNum--;
                    eccObj[StateManager.Instance.atkEnemyNum].SetActive(false);
                    eTimer[StateManager.Instance.atkEnemyNum] = eTimer2[StateManager.Instance.atkEnemyNum];
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
                if(StateManager.Instance.playHpMax < (StateManager.Instance.playHp += (StateManager.Instance.playHp * bItem.HpUp_Mul)))
                {
                    StateManager.Instance.playHp = StateManager.Instance.playHpMax;
                }
                else
                {
                    StateManager.Instance.playHp += (StateManager.Instance.playHp * bItem.HpUp_Mul);
                }

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
                if(StateManager.Instance.playHpMax < (StateManager.Instance.playHp += pItem.UpPoint))
                {
                    StateManager.Instance.playHp = StateManager.Instance.playHpMax;
                }
                else
                {
                    StateManager.Instance.playHp += pItem.UpPoint;
                }

                StateManager.Instance.potionNum[StateManager.Instance.useItemNum]--;
                if (StateManager.Instance.potionNum[StateManager.Instance.useItemNum] == 0)
                {
                    DestroyObject(StateManager.Instance.potionItemBag[StateManager.Instance.useItemNum]);
                }
                break;
            case 1:
                //상 회 물
                //코루틴으로 이펙트 넣을것  StartCoroutine(이펙트 함수명(StateManager.Instance.useItemNum))
                if (StateManager.Instance.playHpMax < (StateManager.Instance.playHp += pItem.UpPoint))
                {
                    StateManager.Instance.playHp = StateManager.Instance.playHpMax;
                }
                else
                {
                    StateManager.Instance.playHp += pItem.UpPoint;
                }

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
