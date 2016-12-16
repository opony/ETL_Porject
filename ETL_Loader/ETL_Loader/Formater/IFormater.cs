using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETL_Loader.Formater
{
    interface IFormater
    {
        string ConvertToString(DataTable tb, string dataTimeFormat = "yyyy-mm-dd HH:mi:ss");

        DataTable ConvertToDataTable(string str);

    }
}
