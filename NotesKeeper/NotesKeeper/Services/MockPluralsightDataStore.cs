using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NotesKeeper.Models;

namespace NotesKeeper.Services
{
	public class MockPluralsightDataStore : IPluralsightDataStore
	{
		private static readonly List<string> mockCourses;
		private static readonly List<Note> mockNotes;
		private static int nextNoteId;

		static MockPluralsightDataStore()
		{
            mockCourses = new List<string>
            {
                "Introduction to Xamarin.Forms",
                "Android Apps with Kotlin: First App",
                "iOS Development with SwiftUI"
            };

            mockNotes = new List<Note>
            {
                new Note {
                    Id = Guid.NewGuid().ToString(),
                    Heading = "UI Code",
                    Text = "Xamarin.Forms allows UI code to be shared",
                    Course = "Introduction to Xamarin.Forms",
                },
                new Note {
                    Id = Guid.NewGuid().ToString(),
                    Heading = "Jetpack Compose",
                    Text = "Using jetpack compose improves the android UI development",
                    Course = "Android Apps with Kotlin: First App",
                },
                new Note {
                    Id = Guid.NewGuid().ToString(),
                    Heading = "UI Code",
                    Text = "SwiftUI allows us to decrease the UI developmente time on iOS",
                    Course = "iOS Development with SwiftUI",
                }
            };
		}

        public async Task<String> AddNoteAsync(Note note)
        {
            lock (this)
            {
                note.Id = nextNoteId.ToString();
                mockNotes.Add(note);
                nextNoteId++;
            }
            return await Task.FromResult(note.Id);
        }

        public async Task<bool> UpdateNoteAsync(Note note)
        {
            var noteIndex = mockNotes.FindIndex((Note arg) => arg.Id == note.Id);
            var noteFound = noteIndex != -1;
            if (noteFound)
            {
                mockNotes[noteIndex].Heading = note.Heading;
                mockNotes[noteIndex].Text = note.Text;
                mockNotes[noteIndex].Course = note.Course;
            }
            return await Task.FromResult(noteFound);
        }

        public async Task<Note> GetNoteAsync(string id)
        {
            var note = mockNotes.FirstOrDefault(courseNote => courseNote.Id == id);

            var returnNote = CopyNote(note);
            return await Task.FromResult(returnNote);
        }

        public async Task<IList<Note>> GetNotesAsync()
        {
            var returnNotes = new List<Note>();

            foreach (var note in mockNotes)
                returnNotes.Add(CopyNote(note));

            return await Task.FromResult(returnNotes);
        }

        public async Task<IList<String>> GetCoursesAsync()
        {
            return await Task.FromResult(mockCourses);
        }

        private static Note CopyNote(Note note)
        {
            return new Note { Id = note.Id, Heading = note.Heading, Text = note.Text, Course = note.Course };
        }
    }
}

