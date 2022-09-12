using System.Xml;

namespace NewsApi.Service.ParseXML
{
    public class ParseSimpleData
    {
        public string Parse(XmlNode item, params string[] nodes)
        {
            foreach (XmlNode child in item.ChildNodes)
            {
                foreach (string node in nodes)
                {
                    if (Convert.ToString(child.Name).ToLower().Contains(node.ToLower()))
                    {
                        return item[Convert.ToString(child.Name)].InnerText;
                    }
                }

            }
            return null;

        }
    }
}
