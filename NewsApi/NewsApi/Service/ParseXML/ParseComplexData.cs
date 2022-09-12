using System.Xml;

namespace NewsApi.Service.ParseXML
{
    public class ParseComplexData
    {
        public string Parse(XmlNode item, string attribute, params string[] nodes)
        {
            foreach (XmlNode child in item.ChildNodes)
            {
                foreach (string node in nodes)
                {
                    if (Convert.ToString(child.Name).ToLower().Contains(node.ToLower()))
                    {
                        return item[Convert.ToString(child.Name)].Attributes[attribute].Value;
                    }
                }

            }
            return null;
        }
    }
}
