using NewsApi.Data;
using NewsApi.Models;

namespace NewsApi.DAL
{
    public class NewsDAL
    {
        public void Insert(List<News> newsList, NewsContext _context, string URLpath)
        {
            DbAccess dbAccess = new DbAccess();

            foreach (var news in newsList)
            {
                bool inserted;
                String Id = "";
                inserted = dbAccess.InsertNews(news, _context, ref Id);

                if (inserted == false)
                {
                    DuplicatedNews duplicatedNews = new DuplicatedNews(Guid.Parse(Id), news.Guid, Convert.ToString(DateTime.Now), news.DateReceived, URLpath);
                    dbAccess.InsertDuplicatedNews(duplicatedNews, _context);
                }

            }
            _context.SaveChanges();
        }


        public List<News> GetNews(NewsContext _context)
        {
            return _context.News.ToList();
        }





    }

}
