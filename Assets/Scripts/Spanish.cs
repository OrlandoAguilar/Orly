using UnityEngine;
using System.Collections;

public class Spanish : Texts {

	public new string accel {
		get{
			string sbase="Puedes acelerar a Orly con ";
			if (Globals.supportTouchScreen)
				sbase+="\nEl botón táctil derecho";
			if (Globals.supportMouseAndKeyboard || !Globals.supportTouchScreen)
				sbase+="\nEl botón central del ratón \n La tecla a del teclado";
			return sbase;
		} 
		protected set{

		}

	}

	public new string deaccel {
		get{
			string sbase="Puedes Frenar a Orly con ";
			if (Globals.supportTouchScreen)
				sbase+="\nEl botón táctil izquierdo";
			if (Globals.supportMouseAndKeyboard || !Globals.supportTouchScreen)
				sbase+="\nEl botón derecho del ratón \nLa tecla d del teclado";
			return sbase;
		} 
		protected set{
			
		}
		
	}

	public new string move {
		get{
			string sbase="Controla la gravedad para mover a Orly ";
			if (Globals.activatedAccelerometer){
				sbase+="\nInclina el dispositivo";
			}else{ 
				if (Globals.supportTouchScreen){
					sbase+="\nToca la pantalla hacia donde quieras dirigir a Orly";
				}
				if (Globals.supportMouseAndKeyboard){
					sbase+="\nHaz click hacia donde quieras dirigir a Orly \nUsando las flechas del teclado";
				}
			}
			return sbase;
		} 
		protected set{
			
		}
		
	}

	public Spanish(){
		sparks="Espera que se apaguen las llamas de fuego, antes de cruzarlas.";
		close="Cerrar";
		move="Controla la gravedad para mover a Orly "+"\nInclina el dispositivo."; 
		jewel="Recoge todas las gemas que puedas, te permitirán comprar nuevas zonas.";
		atack="Acelera hasta cambiar de color para derrotar estos enemigos.";
		time="Tiempo";
		jewelText="Joya";
		deads="Muertes";
		total="Total";
		continueText="Continuar";
		back="Atrás";
		restart="Reiniciar";
		bestScore="<color=red>Mejor</color>";
		toolbarstrings=new string[]{"Audio","Gráficos","Control"};
		play="Jugar";
		options="Opciones";
		exit="Salir";
		credits="Créditos";
		volume="Volumen";
		increase="Incrementar";
		decrease="Decrementar";
		accelerometer="Acelerómetro";
		newGame="Nuevo Juego";
		adviceNewGame="Todos los progresos de antiguas partidas se perderán ¿Estás seguro?";
		Yes="Si";
		No="No";
		PubishScreenshootFacebook="Publicar en Facebook";
		nameGame="Orly";
		withoutConnection="Error de conexión.";
		noPlayed="<color=red>No jugado</color>";
		level="<color=olive>Nivel</color>";
		Facebook="Facebook";
		error="Error";
		botonesTouch="Botones Táctiles";
		itemToolbar=new string[]{"Usar","Comprar","Monedas"};
		itemStore="Items y Tienda";
		descriptionSave="Usa este ítem para guardar la posición actual de Orly y aparecer ahí la próxima vez que sea vencido por un enemigo.";
		descriptionInmune="Este ítem cambia el estado de Orly a modo invencible durante un tiempo. Orly tampoco puede destruir enemigos en este estado.";
		descriptionFreeze="Este ítem congela todos los enemigos durante un momento.";
		factor="Factor";
		price="Precio";
	}

}
