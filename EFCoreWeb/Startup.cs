using BookLibrary;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;

namespace EFCoreWeb
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<BooksContext>(options => options.UseSqlite(Configuration.GetConnectionString("BooksLibrary:SQLite")));

            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, BooksContext db)
        {
            if (db.Database.EnsureCreated())
            {
                db.Authors.Add(new Author
                {
                    Name = "Jane Austen",
                    DateOfBirth = new DateTime(1775, 12, 16),
                    Books = new List<Book>
                    {
                        new Book {Title = "Emma", YearOfPublication = 1815},
                        new Book {Title = "Persuasion", YearOfPublication = 1818},
                        new Book {Title = "Mansfield Park", YearOfPublication = 1814}
                    }
                });

                db.Authors.Add(new Author
                {
                    Name = "Ian Fleming",
                    DateOfBirth = new DateTime(1908, 5, 28),
                    Books = new List<Book>
                    {
                        new Book {Title = "Dr No", YearOfPublication = 1958},
                        new Book {Title = "Goldfinger", YearOfPublication = 1959},
                        new Book {Title = "From Russia with Love", YearOfPublication = 1957}
                    }
                });

                db.Authors.Add(new Author
                {
                    Name = "George Eliot",
                    DateOfBirth = new DateTime(1819, 11, 22),
                    Books = new List<Book>
                    {
                        new Book {Title = "Silas Marner", YearOfPublication = 1815},
                        new Book {Title = "Middlemarch", YearOfPublication = 1871}
                    }
                });

                db.SaveChanges();
            }

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Books}/{action=Index}/{id?}");
            });
        }
    }
}
