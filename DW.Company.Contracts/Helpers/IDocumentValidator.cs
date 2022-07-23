namespace DW.Company.Contracts.Helpers
{
    public interface IDocumentValidator
    {
        bool IsCnpjDocumentValid(string value);
        bool IsCpfDocumentValid(string value);
        bool IsAValidDocument(string value);
    }
}
