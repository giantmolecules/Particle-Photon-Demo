using UnityEngine;
using System.Collections;
using System.Net.Sockets;
using UnityEngine.Networking;
using Particle.SDK;
using System.Threading;

public class GetPhotonVariable: MonoBehaviour {

	public string deviceID;
	public string accessToken;
	public string variableName;
	private string result;
	private bool status = ParticleCloud.SharedCloud.LoginAsync("giantmolecules@gmail.com", "c0t1onic");

	void Start() {
		InvokeRepeating("startRepeating", 1, 5.0F);
	}

	void startRepeating(){
		StartCoroutine(GetText());
	}

	IEnumerator GetText() {
		UnityWebRequest www = UnityWebRequest.Get("https://api.particle.io/v1/devices/" + deviceID + "/" + variableName + "?access_token=" + accessToken);
		yield return www.Send();

		if(www.isError) {
			Debug.Log ("Error:");
			Debug.Log(www.error);
		}
		else {
			// Show results as text
			Debug.Log("RESULTS");
			Debug.Log(JsonUtility.FromJson<string>(www.downloadHandler.text));
			//result = www.ser;
			//Debug.Log (result);

			// Or retrieve results as binary data
			//byte[] results = www.downloadHandler.data;
			//Debug.Log (results.ToString());
			//Debug.Log(results.);
			;
		}
	}
}