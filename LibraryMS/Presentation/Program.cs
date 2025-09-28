using Figgle.Fonts;
using LibraryMS.Application_Service.Services;
using LibraryMS.Domain.Contracts.Service_Contracts;
using LibraryMS.Domain.Entities;
using LibraryMS.Domain.Enums;
using LibraryMS.Framework;
using Sharprompt;
using Spectre.Console;
using System.Collections.Generic;
using LibraryMS.Application_Service.DTOs;

Console.OutputEncoding = System.Text.Encoding.UTF8;



IAuthenticationService authenticationService = new AuthenticationService();
IBookService bookService = new BookService();
IUserService userService = new UserService();
IBookCategoryService bookCategoryService = new CategoryService();
IBorrowedBookService borrowedBookService = new BorrowedBookService();
IReviewService reviewService = new ReviewService();
IWishListService wishListService = new WishListService();




// Console.WriteLine("نسخه 2.5.6 کتابخانه پشمک حاج عبدالاه".Reverse().ToArray());
//
// await AnsiConsole.Progress()
//     .Columns(new ProgressColumn[]
//     {
//         new TaskDescriptionColumn(),
//         new ProgressBarColumn(),
//         new PercentageColumn(),
//         new SpinnerColumn()
//     })
//     .StartAsync(async ctx =>
//     {
//         var dbTask = ctx.AddTask("[red]Connecting to database[/]");
//         var appTask = ctx.AddTask("[yellow]Loading application[/]");
//         var uiTask = ctx.AddTask("[green]Building UI[/]");
//
//         while (!ctx.IsFinished)
//         {
//             await Task.Delay(150);
//
//             dbTask.Increment(2.5);
//             appTask.Increment(2);
//             uiTask.Increment(3.0);
//         }
//     });
//
// AnsiConsole.MarkupLine("[bold cyan]✔ Application started successfully![/]");
// Console.ReadKey();



UserDto? currentUser = null!;
AuthenticationMenu();








void AuthenticationMenu()
{
    while (true)
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine(FiggleFonts.Standard.Render("Authentication"));
        Console.ResetColor();

        var select = Prompt.Select("Select an option", new[]
        {
            "1. Login",
            "2. Register",
            "3. Exit"
        });

        try
        {
            switch (select)
            {
                case "1. Login":
                    {
                        string userName = AnsiConsole.Ask<string>("\n[cyan]Username:[/] ");

                        string password = AnsiConsole.Prompt(
                            new TextPrompt<string>("[cyan]Password:[/] ")
                                .PromptStyle("red")
                                .Secret());

                        currentUser = authenticationService.Login(userName, password);

                        if (currentUser != null && currentUser.Roll == UserRoleEnum.Member)
                            MemberMenu();
                        else if (currentUser != null && currentUser.Roll == UserRoleEnum.Admin)
                            AdminMenu();

                        break;
                    }

                case "2. Register":
                    {
                        string newFirstName = AnsiConsole.Ask<string>("\n[cyan]First Name:[/] ");
                        string newLastName = AnsiConsole.Ask<string>("[cyan]Last Name:[/] ");
                        string newUsername = AnsiConsole.Ask<string>("[cyan]Username:[/] ");

                        string newPassword = AnsiConsole.Prompt(
                            new TextPrompt<string>("[cyan]Password:[/] ")
                                .PromptStyle("red")
                                .Secret());

                        string newEmail = AnsiConsole.Ask<string>("[cyan]Email:[/] ");

                        var role = Prompt.Select("Select your role", new[]
                        {
                        "Member",
                        "Admin"
                    });
                        var newRole = role == "Member" ? UserRoleEnum.Member : UserRoleEnum.Admin;

                        authenticationService.Register(newFirstName, newLastName, newUsername, newPassword, newEmail, newRole);

                        AnsiConsole.MarkupLine("[green]✅ Registration successful![/]");
                        break;
                    }

                case "3. Exit":
                    Environment.Exit(0);
                    break;
            }
        }
        catch (Exception e)
        {
            AnsiConsole.MarkupLine($"[bold red]❌ Error: {e.Message}[/]");
        }

        Console.ReadKey();
    }
}



void MemberMenu()
{
    while (true)
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine(FiggleFonts.Standard.Render("Member Panel"));
        Console.ResetColor();
        Console.WriteLine($"\n\n PenaltyAmount: {userService.GetById(currentUser.Id).PenaltyAmount} Tomans\n\n");

        var select = Prompt.Select("Select an option", new[]
        {
            "1. Show all books",
            "2. Borrow a book",
            "3. Show my borrowed book history",
            "4. Show borrowed books",
            "5. Return a book",
            "6. Add a Review",
            "7. Edit a Review",
            "8. Delete a Review",
            "9. Show reviews of a book",
            "10. WishList",
            "11. Logout"
        });



        try
        {
            switch (select)
            {
                case "1. Show all books":
                    {
                        var books = bookService.GetUnBorrowedBooks();

                        if (books == null || books.Count == 0)
                        {
                            AnsiConsole.MarkupLine("[red]📕 No available books found![/]");
                        }
                        else
                        {
                            var table = new Table()
                                .Border(TableBorder.Rounded)
                                .Title("[bold green]📚 Available Books[/]")
                                .Expand();

                            table.AddColumn("[yellow]ID[/]");
                            table.AddColumn("[cyan]Title[/]");
                            table.AddColumn("[green]Author[/]");
                            table.AddColumn("[blue]Category[/]");
                            table.AddColumn("[purple]Description[/]");
                            table.AddColumn("[orange1]⭐ Avg. Score[/]");

                            foreach (var book in books)
                            {
                                table.AddRow(
                                    book.Id.ToString(),
                                    $"[bold]{book.Title}[/]",
                                    book.Author ?? "[grey]-[/]",
                                    book.CategoryName ?? "[grey]-[/]",
                                    string.IsNullOrWhiteSpace(book.Description)
                                        ? "[grey]-[/]"
                                        : book.Description.Length > 40
                                            ? book.Description.Substring(0, 40) + "..."
                                            : book.Description,
                                    book.AvgScore > 0
                                        ? $"[bold yellow]{book.AvgScore:F1}[/]"
                                        : "[grey]No ratings[/]"
                                );
                            }

                            AnsiConsole.Write(table);
                        }

                        Console.ReadKey();
                        break;
                    }

                case "2. Borrow a book":
                    {
                        var books = bookService.GetUnBorrowedBooks();

                        if (books == null || books.Count == 0)
                        {
                            AnsiConsole.MarkupLine("[red]📕 No available books to borrow![/]");
                        }
                        else
                        {
                            var table = new Table()
                                .Border(TableBorder.Rounded)
                                .Title("[bold green]📚 Available Books[/]")
                                .Expand();

                            table.AddColumn("[yellow]ID[/]");
                            table.AddColumn("[cyan]Title[/]");
                            table.AddColumn("[green]Author[/]");
                            table.AddColumn("[blue]Category[/]");

                            foreach (var book in books)
                            {
                                table.AddRow(
                                    book.Id.ToString(),
                                    $"[bold]{book.Title}[/]",
                                    book.Author ?? "[grey]-[/]",
                                    book.CategoryName ?? "[grey]-[/]"
                                );
                            }

                            AnsiConsole.Write(table);
                            int bookIdToBorrow = AnsiConsole.Ask<int>("[bold yellow]Enter book ID to borrow:[/]");
                            borrowedBookService.BorrowBook(currentUser.Id, bookIdToBorrow);
                            AnsiConsole.MarkupLine("[bold green]✅ Book borrowed successfully![/]");
                        }

                        Console.ReadKey();
                        break;
                    }


                case "3. Show my borrowed book history":
                    {
                        var myBorrows = borrowedBookService.GetMyBorrowHistory(currentUser.Id);

                        if (myBorrows == null || !myBorrows.Any())
                        {
                            AnsiConsole.MarkupLine("[red]📕 You have not borrowed any books.[/]");
                        }
                        else
                        {
                            var table = new Table()
                                .Border(TableBorder.Rounded)
                                .Title("[bold green]📚 My Borrowed Book History[/]")
                                .Expand();

                            table.AddColumn("[yellow]Book Name[/]");
                            table.AddColumn("[blue]Borrowed Date[/]");
                            table.AddColumn("[green]Return Date[/]");

                            foreach (var borrow in myBorrows)
                            {
                                table.AddRow(
                                    $"[bold]{borrow.Name}[/]",
                                    borrow.BorrowedDate.ToString("yyyy/MM/dd"),
                                    borrow.ReturnDate?.ToString("yyyy/MM/dd") ?? "[grey]Not returned[/]"
                                );
                            }

                            AnsiConsole.Write(table);
                        }

                        Console.ReadKey();
                        break;
                    }


                case "4. Show borrowed books":
                    {
                        var activeBorrows = borrowedBookService.GetMyActiveBorrows(currentUser.Id);

                        if (activeBorrows == null || !activeBorrows.Any())
                        {
                            AnsiConsole.MarkupLine("[red]📕 You have no active borrowed books.[/]");
                        }
                        else
                        {
                            var table = new Table()
                                .Border(TableBorder.Rounded)
                                .Title("[bold green]📚 My Active Borrowed Books[/]")
                                .Expand();

                            table.AddColumn("[yellow]ID[/]");
                            table.AddColumn("[cyan]Title[/]");
                            table.AddColumn("[blue]Borrowed Date[/]");
                            table.AddColumn("[green]Return Date[/]");

                            foreach (var borrow in activeBorrows)
                            {
                                table.AddRow(
                                    borrow.Id.ToString(),
                                    $"[bold]{borrow.Title}[/]",
                                    borrow.BorrowedDate.ToString("yyyy/MM/dd"),
                                    borrow.ReturnDate?.ToString("yyyy/MM/dd") ?? "[grey]Not returned[/]"
                                );
                            }

                            AnsiConsole.Write(table);
                        }

                        Console.ReadKey();
                        break;
                    }


                case "5. Return a book":
                    {
                        var myActiveBorrows = borrowedBookService.GetMyActiveBorrows(currentUser.Id);

                        if (myActiveBorrows == null || !myActiveBorrows.Any())
                        {
                            AnsiConsole.MarkupLine("[red]📕 You do not have any books to return at the moment.[/]");
                            Console.ReadKey();
                            break;
                        }

                        var table = new Table()
                            .Border(TableBorder.Rounded)
                            .Title("[bold green]📚 Your Currently Borrowed Books[/]")
                            .Expand();

                        table.AddColumn("[yellow]ID[/]");
                        table.AddColumn("[cyan]Title[/]");
                        table.AddColumn("[blue]Borrowed Date[/]");
                        table.AddColumn("[green]Return Date[/]");

                        foreach (var borrow in myActiveBorrows)
                        {
                            table.AddRow(
                                borrow.Id.ToString(),
                                $"[bold]{borrow.Title}[/]",
                                borrow.BorrowedDate.ToString("yyyy/MM/dd"),
                                borrow.ReturnDate?.ToString("yyyy/MM/dd") ?? "[grey]Not returned[/]"
                            );
                        }

                        AnsiConsole.Write(table);

                        int bookIdToReturn = AnsiConsole.Ask<int>("[bold yellow]Enter the ID of the book you want to return:[/]");
                        borrowedBookService.ReturnBook(currentUser.Id, bookIdToReturn);
                        AnsiConsole.MarkupLine("[bold green]✅ Book returned successfully![/]");
                        Console.ReadKey();
                        break;
                    }

                case "6. Add a Review":
                    {
                        var book = bookService.GetAll();
                        ConsolePainter.WriteTable(bookService.GetAll());
                        Console.Write("Enter a BookId: ");
                        int bookId = int.Parse(Console.ReadLine()!);

                        Console.Write("Write your comment: ");
                        string comment = Console.ReadLine()!;


                        int rating = Prompt.Select("Rate to this Book", new[]
                        {
                            1,2,3,4,5
                        });

                        reviewService.Add(currentUser.Id, bookId, rating, comment);
                        Console.ReadKey();
                        break;
                    }


                case "7. Edit a Review":
                    {
                        var myReviews = reviewService.GetMyReviews(currentUser.Id);

                        if (myReviews == null || myReviews.Count == 0)
                        {
                            AnsiConsole.MarkupLine("[red]📝 You have not written any reviews yet![/]");
                        }
                        else
                        {
                            var table = new Table()
                                .Border(TableBorder.Rounded)
                                .Title("[bold green]✍️ My Reviews[/]")
                                .Expand();

                            table.AddColumn("[yellow]ID[/]");
                            table.AddColumn("[cyan]Book[/]");
                            table.AddColumn("[orange1]⭐ Rating[/]");
                            table.AddColumn("[blue]Comment[/]");

                            foreach (var review in myReviews)
                            {
                                table.AddRow(
                                    review.Id.ToString(),
                                    review.BookName ?? "[grey]-[/]",
                                    $"[yellow]{review.Rating:F1}[/]",
                                    string.IsNullOrWhiteSpace(review.Comment)
                                        ? "[grey]-[/]"
                                        : review.Comment.Length > 40
                                            ? review.Comment.Substring(0, 40) + "..."
                                            : review.Comment
                                );
                            }

                            AnsiConsole.Write(table);

                            Console.Write("Enter a review ID to edit: ");
                            if (!int.TryParse(Console.ReadLine(), out int reviewId))
                            {
                                AnsiConsole.MarkupLine("[red]❌ Invalid review ID![/]");
                                break;
                            }

                            int rating = Prompt.Select("Rate this Book", new[] { 1, 2, 3, 4, 5 });

                            Console.Write("Write your comment: ");
                            string comment = Console.ReadLine() ?? string.Empty;

                            reviewService.Edit(reviewId, rating, comment, currentUser.Id);

                            AnsiConsole.MarkupLine("[green]✅ Review updated successfully![/]");
                        }

                        Console.ReadKey();
                        break;
                    }



                case "8. Delete a Review":
                    {
                        var myReviews = reviewService.GetMyReviews(currentUser.Id);

                        if (myReviews == null || myReviews.Count == 0)
                        {
                            AnsiConsole.MarkupLine("[red]📝 You have not written any reviews yet![/]");
                        }
                        else
                        {
                            var table = new Table()
                                .Border(TableBorder.Rounded)
                                .Title("[bold red]❌ My Reviews (Delete)[/]")
                                .Expand();

                            table.AddColumn("[yellow]ID[/]");
                            table.AddColumn("[cyan]Book[/]");
                            table.AddColumn("[orange1]⭐ Rating[/]");
                            table.AddColumn("[blue]Comment[/]");

                            foreach (var review in myReviews)
                            {
                                table.AddRow(
                                    review.Id.ToString(),
                                    review.BookName ?? "[grey]-[/]",
                                    $"[yellow]{review.Rating:F1}[/]",
                                    string.IsNullOrWhiteSpace(review.Comment)
                                        ? "[grey]-[/]"
                                        : review.Comment.Length > 40
                                            ? review.Comment.Substring(0, 40) + "..."
                                            : review.Comment
                                );
                            }

                            AnsiConsole.Write(table);

                            Console.Write("Enter a review ID to delete: ");
                            if (!int.TryParse(Console.ReadLine(), out int reviewId))
                            {
                                AnsiConsole.MarkupLine("[red]❌ Invalid review ID![/]");
                                break;
                            }

                            var confirm = AnsiConsole.Confirm("[bold red]Are you sure you want to delete this review?[/]");
                            if (!confirm)
                            {
                                AnsiConsole.MarkupLine("[yellow]⚠️ Deletion cancelled.[/]");
                                break;
                            }

                            reviewService.Delete(reviewId, currentUser.Id);
                            AnsiConsole.MarkupLine("[green]✅ Review deleted successfully![/]");
                        }

                        Console.ReadKey();
                        break;
                    }

                case "9. Show reviews of a book":
                    {

                        Console.Write("Enter the Book ID: ");
                        if (!int.TryParse(Console.ReadLine(), out int bookId))
                        {
                            AnsiConsole.MarkupLine("[red]❌ Invalid Book ID![/]");
                            Console.ReadKey();
                            break;
                        }


                        var reviews = reviewService.GetApprovedReviewsByBookId(bookId);

                        if (reviews == null || reviews.Count == 0)
                        {
                            AnsiConsole.MarkupLine("[red]📚 No reviews available for this book![/]");
                        }
                        else
                        {
                            var table = new Table()
                                .Border(TableBorder.Rounded)
                                .Title("[bold green]📖 Approved Reviews[/]")
                                .Expand();

                            table.AddColumn("[yellow]ID[/]");
                            table.AddColumn("[cyan]User[/]");
                            table.AddColumn("[blue]Rating[/]");
                            table.AddColumn("[purple]Comment[/]");
                            table.AddColumn("[grey]Date[/]");

                            foreach (var review in reviews.OrderBy(r => r.CreatedAt))
                            {
                                table.AddRow(
                                    review.Id.ToString(),
                                    review.User.FirstName,
                                    $"[yellow]{review.Rating}⭐[/]",
                                    string.IsNullOrWhiteSpace(review.Comment) ? "[grey]No comment[/]" : review.Comment.Length > 40
                                        ? review.Comment.Substring(0, 40) + "..."
                                        : review.Comment,
                                    review.CreatedAt.ToString("yyyy/MM/dd HH:mm")
                                );
                            }

                            AnsiConsole.Write(table);
                        }

                        Console.ReadKey();
                        break;
                    }

                case "10. WishList":
                    WishListMenu();
                    break;


                case "11. Logout":
                    AuthenticationMenu();
                    break;

            }

        }
        catch (Exception e)
        {
            AnsiConsole.MarkupLine($"[bold red]❌ Error: {e.Message}[/]");
            Console.ReadKey();
        }

    }
}


void WishListMenu()
{

    while (true)
    {

        try
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(FiggleFonts.Standard.Render("WishList"));
            Console.ResetColor();

            var select = Prompt.Select("Select an option", new[]
            {
                "1. Show Wishlist",
                "2. Add a book To Wishlist",
                "3. Delete a book from Wishlist",
                "4. Back to main menu"
            });

            switch (select)
            {

                case "1. Show Wishlist":
                    {
                        var wishList = wishListService.GetAllByUserId(currentUser.Id);

                        if (wishList == null || !wishList.Any())
                        {
                            AnsiConsole.MarkupLine("[red]Your wishlist is empty.[/]");
                            Console.ReadKey();
                            break;
                        }

                        var table = new Table();
                        table.Border = TableBorder.Rounded;
                        table.AddColumn("[yellow]Id[/]");
                        table.AddColumn("[yellow]Book Name[/]");

                        foreach (var item in wishList)
                        {
                            table.AddRow(
                                item.Id.ToString(),
                                item.BookName
                            );
                        }

                        AnsiConsole.Write(table);
                        Console.ReadKey();
                        break;
                    }


                case "2. Add a book To Wishlist":
                    {
                        var books = bookService.GetAll();

                        if (books == null || !books.Any())
                        {
                            AnsiConsole.MarkupLine("[red]No books available to add to wishlist.[/]");
                            Console.ReadKey();
                            break;
                        }

                        var table = new Table();
                        table.Border = TableBorder.Rounded;
                        table.AddColumn("[yellow]Id[/]");
                        table.AddColumn("[yellow]Title[/]");
                        table.AddColumn("[yellow]Author[/]");
                        table.AddColumn("[yellow]Category[/]");
                        table.AddColumn("[yellow]Avg Score[/]");

                        foreach (var book in books)
                        {
                            table.AddRow(
                                book.Id.ToString(),
                                book.Title,
                                book.Author,
                                book.CategoryName,
                                book.AvgScore.ToString("0.0")
                            );
                        }

                        AnsiConsole.Write(table);

                        Console.Write("Enter an Id to add: ");
                        int wishId = int.Parse(Console.ReadLine()!);
                        wishListService.Add(currentUser.Id, wishId);
                        AnsiConsole.MarkupLine("[green]Book added to wishlist![/]");
                        Console.ReadKey();
                        break;
                    }



                case "3. Delete a book from Wishlist":
                    {
                        var wishList = wishListService.GetAllByUserId(currentUser.Id);

                        if (wishList == null || !wishList.Any())
                        {
                            AnsiConsole.MarkupLine("[red]Your wishlist is empty.[/]");

                            Console.ReadKey();
                            break;
                        }


                        var table = new Table();
                        table.Border = TableBorder.Rounded;
                        table.AddColumn("[yellow]Id[/]");
                        table.AddColumn("[yellow]Book Name[/]");

                        foreach (var item in wishList)
                        {
                            table.AddRow(
                                item.Id.ToString(),
                                item.BookName
                            );
                        }

                        AnsiConsole.Write(table);

                        Console.Write("\nEnter an Id to delete: ");
                        
                        if (int.TryParse(Console.ReadLine(), out int wishId))
                        {
                            wishListService.Delete(wishId);
                            AnsiConsole.MarkupLine("[green]Book removed from wishlist successfully![/]");
                            
                        }
                        else
                        {
                            AnsiConsole.MarkupLine("[red]Invalid Id entered![/]");
                        }

                        Console.ReadKey();
                        break;
                    }



                case "4. Back to main menu":
                    MemberMenu();
                    break;
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            Console.ReadKey();
        }

    }



}




void AdminMenu()
{
    while (true)
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine(FiggleFonts.Standard.Render("Admin Panel"));
        Console.ResetColor();

        var select = Prompt.Select("Select an option", new[]
        {
            "1. Show all users",
            "2. Activate user",
            "3. Deactivate user",
            "4. Add a new book",
            "5. Add a new category",
            "6. Show all books",
            "7. Show all categories",
            "8. Status Reviews",
            "9. User List with Penalty Amount",
            "0. Logout"
        });

        try
        {
            switch (select)
            {
                case "1. Show all users":
                    {
                        var users = userService.GetAll();

                        if (users == null || !users.Any())
                        {
                            AnsiConsole.MarkupLine("[red]📕 No users found.[/]");
                        }
                        else
                        {
                            var table = new Table()
                                .Border(TableBorder.Rounded)
                                .Title("[bold green]👥 All Users[/]")
                                .Expand();

                            table.AddColumn("[yellow]ID[/]");
                            table.AddColumn("[cyan]Full Name[/]");
                            table.AddColumn("[green]Email[/]");
                            table.AddColumn("[blue]Username[/]");
                            table.AddColumn("[magenta]Role[/]");
                            table.AddColumn("[red]Status[/]");

                            foreach (var user in users)
                            {
                                table.AddRow(
                                    user.Id.ToString(),
                                    $"[bold]{user.FirstName} {user.LastName}[/]",
                                    user.Email ?? "[grey]-[/]",
                                    user.Username ?? "[grey]-[/]",
                                    user.UserRole.ToString(),
                                    user.IsActive ? "[green]Active[/]" : "[red]Inactive[/]"
                                );
                            }

                            AnsiConsole.Write(table);
                        }

                        Console.ReadKey();
                        break;
                    }


                case "2. Activate user":
                    {
                        var inactiveUsers = userService.GetAllInActive();

                        if (inactiveUsers == null || !inactiveUsers.Any())
                        {
                            AnsiConsole.MarkupLine("[red]❌ No inactive users found.[/]");
                        }
                        else
                        {
                            var table = new Table()
                                .Border(TableBorder.Rounded)
                                .Title("[bold green]🔹 Inactive Users[/]")
                                .Expand();

                            table.AddColumn("[yellow]ID[/]");
                            table.AddColumn("[cyan]Full Name[/]");
                            table.AddColumn("[green]Username[/]");
                            table.AddColumn("[blue]Role[/]");

                            foreach (var user in inactiveUsers)
                            {
                                table.AddRow(
                                    user.Id.ToString(),
                                    $"[bold]{user.FullName}[/]",
                                    user.Username,
                                    user.Roll.ToString()
                                );
                            }

                            AnsiConsole.Write(table);

                            int idToActivate =
                                AnsiConsole.Ask<int>("[bold yellow]Enter the ID of the user to activate:[/]");

                            userService.Activate(idToActivate);

                            AnsiConsole.MarkupLine("[bold green]✅ User status changed successfully![/]");
                        }

                        Console.ReadKey();
                        break;
                    }

                case "3. Deactivate user":
                    {
                        var activeUsers = userService.GetAllActive();

                        if (activeUsers == null || !activeUsers.Any())
                        {
                            AnsiConsole.MarkupLine("[red]❌ No active users found.[/]");
                        }
                        else
                        {
                            var table = new Table()
                                .Border(TableBorder.Rounded)
                                .Title("[bold green]🔹 Active Users[/]")
                                .Expand();

                            table.AddColumn("[yellow]ID[/]");
                            table.AddColumn("[cyan]Full Name[/]");
                            table.AddColumn("[green]Username[/]");
                            table.AddColumn("[blue]Role[/]");

                            foreach (var user in activeUsers)
                            {
                                table.AddRow(
                                    user.Id.ToString(),
                                    $"[bold]{user.FullName}[/]",
                                    user.Username,
                                    user.Roll.ToString()
                                );
                            }

                            AnsiConsole.Write(table);

                            int idToDeActivate =
                                AnsiConsole.Ask<int>("[bold yellow]Enter the ID of the user to deactivate:[/]");

                            userService.Deactivate(idToDeActivate);

                            AnsiConsole.MarkupLine("[bold green]✅ User status changed successfully![/]");
                        }

                        Console.ReadKey();
                        break;
                    }


                case "4. Add a new book":

                    Console.Write("Enter book title: ");
                    string newBookTitle = Console.ReadLine()!;

                    Console.Write("Enter book description: ");
                    string newBookDescription = Console.ReadLine()!;

                    Console.Write("Enter book author: ");
                    string newAuthor = Console.ReadLine()!;

                    Console.Write("Enter book categoryId: ");
                    int newBookCategoryId = int.Parse(Console.ReadLine()!);

                    bookService.Add(newBookTitle, newBookDescription, newAuthor, newBookCategoryId);
                    ConsolePainter.GreenMessage("The new book added");
                    Console.ReadKey();
                    break;

                case "5. Add a new category":
                    {
                        string categoryName = AnsiConsole.Ask<string>("[bold yellow]Enter the category name:[/]");
                        bookCategoryService.Add(categoryName);
                        AnsiConsole.MarkupLine("[bold green]✅ New category added successfully![/]");
                        Console.ReadKey();
                        break;
                    }


                case "6. Show all books":
                    {
                        var books = bookService.GetAll();
                        if (books == null || !books.Any())
                        {
                            AnsiConsole.MarkupLine("[red]📕 No books found.[/]");
                        }
                        else
                        {
                            var table = new Table()
                                .Border(TableBorder.Rounded)
                                .Title("[bold green]📚 All Books[/]")
                                .Expand();

                            table.AddColumn("[yellow]ID[/]");
                            table.AddColumn("[cyan]Title[/]");
                            table.AddColumn("[green]Author[/]");
                            table.AddColumn("[blue]Category[/]");
                            table.AddColumn("[magenta]Description[/]");

                            foreach (var book in books)
                            {
                                table.AddRow(
                                    book.Id.ToString(),
                                    $"[bold]{book.Title}[/]",
                                    book.Author ?? "[grey]-[/]",
                                    book.CategoryName ?? "[grey]-[/]",
                                    book.Description ?? "[grey]-[/]"
                                );
                            }

                            AnsiConsole.Write(table);
                        }

                        Console.ReadKey();
                        break;
                    }


                case "7. Show all categories":
                    {
                        var categories = bookCategoryService.GetAll();

                        if (categories == null || !categories.Any())
                        {
                            AnsiConsole.MarkupLine("[red]📕 No categories found.[/]");
                        }
                        else
                        {
                            var table = new Table()
                                .Border(TableBorder.Rounded)
                                .Title("[bold green]📚 All Book Categories[/]")
                                .Expand();

                            table.AddColumn("[yellow]ID[/]");
                            table.AddColumn("[cyan]Category Name[/]");
                            table.AddColumn("[green]Number of Books[/]");

                            foreach (var category in categories)
                            {
                                table.AddRow(
                                    category.Id.ToString(),
                                    $"[bold]{category.Name}[/]",
                                    category.Books?.Count.ToString() ?? "0"
                                );
                            }

                            AnsiConsole.Write(table);
                        }

                        Console.ReadKey();
                        break;
                    }

                case "8. Status Reviews":
                    {
                        var reviews = reviewService.GetPendingReviews();

                        if (reviews == null || reviews.Count == 0)
                        {
                            AnsiConsole.MarkupLine("[red]📝 No pending reviews found![/]");
                        }
                        else
                        {
                            var table = new Table()
                                .Border(TableBorder.Rounded)
                                .Title("[bold green]📝 Pending Reviews[/]")
                                .Expand();

                            table.AddColumn("[yellow]ID[/]");
                            table.AddColumn("[cyan]User[/]");
                            table.AddColumn("[blue]Book[/]");
                            table.AddColumn("[purple]Comment[/]");
                            table.AddColumn("[orange1]⭐ Rating[/]");

                            foreach (var review in reviews)
                            {
                                table.AddRow(
                                    review.Id.ToString(),
                                    $"[bold]{review.FullName}[/]",
                                    review.BookName ?? "[grey]-[/]",
                                    string.IsNullOrWhiteSpace(review.Comment)
                                        ? "[grey]-[/]"
                                        : review.Comment.Length > 40
                                            ? review.Comment.Substring(0, 40) + "..."
                                            : review.Comment,
                                    review.Rating > 0
                                        ? $"[yellow]{review.Rating:F1}[/]"
                                        : "[grey]No rating[/]"
                                );
                            }

                            AnsiConsole.Write(table);

                            Console.Write("Enter an id to change: ");
                            if (int.TryParse(Console.ReadLine(), out int reviewId))
                            {
                                var status = Prompt.Select("Rate this Book", new[]
                                {
                                "Approved",
                                "Rejected"
                            });

                                var statusReview = status == "Approved"
                                    ? ReviewStatusEnum.Approved
                                    : ReviewStatusEnum.Rejected;

                                reviewService.ChangeStatus(reviewId, statusReview, currentUser.Id);
                                AnsiConsole.MarkupLine("[green]✅ Review status updated successfully[/]");
                            }
                            else
                            {
                                AnsiConsole.MarkupLine("[red]❌ Invalid review ID[/]");
                            }
                        }


                        Console.ReadKey();
                        break;
                    }
                case "9. User List with Penalty Amount":
                    {
                        var usersWithPenalty = userService.GetUserHasPenaltyAmount();

                        if (usersWithPenalty == null || !usersWithPenalty.Any())
                        {
                            AnsiConsole.MarkupLine("[red]❌ No users with penalties found![/]");
                        }
                        else
                        {
                            var table = new Table()
                                .Border(TableBorder.Rounded)
                                .Title("[bold green]👥 Users with Penalties[/]")
                                .Expand();

                            table.AddColumn("[yellow]ID[/]");
                            table.AddColumn("[cyan]Full Name[/]");
                            table.AddColumn("[blue]Username[/]");
                            table.AddColumn("[magenta]Role[/]");
                            table.AddColumn("[red]Penalty Amount[/]");
                            table.AddColumn("[green]Status[/]");

                            foreach (var user in usersWithPenalty)
                            {
                                table.AddRow(
                                    user.Id.ToString(),
                                    user.FullName,
                                    user.Username,
                                    user.Roll.ToString(),
                                    user.PenaltyAmount > 0
                                        ? $"[bold red]{user.PenaltyAmount}[/]"
                                        : "[grey]No Penalty[/]",
                                    user.Status ? "[green]Active[/]" : "[red]Inactive[/]"
                                );
                            }

                            AnsiConsole.Write(table);
                        }

                        Console.ReadKey();
                        break;
                    }


                case "0. Logout":
                    AuthenticationMenu();
                    break;
            }

        }
        catch (Exception e)
        {
            AnsiConsole.MarkupLine($"[bold red]❌ Error: {e.Message}[/]");
            Console.ReadKey();
        }

    }
}