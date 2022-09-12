using System.Xml;
using System.Reflection;
using NewsApi.DTO;
using NewsApi.Service.ParseXML;
using NewsApi.Models;
using AutoMapper;
using NewsApi.Data;
using NewsApi.DAL;
using System.Globalization;

namespace NewsApi.Service
{
    public class NewsService : INewsService
    {

        private List<NewsDto> Parse(string URLpath)
        {
            URLpath = System.Web.HttpUtility.UrlDecode(URLpath);

            List<NewsDto> listOfNews = new List<NewsDto>();
            XmlDocument xmlDoc = new XmlDocument();

            xmlDoc.Load(URLpath);
            if (xmlDoc.DocumentElement != null)
            {
                XmlElement listOfNodes = xmlDoc.DocumentElement;

                XmlNodeList channel = listOfNodes.SelectNodes("channel/title");

                XmlNodeList items = listOfNodes.GetElementsByTagName("item");

                AssignData assignData = new AssignData();
                listOfNews = assignData.Parse(items, channel[0].InnerText);

            }


            return listOfNews;
        }

        public List<NewsDto> GetNewsSorted(string FieldSort, string SortOrder, string URLpath)
        {
            List<NewsDto> list = Parse(URLpath);

            switch (FieldSort)
            {
                case "title":
                    switch (SortOrder)
                    {
                        case "0":
                            return list.OrderBy(news => news.Title).ToList();

                        case "1":
                            return list.OrderByDescending(news => news.Title).ToList();
                    }
                    break;

                case "date":
                    switch (SortOrder)
                    {
                        case "0":
                            return list.OrderBy(news => (news.Date + " 12:00:00 AM", "MM/dd/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture)).ToList();

                        case "1":
                            return list.OrderByDescending(news => (news.Date + " 12:00:00 AM", "MM/dd/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture)).ToList();
                    }

                    break;

                default:

                    break;
            }
            return list;
        }

        public List<NewsDto> SearchKeyword(string FieldSearch, string URLpath)
        {
            List<NewsDto> list = Parse(URLpath);
            List<NewsDto> keywordContainingList = new List<NewsDto>();

            if (FieldSearch != null)
            {
                string keyword = FieldSearch;

                foreach (NewsDto n in list)
                {
                    Type t = n.GetType();
                    PropertyInfo[] props = t.GetProperties();
                    foreach (var prop in props)
                        if (prop.GetIndexParameters().Length == 0)
                        {
                            if (Convert.ToString(prop.GetValue(n)).ToLower().Contains(keyword.ToLower()))
                            {
                                keywordContainingList.Add(n);
                            }
                        }
                }
            }

            return keywordContainingList;
        }

        public List<NewsDto> GetNewsFromDb(NewsContext _context, IMapper _mapper)
        {
            NewsDAL newsDal = new();
            var list = newsDal.GetNews(_context);

            var _mappedNews = _mapper.Map<List<NewsDto>>(list);

            return _mappedNews;
        }

        public async Task<string> AddToDb(NewsContext _context, IMapper _mapper, string URLpath)
        {
            List<NewsDto> list = Parse(URLpath);
            var _mappedNews = _mapper.Map<List<News>>(list);

            NewsDAL newsDal = new();
            newsDal.Insert(_mappedNews, _context, URLpath);
            return "Safe succesfully!";
        }

    }
}