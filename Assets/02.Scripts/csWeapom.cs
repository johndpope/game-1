using UnityEngine;
using System.Collections;



public class csWeapom : MonoBehaviour
{

    public static bool itemUsePopBool;
    public static int weaponNum;

    // Use this for initialization
    void Start()
    {
        itemUsePopBool = false;
    }
	
	// Update is called once per frame
	void Update ()
    {
	
	}

    public void Equip1(int itemIndex)
    {
        for (int i = 0; i < 10; i++)
        {
            if (gameObject.name == "Weapon10"+i)
            {
                _useWeapon(0);
                StateManager.Instance.bagNum = i;
            }
            else if (gameObject.name == "Weapon50"+i)
            {
                _useWeapon(1);
                StateManager.Instance.bagNum = i;

            }
            else if (gameObject.name == "Weapon3"+i)
            {
                _useWeapon(2);

            }
        }

    }

    public void _useWeapon(int itemIndex)
    {
        switch(itemIndex)
        {
            case 0:
                Debug.Log("클릭됨     10");

                itemUsePopBool = true;
                weaponNum = 0;

                break;
            case 1:
                Debug.Log("클릭됨     50");

                itemUsePopBool = true;
                weaponNum = 1;
                break;
        }
    }
}
