using Microsoft.EntityFrameworkCore;
using TV.Domain.Models;

namespace TV.Infrastructure
{
    public class TVDbContext : DbContext
    {
        public DbSet<TVShow> TVShows { get; set; }
        public DbSet<Attachment> Attachments { get; set; }
        public DbSet<Languages> Languages { get; set; }
        public DbSet<LanguagesTVShow> LanguagesTVShows { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionString = "Data Source = (localdb)\\MSSQLLocalDB; Initial Catalog = TVMvcApp";
            optionsBuilder.UseSqlServer(connectionString);
            base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TVShow>()
                .HasMany(t => t.Languages)
                .WithMany(l => l.TVShows)
                .UsingEntity<LanguagesTVShow>();
        }
        public static void CreatingInitialTestingDataBase(TVDbContext context)
        {
            context.Database.EnsureCreated();
            context.Database.EnsureDeleted();
            context.Database.Migrate();

            Languages en = new Languages() { Name = "English" };
            Languages ar = new Languages() { Name = "Arabic" };
            Languages tr = new Languages() { Name = "Turkish" };
            context.Languages.AddRange(en, ar, tr);

            Attachment minshawi = new Attachment { Path = "~/Attachments/9cd65792-0d43-493b-b644-18d525160710.png", Name = "9cd65792-0d43-493b-b644-18d525160710", FileType = ".png" };
            Attachment babAlhara = new Attachment { Path = "~/Attachments/422e736d-51d1-4ce8-994d-27782015a464.jpeg", Name = "422e736d-51d1-4ce8-994d-27782015a464", FileType = ".jpeg" };
            Attachment GOT = new Attachment { Path = "~/Attachments/5c659b96-82ef-4dea-8b64-90c05d9b9d88.jpeg", Name = "5c659b96-82ef-4dea-8b64-90c05d9b9d88", FileType = ".jpeg" };
            context.Attachments.AddRange(minshawi, babAlhara, GOT);

            context.TVShows.Add(new TVShow() { TVShowId = Guid.Parse("9cd65792-0d43-493b-b644-18d525160710"), Rating = 5, RealeseDate = DateOnly.FromDateTime(DateTime.UtcNow), Title = "Surah Al-Kahf Alminshawi", URL = "https://www.youtube.com/embed/GZqQ8AqzfQc", Languages = {tr, ar}, Attachment = minshawi });
            context.TVShows.Add(new TVShow() { TVShowId = Guid.Parse("5c659b96-82ef-4dea-8b64-90c05d9b9d88"), Rating = 3, RealeseDate = DateOnly.FromDateTime(DateTime.UtcNow), Title = "Game of Thrones", URL = "https://www.youtube.com/embed/TZE9gVF1QbA", Languages = {en, ar}, Attachment = GOT });
            context.TVShows.Add(new TVShow() { TVShowId = Guid.Parse("422e736d-51d1-4ce8-994d-27782015a464"), Rating = 4, RealeseDate = DateOnly.FromDateTime(DateTime.UtcNow), Title = "Bab-Alhara", URL = "https://www.youtube.com/embed/VBt2dK7Ng9U", Languages = {ar }, Attachment = babAlhara });

            context.SaveChanges();
        }
    }
}
