using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public CharacterController controller;
    public Animator animator;
    public Camera cam;

    public float speed = 10f;
    public float dashDelay;
    public float dashSpeed;
    public float dashTime;
    private float dashColldown = 0;
    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        if (direction.magnitude > 0.1f)
        {
            //animator.SetTrigger("Run");
            animator.SetBool("Run", true);
            controller.Move(direction * speed * Time.deltaTime);
        }
        else
            animator.SetBool("Run", false);

        RotateWithMouseVector();

        if(Input.GetButton("Jump") && dashColldown <= 0)
        {
            StartCoroutine(Dash(direction));
        }
        dashColldown -= Time.deltaTime;
    }

    private void RotateWithMouseVector()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);

        if(Physics.Raycast(ray, out RaycastHit hit, maxDistance:100f))
        {
            Vector3 target = hit.point;
            target.y = transform.position.y;
            transform.LookAt(target);
        }
    }

    IEnumerator Dash(Vector3 dir)
    {
        float startTime = Time.time;

        while(Time.time < startTime + dashTime)
        {
            controller.Move(dir * dashSpeed * Time.deltaTime);

            yield return null;
        }
        dashColldown = dashDelay;
    }
}
