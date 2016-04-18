using UnityEngine;
using System;
using System.Collections;
using live2d;

[ExecuteInEditMode]
public class csDrake : MonoBehaviour
{
    public TextAsset mocFile;
    public Texture2D[] textures;
    public TextAsset[] mtnFiles;

    private Live2DModelUnity live2DModel;

    private Live2DMotion motion;
    private MotionQueueManager motionManager;



    int num;
    // Use this for initialization
    void Start()
    {
        Live2D.init();

        live2DModel = Live2DModelUnity.loadModel(mocFile.bytes);

        for (int i = 0; i < textures.Length; i++)
        {
            live2DModel.setTexture(i, textures[i]);
        }
        motion = Live2DMotion.loadMotion(mtnFiles[0].bytes);
        motion.setLoop(true);

        motionManager = new MotionQueueManager();

        motionManager.startMotion(motion, false);

    }

    void Update()
    {
        float modelWidth = live2DModel.getCanvasWidth();
        Matrix4x4 m1 = Matrix4x4.Ortho(
                0, modelWidth, modelWidth,
                0, -50.0f, 50.0f);
        Matrix4x4 m2 = transform.localToWorldMatrix;
        Matrix4x4 m3 = m2 * m1;

        motionManager.updateParam(live2DModel);
        if (live2DModel == null) return;

        live2DModel.setMatrix(m3);
        live2DModel.update();
    }

    void OnRenderObject()
    {
        live2DModel.draw();
    }


    public void ani(int num)
    {
        switch (num)
        {
            case 0://대기
                motion = Live2DMotion.loadMotion(mtnFiles[num].bytes);
                motion.setLoop(true);
                motionManager.startMotion(motion, false);
                break;
            case 1://공격
                motion = Live2DMotion.loadMotion(mtnFiles[num].bytes);
                motionManager.startMotion(motion, false);
                break;
            case 2://죽음
                motion = Live2DMotion.loadMotion(mtnFiles[num].bytes);
                motionManager.startMotion(motion, false);
                break;
            case 3://방어
                motion = Live2DMotion.loadMotion(mtnFiles[num].bytes);
                motionManager.startMotion(motion, false);
                break;
            case 4://점프공격
                motion = Live2DMotion.loadMotion(mtnFiles[num].bytes);
                motionManager.startMotion(motion, false);
                break;
        }
    }
}