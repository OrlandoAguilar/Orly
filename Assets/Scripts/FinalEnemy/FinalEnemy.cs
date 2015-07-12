using UnityEngine;
using System.Collections;

public class FinalEnemy : Congelable {

	public Texture2D textureHeart;
	public AudioClip punched;
	private Vector3 positionArmOriginal = new Vector3(0.85f,0,0.0f);
	private int life=3;
	public GameObject arm;
	private Vector3 position=Vector3.zero;
	private Animator animator;
	private Heart[] hearts=new Heart[3];

	enum state{ START,STATE1,ATACK_STATE1, STATE2,ATACK_STATE2, STATE3,ATACK_STATE3, DIE};

	state _state=state.START;

	state actualState{
		get{
			return _state;
		}
		set{
			_state=value;
			changeState();
		}
	}

	// Use this for initialization
	void Start () {
		animator=GetComponent<Animator>();
		for (int z=0;z<3;++z){
			hearts[z]=new Heart(new Rect(20,20+z*60,50,50),textureHeart);
		}
	}

	private static Vector3 posInit=new Vector3(-3.1f,0.0f,0.0f);
	public GameObject bat;
	public GameObject gun;
	public GameObject ant;
	public GameObject canion;
	private GameObject[] enemies=new GameObject[3];
	public GameObject laser1,laser2;
	public GameObject pinkBall;


	// Update is called once per frame
	void Update () {
		if (!isCongelated()){
			switch(actualState){
			case state.START:
				advance(posInit,1.0f);
				
				if (transform.position==posInit){
					actualState=state.STATE1;
				}
				break;
			case state.STATE1: case state.STATE2: case state.STATE3:
				if (haveDied()){
					++actualState;
				}
				break;
			case state.ATACK_STATE1: case state.ATACK_STATE2: case state.ATACK_STATE3:
				atackLaser();
				Vector3 ang=laser1.transform.eulerAngles;
				if (ang.z<1 || ang.z>300)
					actualState--;
				break;
			case state.DIE:
				position.y-=0.07f*Time.timeScale;
				transform.position=position;
				animator.enabled=false;
				GetComponent<BoxCollider2D>().enabled=false;
				if (position.y<-3.0f){
					pinkBall.SetActive(true);
				}
				break;
			}
		}
			updateCongelable();

	}


	private void activateArm(bool activ){
		if (activ){
			arm.SetActive(true);
			arm.transform.position=positionArmOriginal;
			arm.rigidbody2D.velocity=Vector2.zero;
			canion.SetActive(true);
			return;
		}
		arm.transform.position=positionArmOriginal;
		arm.rigidbody2D.velocity=Vector2.zero;
		arm.SetActive(false);
		canion.SetActive(false);
		
	}

	void atackLaser(){
		float advance=Time.timeScale*0.1f;
		laser1.transform.Rotate(Vector3.back,advance);
		laser2.transform.Rotate(Vector3.back,-advance);
	}

	private void advance(Vector3 destiny, float speed){
		float step = speed * Time.deltaTime;
		transform.position = Vector3.MoveTowards(transform.position, destiny, step);
	}

	void OnTriggerEnter2D(Collider2D col){
		if (col.gameObject.tag == "Player"){
			Globals.pinky.Die();
		}else if (col.gameObject.tag=="PinkyRock"){
			actualState++;
			activateArm(false);
			AudioSource.PlayClipAtPoint(punched, transform.position);
		}
	}

	void changeState(){
		switch(actualState){
		case state.STATE1:
			activateArm(false);
			activateLaser(false);

			enemies[0]=(GameObject)Instantiate(bat,new Vector3(-0.7f,1.47f,0.0f),Quaternion.identity);
			enemies[1]=(GameObject)Instantiate(bat,new Vector3(2.3f,0.148f,0.0f),Quaternion.identity);
			enemies[2]=(GameObject)Instantiate(bat,new Vector3(-0.5f,-1.3f,0.0f),Quaternion.identity);
			life=3;
			break;
		case state.ATACK_STATE1: case state.ATACK_STATE2: case state.ATACK_STATE3:
			activateArm(true);
			activateLaser(true);
			laser1.transform.eulerAngles=new Vector3(0,0,90);
			laser2.transform.eulerAngles=new Vector3(0,0,-90);
			break;
		case state.STATE2:
			activateArm(false);
			activateLaser(false);
			enemies[0]=(GameObject)Instantiate(gun,new Vector3(0.7f,1.47f,0.0f),Quaternion.identity);
			enemies[1]=(GameObject)Instantiate(gun,new Vector3(-2.3f,0.148f,0.0f),Quaternion.identity);
			enemies[2]=(GameObject)Instantiate(gun,new Vector3(0.5f,-1.3f,0.0f),Quaternion.identity);
			for (int z=0;z<3;++z){
				Gun gunz=enemies[z].GetComponent<Gun>();
				gunz.shift=z;
				gunz.everyBlast=1;
				Destroy(enemies[z],10+z*4);
			}
			life=2;
			break;
		case state.STATE3:
			activateArm(false);
			activateLaser(false);
			Vector3 point=Globals.pinky.transform.position;
			for (int z=0;z<3;++z){
				enemies[z]=(GameObject)Instantiate(ant,point,Quaternion.identity);
				Ant antz=enemies[z].GetComponent<Ant>();
				antz.velocity=1.0f/(z+1.0f);
				antz.radius=z/1.3f+0.7f;
				Destroy(enemies[z],15+z*8);
			}
			life=1;
			break;
		case state.DIE:
			life=0;
			break;
		}
	}

	bool haveDied(){
		int cant=0;
		for (int z=0;z<3;++z){
			if (enemies[z]!=null)
				++cant;
		}
		return cant==0;
	}

	void activateLaser(bool state){
		laser1.GetComponent<Laser>().activate(state);
		laser2.GetComponent<Laser>().activate(state);
	}

	void OnGUI() {
		for (int z=0;z<life;++z){
			hearts[z].Draw();
		}
	}
	protected override void OnCongela(){
		GetComponent<SpriteRenderer>().color=new Color(0.5f,0.85f,0.89f,0.9f);
		GetComponent<Animator>().enabled=false;
	}
	
	protected override void OnDescongela(){
		GetComponent<SpriteRenderer>().color=Color.white;
		GetComponent<Animator>().enabled=true;
		
	}
	
	protected override void OnAlmost(){
		GetComponent<SpriteRenderer>().color=Color.gray;
	}

}
