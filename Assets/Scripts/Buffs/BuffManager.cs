using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffManager : MonoBehaviour
{
    private List<IBuff> buffList;
    public List<IBuff> BuffList { get; }

    private static BuffManager instance;
    public static BuffManager Instance
    {
        get
        {
            if (instance != null)
                return instance;
            return null;
        }
    }

    private void Awake()
    {
        if (instance != null)
            Destroy(this);

        instance = this;
    }
}
