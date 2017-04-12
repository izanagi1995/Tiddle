using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tiddle.Definition
{
    class GoBack:IPossibility
    {
        private string _id;
        public string Id
        {
            get
            {
                return this._id;
            }
        }

        public GoBack(string id)
        {
            this._id = id;
        }
    }
}
