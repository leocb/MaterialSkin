using System.Drawing;

namespace MaterialSkin.Themes {

	public class Theme {

		// Text
		public Color TextHighEmphasisColor { get; set; }

		public Brush TextHighEmphasisBrush { get; set; }
		public Color TextMediumEmphasisColor { get; set; }
		public Brush TextMediumEmphasisBrush { get; set; }
		public Color TextDisabledOrHintColor { get; set; }
		public Brush TextDisabledOrHintBrush { get; set; }

		// Divider
		public Color DividersColor { get; set; }

		public Brush DividersBrush { get; set; }
		public Color DividersAlternativeColor { get; set; }
		public Brush DividersAlternativeBrush { get; set; }

		// Checkbox / Radio / Switch
		public Color CheckboxOffColor { get; set; }

		public Brush CheckboxOffBrush { get; set; }
		public Color CheckBoxOffDisabledColor { get; set; }
		public Brush CheckBoxOffDisabledBrush { get; set; }

		// Switch
		public Color SwitchOffColor { get; set; }

		public Color SwitchOffThumbColor { get; set; }
		public Color SwitchOffTrackColor { get; set; }
		public Color SwitchOffDisabledThumbColor { get; set; }

		// Control Back colors
		public Color BackgroundColor { get; set; }

		public Brush BackgroundBrush { get; set; }
		public Color BackgroundAlternativeColor { get; set; }
		public Brush BackgroundAlternativeBrush { get; set; }
		public Color BackgroundDisabledColor { get; set; }
		public Brush BackgroundDisabledBrush { get; set; }
		public Color BackgroundHoverColor { get; set; }
		public Brush BackgroundHoverBrush { get; set; }
		public Color BackgroundFocusColor { get; set; }
		public Brush BackgroundFocusBrush { get; set; }

		// Backdrop color
		public Color BackdropColor { get; set; }

		public Brush BackdropBrush { get; set; }

	}
}
