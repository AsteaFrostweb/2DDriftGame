using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DriftGame.Utility;
using TMPro;

public class DebuggingUI : MonoBehaviour
{
    public int max_debug_console_lines = 72;
    private TextMeshProUGUI debugOutTMP;
    private TextMeshProUGUI debugInTMP;
    private GameObject debugPanel;
    private GameObject debugCanvas;
    private bool inputSelected;

    // Start is called before the first frame update
    void Start()
    {
        LocateObjects();
        SetupDevConsole();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.BackQuote) && !inputSelected)
        {          
            Debugging.ToggleDeveloperConsole();
        }
        if (Input.GetKeyDown(KeyCode.Return) && Debugging.isVisible) 
        {

        }
    }

    private void LocateObjects()
    {
        debugCanvas = GameObject.Find("DebugCanvas");
        debugOutTMP = GameObject.Find("DebugTextTMP").GetComponent<TextMeshProUGUI>();
        debugInTMP = GameObject.Find("InputTextTMP").GetComponent<TextMeshProUGUI>();
        debugPanel = GameObject.Find("DebugPanel");        
    }
    private void SetupDevConsole()
    {
        if (debugCanvas) DontDestroyOnLoad(debugCanvas);

        //Debugging.inEditor = Application.isEditor;
        Debugging.inEditor = false;
        Debugging.maxLines = max_debug_console_lines;
     
        Debugging.Initialize(debugInTMP, debugOutTMP, debugPanel);
    }

    public void SetInputFocus(bool val) 
    {
        inputSelected = val;
    }
}
