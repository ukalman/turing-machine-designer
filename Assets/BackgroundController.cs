using System;
using System.Collections;
using System.Collections.Generic;
using Managers;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundController : MonoBehaviour
{
    [SerializeField]
    protected Image backgroundImage;


    private void Start()
    {
        GameManager.Instance.OnGameStart += OnGameStart;
        GameManager.Instance.OnTMDesign += OnTMDesign;
        GameManager.Instance.OnTMStateRules += OnTMStateRules;
    }

    private void OnDestroy()
    {
        GameManager.Instance.OnTMDesign -= OnTMDesign;
        GameManager.Instance.OnGameStart -= OnGameStart;
        GameManager.Instance.OnTMStateRules -= OnTMStateRules;
    }

    private void OnGameStart()
    {
        Sprite newBackground = Resources.Load<Sprite>("Sprites/Main-Menu-BG");
        
        if (newBackground != null)
        {
            backgroundImage.sprite = newBackground;
            backgroundImage.gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
        }
        else
        {
            Debug.LogError("Failed to load the background sprite. Check the path and name.");
        }
    }
    
    
    private void OnTMDesign()
    {
        Sprite newBackground = Resources.Load<Sprite>("Sprites/Background-1");
        
        if (newBackground != null)
        {
            backgroundImage.sprite = newBackground;
            backgroundImage.gameObject.transform.localScale = new Vector3(1f, 1.12f, 1f);
        }
        else
        {
            Debug.LogError("Failed to load the background sprite. Check the path and name.");
        }
    }

    private void OnTMStateRules()
    {
        Sprite newBackground = Resources.Load<Sprite>("Sprites/Background-5");
        
        if (newBackground != null)
        {
            backgroundImage.sprite = newBackground;
            backgroundImage.gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
        }
        else
        {
            Debug.LogError("Failed to load the background sprite. Check the path and name.");
        }
    }
    
    
    
    
    
    
}
