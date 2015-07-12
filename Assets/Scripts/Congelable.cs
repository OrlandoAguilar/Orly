using UnityEngine;
using System.Collections;

public abstract class Congelable : MonoBehaviour {
	protected float timeChange;
	bool congelado;
	protected float fTime=0.0f;
	protected abstract void OnCongela();
	protected abstract void OnDescongela();
	protected abstract void OnAlmost();

	public void Congela(float time){
		timeChange=time;
		congelado=true;
		OnCongela();
	}

	protected bool isCongelated(){
		return congelado;
	}

	protected void updateCongelable(){
		if (congelado){
			if (timeChange<1){
				OnAlmost();
			}
			timeChange-=Time.deltaTime;
			if (timeChange<0){
				congelado=false;
				OnDescongela();
			}
		}else{
			fTime+=Time.deltaTime;
		}
	}

	public static void congelaCongelable(float timef){
		GameObject[] monos = GameObject.FindGameObjectsWithTag("Enemy");
		for(int z=0;z<monos.Length;++z){
			Congelable enem=monos[z].GetComponent<Congelable>();
			if (enem!=null){
				enem.Congela(timef);
			}
		}
		
		monos = GameObject.FindGameObjectsWithTag("Bullet");
		for(int z=0;z<monos.Length;++z){
			Congelable enem=monos[z].GetComponent<Congelable>();
			if (enem!=null){
				enem.Congela(timef);
			}
		}
	}
}
