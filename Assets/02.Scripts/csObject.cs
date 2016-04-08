using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class csObject : MonoBehaviour
{
    //아이템 구입시 인벤토리 셋팅 오브젝트
    public GameObject itemUseGrid;  //아이템 그리드
    public GameObject skillUseGrid; //스킬 그리드

    //아이템 인벤토리 셋팅
    public GameObject itemUseSetObj;
    public GameObject itemUseName;
    public GameObject itemUseExplain;
    public GameObject itemUseImage;

    Text itemUseNameText;
    Text itemUseExplainText;

    GameObject itemUse = null;

    //인벤토리 무기 셋팅
    public GameObject WeaponUse;
    public GameObject weaponNameText;
    public GameObject weaponDurabilityText;
    public GameObject weaponImage;
    GameObject weaponSetObj = null;

    public GameObject weaponGrid;

    //정면에 돌을 발견했을때 나오는 Text Object
    public GameObject findText;
    Text fText; //위 Object의 Text를 변경하기 위한 Txet변수

    //돌과 부딧쳤을때 나오는 팝업
    public GameObject breakRockPop;
    //상자와 부딧쳤을때 나오는 팝업
    public GameObject openTreasurePop;

    //돌의 위치값을 저장
    Transform rockTransform;

    //전투하는곳의 player위치(기즈모 포인트)
    public GameObject battlePlayerPos;
    //전투하는곳의 Rock과 몬스터의 위치(기즈모 포인트)
    public GameObject[] battlePos;
    

    //맵에서 부딧친 돌의 GameObject를 저장하는 GameObject저장소
    GameObject mapRock;
    //전투에 사용될 돌을 생성하는 프리팹
    public GameObject battleRock;
    //전투에 사용되는 돌을 저장하는 클론
    GameObject gameObj;
    //전투시 켜지는 카메라
    public GameObject battleCameraObj;
    Camera battelCamera = new Camera();

    public int wValue = 1;  //무기확률
    public int pValue = 1;  //포션(소모품)확률
    public int sValue = 1;  //스킬 마법 버프 스크롤 확률
    public int moValue = 2; //몬스터 확률
    public int nValue = 3;  //꽝
    public int moneyValue = 5;  //돈

    //public

    //무기확률5%, 포션5%, 스크롤(스킬,마법,버프)5%, 몬스터15%, 꽝10%, 돈50%

    int num;
    void Start()
    {
        fText = findText.GetComponent<Text>();
        battelCamera = battleCameraObj.GetComponent<Camera>();

        gameObj = Instantiate(battleRock) as GameObject;
        gameObj.name = "battleRock";
        gameObj.SetActive(false);

        itemUseNameText = itemUseName.GetComponent<Text>();
        itemUseExplainText = itemUseExplain.GetComponent<Text>();
    }

    void Update()
    {
        if (breakRockPop.activeSelf == true)
        {
            Vector3 dir = rockTransform.position - gameObject.transform.position;
            dir.y = 0.0f;
            if (dir.x < -3.0f || dir.x > 3.0f || dir.z < -3.0f || dir.z > 3.0f)
            {
                breakRockPop.SetActive(false);
            }
        }
        //open();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Rock1f")
        {
            num = 1;
            StartCoroutine("findObj");
            mapRock = collision.gameObject;
            MeetMonster();

        }
        if (collision.gameObject.tag == "Rock1")
        {
           
            StateManager.Instance.playerPos = gameObject.transform.position;
            rockTransform = collision.gameObject.transform;
            breakRockPop.SetActive(true);
        }

        if (collision.gameObject.tag == "Treasuref")
        {
            num = 2;
            StartCoroutine("findObj");
            
        }
        if (collision.gameObject.tag == "Treasure")
        {
           
            open();
           
        }

        

    }
  
    IEnumerator findObj()
    {
        switch(num)
        {
            case 1:
                fText.text = "정 면 에  길 을  막 고  있 는 \n 바 위 가  보 입 니 다.";
                findText.SetActive(true);
                break;
            case 2:
                fText.text = "정 면 에  상 자 를 \n 발 견 했 습 니 다.";
                findText.SetActive(true);
                break;
        }
        yield return new WaitForSeconds(1.5f);
        findText.SetActive(false);
        num = 0;
    }

    public void breakRockYes()
    {
        DestroyObject(mapRock);
        breakRockPop.SetActive(false);
        battelCamera.enabled = true;
        gameObj.SetActive(true);
        gameObj.transform.position = battlePos[0].transform.position;
        StateManager.Instance.timerIsActive = true;
        StateManager.Instance.objBlocked = true;
    }
    public void breakRockNo()
    {
        breakRockPop.SetActive(false);
    }

    public void open()
    {
        int TreasureNum = Random.Range(1, 15);

        if (TreasureNum <= moneyValue)
        {
           int money = Random.Range(2, 4);
            switch(money)
            {
                case 2:
                    int ten = Random.Range(1, 10);
                    StateManager.Instance.playGold += ten * 10;
                    Debug.Log(ten * 10);
                    break;
                case 3:
                    int hundred = Random.Range(1, 2);
                    int ten2 = Random.Range(1, 10);
                    StateManager.Instance.playGold += ten2 * 10;
                    StateManager.Instance.playGold += hundred * 100;
                    Debug.Log(hundred * 100);
                    break;
            }
            Debug.Log("돈" + TreasureNum);
        }

        if (TreasureNum > moneyValue && TreasureNum <= nValue + moneyValue)
        {

            Debug.Log("꽝" + TreasureNum);
        }

        if (TreasureNum > nValue + moneyValue && TreasureNum <= nValue + moneyValue + moValue)
        {

            Debug.Log("몬스터" + TreasureNum);
        }

        if (TreasureNum > nValue + moneyValue + moValue && TreasureNum <= nValue + moneyValue + moValue  + sValue)
        {
            int itemNum = Random.Range(0, 6);
            GetScrollItem(itemNum, gameObj);
            Debug.Log("스크롤" + TreasureNum);
        }

        if (TreasureNum > nValue + moneyValue + moValue + sValue && TreasureNum <= nValue + moneyValue + moValue + sValue + pValue)
        {
            int itemNum = Random.Range(6, 8);
            GetScrollItem(itemNum, gameObj);
            Debug.Log("포션" + TreasureNum);
        }

        if (TreasureNum > nValue + moneyValue + moValue + sValue + pValue && TreasureNum <= nValue + moneyValue + moValue + sValue + pValue + wValue)
        {
            int itemNum = Random.Range(0, 4);
            GetWeapon(weaponSetObj, itemNum);
            Debug.Log("무기" + TreasureNum);
        }

    }

    private void GetScrollItem(int itemIndex, GameObject itemUseSet)
    {
        int itemNum = Random.Range(0, 4);

        switch (itemIndex)
        {
            case 0:
                //스크롤이 오래되서 부서짐
                break;
            case 1:
                //스킬
                var sItem = (SkillItem)StateManager.Instance.skillScrollItems[itemNum];
                if (StateManager.Instance.SkscrollNum[itemNum] == 0)
                {
                    itemUseNameText.text = "이 름: " + sItem.Name;
                    itemUseExplainText.text = "설 명: " + sItem.Explain;
                    itemUseImage.GetComponent<Image>().sprite = (Sprite)Resources.Load(sItem.Image, typeof(Sprite));

                    itemUseSet = Instantiate(itemUseSetObj) as GameObject;
                    itemUseSet.transform.SetParent(skillUseGrid.transform);
                    itemUseSet.transform.localScale = new Vector3(1, 1, 1);
                    itemUseSet.name = "Skill" + itemNum;
                    StateManager.Instance.SkScrollBag[itemNum] = itemUseSet;
                }
                StateManager.Instance.SkscrollNum[itemNum]++;
                StateManager.Instance.SkScrollBag[itemNum].transform.FindChild("ScrollUseCut").GetComponent<Text>().text = "보 유" + "\n" + StateManager.Instance.SkscrollNum[itemNum] + " 개";
                break;
            case 2:
                //스크롤이 오래되서 부서짐
                break;
            case 3:
                MagicItem mItem = (MagicItem)StateManager.Instance.magicScrollItems[itemNum];
                if (StateManager.Instance.MgscrollNum[itemNum] == 0)
                {
                    itemUseNameText.text = "이 름: " + mItem.Name;
                    itemUseExplainText.text = "설 명: " + mItem.Explain;
                    itemUseImage.GetComponent<Image>().sprite = (Sprite)Resources.Load(mItem.Image, typeof(Sprite));

                    itemUseSet = Instantiate(itemUseSetObj) as GameObject;
                    itemUseSet.transform.SetParent(skillUseGrid.transform);
                    itemUseSet.transform.localScale = new Vector3(1, 1, 1);
                    itemUseSet.name = "Magic" + itemNum;
                    StateManager.Instance.MgScrollBag[itemNum] = itemUseSet;
                }
                StateManager.Instance.MgscrollNum[itemNum]++;
                StateManager.Instance.MgScrollBag[itemNum].transform.FindChild("ScrollUseCut").GetComponent<Text>().text = "보 유" + "\n" + StateManager.Instance.MgscrollNum[itemNum] + " 개";
                break;
            case 4:
                //스크롤이 오래되서 부서짐
                break;
            case 5:
                BuffItem bItem = (BuffItem)StateManager.Instance.buffScrollItems[itemNum];

                if (StateManager.Instance.BufscrollNum[itemNum] == 0)
                {
                    itemUseNameText.text = "이 름: " + bItem.Name;
                    itemUseExplainText.text = "설 명: " + bItem.Explain;
                    itemUseImage.GetComponent<Image>().sprite = (Sprite)Resources.Load(bItem.Image, typeof(Sprite));

                    itemUseSet = Instantiate(itemUseSetObj) as GameObject;
                    itemUseSet.transform.SetParent(skillUseGrid.transform);
                    itemUseSet.transform.localScale = new Vector3(1, 1, 1);
                    itemUseSet.name = "Buff" + itemNum;
                    StateManager.Instance.BufScrollBag[itemNum] = itemUseSet;
                }
                StateManager.Instance.BufscrollNum[itemNum]++;
                StateManager.Instance.BufScrollBag[itemNum].transform.FindChild("ScrollUseCut").GetComponent<Text>().text = "보 유" + "\n" + StateManager.Instance.BufscrollNum[itemNum] + " 개";
                break;
            case 6:
                //깨진 포션병을 발견
                break;
            case 7:
                if(itemNum==1)
                {
                    itemNum = 0;
                }
                //포션
                PotionItem item = (PotionItem)StateManager.Instance.potionItems[itemNum];

                if (StateManager.Instance.potionNum[itemNum] == 0)
                {
                    itemUseNameText.text = "이 름: " + item.Name;
                    itemUseExplainText.text = "설 명: " + item.Explain;
                    itemUseImage.GetComponent<Image>().sprite = (Sprite)Resources.Load(item.Image, typeof(Sprite));

                    itemUseSet = Instantiate(itemUseSetObj) as GameObject;
                    itemUseSet.transform.SetParent(itemUseGrid.transform);
                    itemUseSet.transform.localScale = new Vector3(1, 1, 1);
                    itemUseSet.name = "Potion" + itemNum;

                    StateManager.Instance.potionItemBag[itemNum] = itemUseSet;
                }

                StateManager.Instance.potionNum[itemNum]++;
                StateManager.Instance.potionItemBag[itemNum].transform.FindChild("ScrollUseCut").GetComponent<Text>().text = "보 유" + "\n" + StateManager.Instance.potionNum[itemNum] + " 개";

                break;
        }
    }

    private void GetWeapon(GameObject gameObj, int itemIndex)
    {
        if (StateManager.Instance.bagSize == 5)
        {
            return;
        }
        StateManager.Instance.bagSize++;
        int WeaponNum = Random.Range(0, 4);
        if(WeaponNum == 3)
        {
            WeaponNum = 2;
        }
        switch (itemIndex)
        {
            case 0:
                //망가진 무기을 발견(꽝)
                break;
            case 1:
                HMWeaponItem witem = (HMWeaponItem)StateManager.Instance.weaponItems[itemIndex];

                weaponDurabilityText.GetComponent<Text>().text = "내구도: " + witem.Durability.ToString();
                weaponNameText.GetComponent<Text>().text = witem.Name + " 공격력: " + witem.AttackPoint.ToString();

                weaponImage.GetComponent<Image>().sprite = (Sprite)Resources.Load(witem.Image, typeof(Sprite));

                gameObj = Instantiate(WeaponUse) as GameObject;
                gameObj.transform.SetParent(weaponGrid.transform);
                gameObj.transform.localScale = new Vector3(1, 1, 1);

                for (int wNum = 0; wNum < 5; wNum++)
                {
                    if (StateManager.Instance.weaponSpace[wNum] == null)
                    {
                        gameObj.name = witem.WeaponName + wNum;
                        StateManager.Instance.weaponDurability[wNum] = witem.Durability;
                        StateManager.Instance.weaponSpace[wNum] = gameObj;
                        return;
                    }
                }
                break;
            case 2:
                HMWeaponItem item = (HMWeaponItem)StateManager.Instance.weaponItems[itemIndex];

                weaponDurabilityText.GetComponent<Text>().text = "내구도: " + item.Durability.ToString();
                weaponNameText.GetComponent<Text>().text = item.Name + " 공격력: " + item.AttackPoint.ToString();

                weaponImage.GetComponent<Image>().sprite = (Sprite)Resources.Load(item.Image, typeof(Sprite));

                gameObj = Instantiate(WeaponUse) as GameObject;
                gameObj.transform.SetParent(weaponGrid.transform);
                gameObj.transform.localScale = new Vector3(1, 1, 1);

                for (int wNum = 0; wNum < 5; wNum++)
                {
                    if (StateManager.Instance.weaponSpace[wNum] == null)
                    {
                        gameObj.name = item.WeaponName + wNum;
                        StateManager.Instance.weaponDurability[wNum] = item.Durability;
                        StateManager.Instance.weaponSpace[wNum] = gameObj;
                        return;
                    }
                }
                break;
        }
    }

    private void GetPop()
    {

    }

    private void MeetMonster()
    {
        var level = (Level)StateManager.Instance.dungeonLevels[0/*StateManager.Instance.dungeonLevel*/];
        int monsterNum = Random.Range(1, (level.Monster + 1));
        Debug.Log(monsterNum + "몬스터 랜덤값");
        for(int i=0; i < monsterNum; i++)
        {
            StateManager.Instance.monster[i].transform.position = battlePos[i].transform.position;
            StateManager.Instance.monster[i].SetActive(true);
        }
        battelCamera.enabled = true;
        StateManager.Instance.timerIsActive = true;
        StateManager.Instance.monsterBattle = true;
    }
}
