using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class csBattle : MonoBehaviour 
{

    public Scrollbar pcc;
    public Scrollbar ecc;
    public float pTimer = 10;
    public float eTimer = 10;
    float pTimer2;
    float eTimer2;

    bool s;
  
    public GameObject atkbtn;
    public GameObject runbtn;
    public GameObject player;
    
    public GameObject timer;

    public GameObject playPos;

    private int damRock;

    public GameObject battleCameraObj;
    Camera battelCamera = new Camera();

    void Start ()
    {
        pTimer2 = pTimer;
        eTimer2 = eTimer;
        battelCamera = battleCameraObj.GetComponent<Camera>();
    }
	
	void Update ()
    {
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
            player.transform.position = StateManager.Instance.playerPos;
            battelCamera.enabled = false;
            timer.SetActive(false);
        }
    }

    public void TimerCut()
    {
        if (s == true)
        {
            player.transform.Translate(0, 0, -0.2f);
            if (player.transform.position.z <= -27.0f)
            {
                player.transform.position = playPos.transform.position;
                s = false;
                pcc.value = 0;
                damRock++;
                StateManager.Instance.timerIsActive = true;
            }

        }

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

                if(StateManager.Instance.monsterBattle==true)
                {
                    pTimer -= Time.deltaTime;
                    pcc.value += Time.deltaTime / pTimer2;

                    eTimer -= Time.deltaTime;
                    ecc.value += Time.deltaTime / eTimer2;

                    if (pTimer <= 0)
                    {
                        pTimer = 0;
                        atkbtn.SetActive(true);
                        runbtn.SetActive(true);
                        StateManager.Instance.timerIsActive = false;
                    }
                }
            }
        }
    }

    public void atk()
    {
        pcc.value = 0;
        atkbtn.SetActive(false);
        runbtn.SetActive(false);
        StateManager.Instance.timerIsActive = true;
        pTimer = pTimer2;
    }

    private void ObjBreak()
    {
        player.transform.Translate(0, 0, 0.2f);

        if(player.transform.position.z >= -21.0f)
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
}
