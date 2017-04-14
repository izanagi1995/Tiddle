using System;
using System.Collections.Generic;
using System.Xml;

namespace Tiddle.Definition
{
    public class SaveObject
    {
        private string _id;
        public string Id
        {
            get { return this._id;  }
        }
        private bool _hasChildren;
        public string Value;
        public Dictionary<string, SaveObject> Children;

        public SaveObject(string id)
        {
            this._id = id;
            this.Children = new Dictionary<string, SaveObject>();
        }

        public SaveObject this[string key]
        {
            get
            {
                if (this.Children.ContainsKey(key))
                {
                    return this.Children[key];
                }
                return null;
            }
            set
            {
                this.Children[key] = value;
            }
        }

        internal void Parse(XmlNode saveObjXML)
        {
            if (saveObjXML.HasChildNodes && saveObjXML.SelectNodes("SaveObject").Count > 0)
            {
                foreach(XmlNode n in saveObjXML.ChildNodes)
                {
                    SaveObject s = new SaveObject(n.Attributes["id"].Value);
                    s.Parse(n);
                    this.Children.Add(s.Id, s);
                    this._hasChildren = true;
                }
            }
            else
            {
                this.Value = saveObjXML.InnerText;
            }
        }
    }
}