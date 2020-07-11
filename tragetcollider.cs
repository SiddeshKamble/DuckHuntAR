using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class tragetcollider : DefaultTrackableEventHandler 
{
   	public static tragetcollider instance;

	void Awake(){
	if(instance == null)
	{instance = this;}
	}
	


	void OnTriggerEnter(Collider other)
   {
	moveTarget ();
   }
	public void moveTarget()
	{
	Vector3 temp;
	temp.x = Random.Range (-45f, 45f);
	temp.y = Random.Range (10f, 45f);
	temp.z = Random.Range (-45f, 45f);
	transform.position = new Vector3(temp.x, temp.y, temp.z);

        if (DefaultTrackableEventHandler.trueFalse == true)
        {
            RaycastController.instance.playSound(0);
        }
	}
}
