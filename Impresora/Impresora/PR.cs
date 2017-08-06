using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Impresora
{
    public class PR
    {
        static int operador = 0;
        static int permiso = 0;
        static int superusuario = 0;
        static int kinex = 0;
        static int idusuario = 0;
        static int reset = 0;
        public static bool alarmP = true;

        public static void set_0()
        {
            permiso = 0;
        }

        public static void set_1()
        {
            permiso = 1;
        }

        public static int get_p()
        {
            return permiso;

        }

        public static void set_sp_0()
        {
            superusuario = 0;
        }

        public static void set_sp_1()
        {
            superusuario = 1;
        }

        public static int get_sp()
        {
            return superusuario;

        }

        public static void set_k_0()
        {
            kinex = 0;
        }

        public static void set_k_1()
        {
            kinex = 1;
        }

        public static int get_k()
        {
            return kinex;
        }

        public static int get_id()
        {
            return idusuario;
        }

        public static void set_id(int idu)
        {
            idusuario = idu;
        }

        public static void set_r0()
        {
            reset = 0;
        }

        public static void set_r1()
        {
            reset = 1;
        }

        public static int get_r()
        {
            return reset;

        }

        public static double getOffset(string idOffset)
        {
            conexionBD cnn = new conexionBD();
            double res;
            if (double.TryParse(cnn.selectVal("offsetcol", "offset where idoffset = " + idOffset), out res))
                return res;
            else
                return 0;

        }

        public enum FxTypes
        {
            UpperGap, Slotter, UpperDiedCut, LowerDiedCut, Feeder, Slotter53, SlotterCab
        }

        public static double ConvertTo(double x, FxTypes type)
        {
            switch (type)
            {
                case FxTypes.UpperGap:
                    return (0.432858466 * Math.Pow(x, 3)) - (9.115435799 * Math.Pow(x, 2)) + (148.684479581 * x) - 91.396306170;
                case FxTypes.Slotter:
                    return (0.0081723866482 * Math.Pow(x, 5) - 0.3231208919824 * Math.Pow(x, 4) + 5.4210848012008 * Math.Pow(x, 3) - 45.8812790352385 * Math.Pow(x, 2) + 374.6670078253370 * x + 21.2922642931807);
                case FxTypes.Slotter53:
                    return (0.0141776202054 * Math.Pow(x, 5) - 0.4950094791721 * Math.Pow(x, 4) + 6.9229913843154 * Math.Pow(x, 3) - 48.5638934004811 * Math.Pow(x, 2) + 326.3475472995670 * x - 87.5932375930130);
                case FxTypes.SlotterCab:
                    return x;
                case FxTypes.UpperDiedCut:
                    return (0.0185056776326 * Math.Pow(x, 5) - 0.5519021578330 * Math.Pow(x, 4) + 6.6141292482498 * Math.Pow(x, 3) - 39.5548152213450 * Math.Pow(x, 2) + 298.1860532774580 * x + 1.4992446758552);
                case FxTypes.LowerDiedCut:
                    return (0.00248041262832999000 * Math.Pow(x, 6) + 0.15366615274933800000 * Math.Pow(x, 5) + 2.72703668457507000000 * Math.Pow(x, 4) + 22.23253668332470000000 * Math.Pow(x, 3) + 95.38453468965600000000 * Math.Pow(x, 2) + 424.60514857067000000000 * x - 14.19394456227750000000);
                case FxTypes.Feeder:
                    return -((0.767307284988931 * Math.Pow(x, 3)) - (16.247265783027600 * Math.Pow(x, 2)) + (250.490997099449000 * x) + 26.529940022039200);
                default:
                    return 0;
            }

        }

        public static double ConvertFrom(double x, FxTypes type)
        {
            switch (type)
            {
                case FxTypes.UpperGap:
                    return ((-0.000000004 * Math.Pow(x, 3)) + (0.000007711 * Math.Pow(x, 2)) + (0.006341958 * (x)) + 0.667484705);
                case FxTypes.Slotter:
                    return (0.000000000000020 * Math.Pow(x, 4) - 0.000000000521254 * Math.Pow(x, 3) + 0.000002210336981 * Math.Pow(x, 2) + 0.002028848111614 * x - 0.008304043760290);
                case FxTypes.Slotter53:
                    return (0.000000000000006 * Math.Pow(x, 4) - 0.000000000773037 * Math.Pow(x, 3) + 0.000002827237866 * Math.Pow(x, 2) + 0.003082374389278 * x + 0.283551682927850);
                case FxTypes.SlotterCab:
                    return x;
                case FxTypes.UpperDiedCut:
                    return (-0.00000000000000008731 * Math.Pow(x, 5) + 0.00000000000055471832 * Math.Pow(x, 4) - 0.00000000175165993391 * Math.Pow(x, 3) + 0.00000298898038539330 * Math.Pow(x, 2) + 0.00304441808674483000 * x + 0.00385375889618444000);
                case FxTypes.LowerDiedCut:
                    return (0.00000000000000000026 * Math.Pow(x, 6) + 0.00000000000000138406 * Math.Pow(x, 5) + 0.00000000000258224291 * Math.Pow(x, 4) + 0.00000000143151606889 * Math.Pow(x, 3) - 0.00000159879589673589 * Math.Pow(x, 2) + 0.00219169953045796000 * x + 0.04032715572200020000);
                case FxTypes.Feeder:
                    x = x * -1;
                    return (-0.000000000908441 * Math.Pow(x, 3) + 0.000003417742710 * Math.Pow(x, 2) + 0.002763253825584 * x + 0.003050321587034);
                default:
                    return 0;
            }

        }


        internal static void set_2()
        {
            operador = 1;
        }

        internal static int get_o()
        {
            return operador;
        }

        internal static void set_o(int p)
        {
            operador = p;
        }
    }
}

