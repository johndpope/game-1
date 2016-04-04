using UnityEngine;
using System.Collections;
using itemPool;
using live2d;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

public class csText : MonoBehaviour
{

    public TextAsset modelJson;          // model.json
    public TextAsset mocFile;           // 모델 파일
    public Texture2D texture;         // 텍스처 파일
    public TextAsset mtnFile;         // 모션 파일
    private AudioClip[] soundFile;       // 음성 파일
    private Live2DModelUnity live2DModel;
    private Live2DMotion motion;                 // 모션 클래스
    private MotionQueueManager motionManager;    // 모션 관리 클래스
    private int cnt = 0;     // 파일 참조 번호


    void Start()
    {
        // JSON을로드
        Json_Read();
        // Live2D 초기화
        Live2D.init();
        live2DModel = Live2DModelUnity.loadModel(mocFile.bytes);      
        live2DModel.setTexture(0, texture);
       
        // 모션 관리 클래스의 인스턴스 생성
        motionManager = new MotionQueueManager();
        // 모션 인스턴스의 생성
        motion = Live2DMotion.loadMotion(mtnFile.bytes);
        // 모션 재생
        motionManager.startMotion(motion, false);
    }


    void Json_Read()
    {
        // JSON 텍스트 디코딩

        Hashtable itemTable = (Hashtable)HMJson.objectFromJsonString(modelJson.text);

        // 모델 읽기
        string model = (string)itemTable["model"];
        Debug.Log(itemTable["model"].GetType());

        mocFile = new TextAsset();
        mocFile = (Resources.Load(model, typeof(TextAsset)) as TextAsset);
        // 텍스처 읽기
        ArrayList textureList = (ArrayList)itemTable["textures"];        
       
        foreach (string txtNm in textureList)
        {
            //불필요한 확장자를 제거

            string pngNm = Regex.Replace(txtNm, ".png$", "");
            Debug.Log(pngNm);
            texture = (Resources.Load(pngNm, typeof(Texture2D)) as Texture2D);
            Debug.Log(texture);
        }
        // 모션 읽기
        //ArrayList motions = (ArrayList)itemTable["motions"];
        //ArrayList file = (ArrayList)motions;
        //int i = 0;
        //mtnFile = new TextAsset[file.Count];

        //foreach (IDictionary mtnNm in file)
        //{
        //    mtnFile[i] = (Resources.Load((string)mtnNm["file"], typeof(TextAsset)) as TextAsset);
        //    i++;
        //}
    }
    void Update()
    {
        //// a 키 누를 때 모션 전환
        //if (Input.GetKeyDown("a"))
        //{
        //    if (cnt >= mtnFile.Length - 1)
        //    {
        //        cnt = 0;
        //    }
        //    cnt++;
        //    // 모션를 로딩하는
        //    motion = Live2DMotion.loadMotion(mtnFile[cnt].bytes);
        //    // 페이드 아웃은 0
        //    motion.setFadeOut(0);
        //    // 모션 시작
        //    motionManager.startMotion(motion, false);
        //}
    }


    /// <summary>
    /// 카메라가 장면 렌더링에 불리는
    /// </ summary>
    //void OnRenderObject()
    //{
    //    // 묘화 위치를 지정
    //    Matrix4x4 m1 = Matrix4x4.Ortho(0, 1000.0f, 1000.0f, 0, -0.5f, 0.5f);
    //    Matrix4x4 m2 = transform.localToWorldMatrix;
    //    Matrix4x4 m3 = m2 * m1;
    //    live2DModel.setMatrix(m3);

    //    if (live2DModel == null) return;
    //    // 정점의 업데이트
    //    live2DModel.update();
    //    // 모델 그리기
    //    live2DModel.draw();
    //}

}
