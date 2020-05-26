using System;
using UnityEngine;

public class PlayerGraber : MonoBehaviour
{
    [SerializeField] private GameObject hand;
    [SerializeField] private Rigidbody2D playerRb;
    private IGraberable _graberable;
    private IGraberable _grabedBody;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        IGraberable tempGrable = other.GetComponent<IGraberable>();
        if (tempGrable != null)
            _graberable = tempGrable;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        IGraberable tempGrable = other.GetComponent<IGraberable>();
        if (tempGrable != null)
            _graberable = null;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (_grabedBody == null)
            {
                if(_graberable != null)
                    GrabBody();   
            }
            else
            {
                DropBody();
            }
        }
    }

    private void GrabBody()
    {
        bool grabed = _graberable.Grab(gameObject);
        if(!grabed)
            return;
        
        _grabedBody = _graberable;
        _grabedBody.body.transform.SetParent(hand.transform);
        _grabedBody.body.transform.localPosition = Vector2.zero;
        _grabedBody.body.velocity = Vector2.zero;
        _grabedBody.body.angularVelocity = 0;
        _grabedBody.body.bodyType = RigidbodyType2D.Kinematic;
        
        _grabedBody.body.GetComponent<Collider2D>().enabled = false;
    }
    private void DropBody()
    {
        bool droped = _grabedBody.Drop(gameObject);
        if(!droped)
            return;

        _grabedBody.body.transform.SetParent(null);
        _grabedBody.body.bodyType = RigidbodyType2D.Dynamic;
        _grabedBody.body.velocity = playerRb.velocity;
        _grabedBody.body.GetComponent<Collider2D>().enabled = true;
        
        _grabedBody = null;
    }
}