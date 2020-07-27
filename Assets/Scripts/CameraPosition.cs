using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPosition : MonoBehaviour
{
    [SerializeField]
    List<GameObject> ChildCams;

    // Start is called before the first frame update
    void Start()
    {
        int w = Screen.width;
        int h = Screen.height;
        float a = (float)w / (float)h;

        //adjusting a position of other cameras to align with different screen resolutions
        //used seperate cameras for transitioning seamlessly when looping across the screen
        foreach (GameObject inst in ChildCams)
        {
            if(inst.name == "LeftCam")
            {
                inst.transform.position = new Vector3(-a * 10, 0, transform.position.z);
            }            
            if(inst.name == "RightCam")
            {
                inst.transform.position = new Vector3(a * 10, 0, transform.position.z);
            }            
            if(inst.name == "UpperCam")
            {
                inst.transform.position = new Vector3(0, 10, transform.position.z);
            }            
            if(inst.name == "LowerCam")
            {
                inst.transform.position = new Vector3(0, -10, transform.position.z);
            }            
            if(inst.name == "BottomLeftCam")
            {
                inst.transform.position = new Vector3(-a * 10, -10, transform.position.z);
            }            
            if(inst.name == "BottomRightCam")
            {
                inst.transform.position = new Vector3(a * 10, -10, transform.position.z);
            }            
            if(inst.name == "TopLeftCam")
            {
                inst.transform.position = new Vector3(-a * 10, 10, transform.position.z);
            }            
            if(inst.name == "TopRightCam")
            {
                inst.transform.position = new Vector3(a * 10, 10, transform.position.z);
            }            
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
