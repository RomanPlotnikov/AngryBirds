using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Bird : MonoBehaviour
{
    [SerializeField] private bool isPressed = false;

    [SerializeField] public GameObject BirdPrefab;


    [SerializeField] private Rigidbody2D BirdRigid;
    [SerializeField] public Rigidbody2D ShootRigid;

    [SerializeField] private float maxDistance = 2f;
    private void Start()
    {
        BirdRigid = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        if (isPressed  == true)
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            if (Vector2.Distance(mousePos, ShootRigid.position) > maxDistance)
            {
                BirdRigid.position = ShootRigid.position + (mousePos - ShootRigid.position).normalized * maxDistance;
            }
            else
            {
                BirdRigid.position = mousePos;
            }
        }
    }
    private void OnMouseDown()
    {
        isPressed = true;
        BirdRigid.isKinematic = true;
    }
    private void OnMouseUp()
    {
        isPressed = false;
        BirdRigid.isKinematic = false;

        StartCoroutine(LetGo());
    }
    IEnumerator LetGo()
    {
        yield return new WaitForSeconds(0.1f);

        gameObject.GetComponent<SpringJoint2D>().enabled = false;
        this.enabled = false;
        Destroy(gameObject, 4f);

        yield return new WaitForSeconds(2f);
        if (BirdPrefab != null) 
        {
            BirdPrefab.transform.position = ShootRigid.position;
        }
        else
        {
            SceneManager.LoadScene(0);
        }
    }
}
