using UnityEngine;
using System.Collections;

public class csCameraMove : MonoBehaviour {

    public GameObject caMove;

	// Use this for initialization
	void Start ()
    {
        iTween.MoveTo(caMove, iTween.Hash("path", iTweenPath.GetPath("caMove")
                                          , "time", 10.0f
                                          , "easetype", iTween.EaseType.linear
                                          , "looptype", iTween.LoopType.loop));
    }    	

}
