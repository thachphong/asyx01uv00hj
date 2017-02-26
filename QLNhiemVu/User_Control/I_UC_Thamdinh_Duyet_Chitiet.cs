using QLNhiemvu_DBEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLNhiemVu.User_Control
{
    public interface I_UC_Thamdinh_Duyet_Chitiet
    {
        void AssignData(TD_Thamdinh_Duyet objData, bool replaceCurrent = true);

        void RefreshUI();

        void FillData(ref TD_Thamdinh_Duyet objData);
    }
}
