namespace BasicIC.Models.Main
{
    public class EmailModel
    {
        public string toEmail { get; set; }
        public string subject { get; set; }
        public string body { get; set; }

        public EmailModel(string _toEmail, string _subject, string _body)
        {
            toEmail = _toEmail;
            subject = _subject;
            body = _body;
        }
    }
}