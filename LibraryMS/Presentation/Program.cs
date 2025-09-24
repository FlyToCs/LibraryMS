using Figgle.Fonts;
using LibraryMS.Application_Service.Services;
using LibraryMS.Domain.Contracts.Repository_Contracts;
using LibraryMS.Domain.Contracts.Service_Contracts;
using LibraryMS.Domain.Entities;
using LibraryMS.Domain.Enums;
using LibraryMS.Framework;
using LibraryMS.Infrastructure.Repositories;
using Microsoft.Identity.Client.Extensions.Msal;
using Sharprompt;
using Spectre.Console;
Console.OutputEncoding = System.Text.Encoding.UTF8;



IAuthenticationService authenticationService = new AuthenticationService();
IBookService bookService = new BookService();
IUserService userService = new UserService();
IBookCategoryService bookCategoryService = new CategoryService();




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
User currentUser = null!;
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
            Console.WriteLine(e.Message);
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
            "4. Show my borrowed books",
            "5. Logout"
        });



        try
        {
            switch (select)
            {
                case "1. Show all books":
                    
                    break;

                case "2. Show books based on categories":
                    break;

                case "3. Borrow a book":
                    break;

                case "4. Show my borrowed books":
                    break;

                case "5. Logout":
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
            "7. Show all categories"
        });

        try
        {
            switch (select)
            {
                case "1. Show all users":
                    
                    ConsolePainter.WriteTable(userService.GetAll());
                    Console.ReadKey();
                    break;

                case "2. Activate user":
                    ConsolePainter.WriteTable(userService.GetAllInActive());
                    Console.ReadKey();
                    break;

                case "3. Deactivate user":
                    ConsolePainter.WriteTable(userService.GetAllActive());
                    Console.ReadKey();
                    break;

                case "4. Add a new book":
                    //string title, string description, string author
                    break;

                case "5. Add a new category":
                    Console.Write("Enter Category name: ");
                    string categoryName = Console.ReadLine()!;
                    bookCategoryService.Add(categoryName);
                    ConsolePainter.GreenMessage("new category added");
                    break;

                case "6. Show all books":
                    ConsolePainter.WriteTable(bookService.GetAll()); 
                    Console.ReadKey();
                    break;

                case "7. Show all categories":
                    ConsolePainter.WriteTable(bookCategoryService.GetAll());
                    Console.ReadKey();
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