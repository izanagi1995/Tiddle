using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tiddle.Definition
{
    class Check:IPossibility
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

        public string Then
        {
            get
            {
                return _then;
            }
        }

        public string Else
        {
            get
            {
                return _else;
            }
        }

        public string Type
        {
            get
            {
                return _type;
            }
        }

        private string _operator;
        private string _prop;
        private string _val;
        private string _then;
        private string _else;
        private string _type;

        public Check(string op, string type, string prop, string val, string then, string els) {
            this._type = type;
            this._operator = op;
            this._prop = prop;
            this._val = val;
            this._then = then;
            this._else = els;
        }

        internal string Resolve(dynamic value)
        {
            switch (this.Type)
            {
                
                case "int":
                    if (!(value is int))
                        throw new InvalidOperationException("value is not int");
                    int i = (int)value;
                    if(i > int.Parse(this.Value))
                        return this.Then;
                    else
                        return this.Else;
                case "bool":
                    bool b = bool.Parse(value);
                    if (b == bool.Parse(this.Value))
                        return this.Then;
                    else
                        return this.Else;
                default:
                    throw new NotImplementedException();
            }
        }
    }
}
