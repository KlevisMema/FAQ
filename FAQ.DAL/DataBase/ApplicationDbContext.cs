using FAQ.DAL.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FAQ.DAL.DataBase
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Answer> Answers { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<QuestionTag> QuestionTags { get; set; }
        public DbSet<Log> Logs { get; set; }
        public DbSet<LogType> LogTypes { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<QuestionTag>()
                .HasKey(qt => new { qt.QuestionId, qt.TagId });

            builder.Entity<QuestionTag>()
                   .HasOne(qt => qt.Question)
                   .WithMany(q => q.QuestionTags)
                   .HasForeignKey(qt => qt.QuestionId);

            builder.Entity<QuestionTag>()
                   .HasOne(qt => qt.Tag)
                   .WithMany(t => t.QuestionTags)
                   .HasForeignKey(qt => qt.TagId);

            builder.Entity<Answer>()
                   .HasOne(a => a.ParentAnswer)
                   .WithMany(a => a.ChildAnswers)
                   .HasForeignKey(a => a.ParentAnswerId);
        }
    }
}