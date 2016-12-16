using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETL_Interface.Interface
{
    public interface ISource
    {
        DataTable GetData();
    }
}
