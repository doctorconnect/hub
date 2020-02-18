using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Web.Mvc;

namespace doctorhubBusinessEntities.viewModels
{
    class QuizModel
    {

    }

    public class QuizVM:Base
    {
        public int QuizID { get; set; }
        public string QuizName { get; set; }

     //   [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? FromDate { get; set; }

        //   [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? ToDate { get; set; }

        public List<SelectListItem> ListOfQuiz { get; set; }
        public string Assessment { get; set; }
        public string CreatedByMailId { get; set; }


    }

    public class QuestionVM:ChoiceVM

    {
        public int QuizID { get; set; }
        public string QuizName { get; set; }
        public string QuestionType { get; set; }
        public string Answer { get; set; }
        public string AnswerText { get; set; }
        public ICollection<ChoiceVM> Choices { get; set; }

    }

    public class ChoiceVM
    {
        public bool IsAnswer { get; set; }
        public int QuestionID { get; set; }
        public string QuestionText { get; set; }
        public int ChoiceID { get; set; }
        public string ChoiceText { get; set; }
        public string ChoiceText1 { get; set; }
        public string ChoiceText2 { get; set; }
        public string ChoiceText3 { get; set; }
        public string ChoiceText4 { get; set; }
        public string ChoiceID1 { get; set; }
        public string ChoiceID2 { get; set; }
        public string ChoiceID3 { get; set; }
        public string ChoiceID4 { get; set; }
        public bool IsAnswer1 { get; set; }
        public bool IsAnswer2 { get; set; }
        public bool IsAnswer3 { get; set; }
        public bool IsAnswer4 { get; set; }
        public string ChoicestringID { get; set; }

    }


    public class AssessmentAttend : Base
    {
        public int QuizID { get; set; }
        public string QuizName { get; set; }
        public string UserName { get; set; }
        public int AttendCount { get; set; }
        public string status { get; set; }
        public int Marks { get; set; }
        
    }
}
