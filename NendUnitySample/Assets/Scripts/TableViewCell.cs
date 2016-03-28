using UnityEngine;

[RequireComponent (typeof(RectTransform))]
public class TableViewCell: MonoBehaviour
{
	private RectTransform m_CachedRectTransform;

	public RectTransform CachedRectTransform {
		get {
			return m_CachedRectTransform ?? (m_CachedRectTransform = GetComponent<RectTransform> ());
		}
	}

	public int DataIndex { get; set; }

	public virtual void UpdateContent (int index)
	{
	}

	public float Height {
		get { return CachedRectTransform.sizeDelta.y; }
		set {
			Vector2 sizeDelta = CachedRectTransform.sizeDelta;
			sizeDelta.y = value;
			CachedRectTransform.sizeDelta = sizeDelta;
		}
	}

	public Vector2 Top {
		get {
			Vector3[] corners = new Vector3[4];
			CachedRectTransform.GetLocalCorners (corners);
			return CachedRectTransform.anchoredPosition + new Vector2 (0.0f, corners [1].y);
		}
		set {
			Vector3[] corners = new Vector3[4];
			CachedRectTransform.GetLocalCorners (corners);
			CachedRectTransform.anchoredPosition = value - new Vector2 (0.0f, corners [1].y);
		}
	}

	public Vector2 Bottom {
		get {
			Vector3[] corners = new Vector3[4];
			CachedRectTransform.GetLocalCorners (corners);
			return CachedRectTransform.anchoredPosition + new Vector2 (0.0f, corners [3].y);
		}
		set {
			Vector3[] corners = new Vector3[4];
			CachedRectTransform.GetLocalCorners (corners);
			CachedRectTransform.anchoredPosition = value - new Vector2 (0.0f, corners [3].y);
		}
	}
}
