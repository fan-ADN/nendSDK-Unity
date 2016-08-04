using UnityEngine;
using UnityEngine.UI;
using NendUnityPlugin.AD.Native;

public class ScrollController : MonoBehaviour
{
	public enum Direction
	{
		VERTICAL,
		HORIZONTAL
	}

	public Text targetText;
	public Direction direction;
	public float speed = 1.0f;
	public float delay = 1.0f;

	private Scroller m_Scroller;
	private bool m_CanScroll = false;
	private float m_TimeElapsed = .0f;

	// Use this for initialization
	void Start ()
	{
		if (Direction.HORIZONTAL == direction) {
			m_Scroller = new HScroller (targetText);
		} else {
			m_Scroller = new VScroller (targetText);
		}
	}

	// Update is called once per frame
	void Update ()
	{
		if (m_CanScroll) {
			if (m_TimeElapsed <= delay) {
				m_TimeElapsed += Time.deltaTime;
				return;
			}
			m_Scroller.Move (speed);
		}
	}

	public void OnDisplayAd (NendAdNativeView view)
	{
		m_Scroller.SetMaxScroll ();
		m_CanScroll = true;
	}
		
	private abstract class Scroller
	{
		protected Text m_Text;
		protected float m_Position = .0f;
		protected float m_MaxScroll = .0f;
		protected float m_Offset = .0f;

		public Scroller (Text text)
		{
			m_Text = text;
		}

		protected bool CanScrollMore ()
		{
			return Mathf.Abs (m_Position) <= Mathf.Abs (m_MaxScroll);
		}

		public abstract void Move (float speed);

		public abstract void SetMaxScroll ();
	}

	private class VScroller : Scroller
	{
		public VScroller (Text text) : base (text)
		{
			m_Position = m_Offset = m_Text.rectTransform.localPosition.y;
		}

		public override void Move (float speed)
		{
			m_Position += speed;
			if (!CanScrollMore ()) {
				m_Position = m_Text.rectTransform.rect.size.y * -1 + m_Offset;
			}
			m_Text.rectTransform.localPosition = new Vector2 (m_Text.rectTransform.localPosition.x, m_Position);
		}

		public override void SetMaxScroll ()
		{
			m_MaxScroll = m_Text.preferredHeight + m_Offset;
		}
	}

	private class HScroller : Scroller
	{
		public HScroller (Text text) : base (text)
		{
			m_Position = m_Offset = text.rectTransform.localPosition.x;
		}

		public override void Move (float speed)
		{
			m_Position -= speed;
			if (!CanScrollMore ()) {
				m_Position = m_Text.rectTransform.rect.size.x + m_Offset;
			}
			m_Text.rectTransform.localPosition = new Vector2 (m_Position, m_Text.rectTransform.localPosition.y);
		}

		public override void SetMaxScroll ()
		{
			m_MaxScroll = m_Text.preferredWidth - m_Offset;
		}
	}
}
