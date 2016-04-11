using UnityEngine;
using System.Collections;


public class csText : MonoBehaviour
{
    public GameObject battleCameraObj;
    Camera battelCamera = new Camera();

    public GameObject battlePop;
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
            if (hitObj.transform.tag.Equals("enemy"))
            {
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

                    if(StateManager.Instance.useItemNum == 1)
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
                }
            }
        }
    }

    public void battelYes()
    {
        if(StateManager.Instance.normalAtk == true)
        {
            StateManager.Instance.playerBattleBool = true;
        }
        if (StateManager.Instance.normalAtk == true)
        {
            StateManager.Instance.useItemAtkBool = true;
            StateManager.Instance.playerBattleBool = true;
        }

        particle.Stop();
        battlePop.SetActive(false);

        if (StateManager.Instance.useItemNum == 1)
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
    }
    public void battelNo()
    {
        particle.Stop();
        gameObject.SetActive(true);
        battlePop.SetActive(false);
    }
}


 

