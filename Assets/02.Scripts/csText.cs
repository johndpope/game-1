using UnityEngine;
using System.Collections;


public class csText : MonoBehaviour
{
    Vector2[] touchPos = new Vector2[5];
    Vector3 rayDir;
    public GameObject battleCameraObj;
    Camera battelCamera = new Camera();
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
                Debug.Log("터치 되었습니다.");
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
            if(hitObj.transform.tag.Equals("enemy"))
            {
                Debug.Log("몬스터 클릭됨");
            }
        }
    }
}


 

