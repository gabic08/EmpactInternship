using NewsApi.Data;
using NewsApi.Models;

namespace NewsApi.DAL
{
    public class DbAccess
    {
        public bool InsertNews(News news, NewsContext _context, ref string Id)
        {
            if (_context.News.Count() == 0)
            {
                _context.News.Add(news);
                return true;
            }
            else
            {
                var inserted = _context.News.Where(news.Guid != null ? x => x.Guid == news.Guid : x => x.Link == news.Link).ToList();
                if (inserted.Count() == 0)
                {
                    _context.News.Add(news);
                    return true;
                }
                else
                {
                    Id = Convert.ToString(inserted[0].Id);
                    return false;
                }

            }
        }




        public void InsertDuplicatedNews(DuplicatedNews duplicatedNews, NewsContext _context)
        {
            if (_context.DuplicatedNews.Count() == 0)
            {
                _context.DuplicatedNews.Add(duplicatedNews);
            }

            else
            {
                var inserted = _context.DuplicatedNews.Where(x => x.GuidLink == duplicatedNews.GuidLink).ToList();
                if (inserted == null)
                {
                    duplicatedNews.Id = inserted[0].Id;
                    duplicatedNews.DateInitialNewsReceived = inserted[0]?.DateReceived;

                    _context.DuplicatedNews.Add(duplicatedNews);
                }
            }

        }


    }
}
