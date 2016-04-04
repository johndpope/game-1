using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class csPlayerData : MonoBehaviour
{
    GameObject weaponObj;
    GameObject scrollObj;
    GameObject itemObj;

    public GameObject wGrid;
    public GameObject sGrid;
    public GameObject iGrid;

    GameObject[] weaponSpace;

    GameObject[] potionItemBag;
    GameObject[] SkScrollBag;
    GameObject[] MgScrollBag;
    GameObject[] BufScrollBag;

    public GameObject playerHpText;
    public GameObject playerAtkText;
    public GameObject playerDefText;
    public GameObject playerSpdText;
    public GameObject playerGoldText;


    int i =0;
  
    void Awake()
    {
        weaponSpace = StateManager.Instance.weaponSpace;

        potionItemBag = StateManager.Instance.potionItemBag;
        SkScrollBag = StateManager.Instance.SkScrollBag;
        MgScrollBag = StateManager.Instance.MgScrollBag;
        BufScrollBag = StateManager.Instance.BufScrollBag;

        for (i = 0; i < weaponSpace.Length; i++)
        {
            if (weaponSpace[i] == null)
            {
                Debug.Log("들어옴" + i);
            }
            else if (weaponSpace[i] != null)
            {
                weaponObj = weaponSpace[i];
                weaponObj.transform.SetParent(wGrid.transform);
                weaponObj.transform.localScale = new Vector3(1, 1, 1);
            }
        }

        for (i = 0; i < potionItemBag.Length; i++)
        {
            if (potionItemBag[i] == null)
            {

            }
            else if (potionItemBag[i] != null)
            {
                itemObj = potionItemBag[i];
                itemObj.transform.SetParent(iGrid.transform);
                itemObj.transform.localScale = new Vector3(1, 1, 1);
            }
        }
        for (i = 0; i < SkScrollBag.Length; i++)
        {
            if (SkScrollBag[i] == null)
            {

            }
            else if (SkScrollBag[i] != null)
            {
                itemObj = SkScrollBag[i];
                itemObj.transform.SetParent(sGrid.transform);
                itemObj.transform.localScale = new Vector3(1, 1, 1);
            }
        }
        for (i = 0; i < MgScrollBag.Length; i++)
        {
            if (MgScrollBag[i] == null)
            {

            }
            else if (MgScrollBag[i] != null)
            {
                itemObj = MgScrollBag[i];
                itemObj.transform.SetParent(sGrid.transform);
                itemObj.transform.localScale = new Vector3(1, 1, 1);
            }
        }

        for (i = 0; i < BufScrollBag.Length; i++)
        {
            if (BufScrollBag[i] == null)
            {

            }
            else if (BufScrollBag[i] != null)
            {
                itemObj = BufScrollBag[i];
                itemObj.transform.SetParent(sGrid.transform);
                itemObj.transform.localScale = new Vector3(1, 1, 1);
            }
        }


        playerGoldText.GetComponent<Text>().text = "" + StateManager.Instance.playGold;

        playerHpText.GetComponent<Text>().text = "" + StateManager.Instance.playHp;
        playerAtkText.GetComponent<Text>().text = "" + StateManager.Instance.playAtk + " + " + StateManager.Instance.playUseAtk;
        playerDefText.GetComponent<Text>().text = "" + StateManager.Instance.playDef + " + " + StateManager.Instance.playUseDef;
        playerSpdText.GetComponent<Text>().text = "" + StateManager.Instance.playSpd + " + " + StateManager.Instance.playUseSpd;
    }

    void Update ()
    {
	
	}
}
