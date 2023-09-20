namespace AlpataTech.MeetingAppDemo.Entities
{
    public class FileUploadModel
    {
        public string FileName { get; set; }
        public string FileExtension { get; set; }
        public string ContentType { get; set; }
        public byte[] FileData {  get; set; }
    }
}
