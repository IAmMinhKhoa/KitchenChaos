using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadCallBack : MonoBehaviour
{
    protected bool isFirstUpdate = true;

    private void Update()
    {
        if (isFirstUpdate)
        {
            isFirstUpdate = false;
            Loader.LoaderCallBack();
        }
    }
}
