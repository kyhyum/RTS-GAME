using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cam : MonoBehaviour
{
    public float CamMoveSpeed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            transform.position -= new Vector3(0f, Input.GetAxis("Mouse Y") * CamMoveSpeed * Time.deltaTime, 0f);
            for (int i = 0; i < Input.touchCount; i++)
            {
                Touch t = Input.GetTouch(i);
                Vector3 prevPoint = t.position - t.deltaPosition;
                this.transform.position += new Vector3(0f, 0.000001f*(prevPoint.y - t.position.y)*Time.deltaTime, 0f);

            }
            float y = Mathf.Clamp(transform.position.y, 195f, 274f);
            this.transform.position = new Vector3(transform.position.x, y, transform.position.z);
        }
    }
}
