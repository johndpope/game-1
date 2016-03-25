using UnityEngine;
using System.Collections;

public class csPlayerData : MonoBehaviour
{
    GameObject weaponObj;
    public GameObject grid;

	void Awake ()
    {
        for (int i = 0; i < 10; i++)
        {
            weaponObj = StateManager.Instance.weaponSpace[i];
            weaponObj.transform.SetParent(grid.transform);
            weaponObj.transform.localScale = new Vector3(1, 1, 1);
            if (StateManager.Instance.weaponSpace[i] == null)
            {
                return;
            }
        }

       
        //weaponObj.name = "Weapon10" + "-" + wNum;


    }

    // Update is called once per frame
    void Update ()
    {
	
	}
}
