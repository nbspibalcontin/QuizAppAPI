using AutoMapper;
using QuizApp.Entity;
using QuizApp.Request;
using QuizApp.Request.Answer;
using QuizApp.Request.Quiz;
using QuizApp.Request.QuizScore;
using QuizApp.Response.QuizAnswerDtos;
using QuizApp.Response.QuizDtos;

namespace QuizApp
{
    public class AutoMapper : Profile
    {
        public AutoMapper()
        {
            //Quiz
            CreateMap<Quiz, QuizRequest>();
            CreateMap<QuizRequest, Quiz>();

            //Question
            CreateMap<Question, QuestionRequest>();
            CreateMap<QuestionRequest, Question>();

            //QuizScore
            CreateMap<QuizScore, QuizScoreRequest>();
            CreateMap<QuizScoreRequest, QuizScore>();

            //User
            CreateMap<User, UserRequest>();
            CreateMap<UserRequest, User>();

            //OneToMany
            CreateMap<Quiz, QuizDto>().ReverseMap();
            CreateMap<Question, QuestionDto>().ReverseMap();

            CreateMap<Quiz, QuizDto>()
           .ForMember(dest => dest.Questions, opt => opt.MapFrom(src => src.Questions));
            CreateMap<Question, QuestionDto>();


            //Answer Mapper
            CreateMap<SubmitQuizAnswersRequest, List<QuizAnswer>>()
                .AfterMap((src, dest) =>
                {
                    dest.AddRange(src.QuestionAnswers.Select(qa => new QuizAnswer
                    {
                        QuizId = src.QuizId,
                        UserId = src.UserId,
                        QuestionId = qa.QuestionId,
                        SelectedOptionIndex = qa.SelectedOptionIndex
                    }));
                });

            //Get the Answer
            CreateMap<QuizAnswer, QuestionAnswerDto>()
            .ForMember(dest => dest.QuestionText, opt => opt.MapFrom(src => src.Question.QuestionText));
        }
    }
}
