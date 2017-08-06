using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Impresora
{
    class AppModule
    {
        private static AppModule mMe = new AppModule();

        public static AppModule Instance { get { return mMe; } }
        public object Object { get; set; }
        public Objects.SystemParameters SysParams { get; set; }
        public List<KeyValuePair<int, bool>> DrivesStatus { get; set; }

        private AppModule()
        {
            SysParams = new Objects.SystemParameters();
            DrivesStatus = new List<KeyValuePair<int, bool>>();
            conexionBD cx = new conexionBD();

            for (int i = 10; i < 154; i++)
            {
                switch (i)
                {
                    case 17:
                        i = 20;
                        break;
                    case 26:
                        i = 30;
                        break;
                    case 36:
                        i = 40;
                        break;
                    case 43:
                        i = 50;
                        break;
                    case 55:
                        i = 120;
                        break;
                    case 126:
                        i = 130;
                        break;
                    case 136:
                        i = 150;
                        break;
                    default:
                        break;
                }
                int v = 0;
                int.TryParse(cx.selectVal("val", "en_drive where idx=" + i), out v);
                DrivesStatus.Add(new KeyValuePair<int, bool>(i, (v == 1 ? true : false)));
            }
        }
    }
}
