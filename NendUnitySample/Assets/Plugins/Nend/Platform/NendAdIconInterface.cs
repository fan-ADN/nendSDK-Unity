namespace NendUnityPlugin.Platform
{
	internal interface NendAdIconInterface
	{
		void TryCreateIcons (string paramString);

		void ShowIcons (string gameObject);

		void HideIcons (string gameObject);

		void ResumeIcons (string gameObject);

		void PauseIcons (string gameObject);

		void DestroyIcons (string gameObject);

		void LayoutIcons (string gameObject, string paramString);
	}
}
