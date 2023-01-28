using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NotesKeeper.Models;

namespace NotesKeeper.Services
{
	public interface IPluralsightDataStore
	{
		Task<string> AddNoteAsync(Note note);
		Task<bool> UpdateNoteAsync(Note note);
		Task<Note> GetNoteAsync(string id);
		Task<IList<Note>> GetNotesAsync();
		Task<IList<string>> GetCoursesAsync();
	}
}

