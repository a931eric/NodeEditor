using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;

public class Player : MonoBehaviour
{

    public List<Block> blocks=new List<Block>();
    public int selBlockIdx = 0;
    public World world;
    Transform cam;
    public float maxDist = 100;
    void Start()
    {
        cam=transform.GetChild(0);
    }
    public Transform longPressing=null;
    public float longPressTime, curLongPressTime;
    void Update()
    {
        
        Ray ray = new Ray(cam.position,cam.forward);
        Physics.Raycast(ray,out RaycastHit hit, maxDist,1<<9);
        
        if (Input.GetMouseButtonDown(1))
        {
            if (hit.collider!=null) {
                world.CreateBlock( blocks[selBlockIdx], hit.point + hit.normal / 2);
            }
        }
        if(Input.GetMouseButton(0))
        {
            if (hit.collider != null)
            {
                
                if (longPressing != hit.transform)
                {
                    curLongPressTime = 0;
                    longPressing = hit.transform;
                }
                if (longPressing == null) longPressing = hit.transform;
                curLongPressTime += Time.deltaTime;
                if (curLongPressTime > longPressTime)
                {
                    curLongPressTime = 0;
                    if (longPressing.gameObject.GetComponentInParent<Block>() == null)
                    {
                        
                        return;
                    }
                    world.DestroyBlock(longPressing.gameObject.GetComponentInParent<Block>());
                }
            }
        }
        else
        {
            longPressing = null;
        }
    }
}
