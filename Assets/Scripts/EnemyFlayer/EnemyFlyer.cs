using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyFlyer : Congelable {

	public static LinkedList<Bat> bats=new LinkedList<Bat>();
	enum state{PRESENTATION,AROUND,SPIRAL,FOLLOWING,DIE};
	state _actualState;
	state actualState{
		get{return _actualState;}
		set{_actualState=value; InitializaState(); }
	}
	private static Vector3 gira=new Vector3(0.0f,0.0f,-90.0f);
	private float acum=0.0f, radio;
	private static Vector3 advance=new Vector3(0.06f,0.0f,0.0f);
	public GameObject batPattern;
	public GameObject jewel;

	// Use this for initialization
	void Start () {
		Vector3 adv=new Vector3(0.6f,0.0f,0.0f);
		for (int z=0;z<15;++z){
			transform.Translate(adv);
			GameObject bat=(GameObject) Instantiate(batPattern,transform.position,Quaternion.identity);
			bats.AddFirst(bat.GetComponent<Bat>());
		}
		actualState=state.PRESENTATION;
	}
	
	// Update is called once per frame
	void Update () {
		if (!isCongelated()){
			if (bats.Count==0)
				Globals.pinky.Gana();

			if (bats.Count>0 && (bats.First.Value.transform.position-transform.position).sqrMagnitude>0.55f){

				for (LinkedListNode<Bat> nodo=bats.Last;nodo.Previous!=null;nodo=nodo.Previous){
					nodo.Value.Move(nodo.Previous.Value.transform.position);
				}
				bats.First.Value.Move(transform.position);

			}

			if (!Globals.pinky.isDied() )
				Bat.dispara++;

			switch(actualState){
			case state.PRESENTATION:
				actualState=state.AROUND;
				break;
			case state.AROUND:
				if (transform.position.x>3.5f && acum==0){
					transform.Rotate(gira);
					acum++;
				}
				
				if (transform.position.y<-2.5f  && acum==1){
					transform.Rotate(gira);
					acum++;
				}
				
				if (transform.position.x<-3.5f  && acum==2){
					transform.Rotate(gira);
					acum++;
				}
				if (transform.position.y>2.5f && acum==3){
					transform.Rotate(gira);
					acum++;
				}

				transform.Translate(advance*Time.timeScale);

				if (acum>3)
					actualState=state.SPIRAL;

				break;
			case state.SPIRAL:


				radio-=0.005f*Time.timeScale;

				if (radio>0.0f){
					acum-=0.02f*Time.timeScale;
				}else{
					acum+=0.02f*Time.timeScale;
				}

				Vector3 pos=Vector3.zero;
				pos.x=radio*Mathf.Cos(acum);
				pos.y=radio*Mathf.Sin(acum);

				transform.position=pos;

				if (Time.timeScale>0.5 && Random.Range(0,100)==3)
					Instantiate(jewel,transform.position,Quaternion.identity);

				if (radio<-3.0f)
					actualState=state.FOLLOWING;

				break;

			case state.FOLLOWING:
				if (Random.Range(0,30)==4){
					FollowPinky();
					radio++;
				}

				if (transform.position.x>3.5f || transform.position.x<-3.5f || 
				    transform.position.y<-2.5f ||transform.position.y>2.5f){

					FollowPinky();
				}

				transform.Translate(advance*Time.timeScale);

				if (radio==35.0f){
					actualState=state.AROUND;
				}

				break;
			}
		}
			updateCongelable();


	}

	Quaternion UP		=Quaternion.Euler(new Vector3(0.0f,0.0f,90.0f));
	Quaternion DOWN	=Quaternion.Euler(new Vector3(0.0f,0.0f,-90.0f));
	Quaternion LEFT	=Quaternion.Euler(new Vector3(0.0f,0.0f,180.0f));
	Quaternion RIGHT	=Quaternion.Euler(Vector3.zero);
	
	const float FUP 	= 0.0f;
	const float FDOWN 	= 1.0f;
	const float FLEFT 	= 2.0f;
	const float FRIGHT 	= 3.0f;

	private void FollowPinky(){
		if (acum==FUP || acum==FDOWN){
			if (Globals.pinky.transform.position.x>transform.position.x){
				transform.rotation=RIGHT;
				acum=FRIGHT;
			}else{
				transform.rotation=LEFT;
				acum=FLEFT;
			}
			return;
		}

		if (acum==FLEFT || acum==FRIGHT){
			if (Globals.pinky.transform.position.y>transform.position.y){
				transform.rotation=UP;
				acum=FUP;
			}else{
				transform.rotation=DOWN;
				acum=FDOWN;
			}
		}
	}

	private void InitializaState(){
		switch(_actualState){
		case state.PRESENTATION:
			acum=0.0f;
			break;
		case state.AROUND:
			acum=0.0f;
			transform.rotation=Quaternion.identity;
			break;
		case state.SPIRAL:
			acum=Mathf.Atan2(transform.position.y,transform.position.x)-0.04f;
			radio=3.0f;
			break;
		case state.FOLLOWING:
			acum=FRIGHT;
			transform.rotation=Quaternion.identity;
			radio=0.0f;
			break;
		}
	}

	protected override void OnCongela(){
	}
	
	protected override void OnDescongela(){

	}
	
	protected override void OnAlmost(){
	}

}
