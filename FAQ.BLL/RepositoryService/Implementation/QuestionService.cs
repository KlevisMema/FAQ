using FAQ.DAL.DataBase;
using FAQ.DAL.Models;
using FAQ.SHARED.ResponseTypes;
using Microsoft.EntityFrameworkCore;

namespace FAQ.BLL.RepositoryService.Implementation
{
    public class QuestionService
    {
        private readonly ApplicationDbContext _db;

        public QuestionService(ApplicationDbContext db)
        {
            _db = db;
        }


        public async Task<CommonResponse<IEnumerable<Question>>> GetAllQuestions()
        {
            try
            {
                var questions = await _db.Questions.Include(user => user.User).ToListAsync();

                return CommonResponse<IEnumerable<Question>>.Response("Question list retrieved succsessfully", true, System.Net.HttpStatusCode.OK, questions);
            }
            catch (Exception ex)
            {
                return CommonResponse<IEnumerable<Question>>.Response("Internal server error!", false, System.Net.HttpStatusCode.InternalServerError, Enumerable.Empty<Question>());
            }
        }

        public async Task<Question> GetQuestion(int id)
        {
            try
            {
                var question = await _db.Questions.FirstOrDefaultAsync(q => q.Id.Equals(id));

                if (question is null)
                    return new Question();

                return question;

            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Question> CreateQuestion(Question question)
        {
            try
            {
                if (question is null)
                    return new Question();

                _db.Questions.Add(question);
                await _db.SaveChangesAsync();

                return question;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Question> UpdateQuestion(Question question)
        {
            try
            {
                if (question is null)
                    return new Question();

                _db.Questions.Update(question);
                await _db.SaveChangesAsync();

                return question;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Question> DeleteQuestion(int id)
        {
            try
            {
                var question = _db.Questions.FirstOrDefault(q => q.Id.Equals(id));

                if (question is null)
                    return new Question();

                _db.Questions.Remove(question);
                await _db.SaveChangesAsync();

                return question;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}