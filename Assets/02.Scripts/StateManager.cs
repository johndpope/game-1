using UnityEngine;
using System.Collections;

public class StateManager : MonoBehaviour
{
    // 싱글턴 클래스를 구현한다.
    static StateManager instance; // private static 으로 선언 ( 중요 ).

    void Awake()
    {
        if (startButton.Equals(false))
        {
            firstGameNum = PlayerPrefs.GetInt("firstGame");
            if (firstGameNum.Equals(1))
            {
                firstGame = true;
            }

            playGold = PlayerPrefs.GetInt("playGold");
            playHp = PlayerPrefs.GetFloat("playHp");
            playHpMax = PlayerPrefs.GetFloat("playHpMax");
            playAtk = PlayerPrefs.GetFloat("playAtk");
            playDef = PlayerPrefs.GetFloat("playDef");
            playSpd = PlayerPrefs.GetFloat("playSpd");

            bagSize = PlayerPrefs.GetInt("bagSize");

            cntHp = PlayerPrefs.GetInt("cntHp");
            cntAtk = PlayerPrefs.GetInt("cntAtk");
            cntDef = PlayerPrefs.GetInt("cntDef");
            cntSpd = PlayerPrefs.GetInt("cntSpd");

            playUseAtk = PlayerPrefs.GetInt("playUseAtk");
            playUseDef = PlayerPrefs.GetInt("playUseDef");
            playUseSpd = PlayerPrefs.GetFloat("playUseSpd");
        }
        DontDestroyOnLoad(gameObject);
    }

    public static StateManager Instance  // public static 으로 선언 ( 중요 ).
    {
        get  // set 이 아닌 get 이다 !
        {
            if (instance == null)
            {
                // 현재 씬 내에서 GameManager 컴포넌트를 검색
                instance = FindObjectOfType(typeof(StateManager)) as StateManager;

                if (instance == null)
                {
                    // 현재 씬에 GameManager 컴포넌트가 없으면 새로 생성
                    instance = new GameObject("Game Manager", typeof(StateManager)).GetComponent<StateManager>();
                }
            }
          

            return instance;
        }
    }

    

    // 멤버 변수

    //json에서 읽어온 데이터를 저장하는 곳 아이템과 스크롤 ArryList
    public ArrayList potionItems;
    public ArrayList skillScrollItems;
    public ArrayList magicScrollItems;
    public ArrayList buffScrollItems;

    //json에서 읽어온 데이터를 저장하는 던전레벨을 ArryList
    public ArrayList dungeonLevels;
    public ArrayList dungeonMonsters;

    //public GameObject[] weaponItemGet;

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
    public float playHp;
    public float playHpMax;
    public float playAtk;
    public float playDef;
    public float playSpd;

    public int cntHp;
    public int cntAtk;
    public int cntDef;
    public int cntSpd;

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
    public int wUse;

    public int dungeonMap;
    public int dungeonLevel;

    //배틀 타이머 작동 bool값
    public bool timerIsActive;

    //길 막는 오브젝트와 전투 판단값
    public bool objBlocked;
    //몬스터와 전투 판단값
    public bool monsterBattle;

    //public Vector3 playerPos;

    public GameObject dText;

    //생성몬스터 저장 게임오브젝트 배열
    public GameObject[] monster;
    public GameObject[] slime;
    public GameObject[] mimic;
    public GameObject[] mimic2;
    public GameObject[] ghost;
    public GameObject[] pumkin;
    public GameObject[] drake;

    public int slimeNum;
    public int mimicNum;
    public int mimic2Num;
    public int ghostNum;
    public int pumkinNum;
    public int drakeNum;

    public float[] monsterHp = new float[3];
    public float[] monsterAtk = new float[3];
    public float[] monsterDef = new float[3];
    public float[] monsterSpd = new float[3];

    public int monsterNum;

    public float playerPos = -31.0f;

    //멤버 함수
    //public void SetState(int state)
    //{
    //    State = state;
    //}
    //public bool attEnemyBool;

    //public bool enemyAtt;

    public bool playerBattleBool;
    public bool playerMagicBool;
    public bool playerbuffBool;
    public bool playerPotionBool;

    public int atkEnemyNum;

    public int useItemNum;
    public bool useItemBool;
    public string useItemName;

    public bool normalAtk;
    public bool scrollAtk;
    public bool potionUse;

    public bool skillAtk;
    public bool MagicAtk;
    public bool buffUse;

    public Sprite useWeapon;
    public Sprite useArmor;
    public Sprite useBoots;

    public void attEnemy()
    {
        //attEnemyBool = true;
    }

    public bool dunFinish;

    public bool startButton;

    public bool firstGame;
    public int firstGameNum;

   
}


