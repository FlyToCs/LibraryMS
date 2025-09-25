namespace LibraryMS.Domain.Entities;

public class Book
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string Author { get; set; }
    //public bool IsBorrow { get; set; }

    public BookCategory BookCategory { get; set; }
    public int BookCategoryId { get; set; }

    public List<BorrowedBook> BorrowedBooks { get; set; } = [];
    public List<Review> Reviews { get; set; } = [];

    //ask it
    public Book(string title, string description, string author, int categoryId)
    {
        Title = title;
        Description = description;
        Author = author;
        BookCategoryId = categoryId;
    }

    public Book()
    {
        
    }

}