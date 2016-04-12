using UnityEngine;
using System.Collections;

public class csSpdDown : MonoBehaviour
{
    int turn;
    int spdDownEnemy;
    float monsterSpd;

    // Use this for initialization
    void Start ()
    {
        turn = csBattle.turn;
        spdDownEnemy = csBattle.spdDownEnemy;
        monsterSpd = csBattle.monsterSpd;
        StartCoroutine(Down());
    }

    IEnumerator Down()
    {
        yield return new WaitForSeconds(20.0f);
        StateManager.Instance.monsterSpd[spdDownEnemy] = monsterSpd;
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        gameObject.GetComponent<csSpdDown>().enabled = false;
    }
}
