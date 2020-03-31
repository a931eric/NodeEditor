using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandBlock :Block
{
    string command="";
    public override int ID { get { return 1; } }
    void Start()
    {
        GetComponentInChildren<Canvas>().worldCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
