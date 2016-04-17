using UnityEngine;
using System.Collections;

public class SortingLayerSetter : MonoBehaviour
{

    [SerializeField]
    private string _sortingLayerName = "Default";

    // Use this for initialization 
    void Start()
    {
        gameObject.GetComponent<Renderer>().sortingLayerName = _sortingLayerName;
    }
}
