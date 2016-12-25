using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLNhiemVu
{
    public class lblHeadTitle : System.Windows.Forms.Label
    {
        public lblHeadTitle() 
        {
            this.Font = new System.Drawing.Font("Times New Roman", 15.75f, FontStyle.Bold);
            this.ForeColor = Color.FromArgb(0, 0, 255);
            this.Text = this.Text.ToUpper();
        }

        public void setText(string text)
        {
            this.Text = text.ToUpper();
        }

        /// <summary>
        /// Căn giữa(X) lblHeader title và Y=19 với parent control
        /// </summary>
        /// <param name="ctrl"></param>
        public void defaultAlign_XCenter_And_Y19(Control ctrl)
        {
            this.Location = new Point((ctrl.Width - this.Width) / 2, 19);
        }

        /// <summary>
        /// Căn giữa(X) lblHeader title với Y input
        /// </summary>
        /// <param name="ctrl"></param>
        /// <param name="Y"></param>
        public void defaultAlign_X_CenterAnd_Y_Input(Control ctrl, int Y)
        {
            this.Location = new Point((ctrl.Width - this.Width) / 2, Y);
        }

        /// <summary>
        /// Căn giữa(X) lblHeader title với Y input (nếu không gán giá trị cho Y thì sử dụng giá trị mặc định)
        /// </summary>
        /// <param name="ctrl"></param>
        /// <param name="Y"></param>
        public void alignCenter(Control ctrl, params int[] Y)
        {
            this.Location = new Point((ctrl.Width - this.Width) / 2, Y.Length > 0 ? Y[0] : this.Location.Y);
        }
    }
}
