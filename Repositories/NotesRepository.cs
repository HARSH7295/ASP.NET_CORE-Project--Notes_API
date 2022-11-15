using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NotesAPI___with_Repository_Pattern_and_Dtos.DBControl;
using NotesAPI___with_Repository_Pattern_and_Dtos.DTOs;
using NotesAPI___with_Repository_Pattern_and_Dtos.Models;

namespace NotesAPI___with_Repository_Pattern_and_Dtos.Repositories
{
    public class NotesRepository : INotesRepository
    {
        private readonly DBContext _dbContext;

        public NotesRepository(DBContext context)
        {
            _dbContext = context;
        }

        public async Task<bool> NoteExists(string title)
        {
            var note = await _dbContext.Notes.FirstOrDefaultAsync(x => x.Title == title);
            if (note != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<Note> Create(Note note)
        {
            await _dbContext.Notes.AddAsync(note);
            if (await _dbContext.SaveChangesAsync() > 0)
            {
                return note;
            }
            else
            {
                return null;
            }
        }

        public async Task<string> Delete(int id)
        {
            var note = await _dbContext.Notes.FirstOrDefaultAsync(x => x.Id == id);
            _dbContext.Notes.Remove(note);
            if(await _dbContext.SaveChangesAsync() > 0)
            {
                return "Deleted";
            }
            else
            {
                return "Error";
            }

        }

        public async Task<Note> Get(int id)
        {
            var note = await _dbContext.Notes.FirstOrDefaultAsync(x => x.Id == id);
            return note;
        }

        public async Task<IEnumerable<Note>> GetAll()
        {
            var notes = await _dbContext.Notes.ToListAsync();
            return notes;
        }

        public async Task<Note> Update(NoteForCreateDTO dto, int id)
        {
            var note = await _dbContext.Notes.FirstOrDefaultAsync(x => x.Id == id);
            note.Title = dto.Title;
            note.Description = dto.Description;
            note.LastModifiedAt = DateTime.Now;
            if (await _dbContext.SaveChangesAsync() > 0)
            {
                return note;
            }
            else
            {
                return null;
            }
        }
    }
}
