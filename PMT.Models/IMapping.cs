using PMT.Entities;

namespace PMT.Models
{
    public interface IMapping
    {
        TransactionVM TransactionToTransactionVM(Transaction source);
        Transaction TransactionVMToTransaction(TransactionVM source);
    }
}