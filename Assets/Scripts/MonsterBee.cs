using UnityEngine;
using System.Collections;

public class MonsterBee : Congelable {

	private float shootFirst=0.5f;
	private float acumulator=0.0f;
	private float changePart=0.0f;

	private Animator animator;
	private Vector3 positionPum=new Vector3(0.01130357f,2.320034f,0.0f);
	private enum state{PRESENTATION=0,PART1=1, REST1=2,PART2=3, PART3=4, PART4=5, REST4=6, DIE=7};
	private state _actualState=state.PRESENTATION;
	private state actualState{
		get{return _actualState;}

		set{_actualState=value; InitializaState();  }
	}
	private Vector3 angle=Vector3.zero;
	private Vector3 position=Vector3.zero;
	private Vector3 positionArmOriginal = new Vector3(2.591914f,1.398816f,0.0f);


	public GameObject spike=null;
	public GameObject arm;
	public Texture2D textureHeart;
	public AudioClip punched;

	private int life=3;

	private Heart[] hearts=new Heart[3];
		// Use this for initialization
	void Start () {
		animator=GetComponent<Animator>();
		for (int z=0;z<3;++z){
			hearts[z]=new Heart(new Rect(20,20+z*60,50,50),textureHeart);
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (!isCongelated()){
			switch(actualState){
			case state.PRESENTATION:
				transform.rotation=Quaternion.identity;
				advance(Vector3.zero,1.0f);

				if (transform.position==Vector3.zero){
					actualState=state.PART1;
				}
				break;

			case state.PART1:

				angle.z=0.4f*Time.timeScale;
				changePart+=angle.z;

				transform.Rotate(angle);
				if (fTime>acumulator ){
					animator.Play("BeeShoot");
					acumulator+=shootFirst;
					GameObject oSpark=(GameObject) Instantiate(spike,transform.position, transform.rotation); 
					oSpark.transform.Rotate(new Vector3(0.0f,0.0f,-70.0f));
					oSpark.transform.Translate(0.5f,-0.1f,0.0f);
				}

				if (changePart>360.0f){
					actualState=state.REST1;
				}
				break;
			case state.REST1: case state.REST4:
				advance(positionPum,1.0f);

				if (positionPum!=transform.position)
					acumulator=fTime+7.0f;

				if (fTime>acumulator){
						actualState=state.PRESENTATION;//restart all
				}
				break;
				case state.PART2:
					position.y-=0.007f*Time.timeScale;
					position.x=Mathf.PingPong(position.y,1)*6.5f-3.25f;
					transform.position=position;
					if (position.y<-3.0f){
						actualState=state.PART3;
					}
					
				break;
			case state.PART3:
				position.x+=0.007f*Time.timeScale;
				transform.position=position;

				if (fTime>acumulator ){
					animator.Play("BeeShoot");
					acumulator+=shootFirst;
					GameObject oSpark=(GameObject) Instantiate(spike,transform.position, transform.rotation); 
					oSpark.transform.Rotate(new Vector3(0.0f,0.0f,-70.0f));
					oSpark.transform.Translate(0.5f,-0.1f,0.0f);
				}

				if (position.x>2.85f){
					actualState=state.PRESENTATION;
				}

				break;
			case state.PART4:
				bool _outRegion=outRegion();
				if (_outRegion || Random.Range(0,100)==3){
					position=Globals.pinky.transform.position;
					angle.z=Mathf.Atan2(position.y-transform.position.y,position.x-transform.position.x);
					position=new Vector3(Mathf.Cos(angle.z),Mathf.Sin(angle.z),0)*0.03f;
					if (!_outRegion)
						acumulator++;
				}
				transform.position+=position;
				if (acumulator>15.0f){
					actualState=state.REST4;
				}
				break;
			case state.DIE:
				position.y-=0.07f*Time.timeScale;
				transform.position=position;
				animator.enabled=false;
				GetComponent<CircleCollider2D>().enabled=false;
				if (position.y<-3.0f){
					Globals.pinky.Gana();
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
			return;
		}
		arm.transform.position=positionArmOriginal;
		arm.rigidbody2D.velocity=Vector2.zero;
		arm.SetActive(false);

	}

	private bool outRegion(){
		return transform.position.x>4 || transform.position.x<-4 || transform.position.y>3 || transform.position.y<-3;
	}

	private void advance(Vector3 destiny, float speed){
		float step = speed * Time.deltaTime;
		transform.position = Vector3.MoveTowards(transform.position, destiny, step);
	}

	private void InitializaState(){

		switch(actualState){
		case state.PRESENTATION:
			changePart=0.0f;
			angle.z=-180.0f;
			transform.Rotate(angle);
			activateArm(false);
			life=3;
			break;
		case state.PART1:
			arm.SetActive(false);
			acumulator=fTime+shootFirst;
			changePart=0.0f;
			angle.z=-180.0f;
			transform.Rotate(angle);
			life=3;
			break;
		case state.REST1:
			acumulator=fTime+7.0f;
			transform.rotation=Quaternion.identity;
			position=transform.position;
			activateArm(true);
			life=3;
			break;
		case state.PART2:
			position=transform.position;
			life=2;
			break;
		case state.PART3:
			position.x=-4.6f;
			position.y=2.48f;
			transform.position=position;
			shootFirst=0.7f;
			activateArm(true);
			acumulator=fTime+shootFirst;
			life=2;
			break;
		case state.PART4:
			acumulator=0.0f;
			life=1;
			break;
		case state.REST4:
			activateArm(true);
			acumulator=fTime+7.0f;
			life=1;
			break;
		case state.DIE:
			life=0;
			break;
		}

	}

	void OnTriggerEnter2D(Collider2D col){
		if (col.gameObject.tag == "Player"){
			Globals.pinky.Die();
			//actualState=state.PRESENTATION;
		}else if (col.gameObject.tag=="PinkyRock"){
			acumulator=0.0f;
			actualState++;
			activateArm(false);
			AudioSource.PlayClipAtPoint(punched, transform.position);
		}
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
