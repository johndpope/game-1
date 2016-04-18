using UnityEngine;
using System;
using System.Collections;
using live2d;
using live2d.framework;
using System.Text.RegularExpressions;

[ExecuteInEditMode]
public class live2d_setting : MonoBehaviour
{
    public TextAsset mocFile;
    public Texture2D[] textures;
    public TextAsset[] mtnFiles;

    public Live2DModelUnity live2DModel;

    public Live2DMotion motion;
    public MotionQueueManager motionManager;

    [Range(0.0f, 1.0f)]
    public float modelOpacity = 1.0f;

    // Use this for initialization
    void Start()
    {

        Live2D.init();

        motion = Live2DMotion.loadMotion(mtnFiles[0].bytes);

        motion.setLoop(true);
 
        live2DModel = Live2DModelUnity.loadModel(mocFile.bytes);
        for (int i = 0; i < textures.Length; i++)
        {
            live2DModel.setTexture(i, textures[i]);
        }
        
        motionManager = new MotionQueueManager();
      
        motionManager.startMotion(motion, false);

        live2DModel.setPartsOpacity("PARTS_WEAPON01", 0);
        live2DModel.setPartsOpacity("PARTS_WEAPON02", 0);
        live2DModel.setPartsOpacity("PARTS_WEAPON03", 0);
        live2DModel.setPartsOpacity("PARTS_WEAPON04", 0);
        live2DModel.setPartsOpacity("PARTS_WEAPON05", 0);
        live2DModel.setPartsOpacity("PARTS_WEAPON06", 0);
    }
 
    void Update()
    {

        

        if (Input.GetButtonDown("Fire1"))
        {
            op(1);
            motion = Live2DMotion.loadMotion(mtnFiles[2].bytes);
            //motion.setLoop(true);
            
            motionManager.startMotion(motion, false);
            Debug.Log("Fire1");
            //motionoff = true;
        }
        if (Input.GetButtonDown("Fire2"))
        {
            motion = Live2DMotion.loadMotion(mtnFiles[1].bytes);
            //motion.setLoop(true);
            motionManager.startMotion(motion, false);
           // motionManager.stopAllMotions();
            Debug.Log("Jump");
           // motionoff = true;
        }

       
        //if (Input.GetButtonDown("Fire1"))
        //{
        //    motion = Live2DMotion.loadMotion(mtnFiles[2].bytes);
        //    //motion.setLoop(true);
        //    motionManager.startMotion(motion, false);
        //    Debug.Log("Fire1");
        //    //motionoff = true;
        //}
        //if (Input.GetButtonDown("Fire2"))
        //{
        //    motion = Live2DMotion.loadMotion(mtnFiles[1].bytes);
        //    //motion.setLoop(true);
        //    motionManager.startMotion(motion, false);
        //   // motionManager.stopAllMotions();
        //    Debug.Log("Jump");
        //   // motionoff = true;
        //}


        float modelWidth = live2DModel.getCanvasWidth();
        Matrix4x4 m1 = Matrix4x4.Ortho(
                0, modelWidth, modelWidth,
                0, -50.0f, 50.0f);
        Matrix4x4 m2 = transform.localToWorldMatrix;
        Matrix4x4 m3 = m2 * m1;

        motionManager.updateParam(live2DModel);
        if (live2DModel == null) return;
        //live2DModel.setParamFloat("PARAM_WEAPON01", 1.0f);
        live2DModel.setMatrix(m3);
        live2DModel.update();
    }

   

    void OnRenderObject()
    {
        live2DModel.draw();

        var partList = live2DModel.getModelImpl().getPartsDataList();

        foreach (var item in partList)
        {
            live2DModel.setPartsOpacity(item.getPartsDataID().ToString(), modelOpacity);
            if (item.getPartsDataID().ToString() == "PARTS_WEAPON01")
                return;
            if (item.getPartsDataID().ToString() == "PARTS_WEAPON02")
                return;
            if (item.getPartsDataID().ToString() == "PARTS_WEAPON03")
                return;
            if (item.getPartsDataID().ToString() == "PARTS_WEAPON04")
                return;
            if (item.getPartsDataID().ToString() == "PARTS_WEAPON05")
                return;
            if (item.getPartsDataID().ToString() == "PARTS_WEAPON06")
                return;

        }
    }

    public void Weapon(int num)
    {
        switch(num)
        {
            case 0:
                live2DModel.setPartsOpacity("PARTS_WEAPON01", 1);
                break;
            case 1:
                live2DModel.setPartsOpacity("PARTS_WEAPON02", 1);
                break;
            case 2:
                live2DModel.setPartsOpacity("PARTS_WEAPON03", 1);
                break;
            case 3:
                live2DModel.setPartsOpacity("PARTS_WEAPON04", 1);
                break;
            case 4:
                live2DModel.setPartsOpacity("PARTS_WEAPON05", 1);
                break;
            case 5:
                live2DModel.setPartsOpacity("PARTS_WEAPON06", 1);
                break;
            case 6:
                live2DModel.setPartsOpacity("PARTS_WEAPON01", 0);
                live2DModel.setPartsOpacity("PARTS_WEAPON02", 0);
                live2DModel.setPartsOpacity("PARTS_WEAPON03", 0);
                live2DModel.setPartsOpacity("PARTS_WEAPON04", 0);
                live2DModel.setPartsOpacity("PARTS_WEAPON05", 0);
                live2DModel.setPartsOpacity("PARTS_WEAPON06", 0);
                break;

        }
            }

    

    public void Ani(int num)
    {
        switch (num)
        {
            case 0:
                motion = Live2DMotion.loadMotion(mtnFiles[num].bytes);
                motion.setLoop(true);
                motionManager.startMotion(motion, false);
                break;
            case 1:
                motion = Live2DMotion.loadMotion(mtnFiles[num].bytes);
                motion.setLoop(true);
                motionManager.startMotion(motion, false);
                break;
            case 2:
                motion = Live2DMotion.loadMotion(mtnFiles[num].bytes);
                motion.setLoop(true);
                motionManager.startMotion(motion, false);
                break;
            case 3:
                motion = Live2DMotion.loadMotion(mtnFiles[num].bytes);
                motion.setLoop(true);
                motionManager.startMotion(motion, false);
                break;
            case 4:
                motion = Live2DMotion.loadMotion(mtnFiles[num].bytes);
                motion.setLoop(true);
                motionManager.startMotion(motion, false);
                break;
            case 5:
                motion = Live2DMotion.loadMotion(mtnFiles[num].bytes);
                motion.setLoop(true);
                motionManager.startMotion(motion, false);
                break;
            case 6:
                motion = Live2DMotion.loadMotion(mtnFiles[num].bytes);
                motionManager.startMotion(motion, false);
                break;
        }

    }
}