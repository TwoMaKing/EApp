using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EApp.UI.Controls.GridView
{
    public class ValueImage
    {
        protected int? imageIndex;

        private string resourceManagerName = string.Empty;

        protected string imageName = string.Empty;

        public ValueImage() { }

        public ValueImage(int? imageIndex)
        {
            this.imageIndex = imageIndex;
        }

        public ValueImage(string imageName)
        {
            this.imageName = imageName;
        }

        public ValueImage(string resourceManagerName, string imageName)
        {
            this.resourceManagerName = resourceManagerName;
            this.imageName = imageName;
        }

        public int? ImageIndex
        {
            get
            {
                return this.imageIndex;
            }
        }

        public string ResourceManagerName
        {
            get
            {
                return this.resourceManagerName;
            }
        }

        public string ImageName
        {
            get
            {
                return this.imageName;
            }
        }
    }

    public class ValueTextImage : ValueImage
    {
        private string text = string.Empty;

        public ValueTextImage(string text, string resourceManagerName, string imageName)
            : base(resourceManagerName, imageName)
        {
            this.text = text;
        }

        public ValueTextImage(string text, string imageName)
        {
            this.text = text;
            this.imageName = imageName;
        }

        public ValueTextImage(string text, int? imageIndex)
        {
            this.text = text;
            this.imageIndex = imageIndex;
        }

        public string Text
        {
            get
            {
                return text;
            }
            set
            {
                text = value;
            }
        }

        private DataGridViewTextImageCellTextPosition textPosition = DataGridViewTextImageCellTextPosition.left;
        public DataGridViewTextImageCellTextPosition TextPosition
        {
            get
            {
                return this.textPosition;
            }
            set
            {
                this.textPosition = value;
            }
        }
    }

}
