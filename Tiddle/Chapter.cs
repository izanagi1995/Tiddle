using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tiddle.Definition
{
    class Chapter
    {

        private int _id;
        private string _name;
        public Step Step;

        public string Name { get
            {
                return this._name;
            }
        }

        public int Id
        {
            get
            {
                return this._id;
            }
        }

        public Chapter(int id, string name) {
            this._id = id;
            this._name = name;
        }

        
    }
}
