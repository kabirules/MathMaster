using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	public int initalValue = 0;
	public int currentValue = 0;
	public int targetValue = 0;
	public int endValue = 100;
	public float upLimit = 10f; // Random generated numbers must equal or lower than upLimit
	public float downLimit = 2f; // Random generated numbers must equal or higher than downLimit
	public int generatedValue = 0; // Random generated number, it will be the right answer
	public int generatedWrongValue1 = 0; // Random generated number, it will be the wrong answer 1
	public int generatedWrongValue2 = 0; // Random generated number, it will be the wrong answer 2
	

	public Text txtCurrentValue;
	public Text txtTargetValue;	

	// Use this for initialization
	void Start () {
		this.initalValue = 0;
		this.endValue = 100;
		this.upLimit = 10f;
		this.downLimit = 2f;
		this.txtTargetValue.text = this.initalValue.ToString() + " / " + this.endValue.ToString();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void StartGame() {
		this.GenerateRandomNumber();
		this.GenerateRandonWrongNumbers();
		this.UpdateButtonTexts();
	}

	// Create a random number with this conditions
	// 1. Higher than the current value, but not between downLimit or upLimit + currentValue
	// 2. Lower or equals than the end value
	public void GenerateRandomNumber() {
		float aux = Random.Range(downLimit, upLimit);
		int intAux = (int) aux;
		if (intAux + this.currentValue > this.endValue) {
			intAux = this.endValue - this.currentValue;
		}
		this.generatedValue = intAux;
		this.txtTargetValue.text = intAux.ToString() + " / " + this.endValue.ToString();
	}

	public void GenerateRandonWrongNumbers() {
		this.generatedWrongValue1 = this.generatedValue;
		this.generatedWrongValue2 = this.generatedValue;
		while (this.generatedWrongValue1 == this.generatedValue)
		{
			float aux = Random.Range(downLimit, upLimit);
			int intAux = (int) aux;
			this.generatedWrongValue1 = intAux;
		}
		while (this.generatedWrongValue2 == this.generatedValue ||
			   this.generatedWrongValue2 == this.generatedWrongValue1) 
		{
			float aux = Random.Range(downLimit, upLimit);
			int intAux = (int) aux;
			this.generatedWrongValue2 = intAux;
		}
	}

	// Set the button texts randomly
	public void UpdateButtonTexts() {
		float fltRnd1 = Random.Range(0.5f, 3.5f);
		fltRnd1 = (float)System.Math.Round((System.Decimal)fltRnd1, 0, System.MidpointRounding.AwayFromZero);
		int rnd1 = (int)fltRnd1;
		int rnd2 = rnd1;
		while (rnd2 == rnd1) {
			float fltRnd2 = Random.Range(0.5f, 3.5f);
			fltRnd2 = (float)System.Math.Round((System.Decimal)fltRnd2, 0, System.MidpointRounding.AwayFromZero);
			rnd2 = (int)fltRnd2;
		}
		int [] allNumbers = new int[3]  { 1, 2, 3};
		allNumbers = allNumbers.Except(new int[]{rnd1, rnd2}).ToArray();
		int rnd3 = allNumbers[0];
		GameObject.Find("Button" + rnd1).GetComponentInChildren<Text>().text = this.generatedWrongValue2.ToString();
		GameObject.Find("Button" + rnd2).GetComponentInChildren<Text>().text = this.generatedWrongValue1.ToString();
		GameObject.Find("Button" + rnd3).GetComponentInChildren<Text>().text = this.generatedValue.ToString();
	}
}
