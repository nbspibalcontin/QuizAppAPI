using AutoMapper;
using QuizApp.Entity;
using QuizApp.Request.Answer;
using QuizApp.Request.Quiz;
using QuizApp.Request.QuizScore;
using QuizApp.Request.User;
using QuizApp.Response.QuizAnswerDtos;
using QuizApp.Response.QuizDtos;
using QuizApp.Response.QuizScore;

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


            //QuizScore
            CreateMap<QuizAnswer, QuestionScoreDto>()
                .ForMember(dest => dest.QuestionId, opt => opt.MapFrom(src => src.QuestionId))
                .ForMember(dest => dest.CorrectOptionIndex, opt => opt.MapFrom(src => src.Question.CorrectOptionIndex))
                .ForMember(dest => dest.SelectedOptionIndex, opt => opt.MapFrom(src => src.SelectedOptionIndex))
                .ForMember(dest => dest.IsCorrect, opt => opt.MapFrom(src => src.SelectedOptionIndex == src.Question.CorrectOptionIndex));

            CreateMap<Quiz, QuizScoreDto>()
                .ForMember(dest => dest.QuizId, opt => opt.MapFrom(src => src.QuizId))
                .ForMember(dest => dest.QuestionScores, opt => opt.MapFrom(src => src.Questions));

            //Calculate Answer Score
            CreateMap<QuizScoreDto, QuizScore>()
            .ForMember(dest => dest.QuizScoreId, opt => opt.Ignore());


            CreateMap<UserRequest, User>()
            .ForMember(dest => dest.Fullname, opt => opt.MapFrom(src => src.Fullname))
            .ForMember(dest => dest.Age, opt => opt.MapFrom(src => src.Age))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Email))
            .ForMember(dest => dest.PasswordHash, opt => opt.MapFrom(src => src.Password));

        }
    }
}
