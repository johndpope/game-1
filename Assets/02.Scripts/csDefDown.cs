using UnityEngine;
using System.Collections;

public class csDefDown : MonoBehaviour
{
    int turn;
    int defDownEnemy;
    float monsterDef;

    // Use this for initialization
    void Start()
    {
        turn = csBattle.turn;
        defDownEnemy = csBattle.defDownEnemy;
        monsterDef = csBattle.monsterDef;
        StartCoroutine(Down());
    }

    IEnumerator Down()
    {

        yield return new WaitForSeconds(20.0f);
        StateManager.Instance.monsterDef[defDownEnemy] = csBattle.monsterDef;
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        gameObject.GetComponent<csDefDown>().enabled = false;
    }
}
