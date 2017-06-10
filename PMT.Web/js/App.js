

var pmt = function () {
    
    var rootPath;
    var currentLocal;



    var onDocumentLoadMaster = function () {
        commonUI.calculateActiveElement();
        commonUI.loadingEvents();
        commonUI.loadingFinished();
        commonUI.showNotifications();
    };

    var onDocumentLoadHome = function () {
        transactionsFiltersUI.onTransactionsFiltersInit();
        homeCharts.onHomeChartsInit();
    };

    var onDocumentLoadSettings = function () {
        themeSelector.initThemeSelector();
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
        onDocumentLoadHome: onDocumentLoadHome,
        onDocumentLoadSettings: onDocumentLoadSettings,
        onDocumentLoadMaster: onDocumentLoadMaster,
        onDocumentLoadIndexTransaction: onDocumentLoadIndexTransaction,
        onDocumentTransactionCreate: onDocumentTransactionCreate,
        onDocumentLoadIndexAccounts: onDocumentLoadIndexAccounts,
        onDocumentLoadIndexCategories: onDocumentLoadIndexCategories,
        onDocumentLoadCategoriesCreate: onDocumentLoadCategoriesCreate

    };

}(); 
   




