using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class csBattle : MonoBehaviour 
{

    public Scrollbar pcc;
    public GameObject[] eccObj = new GameObject[3];
    public Scrollbar[] ecc = new Scrollbar[3];

    public float pTimer = 10;
    public static float[] eTimer = new float[3];

    float pTimer2;
    float[] eTimer2 = new float[3];

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
    Monster slime;

    public GameObject touchEvent;
    //공격시 보이는 텍스트
    public GameObject battleTextObj;
    Text battleText;

    void Start()
    {
        attEnemyBool = false;
        enemySpd = 0.1f;
        enemyro = 5.0f;

        pTimer2 = pTimer;
        battelCamera = battleCameraObj.GetComponent<Camera>();
        battleText = battleTextObj.GetComponent<Text>();
    }
	
	void Update ()
    {

        slime = (Monster)StateManager.Instance.dungeonMonsters[0];

        TimerCut();
      
        if(damRock == 2)
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
    }

    public void TimerCut()
    {
        if (StateManager.Instance.timerIsActive == true)
        {
            timer.SetActive(true);

            if(timer.activeSelf == true)
            {
                //돌과 싸울경우 시간 감소
                if (StateManager.Instance.objBlocked == true)
                {
                    pTimer -= Time.deltaTime;
                    pcc.value += Time.deltaTime / pTimer2;

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
                        if (StateManager.Instance.monster[i] == null)
                        {
                            i++;
                            if(i >= StateManager.Instance.monsterNum)
                            {
                                return;
                            }
                            Debug.Log(i + "              전투 끝");
                        }
                        eTimer[i] -= Time.deltaTime;
                        ecc[i].value += Time.deltaTime / (eTimer[i]*2);

                        

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

    public void atk()
    {
        pcc.value = 0;
        atkBtn.SetActive(false);
        runBtn.SetActive(false);

        StartCoroutine(BattleText());

        touchEvent.SetActive(true);

       
        pTimer = pTimer2;
    }

    private void ObjBreak()
    {
        player2D.transform.Translate(0, 0, 0.2f);

        if(player2D.transform.position.z >= -21.0f)
        {
            pTimer = 0;            
            StateManager.Instance.timerIsActive = false;
            pTimer = pTimer2;
            StartCoroutine("PlayPos");
            
        }
    }
    IEnumerator PlayPos()
    {
        if(StateManager.Instance.weaponSpace[StateManager.Instance.wUse] != null)
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
        if (StateManager.Instance.monster[num].name == slime.Name + num && ecc[num].value != 0)
        {
            if (slime.MonsterAtt - StateManager.Instance.playDef > 0)
            {
                StateManager.Instance.playHp -= slime.MonsterAtt - StateManager.Instance.playDef;
                Debug.Log(StateManager.Instance.playHp);
            }
   
        }
        StateManager.Instance.monster[num].transform.FindChild("mo").GetComponent<main1>().ani(1);
        yield return new WaitForSeconds(1.5f);
        enemyPos(num);
    }

    private void enemyPos(int num)
    {
        if (StateManager.Instance.monster[num].name == slime.Name + num)
        {
            eTimer[num] = Random.Range(slime.MonsterMinSpd, slime.MonsterMaxSpd + 1);
            ecc[num].value = 0;
        }
        StateManager.Instance.monster[num].transform.position = battlePos[num].transform.position;
        StateManager.Instance.monster[num].transform.FindChild("mo").GetComponent<main1>().ani(0);
        StateManager.Instance.monsterBattle = true;
    }


    private void PlayerBattle()
    {
        if (StateManager.Instance.playerBattleBool == true)
        {     
            if (player2D.transform.position.x + 2 >= StateManager.Instance.monster[StateManager.Instance.atkEnemyNum].transform.position.x)//터치로 넘버값 받아오기
            { 
                //player2D.transform.LookAt(StateManager.Instance.monster[StateManager.Instance.atkEnemyNum].transform.position);
                //player2D.transform.rotation = new Quaternion(0, 270, 0, 0);
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
    }

    IEnumerator PlayerAtk()
    {
        if (StateManager.Instance.weaponSpace[StateManager.Instance.wUse] != null)
        {
            StateManager.Instance.weaponDurability[StateManager.Instance.wUse]--;
            StateManager.Instance.dText.GetComponent<Text>().text = StateManager.Instance.weaponDurability[StateManager.Instance.wUse].ToString();
            StateManager.Instance.weaponSpace[StateManager.Instance.wUse].transform.FindChild("weaponDurabilityText").GetComponent<Text>().text = "내구도: " + StateManager.Instance.weaponDurability[StateManager.Instance.wUse].ToString();
            StateManager.Instance.monsterHp[StateManager.Instance.atkEnemyNum] -= (StateManager.Instance.playAtk + StateManager.Instance.playUseAtk);
            if (StateManager.Instance.monsterHp[StateManager.Instance.atkEnemyNum]<=0)
            {
                eccObj[StateManager.Instance.atkEnemyNum].SetActive(false);
                DestroyObject(StateManager.Instance.monster[StateManager.Instance.atkEnemyNum]);
            }
        }
        else
        {
            StateManager.Instance.playHp -= 5;
            StateManager.Instance.monsterHp[StateManager.Instance.atkEnemyNum] -= (StateManager.Instance.playAtk + StateManager.Instance.playUseAtk);
            if (StateManager.Instance.monsterHp[StateManager.Instance.atkEnemyNum] <= 0)
            {
                eccObj[StateManager.Instance.atkEnemyNum].SetActive(false);
                DestroyObject(StateManager.Instance.monster[StateManager.Instance.atkEnemyNum]);
            }
            Debug.Log(StateManager.Instance.playHp);
        }
        //StateManager.Instance.playerBattleBool = false;
        yield return new WaitForSeconds(1.0f);
        StateManager.Instance.timerIsActive = true;
        StateManager.Instance.monsterBattle = true;
        
        Debug.Log("전투 끝");
        Debug.Log(player2D.transform.rotation);
        player2D.transform.position = new Vector3(playPos.transform.position.x - 1.5f, playPos.transform.position.y, playPos.transform.position.z);
        Debug.Log(player2D.transform.rotation);
        player2D.transform.LookAt(playPos.transform.position);
        Debug.Log(player2D.transform.rotation);
        player2D.transform.rotation = new Quaternion(0,-0.5f, 0, 0.8f);
        Debug.Log(player2D.transform.rotation);
    }

    IEnumerator BattleText()
    {
        battleTextObj.SetActive(true);
        battleText.text = "일 반 공 격  할  \n 적 을  클 릭 하 세 요.";
        yield return new WaitForSeconds(1.0f);
        battleTextObj.SetActive(false);
    }
}
