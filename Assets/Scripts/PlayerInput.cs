using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    private AudioSource as_;
    public AudioClip[] smashSounds;
    public GameObject BloodEffect;

    //Awake is for getting audioSource

    void Awake()
    {
        as_ = GetComponent<AudioSource>();
    }


    void Update()
    {
        HitEnemy();
    }

    /*Hit Enemy will
    first checks if input from mouse button true or false then
    with the use of ray casting check collision bet enemy and raypos 
    if true then called kill enemy and shakes screen by animator and display blood effect*/

    private void HitEnemy()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

            if (hit.collider != null)
            {
                if (hit.collider.tag == "Enemy")
                {
                    gameObject.GetComponent<GameManager>().killEnemy();

                    as_.clip = smashSounds[Random.Range(0, smashSounds.Length)];
                    as_.PlayOneShot(as_.clip, 0.5f);
                    Camera.main.GetComponent<Animator>().SetTrigger("Shake");
                    DislpayBloodEffect(Camera.main.ScreenToWorldPoint(Input.mousePosition));


                }
            }
        }

    }

    //this fun will display blood effect at pos given by hitenemy fun and also run animation for smashed
    private void DislpayBloodEffect(Vector2 pos)
    {
        BloodEffect.transform.position = pos;
        BloodEffect.GetComponent<Animator>().SetTrigger("Smashed");
    }
}
