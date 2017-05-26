

var pmt = function () {
    
    var rootPath;
    var currentLocal;

    var onDocumentLoadHome = function () {
        transactionsFiltersUI.onTransactionsFiltersInit();
        homeCharts.onHomeChartsInit();
    };

    var onDocumentLoadMaster = function () {
        commonUI.calculateActiveElement();
        commonUI.loadingEvents();
        commonUI.loadingFinished();
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

    var onDocumentLoadIndexCategories = function () {
        categoriesUI.onLoadIndexInit();
    }

    var onDocumentLoadCategoriesCreate = function () {
        categoriesCreateUI.onLoadCreateInit();
    }

    return {
        rootPath: rootPath,
        onDocumentLoadHome:onDocumentLoadHome,
        onDocumentLoadMaster: onDocumentLoadMaster,
        onDocumentLoadIndexTransaction: onDocumentLoadIndexTransaction,
        onDocumentTransactionCreate: onDocumentTransactionCreate,
        onDocumentLoadIndexAccounts: onDocumentLoadIndexAccounts,
        onDocumentLoadIndexCategories: onDocumentLoadIndexCategories,
        onDocumentLoadCategoriesCreate: onDocumentLoadCategoriesCreate

    };

}(); 
   




