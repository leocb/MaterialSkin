using System;
using System.Drawing;

namespace MaterialSkin.Themes {

	public class ThemeLight : Theme {

		private MaterialSkinManager skinManager;

		public ThemeLight(MaterialSkinManager skinManager) {
			this.skinManager = skinManager;

			// Text
			TextHighEmphasisColor = Color.FromArgb(222, 0, 0, 0); // Alpha 87%
			TextMediumEmphasisColor = Color.FromArgb(153, 0, 0, 0); // Alpha 60%
			TextDisabledOrHintColor = Color.FromArgb(97, 0, 0, 0); // Alpha 38%

			// Divider
			DividersColor = Color.FromArgb(30, 0, 0, 0); // Alpha 30%
			DividersAlternativeColor = Color.FromArgb(153, 0, 0, 0); // Alpha 60%

			// Checkbox / Radio / Switch
			CheckboxOffColor = Color.FromArgb(138, 0, 0, 0);
			CheckBoxOffDisabledColor = Color.FromArgb(66, 0, 0, 0);

			// Switch
			SwitchOffColor = Color.FromArgb(179, 255, 255, 255);

			SwitchOffThumbColor = Color.FromArgb(255, 255, 255, 255);
			SwitchOffTrackColor = Color.FromArgb(100, 0, 0, 0);
			SwitchOffDisabledThumbColor = Color.FromArgb(255, 230, 230, 230);

			// Control Back colors
			BackgroundColor = Color.FromArgb(255, 255, 255, 255);
			BackgroundAlternativeColor = Color.FromArgb(10, 0, 0, 0);
			BackgroundDisabledColor = Color.FromArgb(25, 0, 0, 0);
			BackgroundHoverColor = Color.FromArgb(20, 0, 0, 0);
			BackgroundFocusColor = Color.FromArgb(30, 0, 0, 0);

			// Backdrop colors - for containers, like forms or panels
			BackdropColor = Color.FromArgb(255, 242, 242, 242);
			BackdropBrush = new SolidBrush(BackgroundColor); // not a bug?

			// for controls
			SwitchRippleColor = Color.Black;
			DrawerLightness = 0f;
		}


		public override Color RippleColor {
			get => skinManager.ColorScheme.PrimaryColor;
			set => throw new InvalidOperationException();
		}

	}
}
