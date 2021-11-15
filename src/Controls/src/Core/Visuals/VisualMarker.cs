using System.ComponentModel;
using Microsoft.Extensions.Logging;

namespace Microsoft.Maui.Controls
{
	public static class VisualMarker
	{
		static bool _isMaterialRegistered = false;
		static bool _warnedAboutMaterial = false;

		[EditorBrowsable(EditorBrowsableState.Never)]
		public static IVisual MatchParent { get; } = new MatchParentVisual();
		public static IVisual Default { get; } = new DefaultVisual();
		public static IVisual Material { get; } = new MaterialVisual();

		internal static void RegisterMaterial() => _isMaterialRegistered = true;
		internal static void MaterialCheck()
		{
			if (_isMaterialRegistered || _warnedAboutMaterial)
				return;

			var logger = Application.Current?.Handler?.MauiContext?.CreateLogger<IVisual>();
			_warnedAboutMaterial = true;
			if (Device.RuntimePlatform == Device.iOS || Device.RuntimePlatform == Device.Android || Device.RuntimePlatform == Device.Tizen)
				logger?.LogWarning("Visual", $"Material needs to be registered on {Device.RuntimePlatform} by calling FormsMaterial.Init() after the Microsoft.Maui.Controls.Forms.Init method call.");
			else
				logger?.LogWarning("Visual", $"Material is currently not support on {Device.RuntimePlatform}.");
		}

		public sealed class MaterialVisual : IVisual { public MaterialVisual() { } }
		public sealed class DefaultVisual : IVisual { public DefaultVisual() { } }
		internal sealed class MatchParentVisual : IVisual { public MatchParentVisual() { } }
	}
}