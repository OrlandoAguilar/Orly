using UnityEngine;
using System.Collections;

public class Texts{
	public string sparks {get; protected set;}
	public string close {get; protected set;}
	public string accel {get; protected set;}
	public string deaccel {get; protected set;}
	public string move {get; protected set;}
	public string jewel {get; protected set;}
	public string atack {get; protected set;}
	public string time {get; protected set;}
	public string deads {get; protected set;}
	public string jewelText {get; protected set;}
	public string total {get; protected set;}
	public string continueText {get; protected set;}
	public string back {get; protected set;}
	public string restart {get; protected set;}
	public string bestScore {get; protected set;}
	public string[] toolbarstrings {get; protected set;}
	public string play {get; protected set;}
	public string options {get; protected set;}
	public string credits {get; protected set;}
	public string exit {get; protected set;}
	public string volume {get; protected set;}
	public string increase {get; protected set;}
	public string decrease {get; protected set;}
	public string accelerometer {get; protected set;}
	public string newGame {get; protected set;}
	public string adviceNewGame {get; protected set;}
	public string Yes {get; protected set;}
	public string No {get; protected set;}
	public string PubishScreenshootFacebook {get; protected set;}
	public string nameGame {get; protected set;}
	public string withoutConnection {get; protected set;}
	public string noPlayed {get; protected set;}
	public string level {get; protected set;}
	public string Facebook {get; protected set;}
	public string error {get; protected set;}
	public string botonesTouch{get; protected set;}
	public string[] itemToolbar {get; protected set;}
	public string itemStore{get; protected set;}
	public string descriptionFreeze{get; protected set;}
	public string descriptionInmune{get; protected set;}
	public string descriptionSave{get; protected set;}
	public string factor{get; protected set;}
	public string price{get; protected set;}

	public Texts(){}

	public string getText(string text)
	{
		return this.GetType()
			.GetProperty(text)
			.GetValue(this,null).ToString();
				
	}
}
