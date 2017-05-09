

var pmt = function () {
    
    var rootPath;

    var onDocumentLoadMaster = function () {
        commonUI.calculateActiveElement();
    };

    var onDocumentLoadIndexTransaction = function () {
        transactionsUI.calculateIndexActiveElement();
        transactionsUI.addIndexButtonEvents();
    };

    var onDocumentTransactionCreate = function () {
        transactionsUI.onLoadCreateInit();
    }

    return {
        rootPath: rootPath,
        onDocumentLoadMaster: onDocumentLoadMaster,
        onDocumentLoadIndexTransaction: onDocumentLoadIndexTransaction,
        onDocumentTransactionCreate: onDocumentTransactionCreate,
    };

}(); 
   




