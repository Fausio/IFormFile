namespace IFormFile.Models
{
    public class FileContent
    {
        public int Id { get; set; }

        public Microsoft.AspNetCore.Http.IFormFile Content { get; set; }
    }
}
