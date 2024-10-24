namespace ECC_APP_2.Models
{
    public class BusinessProposalUpload
    {
        public int UploadID { get; set; }
        public int StudentNum { get; set; } // Make sure this matches your Students table
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public DateTime UploadDate { get; set; }
    }

}
