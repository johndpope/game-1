using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class csdungeonManager : MonoBehaviour
{
    public GameObject levelgrid;
    public GameObject floor;

    GameObject[] floorPoolSet = new GameObject[30];


    //private ArrayList nMaps;
    //private ArrayList nLevel;
    void Start ()
    {
        //nMaps = StateManager.Instance.dungeonMapList;
        //nLevel = StateManager.Instance.dungeonLevelList;

        for (int i = 0; i < 30; i++)
        {
            _setupFloor(i);
        }
    }
	
	void Update ()
    {

    }

    public void Floor(int level)
    {

        //StateManager.Instance.dungeonMap= ;
        StateManager.Instance.dungeonLevel = level;
        Application.LoadLevel(1);
    }

    private void _setupFloor(int levelNum)
    {
        floorPoolSet[levelNum] = Instantiate(floor) as GameObject;
        floorPoolSet[levelNum].transform.SetParent(levelgrid.transform);
        floorPoolSet[levelNum].transform.localScale = new Vector3(1, 1, 1);

        floorPoolSet[levelNum].GetComponent<Button>().onClick.AddListener(delegate { Floor(levelNum); });

        floorPoolSet[levelNum].SetActive(false);
    }

    public void firstFloor()
    {
        int i = 0;
        for (i = 0; i < 9; i++)
        {
            floorPoolSet[i].SetActive(true);
            floorPoolSet[i+9].SetActive(false);
            floorPoolSet[i+18].SetActive(false);
        }
        
    }
    public void secondFloor()
    {
        int i = 0;
        for (i = 0; i < 9; i++)
        {
            floorPoolSet[i].SetActive(false);
            floorPoolSet[i + 9].SetActive(true);
            floorPoolSet[i + 18].SetActive(false);
        }
    }
    public void thirdFloor()
    {
        int i = 0;
        for (i = 0; i < 9; i++)
        {
            floorPoolSet[i].SetActive(false);
            floorPoolSet[i + 9].SetActive(false);
            floorPoolSet[i + 18].SetActive(true);
        }
    }
}
