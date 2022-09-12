using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NewsApi.Data;
using NewsApi.DTO;
using NewsApi.Service;

namespace NewsApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewsController : ControllerBase
    {
        private NewsContext _context;
        private readonly IMapper _mapper;
        private readonly INewsService newsService;
        public NewsController(NewsContext context, IMapper mapper, INewsService newsService)
        {
            _context = context;
            _mapper = mapper;
            this.newsService = newsService;
        }


        [HttpGet]
        [Route("NewsSorted")]
        public ActionResult<List<NewsDto>> GetNewsSorted(string FieldSort, string SortOrder, string URLpath)
        {
            return Ok(newsService.GetNewsSorted(FieldSort, SortOrder, URLpath));
        }


        [HttpGet]
        [Route("SearchKeyword")]
        public ActionResult<IEnumerable<NewsDto>> SearchKeyword(string FieldSearch, string URLpath)
        {
            return Ok(newsService.SearchKeyword(FieldSearch, URLpath));
        }



        [Route("GetNews")]
        [HttpGet]
        public ActionResult<IEnumerable<NewsDto>> GetNews()
        {
            if (_context.News == null)
            {
                return NotFound();
            }
            return Ok(newsService.GetNewsFromDb(_context, _mapper));
        }

        [Route("InsertNews")]
        [HttpPost]
        public async Task<ActionResult<NewsDto>> PostNews(string URLpath)
        {
            return Ok(await newsService.AddToDb(_context, _mapper, URLpath));
        }
    }
}