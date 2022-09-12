using System.Xml;
using NewsApi.DTO;

namespace NewsApi.Service.ParseXML
{
    public class AssignData
    {
        public List<NewsDto> Parse(XmlNodeList items, string channel)
        {
            List<NewsDto> listOfNews = new List<NewsDto>();
            foreach (XmlNode item in items)
            {
                NewsDto news = new NewsDto();
                Guid Id = Guid.NewGuid();
                news.Id = Id;

                news.Channel = channel;

                ParseSimpleData simpleData = new ParseSimpleData();
                ParseComplexData complexData = new ParseComplexData();
                news.Title = simpleData.Parse(item, "title");
                news.Guid = simpleData.Parse(item, "guid");
                news.Creator = simpleData.Parse(item, "creator");
                news.Date = simpleData.Parse(item, "date", "pub");
                news.DateReceived = Convert.ToString(DateTime.Now);
                news.Description = simpleData.Parse(item, "description");
                news.Link = simpleData.Parse(item, "link");
                news.MediaContent = complexData.Parse(item, "url", ":content", "thumbnail");

                if (news.Link == null)
                {
                    news.Link = complexData.Parse(item, "href", "atom:link");
                }

                if (news.Link == null)
                {
                    news.Link = complexData.Parse(item, "url", "source");
                }

                if (news.MediaContent == null)
                {
                    foreach (XmlNode child in item.ChildNodes)
                    {
                        if (Convert.ToString(child.Name).Contains("group"))
                        {
                            news.MediaContent = complexData.Parse(child, "url", ":content", "thumbnail");
                        }
                    }
                }

                listOfNews.Add(news);
            }


            return listOfNews;
        }
    }
}
