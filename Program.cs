using EFIntro;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System.Net;

using var db = new BloggingContext();
string[] users = File.ReadAllLines("../../../Users.csv");
string[] blogs = File.ReadAllLines("../../../Blogs.csv");
string[] posts = File.ReadAllLines("../../../Posts.csv");

Console.WriteLine($"SQLite DB Located at: {db.DbPath}\n");


foreach (var u in users)
{
    string[] usersTable = u.Split(",");

    int userId = int.Parse(usersTable[0]);
    string username = usersTable[1];
    string password = usersTable[2];

    User? userDb = db.Users?.Find(userId);

    if (userDb != null)
    {
        continue;
    }
    db.Add(new User { Id = userId, Username = username, Password = password });
}
db.SaveChanges();

foreach (var b in blogs)
{
    string[] blogsTable = b.Split(",");

    int blogId = int.Parse(blogsTable[0]);
    string url = blogsTable[1];
    string name = blogsTable[2];

    Blog? blogDb = db.Blogs?.Find(blogId);

    if (blogDb != null)
    {
        continue;
    }
    db.Add(new Blog {Id = blogId, Url = url, Name = name });

}
db.SaveChanges();

foreach (var post in posts)
{
    string[] postsTable = post.Split(",");

    int id = int.Parse(postsTable[0]);
    string title = postsTable[1];
    string content = postsTable[2];
    DateOnly date = DateOnly.Parse(postsTable[3]);
    int BlogId = int.Parse(postsTable[4]);
    int UserId = int.Parse(postsTable[5]);

    db.Add(new Post { Title = title, Content = content, PublishedOn = date, BlogId = BlogId, UserId = UserId });
}
db.SaveChanges();


foreach (var u in db.Users)
{
    Console.WriteLine($"{u.Username} - User");

    foreach (var b in u.Posts)
    {
        Console.WriteLine($"  └─[{b.Blog.Name}] - Blog \n     └─{b.Title} - Title Published: {b.PublishedOn}" +
            $"\n\n{b.Content}\n");
        
    }
}

db.RemoveRange(db.Blogs);
db.RemoveRange(db.Posts);
db.RemoveRange(db.Users);

db.SaveChanges();
