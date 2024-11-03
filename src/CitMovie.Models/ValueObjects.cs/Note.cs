using System;

namespace CitMovie.Models.DomainObjects
{
    public class Note
    {
        public string Value { get; private set; }

        public Note(string username, string mediaTitle, string? userText = null)
        {
            Value = GenerateFormattedNote(username, mediaTitle, userText);
        }

        private static string GenerateFormattedNote(string username, string mediaTitle, string? userText)
        {
            var createdDate = DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss");
            return $"{{\n" +
                   $"  \"User\": \"{username}\",\n" +
                   $"  \"Created\": \"{createdDate}\",\n" +
                   $"  \"NoteText\": \"{userText?.Trim() ?? string.Empty}\",\n" +
                   $"  \"Media\": \"{mediaTitle}\"\n" +
                   $"}}";
        }

        public override string ToString() => Value;

        public override bool Equals(object obj) =>
            obj is Note note && Value == note.Value;

        public override int GetHashCode() => Value.GetHashCode();
    }
}
