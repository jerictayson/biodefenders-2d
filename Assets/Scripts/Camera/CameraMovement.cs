using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    
    [SerializeField]
    private Transform _target;
    [SerializeField]
    private float angle = 9f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // follow the player
        var position = _target.position;
        transform.position = new Vector3(position.x + angle, position.y, transform.position.z);
    }
}
