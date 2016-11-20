using System.Collections.Generic;

namespace ADL.Models {

    public interface IAnswerRepository {
        IEnumerable<Answer> Answers { get; }
        void SaveAnswer(Answer answer);
        Answer DeleteAnswer(int answerId);

    }
}
