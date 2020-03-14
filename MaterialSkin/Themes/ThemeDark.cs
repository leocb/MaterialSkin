using System.Drawing;

namespace MaterialSkin.Themes {

	public class ThemeDark : Theme {

		public ThemeDark() {

			// Text
			TextHighEmphasisColor = TEXT_HIGH_EMPHASIS_LIGHT;

			TextHighEmphasisBrush = TEXT_HIGH_EMPHASIS_LIGHT_BRUSH;
			TextMediumEmphasisColor = TEXT_MEDIUM_EMPHASIS_LIGHT;
			TextMediumEmphasisBrush = TEXT_MEDIUM_EMPHASIS_LIGHT_BRUSH;
			TextDisabledOrHintColor = TEXT_DISABLED_OR_HINT_LIGHT;
			TextDisabledOrHintBrush = TEXT_DISABLED_OR_HINT_LIGHT_BRUSH;

			// Divider
			DividersColor = DIVIDERS_LIGHT;

			DividersBrush = DIVIDERS_LIGHT_BRUSH;
			DividersAlternativeColor = DIVIDERS_ALTERNATIVE_LIGHT;
			DividersAlternativeBrush = DIVIDERS_ALTERNATIVE_LIGHT_BRUSH;

			// Checkbox / Radio / Switch
			CheckboxOffColor = CHECKBOX_OFF_DARK;

			CheckboxOffBrush = CHECKBOX_OFF_DARK_BRUSH;
			CheckBoxOffDisabledColor = CHECKBOX_OFF_DISABLED_DARK;
			CheckBoxOffDisabledBrush = CHECKBOX_OFF_DISABLED_DARK_BRUSH;

			// Switch
			SwitchOffColor = CHECKBOX_OFF_LIGHT;

			SwitchOffThumbColor = SWITCH_OFF_THUMB_DARK;
			SwitchOffTrackColor = SWITCH_OFF_TRACK_DARK;
			SwitchOffDisabledThumbColor = SWITCH_OFF_DISABLED_THUMB_DARK;

			// Control Back colors
			BackgroundColor = BACKGROUND_DARK;

			BackgroundBrush = BACKGROUND_DARK_BRUSH;
			BackgroundAlternativeColor = BACKGROUND_ALTERNATIVE_DARK;
			BackgroundAlternativeBrush = BACKGROUND_ALTERNATIVE_DARK_BRUSH;
			BackgroundDisabledColor = BACKGROUND_DISABLED_DARK;
			BackgroundDisabledBrush = BACKGROUND_DISABLED_DARK_BRUSH;
			BackgroundHoverColor = BACKGROUND_HOVER_DARK;
			BackgroundHoverBrush = BACKGROUND_HOVER_DARK_BRUSH;
			BackgroundFocusColor = BACKGROUND_FOCUS_DARK;
			BackgroundFocusBrush = BACKGROUND_FOCUS_DARK_BRUSH;

			// Backdrop color
			BackdropColor = BACKDROP_DARK;

			BackdropBrush = BACKDROP_DARK_BRUSH;
		}


		// Text
		private static readonly Color TEXT_HIGH_EMPHASIS_LIGHT = Color.FromArgb(222, 255, 255, 255); // Alpha 87%

		private static readonly Brush TEXT_HIGH_EMPHASIS_LIGHT_BRUSH = new SolidBrush(TEXT_HIGH_EMPHASIS_LIGHT);

		private static readonly Color TEXT_MEDIUM_EMPHASIS_LIGHT = Color.FromArgb(153, 255, 255, 255); // Alpha 60%
		private static readonly Brush TEXT_MEDIUM_EMPHASIS_LIGHT_BRUSH = new SolidBrush(TEXT_MEDIUM_EMPHASIS_LIGHT);

		private static readonly Color TEXT_DISABLED_OR_HINT_LIGHT = Color.FromArgb(97, 255, 255, 255); // Alpha 38%
		private static readonly Brush TEXT_DISABLED_OR_HINT_LIGHT_BRUSH = new SolidBrush(TEXT_DISABLED_OR_HINT_LIGHT);

		// Dividers and thin lines
		private static readonly Color DIVIDERS_LIGHT = Color.FromArgb(30, 255, 255, 255); // Alpha 30%

		private static readonly Brush DIVIDERS_LIGHT_BRUSH = new SolidBrush(DIVIDERS_LIGHT);
		private static readonly Color DIVIDERS_ALTERNATIVE_LIGHT = Color.FromArgb(153, 255, 255, 255); // Alpha 60%
		private static readonly Brush DIVIDERS_ALTERNATIVE_LIGHT_BRUSH = new SolidBrush(DIVIDERS_ALTERNATIVE_LIGHT);

		// Checkbox / Radio / Switches
		private static readonly Color CHECKBOX_OFF_LIGHT = Color.FromArgb(138, 0, 0, 0);

		private static readonly Color CHECKBOX_OFF_DARK = Color.FromArgb(179, 255, 255, 255);
		private static readonly Brush CHECKBOX_OFF_DARK_BRUSH = new SolidBrush(CHECKBOX_OFF_DARK);
		private static readonly Color CHECKBOX_OFF_DISABLED_DARK = Color.FromArgb(77, 255, 255, 255);
		private static readonly Brush CHECKBOX_OFF_DISABLED_DARK_BRUSH = new SolidBrush(CHECKBOX_OFF_DISABLED_DARK);

		// Switch specific
		private static readonly Color SWITCH_OFF_THUMB_DARK = Color.FromArgb(255, 190, 190, 190);
		private static readonly Color SWITCH_OFF_TRACK_DARK = Color.FromArgb(100, 255, 255, 255);
		private static readonly Color SWITCH_OFF_DISABLED_THUMB_DARK = Color.FromArgb(255, 150, 150, 150);

		// Generic back colors - for user controls
		private static readonly Color BACKGROUND_DARK = Color.FromArgb(255, 80, 80, 80);
		private static readonly Brush BACKGROUND_DARK_BRUSH = new SolidBrush(BACKGROUND_DARK);
		private static readonly Color BACKGROUND_ALTERNATIVE_DARK = Color.FromArgb(10, 255, 255, 255);
		private static readonly Brush BACKGROUND_ALTERNATIVE_DARK_BRUSH = new SolidBrush(BACKGROUND_ALTERNATIVE_DARK);
		private static readonly Color BACKGROUND_HOVER_DARK = Color.FromArgb(20, 255, 255, 255);
		private static readonly Brush BACKGROUND_HOVER_DARK_BRUSH = new SolidBrush(BACKGROUND_HOVER_DARK);
		private static readonly Color BACKGROUND_FOCUS_DARK = Color.FromArgb(30, 255, 255, 255);
		private static readonly Brush BACKGROUND_FOCUS_DARK_BRUSH = new SolidBrush(BACKGROUND_FOCUS_DARK);
		private static readonly Color BACKGROUND_DISABLED_DARK = Color.FromArgb(25, 255, 255, 255);
		private static readonly Brush BACKGROUND_DISABLED_DARK_BRUSH = new SolidBrush(BACKGROUND_DISABLED_DARK);

		// Backdrop colors - for containers, like forms or panels
		private static readonly Color BACKDROP_DARK = Color.FromArgb(255, 50, 50, 50);
		private static readonly Brush BACKDROP_DARK_BRUSH = new SolidBrush(BACKGROUND_DARK);

	}
}