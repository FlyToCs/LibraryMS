using Figgle.Fonts;
using LibraryMS.Application_Service.Services;
using LibraryMS.Domain.Contracts.Service_Contracts;
using LibraryMS.Domain.Entities;
using LibraryMS.Domain.Enums;
using LibraryMS.Framework;
using Sharprompt;
using Spectre.Console;
Console.OutputEncoding = System.Text.Encoding.UTF8;



IAuthenticationService authenticationService = new AuthenticationService();
IBookService bookService = new BookService();
IUserService userService = new UserService();
IBookCategoryService bookCategoryService = new CategoryService();
IBorrowedBookService borrowedBookService = new BorrowedBookService();




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

AnsiConsole.MarkupLine("[bold cyan]✔ Application started successfully![/]");
Console.ReadKey();
User? currentUser = null!;
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
        Console.WriteLine("------------------");

        try
        {
            switch (select)
            {
                case "1. Login":
                    Console.Write("\nUsername: ");
                    string userName = Console.ReadLine()!;

                    Console.Write("Password: ");
                    string password = Console.ReadLine()!;
                    currentUser = authenticationService.Login(userName, password);


                    if (currentUser != null && currentUser.UserRole == UserRoleEnum.Member)
                        MemberMenu();

                    else
                        AdminMenu();

                    Console.ReadKey();

                    break;

                case "2. Register":

                    Console.Write("\nFirst Name: ");
                    string newFirstName = Console.ReadLine()!;

                    Console.Write("Last Name: ");
                    string newLastName = Console.ReadLine()!;

                    Console.Write("Username: ");
                    string newUsername = Console.ReadLine()!;

                    Console.Write("Password: ");
                    string newPassword = Console.ReadLine()!;

                    Console.Write("Email: ");
                    string newEmail = Console.ReadLine()!;

                    var roll = Prompt.Select("Select your roll", new[]
                    {
                        "Member",
                        "Admin"
                    });
                    var newRoll = roll == "Member" ? UserRoleEnum.Member : UserRoleEnum.Admin;


                    authenticationService.Register(newFirstName, newLastName, newUsername, newPassword, newEmail, newRoll);

                    
                    Console.WriteLine("registration successfully");
    
                    Console.ReadKey();
                    break;

                case "3. Exit":
                    Environment.Exit(-1);
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

void MemberMenu()
{
    while (true)
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine(FiggleFonts.Standard.Render("Member Panel"));
        Console.ResetColor();

        var select = Prompt.Select("Select an option", new[]
        {
            "1. Show all books",
            "2. Show books based on categories",
            "3. Borrow a book",
            "4. Show my borrowed book history",
            "5. Show borrowed books",
            "6. Return a book",
            "7. Logout"
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
                    }

                    Console.ReadKey();
                    break;
                }


                case "2. Show books based on categories":
                    break;

                case "3. Borrow a book":
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


                case "4. Show my borrowed book history":
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


                case "5. Show borrowed books":
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


                case "6. Return a book":
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


                case "7. Logout":
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
            "8. Logout"
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

                            int idToActivate = AnsiConsole.Ask<int>("[bold yellow]Enter the ID of the user to activate:[/]");

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

                            int idToDeActivate = AnsiConsole.Ask<int>("[bold yellow]Enter the ID of the user to deactivate:[/]");

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

                case "8. Logout":
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