using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class csBattle : MonoBehaviour {

    public Scrollbar pcc;
    public Scrollbar ecc;
    public float pTimer = 10;
    public float eTimer = 10;
    float pTimer2;
    float eTimer2;
    public bool timerIsActive = true;

    public GameObject atkbtn;
    public GameObject runbtn;
    public GameObject itembtn;
    public GameObject player;


    // Use this for initialization
    void Start () {
        pTimer2 = pTimer;
        eTimer2 = eTimer;
    }
	
	// Update is called once per frame
	void Update ()
    {
        //cc.rectTransform.position.x();
        timer();

    }

    public void timer()
    {
        if (timerIsActive)
        {
            pTimer -= Time.deltaTime;
            eTimer -= Time.deltaTime;

            pcc.value += Time.deltaTime / pTimer2;
            ecc.value += Time.deltaTime / eTimer2;

            if (pTimer <= 0)
            {
                pTimer = 0;
                atkbtn.SetActive(true);
                runbtn.SetActive(true);
                itembtn.SetActive(true);
                //player.SetActive(false);
                timerIsActive = false;
            } 
            if(eTimer <= 0)
            {
                ecc.value = 0;
                eTimer = eTimer2;
            }           
        }
    }

    public void atk()
    {
        pcc.value = 0;
        atkbtn.SetActive(false);
        runbtn.SetActive(false);
        itembtn.SetActive(false);
        //player.SetActive(true);
        timerIsActive = true;
        pTimer = pTimer2;
    }

}
