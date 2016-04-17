using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;

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
    //보물 위치 값을 저장
    Transform treasureTransform;

    //전투하는곳의 player위치(기즈모 포인트)
    public GameObject battlePlayerPos;
    //전투하는곳의 Rock과 몬스터의 위치(기즈모 포인트)
    public GameObject[] battlePos;
    

    //맵에서 부딧친 돌의 GameObject를 저장하는 GameObject저장소
    GameObject mapRock;
    //맵에서 부딧친 상자의 GameObject를 저장하는 GameObject저장소
    GameObject mapTreasure;
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

    public GameObject battelM;

    //무기확률5%, 포션5%, 스크롤(스킬,마법,버프)5%, 몬스터15%, 꽝10%, 돈50%

    public GameObject cBoxOpen;
    public GameObject boxOpen1;
    public GameObject boxOpen2;
    public GameObject boxOpen3;

    public GameObject getItem;
    Sprite getItemImage;

    public GameObject getItemTextObj;
    Text getItemText;

    public GameObject battleYesBtn;

    public GameObject finishGold;

    int num;
    void Start()
    {
        getItemText = getItemTextObj.GetComponent<Text>();
       

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

        if (breakRockPop.activeSelf.Equals(true))
        {
            Vector3 dir = rockTransform.position - gameObject.transform.position;
            dir.y = 0.0f;
            if (dir.x < -3.0f || dir.x > 3.0f || dir.z < -3.0f || dir.z > 3.0f)
            {
                breakRockPop.SetActive(false);
            }
        }
        if (openTreasurePop.activeSelf.Equals(true))
        {
            Vector3 dir = treasureTransform.position - gameObject.transform.position;
            dir.y = 0.0f;
            if (dir.x < -3.0f || dir.x > 3.0f || dir.z < -3.0f || dir.z > 3.0f)
            {
                openTreasurePop.SetActive(false);
            }
        }
        //open();

        if(Joystick.run.Equals(30))
        {
            Debug.Log("300임 ㅋㅋㅋ");
           
            MeetMonster();
            Joystick.run = 0;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Rock1f")
        {
            mapRock = collision.gameObject;
            StartCoroutine(findObj(1,0,null));
            //MeetMonster();

        }
        if (collision.gameObject.tag == "Rock1")
        { 
            rockTransform = collision.gameObject.transform;
            breakRockPop.SetActive(true);
        }

        if (collision.gameObject.tag == "Treasuref")
        {
            mapTreasure = collision.gameObject;
            StartCoroutine(findObj(2,0, null));

        }
        if (collision.gameObject.tag == "Treasure")
        {
            treasureTransform = collision.gameObject.transform;
            openTreasurePop.SetActive(true);           
        }

        

    }
  
    IEnumerator findObj(int num, int maony, string itemName)
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
            case 3:
                fText.text = "상 자 가 \n텅  비 어 있 습 니 다.";
                findText.SetActive(true);
                break;
            case 4:
                fText.text = maony+" 골 드 를 \n발 견 하 였 습 니 다.";
                findText.SetActive(true);
                break;
            case 5:
                fText.text = "몬 스 터 가 \n나 타 났 다.";
                findText.SetActive(true);
                break;
            case 6:
                fText.text = itemName;
                findText.SetActive(true);
                break;
            case 7:
                fText.text = itemName;
                findText.SetActive(true);
                break;
            case 8:
                fText.text = itemName;
                findText.SetActive(true);
                break;
        }
        yield return new WaitForSeconds(1.5f);
        findText.SetActive(false);
        if(num.Equals(5))
        {
            MeetMonster();
        }
        num = 0;
    }

    public void breakRockYes()
    {
        mapRock.SetActive(false);
        //DestroyObject(mapRock);
        breakRockPop.SetActive(false);
        battelCamera.enabled = true;
        battelM.SetActive(true);
        gameObj.SetActive(true);
        gameObj.transform.position = battlePos[0].transform.position;
        StateManager.Instance.timerIsActive = true;
        StateManager.Instance.objBlocked = true;
    }

    public void breakRockNo()
    {
        breakRockPop.SetActive(false);
        openTreasurePop.SetActive(false);
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
                    StartCoroutine(findObj(4, ten * 10, null));
                    break;
                case 3:
                    int hundred = Random.Range(1, 2);
                    int ten2 = Random.Range(1, 10);
                    StateManager.Instance.playGold += ten2 * 10;
                    StateManager.Instance.playGold += hundred * 100;
                    Debug.Log(hundred * 100);
                    StartCoroutine(findObj(4, (ten2 * 10+ hundred * 100), null));
                    break;
            }
            Debug.Log("돈" + TreasureNum);         
        }

        if (TreasureNum > moneyValue && TreasureNum <= nValue + moneyValue)
        {
            StartCoroutine(findObj(3,0, null));
            Debug.Log("꽝" + TreasureNum);
        }

        if (TreasureNum > nValue + moneyValue && TreasureNum <= nValue + moneyValue + moValue)
        {
            StartCoroutine(findObj(5, 0, null));            
            Debug.Log("몬스터" + TreasureNum);
        }

        if (TreasureNum > nValue + moneyValue + moValue && TreasureNum <= nValue + moneyValue + moValue  + sValue)
        {
            int itemNum = Random.Range(1, 6);
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
            int itemNum = Random.Range(0, 3);
            GetWeapon(weaponSetObj, itemNum);
            Debug.Log("무기" + TreasureNum);
        }
        mapTreasure.SetActive(false);
        openTreasurePop.SetActive(false);
    }

    private void GetScrollItem(int itemIndex, GameObject itemUseSet)
    {
        int itemNum = Random.Range(0, 4);

        switch (itemIndex)
        {
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

                if (cBoxOpen.activeSelf.Equals(false))
                {
                    StartCoroutine(findObj(6, 0, sItem.Name + "\n 스 크 롤 을  발 견 "));
                }
                else if(cBoxOpen.activeSelf.Equals(true))
                {
                    getItemText.text = "획득 아이템 \n: " + sItem.Name;
                    getItem.GetComponent<Image>().sprite = (Sprite)Resources.Load(sItem.Image, typeof(Sprite));
                }
                
                break;
            case 2:
                if (cBoxOpen.activeSelf.Equals(false))
                {
                    StartCoroutine(findObj(6, 0, "스 크 롤 이 \n오 래 되 서  부 서 짐"));
                }
                else if (cBoxOpen.activeSelf.Equals(true))
                {
                    getItemText.text = "획득 아이템 \n: 없 음";
                }
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
                if (cBoxOpen.activeSelf.Equals(false))
                {
                    StartCoroutine(findObj(6, 0, mItem.Name + "\n 스 크 롤 을  발 견 "));
                }
                else if (cBoxOpen.activeSelf.Equals(true))
                {
                    getItemText.text = "획득 아이템 \n: " + mItem.Name;
                    getItem.GetComponent<Image>().sprite = (Sprite)Resources.Load(mItem.Image, typeof(Sprite));
                }
                
                break;
            case 4:
                //스크롤이 오래되서 부서짐
                if (cBoxOpen.activeSelf.Equals(false))
                {
                    StartCoroutine(findObj(6, 0, "스 크 롤 이 \n오 래 되 서  부 서 짐"));
                }
                else if (cBoxOpen.activeSelf.Equals(true))
                {
                    getItemText.text = "획득 아이템 \n: 없 음";
                }
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
                if (cBoxOpen.activeSelf.Equals(false))
                {
                    StartCoroutine(findObj(6, 0, bItem.Name + "\n 스 크 롤 을  발 견 "));
                }
                else if (cBoxOpen.activeSelf.Equals(true))
                {
                    getItemText.text = "획득 아이템 \n: " + bItem.Name;
                    getItem.GetComponent<Image>().sprite = (Sprite)Resources.Load(bItem.Image, typeof(Sprite));
                }
                break;
            case 6:
                //깨진 포션병을 발견
                if (cBoxOpen.activeSelf.Equals(false))
                {
                    StartCoroutine(findObj(7, 0, "깨 진  포 션 병 을  발 견"));
                }
                else if (cBoxOpen.activeSelf.Equals(true))
                {
                    getItemText.text = "획득 아이템 \n: 없 음";
                }
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

                if (cBoxOpen.activeSelf.Equals(false))
                {
                    StartCoroutine(findObj(7, 0, item.Name + "\n 포 션 을  발 견 "));
                }
                else if (cBoxOpen.activeSelf.Equals(true))
                {
                    getItemText.text = "획득 아이템 \n: " + item.Name;
                    getItem.GetComponent<Image>().sprite = (Sprite)Resources.Load(item.Image, typeof(Sprite));
                }
                break;
        }
    }

    private void GetWeapon(GameObject gameObj, int itemIndex)
    {
        int WeaponNum = Random.Range(0, 4);
        if(WeaponNum == 3)
        {
            WeaponNum = 2;
        }

        Debug.Log(WeaponNum+"무기 번호");
        switch (itemIndex)
        {
            case 0:
                //망가진 무기을 발견(꽝)
                if (cBoxOpen.activeSelf.Equals(false))
                {
                    StartCoroutine(findObj(8, 0, "망 가 진 \n 무 기 를  발 견 "));
                }
                else if (cBoxOpen.activeSelf.Equals(true))
                {
                    getItemText.text = "획득 아이템 \n: 망가진 무기 발견 ";
                }
                break;
            case 1:
                HMWeaponItem witem = (HMWeaponItem)StateManager.Instance.weaponItems[WeaponNum];
                if (StateManager.Instance.bagSize == 5)
                {
                    getItemText.text = "획득 아이템 \n: 가방이 꽉 찼다.";
                    return;
                }
                if (cBoxOpen.activeSelf.Equals(false))
                {
                    StartCoroutine(findObj(8, 0, witem.Name + " 내 구 도: " + (witem.Durability / 2).ToString() + "\n의 무 기  발 견"));
                }
                else if (cBoxOpen.activeSelf.Equals(true))
                {
                    getItemText.text = "획득 아이템 \n: " + witem.Name + "\n 내 구 도: " + witem.Durability.ToString();
                    getItem.GetComponent<Image>().sprite = (Sprite)Resources.Load(witem.Image, typeof(Sprite));
                }

                StateManager.Instance.bagSize++;
                weaponDurabilityText.GetComponent<Text>().text = "내구도: " + (witem.Durability/2).ToString();
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
                        StateManager.Instance.weaponDurability[wNum] = witem.Durability/2;
                        StateManager.Instance.weaponSpace[wNum] = gameObj;
                        return;
                    }
                }

               
                break;
            case 2:
                HMWeaponItem item = (HMWeaponItem)StateManager.Instance.weaponItems[WeaponNum];

                if (StateManager.Instance.bagSize == 5)
                {
                    getItemText.text = "획득 아이템 \n: 가방이 꽉 찼다.";
                    return;
                }
                if (cBoxOpen.activeSelf.Equals(false))
                {
                    StartCoroutine(findObj(8, 0, item.Name + " 내 구 도: " + item.Durability.ToString() + "\n의 무 기  발 견"));
                }
                else if (cBoxOpen.activeSelf.Equals(true))
                {
                    getItem.GetComponent<Image>().sprite = (Sprite)Resources.Load(item.Image, typeof(Sprite));
                    getItemText.text = "획득 아이템 \n: " + item.Name + "\n 내 구 도: " + item.Durability.ToString();
                }
                StateManager.Instance.bagSize++;
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


    private void MeetMonster()
    {
        //gameObject.SetActive(false);
        var level = (Level)StateManager.Instance.dungeonLevels[0/*StateManager.Instance.dungeonLevel*/];
        StateManager.Instance.monsterNum = Random.Range(1, (level.Monster + 1));
        
        Debug.Log(StateManager.Instance.monsterNum + "몬스터 랜덤값");
        
        for (int i=0; i < StateManager.Instance.monsterNum;)
        {
            //몬스터 레벨 1~5
            if (0 <= StateManager.Instance.dungeonLevel && 4 >= StateManager.Instance.dungeonLevel)
            {
                int num = Random.Range(0, 1);

                switch (num)
                {
                    case 0:
                        Slime(i);
                        i++;
                        break;
                    case 1:
                        Mimic(i);
                        i++;
                        break;
                    case 2:
                        Mimic2(i);
                        i++;
                        break;
                }
            }
            //몬스터 레벨 6~9
            if (5 <= StateManager.Instance.dungeonLevel && 8 >= StateManager.Instance.dungeonLevel)
            {
                int num = Random.Range(0, 3);

                switch (num)
                {
                    case 0:
                        Slime(i);
                        i++;
                        break;
                    case 1:
                        Mimic(i);
                        i++;
                        break;
                    case 2:
                        Mimic2(i);
                        i++;
                        break;
                }
            }
            //보스전
            if (StateManager.Instance.dungeonLevel.Equals(9))
            {
                int num = Random.Range(0, 2);
                switch (num)
                {
                    case 0:
                        break;
                    case 1:
                        Slime(i);
                        i++;
                        break;
                }
            }
            //11~19 던전
            if (10 <= StateManager.Instance.dungeonLevel && 18 >= StateManager.Instance.dungeonLevel)
            {
                int num = Random.Range(0, 4);

                switch (num)
                {
                    case 0:
                        Ghost(i);
                        i++;
                        break;
                    case 1:
                        Pumkin(i);
                        i++;
                        break;
                    case 2:
                        Mimic(i);
                        i++;
                        break;
                    case 3:
                        Mimic2(i);
                        i++;
                        break;
                }
            }
            //20보스전
            if (StateManager.Instance.dungeonLevel.Equals(19))
            {
                int num = Random.Range(0, 2);
                switch (num)
                {
                    case 0:
                        break;
                    case 1:
                        Ghost(i);
                        i++;
                        break;
                }
            }
            //21~29 던전
            if (20 <= StateManager.Instance.dungeonLevel && 28 >= StateManager.Instance.dungeonLevel)
            {
                int num = Random.Range(0, 4);

                switch (num)
                {
                    case 0:
                        Ghost(i);
                        i++;
                        break;
                    case 1:
                        Pumkin(i);
                        i++;
                        break;
                    case 2:
                        Mimic(i);
                        i++;
                        break;
                    case 3:
                        Mimic2(i);
                        i++;
                        break;
                }
            }
            //30보스전
            if (StateManager.Instance.dungeonLevel.Equals(29))
            {
                int num = Random.Range(0, 2);
                switch (num)
                {
                    case 0:
                        break;
                    case 1:
                        Ghost(i);
                        i++;
                        break;
                }
            }

        }

        battelM.SetActive(true);        
        battelCamera.enabled = true;
        StateManager.Instance.timerIsActive = true;
        StateManager.Instance.monsterBattle = true;
    }

    public void Slime(int i)
    {
       
        Monster slime = (Monster)StateManager.Instance.dungeonMonsters[0];
        StateManager.Instance.monster[i] = StateManager.Instance.slime[i];
        StateManager.Instance.monster[i].name = slime.Name + i;
        StateManager.Instance.monster[i].transform.position = battlePos[i].transform.position;
        StateManager.Instance.monster[i].SetActive(true);
        csBattle.eTimer[i] = Random.Range(slime.MonsterMinSpd, slime.MonsterMaxSpd + 1);
        StateManager.Instance.monsterHp[i] = slime.MonsterHp + (slime.MonsterHp * StateManager.Instance.dungeonLevel * 0.2f);
        StateManager.Instance.monsterAtk[i] = slime.MonsterAtt;
        StateManager.Instance.monsterDef[i] = slime.MonsterDef;
        StateManager.Instance.monsterSpd[i] = csBattle.eTimer[i];
        StateManager.Instance.slimeNum++;
        Debug.Log("슬라임 숫자" + StateManager.Instance.slimeNum);
    }

    public void Mimic(int i)
    {
        Monster mimic = (Monster)StateManager.Instance.dungeonMonsters[1];
        StateManager.Instance.monster[i] = StateManager.Instance.mimic[i];
        StateManager.Instance.monster[i].name = mimic.Name + i;
        StateManager.Instance.monster[i].transform.position = battlePos[i].transform.position;
        StateManager.Instance.monster[i].SetActive(true);
        csBattle.eTimer[i] = Random.Range(mimic.MonsterMinSpd, mimic.MonsterMaxSpd + 1);
        StateManager.Instance.monsterHp[i] = mimic.MonsterHp;
        StateManager.Instance.monsterAtk[i] = mimic.MonsterAtt;
        StateManager.Instance.monsterDef[i] = mimic.MonsterDef;
        StateManager.Instance.monsterSpd[i] = csBattle.eTimer[i];
        StateManager.Instance.mimicNum++;
        Debug.Log("미믹 숫자" + StateManager.Instance.mimicNum);
    }

    public void Mimic2(int i)
    {
        Monster mimic2 = (Monster)StateManager.Instance.dungeonMonsters[2];
        StateManager.Instance.monster[i] = StateManager.Instance.mimic2[i];
        StateManager.Instance.monster[i].name = mimic2.Name + i;
        StateManager.Instance.monster[i].transform.position = battlePos[i].transform.position;
        StateManager.Instance.monster[i].SetActive(true);
        csBattle.eTimer[i] = Random.Range(mimic2.MonsterMinSpd, mimic2.MonsterMaxSpd + 1);
        StateManager.Instance.monsterHp[i] = mimic2.MonsterHp;
        StateManager.Instance.monsterAtk[i] = mimic2.MonsterAtt;
        StateManager.Instance.monsterDef[i] = mimic2.MonsterDef;
        StateManager.Instance.monsterSpd[i] = csBattle.eTimer[i];
        StateManager.Instance.mimic2Num++;
    }

    public void Ghost(int i)
    {
        Monster ghost = (Monster)StateManager.Instance.dungeonMonsters[3];
        StateManager.Instance.monster[i] = StateManager.Instance.ghost[i];
        StateManager.Instance.monster[i].name = ghost.Name + i;
        StateManager.Instance.monster[i].transform.position = battlePos[i].transform.position;
        StateManager.Instance.monster[i].SetActive(true);
        csBattle.eTimer[i] = Random.Range(ghost.MonsterMinSpd, ghost.MonsterMaxSpd + 1);
        StateManager.Instance.monsterHp[i] = ghost.MonsterHp;
        StateManager.Instance.monsterAtk[i] = ghost.MonsterAtt;
        StateManager.Instance.monsterDef[i] = ghost.MonsterDef;
        StateManager.Instance.monsterSpd[i] = csBattle.eTimer[i];
        StateManager.Instance.ghostNum++;
    }

    public void Pumkin(int i)
    {
        Monster pumkin = (Monster)StateManager.Instance.dungeonMonsters[4];
        StateManager.Instance.monster[i] = StateManager.Instance.pumkin[i];
        StateManager.Instance.monster[i].name = pumkin.Name + i;
        StateManager.Instance.monster[i].transform.position = battlePos[i].transform.position;
        StateManager.Instance.monster[i].SetActive(true);
        csBattle.eTimer[i] = Random.Range(pumkin.MonsterMinSpd, pumkin.MonsterMaxSpd + 1);
        StateManager.Instance.monsterHp[i] = pumkin.MonsterHp;
        StateManager.Instance.monsterAtk[i] = pumkin.MonsterAtt;
        StateManager.Instance.monsterDef[i] = pumkin.MonsterDef;
        StateManager.Instance.monsterSpd[i] = csBattle.eTimer[i];
        StateManager.Instance.pumkinNum++;
    }

    public void openBattleBox(int num)
    {
        bool open = false;

        if (num.Equals(1))
        {
            cBoxOpen.SetActive(true);
            cBoxOpen.transform.position = boxOpen1.transform.position;
            boxOpen2.GetComponent<Button>().enabled = false;
            boxOpen3.GetComponent<Button>().enabled = false;
            boxOpen1.SetActive(false);
            battleYesBtn.SetActive(true);
            open = true;
        }

        if (num.Equals(2))
        {
            cBoxOpen.SetActive(true);
            cBoxOpen.transform.position = boxOpen2.transform.position;
            boxOpen1.GetComponent<Button>().enabled = false;
            boxOpen3.GetComponent<Button>().enabled = false;
            boxOpen2.SetActive(false);
            battleYesBtn.SetActive(true);
            open = true;
        }

        if (num.Equals(3))
        {
            cBoxOpen.SetActive(true);
            cBoxOpen.transform.position = boxOpen3.transform.position;
            boxOpen2.GetComponent<Button>().enabled = false;
            boxOpen1.GetComponent<Button>().enabled = false;
            boxOpen3.SetActive(false);
            battleYesBtn.SetActive(true);
            open = true;
        }

        if (open == true)
        {
            int TreasureNum = Random.Range(1, 130);

            int wValue = 10;  //무기확률
            int pValue = 10;  //포션(소모품)확률
            int sValue = 10;  //스킬 마법 버프 스크롤 확률
            int nValue = 30;  //꽝
            int moneyValue = 70;  //돈

            if (TreasureNum <= moneyValue)
            {
                int money = Random.Range(2, 4);
                switch (money)
                {
                    case 2:
                        int ten = Random.Range(1, 7);
                        StateManager.Instance.playGold += ten * 10;
                        finishGold.GetComponent<Text>().text = "획득 골드\n: " + csBattle.monsterGold + " + " + ten * 10;
                        break;
                    case 3:
                        int hundred = Random.Range(1, 2);
                        int ten2 = Random.Range(1, 7);
                        StateManager.Instance.playGold += ten2 * 10;
                        StateManager.Instance.playGold += hundred * 50;
                        finishGold.GetComponent<Text>().text = "획득 골드\n: " + csBattle.monsterGold + " + " + ((ten2 * 10) + (hundred * 100));
                        break;
                }
            }


            if (TreasureNum > moneyValue && TreasureNum <= nValue + moneyValue)
            {
                finishGold.GetComponent<Text>().text = "획득 골드\n: " + csBattle.monsterGold + " + ";
                Debug.Log("꽝" + TreasureNum);
            }

            if (TreasureNum > nValue + moneyValue && TreasureNum <= nValue + moneyValue + sValue)
            {
                finishGold.GetComponent<Text>().text = "획득 골드\n: " + csBattle.monsterGold + " + ";
                int itemNum = Random.Range(1, 6);
                GetScrollItem(itemNum, gameObj);
                Debug.Log("스크롤" + TreasureNum);
            }

            if (TreasureNum > nValue + moneyValue + sValue && TreasureNum <= nValue + moneyValue + sValue + pValue)
            {
                finishGold.GetComponent<Text>().text = "획득 골드\n: " + csBattle.monsterGold + " + ";
                int itemNum = Random.Range(6, 8);
                GetScrollItem(itemNum, gameObj);
                Debug.Log("포션" + TreasureNum);
            }

            if (TreasureNum > nValue + moneyValue + sValue + pValue && TreasureNum <= nValue + moneyValue + sValue + pValue + wValue)
            {
                finishGold.GetComponent<Text>().text = "획득 골드\n: " + csBattle.monsterGold + " + ";
                int itemNum = Random.Range(0, 3);
                GetWeapon(weaponSetObj, itemNum);
                Debug.Log("무기" + TreasureNum);
            }
        }
    }
}
