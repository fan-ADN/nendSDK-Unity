using System.Runtime.InteropServices;

namespace NendUnityPlugin.Platform.iOS
{
	internal class IOSIconInterface : NendAdIconInterface
	{
		public void TryCreateIcons (string paramString)
		{
			_TryCreateIcons (paramString);
		}

		public void ShowIcons (string gameObject)
		{
			_ShowIcons (gameObject);
		}

		public void HideIcons (string gameObject)
		{
			_HideIcons (gameObject);
		}

		public void ResumeIcons (string gameObject)
		{
			_ResumeIcons (gameObject);
		}

		public void PauseIcons (string gameObject)
		{
			_PauseIcons (gameObject);
		}

		public void DestroyIcons (string gameObject)
		{
			_DestroyIcons (gameObject);
		}

		public void LayoutIcons (string gameObject, string paramString)
		{
			_LayoutIcons (gameObject, paramString);
		}

		[DllImport ("__Internal")]
		private static extern void _TryCreateIcons (string paramString);

		[DllImport ("__Internal")]
		private static extern void _ShowIcons (string gameObject);

		[DllImport ("__Internal")]
		private static extern void _HideIcons (string gameObject);

		[DllImport ("__Internal")]
		private static extern void _ResumeIcons (string gameObject);

		[DllImport ("__Internal")]
		private static extern void _PauseIcons (string gameObject);

		[DllImport ("__Internal")]
		private static extern void _DestroyIcons (string gameObject);

		[DllImport ("__Internal")]
		private static extern void _LayoutIcons (string gameObject, string paramString);
	}
}