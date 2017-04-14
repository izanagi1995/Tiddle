using System;
using System.Collections.Generic;
using System.Xml;

namespace Tiddle.Definition
{
    public class DefaultSave
    {
        public Dictionary<string, SaveObject> children;
        public DefaultSave()
        {
            this.children = new Dictionary<string, SaveObject>();
        }
        public SaveObject this[string key]
        {
            get
            {
                if (this.children.ContainsKey(key))
                {
                    return this.children[key];
                }
                return null;
            }
            set
            {
                this.children[key] = value;
            }
        }

        internal void Parse(XmlNode defaultSaveNode)
        {
            if (defaultSaveNode.HasChildNodes)
            {
                foreach (XmlNode saveObjsXML in defaultSaveNode.ChildNodes)
                {
                    SaveObject so = new SaveObject(saveObjsXML.Attributes["id"].Value);
                    so.Parse(saveObjsXML);
                    this.children.Add(so.Id, so);
                }
            }
        }
    }
}