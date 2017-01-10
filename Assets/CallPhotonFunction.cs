using UnityEngine;
using System.Collections;
using UnityEngine.Experimental.Networking;

public class CallPhotonFunction: MonoBehaviour {

	public string deviceID;
	public string accessToken;
	private string requestURL;
	private string func;
	private Renderer theRenderer;

	void Start() {
		Debug.Log ("START");
		func = "ledSwitch";
		requestURL = "https://api.spark.io/v1/devices/" + deviceID + "/" + func + "/";
		theRenderer = GetComponent<Renderer> ();
		theRenderer.material.color = new Color (1f, 1f, 1f);
	}

	void OnTriggerEnter(Collider other){
		Debug.Log ("ENTER");
		StartCoroutine(Upload(1));
		theRenderer.material.color = new Color (0f, 0f, 1f);
	}

	void OnTriggerExit(Collider other){
		Debug.Log ("EXIT");
		StartCoroutine(Upload(0));
		theRenderer.material.color = new Color (1f, 1f, 1f);
	}

	IEnumerator Upload(int value) {
		WWWForm form = new WWWForm();
		form.AddField("newValue", value.ToString());
		form.AddField("access_token", accessToken);
		UnityEngine.Networking.UnityWebRequest www = UnityEngine.Networking.UnityWebRequest.Post(requestURL, form);
		yield return www.Send();

		if(www.isError) {
			Debug.Log(www.error);
		}
		else {
			Debug.Log("Form upload complete!");
		}
	}
}