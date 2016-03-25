using UnityEngine;
using System.Collections;

public class csNot : MonoBehaviour
{
    void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
    }
}
