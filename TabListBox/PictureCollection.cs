using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Algebra;
using System.Collections.ObjectModel;

namespace TabListBox
{
    class PictureCollection
    {
        private ObservableCollection<Picture> pObjects;
        public ObservableCollection<Picture> PObjects
        {
            get { return pObjects; }
        }
        public PictureCollection(int number, int Dnum)
        {
            Basis bas = Common.OpenBasFile(number);
            this.pObjects = new ObservableCollection<Picture>();
            int num = bas.SaveTabGroups[Dnum].TabGroupNumerations.Count;
            this.pObjects = new ObservableCollection<Picture>();
            for (int i = 0; i < num; i++)
            {
                this.pObjects.Add(new Picture(new Uri("Pics\\"+number.ToString() + "_" + "Tab" + Dnum.ToString() 
                      + "_" + i.ToString() + ".bmp", UriKind.RelativeOrAbsolute)));
            }
        }
        public PictureCollection(int number)
        {
            Basis bas = Common.OpenBasFile(number);
            int num = bas.SaveTabGroups.Count;
            this.pObjects = new ObservableCollection<Picture>();
            for (int i = 0; i < num; i++)
            {
                this.pObjects.Add(new Picture(new Uri("Pics\\" + number.ToString() + "_" + "Diag" + i.ToString() + ".bmp", UriKind.RelativeOrAbsolute)));
            }
        }
    }
}
