using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Tiddle.Definition
{
    class Step:IPossibility
    {
        public double Days;
        public int Goodness;
        public int Badness;
        public string Result;
        public string Label;
        private string _id;
        public SaveObject Object;
        

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

        public void AddPossibility(IPossibility p)
        {
            this.Possibilities.Add(p);
        }

        internal void Parse(XmlNode stepNode)
        {
            this.Badness = int.Parse(stepNode.SelectSingleNode("Badness").InnerText);
            this.Goodness = int.Parse(stepNode.SelectSingleNode("Goodness").InnerText);
            this.Days = double.Parse(stepNode.SelectSingleNode("Days").InnerText);
            this.Result = stepNode.SelectSingleNode("Result").InnerText;
            this.Label = stepNode.SelectSingleNode("Label").InnerText;
            XmlNode soNode = stepNode.SelectSingleNode("SaveObject");
            if(soNode != null)
            {
                this.Object = new SaveObject(soNode.Attributes["id"].Value);
                this.Object.Parse(soNode);
            }
            else
            {
                this.Object = null;
            }
            if (StepHasPossibilities(stepNode))
            {
                XmlNode possibilites = stepNode.SelectSingleNode("Possibilities");
                foreach (XmlNode poss in possibilites.ChildNodes)
                {
                    switch (poss.Name)
                    {
                        case "GoBack":

                            GoBack gb = new GoBack(poss.Attributes["id"].InnerText);
                            this.AddPossibility(gb);

                            break;
                        case "Step":
                            Step s = new Step(poss.Attributes["id"].InnerText);
                            s.Badness = int.Parse(poss.SelectSingleNode("Badness").InnerText);
                            s.Goodness = int.Parse(poss.SelectSingleNode("Goodness").InnerText);
                            s.Days = double.Parse(poss.SelectSingleNode("Days").InnerText);
                            s.Result = poss.SelectSingleNode("Result").InnerText;
                            s.Label = poss.SelectSingleNode("Label").InnerText;
                            soNode = stepNode.SelectSingleNode("Object");
                            if (soNode != null)
                            {
                                this.Object = new SaveObject(soNode.Attributes["id"].Value);
                                this.Object.Parse(soNode);
                            }
                            else
                            {
                                this.Object = null;
                            }
                            if (StepHasPossibilities(poss))
                            {
                                s.Parse(poss);
                            }
                            this.AddPossibility(s);
                            break;
                        case "Check":

                            Check c = new Check(poss.Attributes["operator"].InnerText, poss.Attributes["type"].InnerText, poss.Attributes["prop"].InnerText, poss.Attributes["val"].InnerText, poss.Attributes["then"].InnerText, poss.Attributes["else"].InnerText);
                            this.AddPossibility(c);

                            break;
                        default:
                            throw new NotImplementedException();
                    }
                }
            }
        }

        static bool StepHasPossibilities(XmlNode step)
        {
            return step.SelectSingleNode("Possibilities").ChildNodes.Count > 0;
        }
        

    }
}
