using AutoMapper;
using NewsApi.Data;
using NewsApi.DTO;

namespace NewsApi.Service
{
    public interface INewsService
    {
        //List<NewsDto> Parse(string URLpath);
        List<NewsDto> GetNewsSorted(string FieldSort, string SortOrder, string URLpath);
        List<NewsDto> SearchKeyword(string FieldSearch, string URLpath);
        List<NewsDto> GetNewsFromDb(NewsContext _context, IMapper _mapper);
        Task<string> AddToDb(NewsContext _context, IMapper _mapper, string URLpath);
    }
}
