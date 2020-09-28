using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TabListBox
{
    class Picture
    {
        private Uri image;

        public Uri CellImage
        {
            get { return image; }
            set { image = value; }
        }
        public Picture(Uri im)
        {
            image = im;
        }
    }
}
