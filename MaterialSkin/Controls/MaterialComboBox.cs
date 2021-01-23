namespace MaterialSkin.Controls
{
    using MaterialSkin.Animations;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Linq;
    using System.Windows.Forms;
    using System.Runtime.InteropServices;

    public class MaterialComboBox : ComboBox, IMaterialControl
    {
        // For some reason, even when overriding the AutoSize property, it doesn't appear on the properties panel, so we have to create a new one.
        [Browsable(true), EditorBrowsable(EditorBrowsableState.Always), Category("Layout")]
        private bool _AutoResize;

        public bool AutoResize
        {
            get { return _AutoResize; }
            set
            {
                _AutoResize = value;
                recalculateAutoSize();
            }
        }

        //Properties for managing the material design properties
        [Browsable(false)]
        public int Depth { get; set; }

        [Browsable(false)]
        public MaterialSkinManager SkinManager => MaterialSkinManager.Instance;

        [Browsable(false)]
        public MouseState MouseState { get; set; }

        private bool _UseTallSize;

        [Category("Material Skin"), DefaultValue(true), Description("Using a larger size enables the hint to always be visible")]
        public bool UseTallSize
        {
            get { return _UseTallSize; }
            set
            {
                _UseTallSize = value;
                setHeightVars();
                Invalidate();
            }
        }

        [Category("Material Skin"), DefaultValue(true)]
        public bool UseAccent { get; set; }

        private string _hint = string.Empty;

        [Category("Material Skin"), DefaultValue(""), Localizable(true)]
        public string Hint
        {
            get { return _hint; }
            set
            {
                _hint = value;
                hasHint = !String.IsNullOrEmpty(Hint);
                Invalidate();
            }
        }

        private const int TEXT_SMALL_SIZE = 18;
        private const int TEXT_SMALL_Y = 4;
        private const int BOTTOM_PADDING = 3;
        private int HEIGHT = 50;
        private int LINE_Y;

        private bool hasHint;

        private readonly AnimationManager _animationManager;

        public MaterialComboBox()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw, true);

            // Material Properties
            Hint = "";
            UseAccent = true;
            UseTallSize = true;
            MaxDropDownItems = 4;

            Font = SkinManager.getFontByType(MaterialSkinManager.fontType.Subtitle2);
            BackColor = SkinManager.BackgroundColor;
            ForeColor = SkinManager.TextHighEmphasisColor;
            DrawMode = DrawMode.OwnerDrawVariable;
            DropDownStyle = ComboBoxStyle.DropDownList;
            DropDownWidth = Width;

            // Animations
            _animationManager = new AnimationManager(true)
            {
                Increment = 0.08,
                AnimationType = AnimationType.EaseInOut
            };
            _animationManager.OnAnimationProgress += sender => Invalidate();

            DropDownClosed += (sender, args) =>
            {
                MouseState = MouseState.OUT;
                if (SelectedIndex < 0 && !Focused) _animationManager.StartNewAnimation(AnimationDirection.Out);
            };
            LostFocus += (sender, args) =>
            {
                MouseState = MouseState.OUT;
                if (SelectedIndex < 0) _animationManager.StartNewAnimation(AnimationDirection.Out);
            };
            DropDown += (sender, args) =>
            {
                _animationManager.StartNewAnimation(AnimationDirection.In);
            };
            GotFocus += (sender, args) =>
            {
                _animationManager.StartNewAnimation(AnimationDirection.In);
            };
            MouseEnter += (sender, args) =>
            {
                MouseState = MouseState.HOVER;
                Invalidate();
            };
            MouseLeave += (sender, args) =>
            {
                MouseState = MouseState.OUT;
                Invalidate();
            };

            // Timer to check when the dropdown is fully visible
            _dropDownCheck.Interval = 100;
            _dropDownCheck.Tick += new EventHandler(dropDownCheck_Tick);

        }

        private Timer _dropDownCheck = new Timer();      // Timer that checks when the drop down is fully visible

        public const int WM_CTLCOLORLISTBOX = 0x0134;

        /// <summary>
        /// Override window messages
        /// </summary>
        protected override void WndProc(ref Message m)
        {
            // Filter window messages
            switch (m.Msg)
            {
                // Draw a custom color border around the drop down list cintaining popup
                case WM_CTLCOLORLISTBOX:
                    base.WndProc(ref m);
                    DrawNativeBorder(m.LParam);
                    break;

                default: base.WndProc(ref m); break;
            }
        }

        public enum PenStyles
        {
            PS_SOLID = 0,
            PS_DASH = 1,
            PS_DOT = 2,
            PS_DASHDOT = 3,
            PS_DASHDOTDOT = 4
        }

        public enum ComboBoxButtonState
        {
            STATE_SYSTEM_NONE = 0,
            STATE_SYSTEM_INVISIBLE = 0x00008000,
            STATE_SYSTEM_PRESSED = 0x00000008
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct COMBOBOXINFO
        {
            public Int32 cbSize;
            public RECT rcItem;
            public RECT rcButton;
            public ComboBoxButtonState buttonState;
            public IntPtr hwndCombo;
            public IntPtr hwndEdit;
            public IntPtr hwndList;
        }

        [DllImport("user32.dll")]
        public static extern bool GetWindowRect(IntPtr hWnd, out RECT lpRect);

        [DllImport("user32.dll")]
        public static extern IntPtr GetWindowDC(IntPtr hWnd);

        [DllImport("user32.dll")]
        public static extern int ReleaseDC(IntPtr hWnd, IntPtr hDC);

        [DllImport("user32.dll")]
        public static extern IntPtr SetFocus(IntPtr hWnd);

        [DllImport("user32.dll")]
        public static extern bool GetComboBoxInfo(IntPtr hWnd, ref COMBOBOXINFO pcbi);

        [DllImport("gdi32.dll")]
        public static extern int ExcludeClipRect(IntPtr hdc, int nLeftRect, int nTopRect, int nRightRect, int nBottomRect);

        [DllImport("gdi32.dll")]
        public static extern IntPtr CreatePen(PenStyles enPenStyle, int nWidth, int crColor);

        [DllImport("gdi32.dll")]
        public static extern IntPtr SelectObject(IntPtr hdc, IntPtr hObject);

        [DllImport("gdi32.dll")]
        public static extern bool DeleteObject(IntPtr hObject);

        [DllImport("gdi32.dll")]
        public static extern void Rectangle(IntPtr hdc, int X1, int Y1, int X2, int Y2);

        public static int RGB(int R, int G, int B)
        {
            return (R | (G << 8) | (B << 16));
        }


        [Serializable, StructLayout(LayoutKind.Sequential)]
        public struct RECT
        {
            public int Left;
            public int Top;
            public int Right;
            public int Bottom;

            public RECT(int left_, int top_, int right_, int bottom_)
            {
                Left = left_;
                Top = top_;
                Right = right_;
                Bottom = bottom_;
            }

            public override bool Equals(object obj)
            {
                if (obj == null || !(obj is RECT))
                {
                    return false;
                }
                return this.Equals((RECT)obj);
            }

            public bool Equals(RECT value)
            {
                return this.Left == value.Left &&
                       this.Top == value.Top &&
                       this.Right == value.Right &&
                       this.Bottom == value.Bottom;
            }

            public int Height
            {
                get
                {
                    return Bottom - Top + 1;
                }
            }

            public int Width
            {
                get
                {
                    return Right - Left + 1;
                }
            }

            public Size Size { get { return new Size(Width, Height); } }

            public Point Location { get { return new Point(Left, Top); } }

            // Handy method for converting to a System.Drawing.Rectangle
            public System.Drawing.Rectangle ToRectangle()
            {
                return System.Drawing.Rectangle.FromLTRB(Left, Top, Right, Bottom);
            }

            public static RECT FromRectangle(Rectangle rectangle)
            {
                return new RECT(rectangle.Left, rectangle.Top, rectangle.Right, rectangle.Bottom);
            }

            public void Inflate(int width, int height)
            {
                this.Left -= width;
                this.Top -= height;
                this.Right += width;
                this.Bottom += height;
            }

            public override int GetHashCode()
            {
                return Left ^ ((Top << 13) | (Top >> 0x13))
                    ^ ((Width << 0x1a) | (Width >> 6))
                    ^ ((Height << 7) | (Height >> 0x19));
            }

            public static implicit operator Rectangle(RECT rect)
            {
                return System.Drawing.Rectangle.FromLTRB(rect.Left, rect.Top, rect.Right, rect.Bottom);
            }

            public static implicit operator RECT(Rectangle rect)
            {
                return new RECT(rect.Left, rect.Top, rect.Right, rect.Bottom);
            }
        }


        /// <summary>
        /// Non client area border drawing
        /// </summary>
        /// <param name="m">The window message to process</param>
        /// <param name="handle">The handle to the control</param>
        public void DrawNativeBorder(IntPtr handle)
        {
            // Define the windows frame rectangle of the control
            RECT controlRect;
            GetWindowRect(handle, out controlRect);
            controlRect.Right -= controlRect.Left; controlRect.Bottom -= controlRect.Top;
            controlRect.Top = controlRect.Left = 0;

            // Get the device context of the control
            IntPtr dc = GetWindowDC(handle);

            // Define the client area inside the control rect
            RECT clientRect = controlRect;
            clientRect.Left += 1;
            clientRect.Top += 1;
            clientRect.Right -= 1;
            clientRect.Bottom -= 1;
            ExcludeClipRect(dc, clientRect.Left, clientRect.Top, clientRect.Right, clientRect.Bottom);

            // Create a pen and select it
            Color borderColor = UseAccent==true ? SkinManager.ColorScheme.AccentColor  : SkinManager.ColorScheme.PrimaryColor;
            IntPtr border = CreatePen(PenStyles.PS_SOLID, 1, RGB(borderColor.R, borderColor.G, borderColor.B));

            // Draw the border rectangle
            IntPtr borderPen = SelectObject(dc, border);
            Rectangle(dc, controlRect.Left, controlRect.Top, controlRect.Right, controlRect.Bottom);
            SelectObject(dc, borderPen);
            DeleteObject(border);

            // Release the device context
            ReleaseDC(handle, dc);
            SetFocus(handle);
        }

        /// <summary>
        /// On drop down
        /// </summary>
        protected override void OnDropDown(EventArgs e)
        {
            base.OnDropDown(e);

            // Start checking for the dropdown visibility
            _dropDownCheck.Start();
        }

        /// <summary>
        /// Checks when the drop down is fully visible
        /// </summary>
        private void dropDownCheck_Tick(object sender, EventArgs e)
        {
            // If the drop down has been fully dropped
            if (DroppedDown)
            {
                // Stop the time, send a listbox update
                _dropDownCheck.Stop();
                Message m = GetControlListBoxMessage(this.Handle);
                WndProc(ref m);
            }
        }

        /// <summary>
        /// Creates a default WM_CTLCOLORLISTBOX message
        /// </summary>
        /// <param name="handle">The drop down handle</param>
        /// <returns>A WM_CTLCOLORLISTBOX message</returns>
        public Message GetControlListBoxMessage(IntPtr handle)
        {
            // Force non-client redraw for focus border
            Message m = new Message();
            m.HWnd = handle;
            m.LParam = GetListHandle(handle);
            m.WParam = IntPtr.Zero;
            m.Msg = WM_CTLCOLORLISTBOX;
            m.Result = IntPtr.Zero;
            return m;
        }

        /// <summary>
        /// Gets the list control of a combo box
        /// </summary>
        /// <param name="handle">Handle of the combo box itself</param>
        /// <returns>A handle to the list</returns>
        public static IntPtr GetListHandle(IntPtr handle)
        {
            COMBOBOXINFO info;
            info = new COMBOBOXINFO();
            info.cbSize = System.Runtime.InteropServices.Marshal.SizeOf(info);
            return GetComboBoxInfo(handle, ref info) ? info.hwndList : IntPtr.Zero;
        }


        protected override void OnPaint(PaintEventArgs pevent)
        {
            Graphics g = pevent.Graphics;

            g.Clear(Parent.BackColor);
            g.FillRectangle(Enabled ? Focused ?
                SkinManager.BackgroundFocusBrush : // Focused
                MouseState == MouseState.HOVER ?
                SkinManager.BackgroundHoverBrush : // Hover
                SkinManager.BackgroundAlternativeBrush : // normal
                SkinManager.BackgroundDisabledBrush // Disabled
                , ClientRectangle.X, ClientRectangle.Y, ClientRectangle.Width, LINE_Y);

            // Create and Draw the arrow
            System.Drawing.Drawing2D.GraphicsPath pth = new System.Drawing.Drawing2D.GraphicsPath();
            PointF TopRight = new PointF(this.Width - 0.5f - SkinManager.FORM_PADDING, (this.Height >> 1) - 2.5f);
            PointF MidBottom = new PointF(this.Width - 4.5f - SkinManager.FORM_PADDING, (this.Height >> 1) + 2.5f);
            PointF TopLeft = new PointF(this.Width - 8.5f - SkinManager.FORM_PADDING, (this.Height >> 1) - 2.5f);
            pth.AddLine(TopLeft, TopRight);
            pth.AddLine(TopRight, MidBottom);

            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            g.FillPath((SolidBrush)(DroppedDown || Focused ? UseAccent ? SkinManager.ColorScheme.AccentBrush : SkinManager.ColorScheme.PrimaryBrush : SkinManager.TextHighEmphasisBrush), pth);
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.None;

            // HintText
            bool userTextPresent = SelectedIndex >= 0;
            Rectangle hintRect = new Rectangle(SkinManager.FORM_PADDING, ClientRectangle.Y, Width, LINE_Y);
            int hintTextSize = 16;

            // bottom line base
            g.FillRectangle(SkinManager.DividersAlternativeBrush, 0, LINE_Y, Width, 1);

            if (!_animationManager.IsAnimating())
            {
                // No animation
                if (hasHint && UseTallSize && (DroppedDown || Focused || SelectedIndex >= 0))
                {
                    // hint text
                    hintRect = new Rectangle(SkinManager.FORM_PADDING, TEXT_SMALL_Y, Width, TEXT_SMALL_SIZE);
                    hintTextSize = 12;
                }

                // bottom line
                if (DroppedDown || Focused)
                {
                    g.FillRectangle(UseAccent ? SkinManager.ColorScheme.AccentBrush : SkinManager.ColorScheme.PrimaryBrush, 0, LINE_Y, Width, 2);
                }
            }
            else
            {
                // Animate - Focus got/lost
                double animationProgress = _animationManager.GetProgress();

                // hint Animation
                if (hasHint && UseTallSize)
                {
                    hintRect = new Rectangle(
                        SkinManager.FORM_PADDING,
                        userTextPresent && !_animationManager.IsAnimating() ? (TEXT_SMALL_Y) : ClientRectangle.Y + (int)((TEXT_SMALL_Y - ClientRectangle.Y) * animationProgress),
                        Width,
                        userTextPresent && !_animationManager.IsAnimating() ? (TEXT_SMALL_SIZE) : (int)(LINE_Y + (TEXT_SMALL_SIZE - LINE_Y) * animationProgress));
                    hintTextSize = userTextPresent && !_animationManager.IsAnimating() ? 12 : (int)(16 + (12 - 16) * animationProgress);
                }

                // Line Animation
                int LineAnimationWidth = (int)(Width * animationProgress);
                int LineAnimationX = (Width / 2) - (LineAnimationWidth / 2);
                g.FillRectangle(UseAccent ? SkinManager.ColorScheme.AccentBrush : SkinManager.ColorScheme.PrimaryBrush, LineAnimationX, LINE_Y, LineAnimationWidth, 2);
            }

            // Calc text Rect
            Rectangle textRect = new Rectangle(
                SkinManager.FORM_PADDING,
                hasHint && UseTallSize ? (hintRect.Y + hintRect.Height) - 2 : ClientRectangle.Y,
                ClientRectangle.Width - SkinManager.FORM_PADDING * 3 - 8,
                hasHint && UseTallSize ? LINE_Y - (hintRect.Y + hintRect.Height) : LINE_Y);

            g.Clip = new Region(textRect);

            using (NativeTextRenderer NativeText = new NativeTextRenderer(g))
            {
                // Draw user text
                NativeText.DrawTransparentText(
                    Text,
                    SkinManager.getLogFontByType(MaterialSkinManager.fontType.Subtitle1),
                    Enabled ? SkinManager.ComboBoxItemHighEmphasisColor : SkinManager.TextDisabledOrHintColor,
                    textRect.Location,
                    textRect.Size,
                    NativeTextRenderer.TextAlignFlags.Left | NativeTextRenderer.TextAlignFlags.Middle);
            }

            g.ResetClip();

            // Draw hint text
            if (hasHint && (UseTallSize || String.IsNullOrEmpty(Text)))
            {
                using (NativeTextRenderer NativeText = new NativeTextRenderer(g))
                {
                    NativeText.DrawTransparentText(
                    Hint,
                    SkinManager.getTextBoxFontBySize(hintTextSize),
                    Enabled ? DroppedDown || Focused ? UseAccent ?
                    SkinManager.ColorScheme.AccentColor : // Focus Accent
                    SkinManager.ColorScheme.PrimaryColor : // Focus Primary
                    SkinManager.TextMediumEmphasisColor : // not focused
                    SkinManager.TextDisabledOrHintColor, // Disabled
                    hintRect.Location,
                    hintRect.Size,
                    NativeTextRenderer.TextAlignFlags.Left | NativeTextRenderer.TextAlignFlags.Middle);
                }
            }
        }

        private void CustomMeasureItem(object sender, System.Windows.Forms.MeasureItemEventArgs e)
        {
            e.ItemHeight = HEIGHT - 7;
        }

        private void CustomDrawItem(object sender, System.Windows.Forms.DrawItemEventArgs e)
        {
            if (e.Index < 0 || e.Index > Items.Count || !Focused) return;

            Graphics g = e.Graphics;

            // Draw the background of the item.
            g.FillRectangle(SkinManager.BackgroundBrush, e.Bounds);

            // Hover
            if (e.State.HasFlag(DrawItemState.Focus)) // Focus == hover
            {
                g.FillRectangle(SkinManager.BackgroundHoverBrush, e.Bounds);
            }
            
            string Text = "";
            if (!string.IsNullOrWhiteSpace(DisplayMember))
            {
                Text = Items[e.Index].GetType().GetProperty(DisplayMember).GetValue(Items[e.Index], null).ToString();
            }
            else
            {
                Text = Items[e.Index].ToString();
            }

            using (NativeTextRenderer NativeText = new NativeTextRenderer(g))
            {
                NativeText.DrawTransparentText(
                Text,
                SkinManager.getFontByType(MaterialSkinManager.fontType.Subtitle1),
                SkinManager.ComboBoxItemHighEmphasisColor,
                new Point(e.Bounds.Location.X + SkinManager.FORM_PADDING, e.Bounds.Location.Y),
                new Size(e.Bounds.Size.Width - SkinManager.FORM_PADDING * 2, e.Bounds.Size.Height),
                NativeTextRenderer.TextAlignFlags.Left | NativeTextRenderer.TextAlignFlags.Middle); ;
            }
        }

        protected override void OnCreateControl()
        {
            base.OnCreateControl();
            MouseState = MouseState.OUT;
            MeasureItem += CustomMeasureItem;
            DrawItem += CustomDrawItem;
            DropDownStyle = ComboBoxStyle.DropDownList;
            DrawMode = DrawMode.OwnerDrawVariable;
            recalculateAutoSize();
            setHeightVars();
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            recalculateAutoSize();
            setHeightVars();
        }

        private void setHeightVars()
        {
            HEIGHT = UseTallSize ? 50 : 36;
            Size = new Size(Size.Width, HEIGHT);
            LINE_Y = HEIGHT - BOTTOM_PADDING;
            ItemHeight = HEIGHT - 7;
            DropDownHeight = ItemHeight * MaxDropDownItems + 2;
        }

        public void recalculateAutoSize()
        {
            if (!AutoResize) return;

            int w = DropDownWidth;
            int padding = SkinManager.FORM_PADDING * 3;
            int vertScrollBarWidth = (Items.Count > MaxDropDownItems) ? SystemInformation.VerticalScrollBarWidth : 0;

            Graphics g = CreateGraphics();
            using (NativeTextRenderer NativeText = new NativeTextRenderer(g))
            {
                var itemsList = this.Items.Cast<object>().Select(item => item.ToString());
                foreach (string s in itemsList)
                {
                    int newWidth = NativeText.MeasureLogString(s, SkinManager.getLogFontByType(MaterialSkinManager.fontType.Subtitle1)).Width + vertScrollBarWidth + padding;
                    if (w < newWidth) w = newWidth;
                }
            }

            if (Width != w)
            {
                DropDownWidth = w;
                Width = w;
            }
        }
    }
}
