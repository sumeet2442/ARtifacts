using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchInfo : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject infoPop;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Pressed Primary Button");
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100))
            {
                Debug.Log("hit");
                Debug.Log(hit.transform.name + ":" + hit.transform.tag);

                if (hit.transform.tag == "mars")
                {
                    Vector3 pos = hit.point;
                    pos.z += 0.25f;
                    pos.y += 0.25f;
                    Instantiate(infoPop, pos, transform.rotation);
                }

                else if (hit.transform.tag == "earth")
                {
                    Vector3 pos = hit.point;
                    pos.z += 0.40f;
                    pos.y += 0.40f;
                    Instantiate(infoPop, pos, transform.rotation);
                }

                else if (hit.transform.tag == "sphinx")
                {
                    Vector3 pos = hit.point;
                    pos.z += 0.25f;
                    pos.y += 0.25f;
                    Instantiate(infoPop, pos, transform.rotation);
                }


                else if (hit.transform.tag == "velociraptor")
                {
                    Vector3 pos = hit.point;
                    pos.z += 0.25f;
                    pos.y += 0.25f;
                    Instantiate(infoPop, pos, transform.rotation);
                }

                else if (hit.transform.tag == "teranodon")
                {
                    Vector3 pos = hit.point;
                    pos.z += 0.65f;
                    pos.y += 0.25f;
                    Instantiate(infoPop, pos, transform.rotation);
                }


                else if (hit.transform.tag == "earth_inf")
                {
                    Destroy(hit.transform.gameObject);
                }

                else if (hit.transform.tag == "mars_inf")
                {
                    Destroy(hit.transform.gameObject);
                }

            }
        }
    }
}
