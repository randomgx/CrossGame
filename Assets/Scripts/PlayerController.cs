using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody _rb;
    private Animator _anim;

    public GameObject ragdoll;
    public GameObject mesh;

    public float speed;
    public Sector currentSector;

    public float move;
    public float strafe;

    private bool controlLock;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _anim = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        move = -Input.GetAxis("Vertical");
        strafe = Input.GetAxis("Horizontal");
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        if (controlLock)
            return;

        var direction = new Vector3(move, _rb.velocity.y, strafe);
        _rb.velocity = direction * speed;
        _anim.SetFloat("speed", _rb.velocity.magnitude);
        _anim.SetFloat("velocityForward", _rb.velocity.x);
        _anim.SetFloat("velocitySideways", _rb.velocity.z);
    }

    private void Respawn()
    {
        controlLock = true;
        mesh.SetActive(false);

        //ragdoll.SetActive(true);
        //Camera.main.GetComponent<CameraController>().CameraDie();

        StartCoroutine(Spawn());
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("SectorTrigger"))
        {
            currentSector = other.GetComponentInParent<Sector>();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.CompareTag("Car"))
        {
            Respawn();
        }
    }

    IEnumerator Spawn()
    {
        yield return new WaitForSeconds(1);

        //ragdoll.SetActive(false);
        //ragdoll.transform.position = Vector3.zero;

        mesh.SetActive(true);
        _anim.Play("Idle");
        transform.position = currentSector.playerRespawnPoints[Random.Range(0, currentSector.playerRespawnPoints.Length)].position;

        //Camera.main.GetComponent<CameraController>().CameraReset();

        controlLock = false;
    }
}
