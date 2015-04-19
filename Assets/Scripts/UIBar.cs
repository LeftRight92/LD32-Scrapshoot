using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIBar : MonoBehaviour {
	
	public float amount;
	
	void Update () {
		GetComponent<Image>().fillAmount = amount;
	}
}
