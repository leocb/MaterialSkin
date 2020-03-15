using System;
using System.Drawing;

namespace MaterialSkin.Themes {

	public class ThemeDark : Theme {

		private readonly MaterialSkinManager skinManager;

		public ThemeDark(MaterialSkinManager skinManager) {
			this.skinManager = skinManager;

			// Text
			TextHighEmphasisColor = Color.FromArgb(222, 255, 255, 255); // Alpha 87%
			TextMediumEmphasisColor = Color.FromArgb(153, 255, 255, 255); // Alpha 60%
			TextDisabledOrHintColor = Color.FromArgb(97, 255, 255, 255); // Alpha 38%

			// Divider
			DividersColor = Color.FromArgb(30, 255, 255, 255); // Alpha 30%
			DividersAlternativeColor = Color.FromArgb(153, 255, 255, 255); // Alpha 60%

			// Checkbox / Radio / Switch
			CheckboxOffColor = Color.FromArgb(179, 255, 255, 255);
			CheckBoxOffDisabledColor = Color.FromArgb(77, 255, 255, 255);

			// Switch
			SwitchOffColor = Color.FromArgb(138, 0, 0, 0);

			SwitchOffThumbColor = Color.FromArgb(255, 190, 190, 190);
			SwitchOffTrackColor = Color.FromArgb(100, 255, 255, 255);
			SwitchOffDisabledThumbColor = Color.FromArgb(255, 150, 150, 150);

			// Control Back colors
			BackgroundColor = Color.FromArgb(255, 80, 80, 80);
			BackgroundAlternativeColor = Color.FromArgb(10, 255, 255, 255);
			BackgroundDisabledColor = Color.FromArgb(25, 255, 255, 255);
			BackgroundHoverColor = Color.FromArgb(20, 255, 255, 255);
			BackgroundFocusColor = Color.FromArgb(30, 255, 255, 255);

			// Backdrop colors - for containers, like forms or panels
			BackdropColor = Color.FromArgb(255, 50, 50, 50);
			BackdropBrush = new SolidBrush(BackgroundColor); // not a bug?

			// for controls
			SwitchRippleColor = Color.White;
			DrawerLightness = 1f;
		}


		public override Color RippleColor {
			get => skinManager.ColorScheme.LightPrimaryColor;
			set => throw new InvalidOperationException();
		}

	}
}