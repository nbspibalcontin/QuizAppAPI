using AutoMapper;
using QuizApp.Entity;
using QuizApp.Request;
using QuizApp.Response;

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
        }
    }
}
