namespace ClassCall.Core.Interfaces
{
    public interface ISignable
    {
        byte[] Signature { get; set; }

        byte[] GetSignatureContent();
    }
}
