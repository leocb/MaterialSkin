namespace MaterialSkin
{
    using MaterialSkin.Controls;
    using MaterialSkin.Properties;
    using MaterialSkin.Themes;
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Drawing.Text;
    using System.Linq;
    using System.Runtime.InteropServices;
    using System.Windows.Forms;

    public class MaterialSkinManager
    {
        private static MaterialSkinManager _instance;

        private readonly List<MaterialForm> _formsToManage = new List<MaterialForm>();

        public delegate void SkinManagerEventHandler(object sender);

        public event SkinManagerEventHandler ColorSchemeChanged;

        public event SkinManagerEventHandler ThemeChanged;

        /// <summary>
        /// Set this property to false to stop enforcing the backcolor on non-materialSkin components
        /// </summary>
        public bool EnforceBackcolorOnAllComponents = true;

        public static MaterialSkinManager Instance => _instance ?? (_instance = new MaterialSkinManager());

        public int FORM_PADDING = 14;

        // Constructor
        private MaterialSkinManager()
        {
			Theme = new ThemeLight(this);
            ColorScheme = new ColorScheme(Primary.Indigo500, Primary.Indigo700, Primary.Indigo100, Accent.Pink200, TextShade.WHITE);

            // Create and cache Roboto fonts
            // Thanks https://www.codeproject.com/Articles/42041/How-to-Use-a-Font-Without-Installing-it
            // And https://www.codeproject.com/Articles/107376/Embedding-Font-To-Resources

            // Add font to system table in memory and save the font family
            addFont(Resources.Roboto_Thin);
            addFont(Resources.Roboto_Light);
            addFont(Resources.Roboto_Regular);
            addFont(Resources.Roboto_Medium);
            addFont(Resources.Roboto_Bold);
            addFont(Resources.Roboto_Black);

            RobotoFontFamilies = new Dictionary<string, FontFamily>();
            foreach (FontFamily ff in privateFontCollection.Families.ToArray())
            {
                RobotoFontFamilies.Add(ff.Name.Replace(' ', '_'), ff);
            }

            // create and save font handles for GDI
            logicalFonts = new Dictionary<string, IntPtr>(18);
            logicalFonts.Add("H1", createLogicalFont("Roboto Light", 96, NativeTextRenderer.logFontWeight.FW_LIGHT));
            logicalFonts.Add("H2", createLogicalFont("Roboto Light", 60, NativeTextRenderer.logFontWeight.FW_LIGHT));
            logicalFonts.Add("H3", createLogicalFont("Roboto", 48, NativeTextRenderer.logFontWeight.FW_REGULAR));
            logicalFonts.Add("H4", createLogicalFont("Roboto", 34, NativeTextRenderer.logFontWeight.FW_REGULAR));
            logicalFonts.Add("H5", createLogicalFont("Roboto", 24, NativeTextRenderer.logFontWeight.FW_REGULAR));
            logicalFonts.Add("H6", createLogicalFont("Roboto Medium", 20, NativeTextRenderer.logFontWeight.FW_MEDIUM));
            logicalFonts.Add("Subtitle1", createLogicalFont("Roboto", 16, NativeTextRenderer.logFontWeight.FW_REGULAR));
            logicalFonts.Add("Subtitle2", createLogicalFont("Roboto Medium", 14, NativeTextRenderer.logFontWeight.FW_MEDIUM));
            logicalFonts.Add("Body1", createLogicalFont("Roboto", 16, NativeTextRenderer.logFontWeight.FW_REGULAR));
            logicalFonts.Add("Body2", createLogicalFont("Roboto", 14, NativeTextRenderer.logFontWeight.FW_REGULAR));
            logicalFonts.Add("Button", createLogicalFont("Roboto Medium", 14, NativeTextRenderer.logFontWeight.FW_MEDIUM));
            logicalFonts.Add("Caption", createLogicalFont("Roboto", 12, NativeTextRenderer.logFontWeight.FW_REGULAR));
            logicalFonts.Add("Overline", createLogicalFont("Roboto", 10, NativeTextRenderer.logFontWeight.FW_REGULAR));
            // Logical fonts for textbox animation
            logicalFonts.Add("textBox16", createLogicalFont("Roboto", 16, NativeTextRenderer.logFontWeight.FW_REGULAR));
            logicalFonts.Add("textBox15", createLogicalFont("Roboto", 15, NativeTextRenderer.logFontWeight.FW_REGULAR));
            logicalFonts.Add("textBox14", createLogicalFont("Roboto", 14, NativeTextRenderer.logFontWeight.FW_REGULAR));
            logicalFonts.Add("textBox13", createLogicalFont("Roboto Medium", 13, NativeTextRenderer.logFontWeight.FW_MEDIUM));
            logicalFonts.Add("textBox12", createLogicalFont("Roboto Medium", 12, NativeTextRenderer.logFontWeight.FW_MEDIUM));
        }

        // Destructor
        ~MaterialSkinManager()
        {
            // RemoveFontMemResourceEx
            foreach (IntPtr handle in logicalFonts.Values)
            {
                NativeTextRenderer.DeleteObject(handle);
            }
        }

        // Themes
        private Theme _theme;

        public Theme Theme
        {
            get { return _theme; }
            set
            {
                _theme = value;
                UpdateBackgrounds();
                ThemeChanged?.Invoke(this);
            }
        }

		private Theme _themeLight, _themeDark;

		public bool EnabledLightTheme { get; private set; }

		public void SetThemes(Theme themeLight, Theme themeDark, bool defaultLight) {
			_themeLight = themeLight;
			_themeDark = themeDark;
			EnabledLightTheme = defaultLight;
			Theme = EnabledLightTheme ? _themeLight : _themeDark;
		}

		public enum ThemeSelector {
			Opposite,
			Light,
			Dark
		}

		public void SwitchTheme(ThemeSelector selector) {
			switch (selector) {
				case ThemeSelector.Light:
					EnabledLightTheme = true;
					break;
				case ThemeSelector.Dark:
					EnabledLightTheme = false;
					break;
				case ThemeSelector.Opposite:
					EnabledLightTheme = !EnabledLightTheme;
					break;
			}
			Theme = EnabledLightTheme ? _themeLight : _themeDark;
		}


        private ColorScheme _colorScheme;

        public ColorScheme ColorScheme
        {
            get { return _colorScheme; }
            set
            {
                _colorScheme = value;
                UpdateBackgrounds();
                ColorSchemeChanged?.Invoke(this);
            }
        }


        // Getters - Using these makes handling the dark theme switching easier
        // Text
        public Color TextHighEmphasisColor => Theme.TextHighEmphasisColor;

        public Brush TextHighEmphasisBrush => Theme.TextHighEmphasisBrush;
        public Color TextMediumEmphasisColor => Theme.TextMediumEmphasisColor;
        public Brush TextMediumEmphasisBrush => Theme.TextMediumEmphasisBrush;
        public Color TextDisabledOrHintColor => Theme.TextDisabledOrHintColor;
        public Brush TextDisabledOrHintBrush => Theme.TextDisabledOrHintBrush;

        // Divider
        public Color DividersColor => Theme.DividersColor;

        public Brush DividersBrush => Theme.DividersBrush;
        public Color DividersAlternativeColor => Theme.DividersAlternativeColor;
        public Brush DividersAlternativeBrush => Theme.DividersAlternativeBrush;

        // Checkbox / Radio / Switch
        public Color CheckboxOffColor => Theme.CheckboxOffColor;

        public Brush CheckboxOffBrush => Theme.CheckboxOffBrush;
        public Color CheckBoxOffDisabledColor => Theme.CheckBoxOffDisabledColor;
        public Brush CheckBoxOffDisabledBrush => Theme.CheckBoxOffDisabledBrush;

        // Switch
        public Color SwitchOffColor => Theme.SwitchOffColor;

        public Color SwitchOffThumbColor => Theme.SwitchOffThumbColor;
        public Color SwitchOffTrackColor => Theme.SwitchOffTrackColor;
        public Color SwitchOffDisabledThumbColor => Theme.SwitchOffDisabledThumbColor;

        // Control Back colors
        public Color BackgroundColor => Theme.BackgroundColor;

        public Brush BackgroundBrush => Theme.BackgroundBrush;
        public Color BackgroundAlternativeColor => Theme.BackgroundAlternativeColor;
        public Brush BackgroundAlternativeBrush => Theme.BackgroundAlternativeBrush;
        public Color BackgroundDisabledColor => Theme.BackgroundDisabledColor;
        public Brush BackgroundDisabledBrush => Theme.BackgroundDisabledBrush;
        public Color BackgroundHoverColor => Theme.BackgroundHoverColor;
        public Brush BackgroundHoverBrush => Theme.BackgroundHoverBrush;
        public Color BackgroundFocusColor => Theme.BackgroundFocusColor;
        public Brush BackgroundFocusBrush => Theme.BackgroundFocusBrush;

        // Backdrop color
        public Color BackdropColor => Theme.BackdropColor;

        public Brush BackdropBrush => Theme.BackdropBrush;

        // Font Handling
        public enum fontType
        {
            H1,
            H2,
            H3,
            H4,
            H5,
            H6,
            Subtitle1,
            Subtitle2,
            Body1,
            Body2,
            Button,
            Caption,
            Overline
        }

        public Font getFontByType(fontType type)
        {
            switch (type)
            {
                case fontType.H1:
                    return new Font(RobotoFontFamilies["Roboto_Light"], 96f, FontStyle.Regular, GraphicsUnit.Pixel);

                case fontType.H2:
                    return new Font(RobotoFontFamilies["Roboto_Light"], 60f, FontStyle.Regular, GraphicsUnit.Pixel);

                case fontType.H3:
                    return new Font(RobotoFontFamilies["Roboto"], 48f, FontStyle.Bold, GraphicsUnit.Pixel);

                case fontType.H4:
                    return new Font(RobotoFontFamilies["Roboto"], 34f, FontStyle.Bold, GraphicsUnit.Pixel);

                case fontType.H5:
                    return new Font(RobotoFontFamilies["Roboto"], 24f, FontStyle.Bold, GraphicsUnit.Pixel);

                case fontType.H6:
                    return new Font(RobotoFontFamilies["Roboto_Medium"], 20f, FontStyle.Bold, GraphicsUnit.Pixel);

                case fontType.Subtitle1:
                    return new Font(RobotoFontFamilies["Roboto"], 16f, FontStyle.Regular, GraphicsUnit.Pixel);

                case fontType.Subtitle2:
                    return new Font(RobotoFontFamilies["Roboto_Medium"], 14f, FontStyle.Bold, GraphicsUnit.Pixel);

                case fontType.Body1:
                    return new Font(RobotoFontFamilies["Roboto"], 14f, FontStyle.Regular, GraphicsUnit.Pixel);

                case fontType.Body2:
                    return new Font(RobotoFontFamilies["Roboto"], 12f, FontStyle.Regular, GraphicsUnit.Pixel);

                case fontType.Button:
                    return new Font(RobotoFontFamilies["Roboto"], 14f, FontStyle.Bold, GraphicsUnit.Pixel);

                case fontType.Caption:
                    return new Font(RobotoFontFamilies["Roboto"], 12f, FontStyle.Regular, GraphicsUnit.Pixel);

                case fontType.Overline:
                    return new Font(RobotoFontFamilies["Roboto"], 10f, FontStyle.Regular, GraphicsUnit.Pixel);
            }
            return new Font(RobotoFontFamilies["Roboto"], 14f, FontStyle.Regular, GraphicsUnit.Pixel);
        }

        /// <summary>
        /// Get the font by size - used for textbox label animation, try to not use this for anything else
        /// </summary>
        /// <param name="size">font size, ranges from 12 up to 16</param>
        /// <returns></returns>
        public IntPtr getTextBoxFontBySize(int size)
        {
            string name = "textBox" + Math.Min(16, Math.Max(12, size)).ToString();
            return logicalFonts[name];
        }

        /// <summary>
        /// Gets a Material Skin Logical Roboto Font given a standard material font type
        /// </summary>
        /// <param name="type">material design font type</param>
        /// <returns></returns>
        public IntPtr getLogFontByType(fontType type)
        {
            return logicalFonts[Enum.GetName(typeof(fontType), type)];
        }

        // Font stuff
        private Dictionary<string, IntPtr> logicalFonts;

        private Dictionary<string, FontFamily> RobotoFontFamilies;

        private PrivateFontCollection privateFontCollection = new PrivateFontCollection();

        private void addFont(byte[] fontdata)
        {
            // Add font to system table in memory
            int dataLength = fontdata.Length;

            IntPtr ptrFont = Marshal.AllocCoTaskMem(dataLength);
            Marshal.Copy(fontdata, 0, ptrFont, dataLength);

            // GDI Font
            NativeTextRenderer.AddFontMemResourceEx(fontdata, dataLength, IntPtr.Zero, out _);

            // GDI+ Font
            privateFontCollection.AddMemoryFont(ptrFont, dataLength);
        }

        private IntPtr createLogicalFont(string fontName, int size, NativeTextRenderer.logFontWeight weight)
        {
            // Logical font:
            NativeTextRenderer.LogFont lfont = new NativeTextRenderer.LogFont();
            lfont.lfFaceName = fontName;
            lfont.lfHeight = -size;
            lfont.lfWeight = (int)weight;
            return NativeTextRenderer.CreateFontIndirect(lfont);
        }

        // Dyanmic Themes
        public void AddFormToManage(MaterialForm materialForm)
        {
            _formsToManage.Add(materialForm);
            UpdateBackgrounds();
        }

        public void RemoveFormToManage(MaterialForm materialForm)
        {
            _formsToManage.Remove(materialForm);
        }

        private void UpdateBackgrounds()
        {
            var newBackColor = BackdropColor;
            foreach (var materialForm in _formsToManage)
            {
                materialForm.BackColor = newBackColor;
                UpdateControlBackColor(materialForm, newBackColor);
            }
        }

        private void UpdateControlBackColor(Control controlToUpdate, Color newBackColor)
        {
            // No control
            if (controlToUpdate == null) return;

            // Control's Context menu
            if (controlToUpdate.ContextMenuStrip != null) UpdateToolStrip(controlToUpdate.ContextMenuStrip, newBackColor);

            // Material Tabcontrol pages
            if (controlToUpdate is TabPage)
            {
                ((TabPage)controlToUpdate).BackColor = newBackColor;
            }

            // Material Divider
            else if (controlToUpdate is MaterialDivider)
            {
                controlToUpdate.BackColor = DividersColor;
            }

            // Other Material Skin control
            else if (controlToUpdate.IsMaterialControl())
            {
                controlToUpdate.BackColor = newBackColor;
                controlToUpdate.ForeColor = TextHighEmphasisColor;
            }

            // Other Generic control not part of material skin
            else if (EnforceBackcolorOnAllComponents && controlToUpdate.HasProperty("BackColor") && !controlToUpdate.IsMaterialControl() && controlToUpdate.Parent != null)
            {
                controlToUpdate.BackColor = controlToUpdate.Parent.BackColor;
                controlToUpdate.ForeColor = TextHighEmphasisColor;
                controlToUpdate.Font = getFontByType(MaterialSkinManager.fontType.Body1);
            }

            // Recursive call to control's children
            foreach (Control control in controlToUpdate.Controls)
            {
                UpdateControlBackColor(control, newBackColor);
            }
        }

        private void UpdateToolStrip(ToolStrip toolStrip, Color newBackColor)
        {
            if (toolStrip == null)
            {
                return;
            }

            toolStrip.BackColor = newBackColor;
            foreach (ToolStripItem control in toolStrip.Items)
            {
                control.BackColor = newBackColor;
                if (control is MaterialToolStripMenuItem && (control as MaterialToolStripMenuItem).HasDropDown)
                {
                    //recursive call
                    UpdateToolStrip((control as MaterialToolStripMenuItem).DropDown, newBackColor);
                }
            }
        }
    }
}