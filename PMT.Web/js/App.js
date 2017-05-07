

var pmt = function () {
    
    var rootPath;

    var onDocumentLoadMaster = function () {
        commonUI.calculateActiveElement();
    };

    var onDocumentLoadTransaction = function () {
        transactionsUI.calculateActiveElement();
        transactionsUI.subscribeButtonEvents();
    };

    return {
        rootPath: rootPath,
        onDocumentLoadMaster: onDocumentLoadMaster,
        onDocumentLoadTransaction: onDocumentLoadTransaction
    };

}(); 
   




