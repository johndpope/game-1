using UnityEngine;
using System.Collections;

public class csCameraMove : MonoBehaviour {

    public GameObject caMove;
    public GameObject title;
    public GameObject startButton;

    // Use this for initialization
    void Start ()
    {
        if(StateManager.Instance.startButton.Equals(false))
        {
            iTween.MoveTo(caMove, iTween.Hash("path", iTweenPath.GetPath("caMove")
                                          , "time", 10.0f
                                          , "easetype", iTween.EaseType.linear
                                          , "looptype", iTween.LoopType.loop));
        }
        else
        {
            title.SetActive(false);
            startButton.SetActive(false);
            GameObject.Find("storeManager").GetComponent<csStoreManager>().onStartButton();
            StateManager.Instance.playHp = StateManager.Instance.playHpMax;
        }
    }    	
}
