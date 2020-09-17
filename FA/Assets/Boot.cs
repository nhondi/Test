using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boot : MonoBehaviour
{
    #region Shooting
    [Header("Boot")]
    public bool shoot;

    public GameObject bootPivot;

    public Vector3 endRotation;
    public Vector3 startRotation;
    public float rotateSpeed = 0.5f;
    public float acceleration = 1f;
    public float deceleration = 1f;
    public float maxRotSpeed = 2f;
    #endregion
    #region Collider
    [Header("Colliders")]
    public Collider2D ground;
    public Collider2D boot;
    #endregion
    // Start is called before the first frame update
    void Start()
    {

    }
    void Update()
    {
        if (shoot == true && rotateSpeed < maxRotSpeed)
        {
            rotateSpeed = rotateSpeed + (acceleration * Time.deltaTime);
        }
        else
        {
            if (rotateSpeed > (deceleration * Time.deltaTime))
            {
                rotateSpeed = rotateSpeed - (deceleration * Time.deltaTime);
            }
            else if (rotateSpeed < (-deceleration * Time.deltaTime))
            {
                rotateSpeed = rotateSpeed + (deceleration * Time.deltaTime);
            }
            else
            {
                rotateSpeed = 0;
            }
        }
    }
    public void Shoot()
    {
        shoot = true;

        bootPivot.transform.rotation = Quaternion.RotateTowards(bootPivot.transform.rotation, Quaternion.Euler(endRotation), rotateSpeed);
    }
    public void ResetBoot()
    {
        shoot = false;
        rotateSpeed = 0.5f;
        bootPivot.transform.rotation = Quaternion.RotateTowards(bootPivot.transform.rotation, Quaternion.Euler(startRotation), rotateSpeed);
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == 8)
        {
            Physics2D.IgnoreCollision(collision.gameObject.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        }
    }
}
