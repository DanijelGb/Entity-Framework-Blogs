namespace EFIntro;
using Microsoft.EntityFrameworkCore;

public class User
{
	public int Id { get; set; }
	public string? Username { get; set; }
	public string? Password { get; set; }
	public List<Post> Posts { get; set; } = new();

}
public class Post
{
	public int Id { get; set; }
	public string? Title { get; set; }
	public string? Content { get; set; }
	public DateOnly PublishedOn { get; set; }

	public Blog? Blog { get; set; }
	public int BlogId { get; set; }
	public User? User { get; set; }
	public int UserId { get; set; }
}
public class Blog
{
	public int Id { get; set; }
	public string? Url { get; set; }
	public string? Name { get; set; }
	public List<Post> Posts { get; set; } = new();
}
public class BloggingContext : DbContext
{
	public DbSet <Blog> Blogs { get; set; }
	public DbSet <Post> Posts { get; set; }
	public DbSet <User> Users { get; set; }

	public string DbPath { get; }
	
	public BloggingContext()
	{
		var folder = Environment.SpecialFolder.LocalApplicationData;
		string? path = Environment.GetFolderPath(folder);
		DbPath = System.IO.Path.Join(path, "cs.forts-Danijel-Grbic.db");
	}

	protected override void OnConfiguring(DbContextOptionsBuilder options) =>
		options.UseSqlite($"Data Source = {DbPath}");
}
