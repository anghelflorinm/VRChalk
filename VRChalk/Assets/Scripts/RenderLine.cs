using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Cryptography;
using UnityEngine;
using VRTK;

public class RenderLine : MonoBehaviour
{
    public GameObject gameObject;
    public VRTK_InteractableObject interactableObject;
    bool isGrabbed = false;
    LineRenderer trailLine;

    // Start is called before the first frame update
    void Start()
    {
        //initializez LineRender-ul
        trailLine = gameObject.AddComponent<LineRenderer>();
        trailLine.material = new Material(Shader.Find("Sprites/Default"));
        trailLine.startColor = trailLine.endColor = Color.white;
        trailLine.startWidth = trailLine.endWidth = 0.10f;
        isGrabbed = false;

        //adaug evenimentele de grab si release
        interactableObject.InteractableObjectGrabbed += HandleGrab;
        interactableObject.InteractableObjectUngrabbed += HandleRelease;
    }

    // Update is called once per frame
    void Update()
    {
        if (isGrabbed)
        {
            //daca este apucat, generez urmatorul punct
            int currentCount = trailLine.positionCount;
            trailLine.positionCount++;
            trailLine.SetPosition(currentCount, gameObject.transform.position);
        }
    }
    void HandleGrab(object sender, InteractableObjectEventArgs e)
    {
        //reiau o noua linie si setez primul punct
        isGrabbed = true;
        trailLine.positionCount = 1;
        trailLine.SetPosition(0, gameObject.transform.position);
    }
    void HandleRelease(object sender, InteractableObjectEventArgs e)
    {
        //sterg linia generata
        isGrabbed = false;
        trailLine.positionCount = 0;
    }
}
