using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdController : MonoBehaviour
{
	private Transform targetFocus;

    void Start()
    {
      targetFocus = GameObject.FindGameObjectWithTag ("target").transform;  
    }

    void Update()
    {
      Vector3 target = targetFocus.position - this.transform.position;
      Debug.Log (target.magnitude); 
	
      
	if(target.magnitude < 1){
	tragetcollider.instance.moveTarget();
	}
	transform.LookAt (targetFocus.transform);
      float speed = Random.Range (15f, 30f);
      transform.Translate (0, 0, speed * Time.deltaTime);
    }
}
