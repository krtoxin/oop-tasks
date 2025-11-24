using LibraryLab.Models;

namespace LibraryLab.UI;

internal static class MenuBuilder
{
    public static List<Command> Build(CommandContext ctx)
    {
        var list = new List<Command>();

        void Add(string key, string desc, Action<CommandContext> action, params string[] aliases) => list.Add(new Command(key, desc, action, aliases));

        Add("user-list", "List users", c => {
            foreach (var u in c.Users.GetAll()) Console.WriteLine($"{u.Id} | {u.Name} | {u.Email}");
        }, "ul", "users");
        Add("user-add", "Add user", c => {
            var name = c.Prompt("Name: ");
            var email = c.Prompt("Email: ");
            c.Users.Create(name, email);
            Console.WriteLine("User added.");
        }, "ua");
        Add("user-edit", "Edit user", c => {
            var id = c.PromptGuid("User Id: ");
            var u = c.Users.Get(id);
            if (u == null) { Console.WriteLine("Not found"); return; }
            var nn = c.Prompt("New Name (blank keep): ");
            if (!string.IsNullOrWhiteSpace(nn)) u.Name = nn;
            var ne = c.Prompt("New Email (blank keep): ");
            if (!string.IsNullOrWhiteSpace(ne)) u.Email = ne;
            c.Users.Update(id, u.Name, u.Email);
            Console.WriteLine("Updated.");
        }, "ue");
        Add("user-delete", "Delete user", c => {
            var id = c.PromptGuid("User Id: ");
            if (id == Guid.Empty) { Console.WriteLine("Invalid id"); return; }
            c.Users.Delete(id);
            Console.WriteLine("Deleted.");
        }, "ud");

        Add("category-list", "List categories", c => {
            foreach (var cat in c.Categories.GetAll()) Console.WriteLine($"{cat.Id} | {cat.Name}");
        }, "cl", "categories");
        Add("category-add", "Add category", c => {
            var name = c.Prompt("Category name: ");
            c.Categories.Create(name);
            Console.WriteLine("Category added.");
        }, "ca");
        Add("category-edit", "Edit category", c => {
            var id = c.PromptGuid("Category Id: ");
            var cat = c.Categories.Get(id);
            if (cat == null) { Console.WriteLine("Not found"); return; }
            var nn = c.Prompt("New Name (blank keep): ");
            if (!string.IsNullOrWhiteSpace(nn)) { c.Categories.Update(id, nn); Console.WriteLine("Updated."); }
        }, "ce");
        Add("category-delete", "Delete category", c => {
            var id = c.PromptGuid("Category Id: ");
            c.Categories.Delete(id);
            Console.WriteLine("Deleted.");
        }, "cd");

        Add("book-list", "List books", c => {
            foreach (var b in c.Books.GetAll())
            {
                var cat = c.Categories.Get(b.CategoryId);
                Console.WriteLine($"{b.Id} | {b.Title} | {b.Author} | {cat?.Name}");
            }
        }, "bl", "books");
        Add("book-add", "Add book", c => {
            var title = c.Prompt("Title: ");
            var author = c.Prompt("Author: ");
            var catId = c.PromptGuid("Category Id: ");
            var cat = c.Categories.Get(catId);
            if (cat == null) { Console.WriteLine("Category not found"); return; }
            var book = c.Books.Create(title, author, catId);
            Console.WriteLine("Book added.");
            c.Notifier.NotifyNewBook(book, cat);
        }, "ba");
        Add("book-edit", "Edit book", c => {
            var id = c.PromptGuid("Book Id: ");
            var b = c.Books.Get(id);
            if (b == null) { Console.WriteLine("Not found"); return; }
            var nt = c.Prompt("New Title (blank keep): ");
            if (!string.IsNullOrWhiteSpace(nt)) b.Title = nt;
            var na = c.Prompt("New Author (blank keep): ");
            if (!string.IsNullOrWhiteSpace(na)) b.Author = na;
            var ncidStr = c.Prompt("New Category Id (blank keep): ");
            if (Guid.TryParse(ncidStr, out var ncid)) b.CategoryId = ncid;
            c.Books.Update(id, b.Title, b.Author, b.CategoryId);
            Console.WriteLine("Updated.");
        }, "be");
        Add("book-delete", "Delete book", c => {
            var id = c.PromptGuid("Book Id: ");
            c.Books.Delete(id);
            Console.WriteLine("Deleted.");
        }, "bd");

        Add("subscribe", "Subscribe user to category", c => {
            var uid = c.PromptGuid("User Id: ");
            var cid = c.PromptGuid("Category Id: ");
            c.Subscriptions.Subscribe(uid, cid);
            Console.WriteLine("Subscribed.");
        }, "sub");
        Add("unsubscribe", "Unsubscribe user from category", c => {
            var uid = c.PromptGuid("User Id: ");
            var cid = c.PromptGuid("Category Id: ");
            c.Subscriptions.Unsubscribe(uid, cid);
            Console.WriteLine("Unsubscribed.");
        }, "unsub");

        Add("help", "Show command list", c => {
            Console.WriteLine("Commands:");
            int i = 1;
            foreach (var cmd in list)
                Console.WriteLine($"{i++}. {cmd.Key,-15} {cmd.Description}");
        }, "h", "?");
        Add("exit", "Exit application", c => { /*  */ }, "quit", "q");

        return list;
    }
}
