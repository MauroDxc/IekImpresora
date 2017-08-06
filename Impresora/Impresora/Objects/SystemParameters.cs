        using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Impresora.Objects
{
    class SystemParameters
    {
        private List<KeyValuePair<string, object>> mParams;
        public List<KeyValuePair<string, object>> Parameters { get { return mParams; } }

        public SystemParameters()
        {
            conexionBD cx = new conexionBD();
            DataTable dt = cx.selectFrom("id,name,value", "sysparams");
            mParams = new List<KeyValuePair<string, object>>();
            foreach (DataRow r in dt.Rows)
            {
                mParams.Add(new KeyValuePair<string, object>(r["name"].ToString(), r["value"]));
            }
        }

        public void UpdateParameter(string paramKey, object val)
        {
            mParams.Remove(mParams.Select(x => x).Where(x => x.Key == paramKey).FirstOrDefault());
            mParams.Add(new KeyValuePair<string, object>(paramKey, val));
        }
    }
}
