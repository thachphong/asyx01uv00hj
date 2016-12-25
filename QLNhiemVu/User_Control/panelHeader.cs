using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLNhiemVu
{
    public class panelHeader:System.Windows.Forms.Panel
    {
        public panelHeader() 
        {
            
        }

        /// <summary>
        /// Căn giữa(X) groupbox Header với Y input (nếu không gán giá trị cho Y thì sử dụng giá trị 0)
        /// </summary>
        /// <param name="ctrl"></param>
        /// <param name="Y"></param>
        public void alignCenter(Control ctrl, params int[] Y)
        {
            this.Location = new Point((ctrl.Width - this.Width) / 2, Y.Length > 0 ? Y[0] : 0);
        }
    }
}
