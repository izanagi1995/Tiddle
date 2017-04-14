using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Tiddle.Utils
{
    class Selector
    {
        public Question question { get; set; }

        public class Question
        {

            public class Answer
            {
                public Answer(string label, string value, ConsoleColor color)
                {
                    this.label = label;
                    this.value = value;
                    this.color = color;
                }

                public string label { get; set; }
                public string value { get; set; }
                public ConsoleColor color { get; set; }



            }

            public string question { get; set; }
            public List<Answer> answers { get; set; }
            public int selection { get; set; }

            public Question(string question)
            {
                this.question = question;
                this.answers = new List<Answer>();
            }

            internal int getAnswerIndex()
            {
                return selection;
            }

            public void addAnswer(string label, string value, ConsoleColor color)
            {
                this.answers.Add(new Answer(label, value, color));
            }

            public string getAnswer()
            {
                return this.answers.ElementAt(selection).value;
            }

        }

        internal class InputHandler
        {
            private Selector sel;
            private bool done = false;

            public InputHandler(Selector s)
            {
                this.sel = s;
            }

            public void handleInput()
            {
                while (!done)
                {
                    switch (Console.ReadKey(true).Key)
                    {
                        case ConsoleKey.UpArrow:
                            sel.question.selection -= 1;
                            sel.redraw();
                            break;
                        case ConsoleKey.DownArrow:
                            sel.question.selection += 1;
                            sel.redraw();
                            break;
                        case ConsoleKey.Enter:
                            done = true;
                            break;
                    }
                }

            }
        }

        private void redraw()
        {
            Console.SetCursorPosition(0, 0);
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Black;
            Console.WriteLine(this.question.question);
            for (int i = 0; i < this.question.answers.Count; i++)
            {
                if (this.question.selection == i)
                {
                    Console.BackgroundColor = ConsoleColor.White;
                    Console.ForegroundColor = ConsoleColor.Black;
                }
                else
                {
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.ForegroundColor = ConsoleColor.White;
                }
                Console.WriteLine(this.question.answers[i].label);
                Console.BackgroundColor = ConsoleColor.Black;
                
            }
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Black;
        }

        public Selector(string question)
        {
            this.question = new Question(question);
        }

        public Thread start()
        {
            this.redraw();
            InputHandler ih = new InputHandler(this);
            Thread t = new Thread(ih.handleInput);
            t.Start();
            return t;
        }


    }

}
