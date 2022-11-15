using AutoMapper;
using NotesAPI___with_Repository_Pattern_and_Dtos.DTOs;
using NotesAPI___with_Repository_Pattern_and_Dtos.Models;
namespace NotesAPI___with_Repository_Pattern_and_Dtos.AutoMapperSettings
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            // mapping note to noteforreturndto
            CreateMap<Note, NoteForReturnDTO>();

            // mapping noteforcreatedto to note
            CreateMap<NoteForCreateDTO, Note>()
                .ForMember(dest => dest.CreatedAt, opt =>
                {
                    opt.MapFrom(src => DateTime.Now);
                })
                .ForMember(dest => dest.LastModifiedAt, opt =>
                {
                    opt.MapFrom(src => DateTime.Now);
                });

        }

    }
}
