using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inputs : MonoBehaviour
{
    Camera cam;
    [SerializeField] LayerMask ClickLayer;
    private void Start() {
        cam = Camera.main;
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) {
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = 100;
            mousePos = cam.ScreenToWorldPoint(mousePos);

            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if(Physics.Raycast(ray, out hit ,100 , ClickLayer)) {
                if (hit.transform.gameObject.GetComponent<ButtonController>() != null)
                    hit.transform.gameObject.GetComponent<ButtonController>().Press();
                else if(hit.transform.gameObject.GetComponent<Places>() != null)
                    hit.transform.gameObject.GetComponent<Places>().Press();
            }
        }
    }
}
