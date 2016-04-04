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
   
    //json에서 읽어온 데이터를 저장하는 아이템과 스크롤 ArryList
    public ArrayList potionItems;
    public ArrayList skillScrollItems;
    public ArrayList magicScrollItems;
    public ArrayList buffScrollItems;

    //몇개나 구입했는지 저장하는 integer형 배열
    public int[] potionNum;
    public int[] SkscrollNum;
    public int[] MgscrollNum;
    public int[] BufscrollNum;

    //구입한 아이템이나 스크롤을 저장하는 GameObject형 배열
    public GameObject[] potionItemBag;
    public GameObject[] SkScrollBag;
    public GameObject[] MgScrollBag;
    public GameObject[] BufScrollBag;

    //json에서 읽어온 데이터를 저장하는 장비 ArryList
    public ArrayList weaponItems;
    public ArrayList armorItems;
    public ArrayList bootItems;
    //구입과 사용한 장비를 저장하는 GameObject형 배열
    public GameObject[] weaponSpace = new GameObject[5];
   
    //가방의 크기
    public int bagSize;

    //플레이어 어빌리티 데이터 저장하는 곳
    public int playGold;
    public int playHp;
    public int playAtk;
    public int playDef;
    public float playSpd;

    //현재 착용중인 장비 능력치를 저장하는곳
    public int playUseAtk;
    public int playUseDef;
    public float playUseSpd;

    //다른곳에서 사용할때.
    //StateManager.Instance.hp = 100;

    //장비의 배열 위치 값을 잠시 저장하는 임시 저장소
    public int bagNum;

    //무기의 내구도를 저장하는 integer형 배열
    public int[] weaponDurability = new int[10];

    public int dungeonMap;
    public int dungeonLevel;

    //멤버 함수
    //public void SetState(int state)
    //{
    //    State = state;
    //}

}


