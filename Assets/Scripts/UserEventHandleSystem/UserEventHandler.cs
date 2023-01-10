using Quiz.AnswersSystem;
using Quiz.CardSystem;

namespace Quiz.UserEventHandleSystem
{
    public class UserEventHandler
    {
        private readonly AnswersController _answersController;

        public UserEventHandler(AnswersController answersController)
        {
            _answersController = answersController;
        }

        public void SubscribeEvents(CardView view)
        {
            view.CardClicked.AddListener(_answersController.CheckAnswer);
        }
    }
}