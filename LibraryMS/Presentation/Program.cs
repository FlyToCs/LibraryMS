using Figgle.Fonts;
using Microsoft.Identity.Client.Extensions.Msal;
using Sharprompt;
using Spectre.Console;

Console.OutputEncoding = System.Text.Encoding.UTF8;

Console.WriteLine("نسخه اول رستوران".Reverse().ToArray());

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



        switch (select)
        {
            case "1. Login":
                Console.Write("\nUsername: ");
                string email = Console.ReadLine()!;

                Console.Write("Password: ");
                string password = Console.ReadLine()!;

                break;

            case "2. Register":
                Console.Write("\nUsername: ");
                string newEmail = Console.ReadLine()!;

                Console.Write("Password: ");
                string newPassword = Console.ReadLine()!;


                break;

            case "3. Exit":
                Environment.Exit(-1);
                break;
        }
    }
}

void MemberMenu()
{

}

void AdminMenu()
{

}