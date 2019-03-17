using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class SteamerControll : MonoBehaviour
{ 
	private float acc;
	private float turn;
	public Rigidbody statek;

	public Slider speedS;
	public Slider turnS;

	private StreamWriter logFile;

	public Text speedK;
	public Text rudderA;
	public Text sliderS;
	public Text sliderPL;
	public Text cogV;

	public float kn;
    public float Y = -77.88707f;
	public float x = 0.0f;
	public float z = 0.0f;
	public float ROT = 0.0f;
	public float COG = 0.0f;
	///float T = 50.66f;
	float K = 0.07f;

	public float sigmaR = 0.0f;
	public float maxSpeed = 10.29f;
	public float actualSpeed = 0.0f;
	public float COGv2;

	public void speedSlider(float value)
	{
		acc = value;

	}
	public void turnSlider(float value)
	{
		turn = value;
	}
	public void resetSpeed()
	{
		speedS.value = 0;
	}
	public void resetTurn()
	{
		turnS.value = 0;
	}

	public void set50 ()
	{
		speedS.value = 50;
	}

	public void setm50 ()
	{
		speedS.value = -50;
	}
	public void set100 ()
	{
		speedS.value = 100;
	}
	public void setm100 ()
	{
		speedS.value = -100;
	}



	void Start()
	{
		
		statek = GetComponent<Rigidbody>();
		speedS = GetComponent<Slider>();
		turnS = GetComponent<Slider>();
		Application.logMessageReceived += ReadLog;
		logFile = new System.IO.StreamWriter(@"LogsSteamer.txt");
	}
		
	void Update()
	{
		
		sigmaR = Mathf.MoveTowards(sigmaR, turn, 2.96f * Time.deltaTime);
		actualSpeed = Mathf.MoveTowards(actualSpeed, acc / 100 * maxSpeed, 0.15f * Time.deltaTime);
		if (actualSpeed != 0) 
		{
			ROT += (K * (sigmaR) - ROT) * Time.deltaTime;
			COG += ROT * Time.deltaTime;
			x += actualSpeed * Time.deltaTime * Mathf.Sin (COG * Mathf.PI / 180);
			z += actualSpeed * Time.deltaTime * Mathf.Cos (COG * Mathf.PI / 180);        
			transform.position = new Vector3 (x, 23.0f, z);
			transform.rotation = Quaternion.Euler (0, COG, 0);

			kn = actualSpeed * 1.9438f; ///zamiana na wezły
			speedK.text = kn.ToString("0.000");
			rudderA.text = sigmaR.ToString("0");
			sliderS.text = acc.ToString();
			sliderPL.text = turn.ToString ();
			COGv2 = COG % 360;
			cogV.text = COGv2.ToString("0.00");
		}
		Debug.Log ("TIME:");
		Debug.Log (Time.realtimeSinceStartup);
		Debug.Log ("ActualSpeed");
		Debug.Log (actualSpeed);
		Debug.Log ("Rudder Angle");
		Debug.Log (sigmaR);
		Debug.Log ("COG");
		Debug.Log (COG);
	}
		

	void OnDisable()
	{
		logFile.Close();
		logFile.Dispose();
	}

	void ReadLog(string zlog, string stack, LogType Log)
	{
		logFile.WriteLine(zlog);

	}
		
}
	