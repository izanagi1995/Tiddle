using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Tiddle.Definition
{
    class Chapter
    {

        private int _id;
        private string _name;
        public Step Step;
        private DefaultSave DefaultSave;
        private Player _player;

        public Player Player {
            get
            {
                return this._player;   
            }
            set {
                value.Save = this.DefaultSave;
                this._player = value;
            }
        }

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


        public static Chapter Parse(string path)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(path);
            XmlNode chapterNode = doc.GetElementsByTagName("Chapter").Item(0);

            XmlNode defaultSaveNode = chapterNode.SelectSingleNode("DefaultSave");
            DefaultSave save = new DefaultSave();
            save.Parse(defaultSaveNode);
            

            XmlNode stepNode = chapterNode.SelectSingleNode("Step");
            Chapter chapter = new Chapter(int.Parse(chapterNode.Attributes["id"].InnerText), chapterNode.Attributes["name"].InnerText);
            chapter.Step = new Step(stepNode.Attributes["id"].InnerText);
            chapter.Step.Parse(stepNode);
            chapter.DefaultSave = save;
            return chapter;
        }

    }
}
