using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Impresora.Objects
{
    public delegate void ChangedEventHandler(object sender, ObjectEventArgs e);
    public class Text
    {
        public event ChangedEventHandler Changed;

        private double mValue;

        public Text()
        {
            AssociatedControls = new List<Control>();
            FormControl = new List<KeyValuePair<string, string>>();
        }
        
        public double Value
        {
            get
            {
                return mValue;
            }
            set
            {
                mValue = value;
                ObjectEventArgs e = new ObjectEventArgs();
                e.Text = value.ToString();
                OnChanged(e);
            }
        }
        public List<Control> AssociatedControls{ get; set; }
        public string Tag { get; set; }
        //public string Value { get; set; }
        public int Handle { get; set; }
        public bool isDWord { get; set; }
        public double Offset { get; set; }
        public int Stat { get; set; }
        public List<KeyValuePair<string,string>> FormControl { get; set; }
        public bool DisableWrite { get; set; }
        public int LimitOf { get; set; }
        public int OffsetId { get; set; }

        protected virtual void OnChanged(ObjectEventArgs e)
        {
            if (Changed != null)
            {
                Changed(this, e);
            }
        }

        public void ForceChange()
        {
            OnChanged(null);
        }



    }
}
