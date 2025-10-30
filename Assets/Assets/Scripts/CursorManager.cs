using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class CursorManager : MonoBehaviour
{
    [SerializeField] private Texture2D cursorTexturechangeEvent;
    [SerializeField] private Texture2D cursorTexturedefault;

    private Vector2 cursorHotspot;
    
    public InputActionReference actionReference;
    
    void Start()
    {
        cursorHotspot = new Vector2(cursorTexturedefault.width / 2, cursorTexturedefault.height / 2);
        Cursor.SetCursor(cursorTexturedefault, cursorHotspot, CursorMode.Auto);
        GameWindowCursor();
    }

    public void cursorTexturechange()
    {

        cursorHotspot = new Vector2(cursorTexturechangeEvent.width / 2, cursorTexturechangeEvent.height / 2);
        Cursor.SetCursor(cursorTexturechangeEvent, cursorHotspot, CursorMode.Auto);

    }

    public void OnEnable()
    {
        actionReference.action.performed += ctx => cursorTexturechange();
    }
    
    public void OnDisable()
    {
        actionReference.action.performed -= ctx => cursorTexturechange();
    }

    public void LockCursor()
    {
        
        Cursor.lockState = CursorLockMode.Locked;
        
    }

    public void UnlockCursor()
    {

        Cursor.lockState = CursorLockMode.None;

    }

    public void GameWindowCursor()
    {

        Cursor.lockState = CursorLockMode.Confined;

    }

    public void Update()
    {
        if(actionReference.action.WasPerformedThisFrame())
        {
            
            UnlockCursor();
            
        }
    }
}