using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScreenResolution : MonoBehaviour
{
    public bool maintainWidth = true;
    [Range(-1,1)]
    public int adaptPosition;

    float defaultWidth;
    float defaultHeight;

    Vector3 CameraPos;
    // Start is called before the first frame update
    void Start()
    {
        CameraPos = UnityEngine.Camera.main.transform.position;

        defaultHeight = UnityEngine.Camera.main.orthographicSize;
        defaultWidth = UnityEngine.Camera.main.orthographicSize * UnityEngine.Camera.main.aspect;
    }

    // Update is called once per frame
    void Update()
    {
        if(maintainWidth)
        {
            UnityEngine.Camera.main.orthographicSize = defaultWidth / Camera.main.aspect;

            UnityEngine.Camera.main.transform.position = new Vector3(CameraPos.x,adaptPosition* (defaultHeight - UnityEngine.Camera.main.orthographicSize + CameraPos.y), CameraPos.z);
        } else
        {
            UnityEngine.Camera.main.transform.position = new Vector3(adaptPosition * (defaultWidth - UnityEngine.Camera.main.orthographicSize * Camera.main.aspect + CameraPos.x),CameraPos.y, CameraPos.z);
        }
    }
}
