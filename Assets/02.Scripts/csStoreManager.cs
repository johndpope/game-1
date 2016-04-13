using UnityEngine;
using System.Collections;
using UnityEngine.UI;



public class csStoreManager : MonoBehaviour
{
    //카메라 이동 속도
    public float cameraSpeed;

    

    //카메라 회전 속도
    public float rotationSpeed = 10.0f;

    //카메라 이동 위치
    Transform target1;
    Transform target1_1;
    Transform target2;
    Transform target2_1;
    Transform target3;
    Transform target3_1;
    Transform target4;
    Transform target4_1;

    //카메라 이동하기 위한 int값
    public int storeNumMove;

    //장비 상점 메뉴 
    public GameObject equipageMenu;

    //능력치 상점 메뉴
    public GameObject abilityScroll;
    public GameObject equInven;

    //아이템 상점 메뉴
    public GameObject itemMenu;

    //던전 입장 메뉴
    public GameObject dungeonMaun;

    //상점 오픈시 인벤토리 오브젝트
    public GameObject equipageScroll;
    
    //상점 팝업 설정 값
    public int storeNum;

    //카메라의 위치 값
    Transform maincamera;

    void Start ()
    {
        target1 = GameObject.Find("equipagePos").transform;
        target1_1 = GameObject.Find("equipageS").transform;
        target2 = GameObject.Find("abilityPos").transform;
        target2_1 = GameObject.Find("abilityS").transform;
        target3 = GameObject.Find("itemPos").transform;
        target3_1 = GameObject.Find("itemS").transform;
        target4 = GameObject.Find("dungeonPos").transform;
        target4_1 = GameObject.Find("dungeonS").transform;

        maincamera = GameObject.Find("Main Camera").transform;
        storeNumMove = 0;
        storeNum = 0;
    }
	
	// Update is called once per frame
	void Update ()
    {
         
        float z = cameraSpeed * Time.deltaTime;

        switch (storeNumMove)
        {
            case 0:
                break;
            case 1:
                //장비상점
                maincamera.Translate(0, 0, z);
                if (maincamera.position.z <= -3.5f)
                //if (maincamera.position.z <= 1.55f && maincamera.position.z >= 1.5f)
                {
                    //gizmo포인트 좌표
                    //maincamera.position = new Vector3(-14.9f, maincamera.position.y, -3.5f);
                    maincamera.position = new Vector3(0, maincamera.position.y, 1.5f);
                    Vector3 dir1_1 = target1_1.position - maincamera.position;
                    dir1_1.y = 0.0f; //높이                
                    dir1_1.Normalize(); // Normalize()백터3함수 x,z를  정규화
                    maincamera.rotation = Quaternion.Lerp(maincamera.rotation, Quaternion.LookRotation(dir1_1), rotationSpeed * Time.deltaTime);
                    StartCoroutine("equipageStorePop");
                }
                else
                {
                    Vector3 dir1 = target1.position - maincamera.position;
                    dir1.y = 0.0f; //높이                
                    dir1.Normalize(); // Normalize()백터3함수 x,z를  정규화
                    maincamera.rotation = Quaternion.Lerp(maincamera.rotation, Quaternion.LookRotation(dir1), rotationSpeed * Time.deltaTime);
                }

                break;
            case 2:
               maincamera.Translate(0, 0, z);
                //능력치상점
                //if (maincamera.position.z <= 1.35f && maincamera.position.z >= 1.3f)
                if (maincamera.position.z <= 9.05f && maincamera.position.z >= 9.0f)
                {
                    //gizmo포인트 좌표
                    //maincamera.position = new Vector3(-11.5f, maincamera.position.y, 1.3f);
                    maincamera.position = new Vector3(-2.0f, maincamera.position.y, 9.0f);
                    Vector3 dir2_1 = target2_1.position - maincamera.position;
                    dir2_1.y = 0.0f; //높이                
                    dir2_1.Normalize(); // Normalize()백터3함수 x,z를  정규화
                    maincamera.rotation = Quaternion.Lerp(maincamera.rotation, Quaternion.LookRotation(dir2_1), rotationSpeed * Time.deltaTime);
                    StartCoroutine("abilityStorePop");
                }
                else
                {
                    Vector3 dir2 = target2.position - maincamera.position;
                    dir2.y = 0.0f; //높이                
                    dir2.Normalize(); // Normalize()백터3함수 x,z를  정규화
                    maincamera.rotation = Quaternion.Lerp(maincamera.rotation, Quaternion.LookRotation(dir2), rotationSpeed * Time.deltaTime);
                }


                break;
            case 3:
                maincamera.Translate(0, 0, z);
                //아이템상점
                if (maincamera.position.z <= -1.9f && maincamera.position.z >= -2.1f)
                //if (maincamera.position.z <= -3.2f && maincamera.position.z >= -3.0f)
                {
                    //gizmo포인트 좌표
                    maincamera.position = new Vector3(-19.0f, maincamera.position.y, -2.0f);
                    //maincamera.position = new Vector3(2.0f, maincamera.position.y, -3.0f);

                    Vector3 dir3_1 = target3_1.position - maincamera.position;
                    dir3_1.y = 0.0f; //높이                
                    dir3_1.Normalize(); // Normalize()백터3함수 x,z를  정규화
                    maincamera.rotation = Quaternion.Lerp(maincamera.rotation, Quaternion.LookRotation(dir3_1), rotationSpeed * Time.deltaTime);
                    StartCoroutine("itemStorePop");
                }
                else
                {
                    Vector3 dir3 = target3.position - maincamera.position;
                    dir3.y = 0.0f; //높이                
                    dir3.Normalize(); // Normalize()백터3함수 x,z를  정규화
                    maincamera.rotation = Quaternion.Lerp(maincamera.rotation, Quaternion.LookRotation(dir3), rotationSpeed * Time.deltaTime);
                }
                break;

            case 4:
                maincamera.Translate(0, 0, z);
                //던전입구
                if (maincamera.position.z >= 6.6f)
                //if (maincamera.position.z >= -30.1f && maincamera.position.z <= -30.0f)
                {
                    //gizmo포인트 좌표
                    //maincamera.position = new Vector3(-18.8f, maincamera.position.y, 6.6f);
                    maincamera.position = new Vector3(-3.0f, maincamera.position.y, -30.0f);

                    Vector3 dir4_1 = target4_1.position - maincamera.position;
                    dir4_1.y = 0.0f; //높이                
                    dir4_1.Normalize(); // Normalize()백터3함수 x,z를  정규화
                    maincamera.rotation = Quaternion.Lerp(maincamera.rotation, Quaternion.LookRotation(dir4_1), rotationSpeed * Time.deltaTime);
                    StartCoroutine("dungeonPop");
                }
                else
                {
                    Vector3 dir4 = target4.position - maincamera.position;
                    dir4.y = 0.0f; //높이                
                    dir4.Normalize(); // Normalize()백터3함수 x,z를  정규화
                    maincamera.rotation = Quaternion.Lerp(maincamera.rotation, Quaternion.LookRotation(dir4), rotationSpeed * Time.deltaTime);
                }
                break;
        }

        switch(storeNum)
        {
            case 1:
                equipageMenu.SetActive(true);
                equipageScroll.SetActive(true);               
                break;
            case 2:          
                abilityScroll.SetActive(true);
                equInven.SetActive(true);
                break;
            case 3:
                itemMenu.SetActive(true);
                break;
            case 4:
                dungeonMaun.SetActive(true);
                break;
        }
	}

    public void equipageStoreMove()
    {
        storeNumMove = 1;
        storeNum = 0;
        equipageMenu.SetActive(false);
        equipageScroll.SetActive(false);
        dungeonMaun.SetActive(false);
        equInven.SetActive(false);
        abilityScroll.SetActive(false);
        itemMenu.SetActive(false);

    }

    public void abilityStoreMove()
    {
        storeNumMove = 2;
        storeNum = 0;
        equipageMenu.SetActive(false);
        equipageScroll.SetActive(false);
        dungeonMaun.SetActive(false);
        equInven.SetActive(false);
        abilityScroll.SetActive(false);
        itemMenu.SetActive(false);
    }

    public void itemStoreMove()
    {
        storeNumMove = 3;
        storeNum = 0;
        equipageMenu.SetActive(false);
        equipageScroll.SetActive(false);
        dungeonMaun.SetActive(false);
        equInven.SetActive(false);
        abilityScroll.SetActive(false);
        itemMenu.SetActive(false);

    }

    public void dungeonMove()
    {
        storeNumMove = 4;
        storeNum = 0;
        equipageMenu.SetActive(false);
        equipageScroll.SetActive(false);
        dungeonMaun.SetActive(false);
        equInven.SetActive(false);
        abilityScroll.SetActive(false);
        itemMenu.SetActive(false);
    }

    IEnumerator equipageStorePop()
    {
        storeNum = 1;
        yield return new WaitForSeconds(0.3f);
        storeNumMove = 0;
    }

    IEnumerator abilityStorePop()
    {
        storeNum = 2;
        yield return new WaitForSeconds(0.3f);
        storeNumMove = 0;
    }
    IEnumerator itemStorePop()
    {
        storeNum = 3;
        yield return new WaitForSeconds(0.3f);
        storeNumMove = 0;
    }
    IEnumerator dungeonPop()
    {
        storeNum = 4;
        yield return new WaitForSeconds(0.3f);
        storeNumMove = 0;
    }
}
