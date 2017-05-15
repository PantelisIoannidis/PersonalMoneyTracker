

var pmt = function () {
    
    var rootPath;
    var currentLocal;

    var onDocumentLoadMaster = function () {
        commonUI.calculateActiveElement();
    };

    var onDocumentLoadIndexTransaction = function () {
        transactionsUI.calculateIndexActiveElement();
        transactionsUI.addIndexButtonEvents();
        transactionsFiltersUI.onTransactionsFiltersInit();
    };

    var onDocumentLoadIndexAccounts = function () {
        moneyAccountUI.addIndexButtonEvents();
    };

    var onDocumentTransactionCreate = function () {
        transactionsUI.onLoadCreateInit();
        
    }

    return {
        rootPath: rootPath,
        onDocumentLoadMaster: onDocumentLoadMaster,
        onDocumentLoadIndexTransaction: onDocumentLoadIndexTransaction,
        onDocumentTransactionCreate: onDocumentTransactionCreate,
        onDocumentLoadIndexAccounts: onDocumentLoadIndexAccounts,


    };

}(); 
   




