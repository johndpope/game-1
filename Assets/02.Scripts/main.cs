using UnityEngine;
using System;
using System.Collections;
using live2d;
using live2d.framework;

[ExecuteInEditMode]
public class main : MonoBehaviour
{
    public TextAsset mocFile;
    public TextAsset physicsFile;
    public Texture2D[] textures;
    public TextAsset[] mtnFiles;

    private Live2DMotion motion;
    private MotionQueueManager motionManager;
    private Live2DModelUnity live2DModel;

    private EyeBlinkMotion eyeBlink = new EyeBlinkMotion();
    private L2DTargetPoint dragMgr = new L2DTargetPoint();
    
    private L2DPhysics physics;
    private Matrix4x4 live2DCanvasPos;


    // Use this for initialization
    void Start()
    {
        Live2D.init();

        load();
    }

    void load()
    {
        live2DModel = Live2DModelUnity.loadModel(mocFile.bytes);

        for (int i = 0; i < textures.Length; i++)
        {
            live2DModel.setTexture(i, textures[i]);
        }

        float modelWidth = live2DModel.getCanvasWidth();
        live2DCanvasPos = Matrix4x4.Ortho(0, modelWidth, modelWidth, 0, -50.0f, 50.0f);

        if (physicsFile != null) physics = L2DPhysics.load(physicsFile.bytes);
        // モーションのロード
        motion = Live2DMotion.loadMotion(mtnFiles[0].bytes);
        motion.setLoop(true);
        // モーション管理クラスのインスタンスの作成
        motionManager = new MotionQueueManager();

        // モーションの再生
        motionManager.startMotion(motion, false);
        
    }

    // Update is called once per frame
    void Update()
    {
        if (live2DModel == null) load();
        live2DModel.setMatrix(transform.localToWorldMatrix * live2DCanvasPos);

        if (!Application.isPlaying)
        {
            live2DModel.update();
            return;
        }

        //if (Input.GetButtonDown("Fire1"))
        //{
        //    motion = Live2DMotion.loadMotion(mtnFiles[0].bytes);
        //    motion.setLoop(true);
        //    motionManager.startMotion(motion, false);
        //    Debug.Log("Fire1");
        //}
        if (Input.GetButtonDown("Fire2"))
        {
            motionManager.stopAllMotions();
            Debug.Log("Jump");
        }

        double timeSec = UtSystem.getUserTimeMSec() / 1000.0;
        double t = timeSec * 2 * Math.PI;
        live2DModel.setParamFloat("PARAM_BREATH", (float)(0.5f + 0.5f * Math.Sin(t / 3.0)));

        //float modelWidth = live2DModel.getCanvasWidth();
        //Matrix4x4 m1 = Matrix4x4.Ortho(
        //        0, modelWidth, modelWidth,
        //        0, -50.0f, 50.0f);
        //Matrix4x4 m2 = transform.localToWorldMatrix;
        //Matrix4x4 m3 = m2 * m1;


        //live2DModel.setMatrix(m3);
        motionManager.updateParam(live2DModel);
        if (live2DModel == null) return;

        eyeBlink.setParam(live2DModel);

        if (physics != null) physics.updateParam(live2DModel);

        live2DModel.update();
    }
  

    void OnRenderObject()
    {
        //live2DModel.draw();
        if (live2DModel == null) load();
        if (live2DModel.getRenderMode() == Live2D.L2D_RENDER_DRAW_MESH_NOW) live2DModel.draw();
    }
    public void onAttack()
    {
        motion = Live2DMotion.loadMotion(mtnFiles[0].bytes);
        motion.setLoop(true);
        motionManager.startMotion(motion, false);
    }
}