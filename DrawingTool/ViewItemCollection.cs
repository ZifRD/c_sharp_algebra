using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DrawingTool
{
    public class ViewItemCollection : List<ViewItem>
    {
        private Controller Master;
        internal int DimX;  
        internal int DimY;

        public ViewItemCollection(Controller m, int x, int y)
        {
            Master = m;
            DimX = x;
            DimY = y;
        }

        public ViewItem GetViewItemAtXY(int x, int y)
        {
            return this.Find(vi => (vi.Row == x && vi.Column == y));
        }

        public void SetViewItemAtXY(ViewItem item)
        {
            int x = item.Row;
            int y = item.Column;
            try
            {
                this.Remove(this.Find(vi => (vi.Row == x && vi.Column == y)));
            }
            catch (Exception e)
            {
            }
            finally
            {
                this.Add(item);
            }
        }

    }
}
