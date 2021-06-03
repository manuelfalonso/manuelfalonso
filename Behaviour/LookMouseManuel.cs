using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookMouseManuel : MonoBehaviour {

    public float speed;
    public float rotationSpeed;
	
	// Update is called once per frame
	void Update () {

        float h = Input.GetAxis("Horizontal") * speed;
        float v = Input.GetAxis("Vertical") * speed;
        transform.Translate(h, v, 0, Space.World);

        Vector2 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);

	}
}
