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
    public TextAsset modelJson;

    public bool motionoff = false;

 
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


        //live2DModel.
    }
 
    void Update()
    {
        if (motionoff == true)
        {
            motion = Live2DMotion.loadMotion(mtnFiles[0].bytes);
            motion.setLoop(true);
            motionManager.startMotion(motion, false);
            motionoff = false;
        }
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

        float modelWidth = live2DModel.getCanvasWidth();
        Matrix4x4 m1 = Matrix4x4.Ortho(
                0, modelWidth, modelWidth,
                0, -50.0f, 50.0f);
        Matrix4x4 m2 = transform.localToWorldMatrix;
        Matrix4x4 m3 = m2 * m1;

        motionManager.updateParam(live2DModel);
        if (live2DModel == null) return;
        live2DModel.setParamFloat("PARAM_WEAPON01", 1.0f);
        live2DModel.setMatrix(m3);
        live2DModel.update();
    }

    void OnRenderObject()
    {
        live2DModel.draw();
    }

    private void op(int num)
    {
        live2DModel.setPartsOpacity("PARTS_WEAPON01", num);
    }
}