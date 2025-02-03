namespace ConsoleApp;

using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Sqlite.Design.Internal;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

[Table("entity")]
[PrimaryKey(nameof(Id))]
public class Entity
{
	[Column("id")]
	public required int Id { get; set; }
}

public class Db : DbContext
{
	public DbSet<Entity> Entities { get; init; } = null!;

	protected override void OnConfiguring(DbContextOptionsBuilder options)
	{
		options.UseSqlite(new SqliteConnectionStringBuilder
		{
			DataSource = "DotnetEfcore35568.db",
		}.ToString());

		options.LogTo(Console.WriteLine, LogLevel.Information);
	}
}

public partial class Program
{
	[SuppressMessage("Usage", "EF1001:Internal EF Core API usage.", Justification = "UwU")]
	public static async Task Main()
	{
		Write($"SQLite: {typeof(SqliteConnection).Assembly.GetName()}");
		Write($"EF Core: {typeof(DbContext).Assembly.GetName()}");
		Write($"EF Core SQLite: {typeof(SqliteDesignTimeServices).Assembly.GetName()}");

		var db = new Db();

		await db.Database.ExecuteSqlRawAsync(
			"""
			DROP TABLE IF EXISTS entity;
			CREATE TABLE IF NOT EXISTS entity (id INT NOT NULL PRIMARY KEY);

			INSERT INTO entity (id) VALUES (1);
			INSERT INTO entity (id) VALUES (2);
			INSERT INTO entity (id) VALUES (3);
			"""
		);

		var setOfInt = new HashSet<int> { 1337 };
		var setOfDouble = new HashSet<double> { 1337d };
		var setOfString = new HashSet<string> { "1337" };


		Write("\nSELECT e.Id");
		try
		{
			var e = await db.Entities.Select((e) => new { e.Id }).ToListAsync();
			Console.WriteLine(JsonSerializer.Serialize(e));
		}
		catch (Exception ex)
		{
			Console.WriteLine(ex);
		}

		Write("\nSELECT e.Id, setOfInt");
		try
		{
			var e = await db.Entities.Select((e) => new { e.Id, setOfInt }).ToListAsync();
			Console.WriteLine(JsonSerializer.Serialize(e));
		}
		catch (Exception ex)
		{
			Console.WriteLine(ex);
		}

		Write("\nSELECT e.Id, setOfDouble");
		try
		{
			var e = await db.Entities.Select((e) => new { e.Id, setOfDouble }).ToListAsync();
			Console.WriteLine(JsonSerializer.Serialize(e));
		}
		catch (Exception ex)
		{
			Console.WriteLine(ex);
		}

		Write("\nSELECT e.Id, setOfString");
		try
		{
			var e = await db.Entities.Select((e) => new { e.Id, setOfString }).ToListAsync();
			Console.WriteLine(JsonSerializer.Serialize(e));
		}
		catch (Exception ex)
		{
			Console.WriteLine(ex);
		}
	}

	public static void Write(string s)
	{
		var c = Console.ForegroundColor;
		Console.ForegroundColor = ConsoleColor.Cyan;
		Console.WriteLine(s);
		Console.ForegroundColor = c;
	}
}
