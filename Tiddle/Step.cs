using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tiddle.Definition
{
    class Step:IPossibility
    {
        public int Days;
        public int Goodness;
        public int Badness;
        private string _id;

        public string Id
        {
            get
            {
                return this._id;
            }
        }

        public List<IPossibility> Possibilities { get; private set; }

        public Step(string id)
        {
            this._id = id;
            this.Possibilities = new List<IPossibility>();
        }

        public void addPossibility(IPossibility p)
        {
            this.Possibilities.Add(p);
        }
    }
}
