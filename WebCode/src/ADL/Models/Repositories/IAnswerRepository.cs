using System.Collections.Generic;
using ADL.Models.Answers;

namespace ADL.Models.Repositories
{

    public interface IAnswerRepository {
        IEnumerable<Answer> Answers { get; }
        void SaveAnswer(Answer answer);
    }
}
