using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookLibrary.Flat;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace EFCoreWeb.Flat
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
            services.AddDbContext<BooksContext>(options => options.UseSqlite(Configuration.GetConnectionString("BooksLibraryFlat:SQLite")));

            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, BooksContext db)
        {
            if (db.Database.EnsureCreated())
            {

                db.Books.Add(new Book { Author = "Jane Austen", AuthorDateOfBirth = new DateTime(1775, 12, 16), Title = "Emma", YearOfPublication = 1815 });
                db.Books.Add(new Book { Author = "Jane Austen", AuthorDateOfBirth = new DateTime(1775, 12, 16), Title = "Persuasion", YearOfPublication = 1818 });
                db.Books.Add(new Book { Author = "Jane Austen", AuthorDateOfBirth = new DateTime(1775, 12, 16), Title = "Mansfield Park", YearOfPublication = 1814 });
                db.Books.Add(new Book { Author = "Ian Fleming", AuthorDateOfBirth = new DateTime(1908, 5, 28), Title = "Dr No", YearOfPublication = 1958 });
                db.Books.Add(new Book { Author = "Ian Fleming", AuthorDateOfBirth = new DateTime(1908, 5, 28), Title = "Goldfinger", YearOfPublication = 1959 });
                db.Books.Add(new Book { Author = "Ian Fleming", AuthorDateOfBirth = new DateTime(1908, 5, 28), Title = "From Russia with Love", YearOfPublication = 1957 });

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
