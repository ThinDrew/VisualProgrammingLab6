using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using ReactiveUI;
using Lab_6.Models;

namespace Lab_6.ViewModels
{
    public class ListNotesViewModel : ViewModelBase
    {

        DateTimeOffset currentDate;
        public DateTimeOffset CurrentDate 
        {
            get => currentDate;
            set => this.RaiseAndSetIfChanged(ref currentDate, value);
        }

        public ListNotesViewModel() 
        {
            
            SingNote = new List<SingleDateNotes>();
            ResultNotes = new ObservableCollection<NoteData>();
            BuildSomeTestNotes();

            CurrentDate = DateTime.Today;
        }

        public ObservableCollection<NoteData> ResultNotes { get; set; }
        
        public List<SingleDateNotes> SingNote { get; set; }

        public void UpdateSingleList(NoteData newNote, NoteData oldNote)
        {
        
            if (SingNote.Exists(p => p.Date == CurrentDate))
            {
                List<NoteData> list = SingNote.Find(p => p.Date == CurrentDate).Notes;
                
                if (list.Contains(oldNote))
                    list.Remove(oldNote);
                list.Add(newNote);
            }
            else
            {
                List<NoteData> listNewNote = new List<NoteData>();
                listNewNote.Add(newNote);
                SingNote.Add(new SingleDateNotes(CurrentDate, listNewNote));
            }
        }
        public void DeleteNote(NoteData aaaa)
        {
            SingNote.Find(p => p.Date == currentDate).Notes.Remove(aaaa);
            CheckDate();
        }

        public List<NoteData> generateTestNotes(string additive)
        {
            List<NoteData> Rotes = new List<NoteData>();
            Rotes.Add(new NoteData("Заметка 1" + additive, "Текст1"));
            Rotes.Add(new NoteData("Заметка 2" + additive, "Текст2"));
            Rotes.Add(new NoteData("Заметка 3" + additive, "Текст3"));
            return Rotes;
        }
        private void BuildSomeTestNotes()
        {
            SingNote.Add(new SingleDateNotes(new DateTime(2022, 3, 23), generateTestNotes(" (23)")));
            SingNote.Add(new SingleDateNotes(new DateTime(2022, 3, 22), generateTestNotes(" (22)")));
        }

        public void CheckDate()
        {
            ResultNotes.Clear();

            SingleDateNotes? tmp = SingNote.Find(p => p.Date == CurrentDate);
            if (tmp == null)
                return;

            foreach (NoteData ptr in tmp.Notes)
            {
                ResultNotes.Add(ptr);
            }
        }
    }
}
