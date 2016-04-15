using UnityEngine;
using System.Collections;


public class csText : MonoBehaviour
{
    public GameObject atkBtn;
    public GameObject skillBtn;
    public GameObject itemBtn;
    public GameObject runBtn;

    public GameObject battleCameraObj;
    Camera battelCamera = new Camera();

    public GameObject battlePop;
    public GameObject buffPop;

    ParticleSystem particle;
    GameObject pos;
    // Use this for initialization
    void Start()
    {
        battelCamera = battleCameraObj.GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        // 현재 터치되어 있는 카운트 가져오기
        int cnt = Input.touchCount;

        //		Debug.Log( "touch Cnt : " + cnt );

        // 동시에 여러곳을 터치 할 수 있기 때문.
        for (int i = 0; i < cnt; ++i)
        {
            // i 번째로 터치된 값 이라고 보면 된다. 
            Touch touch = Input.GetTouch(i);
            Vector2 pos = touch.position;

            if (touch.phase == TouchPhase.Began)
            {

            }
            else if (touch.phase == TouchPhase.Ended)
            {
                Ray ray = battelCamera.ScreenPointToRay(pos);
                rayCasting(ray);
              
            }
        }
    }

    void rayCasting(Ray ray)
    {
        RaycastHit hitObj;
        if(Physics.Raycast(ray, out hitObj, Mathf.Infinity))
        {
            if(hitObj.transform.tag.Equals("Player"))
            {
                Debug.Log("플레이어가 터치 되었습니다.");
                particle = hitObj.transform.FindChild("ring").GetComponent<ParticleSystem>();
                particle.Play();
                buffPop.SetActive(true);
                gameObject.SetActive(false);
            }

            if (hitObj.transform.tag.Equals("enemy"))
            {
                if (StateManager.Instance.buffUse.Equals(true)|| StateManager.Instance.potionUse.Equals(true))
                {
                    return;
                }
                Debug.Log("몬스터가 터지 되었습니다");
                for (int i = 0; i < StateManager.Instance.monsterNum; i++)
                {
                    if (hitObj.transform.name == "Slime" + i)
                    {
                        StateManager.Instance.atkEnemyNum = i;
                        particle = hitObj.transform.FindChild("ring").GetComponent<ParticleSystem>();
                        particle.Play();
                        battlePop.SetActive(true);
                        gameObject.SetActive(false);
                    }
                    if (hitObj.transform.name == "Mimic" + i)
                    {
                        StateManager.Instance.atkEnemyNum = i;
                        particle = hitObj.transform.FindChild("ring").GetComponent<ParticleSystem>();
                        particle.Play();
                        battlePop.SetActive(true);
                        gameObject.SetActive(false);
                    }

                    if (StateManager.Instance.useItemNum == 1 && StateManager.Instance.skillAtk.Equals(true))
                    {
                        for (int j = 0; j < StateManager.Instance.monsterNum; j++)
                        {
                            if(StateManager.Instance.monster[j] == null)
                            {
                                return;
                            }
                            StateManager.Instance.monster[j].transform.FindChild("ring").GetComponent<ParticleSystem>().Play();
                        }
                        battlePop.SetActive(true);
                        gameObject.SetActive(false);
                    }

                    if(StateManager.Instance.useItemNum == 2 && StateManager.Instance.MagicAtk.Equals(true))
                    {
                        for (int j = 0; j < StateManager.Instance.monsterNum; j++)
                        {
                            if (StateManager.Instance.monster[j] == null)
                            {
                                return;
                            }
                            StateManager.Instance.monster[j].transform.FindChild("ring").GetComponent<ParticleSystem>().Play();
                        }
                        battlePop.SetActive(true);
                        gameObject.SetActive(false);
                    }
                }
            }
        }
    }

    public void battelYes()
    {
        if (StateManager.Instance.useItemNum == 1 && StateManager.Instance.skillAtk.Equals(true))
        {
            for (int j = 0; j < StateManager.Instance.monsterNum; j++)
            {
                if (StateManager.Instance.monster[j] == null)
                {
                    return;
                }
                StateManager.Instance.monster[j].transform.FindChild("ring").GetComponent<ParticleSystem>().Stop();
            }
        }
        if (StateManager.Instance.useItemNum == 2 && StateManager.Instance.MagicAtk.Equals(true))
        {
            for (int j = 0; j < StateManager.Instance.monsterNum; j++)
            {
                if (StateManager.Instance.monster[j] == null)
                {
                    return;
                }
                StateManager.Instance.monster[j].transform.FindChild("ring").GetComponent<ParticleSystem>().Stop();
            }
        }
        if (StateManager.Instance.normalAtk.Equals(true))
        {
            StateManager.Instance.playerBattleBool = true;
            StateManager.Instance.normalAtk = false;
        }

        if (StateManager.Instance.scrollAtk.Equals(true))
        {
            //StateManager.Instance.useItemAtkBool = true;
            if (StateManager.Instance.skillAtk.Equals(true))
            {
                StateManager.Instance.playerBattleBool = true;
                StateManager.Instance.scrollAtk = false;
            }
            if(StateManager.Instance.MagicAtk.Equals(true))
            {
                StateManager.Instance.MagicAtk = false;
                StateManager.Instance.playerMagicBool = true;
            }
            if (StateManager.Instance.buffUse.Equals(true))
            {
                StateManager.Instance.buffUse = false;
                StateManager.Instance.playerbuffBool = true;
            }

        }
        if(StateManager.Instance.potionUse.Equals(true))
        {
            StateManager.Instance.playerPotionBool = true;
            StateManager.Instance.potionUse = false;
        }
        particle.Stop();
        buffPop.SetActive(false);
        battlePop.SetActive(false);       
    }

    public void battelNo()
    {
        if (StateManager.Instance.useItemNum == 1 && StateManager.Instance.skillAtk.Equals(true))
        {
            for (int j = 0; j < StateManager.Instance.monsterNum; j++)
            {
                if (StateManager.Instance.monster[j] == null)
                {
                    return;
                }
                StateManager.Instance.monster[j].transform.FindChild("ring").GetComponent<ParticleSystem>().Stop();
            }
        }

        if (StateManager.Instance.useItemNum == 2 && StateManager.Instance.MagicAtk.Equals(true))
        {
            for (int j = 0; j < StateManager.Instance.monsterNum; j++)
            {
                if (StateManager.Instance.monster[j] == null)
                {
                    return;
                }
                StateManager.Instance.monster[j].transform.FindChild("ring").GetComponent<ParticleSystem>().Stop();
            }
        }

        particle.Stop();
        //gameObject.SetActive(true);
        buffPop.SetActive(false);
        battlePop.SetActive(false);
        atkBtn.SetActive(true);
        skillBtn.SetActive(true);
        itemBtn.SetActive(true);
        runBtn.SetActive(true);
        StateManager.Instance.normalAtk = false;
        StateManager.Instance.scrollAtk = false;
        StateManager.Instance.skillAtk = false;
        StateManager.Instance.MagicAtk = false;
        StateManager.Instance.buffUse = false;
        StateManager.Instance.potionUse = false;
    }
}


 

