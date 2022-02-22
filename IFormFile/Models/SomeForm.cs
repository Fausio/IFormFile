namespace IFormFile.Models
{
    public class SomeForm
    {
        public string Name { get; set; }
        public Microsoft.AspNetCore.Http.IFormFile File { get; set; }
    }
}
