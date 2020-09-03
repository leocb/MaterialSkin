using System.Drawing;

namespace MaterialSkin.Themes {

	public class Theme {

		// Text

		private Color textHighEmphasisColor;
		public Color TextHighEmphasisColor {
			get => textHighEmphasisColor;
			set {
				textHighEmphasisColor = value;
				TextHighEmphasisBrush = new SolidBrush(value);
			}
		}
		public Brush TextHighEmphasisBrush { get; set; }

		private Color textMediumEmphasisColor;
		public Color TextMediumEmphasisColor {
			get => textMediumEmphasisColor;
			set {
				textMediumEmphasisColor = value;
				TextMediumEmphasisBrush = new SolidBrush(value);
			}
		}
		public Brush TextMediumEmphasisBrush { get; set; }

		private Color textDisabledOrHintColor;
		public Color TextDisabledOrHintColor {
			get => textDisabledOrHintColor;
			set {
				textDisabledOrHintColor = value;
				TextDisabledOrHintBrush = new SolidBrush(value);
			}
		}
		public Brush TextDisabledOrHintBrush { get; set; }

		// Divider

		private Color dividersColor;
		public Color DividersColor {
			get => dividersColor;
			set {
				dividersColor = value;
				DividersBrush = new SolidBrush(value);
			}
		}
		public Brush DividersBrush { get; set; }

		private Color dividersAlternativeColor;
		public Color DividersAlternativeColor {
			get => dividersAlternativeColor;
			set {
				dividersAlternativeColor = value;
				DividersAlternativeBrush = new SolidBrush(value);
			}
		}
		public Brush DividersAlternativeBrush { get; set; }

		// Checkbox / Radio / Switch

		private Color checkboxOffColor;
		public Color CheckboxOffColor {
			get => checkboxOffColor;
			set {
				checkboxOffColor = value;
				CheckboxOffBrush = new SolidBrush(value);
			}
		}
		public Brush CheckboxOffBrush { get; set; }

		private Color checkBoxOffDisabledColor;
		public Color CheckBoxOffDisabledColor {
			get => checkBoxOffDisabledColor;
			set {
				checkBoxOffDisabledColor = value;
				CheckBoxOffDisabledBrush = new SolidBrush(value);
			}
		}
		public Brush CheckBoxOffDisabledBrush { get; set; }

		// Switch

		public Color SwitchOffColor { get; set; }

		public Color SwitchOffThumbColor { get; set; }
		public Color SwitchOffTrackColor { get; set; }
		public Color SwitchOffDisabledThumbColor { get; set; }

		// Control Back colors

		private Color backgroundColor;
		public Color BackgroundColor {
			get => backgroundColor;
			set {
				backgroundColor = value;
				BackgroundBrush = new SolidBrush(value);
			}
		}
		public Brush BackgroundBrush { get; set; }

		private Color backgroundAlternativeColor;
		public Color BackgroundAlternativeColor {
			get => backgroundAlternativeColor;
			set {
				backgroundAlternativeColor = value;
				BackgroundAlternativeBrush = new SolidBrush(value);
			}
		}
		public Brush BackgroundAlternativeBrush { get; set; }

		private Color backgroundDisabledColor;
		public Color BackgroundDisabledColor {
			get => backgroundDisabledColor;
			set {
				backgroundDisabledColor = value;
				BackgroundDisabledBrush = new SolidBrush(value);
			}
		}
		public Brush BackgroundDisabledBrush { get; set; }

		private Color backgroundHoverColor;
		public Color BackgroundHoverColor {
			get => backgroundHoverColor;
			set {
				backgroundHoverColor = value;
				BackgroundHoverBrush = new SolidBrush(value);
			}
		}
		public Brush BackgroundHoverBrush { get; set; }

		private Color backgroundFocusColor;
		public Color BackgroundFocusColor {
			get => backgroundFocusColor;
			set {
				backgroundFocusColor = value;
				BackgroundFocusBrush = new SolidBrush(value);
			}
		}
		public Brush BackgroundFocusBrush { get; set; }

		// Backdrop color

		private Color backdropColor;
		public Color BackdropColor {
			get => backdropColor;
			set {
				backdropColor = value;
				BackdropBrush = new SolidBrush(value);
			}
		}
		public Brush BackdropBrush { get; set; }

		// for controls

		public virtual Color RippleColor { get; set; }

		public Color SwitchRippleColor { get; set; }

		public float DrawerLightness { get; set; }

	}
}
