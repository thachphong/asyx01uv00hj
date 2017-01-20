using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Data;


namespace QLNhiemVu
{
    public static class UF_Function
    {
        public static DataTable ToDataTable<T>(this IList<T> list)
        {
            //if (list == null || list.Count == 0) return null;
            DataTable table = new DataTable();

            PropertyDescriptorCollection props = TypeDescriptor.GetProperties(typeof(T));            
            for (int i = 0; i < props.Count; i++)
            {
                PropertyDescriptor prop = props[i];
                table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
            }
            object[] values = new object[props.Count];
            if (list != null)
            {
                foreach (T item in list)
                {
                    for (int i = 0; i < values.Length; i++)
                        values[i] = props[i].GetValue(item) ?? DBNull.Value;
                    table.Rows.Add(values);
                }
            }
            return table;
        }
    }
}
