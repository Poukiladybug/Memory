using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBehaviour : MonoBehaviour
{
    public LevelManager manager;
    public int id = -1;

    public bool mouseOver;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonUp(0) && mouseOver)
        {
            manager.RevealMaterial(id);
        }
    }

    private void OnMouseOver()
    {
        transform.localScale = new Vector3(1, 2, 1);
        mouseOver = true;
    }

    private void OnMouseExit()
    {
        transform.localScale = Vector3.one;
        mouseOver = false;
    }

    
}
