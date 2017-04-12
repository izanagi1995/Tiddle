using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Tiddle.Definition;

namespace Tiddle.Utils
{
    class Parser
    {
        static Chapter parseDoc(string path)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(path);
            XmlNode chapterNode = doc.GetElementsByTagName("Chapter").Item(0);
            XmlNode stepNode = chapterNode.SelectSingleNode("Step");
            Chapter chapter = new Chapter(int.Parse(chapterNode.Attributes["id"].Value), chapterNode.Attributes["name"].Value);
            chapter.Step = new Step(stepNode.Attributes["id"].Value);
            PopulateStep(ref chapter.Step, stepNode);
            return chapter;
        }
        static bool StepHasPossibilities(XmlNode step)
        {
            return step.SelectSingleNode("Possibilities").ChildNodes.Count > 0;
        }

        static void PopulateStep(ref Step step, XmlNode stepNode)
        {
            if (StepHasPossibilities(stepNode))
            {
                XmlNode possibilites = stepNode.SelectSingleNode("Possibilities");
                foreach (XmlNode poss in possibilites.ChildNodes)
                {
                    switch (poss.Name)
                    {
                        case "GoBack":

                            GoBack gb = new GoBack(poss.Attributes["id"].Value);
                            step.addPossibility(gb);

                            break;
                        case "Step":
                            Step s = new Step(poss.Attributes["id"].Value);

                            if (StepHasPossibilities(poss)) {
                                PopulateStep(ref s, poss);
                            }
                            step.addPossibility(s);
                            break;
                        case "Check":

                            Check c = new Check(poss.Attributes["operator"].Value, poss.Attributes["prop"].Value, poss.Attributes["val"].Value, poss.Attributes["then"].Value, poss.Attributes["end"].Value);

                            break;
                        default:
                            throw new NotImplementedException();
                    }
                }
            }
        }
    }
}
