using System.Collections.Generic;

using Quiz.CardData;

namespace Quiz.AnswersSystem
{
    public class AnswersModel
    {
        private List<CardView> _currentItems;

        public void SetUsedItems(List<CardView> items)
        {
            _currentItems = items;
        }

        public List<CardView> GetUsedItems()
        {
            return _currentItems;
        }
    }
}