using Microsoft.AspNetCore.Mvc;
using NotesAPI___with_Repository_Pattern_and_Dtos.DTOs;
using NotesAPI___with_Repository_Pattern_and_Dtos.Models;

namespace NotesAPI___with_Repository_Pattern_and_Dtos.Repositories
{
    public interface INotesRepository
    {
        Task<Note> Create(Note note);
        Task<Note> Get(int id);
        Task<IEnumerable<Note>> GetAll();
        Task<Note> Update(NoteForCreateDTO dto,int id);
        Task<string> Delete(int id);


        Task<bool> NoteExists(string title);
    }
}
