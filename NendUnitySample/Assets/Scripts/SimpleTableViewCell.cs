using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SimpleTableViewCell : TableViewCell, IPointerClickHandler
{
	public Text mainText;
	public Text subText;

	public override void UpdateContent (int index)
	{
		mainText.text = string.Format ("Item{0}", index + 1);
		subText.text = string.Format ("detail{0}", index + 1);
	}

	public void OnPointerClick (PointerEventData eventData)
	{
		SceneManager.LoadScene ("Menu");
	}
}
