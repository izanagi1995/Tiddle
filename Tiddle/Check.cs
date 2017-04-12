using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tiddle.Definition
{
    class Check
    {

        public string Operator { get {
                return _operator;
            }
        }

        public string Prop
        {
            get
            {
                return _prop;
            }
        }

        public string Value
        {
            get
            {
                return _val;
            }
        }

        public Step Then
        {
            get
            {
                return _then;
            }
        }

        public Step Else
        {
            get
            {
                return _else;
            }
        }

        private string _operator;
        private string _prop;
        private string _val;
        private Step _then;
        private Step _else;
        public Check(string op, string prop, string val, string then, string els) {
            this._operator = op;
            this._prop = prop;
            this._val = val;
            this._then = null;
            this._else = null;
        }
    }
}
