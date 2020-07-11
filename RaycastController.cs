using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RaycastController : MonoBehaviour{
   
	public float maxDistanceRay = 100f;
	public static RaycastController instance;
	public Transform gunFlashTarget;
	public float fireRate = 1.6f;
	private bool nextShot = true;
	private string objName = "";


	AudioSource audio;
	public AudioClip[] clips;

	void Awake()
     {
	if(instance == null){ 
	   instance = this; 
        }
     }


 	// Start is called before the first frame update
   	 void Start() {
         StartCoroutine (spawnNewBird ()); 
	 audio = GetComponent<AudioSource> (); 
    }

	public void playSound(int sound){
	audio.clip = clips[sound];
	audio.Play ();
	}

   
	 // Update is called once per frame
    	void Update() {
        
	 }
	
	public void Fire(){
	if(nextShot){
		StartCoroutine (takeShot ());
		nextShot = false;
		}
	}
	
	private IEnumerator takeShot(){
		
	gunScript.instance.fireSound ();
	Ray ray = Camera.main.ViewportPointToRay (new Vector3 (0.5f, 0.5f, 0));
	RaycastHit hit;
        gameController.instance.shotsPerRound--;

	int layer_mask = LayerMask.GetMask ("birdLayer");
	if(Physics.Raycast(ray, out hit, maxDistanceRay, layer_mask)){

	    objName = hit.collider.gameObject.name;
	
	    Vector3 birdPosition = hit.collider.gameObject.transform.position;

	      if(objName == "Bird_Asset(Clone)"){
	         GameObject Boom = Instantiate(Resources.Load("boom", typeof(GameObject))) as GameObject;
	            Boom.transform.position = birdPosition;
	            playSound(1);
	            Destroy(hit.collider.gameObject);
	            StartCoroutine(spawnNewBird ());
	            StartCoroutine(clearBoom ());
            gameController.instance.shotsPerRound = 3;
                gameController.instance.playerScore++;
                gameController.instance.roundScore++;
            }


	}

	GameObject gunFlash = Instantiate(Resources.Load("gunFlashSmoke", typeof(GameObject))) as GameObject;
	    gunFlash.transform.position = gunFlashTarget.position;	

	    yield return new WaitForSeconds (fireRate);

	    nextShot = true;
	
	    GameObject[] gunSmokeGroup = GameObject.FindGameObjectsWithTag ("GunSmoke");
	    foreach (GameObject theSmoke in gunSmokeGroup){
	    	Destroy (theSmoke.gameObject);
	    	}	
	
	    }
	
	private IEnumerator clearBoom(){
	yield return new WaitForSeconds (1.5f);
	
	GameObject[] smokeGroup = GameObject.FindGameObjectsWithTag ("Boom");
	foreach (GameObject smoke in smokeGroup){
		Destroy (smoke.gameObject);
		}	
	

	}


	private IEnumerator spawnNewBird()
	{
	 yield return new WaitForSeconds (3f);
	
	//Spawn new Bird
	GameObject newBird = Instantiate(Resources.Load("Bird_Asset", typeof(GameObject))) as GameObject;
	
	//Make Bird Child imageTarget
	newBird.transform.parent = GameObject.Find("ImageTarget").transform;

	//Scale Bird
	newBird.transform.localScale = new Vector3(2.2f, 2.2f, 2.2f);
	
	//Random Start Position
	Vector3 temp;
	temp.x = Random.Range (-45f, 45f);
	temp.y = Random.Range (10f, 45f);
	temp.z = Random.Range (-45f, 45f);
	newBird.transform.position = new Vector3(temp.x, temp.y, temp.z);
	}

	

}
