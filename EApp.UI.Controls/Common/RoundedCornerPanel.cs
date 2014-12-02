//-----------------------------------------------------------------------
// <copyright file="WorkbookPathExpressGridViewDataBehavior.cs" company="HP">
//     Copyright (c) HP. All rights reserved.
// </copyright>
// <author>cheng-jie.li@hp.com</author>
//-----------------------------------------------------------------------
using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace EApp.UI.Controls.Common
{
    /// <summary>
    /// Gradient Type
    /// </summary>
    public enum GradientType
    {
        /// <summary>
        /// Vertical style
        /// </summary>
        Vertical,

        /// <summary>
        /// Horizontal style
        /// </summary>
        Horizontal,

        /// <summary>
        /// Forward Diagonal
        /// </summary>

        ForwardDiagonal,

        /// <summary>
        /// Backward Diagonal
        /// </summary>
        BackwardDiagonal
    }

    /// <summary>
    /// the class of RoundedCornerPanel
    /// </summary>
    public partial class RoundedCornerPanel : Panel
    {
        /// <summary>
        /// panel border Color
        /// </summary>
        public Color borderColor = Color.RoyalBlue;

        /// <summary>
        /// panel border width
        /// </summary>
        public int borderWidth = 1;

        /// <summary>
        /// panel round radius
        /// </summary>
        private int arcRadius = 20;

        /// <summary>
        /// panel start Color
        /// </summary>
        private Color startColor = Color.LightSkyBlue;

        /// <summary>
        /// panel end Color
        /// </summary>
        private Color endColor = Color.LightCyan;

        /// <summary>
        /// gradient Type
        /// </summary>
        private GradientType gradientType = GradientType.Vertical;

        /// <summary>
        /// Owner draw the custom style
        /// </summary>
        private bool ownerDraw = true;

        /// <summary>
        /// Initializes a new instance of the <see cref="RoundedCornerPanel" /> class.
        /// </summary>
        public RoundedCornerPanel()
        {
            SetStyle(
            ControlStyles.UserPaint |
            ControlStyles.OptimizedDoubleBuffer |
            ControlStyles.AllPaintingInWmPaint |
            ControlStyles.ResizeRedraw |
            ControlStyles.SupportsTransparentBackColor,
            true);

            UpdateStyles();
        }

        /// <summary>
        /// Gets or sets Arc Radius
        /// </summary>
        [EditorBrowsable()]
        public int ArcRadius
        {
            get
            {
                return this.arcRadius;
            }

            set
            {
                this.arcRadius = value;
            }
        }

        /// <summary>
        /// Gets or sets Start Color
        /// </summary>
        [EditorBrowsable()]
        public Color StartColor
        {
            get
            {
                return this.startColor;
            }

            set
            {
                this.startColor = value;
            }
        }

        /// <summary>
        /// Gets or sets End Color
        /// </summary>
        [EditorBrowsable()]
        public Color EndColor
        {
            get
            {
                return this.endColor;
            }

            set
            {
                this.endColor = value;
            }
        }

        /// <summary>
        /// Gets or sets Border Color
        /// </summary>
        [EditorBrowsable()]
        public Color BorderColor
        {
            get
            {
                return this.borderColor;
            }

            set
            {
                this.borderColor = value;
            }
        }

        /// <summary>
        /// Gets or sets Border Width
        /// </summary>
        [EditorBrowsable()]
        public int BorderWidth
        {
            get
            {
                return this.borderWidth;
            }

            set
            {
                this.borderWidth = value;
            }
        }

        /// <summary>
        /// Gets or sets Gradient Type
        /// </summary>
        [EditorBrowsable()]
        public GradientType GradientType
        {
            get
            {
                return this.gradientType;
            }

            set
            {
                this.gradientType = value;
            }
        }

        [EditorBrowsable()]
        public bool OwnerDraw 
        {
            get 
            {
                return this.ownerDraw;
            }
            set
            {
                this.ownerDraw = value;
            }
        }

        /// <summary>
        /// On Paint method
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPaint(PaintEventArgs e)
        {            
            base.OnPaint(e);

            if (!this.ownerDraw)
            {
                return;
            }

            this.ApplyGradientColors(this.ApplyPanelCorners(), e);
        }

        protected override void OnScroll(ScrollEventArgs se)
        {
            base.OnScroll(se);

            if (!this.ownerDraw)
            {
                return;
            }

            this.Invalidate(true);
        }

        /// <summary>
        /// Apply Panel Corners
        /// </summary>
        /// <returns>panel Corners Path</returns>
        private GraphicsPath ApplyPanelCorners() 
        {
            int theRadiusLength = Math.Max(Math.Abs(this.arcRadius), 1);

            GraphicsPath panelCornersPath = new GraphicsPath();

            int theArcOffset = 0;

            int leftX = theArcOffset;
            int topY = theArcOffset;
            int rightX = this.Width - theArcOffset - 1;
            int bottomY = this.Height - theArcOffset - 1;

            panelCornersPath.AddArc(leftX, topY, theRadiusLength, theRadiusLength, 180, 90);  // left top rounded corner
            panelCornersPath.AddArc(rightX - theRadiusLength, topY, theRadiusLength, theRadiusLength, 270, 90); // right top rounded corner
            panelCornersPath.AddArc(rightX - theRadiusLength, bottomY - theRadiusLength, theRadiusLength, theRadiusLength, 0, 90); // right bottom rounded corner
            panelCornersPath.AddArc(leftX, bottomY - theRadiusLength, theRadiusLength, theRadiusLength, 90, 90); // left bottom rounded corner

            panelCornersPath.CloseAllFigures();

            return panelCornersPath;
        }

        /// <summary>
        /// Apply Gradient Colors
        /// </summary>
        /// <param name="theGraphicsPath">the Graphics Path</param>
        /// <param name="thePaintEventArgs">the Paint EventArgs</param>
        private void ApplyGradientColors(GraphicsPath theGraphicsPath, PaintEventArgs thePaintEventArgs)
        {
            // Prepare for applying a gradient background color.
            thePaintEventArgs.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            // Set up the gradient direction.
            LinearGradientMode myGradientDirection;

            if (this.gradientType == GradientType.Vertical)
            {
                myGradientDirection = LinearGradientMode.Vertical;
            }
            else if (this.gradientType == GradientType.Horizontal)
            {
                myGradientDirection = LinearGradientMode.Horizontal;
            }
            else if (this.gradientType == GradientType.ForwardDiagonal)
            {
                myGradientDirection = LinearGradientMode.ForwardDiagonal;
            }
            else if (this.gradientType == GradientType.BackwardDiagonal)
            {
                myGradientDirection = LinearGradientMode.BackwardDiagonal;
            }
            else
            {
                // The parameter doesn't match any of the proper values so just default to vertical.
                myGradientDirection = LinearGradientMode.Vertical;
            }

            // Define gradient background region in the panel.
            Rectangle rectBackground = new Rectangle(0, 0, this.Width - 0, this.Height - 0);

            if (this.Width != 0 && this.Height != 0)
            {
                LinearGradientBrush brushBackground = new LinearGradientBrush(rectBackground, this.startColor, this.endColor, myGradientDirection);

                /// Fill in the gradient background region.
                thePaintEventArgs.Graphics.FillPath(brushBackground, theGraphicsPath);

                ///Draw the border line around the gradient background region.
                thePaintEventArgs.Graphics.DrawPath(new Pen(this.borderColor, this.borderWidth), theGraphicsPath);
                brushBackground.Dispose();
            }

        }
    }
}