using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptKnifeLauncher : MonoBehaviour
{
    public LaunchKnife LaunchKnife;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S)) LaunchKnife.LauncherKnife();
    }
}
