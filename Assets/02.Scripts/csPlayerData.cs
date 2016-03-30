using UnityEngine;
using System.Collections;

public class csPlayerData : MonoBehaviour
{
    GameObject weaponObj;
    GameObject scrollObj;
    GameObject itemObj;

    public GameObject wGrid;
    public GameObject sGrid;
    public GameObject iGrid;
    int i=0;
  
    void Awake()
    {
        for(i = 0; i < 6; i++)
        {
            if (StateManager.Instance.weaponSpace[i] == null)
            {
                Debug.Log("들어옴" + i);
            }
            else if (StateManager.Instance.weaponSpace[i] != null)
            {
                weaponObj = StateManager.Instance.weaponSpace[i];
                weaponObj.transform.SetParent(wGrid.transform);
                weaponObj.transform.localScale = new Vector3(1, 1, 1);
            }
        }
        for (i = 0; i < 15; i++)
        {
            if (StateManager.Instance.itemSpace[i] == null)
            {

            }
            else if (StateManager.Instance.itemSpace[i] != null)
            {
                itemObj = StateManager.Instance.itemSpace[i];
                itemObj.transform.SetParent(iGrid.transform);
                itemObj.transform.localScale = new Vector3(1, 1, 1);
            }
        }
        for (i = 0; i < 20; i++)
        {
            if (StateManager.Instance.scrollSpace[i] == null)
            {

            }

            else if (StateManager.Instance.scrollSpace[i] != null)
            {
                itemObj = StateManager.Instance.scrollSpace[i];
                itemObj.transform.SetParent(sGrid.transform);
                itemObj.transform.localScale = new Vector3(1, 1, 1);
            }
        }
           
        
        //weaponObj.name = "Weapon10" + "-" + wNum;


    }

    // Update is called once per frame
    void Update ()
    {
	
	}
}
