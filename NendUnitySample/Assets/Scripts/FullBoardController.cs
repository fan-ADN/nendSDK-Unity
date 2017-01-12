using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

using NendUnityPlugin.AD.FullBoard;

public class FullBoardController : MonoBehaviour
{

	private NendAdFullBoard m_Ad;
	public Text text;
	private string m_Status = "";

	// Use this for initialization
	void Start ()
	{
		#if UNITY_ANDROID && !UNITY_EDITOR
		m_Ad = NendAdFullBoard.NewFullBoardAd ("485520", "a88c0bcaa2646c4ef8b2b656fd38d6785762f2ff");
		#elif UNITY_IPHONE && !UNITY_EDITOR
		m_Ad = NendAdFullBoard.NewFullBoardAd ("485504", "30fda4b3386e793a14b27bedb4dcd29f03d638e5");
		#else
		return;
		#endif

		m_Ad.AdLoaded += (ad) => {
			m_Status = "AdLoaded";
		};
		m_Ad.AdFailedToLoad += (ad, error) => {
			switch (error) {
			case NendAdFullBoard.FullBoardAdErrorType.FailedAdRequest:
				m_Status = "FailedAdRequest";
				break;
			case NendAdFullBoard.FullBoardAdErrorType.FailedDownloadImage:
				m_Status = "FailedDownloadImage";
				break;
			case NendAdFullBoard.FullBoardAdErrorType.InvalidAdSpaces:
				m_Status = "InvalidAdSpaces";
				break;
			}
		};
		m_Ad.AdShown += (ad) => {
			Debug.Log ("AdShown");
		};
		m_Ad.AdClicked += (ad) => {
			Debug.Log ("AdClicked");
		};
		m_Ad.AdDismissed += (ad) => {
			Debug.Log ("AdDismissed");
			LoadAd ();
		};
	}
	
	// Update is called once per frame
	void Update ()
	{
		text.text = m_Status;
	}

	public void LoadAd ()
	{
		if (m_Ad != null) {
			m_Status = "Loading...";
			m_Ad.Load ();
		}
	}

	public void ShowAd ()
	{
		if (m_Ad != null) {
			m_Ad.Show ();
		}
	}

	public void Back ()
	{
		SceneManager.LoadScene ("First");
	}
}
