using UnityEngine;
using System.Collections;
using live2d;

public class EnemyControler : MonoBehaviour {


    public int enemyUP =3;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        gameObject.GetComponent<live2d_setting>().motion = Live2DMotion.loadMotion(gameObject.GetComponent<live2d_setting>().mtnFiles[0].bytes);
        gameObject.GetComponent<live2d_setting>().motionManager.startMotion(gameObject.GetComponent<live2d_setting>().motion, false);
        if (Input.GetButtonDown("Fire1"))
        {
            //gameObject.GetComponent<main1>().mtnFiles[0].bytes =
            gameObject.GetComponent<live2d_setting>().motion = Live2DMotion.loadMotion(gameObject.GetComponent<live2d_setting>().mtnFiles[1].bytes);
            //gameObject.GetComponent<live2d_setting>().motion.setLoop(true);
            gameObject.GetComponent<live2d_setting>().motionManager.startMotion(gameObject.GetComponent<live2d_setting>().motion, false);
            enemyUP -= 1;
            if (enemyUP == 0) {
                onIdle();
            }
            
            Debug.Log("Fire1");
        }

        if (Input.GetButtonDown("Fire2"))
        {
            //motionManager.stopAllMotions();
            Debug.Log("Jump");
        }
        
    }



    public void onIdle()
    {
        //iTween.MoveBy(gameObject, iTween.Hash("x", 1, "easeType", "", "loopType", "", "delay", .1));
        gameObject.GetComponent<live2d_setting>().motion = Live2DMotion.loadMotion(gameObject.GetComponent<live2d_setting>().mtnFiles[2].bytes);
        gameObject.GetComponent<live2d_setting>().motionManager.startMotion(gameObject.GetComponent<live2d_setting>().motion, false);
        enemyUP = 3;

    }
    

}
