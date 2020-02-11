using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    private Vector3 scale = new Vector3(0.0001f, 0.0001f, 0.0001f);
    private Vector3 originalScale;
    public int cameraControll;
    [SerializeField] private bool canChangeScale = false;

    private void Start()
    {        
        originalScale = gameObject.transform.localScale;
    }

    void OnMouseEnter()
    {
        if (canChangeScale)
        {
            gameObject.transform.localScale = originalScale + scale;
        }
    }

    void OnMouseExit()
    {
        if (canChangeScale)
        {
            gameObject.transform.localScale = originalScale;
        }
    }

}
