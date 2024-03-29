﻿using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

using NendUnityPlugin.AD.Video;

public class VideoController : MonoBehaviour
{
	public Text text;
	private string m_Status = "";
	private NendAdInterstitialVideo m_InterstitialVideoAd;
	private NendAdRewardedVideo m_RewardedVideoAd;

	#if !UNITY_EDITOR && UNITY_IOS
	[SerializeField] private int interstitialSpotId = 802557;
	[SerializeField] private string interstitialApiKey = "b6a97b05dd088b67f68fe6f155fb3091f302b48b";
	[SerializeField] private int rewardSpotId = 802555;
	[SerializeField] private string rewardApiKey = "ca80ed7018734d16787dbda24c9edd26c84c15b8";
	private int fallbackSpotId = 485504;
	private string fallbackApiKey = "30fda4b3386e793a14b27bedb4dcd29f03d638e5";
	#else
	[SerializeField] private int interstitialSpotId = 802559;
	[SerializeField] private string interstitialApiKey = "e9527a2ac8d1f39a667dfe0f7c169513b090ad44";
	[SerializeField] private int rewardSpotId = 802558;
	[SerializeField] private string rewardApiKey = "a6eb8828d64c70630fd6737bd266756c5c7d48aa";
	private int fallbackSpotId = 485520;
	private string fallbackApiKey = "a88c0bcaa2646c4ef8b2b656fd38d6785762f2ff";
	#endif

	// Use this for initialization
	void Start () {
		InitializeInterstitialVideo();
		InitializeRewardedVideo();
	}

	// Update is called once per frame
	void Update () {
		text.text = m_Status;
	}

	void OnDestroy () {
		ReleaseInterstitial ();
		ReleaseReward ();
	}

	private void InitializeInterstitialVideo ()
	{
		m_InterstitialVideoAd = NendAdInterstitialVideo.NewVideoAd (interstitialSpotId, interstitialApiKey);

        m_InterstitialVideoAd.IsMuteStartPlaying = false;
		m_InterstitialVideoAd.AddFallbackFullboard (fallbackSpotId, fallbackApiKey);

		m_InterstitialVideoAd.AdLoaded += (instance) => {
			// 広告ロード成功のコールバック
			m_Status = "InterstitialVideoAd.AdLoaded";
			Debug.Log ("Interstitial AdLoaded type = " + instance.AdType);
		};
		m_InterstitialVideoAd.AdFailedToLoad += (instance, errorCode) => {
			// 広告ロード失敗のコールバック
			m_Status = "InterstitialVideoAd.AdFailedToLoad";
			Debug.Log ("Interstitial AdFailedToLoad");
		};
		m_InterstitialVideoAd.AdFailedToPlay += (instance) => {
			// 再生失敗のコールバック
			m_Status = "InterstitialVideoAd.AdFailedToPlay";
			Debug.Log ("Interstitial AdFailedToPlay");
		};
		m_InterstitialVideoAd.AdShown += (instance) => {
			// 広告表示のコールバック
			Debug.Log ("Interstitial AdShown");
		};
		m_InterstitialVideoAd.AdStarted += (instance) => {
			// 再生開始のコールバック
			Debug.Log ("Interstitial AdStarted");
		};
		m_InterstitialVideoAd.AdStopped += (instance) => {
			// 再生中断のコールバック
			Debug.Log ("Interstitial AdStopped");
		};
		m_InterstitialVideoAd.AdCompleted += (instance) => {
			// 再生完了のコールバック
			Debug.Log ("Interstitial AdCompleted");
		};
		m_InterstitialVideoAd.AdClicked += (instance) => {
			// 広告クリックのコールバック
			Debug.Log ("Interstitial AdClicked");
		};
		m_InterstitialVideoAd.InformationClicked += (instance) => {
			// オプトアウトクリックのコールバック
			Debug.Log ("Interstitial InformationClicked");
		};
		m_InterstitialVideoAd.AdClosed += (instance) => {
			// 広告クローズのコールバック
			m_Status = "InterstitialVideoAd.AdClosed";
			Debug.Log ("Interstitial AdClosed");
		};
	}

	private void LoadInterstitial ()
	{
		if (m_InterstitialVideoAd == null) {
			InitializeInterstitialVideo();
		}
		m_InterstitialVideoAd.Load ();
	}

	private void ShowInterstitial ()
	{
		if (m_InterstitialVideoAd.IsLoaded()) {
			m_InterstitialVideoAd.Show ();
		}
	}

	private void ReleaseInterstitial ()
	{
		if (m_InterstitialVideoAd != null) {
			m_InterstitialVideoAd.Release ();
			m_InterstitialVideoAd = null;
			Debug.Log ("ReleaseInterstitial");
		}
	}

	private void InitializeRewardedVideo ()
	{
		m_RewardedVideoAd = NendAdRewardedVideo.NewVideoAd (rewardSpotId, rewardApiKey);

		m_RewardedVideoAd.AdLoaded += (instance) => {
			// 広告ロード成功のコールバック
			m_Status = "RewardedVideoAd.AdLoaded";
			Debug.Log ("Rewarded AdLoaded type = " + instance.AdType);
		};
		m_RewardedVideoAd.AdFailedToLoad += (instance, errorCode) => {
			// 広告ロード失敗のコールバック
			m_Status = "RewardedVideoAd.AdFailedToLoad";
			Debug.Log ("Rewarded AdFailedToLoad");
		};
		m_RewardedVideoAd.AdFailedToPlay += (instance) => {
			// 再生失敗のコールバック
			m_Status = "RewardedVideoAd.AdFailedToPlay";
			Debug.Log ("Rewarded AdFailedToPlay");
		};
		m_RewardedVideoAd.AdShown += (instance) => {
			// 広告表示のコールバック
			Debug.Log ("Rewarded AdShown");
		};
		m_RewardedVideoAd.AdStarted += (instance) => {
			// 再生開始のコールバック
			Debug.Log ("Rewarded AdStarted");
		};
		m_RewardedVideoAd.AdStopped += (instance) => {
			// 再生中断のコールバック
			Debug.Log ("Rewarded AdStopped");
		};
		m_RewardedVideoAd.AdCompleted += (instance) => {
			// 再生完了のコールバック
			Debug.Log ("Rewarded AdCompleted");
		};
		m_RewardedVideoAd.AdClicked += (instance) => {
			// 広告クリックのコールバック
			Debug.Log ("Rewarded AdClicked");
		};
		m_RewardedVideoAd.InformationClicked += (instance) => {
			// オプトアウトクリックのコールバック
			Debug.Log ("Rewarded InformationClicked");
		};
		m_RewardedVideoAd.AdClosed += (instance) => {
			// 広告クローズのコールバック
			m_Status = "RewardedVideoAd.AdClosed";
			Debug.Log ("Rewarded AdClosed");
		};
		m_RewardedVideoAd.Rewarded += (instance, rewardedItem) => {
			// リワード報酬のコールバック
			Debug.Log ("CurrencyName = " + rewardedItem.currencyName);
			Debug.Log ("CurrencyAmount = " + rewardedItem.currencyAmount);
		};
	}

	private void LoadReward ()
	{
		if (m_RewardedVideoAd == null) {
			InitializeRewardedVideo();
		}
		m_RewardedVideoAd.Load ();
	}

	private void ShowReward ()
	{
		if (m_RewardedVideoAd.IsLoaded()) {
			m_RewardedVideoAd.Show ();
		}
	}

	private void ReleaseReward ()
	{
		if (m_RewardedVideoAd != null) {
			m_RewardedVideoAd.Release ();
			m_RewardedVideoAd = null;
			Debug.Log ("ReleaseReward");
		}
	}

	public void TapLoadInterstitial ()
	{
		LoadInterstitial();
	}

	public void TapShowInterstitial ()
	{
		ShowInterstitial();
	}

	public void TapReleaseInterstitial ()
	{
		m_Status = "InterstitialVideoAd not loaded";
		ReleaseInterstitial();
	}

	public void TapLoadReward ()
	{
		LoadReward();
	}

	public void TapShowReward ()
	{
		ShowReward();
	}

	public void TapReleaseReward ()
	{
		m_Status = "RewardedVideoAd not loaded";
		ReleaseReward();
	}

	public void Back ()
	{
		SceneManager.LoadScene ("First");
	}

}
