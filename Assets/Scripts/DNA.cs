using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DNA : MonoBehaviour
{
    // gene for color 
    public float R;
    public float B;
    public float G;
    public float personScale;
    private bool _alive = true;
    //Records how long it has lived for
    public float timeToDie=0;

    SpriteRenderer sRenderer;
    Collider2D sCollider;
    Transform sTransform;

    private void OnMouseDown()
    {
        _alive = false;
        timeToDie = PopulateionManager.elapsed;
        print("Dead At: " + timeToDie);
        sRenderer.enabled = false;
        sCollider.enabled = false;
    }

    void Start()
    {

        //gets the Renderer and Collider of this particular person
        sRenderer = GetComponent<SpriteRenderer>();
        sCollider = GetComponent<Collider2D>();
        sRenderer.color = new Color(R, G, B);
        sTransform = GetComponent<Transform>();
        sTransform.localScale = new Vector3(personScale, personScale, 0);
    }


    void Update()
    {
        
    }
}
