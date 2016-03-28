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

    public void Equip1()
    {
        for (int i = 0; i < 10; i++)
        {
            if (gameObject.name == "Weapon10"+i)
            {
                _useWeapon();
                StateManager.Instance.bagNum = i;
            }
            else if (gameObject.name == "Weapon20"+i)
            {
                _useWeapon();
                StateManager.Instance.bagNum = i;
            }
            else if (gameObject.name == "Weapon30" + i)
            {
                _useWeapon();
                StateManager.Instance.bagNum = i;
            }
            else if (gameObject.name == "Weapon50" + i)
            {
                _useWeapon();
                StateManager.Instance.bagNum = i;
            }
            else if (gameObject.name == "Weapon55" + i)
            {
                _useWeapon();
                StateManager.Instance.bagNum = i;
            }
            else if (gameObject.name == "Weapon3"+i)
            {
                _useWeapon();
                StateManager.Instance.bagNum = i;

            }
        }

    }

    public void _useWeapon()
    {
        itemUsePopBool = true;
    }
}
