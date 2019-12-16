using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerContainerScript : MonoBehaviour
{

    public GameObject playerCapsule;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = playerCapsule.transform.position;
    }
}
