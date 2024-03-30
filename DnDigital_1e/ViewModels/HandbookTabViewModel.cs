using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DnDigital_1e.ViewModels
{
    public class HandbookTabViewModel : MainWindowViewModel
    {
        public class QuestionAnswerPoints : ReactiveObject
        {
            private string _question;
            public string Question
            {
                get => _question;
                set => this.RaiseAndSetIfChanged(ref _question, value);
            }

            private bool _answer;
            public bool Answer
            {
                get => _answer;
                set => this.RaiseAndSetIfChanged(ref _answer, value);
            }

            private (double FirstType, double SecondType, double ThirdType) _points;
            public (double FirstType, double SecondType, double ThirdType) Points
            {
                get => _points;
                set => this.RaiseAndSetIfChanged(ref _points, value);
            }

            public QuestionAnswerPoints(string question, double firstType, double secondType, double thirdType)
            {
                _question = question;
                _answer = false;
                _points = (firstType, secondType, thirdType);
            }
        }
        public static ObservableCollection<QuestionAnswerPoints> SellerQuestions = [
            new QuestionAnswerPoints("предлагает товар энергично и напористо?",                     5,   0,   2.5),
            new QuestionAnswerPoints("не настойчив с клиентом?",                                    0,   10,  0),
            new QuestionAnswerPoints("не идет на уступки в вопросах цены?",                         10,  0,   0),
            new QuestionAnswerPoints("старается избегать возможных осложнений при работе?",         0,   5,   0),
            new QuestionAnswerPoints("уделяет полное внимание клиенту?",                            0,   2.5, 5),
            new QuestionAnswerPoints("компетентен, знает многое предмете продаж?",                  2.5, 0,   10),
            new QuestionAnswerPoints("испытываетчувство собственного преимущества перед клиентом?", 5,   0,   0),
            new QuestionAnswerPoints("открыт и честен с клиентами и коллегами?",                    0,   0,   5),
            new QuestionAnswerPoints("старается прислушиваться к мнению покупателя?",               0,   5,   2.5),
            new QuestionAnswerPoints("можно назвать честолюбивым?",                                 2.5, 0,   0),
            new QuestionAnswerPoints("во всем старается быть полезным покупателю?",                 0,   2.5, 0)
        ];
    }
}