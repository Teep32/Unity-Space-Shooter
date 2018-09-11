using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Evade : MonoBehaviour {

    public Vector2 startWait;
    public Vector2 moveTime;
    public Vector2 moveWait;
    public Boundary boundary;

    public float smooth;
    public float dodge;
    public float tilt;


    private float targetMove;
    private float currentSpeed;
    private Rigidbody rb;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
        currentSpeed = rb.velocity.z;
        StartCoroutine(Avoid());

	}
	

    IEnumerator Avoid ()
    {
        yield return new WaitForSeconds(Random.Range(startWait.x, startWait.y));

        while (true)
        {
            targetMove = Random.Range(1,dodge) * -Mathf.Sign(transform.position.x);
            yield return new WaitForSeconds(Random.Range(moveTime.x, moveTime.y));
            targetMove = 0;
            yield return new WaitForSeconds(Random.Range(moveWait.x,moveWait.y));
        }
    }

	// Update is called once per frame
	void FixedUpdate () {
        float newMove = Mathf.MoveTowards(rb.velocity.x, targetMove, Time.deltaTime * smooth);
        rb.velocity = new Vector3(newMove, 0.0f, currentSpeed);
        rb.position = new Vector3
            (
                Mathf.Clamp(rb.position.x, boundary.xMin, boundary.xMax),
                0.0f,
                Mathf.Clamp(rb.position.z, boundary.zMin, boundary.zMax)
            );
        rb.rotation = Quaternion.Euler(0.0f, 0.0f, rb.velocity.x * -tilt);
	}
}
