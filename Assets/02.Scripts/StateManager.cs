using UnityEngine;
using System.Collections;

public class StateManager : MonoBehaviour
{
    // 싱글턴 클래스를 구현한다.
    private static StateManager instance; // private static 으로 선언 ( 중요 ).

    public static StateManager Instance  // public static 으로 선언 ( 중요 ).
    {
        get  // set 이 아닌 get 이다 !
        {
            if (instance == null)
            {
                Debug.LogError("StateManager == null");
            }
         
            return instance;
        }
    }

    void Awake()
    {
        instance = this;
        DontDestroyOnLoad(transform.gameObject);
    }

    // 멤버 변수
    public int State;
    
    public GameObject[] weaponSpace = new GameObject[5];
    public int weaponATKUse;
    public GameObject[] bag = new GameObject[10];
    public int bagSize;

    public int playGold;
    public int playHp;
    public int playAtk;
    public int playDef;
    public double playSpd;
    
    

    //다른곳에서 사용할때.
    //StateManager.Instance.hp = 100;

    public int bagNum;
    
    public int[] weaponDurability = new int[10];

    //멤버 함수
    public void SetState(int state)
    {
        State = state;
    }

}


