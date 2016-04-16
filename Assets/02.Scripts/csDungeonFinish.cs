using UnityEngine;
using System.Collections;

public class csDungeonFinish : MonoBehaviour
{
    public GameObject finishPop;
    public GameObject state;

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            //finishPop.SetActive(true);
            village();
        }
    }

    public void nextLaval()
    {
        StateManager.Instance.dungeonMap++;
        StateManager.Instance.dungeonLevel++;
        //finishPop.SetActive(false);
        Application.LoadLevel(1);
    }

    public void village()
    {
        //finishPop.SetActive(false);
        StateManager.Instance.dunFinish = true;
        Application.LoadLevel(0);
    }
}
