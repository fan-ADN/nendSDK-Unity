using UnityEngine;
using UnityEngine.UI;
using System;
using System.Linq;
using System.Collections.Generic;

using NendUnityPlugin.AD.Native;

[RequireComponent (typeof(ScrollRect))]
[RequireComponent (typeof(RectTransform))]
public class TableViewController : MonoBehaviour
{
	[SerializeField] private int itemCount = 30;
	[SerializeField] private int adInterval = 5;
	[SerializeField] private int[] adIndexes;
	[SerializeField] private GameObject cellAd;
	[SerializeField] private GameObject cellBase;

	private Vector2 m_PrevScrollPos;
	#if UNITY_5_1 || UNITY_5_2 || UNITY_5_3_OR_NEWER
	private Rect m_VisibleRect = new Rect (Vector2.zero, Vector2.zero);
	#else
	private Rect m_VisibleRect = new Rect (0, 0, 0, 0);
	#endif
	private Dictionary<int, TableViewCell> m_NativeAdCells = new Dictionary<int, TableViewCell> ();
	private LinkedList<TableViewCell> m_Cells = new LinkedList<TableViewCell> ();
	private List<TableViewCell> m_UnusedCells = new List<TableViewCell> ();

	private RectTransform m_CachedRectTransform;

	public RectTransform CachedRectTransform {
		get {
			return m_CachedRectTransform ?? (m_CachedRectTransform = GetComponent<RectTransform> ());
		}
	}

	private ScrollRect m_CachedScrollRect;

	public ScrollRect CachedScrollRect {
		get {
			return m_CachedScrollRect ?? (m_CachedScrollRect = GetComponent<ScrollRect> ());
		}
	}

	void Awake ()
	{
		SetupNativeAdCells (itemCount);
		UpdateContents ();
	}

	void Start ()
	{
		cellBase.SetActive (false);
		cellAd.SetActive (false);
	}

	private void SetupNativeAdCells (int dataCount)
	{
		if (null != adIndexes && 0 < adIndexes.Length) {
			var indexes = adIndexes.Where (i => dataCount > i).ToArray ();
			foreach (int index in indexes) {
				CreateNativeAdAtIndex (index);
			}
		} else if (1 < adInterval) {
			var repeat = 1;
			var index = repeat * adInterval;
			while (dataCount > index) {
				CreateNativeAdAtIndex (index);
				index = ++repeat * adInterval;
			}
		}
	}

	private void CreateNativeAdAtIndex (int index)
	{
		var obj = Instantiate (cellAd) as GameObject;
		var cell = obj.GetComponent <TableViewCell> ();
		SetCellParent (cell, cellAd.transform.parent);

		obj.SetActive (false);
		m_NativeAdCells [index] = cell;
	}

	private void SetCellParent (TableViewCell cell, Transform parent)
	{
		var scale = cell.transform.localScale;
		var sizeDelta = cell.CachedRectTransform.sizeDelta;
		var offsetMin = cell.CachedRectTransform.offsetMin;
		var offsetMax = cell.CachedRectTransform.offsetMax;

		cell.transform.SetParent (parent);

		cell.transform.localScale = scale;
		cell.CachedRectTransform.sizeDelta = sizeDelta;
		cell.CachedRectTransform.offsetMin = offsetMin;
		cell.CachedRectTransform.offsetMax = offsetMax;
	}

	private void UpdateContents ()
	{
		UpdateContentSize ();
		UpdateVisibleRect ();

		if (m_Cells.Count < 1) {
			var cellTop = Vector2.zero;
			for (int i = 0; i < itemCount; i++) {
				float cellHeight = CellHeightAtIndex (i);
				var cellBottom = cellTop + new Vector2 (0.0f, -cellHeight);
				if ((cellTop.y <= m_VisibleRect.y && cellTop.y >= m_VisibleRect.y - m_VisibleRect.height) ||
				    (cellBottom.y <= m_VisibleRect.y && cellBottom.y >= m_VisibleRect.y - m_VisibleRect.height)) {
					var cell = CreateCellForIndex (i);
					cell.Top = cellTop;
					break;
				}
				cellTop = cellBottom;
			}

			FillVisibleRectWithCells ();
		} else {
			var node = m_Cells.First;
			UpdateCellForIndex (node.Value, node.Value.DataIndex);
			node = node.Next;

			while (node != null) {
				UpdateCellForIndex (node.Value, node.Previous.Value.DataIndex + 1);
				node.Value.Top = node.Previous.Value.Bottom;
				node = node.Next;
			}

			FillVisibleRectWithCells ();
		}
	}

	private float CellHeightAtIndex (int index)
	{
		return 80.0f;
	}

	private void UpdateContentSize ()
	{
		float contentHeight = 0.0f;
		for (int i = 0; i < itemCount; i++) {
			contentHeight += CellHeightAtIndex (i);
		}

		var sizeDelta = CachedScrollRect.content.sizeDelta;
		sizeDelta.y = contentHeight;
		CachedScrollRect.content.sizeDelta = sizeDelta;
	}

	private TableViewCell CreateCellForIndex (int index)
	{
		GameObject obj;
		TableViewCell cell;
		if (m_NativeAdCells.ContainsKey (index)) {
			cell = m_NativeAdCells [index];
			obj = cell.gameObject;
		} else {
			obj = Instantiate (cellBase) as GameObject;
			cell = obj.GetComponent<TableViewCell> ();
			SetCellParent (cell, cellBase.transform.parent);
		}
		obj.SetActive (true);

		UpdateCellForIndex (cell, index);
		m_Cells.AddLast (cell);

		return cell;
	}

	private void UpdateCellForIndex (TableViewCell cell, int index)
	{
		cell.DataIndex = index;

		if (cell.DataIndex >= 0 && cell.DataIndex <= itemCount - 1) {
			cell.gameObject.SetActive (true);
			cell.UpdateContent (index);
			cell.Height = CellHeightAtIndex (cell.DataIndex);
		} else {
			cell.gameObject.SetActive (false);
		}
	}

	private void UpdateVisibleRect ()
	{
		m_VisibleRect.x = CachedScrollRect.content.anchoredPosition.x;
		m_VisibleRect.y = -CachedScrollRect.content.anchoredPosition.y;
		m_VisibleRect.width = CachedRectTransform.rect.width;
		m_VisibleRect.height = CachedRectTransform.rect.height;
	}

	private void FillVisibleRectWithCells ()
	{
		if (m_Cells.Count < 1) {
			return;
		}

		var lastCell = m_Cells.Last.Value;
		int nextCellDataIndex = lastCell.DataIndex + 1;
		var nextCellTop = lastCell.Bottom;

		while (nextCellDataIndex < itemCount && nextCellTop.y >= m_VisibleRect.y - m_VisibleRect.height) {
			var cell = CreateCellForIndex (nextCellDataIndex);
			cell.Top = nextCellTop;

			lastCell = cell;
			nextCellDataIndex = lastCell.DataIndex + 1;
			nextCellTop = lastCell.Bottom;
		}
	}

	public void OnScrollPosChanged (Vector2 scrollPos)
	{
		UpdateVisibleRect ();
		ReuseCells ((scrollPos.y < m_PrevScrollPos.y) ? 1 : -1);
		m_PrevScrollPos = scrollPos;
	}

	private void ReuseCells (int scrollDirection)
	{
		if (m_Cells.Count < 1) {
			return;
		}

		TableViewCell cell;
		if (scrollDirection > 0) {
			var firstCell = m_Cells.First.Value;
			while (firstCell.Bottom.y > m_VisibleRect.y) {
				var lastCell = m_Cells.Last.Value;
				var nextIndex = lastCell.DataIndex + 1;
				if (m_NativeAdCells.ContainsKey (nextIndex)) {
					if (!IsAdCell (firstCell)) {
						m_UnusedCells.Add (firstCell);
					}
					firstCell.gameObject.SetActive (false);
					cell = m_NativeAdCells [nextIndex];
				} else if (IsAdCell (firstCell)) {
					if (0 < m_UnusedCells.Count) {
						cell = m_UnusedCells [0];
						m_UnusedCells.RemoveAt (0);
					} else {
						cell = CreateCellForIndex (nextIndex);
					}
					firstCell.gameObject.SetActive (false);
				} else {
					cell = firstCell;
				}
				UpdateCellForIndex (cell, nextIndex);
				cell.Top = lastCell.Bottom;

				m_Cells.AddLast (cell);
				m_Cells.RemoveFirst ();
				firstCell = m_Cells.First.Value;
			}
			FillVisibleRectWithCells ();
		} else if (scrollDirection < 0) {
			var lastCell = m_Cells.Last.Value;
			while (lastCell.Top.y < m_VisibleRect.y - m_VisibleRect.height) {
				var firstCell = m_Cells.First.Value;
				var previousIndex = firstCell.DataIndex - 1;
				if (m_NativeAdCells.ContainsKey (previousIndex)) {
					if (!IsAdCell (lastCell)) {
						m_UnusedCells.Add (lastCell);
					}
					lastCell.gameObject.SetActive (false);
					cell = m_NativeAdCells [previousIndex];
				} else if (IsAdCell (lastCell)) {
					if (0 < m_UnusedCells.Count) {
						cell = m_UnusedCells [0];
						m_UnusedCells.RemoveAt (0);
					} else {
						cell = CreateCellForIndex (previousIndex);
					}
					lastCell.gameObject.SetActive (false);
				} else {
					cell = lastCell;
				}
				UpdateCellForIndex (cell, previousIndex);
				cell.Bottom = firstCell.Top;

				m_Cells.AddFirst (cell);
				m_Cells.RemoveLast ();
				lastCell = m_Cells.Last.Value;
			}
		}
	}

	private bool IsAdCell (TableViewCell cell)
	{
		return cell is NativeAdTableViewCell;
	}

	#region AdEvents

	public void OnLoad (NendAdNativeView view)
	{
		Debug.Log ("OnLoad: " + view.ViewTag);
	}

	public void OnFailedToLoad (NendAdNativeView view, int code, string message)
	{
		Debug.Log ("OnFailedToLoad: " + view.ViewTag);
	}

	#endregion
}