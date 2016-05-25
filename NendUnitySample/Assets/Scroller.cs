using UnityEngine;
using UnityEngine.UI;
using System.Collections;

using NendUnityPlugin.AD.Native;

public class Scroller : MonoBehaviour
{
	public Text targetText;
	public float speed = 0.5f;
	public float margin = 40.0f;

	private bool m_CanScroll = false;
	private float m_Position = 0.0f;
	private float m_MaxScroll = 0.0f;

	// Use this for initialization
	void Start ()
	{
	
	}

	// Update is called once per frame
	void Update ()
	{
		if (m_CanScroll) {
			m_Position -= speed;
			if (Mathf.Abs (m_Position) > Mathf.Abs (m_MaxScroll)) {
				m_Position = targetText.rectTransform.rect.size.x;
			}
			targetText.rectTransform.localPosition = new Vector2 (m_Position, targetText.rectTransform.localPosition.y);
		}
	}

	public void onDisplayAd (NendAdNativeView view)
	{
		var settings = new TextGenerationSettings ();
		settings.textAnchor = targetText.alignment;
		settings.generationExtents = new Vector2 (float.MaxValue, targetText.rectTransform.rect.size.y);
		settings.pivot = targetText.rectTransform.pivot;
		settings.font = targetText.font;
		settings.fontSize = targetText.fontSize;
		settings.fontStyle = targetText.fontStyle;
		settings.verticalOverflow = targetText.verticalOverflow;
		settings.horizontalOverflow = targetText.horizontalOverflow;

		m_MaxScroll = targetText.cachedTextGenerator.GetPreferredWidth (targetText.text, settings) + margin;
		m_CanScroll = true;
	}
}
