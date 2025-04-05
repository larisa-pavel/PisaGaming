using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScalePlane : MonoBehaviour
{
    // Start is called before the first frame update
    private Vector2 screenResolution;
    void Start()
    {
        screenResolution = new Vector2(Screen.width, Screen.height);
        MatchPlaneToScreenSize();
    }

    // Update is called once per frame
    void Update()
    {
        if (screenResolution.x!= Screen.width||  screenResolution.y!= Screen.height)
        {
            MatchPlaneToScreenSize();
            screenResolution.x = Screen.width;
            screenResolution.y = Screen.height;
        }
    }
    private void MatchPlaneToScreenSize()
    {
        float planeToCameradistance = Vector3.Distance(gameObject.transform.position, Camera.main.transform.position);
        float planeHeightScale = (2.0f * Mathf.Tan(0.5f * Camera.main.fieldOfView * Mathf.Deg2Rad)* planeToCameradistance) / 10.0f;
        float planeWidthScale = planeHeightScale * Camera.main.aspect;
        gameObject.transform.localScale = new Vector3(planeWidthScale, 1, planeHeightScale);
    }
}
