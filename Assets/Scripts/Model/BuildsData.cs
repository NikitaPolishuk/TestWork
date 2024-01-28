using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildsData : MonoBehaviour
{
    [SerializeField]
    private GameObject[] _buildsData;

    public GameObject GetBuildWithId(int index)
    {
        try
        {
            return _buildsData[index];
        }
        catch
        {
            return null;
        }
    }
}
