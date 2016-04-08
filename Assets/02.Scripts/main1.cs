using UnityEngine;
using System;
using System.Collections;
using live2d;

[ExecuteInEditMode]
public class main1 : MonoBehaviour
{
    public TextAsset mocFile;
    public Texture2D[] textures;
    public TextAsset[] mtnFiles;

    private Live2DModelUnity live2DModel;

    private Live2DMotion motion;
    private MotionQueueManager motionManager;

    public GameObject playerPos;
    static bool attEnemyBool;

    float enemySpd;
    // Use this for initialization
    void Start()
    {
        playerPos = GameObject.Find("slime0");
        attEnemyBool = false;
        enemySpd = -0.1f;
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

    // Update is called once per frame
    void Update()
    {
        if (attEnemyBool == true)
        {
            playerPos.transform.Translate(0, 0, enemySpd);

            motion = Live2DMotion.loadMotion(mtnFiles[1].bytes);
            motion.setLoop(true);
            motionManager.startMotion(motion, false);

            if (playerPos.transform.position.z <= -24.6f)
            {
                enemySpd = 0;
                attEnemyBool = false;
            }
        }


        if (Input.GetButtonDown("Fire2"))
        {
            motionManager.stopAllMotions();
            Debug.Log("Jump");
        }

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

    public static void attEnemy()
    {

         attEnemyBool = true;
    }
}