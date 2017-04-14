using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tiddle.Definition;

namespace Tiddle
{
    class Player
    {
        public int Goodness { get; set; }
        public int Badness { get; set; }
        public DefaultSave Save { get; set; }
        public string Name { get; set; }

        public double Days { get; set; }

        public Player(string name):this(name, 0, 0) { }

        public Player(string name, int goodness, int badness)
        {
            this.Name = name;
            this.Goodness = goodness;
            this.Badness = badness;
            this.Days = 0;
        }

    }
}
