using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Tiddle.Definition;
using Tiddle.Utils;

namespace Tiddle
{
    class Program
    {
        static void Main(string[] args)
        {

            bool chapterDone = false;

            Player p = new Player("Player");

            //

            Chapter one = Chapter.Parse(@"..\Resources\1.xml");
            one.Player = p;
            
            Console.WriteLine(one.Step.Label);
            Step currentStep = one.Step;
            Dictionary<string, Step> index = buildIndex(currentStep);
            while (!chapterDone)
            {
                Console.Clear()
                    ;
                Console.BackgroundColor = ConsoleColor.Black;
                one.Player.Days += currentStep.Days;
                one.Player.Goodness += currentStep.Goodness;
                one.Player.Badness += currentStep.Badness;
                List<IPossibility> possStep = currentStep.Possibilities.FindAll(x=>x is Step);
                if(possStep.Count > 0)
                {
                    Selector s = new Selector(currentStep.Label);
                    foreach (Step st in possStep)
                    {
                        s.question.addAnswer(st.Label, st.Label, ConsoleColor.White);
                    }
                    s.start().Join();
                    Console.Clear();
                    int i = s.question.getAnswerIndex();
                    currentStep = (Step)possStep[i];
                }
                else if(currentStep.Possibilities[0] is GoBack)
                {
                    Console.WriteLine(currentStep.Result);
                    if(currentStep.Object != null)
                    {
                        bool won = bool.Parse(currentStep.Object.Value);
                        string id = currentStep.Object.Id;
                        string[] spl = id.Split('.');
                        Console.WriteLine("You "+(won ? "won" : "loss") + " the " + spl.Last());
                        dynamic value = one.Player.Save[spl[0]];
                        for (int i = 1; i < spl.Length; i++)
                        {
                            value = value[spl[i]];
                        }
                        value.Value = currentStep.Object.Value;
                    }
                    Console.ReadLine();
                    GoBack gb = (GoBack) currentStep.Possibilities[0];
                    currentStep = index[gb.Id];
                }
                else
                {
                    Console.WriteLine(currentStep.Result);
                    foreach(Check c in currentStep.Possibilities)
                    {
                        dynamic value;
                        string prop = c.Prop;
                        string[] spl = prop.Split('.');
                        if(spl.Length > 1)
                        {
                            value = one.Player.Save[spl[0]];
                            for(int i = 1; i < spl.Length; i++)
                            {
                                value = value[spl[i]].Value;
                            }
                        }
                        else
                        {
                            Type t = one.Player.GetType();
                            PropertyInfo propInfo = t.GetProperty(prop);
                            value = propInfo.GetValue(one.Player, null);
                        }

                        string sceneName = c.Resolve(value);
                        
                        if(sceneName == "End")
                        {
                            chapterDone = true;
                        }
                        else
                        {
                            currentStep = index[sceneName];
                        }
                    }

                    Console.ReadLine();
                }
                
            }
            Console.WriteLine("You have won this chapter!");
            Console.ReadLine();
        }

        static Dictionary<string, Step> buildIndex(Step root)
        {
            Dictionary<string, Step> dic = new Dictionary<string, Step>();
            dic.Add(root.Id, root);
            List<IPossibility> subSteps = root.Possibilities.FindAll(x => x is Step);
            if (subSteps.Count > 0)
            {
                foreach(Step subStep in subSteps)
                {
                    foreach(KeyValuePair<string, Step> indexItem in buildIndex(subStep))
                    {
                        dic.Add(indexItem.Key, indexItem.Value);
                    }
                }
            }
            return dic;
        }
    }
}
