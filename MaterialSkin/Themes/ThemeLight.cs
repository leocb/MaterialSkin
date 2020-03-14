using System.Drawing;

namespace MaterialSkin.Themes {

	public class ThemeLight : Theme {

		public ThemeLight() {

			// Text
			TextHighEmphasisColor = TEXT_HIGH_EMPHASIS_DARK;

			TextHighEmphasisBrush = TEXT_HIGH_EMPHASIS_DARK_BRUSH;
			TextMediumEmphasisColor = TEXT_MEDIUM_EMPHASIS_DARK;
			TextMediumEmphasisBrush = TEXT_MEDIUM_EMPHASIS_DARK_BRUSH;
			TextDisabledOrHintColor = TEXT_DISABLED_OR_HINT_DARK;
			TextDisabledOrHintBrush = TEXT_DISABLED_OR_HINT_DARK_BRUSH;

			// Divider
			DividersColor = DIVIDERS_DARK;

			DividersBrush = DIVIDERS_DARK_BRUSH;
			DividersAlternativeColor = DIVIDERS_ALTERNATIVE_DARK;
			DividersAlternativeBrush = DIVIDERS_ALTERNATIVE_DARK_BRUSH;

			// Checkbox / Radio / Switch
			CheckboxOffColor = CHECKBOX_OFF_LIGHT;

			CheckboxOffBrush = CHECKBOX_OFF_LIGHT_BRUSH;
			CheckBoxOffDisabledColor = CHECKBOX_OFF_DISABLED_LIGHT;
			CheckBoxOffDisabledBrush = CHECKBOX_OFF_DISABLED_LIGHT_BRUSH;

			// Switch
			SwitchOffColor = CHECKBOX_OFF_DARK;

			SwitchOffThumbColor = SWITCH_OFF_THUMB_LIGHT;
			SwitchOffTrackColor = SWITCH_OFF_TRACK_LIGHT;
			SwitchOffDisabledThumbColor = SWITCH_OFF_DISABLED_THUMB_LIGHT;

			// Control Back colors
			BackgroundColor = BACKGROUND_LIGHT;

			BackgroundBrush = BACKGROUND_LIGHT_BRUSH;
			BackgroundAlternativeColor = BACKGROUND_ALTERNATIVE_LIGHT;
			BackgroundAlternativeBrush = BACKGROUND_ALTERNATIVE_LIGHT_BRUSH;
			BackgroundDisabledColor = BACKGROUND_DISABLED_LIGHT;
			BackgroundDisabledBrush = BACKGROUND_DISABLED_LIGHT_BRUSH;
			BackgroundHoverColor = BACKGROUND_HOVER_LIGHT;
			BackgroundHoverBrush = BACKGROUND_HOVER_LIGHT_BRUSH;
			BackgroundFocusColor = BACKGROUND_FOCUS_LIGHT;
			BackgroundFocusBrush = BACKGROUND_FOCUS_LIGHT_BRUSH;

			// Backdrop color
			BackdropColor = BACKDROP_LIGHT;

			BackdropBrush = BACKDROP_LIGHT_BRUSH;
		}


		// Text
		private static readonly Color TEXT_HIGH_EMPHASIS_DARK = Color.FromArgb(222, 0, 0, 0); // Alpha 87%
		private static readonly Brush TEXT_HIGH_EMPHASIS_DARK_BRUSH = new SolidBrush(TEXT_HIGH_EMPHASIS_DARK);

		private static readonly Color TEXT_MEDIUM_EMPHASIS_DARK = Color.FromArgb(153, 0, 0, 0); // Alpha 60%
		private static readonly Brush TEXT_MEDIUM_EMPHASIS_DARK_BRUSH = new SolidBrush(TEXT_MEDIUM_EMPHASIS_DARK);

		private static readonly Color TEXT_DISABLED_OR_HINT_DARK = Color.FromArgb(97, 0, 0, 0); // Alpha 38%
		private static readonly Brush TEXT_DISABLED_OR_HINT_DARK_BRUSH = new SolidBrush(TEXT_DISABLED_OR_HINT_DARK);

		// Dividers and thin lines
		private static readonly Color DIVIDERS_DARK = Color.FromArgb(30, 0, 0, 0); // Alpha 30%
		private static readonly Brush DIVIDERS_DARK_BRUSH = new SolidBrush(DIVIDERS_DARK);
		private static readonly Color DIVIDERS_ALTERNATIVE_DARK = Color.FromArgb(153, 0, 0, 0); // Alpha 60%
		private static readonly Brush DIVIDERS_ALTERNATIVE_DARK_BRUSH = new SolidBrush(DIVIDERS_ALTERNATIVE_DARK);

		// Checkbox / Radio / Switches
		private static readonly Color CHECKBOX_OFF_LIGHT = Color.FromArgb(138, 0, 0, 0);

		private static readonly Brush CHECKBOX_OFF_LIGHT_BRUSH = new SolidBrush(CHECKBOX_OFF_LIGHT);
		private static readonly Color CHECKBOX_OFF_DARK = Color.FromArgb(179, 255, 255, 255);
		private static readonly Color CHECKBOX_OFF_DISABLED_LIGHT = Color.FromArgb(66, 0, 0, 0);
		private static readonly Brush CHECKBOX_OFF_DISABLED_LIGHT_BRUSH = new SolidBrush(CHECKBOX_OFF_DISABLED_LIGHT);

		// Switch specific
		private static readonly Color SWITCH_OFF_THUMB_LIGHT = Color.FromArgb(255, 255, 255, 255);

		private static readonly Color SWITCH_OFF_TRACK_LIGHT = Color.FromArgb(100, 0, 0, 0);
		private static readonly Color SWITCH_OFF_DISABLED_THUMB_LIGHT = Color.FromArgb(255, 230, 230, 230);

		// Generic back colors - for user controls
		private static readonly Color BACKGROUND_LIGHT = Color.FromArgb(255, 255, 255, 255);

		private static readonly Brush BACKGROUND_LIGHT_BRUSH = new SolidBrush(BACKGROUND_LIGHT);
		private static readonly Color BACKGROUND_ALTERNATIVE_LIGHT = Color.FromArgb(10, 0, 0, 0);
		private static readonly Brush BACKGROUND_ALTERNATIVE_LIGHT_BRUSH = new SolidBrush(BACKGROUND_ALTERNATIVE_LIGHT);
		private static readonly Color BACKGROUND_HOVER_LIGHT = Color.FromArgb(20, 0, 0, 0);
		private static readonly Brush BACKGROUND_HOVER_LIGHT_BRUSH = new SolidBrush(BACKGROUND_HOVER_LIGHT);
		private static readonly Color BACKGROUND_FOCUS_LIGHT = Color.FromArgb(30, 0, 0, 0);
		private static readonly Brush BACKGROUND_FOCUS_LIGHT_BRUSH = new SolidBrush(BACKGROUND_FOCUS_LIGHT);
		private static readonly Color BACKGROUND_DISABLED_LIGHT = Color.FromArgb(25, 0, 0, 0);
		private static readonly Brush BACKGROUND_DISABLED_LIGHT_BRUSH = new SolidBrush(BACKGROUND_DISABLED_LIGHT);

		// Backdrop colors - for containers, like forms or panels
		private static readonly Color BACKDROP_LIGHT = Color.FromArgb(255, 242, 242, 242);

		private static readonly Brush BACKDROP_LIGHT_BRUSH = new SolidBrush(BACKGROUND_LIGHT);

	}
}
