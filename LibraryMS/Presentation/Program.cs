using Figgle.Fonts;
using LibraryMS.Application_Service.Services;
using LibraryMS.Domain.Contracts.Repository_Contracts;
using LibraryMS.Domain.Contracts.Service_Contracts;
using LibraryMS.Domain.Enums;
using LibraryMS.Infrastructure.Repositories;
using Microsoft.Identity.Client.Extensions.Msal;
using Sharprompt;
using Spectre.Console;
Console.OutputEncoding = System.Text.Encoding.UTF8;



IAuthenticationService authentication = new AuthenticationService();

Console.WriteLine("نسخه 2.5.6 کتابخانه پشمک حاج عبدالاه".Reverse().ToArray());

await AnsiConsole.Progress()
    .Columns(new ProgressColumn[]
    {
        new TaskDescriptionColumn(),
        new ProgressBarColumn(),
        new PercentageColumn(),
        new SpinnerColumn()
    })
    .StartAsync(async ctx =>
    {
        var dbTask = ctx.AddTask("[red]Connecting to database[/]");
        var appTask = ctx.AddTask("[yellow]Loading application[/]");
        var uiTask = ctx.AddTask("[green]Building UI[/]");

        while (!ctx.IsFinished)
        {
            await Task.Delay(150);

            dbTask.Increment(2.5);
            appTask.Increment(2);
            uiTask.Increment(3.0);
        }
    });

AnsiConsole.MarkupLine("[bold cyan]✔ Application started successfully![/]");
Console.ReadKey();

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
                    authentication.Login(userName, password);
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


                    authentication.Register(newFirstName, newLastName, newUsername, newPassword, newEmail, newRoll);
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

}

void AdminMenu()
{

}