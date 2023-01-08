using Quiz.AnswersSystem;
using Quiz.CardData;

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