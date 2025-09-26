using LibraryMS.Application_Service.DTOs;
using LibraryMS.Domain.Contracts.Repository_Contracts;
using LibraryMS.Domain.Contracts.Service_Contracts;
using LibraryMS.Domain.Entities;
using LibraryMS.Infrastructure.Repositories;

namespace LibraryMS.Application_Service.Services;

public class WishListService : IWishListService
{
    private readonly IWishListRepository _wishListRepository= new WishListRepository();
    private readonly IUserService _userService = new UserService();
    private readonly IBookService _bookService = new BookService();
    public int Add(int userId, int bookId)
    {
        var user = _userService.GetById(userId);
        var book = _bookService.GetById(bookId);

        if (user is null || book is null)
            throw new Exception("user or book not found");

        var newWish = new WishList(userId, bookId);
        _wishListRepository.Add(newWish);
        return newWish.Id;
    }

    public void Delete(int id)
    {
        _wishListRepository.Delete(id);
    }

    public List<WishListDot> GetAllByUserId(int userId)
    {
        var whishes =  _wishListRepository.GetAllByUserId(userId);
        return whishes.Select(x=>new WishListDot()
        {
            Id = x.Id,
            BookName = x.Book.Title
        }).ToList();
    }
}