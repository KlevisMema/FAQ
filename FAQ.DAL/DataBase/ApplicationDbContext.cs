#region Usings
using FAQ.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
#endregion

namespace FAQ.DAL.DataBase
{
    /// <summary>
    ///     A context class inheriting from <see cref="IdentityDbContext{T}"/> where T is out model 
    ///     <see cref="User"/>, this inheritance will provide all the funtionalities to operate on the database and
    ///     will expand the IdentityUser class.
    /// </summary>
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        #region Constructors
        public ApplicationDbContext()
        {
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        #endregion

        #region Db sets

        /// <summary>
        ///     A Db set of <see cref="Answer"/>
        /// </summary>
        public DbSet<Answer> Answers { get; set; }
        /// <summary>
        ///     A Db set of <see cref="Question"/>
        /// </summary>
        public DbSet<Question> Questions { get; set; }
        /// <summary>
        ///     A Db set of <see cref="Tag"/>
        /// </summary>
        public DbSet<Tag> Tags { get; set; }
        /// <summary>
        ///     A Db set of <see cref="QuestionTag"/>
        /// </summary>
        public DbSet<QuestionTag> QuestionTags { get; set; }
        /// <summary>
        ///     A Db set of <see cref="Log"/>
        /// </summary>
        public DbSet<Log> Logs { get; set; }
        /// <summary>
        ///     A Db set of <see cref="LogType"/>
        /// </summary>
        public DbSet<LogType> LogTypes { get; set; }

        #endregion

        #region Fluent Api

        protected override void
        OnModelCreating
        (
            ModelBuilder builder
        )
        {
            base.OnModelCreating(builder);

            #region Configure M:M between Question and Tag

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

            #endregion

            #region Configure the self referencing table 

            builder.Entity<Answer>()
                   .HasOne(a => a.ParentAnswer)
                   .WithMany(a => a.ChildAnswers)
                   .HasForeignKey(a => a.ParentAnswerId);

            #endregion
        }

        #endregion
    }
}