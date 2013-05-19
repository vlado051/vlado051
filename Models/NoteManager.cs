using System.Collections.ObjectModel;
using System.Linq;
using System.Web;

namespace Website.Models
{
    public class NoteManager
    {
        public Collection<Note> Notes
        {
            get
            {
                if(HttpRuntime.Cache["Notes"] == null)
                    this.loadInitialData();
                return (Collection<Note>)HttpRuntime.Cache["Notes"];
            }
        }

        private void loadInitialData()
        {
            var notes = new Collection<Note>();
            notes.Add(new Note
                            {
                                Id = 1, 
                                Title = "Set DVR for Sunday", 
                                Body = "Don't forget to record Game of Thrones!"
                            });
            notes.Add(new Note
                            {
                                Id = 2, 
                                Title = "Read MVC article", 
                                Body = "Check out the new iwantmymvc.com post"
                            });
            notes.Add(new Note
                            {
                                Id = 3, 
                                Title = "Pick up kid", 
                                Body = "Daughter out of school at 1:30pm on Thursday. Don't forget!"
                            });
            notes.Add(new Note
                            {
                                Id = 4, 
                                Title = "Paint", 
                                Body = "Finish the 2nd coat in the bathroom"
                            });
            HttpRuntime.Cache["Notes"] = notes;
        }

        public Collection<Note> GetAll()
        {
            return Notes;
        }

        public Note GetById(int id)
        {
            return Notes.Where(i => i.Id == id).FirstOrDefault();
        }

        public int Save(Note item)
        {
            if (item.Id <= 0)
                return saveAsNew(item);
            var existingNote = Notes.Where(i => i.Id == item.Id).FirstOrDefault();
            existingNote.Title = item.Title;
            existingNote.Body = item.Body;
            return existingNote.Id;
        }

        private int saveAsNew(Note item)
        {
            item.Id = Notes.Count + 1;
            Notes.Add(item);
            return item.Id;
        }
    }
}